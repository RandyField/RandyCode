using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MODEL;

namespace randy.template.mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            string connstr = Configuration.GetConnectionString("MysqlDbConnectionString");
            //取配置文件 示例 Configuration.GetSection("ConnectionStrings:MysqlDbConnectionString");


            //连接字符串传给数据库上下文的DbContextOptionsBuilder
            services.AddDbContext<SchoolContext>(option =>
                    option.UseMySQL(connstr));
            services.AddMvc();

        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
           
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
