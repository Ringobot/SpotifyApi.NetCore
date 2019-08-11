# Spotify API .NET Core

Lightweight .NET Core wrapper for the Spotify Web API.

### Build status

[![Build Status](https://dev.azure.com/daniellarsennz/SpotifyApi.NetCore/_apis/build/status/SpotifyApi.NetCore-Build)](https://dev.azure.com/daniellarsennz/SpotifyApi.NetCore/_build/latest?definitionId=9)

## Features 

* Targets .NET Standard 2.0
* `async` by default
* BYO `HttpClient`
* Authorization support (App and User flows)
* MIT license
* Fully documented

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
Artist artist = await artists.GetArtist("1tpXaFf2F55E7kVJON4j4G");
string artistName = artist.Name;
Trace.WriteLine($"Artist.Name = {artistName}");

// Get recommendations based on seed Artist Ids
var browse = new BrowseApi(http, accounts);
RecommendationsResult result = await browse.GetRecommendations(new[] { "1tpXaFf2F55E7kVJON4j4G", "4Z8W4fKeB5YxbusRsdQVPb" }, null, null);
string firstTrackName = result.Tracks[0].Name;
Trace.WriteLine($"First recommendation = {firstTrackName}");

// Page through a list of tracks in a Playlist
var playlists = new PlaylistsApi(http, accounts);
int limit = 100;
PlaylistPaged playlist = await playlists.GetTracks("4h4urfIy5cyCdFOc1Ff4iN", limit: limit);
int offset = 0;
int j = 0;
// using System.Linq
while (playlist.Items.Any())
{
    for (int i = 0; i < playlist.Items.Length; i++)
    {
        Trace.WriteLine($"Track #{j += 1}: {playlist.Items[i].Track.Artists[0].Name} / {playlist.Items[i].Track.Name}");
    }
    offset += limit;
    playlist = await playlists.GetTracks("4h4urfIy5cyCdFOc1Ff4iN", limit: limit, offset: offset);
}
```

### User Authorization

```csharp
// Get a list of a User's devices
// This requires User authentication and authorization. 
// A `UserAccountsService` is provided to help with this.

// HttpClient and UserAccountsService can be reused. 
// Tokens can be cached by your code
var http = new HttpClient();
var accounts = new UserAccountsService(http);

// See https://developer.spotify.com/documentation/general/guides/authorization-guide/#authorization-code-flow
//  for an explanation of the Authorization code flow

// Generate a random state value to use in the Auth request
string state = Guid.NewGuid().ToString("N");
// Accounts service will derive the Auth URL for you
string url = accounts.AuthorizeUrl(state, new[] { "user-read-playback-state" });

/*
    Redirect the user to `url` and when they have auth'ed Spotify will redirect to your reply URL
    The response will include two query parameters: `state` and `code`.
    For a full working example see `SpotifyApi.NetCore.Samples`.
*/

// Check that the request has not been tampered with by checking the `state` value matches
if (state != query["state"]) throw new ArgumentException();

// Use the User accounts service to swap `code` for a Refresh token
BearerAccessRefreshToken token = await accounts.RequestAccessRefreshToken(query["code"]);

// Use the Bearer (Access) Token to call the Player API
var player = new PlayerApi(http, accounts);
Device[] devices = await player.GetDevices(accessToken: token.AccessToken);

foreach(Device device in devices)
{
    Trace.WriteLine($"Device {device.Name} Status = {device.Type} Active = {device.IsActive}");
}

```

See tests and `SpotifyApi.NetCore.Samples` for more usage examples.

> There is a working demo using the sample project here: <https://spotifyaspnetcore.z5.web.core.windows.net/>

## Spotify Web API Coverage

| Spotify API | Endpoints | Implemented | % | |
| :---------- | --------: | ----------: | -: | - |
| Albums | 3 | 3 | 100% | ✅ |
| Artists | 5 | 4 | 80% |
| Browse | 7 | 1 | 14% |
| Follow | 7 | 0 | 0% |
| Library | 8 | 0 | 0% |
| Personalization | 1 | 0 | 0% |
| Player | 13 | 10 | 77% |
| Playlists | 12 | 2 | 17% |
| Search | 1 | 1 | 100% | ✅ |
| Tracks | 5 | 5 | 100% | ✅ |
| Users Profile | 2 | 0 | 0% |
| **Total** | **64** | **26** | **41%** |

Feature requests welcomed! (log an issue)

## Contributors

Thanks to @aevansme for his contributions!

Contributions welcomed. Read [CONTRIB.md](./CONTRIB.md)