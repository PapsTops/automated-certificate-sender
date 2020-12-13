using AutomatedCertificateSender.Models;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedCertificateSender.Services
{
    public class DataService : IDataService
    {
        private readonly ILogger<DataService> _logger;
        private readonly GoogleAuthSettingsService _options;

        public DataService(ILogger<DataService> logger, GoogleAuthSettingsService options)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public Task<FormResponse> GetExcelSheetValues(string sheetId, string range)
        {
            _logger.LogInformation("Getting headers");

            var url = $"https://sheets.googleapis.com/v4/spreadsheets/{sheetId}/values/{range}";

            return url.WithHeader("Authorization", "Bearer " + _options.CurrentValue.AccessToken)
                .GetAsync()
                .ReceiveJson<FormResponse>();


        }

        public Task<GDriveQueryResult> GetListOfExcelSheetOnGDrive()
        {
            _logger.LogInformation("Getting list of available excel file in google drive");

            var url = "https://www.googleapis.com/drive/v3/files?q=mimeType='application/vnd.google-apps.spreadsheet'";

            return url.WithHeader("Authorization", "Bearer " + _options.CurrentValue.AccessToken)
                .GetAsync()
                .ReceiveJson<GDriveQueryResult>();
        }



        public Task<List<FormResponse>> GetResponses(string v)
        {
            throw new NotImplementedException();
        }
    }
}
