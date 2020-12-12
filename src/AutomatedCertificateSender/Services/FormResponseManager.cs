using AutomatedCertificateSender.Models;
using AutomatedCertificateSender.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutomatedCertificateSender
{
    public class FormResponseManager : IFormResponseManager {

        private readonly ILogger<FormResponseManager> _logger;
        private readonly IDataService _formResponseService;

        public FormResponseManager(ILogger<FormResponseManager> logger, IDataService formResponseService)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._formResponseService = formResponseService ?? throw new ArgumentNullException(nameof(formResponseService));
        }

        public Task<List<FormResponse>> GetFormResponses(string excelSheetName)
        {
            _logger.LogInformation("Geting list of form responses");

            return _formResponseService.GetResponses(excelSheetName);
        }
    }
}