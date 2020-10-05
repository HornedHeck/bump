using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Data;
using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bump
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await AuthInitializer.InitializeAsync(
                    userManager: services.GetRequiredService<UserManager<BumpUser>>(),
                    roleManager: services.GetRequiredService<RoleManager<IdentityRole>>()
                );
                TestInitializer.Initialize(
                    local: services.GetService<ILocalApi>(),
                    userManager: services.GetRequiredService<UserManager<BumpUser>>(),
                    environment: services.GetRequiredService<IWebHostEnvironment>()
                );
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}