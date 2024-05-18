using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace BambaAdminAPI
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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    var allowedHosts = Configuration.GetSection("Cors").Get<string[]>();
                    builder
                    .SetIsOriginAllowed(origin =>
                    {
                        return allowedHosts.Any(host => origin.Contains(host));
                    })
                    //.AllowAnyOrigin()
                    //.WithHeaders("Origin", "Content-Type")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Arya Admin API",
                    Description = "Swagger for testing the admin endpoints",
                });
            });

            services.AddControllers();
            services.AddLogging();

            services.AddSingleton<Services.VoiceCommandsStorageService.IVoiceCommandsStorageService, Services.VoiceCommandsStorageService.VoiceCommandsStorageService>();

            services.Configure<Config.ApplicationConfig>(Configuration.GetSection("Application"));
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
                app.UseHttpsRedirection();
            }            

            app.UseRouting();

            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    //Clear servers -element in swagger.json because it got the wrong port when hosted behind reverse proxy
                    swagger.Servers.Clear();
                });
            });
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
