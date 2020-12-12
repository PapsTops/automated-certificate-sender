using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCertificateSender.Models
{
    public class GoogleAuthSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scopes { get; set; }
        public string  CallbackUrl { get; set; }
        public string AuthorizeEndpoint { get; set; }
    }
}
