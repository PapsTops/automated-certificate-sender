using AutomatedCertificateSender.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedCertificateSender.Services
{
    public interface IGoogleAuth
    {
        string GenerateStartUrl(string state = default);
        Task<OAuthResponse> CodeExchangeForAccessToken(string code);
        Task<OAuthResponse> RefreshAccessToken(string refreshToken);
    }
}
