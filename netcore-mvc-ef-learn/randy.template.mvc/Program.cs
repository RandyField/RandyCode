using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using MODEL;

namespace randy.template.mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configbuilder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("hosting.json")
                                    .Build();

            //BuildWebHost(args).Run();

            // var host = BuildWebHost(args);

            var host =  WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(configbuilder)
                .Build();

            // 当启动应用程序时，DbInitializer.Initialize调用EnsureCreated。
            // EnsureCreated检测到如果 DB 存在，并且如有必要将创建一个。 
            using (var scope = host.Services.CreateScope())
            {
                var Services = scope.ServiceProvider;
                try
                {
                    //从容器中获取一个数据库上下文，<T>
                    var context = Services.GetRequiredService<SchoolContext>();
                    DbInitializer.Initialize(context);
                }
                catch (System.Exception ex)
                {
                    var logger = Services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex.ToString(), "An error occurred while seeding the database");
                }
            }
            host.Run();
        }

        // public static IWebHost BuildWebHost(string[] args) =>
        //     WebHost.CreateDefaultBuilder(args)
        //         .UseStartup<Startup>()
        //         .Build();
    }
}
