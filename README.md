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

### Nuget

Create and publish a Nuget package: <https://docs.microsoft.com/en-nz/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli>

Nuget package versioning: <https://docs.microsoft.com/en-nz/nuget/reference/package-versioning>

Nuget metadata properties: <https://docs.microsoft.com/en-us/dotnet/core/tools/csproj#nuget-metadata-properties>