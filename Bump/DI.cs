using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Policy;
using Bump.Auth;
using Bump.Data;
using Bump.Data.Repo;
using Data;
using Data.Repo;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
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
                .AddIdentity<BumpUser, IdentityRole>(options => { })
                // .AddDefaultUI()
                .AddClaimsPrincipalFactory<ClaimsFactory>()
                .AddEntityFrameworkStores<BumpUserContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => { options.SignIn.RequireConfirmedEmail = false; });

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
                options.AccessDeniedPath = AuthConstants.AccessDeniedPath;
                options.LoginPath = AuthConstants.LoginPath;
                options.LogoutPath = AuthConstants.LogoutPath;
            });


            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });
        }

        public static void RegisterCulture(this IServiceCollection services)
        {
            
            
        }
    }
}