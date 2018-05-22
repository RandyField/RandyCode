using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

//EF
using Microsoft.EntityFrameworkCore;

//使用IConfiguration接口,提供运行时配置设置
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Logging;
using MODEL;


namespace Web
{
    //app可以单独另外定义Startup类用于不同的环境
    //例如可以定义未StartupDevelopment
    //运行时在选中startup类
    //当 ASP.NET Core 应用启动后时， Startup 类启动应用程序。 如果一个类Startup{EnvironmentName}存在，将该调用类EnvironmentName:
    public class Startup
    {
        //Web(主机-宿主)提供某些服务可供Startup类构造函数
        //接受由host(主机-宿主)定义的依赖项
        //IConfiguration 在启动过程中配置(注册)app (表示一组键/值应用程序配置属性--接口) 
        //IHostingEnviromnet 由环境配置(注册)服务(提供有关应用程序的Web(宿主)托管环境的信息。)
        // public Startup(IConfiguration configuration, IHostingEnvironment env)
        // {         
        //       var builder=new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
        //                                           .AddJsonFile("appsettings.json",optional:false,reloadOnChange:true)
        //                                           .AddJsonFile($"appsettings.{env.EnvironmentName}.json",optional:true)
        //                                           .AddEnvironmentVariables();
        //     Configuration = configuration;
        //     HostingEnvironment = env;
        // }

        //构造函数需要使用IConfiguration参数
        //当Program.cs IWebHost开始运行时,此构造函数便回自动获取调用
        //so,ASP.NET Core 会从 依赖关系注入容器中 提供所需的IConfiguration参数
        public Startup(IHostingEnvironment env,IConfiguration ifg)
        {
            // var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
            //                                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //                                     .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            //                                     .AddEnvironmentVariables();
            //builder.Build();

            //在Program.cs中的CreateDefaultBuilder将从.json文件和环境变量读取设置,
            //不过,配置系统是可以扩展的,可以从各种提供程序(.json,.xml,.ini.环境变量,azure key vault等等)读取配置信息
            //使用 IConfiguration 和 IConfigurationBuilder 对象时，请务必记住提供程序的添加顺序。
            //后添加的提供程序可能会重写先添加的提供程序的设置。因此，不妨先添加常见的基础提供程序，
            //然后再添加可能会重写一些设置的环境专属提供程序。
            // ifg=new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
            //                                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //                                     .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            //                                     .AddEnvironmentVariables().Build();

            //以上都移动只Program.cs

            Configuration = ifg;
            HostingEnvironment = env;
        }



        //一种注入IHostingEnvironment的替代方法是使用约定的方法
        public IHostingEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }

        //启动app运行时调用
        //web宿主在调用Configure方法之前来配置(注册)应用程序的服务
        //将服务添加到服务容器中并使他们可以在app和Configure方法内使用
        //服务是通过解析依赖注入或者从IApplicationBuilder.ApplicationServices而来的
        //web主机可以在Startup调用之前配置(注册)一些服务
        //对于那些需要大量的setup,在IServiceCollection接口有一个Add[Service]的扩展方法
        //一个典型的实例就是,webapp注册EF,Identity,MVC服务
        // This method gets called by the runtime. Use this method to add services to the container.
        //应用程序添加其他服务

        //将服务添加到应用程序的依赖关系注入容器中
        //所有的ASP.NET Core应用程序都有一个默认依赖关系注入容器,用于存储服务,以供日后使用
        //so,服务无需与依赖他们的组件紧密耦合,即可供使用
        public void ConfigureServices(IServiceCollection services)
        {
            //增加ef服务
            // Add framework services.
            // services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // services.AddIdentity<ApplicationUser, IdentityRole>()
            //     .AddEntityFrameworkStores<ApplicationDbContext>()
            //     .AddDefaultTokenProviders();
            Configuration.GetConnectionString("MysqlDbConnectionString");
            // 可以使用 IConfiguration.GetSection 方法，检索配置的各个部分，也可以指定特定设置的完整路径（以冒号分隔）
            Configuration.GetSection("ConnectionStrings:MysqlDbConnectionString");

            //连接字符串传给数据库上下文的DbContextOptionsBuilder
            services.AddDbContext<SchoolContext>(option => 
                    option.UseMySQL(
                            //读取配置文件appsetting.json获取连接字符串
                            Configuration.GetConnectionString("MysqlDbConnectionString")
                            ));


            /* 连接字符串使用ConnectRetryCount=0以防止SQLClient从挂起。*/
            services.AddMvc();

            //在 ASP.NET Core 2.0 中，现在可以在Program.cs生成 IWebHost 时通过 ConfigureLogging 方法完成日志设置。
            //虽然仍然可以在Startup中设置日志,但是在web主机创建时配置日志,可以简化Startup类型
            //并能在应用程序启动过程的较早期使用日志
            // services.AddLogging();


            //           // Add application services.
            //注册自己的类型和服务-具体取决于依赖关系注入提供的对象所需的生命周期
            // services.AddTransient<IEmailSender, AuthMessageSender>();
            // services.AddTransient<ISmsSender, AuthMessageSender>();\
            
            // services.AddScoped();

            // services.AddSingleton();

            //如果注册为单一实例，每当有请求获取服务类型时，就会返回服务的单一实例；
            //如果注册为临时实例，就会为每个请求新建一个实例。
            //如果添加为范围内实例，将在一个 HTTP 请求的整个处理过程中使用一个服务实例。

            //若要更深入地了解 ASP.NET Core 中的依赖关系注入，请访问 bit.ly/2w7XtJI。

        }

        // HTTP请求处理管道和中间件
        // 这是设置ASP.NET Core应用程序的核心(即HTTP请求处理管道)的地方
        // 在此方法中,将注册不同的中间件,用于处理传入的HTTP请求,以生成响应
        // 用于指定app如何响应HTTP请求
        // 通过添加中间件(IApplicationBuilder实例)配置(注册)请求管道
        // 其中IApplicationBuilder可提供Configure方法,但是它并没有被注册在服务容器
        // 托管创建IApplicationBuilder并直接将其传递到Configure
        // 下面模版中
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //在这个方法中,这些中间件组件都会被添加至IApplicationBuilder中,以形成处理管道
            //如果有http请求
            //将调用已注册的首个中间件此中间件将执行所需的任何逻辑，
            //然后再调用管道中的下一个中间件，或在已完全处理响应的情况下返回到上一个中间件（若有），
            //这样就可以在响应准备就绪后执行任何所需的逻辑。
            //one by one 一个个
            //so 即在有请求时先依序调用中间件组件,然后再在处理完毕后逆序调用中间件组件

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();//异常页

                app.UseBrowserLink(); //非模版-浏览器连接
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); //错误页
            }

            //调用 StaticFiles 中间件，它可以提供静态文件（例如，图像或样式表），从而处理请求。
            //如果是这样，它将暂停管道，并将控制权归还给上一个中间件，即异常处理程序。
            app.UseStaticFiles(); //静态页


            app.UseAuthentication();
  

            //如果 StaticFiles 中间件无法提供响应，它将调用下一个中间件，即 MVC 中间件。
            //根据指定的路由选项，此中间件会尝试将请求路由到 MVC 控制器（或 Razor 页面），rr以实现请求履行。
            //ASP.NET MVC
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //每个Use扩展方法为请求管道添加一个中间件组建
            //例如UseMvc扩展方法就为请求管道添加一个路由中间件并配置(注册)mvc作为默认处理
            //其他服务,eg:IHostingEnvironment和ILoggerFactory,也可以指定方法签名

            //中间件组件的注册顺序非常重要。如果 UseStaticFiles 是在 UseMvc 后面注册，应用程序会先尝试将所有请求路由到 MVC 控制器，
            //然后再检查是否有静态文件。这可能会导致性能大大降低！ 如果管道的靠后位置上有异常处理中间件，那么它将无法处理在上一个中间件组件中出现的异常。

            //Razor 页面类似于 MVC 视图，不同之处在于可以直接将请求路由到 Razor 页面，而不需要单独使用控制器。
            //这样一来，便可以简化基于页面的应用程序，并确保视图和视图模型在一起。支持页面的模型可以直接包含在 
            //cshtml 页面中（使用 @functions 指令），也可以包含在单独的代码文件中（使用 @model 指令引用）。
        }
    }
}
