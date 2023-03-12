FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["samples/AdsInfoAccessorSampleApi/AdsInfoAccessorSampleApi.csproj", "samples/AdsInfoAccessorSampleApi/"]
RUN dotnet restore "samples/AdsInfoAccessorSampleApi/AdsInfoAccessorSampleApi.csproj"
COPY . .
WORKDIR "/src/samples/AdsInfoAccessorSampleApi"
RUN dotnet build "AdsInfoAccessorSampleApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdsInfoAccessorSampleApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdsInfoAccessorSampleApi.dll"]
