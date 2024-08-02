using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;

namespace RateLimitNet6Demo
{
    public class CustomerRateLimitConfiguration : RateLimitConfiguration
    {
        public CustomerRateLimitConfiguration(IOptions<IpRateLimitOptions> ipOptions, IOptions<ClientRateLimitOptions> clientOptions) : base(ipOptions, clientOptions)
        {

        }

        public override ICounterKeyBuilder EndpointCounterKeyBuilder { get; } = new EndpointCounterKeyBuilder();
    }

    public class EndpointCounterKeyBuilder : ICounterKeyBuilder
    {
        public string Build(ClientRequestIdentity requestIdentity, RateLimitRule rule)
        {
            //举例说明：比如我们这里的GetOneWeatherForecast/{id}，会有GetOneWeatherForecast/1，GetOneWeatherForecast/2，正常的规则是1秒内可以访问两次，那正常就是这两种请求都可访问各两次；加入下面这个限制后，这两种请求这类请求1秒内一共只能访问两次
            // This will allow to rate limit /api/values/1 and api/values/2 under same counter
            return $"_{rule.Endpoint}";
        }
    }
}
