# Spotify API .NET Core

Lightweight .NET Core wrapper for the Spotify Web API.

* Opinionated
* Async by default
* Raw responses
* MIT

## Publishing

Push the Version number in `SpotifyApi.NetCore.csproj`

    <Version>1.0.1-alpha</Version>

Commit and push

    git commit -a -m "Packing v1.0.1-alpha"
    git push

Pack

    dotnet pack src

Publish

    dotnet nuget push .\src\bin\Debug\Feather.SpotifyApi.NetCore.1.0.1-alpha.nupkg -k (api-key) -s https://api.nuget.org/v3/index.json

## Links

### Configuration in .NET Core

Setting up .NET Core Configuration Providers: <https://blogs.msdn.microsoft.com/premier_developer/2018/04/26/setting-up-net-core-configuration-providers/>

Easy Configuration Binding in ASP.NET Core: <https://weblog.west-wind.com/posts/2017/Dec/12/Easy-Configuration-Binding-in-ASPNET-Core-revisited>

Configuration in ASP.NET Core: <https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.1&tabs=basicconfiguration>

### Nuget

Create and publish a Nuget package: <https://docs.microsoft.com/en-nz/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli>

Nuget package versioning: <https://docs.microsoft.com/en-nz/nuget/reference/package-versioning>

Nuget metadata properties: <https://docs.microsoft.com/en-us/dotnet/core/tools/csproj#nuget-metadata-properties>

<https://www.hanselman.com/blog/AddingResilienceAndTransientFaultHandlingToYourNETCoreHttpClientWithPolly.aspx>