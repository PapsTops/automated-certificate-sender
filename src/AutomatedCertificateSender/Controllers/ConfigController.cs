using AutomatedCertificateSender.Models;
using AutomatedCertificateSender.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace AutomatedCertificateSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly ILogger<ConfigController> _logger;
        private readonly IWebHostEnvironment _webHost;

        public ConfigController(ILogger<ConfigController> logger, IWebHostEnvironment webHost)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._webHost = webHost ?? throw new ArgumentNullException(nameof(webHost));
        }

        [HttpPost]
        public IActionResult SaveConfiguration([FromBody] SourceSetting config)
        {
            var appSettings = new AppSettings
            {
                Source = config
            };

            SettingsHelpers.AddOrUpdateAppSetting(appSettings, _webHost);

            return Ok(new
            {
                message = "Success"
            });
        }

        [HttpGet]
        public IActionResult GetConfiguration()
        {
            var currentWorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var appSettingsPath = Path.Combine(currentWorkingDirectory, "appsettings.json");

            var appSettingsText = System.IO.File.ReadAllText(appSettingsPath);

            var appSettings = JsonConvert.DeserializeObject<AppSettings>(appSettingsText);

            return Ok(appSettings);
        }

    }

    public class AppSettings
    {
        public SourceSetting Source { get; set; }
    }
}
