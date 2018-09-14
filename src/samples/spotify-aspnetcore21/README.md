# Spotify API + Vue.js

## Deployment

World. Of. Pain

* Linux App Service
* Select .NET 2.0
* Startup command = `dotnet SpotifyVue.dll`
* Deploy using local git
* `.deployment` file to specify project file as `/src/samples/SpotifyVue`
* Turn on Diagnostic Logs with a 1 day retention
* Download Zipped logs from Kudu to diagnose, e.g. <https://spotifyapidotnetcore.scm.azurewebsites.net/api/logs/docker/zip>
* Manually uploaded the View files! (Need to include them in project file)

```xml
    <MvcRazorExcludeViewFilesFromPublish>false</MvcRazorExcludeViewFilesFromPublish>
    <MvcRazorExcludeRefAssembliesFromPublish>false</MvcRazorExcludeRefAssembliesFromPublish>
```

<https://github.com/aspnet/MvcPrecompilation/issues/107>

<https://docs.microsoft.com/en-us/aspnet/core/migration/20_21?view=aspnetcore-2.1>

<https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/visual-studio-publish-profiles?view=aspnetcore-2.0&tabs=aspnetcore2x#include-files>

## Links

### Docker

Dockerise an ASP.NET Core application: <https://docs.docker.com/engine/examples/dotnetcore/#create-a-dockerfile-for-an-aspnet-core-application>

### Other

Model binding JSON POSTs in ASP.NET Core: <https://andrewlock.net/model-binding-json-posts-in-asp-net-core/>

Bootstrap reference: <https://getbootstrap.com/docs/4.0/components/buttons/>

Vue.js cheat-sheet: <https://vuejs-tips.github.io/cheatsheet/>

`fetch` POST: <https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch#Supplying_request_options>

Installing Docker Ubuntu 18.04 <https://www.digitalocean.com/community/tutorials/how-to-install-and-use-docker-on-ubuntu-18-04>

Install Node.js in sdk container: <https://github.com/aspnet/Announcements/issues/298>

<https://andrewlock.net/exploring-the-net-core-2-1-docker-files-dotnet-runtime-vs-aspnetcore-runtime-vs-sdk/#4-microsoft-dotnet-2-1-300-sdk>