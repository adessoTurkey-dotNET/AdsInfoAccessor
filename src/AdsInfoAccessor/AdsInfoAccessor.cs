using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace AdsInfoAccessor
{
    internal class AdsInfoAccessor : IAdsInfoAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public AdsInfoAccessor(
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._configuration = configuration;
        }

        public TInfo GetInfo<TInfo>() where TInfo : IAdsInfo, new()
        {
            return (TInfo)this.GetInfoObject(typeof(TInfo));
        }

        private object GetInfoObject(
            Type infoType)
        {
            var info = Activator.CreateInstance(infoType);
            foreach (var property in info.GetType().GetProperties()
                         .Where(x => x.CustomAttributes.Any(a => a.AttributeType.IsSubclassOf(typeof(InfoBaseAttribute)))))
            {
                var baseAttribute = property.GetCustomAttribute<InfoBaseAttribute>();
                switch (baseAttribute)
                {
                    case InfoFromConfigurationAttribute infoBaseAttribute:
                    {
                        var value = this._configuration[infoBaseAttribute.Key];
                        property.SetValue(info, ConvertToPropertyType(property, value));
                        break;
                    }
                    case InfoFromHeaderAttribute infoFromHeaderAttribute:
                    {
                        var value = this._httpContextAccessor.HttpContext.Request.Headers[infoFromHeaderAttribute.HeaderName].ToString();
                        property.SetValue(info, ConvertToPropertyType(property, value));
                        break;
                    }
                    case InfoFromJwtClaimAttribute infoFromJwtClaimAttribute:
                    {
                        var token = this._httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString()
                            .Replace("Bearer ", "")
                            .Replace("bearer  ", "");

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                        var claimValue = securityToken.Claims.FirstOrDefault(x => x.Type == infoFromJwtClaimAttribute.ClaimName)?.Value;
                        property.SetValue(info, ConvertToPropertyType(property, claimValue));
                        break;
                    }
                    case InfoSubObjectAttribute _:
                    {
                        var subObject = this.GetInfoObject(property.PropertyType);
                        property.SetValue(info, subObject);
                        break;
                    }
                }
            }

            return info;
        }


        private static object ConvertToPropertyType(
            PropertyInfo property,
            object value)
        {
            if (property.PropertyType == typeof(string))
            {
                return value;
            }
            else if (property.PropertyType.IsEnum)
            {
                return Enum.Parse(property.PropertyType, value.ToString());
            }
            else if (property.PropertyType == typeof(int))
            {
                return Convert.ToInt32(value);
            }
            else if (property.PropertyType == typeof(long))
            {
                return Convert.ToInt64(value);
            }
            else if (property.PropertyType == typeof(short))
            {
                return Convert.ToInt16(value);
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                return Convert.ToDateTime(value);
            }
            else if (property.PropertyType == typeof(DateTimeOffset))
            {
                return DateTimeOffset.Parse(value.ToString());
            }
            else
            {
                throw new ArgumentException($"{property.PropertyType.FullName} in property {property.Name} is not supported.");
            }
        }
    }
}
