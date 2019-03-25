using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace CityInfo.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }


        public Startup(IConfiguration configure)
        {
            Configuration = configure;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o=>o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));
            services.AddLogging(loggingBuilder =>
            {                
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
                
            });
            var connectionString = Startup.Configuration["connectionStrings:cityInfoDBConnectionString"]; 
            services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();
            #if DEBUG
                        services.AddTransient<IMailService,LocalMailService>();
            #else
                        services.AddTransient<IMailService,CloudMailService>();
            #endif

            //.AddJsonOptions(o=>
            //{
            //    if (o.SerializerSettings.ContractResolver!=null)
            //    {
            //        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //        castedResolver.NamingStrategy = null;
            //    }

            //});
        }
         
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CityInfoContext cityInfoContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            cityInfoContext.EnsureSeedDataForContext();
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<Entities.City, Models.CityWithoutPointOfInterestDto>();
                cfg.CreateMap<Entities.City, Models.CityDTO>();
                cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
                cfg.CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
                cfg.CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();
                cfg.CreateMap<Entities.PointOfInterest,Models.PointOfInterestForUpdateDto>();
            });
            app.UseMvc();
            app.UseStatusCodePages();
            app.Run( async(context) =>
            {                
                await context.Response.WriteAsync("Hello World!");
            });

        }
    }
}
