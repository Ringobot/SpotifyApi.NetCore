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
* Fully XML documented

## Installation

Install the latest version using dotnet CLI:

    > dotnet add package SpotifyApi.NetCore

Install using Package Manager Console:

    > Install-Package SpotifyApi.NetCore

## Version 3

Version 3 of `SpotifyApi.NetCore` is a major version overhaul with many improvements including:

* Removal of multi-user authentication in favour of bring-your-own auth
* Simplification of Authentication services
* Consistent approach to paging and auth params throughout the library
* Removal of many overloads in favour of optional params
* Complete XML comment documentation of public methods including links to Spotify reference docs
* Separate [SpotifyApi.NetCore.Samples] repo

> It is highly recommended that users upgrade to `SpotifyApi.NetCore` >= v3.0.1 as soon as possible. 
> Version >= 2.4.7 will be supported until the next major version ships.

### Upgrading from v2 to v3

There are breaking changes in v3 but, for most users, upgrading should be straight-forward. Some minor 
refactoring may be required, e.g.

* Most Authorization objects have moved from namespace `SpotifyApi.NetCore` to `SpotifyApi.NetCore.Authorization`
* `Models.Image.Height` and `Models.Image.Width` have changed from `int` to `int?`
* `Models.CurrentPlaybackContext.ProgressMs` has changed from `long` to `long?`

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

See tests and [SpotifyApi.NetCore.Samples] for more usage examples.

## Spotify Web API Coverage

| Spotify API | Endpoints | Implemented | % | |
| :---------- | --------: | ----------: | -: | - |
| Albums | 3 | 3 | 100% | ✅ |
| Artists | 5 | 5 | 100% | ✅ |
| Browse | 7 | 7 | 100% | ✅ |
| Episodes | 2 | 0 | 0% | |
| Follow | 7 | 1 | 14% |
| Library | 8 | 0 | 0% |
| Personalization | 1 | 0 | 0% |
| Player | 13 | 10 | 77% |
| Playlists | 12 | 2 | 17% |
| Search | 1 | 1 | 100% | ✅ |
| Shows | 3 | 0 | 0% | |
| Tracks | 5 | 5 | 100% | ✅ |
| Users Profile | 2 | 2 | 1000% | ✅ |
| **Total** | **69** | **36** | **52%** |

Feature requests welcomed! (create an issue)

## Maintainer

This project is actively maintained by @DanielLarsenNZ. The easiest way to get in touch is to create an issue. But you can also email daniel@larsen.nz.

## Contributors

Huge thanks to **@aevansme**, **@brandongregoryscott** and **@akshays2112** for their contributions!

Contributions welcomed. Read [CONTRIB.md](./CONTRIB.md)

[SpotifyApi.NetCore.Samples]:https://github.com/Ringobot/SpotifyApi.NetCore.Samples
