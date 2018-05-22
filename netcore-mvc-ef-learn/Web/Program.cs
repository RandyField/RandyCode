using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MODEL;
using Microsoft.Extensions.DependencyInjection;

namespace Web
{
    //ConfigureService和Configure便捷方法可以被替代,而不是非得Startup类
    //多次调用ConfigureService将不断叠加,多次调用Configure,只有最后有效
    public class Program
    {
        //创建IWebHost对象,并调用Run
        public static void Main(string[] args)
        {
            //模版代码
            // BuildWebHost(args).Run();  



            var host =BuildWebHost(args);

            // 当启动应用程序时，DbInitializer.Initialize调用EnsureCreated。
            // EnsureCreated检测到如果 DB 存在，并且如有必要将创建一个。 
            //如果 DB 中, 不有任何学生Initialize方法将添加学生。
            using (var scope=host.Services.CreateScope())
            {
                var Services=scope.ServiceProvider;
                try
                {
                    var context=Services.GetRequiredService<SchoolContext>();
                    DbInitializer.Initialize(context);
                    
                }
                catch (System.Exception ex)
                {
                    var logger=Services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex.ToString(),"An error occurred while seeding the database");
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            //指定web服务器,是指内容根路径和启用IIS集成(老版本是通过WebHostBuilder来创建IWebHost)

            //新版就通过CreateDefaultBuilder可创建现成的IWebHost,且最常见配置均已经完成
            //除此之外还负责处理一些设置(设置配置信息,并注册默认日志提供程序)
            //https://github.com/aspnet/MetaPackages/blob/rel/2.0.0/src/Microsoft.AspNetCore/WebHost.cs
            //上为源码
            WebHost.CreateDefaultBuilder(args)
                //初始化microsoft.aspnetcore.hosting.iwebhostbuilder。 
                //返回IWebHostBuilder(Build,ConfigureAppConfiguration,ConfigureServices,GetSetting,UseSetting)

                .UseStartup<Startup>()          //指定Startup类
                                                //WebHostBuilderExtensions静态类静态方法(扩展方法)
                                                //返回IWebHostBuilder接口

                // .ConfigureAppConfiguration()
                // .ConfigureLogging()
                // .AddConsole()
                // .AddDebug()
                .Build();                       //建立hosts一个webapp的IWebHost

        // public static IWebHost BuildWebHost(string[] args) =>
        //     //WebHost--Microsoft.AspNetCore
        //     WebHost.CreateDefaultBuilder(args)  //args-命令行参数
        //                                         //初始化microsoft.aspnetcore.hosting.iwebhostbuilder。 
        //                                         //返回IWebHostBuilder接口(Build,ConfigureAppConfiguration,ConfigureServices,GetSetting,UseSetting)

        //         .UseStartup<Startup>()          //指定Startup类
        //                                         //WebHostBuilderExtensions静态类静态方法(扩展方法)
        //                                         //返回IWebHostBuilder接口

        //         .Build();                       //建立hosts一个webapp的IWebHost

    }
}


//简单了解一下 CreateDefaultBuilder 中执行的最重要调用及其用途。虽然这些调用全都是由 CreateDefaultBuilder 自动执行，但最好也了解一下幕后运作机制。

//1.UseKestrel 指定应用程序应使用 Kestrel，这是基于 libuv 的跨平台 Web 服务器。
//另外一种方法是，使用 HttpSys 作为 Web 服务器 (UseHttpSys)。虽然 HttpSys 仅受 
//Windows（Windows 7/2008 R2 及更高版本）支持，但具有以下优点：允许进行 Windows 身份验证，
//可直接在 Internet 上安全运行（相比之下，如果接收 Internet 请求，Kestrel 应使用 IIS、Nginx 或 Apache 等反向代理）。

//2.UseContentRoot 为应用程序指定根目录，以便 ASP.NET Core 可以在其中找到整个网站内都有的内容（如配置文件）。
//请注意，这不同于 Web 根（其中包含的是静态文件），尽管默认情况下 Web 根是以内容根为依据 ([ContentRoot]/wwwroot)。

//3.ConfigureAppConfiguration 创建配置对象，以便应用程序可以使用此对象读取运行时设置。
//通过 CreateDefaultBuilder 调用时，它将会从 appsettings.json 文件、环境专属 
//.json 文件（若有）、环境变量和命令行参数读取应用程序配置设置。如果是在开发环境中，
//还将使用用户密钥。这是 ASP.NET Core 2.0 中新增的方法，稍后我将深入介绍。

//4.ConfigureLogging 为应用程序设置日志。通过 CreateDefaultBuilder 调用时，将添加控制台和调试日志提供程序。
//与 ConfigureAppConfiguration 一样，这也是一种新方法。

//5.UseIISIntegration 将应用程序配置为在 IIS 中运行。请注意，仍需要使用 UseKestrel。
//IIS 起到反向代理的作用，而 Kestrel 仍用作主机。此外，如果应用程序没有使用 IIS 作为反向代理，
//那么 UseIISIntegration 不会有任何效果。因此，即使应用程序在非 IIS 方案中运行，也可以安全调用这种方法。

//6.在许多情况下，Create­DefaultBuilder 提供的默认配置足够用了。
//除了调用此方法之外，还需要做的就只是调用 UseStartup<T>（其中 T 为 Startup 类型），为应用程序指定 Startup 类而已。

//7.如果 CreateDefaultBuilder 无法满足方案需求，随时都可以自定义 IWebHost 的创建方式。
//如果只需进行细微调整，可以调用 CreateDefault­Builder，再修改返回的 WebHostBuilder
//（例如，或许可以再次调用 ConfigureAppConfiguration，从而添加更多的配置源）。
//如果需要对 IWebHost 执行更大幅度的更改，可以完全跳过调用 CreateDefaultBuilder 这一步，
//直接自行构造 WebHostBuilder，就像在 ASP.NET Core 1.0 或 1.1 中一样。不过，即使这样做，
//也仍可以利用新方法 ConfigureAppConfiguration 和 ConfigureLogging。有关 Web 主机配置的更多详细信息，请访问 bit.ly/2uuSwwM。



// 如 WebHost.CreateDefaultBuilder 的源代码所示，
// 可以调用提供程序专属扩展方法（如 ILoggingBuilder 中的 AddDebug 或 AddConsole），添加日志提供程序。
// 如果使用的是 WebHost.CreateDefaultBuilder，
// 但仍希望注册除默认 Debug 和 Console 之外的日志提供程序，
// 可以对 CreateDefaultBuilder 返回的 IWebHostBuilder 额外调用 ConfigureLogging。

// 在日志配置完成且提供程序已注册后，ASP.NET Core 便会自动记录工作状况消息，
// 以处理传入请求。还可以通过依赖关系注入请求获取 ILogger 对象，从而记录自己的诊断消息（下一部分将对此进行详细介绍）。
// 调用 ILogger.Log 和级别专属变量（如 LogCritical、LogInformation 等）可用于记录消息。
