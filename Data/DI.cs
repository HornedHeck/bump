using Data.Api.Local;
using Data.Repo;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    // ReSharper disable once InconsistentNaming
    public static class DI
    {
        public static void InitData(this IServiceCollection services)
        {
            services.RegisterApi();
            services.RegisterRepos();
        }

        private static void RegisterRepos(this IServiceCollection services)
        {
            services.AddSingleton<ThemeRepo>();
            services.AddSingleton<MediaRepo>();
            services.AddSingleton<MessageRepo>();
        }

        private static void RegisterApi(this IServiceCollection services)
        {
            services.AddSingleton<ILocalApi, EntityLocal>();
        }
    }
}