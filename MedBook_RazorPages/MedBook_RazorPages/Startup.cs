using MedBook_RazorPages.Installer;
using MedBook_RazorPages.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace MedBook_RazorPages
{
    public class Startup        
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

      

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddSession();
            services.AddRazorPages(); 
            services.AddEasyCaching(options =>
            {
                //use memory cache that named default
                options.UseInMemory("default");

                // // use memory cache with your own configuration
                // options.UseInMemory(config => 
                // {
                //     config.DBConfig = new InMemoryCachingOptions
                //     {
                //         // scan time, default value is 60s
                //         ExpirationScanFrequency = 60, 
                //         // total count of cache items, default value is 10000
                //         SizeLimit = 100 
                //     };
                //     // the max random second will be added to cache's expiration, default value is 120
                //     config.MaxRdSecond = 120;
                //     // whether enable logging, default is false
                //     config.EnableLogging = false;
                //     // mutex key's alive time(ms), default is 5000
                //     config.LockMs = 5000;
                //     // when mutex key alive, it will sleep some time, default is 300
                //     config.SleepMs = 300;
                // }, "m2");

                //use redis cache that named redis1
                options.UseRedis(config =>
                {
                    config.DBConfig.Endpoints.Add(new EasyCaching.Core.Configurations.ServerEndPoint("127.0.0.1", 6379));
                }, "redis1");
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);

            /*   var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
               typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

               installers.ForEach(Installer => Installer.InstallService(services, Configuration));*/
            services.AddAuthentication().AddFacebook();

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DatabaseContext>(opstions => opstions.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

           
            app.UseMvc();
        }
    }
}
