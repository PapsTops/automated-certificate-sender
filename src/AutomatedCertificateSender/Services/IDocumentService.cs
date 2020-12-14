using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedCertificateSender.Services
{
    public interface IDocumentService
    {
        Task<string> GenerateCertificate(string documentPath, Dictionary<string, string> keywordsMap, string fileName);
    }
}
