using AutomatedCertificateSender.Models;
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
        private readonly IDataService _dataService;

        public GoogleAuthController(ILogger<GoogleAuthController> logger, GoogleAuthSettingsService settingsService, IDataService dataService)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
            this._dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        [HttpGet]
        public async Task<IActionResult> Get(string code)
        {
            try
            {
                await _settingsService.CodeExchangeForAccessToken(code);

                _logger.LogInformation("Successful code exchange. Redirecting....");

                return LocalRedirect("~/api/excel/list");
            }
            catch (FlurlHttpException ex)
            {
                _logger.LogError(ex.Message);
            }

            return Ok();

            
        }
    }
}
