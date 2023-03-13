<!-- PROJECT SHIELDS -->
<div style="text-align: center">

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![NuGet][nuget-shield]][nuget-url]
[![MIT License][license-shield]][license-url]
</div>


<!-- PROJECT LOGO -->
<br />
<div style="text-align: center; align-items: center; align-content: center" >
<img src="https://github.com/adessoTurkey-dotNET/AdsInfoAccessor/blob/main/logo.png" alt="Logo" width="200" height="200">


<h3 align="center">AdsInfoAccessor</h3>

  <p style="text-align: center; align-content: center; align-items: center">
<b>AdsInfoAccessor</b> is a library that provides single point to get common info in your application pipeline.
 <br />
    <a href="https://github.com/adessoTurkey-dotNET/AdsInfoAccessor/issues">Report Bug or Request Feature</a>
    ·
    <a href="mailto:mail@anildursunsenel.com?subject=AdsInfoAccessor">Contect Me Via Mail</a>
    ·
    <a href="https://www.linkedin.com/in/anıl-dursun-şenel">Contect Me Via Linkedin</a>
  </p>
</div>

# Features - Data providers
1. Get data from configuration
2. Get data from request headers
3. Get data from bearer token.

# Get It Started 

## Configuration
You can easy configure and use **AdsInfoAccessor** in you .NET Core & .NET applications. 

If you're using .NET 5 & alter versions, you can put the following line of code in your `Program.cs` file.



````csharp

using AdsInfoAccessor;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAdsInfo();

...

app.Run();
````

If you're using .NET Core app, you can put the following line in your` Startup.cs` file **ConfigureServices** method.

````csharp

using AdsPush.Extensions;
...

public override void ConfigureServices(IServiceCollection services)
{
    services.AddAdsPush<MyProvider>();
}  
...    

````

## Usage

- Create your info model.


````csharp

using AdsInfoAccessor;

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

````
- Use `IAdsInfoAccessor` interface to get info instance.

````csharp

app.MapGet("/", (
    IAdsInfoAccessor adsInfoAccessor) =>
{
    var info = adsInfoAccessor.GetInfo<SampleInfo>();
    return Results.Ok(info);
});

````

Now, you can test by making the following sample request.

````
curl --location 'http://localhost:5181' \
--header 'x-deviceModel: iPhone 12 Pro Max' \
--header 'x-clientIp: 52.52.52.52' \
--header 'Authorization: Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2Nzg2MjU4MDksImV4cCI6MTcxMDE2MTgwOSwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoiMTIxMjEiLCJHaXZlbk5hbWUiOiJKb2hubnkiLCJTdXJuYW1lIjoiUm9ja2V0IiwiRW1haWwiOiJqcm9ja2V0QGV4YW1wbGUuY29tIiwiUm9sZSI6WyJNYW5hZ2VyIiwiUHJvamVjdCBBZG1pbmlzdHJhdG9yIl19.Urn9phKqdztKf7QI7CmAjCpWB9pBjZchGFTTH-Swdwo' \
--data ''

````
- [Sample Project](https://github.com/adessoTurkey-dotNET/AdsInfoAccessor/tree/main/samples/AdsInfoAccessorSampleApi)


<!-- MARKDOWN LINKS & IMAGES -->
[contributors-shield]: https://img.shields.io/github/contributors/adessoTurkey-dotNET/AdsInfoAccessor.svg?style=for-the-badge
[contributors-url]: https://github.com/adessoTurkey-dotNET/AdsInfoAccessor/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/adessoTurkey-dotNET/AdsInfoAccessor.svg?style=for-the-badge
[forks-url]: https://github.com/adessoTurkey-dotNET/AdsInfoAccessor/network/members
[stars-shield]: https://img.shields.io/github/stars/adessoTurkey-dotNET/AdsInfoAccessor.svg?style=for-the-badge
[stars-url]: https://github.com/adessoTurkey-dotNET/AdsInfoAccessor/stargazers
[issues-shield]: https://img.shields.io/github/issues/adessoTurkey-dotNET/AdsInfoAccessor.svg?style=for-the-badge
[issues-url]: https://github.com/adessoTurkey-dotNET/AdsInfoAccessor/issues
[license-shield]: https://img.shields.io/github/license/adessoTurkey-dotNET/AdsInfoAccessor.svg?style=for-the-badge
[license-url]: https://github.com/adessoTurkey-dotNET/AdsInfoAccessor/blob/main/LICENSE
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/tolgacakirx
[.Net]: https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[.Net-shield]: https://img.shields.io/badge/.NET-5C2D91?
[nuget-shield]: https://img.shields.io/nuget/v/AdsInfoAccessor?style=for-the-badge
[nuget-url]: https://www.nuget.org/packages/AdsInfoAccessor
