using AutomatedCertificateSender.Services;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedCertificateSender.Controllers
{
    [Route("google-auth-callback")]
    [ApiController]
    public class GoogleAuthController : ControllerBase
    {
        private readonly ILogger<GoogleAuthController> _logger;
        private readonly GoogleAuthSettingsService _settingsService;

        public GoogleAuthController(ILogger<GoogleAuthController> logger, GoogleAuthSettingsService settingsService)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        }

        [HttpGet]
        public async Task Get(string code)
        {
            try
            {
                await _settingsService.CodeExchangeForAccessToken(code);

                _logger.LogInformation("Successful code exchange. Redirecting....");
            }
            catch (FlurlHttpException ex)
            {
                _logger.LogError(ex.Message);
            }
            
        }
    }
}
