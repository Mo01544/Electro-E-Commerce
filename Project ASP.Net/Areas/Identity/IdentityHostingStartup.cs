using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Project_ASP.Net.Areas.Identity.IdentityHostingStartup))]
namespace Project_ASP.Net.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}