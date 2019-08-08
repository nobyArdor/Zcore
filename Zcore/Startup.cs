using DbCore;
using DbCore.Models;
using LibCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zcore.Service;
using Zcore.Tools;

namespace Zcore
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
            services
                
                .AddConnections()
                .AddLogging()
                .AddScoped<IUserManager, CustomAuthManager>()
                .AddScoped<ILogicService<NotifyRecords>, NotifyRecordsLogicService>()
                .AddScoped<ILogicService<SensorData>, SensorDataLogicService>()
                .AddScoped<ILogicBatchService<SensorData>, SensorDataLogicService>()
                .AddDbContext<BDContext>()

                .AddMvc(
                    options =>
                    {
                       // options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
                    }
                    ).AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateFormatString = "dd.MM.yyyy hh:mm:ss";
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
