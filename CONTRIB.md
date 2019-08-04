# Contributing

* Feature requests welcomed (log an issue)
* Bug reports welcomed (log an issue)
* Pull requests welcomed

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
