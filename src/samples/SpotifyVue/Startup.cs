using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpotifyApi.NetCore;
using SpotifyApi.NetCore.Authorization;
using System.Net.Http;
using SpotifyVue.Data;

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
            services.AddSingleton(typeof(HttpClient), new HttpClient());
            services.AddSingleton(typeof(IAccountsService), typeof(AccountsService));
            services.AddSingleton(typeof(IArtistsApi), typeof(ArtistsApi));

            services.AddSingleton(typeof(IRefreshTokenStore), typeof(RefreshTokenStore));
            services.AddSingleton(typeof(UserAuthStorage), typeof(UserAuthStorage));
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
