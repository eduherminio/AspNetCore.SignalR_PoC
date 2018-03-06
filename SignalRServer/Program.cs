﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SignalRServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseConfiguration(Startup.GetConfigurationBuilder())
                .UseStartup<Startup>()
                .Build();
    }
}
