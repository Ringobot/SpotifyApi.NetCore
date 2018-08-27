using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpotifyApi.NetCore;
using System.Net.Http;
using SpotifyVue.Services;

namespace SpotifyAspNetCore2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<HttpClient>(new HttpClient());
            services.AddSingleton(typeof(IAccountsService), typeof(AccountsService));
            services.AddSingleton(typeof(IArtistsApi), typeof(ArtistsApi));

            // two service types, one implementation: https://stackoverflow.com/a/41812930/610731
            services.AddSingleton(typeof(UserAuthService), typeof(UserAuthService));
            services.AddSingleton(typeof(IRefreshTokenProvider), x => x.GetService(typeof(UserAuthService)));
            services.AddSingleton(typeof(AuthStateService), typeof(AuthStateService));

            services.AddSingleton(typeof(IUserAccountsService), typeof(UserAccountsService));
            services.AddSingleton(typeof(IPlayerApi), typeof(PlayerApi));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors
                (
                    builder => builder
                    .WithOrigins("http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                );
            }
            else
            {
                //TODO: ValidateAppSettings()
                //TODO: Handle multiple settings with same name
                if (string.IsNullOrEmpty(Configuration["SPOTIFYAPI_APP_CLIENT_ORIGIN"])) 
                    throw new InvalidOperationException("App Setting SPOTIFYAPI_APP_CLIENT_ORIGIN is not set");

                app.UseHsts();
                app.UseHttpsRedirection();
                app.UseCors
                (
                    builder => builder
                    .WithOrigins(Configuration["SPOTIFYAPI_APP_CLIENT_ORIGIN"])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                );
            }

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new JsonExceptionMiddleware().Invoke
            });

            app.UseMvc();
        }
    }
}
