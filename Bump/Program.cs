using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Bump {

    public class Program {

        public static async Task Main( string[] args ) {
            var host = CreateHostBuilder( args ).Build();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder( string[] args ) {
            return Host.CreateDefaultBuilder( args )
                // .ConfigureLogging(builder =>
                // {
                // builder.ClearProviders();
                // builder.AddProvider(new LoggerProvider());
                // })
                .ConfigureWebHostDefaults( webBuilder => { webBuilder.UseStartup< Startup >(); } );
        }

    }

}