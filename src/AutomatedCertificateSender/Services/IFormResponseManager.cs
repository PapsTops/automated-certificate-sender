using System.Collections.Generic;
using System.Threading.Tasks;
using AutomatedCertificateSender.Models;

namespace AutomatedCertificateSender.Services
{
    public interface IFormResponseManager
    {
        Task<List<FormResponse>> GetFormResponses(string excelSheetName);
    }
    
}