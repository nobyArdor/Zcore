﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Zcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://*:80")
                .UseStartup<Startup>()
        ;
    }
}
