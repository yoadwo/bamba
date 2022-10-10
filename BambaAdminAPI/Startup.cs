using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.AzureAppServices;
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

            services.AddControllers();
            services.AddLogging();
            services.Configure<AzureFileLoggerOptions>(options =>
            {
                options.FileName = "bamba-admin-api-log";
                options.FileSizeLimit = 50 * 1024;
                options.RetainedFileCountLimit = 10;
            });

            services.AddSingleton<Services.VoiceCommandsStorageService.IVoiceCommandsStorageService, Services.VoiceCommandsStorageService.VoiceCommandsStorageService>();
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

            System.Console.WriteLine("Env: " + env.EnvironmentName);
            

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
