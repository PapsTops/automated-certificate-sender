using AutomatedCertificateSender.Models;
using AutomatedCertificateSender.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AutomatedCertificateSender
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
            services.AddControllers();
            
            services.Configure<GoogleAuthSettings>(Configuration.GetSection("GoogleAuthSettings"));

            services.AddSingleton(sp =>
            {
                var logger = sp.GetService<ILogger<GoogleAuthSettingsService>>();
                var options = sp.GetService<IOptionsMonitor<GoogleAuthSettings>>();
                var googleAuth = sp.GetService<IGoogleAuth>();
                var webHost = sp.GetService<IWebHostEnvironment>();

                return new GoogleAuthSettingsService(logger, options, googleAuth, webHost);
            });

            services.AddSingleton<IGoogleAuth, GoogleAuth>();
            
            services.AddSingleton<IFormResponseManager, FormResponseManager>();

            services.AddTransient<IDataService, DataService>();

            services.AddHostedService<App>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}