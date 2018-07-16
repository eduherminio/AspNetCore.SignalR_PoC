using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SignalRServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

            if (args.Length > 0)
                builder.UseUrls(args);

            return builder.Build();
        }
    }
}
