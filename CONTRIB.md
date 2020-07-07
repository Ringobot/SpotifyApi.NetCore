# Contributing

* Feature requests welcomed (log an issue)
* Bug reports welcomed (log an issue)
* Pull requests welcomed

## Pull requests

* Open a separate branch _and_ Pull Request (PR) for each family of endpoints (Artists, Users, Player, etc), or for 
  each bug. Follow [GitHub Flow](https://guides.github.com/introduction/flow/) and don't PR from `master`.
* Keep PR's small and confined to only the files that are related to the API / Bug / Change you are
  working on. Don't be tempted to refactor the whole library while you are at it, this is production
  code used by thousands of users; changes need to be small.
* Open a Pull Request early so that we can collaborate on the code as you write it. The PR won't be 
  merged until reviews are completed and merge conflicts are resolved. Consider the reviewer (who is
  a volunteer too) by keeping changes small and relevant.

> See [Proposing changes to your work with pull requests](https://help.github.com/en/github/collaborating-with-issues-and-pull-requests/proposing-changes-to-your-work-with-pull-requests)
> to learn about Pull Requests.

## Coding guide, style and conventions

* Study existing code for guidance on style and conventions
* All public methods must be fully documented with XML Comments
* Copy and paste text from Spotify Api Reference documentation for all methods and params so that docs
  match as closely as possible
* Include a link to the original Spotify docs in the `<remarks>` XML comment tag
* Provide generic versions of all API Methods so that users can provide their own types for deserialization
* Name API methods to closely match the name of the Spotify endpoint.
* In public methods, required params should be validated, e.g. 

```csharp
if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("username");
```

* The convention in this project is to use `var` whenever the type is obvious, e.g. 

```csharp
var http = new HttpClient();
```

* This library prefers simple types wherever possible. For example, use `string[]` instead of `List<string>`
  unless the implementation could truly benefit from a List.
* Currently, `null` is meaningful and is used to indicate when a param has not been set. This may change
  (at least internally) in a future major version.
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
