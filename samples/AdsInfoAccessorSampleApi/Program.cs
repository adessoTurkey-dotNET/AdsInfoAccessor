using System.Text.Json.Serialization;
using AdsInfoAccessor;
using AdsInfoAccessorSampleApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.DefaultIgnoreCondition =
        JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddAdsInfo();
var app = builder.Build();


app.MapGet("/", (
    IAdsInfoAccessor adsInfoAccessor) =>
{
    var info = adsInfoAccessor.GetInfo<SampleInfo>();
    return Results.Ok(info);
});

app.Run();