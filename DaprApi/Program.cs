using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace DaprApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.ListenLocalhost(5001, o => o.Protocols =
                            HttpProtocols.Http1);

                        options.ListenLocalhost(5002, o=>o.Protocols =
                            HttpProtocols.Http2);
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
