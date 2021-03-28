using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesManagementSystem.Setting;

namespace SalesManagementSystem
{
    public class Startup
    {
        //追加
        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }
        //追加
        public IConfiguration Configuration {get;set;}

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //追加
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Postgres追加
            services.Configure<DBSetting>(Configuration.GetSection("ConnectionStrings"));
        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //追加
            app.UseMvc();

            //追加
            app.UseDefaultFiles();
            app.UseStaticFiles();

            /*
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
            */
        }
    }
}
