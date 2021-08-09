using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ResilienceHTTPClinetwithPolly.DataClientService;
using ResilienceHTTPClinetwithPolly.PollyInfo;

namespace ResilienceHTTPClinetwithPolly
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILoggerFactory _loggerFactory { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var _loggerFactory = LoggerFactory.Create(builder => { /*configure*/ });
            services.AddLogging(config =>
            {
                config.AddDebug();
                config.AddConsole();
                //etc
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ResilienceHTTPClinetwithPolly", Version = "v1" });
            });


            services.AddHttpClient<IDataClient, DataClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:55124/");
            })
           .AddPolicyHandlers("PolicyConfig", _loggerFactory, Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ResilienceHTTPClinetwithPolly v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
