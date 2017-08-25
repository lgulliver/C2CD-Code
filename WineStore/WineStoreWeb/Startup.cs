using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WineStoreWeb.Data;

namespace WineStoreWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Startup>()
                .AddEnvironmentVariables()
                .Build();


        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.Name = "WineStore.Session";

                // disabling this while in test/dev
                options.Cookie.HttpOnly = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            var trolleyAPI = Configuration.GetSection("Endpoints:TrolleyAPI").Value;
            var inventoryAPI = Configuration.GetSection("Endpoints:InventoryAPI").Value;
            var purchaseAPI = Configuration.GetSection("Endpoints:PurchaseAPI").Value;

            var trolleyAPIKey = Configuration.GetSection("Keys:TrolleyAPIKey").Value;
            var inventoryAPIKey = Configuration.GetSection("Keys:InventoryAPIKey").Value;
            var purchaseAPIKey = Configuration.GetSection("Keys:PurchaseAPIKey").Value;

            new TrolleyProxy(trolleyAPI, trolleyAPIKey);
            new InventoryProxy(inventoryAPI, inventoryAPIKey);
            new PurchaseProxy(purchaseAPI, purchaseAPIKey);
        }
    }
}
