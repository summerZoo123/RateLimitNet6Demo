using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;

namespace RateLimitNet6Demo
{
    public static class ServiceExtension
    {
        public static void ConfigureServices(this IServiceCollection services,IConfiguration configuration)
        {
            // needed to load configuration from appsettings.json
            services.AddOptions();

            // needed to store rate limit counters and ip rules
            services.AddMemoryCache();

            //load general configuration from appsettings.json
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

            //load ip rules from appsettings.json
            services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));

            // inject counter and rules stores
            services.AddInMemoryRateLimiting();
            //services.AddDistributedRateLimiting<AsyncKeyLockProcessingStrategy>();
            //services.AddDistributedRateLimiting<RedisProcessingStrategy>();
            //services.AddRedisRateLimiting();

            // Add framework services.
            services.AddMvc();

            // configuration (resolvers, counter key builders)
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }

        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    app.UseIpRateLimiting();

        //    app.UseMvc();
        //}
    }
}
