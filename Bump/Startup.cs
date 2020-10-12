using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Bump.Data;
using Bump.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bump
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
            services.RegisterApi();
            services.RegisterRepos();
            services.RegisterAuth(Configuration);
            services.AddSingleton<FileManager>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services
                .AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(type);
                });
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var supportedCultures = new CultureInfo[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru"),
            };

            app.UseRequestLocalization(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(
                    supportedCultures.First(),
                    supportedCultures.First()
                );
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=User}/{action=Index}");
                endpoints.MapRazorPages();
            });
        }
    }
}