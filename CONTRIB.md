# Contributing

* Feature requests welcomed (log an issue)
* Bug reports welcomed (log an issue)
* Pull requests welcomed

## Pull requests

Please open a Pull Request early so that we can collaborate on the code as you write it. The PR won't 
be merged until contributors are happy with style and tests.

> See [Proposing changes to your work with pull requests](https://help.github.com/en/github/collaborating-with-issues-and-pull-requests/proposing-changes-to-your-work-with-pull-requests)
> to learn about Pull Requests.

## Coding guide, style and conventions

* All public methods must be fully documented with XML Comments
* Copy and paste text from Spotify Api Reference documentation for all methods and params so that docs
  match as closely as possible
* Include a link to the original Spotify docs in the `<remarks>` XML comment tag
* This library prefers simple types wherever possible. For example, use `string[]` instead of `List<string>`
  unless the implementation could truly benefit from a List.
* Provide generic versions of all API Methods so that users can provide their own types for deserialization
* Name API methods to closely match the name of the Spotify endpoint.
* We have opted to not use DI to resolve `SearchApi` which is proxied by several API classes. This is
  purely for simplicity; SearchApi can still be tested on its own. This may be reviewed and changed
  in a future release. 

## Writing tests

We prefer valuable tests over extensive code coverage. However all public methods must be exercised
by at least one test. Generally a few Integration tests are sufficient for testing an Endpoint method. 
For business and helper logic, Unit tests are good. If you are not sure what to test, ask for a review.

### Configuring environment for testing

Running Integration Tests requires your Application to be registered in your Spotify Developer Account.
[See this page for instructions](https://developer.spotify.com/documentation/general/guides/app-settings/).

1. Copy `src\SpotifyApi.NetCore.Tests\appsettings-template.json` to `src\SpotifyApi.NetCore.Tests\appsettings.local.json`
2. Copy and paste the Spotify API Client Id and Client Secret into the `appsettings.local.json` file.
   This file is git-ignored and should never be committed.
3. Run all tests using the Visual Studio Test Runner, VS Code Test runner or by running `dotnet test`
   from the command line.

### User profile and Current user tests

User Profile and Current User tests require a current Spotify User Bearer Access Token. The easiest
way to generate an access token is to use the "Try It" function in the Spotify Developer Reference, 
e.g. [Get Current User's Profile](https://developer.spotify.com/console/get-current-user/). 

1. Click the "Get Token" button to generate a new token that is good for one hour. 
1. Copy this token into the `SpotifyUserBearerAccessToken` app setting in the `appsettings.local.json`
   file.

You can now run the User profile tests.

## Publishing

Push the Version number in `SpotifyApi.NetCore.csproj`

```xml
<Version>2.5.0</Version>
```

Commit and push

    git commit -a -m "Packing v2.5.0"
    git push

And then create a Release in Github.

    Tag = 2.5.0 (no "v" - this must match .csproj to work with Nuget packaging)
    Title = v2.5.0
    Description = release notes

Or for pre-release

    git tag -a -m "v2.5.0-beta" 2.5.0-beta
    git push origin 2.5.0-beta

Creating a Release in Github, or pushing a tag, _should_ trigger a build that will pack and publish
the Nuget package. If the trigger does not fire you can queue manually using the tag ref, e.g. `refs/tags/2.5.0`
