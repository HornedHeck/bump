using System;
using Bump.Auth;
using Bump.Features.LiveUpdate;
using Bump.Utils;
using Data;
using Data.Api.Live;
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
        public static void Init(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR();
            services.AddSingleton<ILiveUpdate, SignalRLive>();
            services.InitData();
            services.RegisterAuth(configuration);
            services.RegisterFileManager();
        }

        private static void RegisterAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BumpUserContext>(options => { options.UseSqlite("Filename=Identity.db"); });

            services
                .AddIdentity<BumpUser, IdentityRole>(options => { })
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

        private static void RegisterFileManager(this IServiceCollection services)
        {
            services.AddSingleton<FileManager>();
        }
    }
}