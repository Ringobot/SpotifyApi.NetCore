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

Or for pre-release

    git tag -a -m "v2.5.0-beta" 2.5.0-beta
    git push origin 2.5.0-beta

Creating a Release in Github, or pushing a tag, will trigger a build that will pack and publish the Nuget 
package.