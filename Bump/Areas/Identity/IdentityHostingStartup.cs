using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Bump.Areas.Identity.IdentityHostingStartup))]
namespace Bump.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {});
        }
    }
}