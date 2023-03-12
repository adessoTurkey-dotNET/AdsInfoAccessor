using Microsoft.Extensions.DependencyInjection;

namespace AdsInfoAccessor
{
    public static class AdsInfoAccessorExtension
    {
        public static IServiceCollection AddAdsInfo(
            this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IAdsInfoAccessor, AdsInfoAccessor>();

            return services;
        }
    }
}