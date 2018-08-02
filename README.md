# Spotify API .NET Core

Lightweight .NET Core wrapper for the Spotify Web API.

## Features 

* Targets .NET Standard 2.0
* Opinionated
* `async` by default
* BYO `HttpClient`
* Multi-user auth support
* MIT license

## Installation

    > dotnet add package SpotifyApi.NetCore --version 1.0.4-alpha

## Usage

Set Environment variables:
    
    SpotifyApiClientId=(SpotifyApiClientId)
    SpotifyApiClientSecret=(SpotifyApiClientSecret)

```csharp
// HttpClient and AccountsService can be reused. 
// Tokens are automatically cached and refreshed
var http = new HttpClient();
var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

// Get an artist by Spotify Artist Id
var artists = new ArtistsApi(http, accounts);
var artist = await artists.GetArtist("1tpXaFf2F55E7kVJON4j4G");
string artistName = artist.Name;
Trace.WriteLine($"Artist.Name = {artistName}");

// Get recommendations based on seed Artist Ids
var browse = new BrowseApi(http, accounts);
var result = await browse.GetRecommendations(new[] { "1tpXaFf2F55E7kVJON4j4G", "4Z8W4fKeB5YxbusRsdQVPb" }, null, null);
string firstTrackName = result.Tracks[0].Name;
Trace.WriteLine($"First recommendation = {firstTrackName}");
```

See tests for more usage examples.

## Spotify Web API Coverage

| Spotify API | Endpoints | Implemented | % |
| :---------- | --------: | ----------: | -: |
| Albums | 3 | 0 | 0% |
| Artists | 5 | 1 | 20% |
| Browse | 6 | 0.5 | 8% |
| Follow | 7 | 0 | 0% |
| Library | 8 | 0 | 0% |
| Personalization | 1 | 0 | 0% |
| Player | 13 | 1 | 8% |
| Playlists | 12 | 1.5 | 13% |
| Search | 1 | 0 | 0% |
| Tracks | 5 | 0 | 0% |
| Users Profile | 2 | 0 | 0% |
| **Total** | **63** | **4** | **6%** |

Pull requests and feature requests welcomed!

## Publishing

Push the Version number in `SpotifyApi.NetCore.csproj`

```xml
<Version>1.0.2-alpha</Version>
```

Commit and push

    git commit -a -m "Packing v1.0.2-alpha"
    git push

Pack

    dotnet pack src/Spotify.NetCore

Publish

    dotnet nuget push .\src\Spotify.NetCore\bin\Debug\SpotifyApi.NetCore.1.0.2-alpha.nupkg -k (api-key) -s https://api.nuget.org/v3/index.json

## Links

### Spotify

Auth guide <https://developer.spotify.com/documentation/general/guides/authorization-guide/>

### .NET Core

Concurrent Dictionary <https://docs.microsoft.com/en-nz/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=netframework-4.7.1#remarks>

### HttpClient factory

<https://blogs.msdn.microsoft.com/webdev/2018/02/28/asp-net-core-2-1-preview1-introducing-httpclient-factory/>

### Configuration in .NET Core

Setting up .NET Core Configuration Providers: <https://blogs.msdn.microsoft.com/premier_developer/2018/04/26/setting-up-net-core-configuration-providers/>

Easy Configuration Binding in ASP.NET Core: <https://weblog.west-wind.com/posts/2017/Dec/12/Easy-Configuration-Binding-in-ASPNET-Core-revisited>

Configuration in ASP.NET Core: <https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.1&tabs=basicconfiguration>

### Nuget

Create and publish a Nuget package: <https://docs.microsoft.com/en-nz/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli>

Nuget package versioning: <https://docs.microsoft.com/en-nz/nuget/reference/package-versioning>

Nuget metadata properties: <https://docs.microsoft.com/en-us/dotnet/core/tools/csproj#nuget-metadata-properties>

<https://www.hanselman.com/blog/AddingResilienceAndTransientFaultHandlingToYourNETCoreHttpClientWithPolly.aspx>