using AutomatedCertificateSender.Models;
using AutomatedCertificateSender.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedCertificateSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly ILogger<ExcelController> _logger;
        private readonly IDataService _dataService;

        public ExcelController(ILogger<ExcelController> logger, IDataService dataService)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        [HttpGet("list")]
        public async Task<GDriveQueryResult> GetListOfExcelSheetOnYourDrive()
        {
            var files = await _dataService.GetListOfExcelSheetOnGDrive();

            return files;
        }

        [HttpGet("values")]
        public async Task<FormResponse> GetFormResponses(string sheetId, string range)
        {
            var values = await _dataService.GetExcelSheetValues(sheetId, range);

            return values;
        }
    }
}
