using AutomatedCertificateSender.Models;
using Flurl;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace AutomatedCertificateSender.Services
{
    public class GoogleAuth : IGoogleAuth
    {
        private readonly ILogger<GoogleAuth> _logger;
        private readonly IOptions<GoogleAuthSettings> _googleAuthSettings;

        private string _state;

        public GoogleAuth(ILogger<GoogleAuth> logger, IOptions<GoogleAuthSettings> googleAuthSettings)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._googleAuthSettings = googleAuthSettings ?? throw new ArgumentNullException(nameof(googleAuthSettings));
        }

        public string GenerateStartUrl(string state = default)
        {
            _state = state;

            var googleAuthSettings = _googleAuthSettings.Value;

            var startUrl = googleAuthSettings.AuthorizeEndpoint
                .SetQueryParams(new
                {
                    scope = googleAuthSettings.Scopes,
                    access_type = "offline",
                    include_granted_scopes = "true",
                    response_type = "code",
                    state = _state,
                    redirect_uri = googleAuthSettings.CallbackUrl,
                    client_id = googleAuthSettings.ClientId
                });

            return startUrl.ToString();

        }
    }
}
