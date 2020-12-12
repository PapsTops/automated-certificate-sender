using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCertificateSender.Services
{
    public interface IGoogleAuth
    {
        string GenerateStartUrl(string state = default);
    }
}
