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

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Lints and fixes files
```
npm run lint
```


## Links

Model binding JSON POSTs in ASP.NET Core: <https://andrewlock.net/model-binding-json-posts-in-asp-net-core/>

Bootstrap reference: <https://getbootstrap.com/docs/4.0/components/buttons/>

Vue.js cheat-sheet: <https://vuejs-tips.github.io/cheatsheet/>

`fetch` POST: <https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch#Supplying_request_options>

Installing Docker Ubuntu 18.04 <https://www.digitalocean.com/community/tutorials/how-to-install-and-use-docker-on-ubuntu-18-04>

Install Node.js in sdk container: <https://github.com/aspnet/Announcements/issues/298>

<https://andrewlock.net/exploring-the-net-core-2-1-docker-files-dotnet-runtime-vs-aspnetcore-runtime-vs-sdk/#4-microsoft-dotnet-2-1-300-sdk>

## Vue.js

Vue CLI 3 is awesome: <https://cli.vuejs.org/>, <https://alligator.io/vuejs/using-new-vue-cli-3/>

The 12 factor app: <https://12factor.net/config>

# SPA

If there are differences in SPA and API origin, you must set the `fetch` credentials
property to `include`: <https://stackoverflow.com/a/38935838/610731>. This is used
in conjunction with CORS on the API; use a specific origin and allow credentials, e.g.

```csharp
// ASP.NET Core 2.1 (C#)
app.UseCors
(
    builder => builder
    .WithOrigins("http://localhost:8080")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);
```

More info on Same origin policy: <https://developer.mozilla.org/en-US/docs/Web/Security/Same-origin_policy#Cross-origin_data_storage_access>

## Boostrap

This project uses Bootstrap, CSS only. <https://getbootstrap.com/docs/3.3/css/>