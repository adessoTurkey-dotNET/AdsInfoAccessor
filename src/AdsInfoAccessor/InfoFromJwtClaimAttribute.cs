using System;

namespace AdsInfoAccessor
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InfoFromJwtClaimAttribute : InfoBaseAttribute
    {
        public InfoFromJwtClaimAttribute(
            string claimName,
            bool includeBearer = true,
            string headerName = "Authorization")
        {
            ClaimName = claimName;
            IncludeBearer = includeBearer;
            HeaderName = headerName;
        }

        public string ClaimName { get; set; }
        public bool IncludeBearer { get; set; }
        public string HeaderName { get; set; }
    }
}