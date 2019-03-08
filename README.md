# Spotify API .NET Core

Lightweight .NET Core wrapper for the Spotify Web API.

### Build status

[![Build Status](https://dev.azure.com/daniellarsennz/SpotifyApi.NetCore/_apis/build/status/SpotifyApi.NetCore-Build)](https://dev.azure.com/daniellarsennz/SpotifyApi.NetCore/_build/latest?definitionId=9)

## Features 

* Targets .NET Standard 2.0
* `async` by default
* BYO `HttpClient`
* Multi-user auth support
* MIT license

## Installation

    > dotnet add package SpotifyApi.NetCore

## Basic usage

Set Environment variables:
    
    SpotifyApiClientId=(SpotifyApiClientId)
    SpotifyApiClientSecret=(SpotifyApiClientSecret)

```csharp
// HttpClient and AccountsService can be reused. 
// Tokens are automatically cached and refreshed
var http = new HttpClient();
var accounts = new AccountsService(http);

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

See tests and samples for more usage examples.

> There is a working demo using the sample project here: <https://spotifyaspnetcore.z5.web.core.windows.net/>

## In this repo

| Path | Remarks |
| ---- | ------- |
| `src/SpotifyApi.NetCore` | SpotifyApi.NetCore project |
| `src/SpotifyApi.NetCore.Tests` | Tests |
| `samples/SpotifyVue` | Sample project using ASP.NET Core + Vue.js. [Try the  demo](https://spotifyaspnetcore.z5.web.core.windows.net/). |

## Spotify Web API Coverage

| Spotify API | Endpoints | Implemented | % | |
| :---------- | --------: | ----------: | -: | - |
| Albums | 3 | 3 | 100% | ✅ |
| Artists | 5 | 4 | 80% |
| Browse | 6 | 0.5 | 8% |
| Follow | 7 | 0 | 0% |
| Library | 8 | 0 | 0% |
| Personalization | 1 | 0 | 0% |
| Player | 13 | 3 | 23% |
| Playlists | 12 | 2 | 17% |
| Search | 1 | 1 | 100% | ✅ |
| Tracks | 5 | 5 | 100% | ✅ |
| Users Profile | 2 | 0 | 0% |
| **Total** | **63** | **18.5** | **29%** |

Feature requests welcomed! (log an issue)

## Contributors

Thanks to @aevansme for his contributions!

Contributions welcomed. Read [CONTRIB.md](./CONTRIB.md)