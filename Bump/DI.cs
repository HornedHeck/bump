using Bump.Data;
using Bump.Data.Repo;
using Data;
using Data.Repo;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            services.AddSingleton<ILocalApi, TempLocalApiImpl>();
        }

        public static void RegisterAuth(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/User/Login");
                });
        }

    }
}