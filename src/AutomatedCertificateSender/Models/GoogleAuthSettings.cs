using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCertificateSender.Models
{
    public record GoogleAuthSettings
    {
        public string ClientId { get; init; }
        public string ClientSecret { get; init; }
        public string Scopes { get; init; }
        public string  CallbackUrl { get; init; }
        public string AuthorizeEndpoint { get; init; }
    }
}
