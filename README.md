# Spotify API .NET Core

Lightweight .NET Core wrapper for the Spotify Web API.

* Opinionated
* Async by default
* Raw responses
* MIT

## Installation

    > dotnet add package SpotifyApi.NetCore --version 1.0.3-alpha

## Usage

```csharp
// Set Environment variables:
// SpotifyApiClientId=(SpotifyApiClientId)
// SpotifyApiClientSecret=(SpotifyApiClientSecret)

// HttpClient and ClientCredentials can be reused. 
// Client creds are cached and refreshed
var http = new HttpClient();
var auth = new ClientCredentialsAuthorizationApi(http);
var api = new ArtistsApi(http, auth);

// Get an artist by Spotify Artist Id
dynamic response = await api.GetArtist("1tpXaFf2F55E7kVJON4j4G");
```

See tests for more usage examples.

## Publishing

Push the Version number in `SpotifyApi.NetCore.csproj`

    <Version>1.0.2-alpha</Version>

Commit and push

    git commit -a -m "Packing v1.0.2-alpha"
    git push

Pack

    dotnet pack src/Spotify.NetCore

Publish

    dotnet nuget push .\src\Spotify.NetCore\bin\Debug\SpotifyApi.NetCore.1.0.2-alpha.nupkg -k (api-key) -s https://api.nuget.org/v3/index.json

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