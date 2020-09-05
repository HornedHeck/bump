using Bump.Data;
using Bump.Data.Repo;
using Data;
using Data.Repo;
using Microsoft.Extensions.DependencyInjection;

namespace Bump
{
    // ReSharper disable once InconsistentNaming
    public static class DI
    {
        public static void RegisterRepos(this IServiceCollection services)
        {
            services.AddTransient<IThemeRepo, ThemeRepoImpl>();
            services.AddTransient<IUserRepo, UserRepoImpl>();
            services.AddTransient<IMediaRepo, MediaRepoImpl>();
            services.AddTransient<IMessageRepo, MessageRepoImpl>();
        }

        public static void RegisterApi(this IServiceCollection services)
        {
            services.AddTransient<ILocalApi, TempLocalApiImpl>();
        }
    }
}