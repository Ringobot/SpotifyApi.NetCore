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

    Tag = 2.5.0 (no "V")
    Title = v2.5.0
    Description = release notes

The Release will trigger a build that will pack and publish the Nuget package.