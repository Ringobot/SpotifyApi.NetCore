using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpotifyApi.NetCore;
using System.Net.Http;
using SpotifyVue.Services;

namespace SpotifyVue
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
            services.AddMvc();
            services.AddSingleton<HttpClient>(new HttpClient());
            services.AddSingleton(typeof(IAccountsService), typeof(AccountsService));
            services.AddSingleton(typeof(IArtistsApi), typeof(ArtistsApi));

            // two service types, one implementation: https://stackoverflow.com/a/41812930/610731
            services.AddSingleton(typeof(SpotifyAuthService), typeof(SpotifyAuthService));
            services.AddSingleton(typeof(IRefreshTokenStore), x => x.GetService(typeof(SpotifyAuthService)));

            services.AddSingleton(typeof(IUserAccountsService), typeof(UserAccountsService));
            services.AddSingleton(typeof(IPlayerApi), typeof(PlayerApi));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new JsonExceptionMiddleware().Invoke
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
