using AdsInfoAccessor;

namespace AdsInfoAccessorSampleApi;

public class SampleInfo : IAdsInfo
{
    [InfoFromJwtClaim("sub")]
    public long UserId { get; set; }
    
    [InfoFromConfiguration("ASPNETCORE_ENVIRONMENT")]
    public Env CurrentEnv { get; set; }

    [InfoFromHeader("x-clientIp")]
    public string ClientIp { get; set; }

    [InfoFromHeader("x-deviceModel")]
    public string DeviceModel { get; set; }

    [InfoSubObject]
    public SampleSubInfo SubInfo { get; set; }
}

public class SampleSubInfo : IAdsInfo
{
    [InfoFromJwtClaim("sub")]
    public long AnOtherUserId { get; set; }
}

public enum Env
{
    Unknown,    
    Development,
    Production
}