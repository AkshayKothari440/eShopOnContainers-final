using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OcelotApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            //CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args);
            //var builder = WebHost.CreateDefaultBuilder(args);

            builder.ConfigureServices(s => s.AddSingleton(builder))
                    .ConfigureAppConfiguration(ic => ic.AddJsonFile("configuration.json").AddJsonFile("appsettings.json"))
                    .UseStartup<Startup>()
                    .ConfigureLogging((hostingContext, loggingbuilder) =>
                    {
                        loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        loggingbuilder.AddConsole();
                        loggingbuilder.AddDebug();
                    });
            var host = builder.Build();
            return host;
        }
    }
}
