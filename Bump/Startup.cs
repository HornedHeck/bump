using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Features.LiveUpdate;
using Bump.Localization;
using Bump.Services.Email;
using Bump.Utils;
using Data.Api.Local;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bump
{
    public class Startup
    {
        private const string CorsName = "SignalRCors";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(CorsName, builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("https://localhost:5001")));

            services.Configure<EmailSettings>(Configuration.GetSection(EmailSettings.SectionName));

            services.Init(Configuration);
            services.AddLocalization(options => options.ResourcesPath = "Resources");


            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };
                options.DefaultRequestCulture = new RequestCulture("en", "en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                    {new CookieRequestCultureProvider()};
            });

            services.AddSingleton<ErrorsLocalizer>();
            services
                .AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
                .AddDataAnnotationsLocalization();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCors(CorsName);

            var supported = new[] {"en", "ru"};
            var options = new RequestLocalizationOptions()
                .SetDefaultCulture("en")
                .AddSupportedCultures(supported)
                .AddSupportedUICultures(supported);
            options.RequestCultureProviders = new IRequestCultureProvider[] {new CookieRequestCultureProvider()};

            app.UseRequestLocalization(options);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            using (var scope = app.ApplicationServices.CreateScope())
            {
                await AuthInitializer.InitializeAsync(
                    userManager: scope.ServiceProvider.GetRequiredService<UserManager<BumpUser>>(),
                    roleManager: scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>(),
                    configuration: Configuration
                );
                TestInitializer.Initialize(
                    local: scope.ServiceProvider.GetService<ILocalApi>(),
                    userManager: scope.ServiceProvider.GetRequiredService<UserManager<BumpUser>>(),
                    environment: env
                );
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ThemeHub>("/subcategory");
                endpoints.MapHub<MessageHub>("/theme");
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=User}/{action=Index}");
                endpoints.MapRazorPages();
            });
        }
    }
}