using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CodeSampleWebApp.CustomerProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeSampleWebApp
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
            //var assembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "DLL\\PluginController\\PluginController.dll");
            var instance = new PluginActionDescriptorProvider();
            services.AddSingleton<IActionDescriptorChangeProvider>(instance);
            services.AddSingleton(instance);
            services.AddMvc(options =>
            {
            })
            .AddJsonOptions(options =>
            {
                
            })
            //.AddControllersAsServices()
            .AddDataAnnotationsLocalization(options =>
            {

            })
            //.AddApplicationPart(null)
            .AddCookieTempDataProvider()
            .ConfigureApiBehaviorOptions(options =>
            {
                
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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
