using AutomatedCertificateSender.Models;
using Flurl.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutomatedCertificateSender.Services
{
    public class GoogleAuthSettingsService
    {
        private readonly ILogger<GoogleAuthSettingsService> _logger;
        private readonly IGoogleAuth _googleAuth;
        private readonly IWebHostEnvironment _webHost;
        private Timer _timer;

        public GoogleAuthSettings CurrentValue { get; private set; }

        public GoogleAuthSettingsService(
            ILogger<GoogleAuthSettingsService> logger,
            IOptionsMonitor<GoogleAuthSettings> options,
            IGoogleAuth googleAuth,
            IWebHostEnvironment webHost
            )
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._googleAuth = googleAuth ?? throw new ArgumentNullException(nameof(googleAuth));
            this._webHost = webHost ?? throw new ArgumentNullException(nameof(webHost));
            this.CurrentValue = options.CurrentValue;

            options.OnChange(Listener);

            _timer = new Timer(RefreshAccessToken, this, TimeSpan.Zero, TimeSpan.FromMinutes(15));
        }

        private void RefreshAccessToken(object state)
        {
            if (string.IsNullOrEmpty(CurrentValue.RefreshToken))
            {
                _logger.LogWarning("Cant request for new access token, there is no refresh token available");
                return;
            }
            try
            {
                var oauthResponse = _googleAuth.RefreshAccessToken(CurrentValue.RefreshToken).GetAwaiter().GetResult();
                CurrentValue.AccessToken = oauthResponse.AccessToken;

                _logger.LogInformation("Successfully refresh access token");
            }
            catch (FlurlHttpException ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private void Listener(GoogleAuthSettings update)
        {
            _logger.LogInformation("Recieved configuration update");
            CurrentValue = update;
        }

        public async Task<bool> CodeExchangeForAccessToken(string code)
        {

            var oauthResponse = await _googleAuth.CodeExchangeForAccessToken(code);
            
            CurrentValue.AccessToken = oauthResponse.AccessToken;
            CurrentValue.RefreshToken = oauthResponse.RefreshToken;
            var appSettings = new AppSettings
            {
                GoogleAuthSettings = CurrentValue
            };

            SettingsHelpers.AddOrUpdateAppSetting(appSettings, _webHost);

            return true;
        }

    }
}
