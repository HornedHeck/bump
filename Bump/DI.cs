using System.Security.Claims;
using Bump.Auth;
using Bump.Data;
using Bump.Data.Repo;
using Data;
using Data.Repo;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bump
{
    // ReSharper disable once InconsistentNaming
    public static class DI
    {
        public static void RegisterRepos(this IServiceCollection services)
        {
            services.AddSingleton<IThemeRepo, ThemeRepoImpl>();
            services.AddSingleton<IUserRepo, UserRepoImpl>();
            services.AddSingleton<IMediaRepo, MediaRepoImpl>();
            services.AddSingleton<IMessageRepo, MessageRepoImpl>();
        }

        public static void RegisterApi(this IServiceCollection services)
        {
            services.AddSingleton<ILocalApi, EntityLocal>();
        }

        public static void RegisterAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BumpUserContext>(options => { options.UseSqlite("Filename=Identity.db"); });
            services
                .AddIdentity<BumpUser, IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<BumpUserContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
            });

        services
                .AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
                .AddGoogle(options =>
                {
                    var googleAuthNSection =
                        configuration.GetSection("Authentication:Google");
                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });
            services.ConfigureApplicationCookie(options =>
            {
                /*options.LoginPath = "/User/Login";*/
            });
        }
    }
}