using AutomatedCertificateSender.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AutomatedCertificateSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ILogger<CertificateController> _logger;
        private readonly IDocumentService _documentService;

        public CertificateController(ILogger<CertificateController> logger, IDocumentService documentService)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._documentService = documentService ?? throw new ArgumentNullException(nameof(documentService));
        }


        [HttpGet]
        public async Task Get()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            directory = Path.Combine(directory, "Certificate-of-Participation.docx");

            var keywordMap = new Dictionary<string, string>
            {
                { "<<full name>>", "Christopher Enriquez" }
            };

            await _documentService.GenerateCertificate(directory, keywordMap, "tops");
        }
    }
}
