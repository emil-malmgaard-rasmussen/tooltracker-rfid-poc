using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using Hexio.ServiceBus;
using Autofac;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smoerfugl.AspNetCore;
using TooltrackerRfid.Database;
using TooltrackerRfid.Bll.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace TooltrackerRfid
{
    public class Startup: SmoerfuglStartup
    {
        public Startup(IConfiguration configuration,  IHostEnvironment env) : base(env)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public override void ConfigureServices(IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetCallingAssembly());

            services
                .AddAuthentication(c => c.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents();
                    options.SaveToken = true;
                });

            services.AddControllers()
                .AddJsonOptions(d => d.JsonSerializerOptions.Converters
                    .Add(new JsonStringEnumConverter()));

        }
        
        public override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new PlatformModule(Configuration));
            
            var rabbitMqSettings = new RabbitMqConfiguration();
            Configuration.Bind("RabbitMq", rabbitMqSettings);
            builder.UseServiceBus(rabbitMqSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IBusControl busControl, IApplicationBuilder app, IWebHostEnvironment env,
            TooltrackerRfidDbContext dbContext)
        {
            dbContext.Database.Migrate();
            busControl.Start();

            if (env.IsDevelopment())
            {
                app.UseForwardedHeaders();
            }
            else
            {
                app.UseForwardedHeaders();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}