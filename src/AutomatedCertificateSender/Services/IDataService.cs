using AutomatedCertificateSender.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedCertificateSender.Services
{
    public interface IDataService
    {
        Task<List<FormResponse>> GetResponses(string v);
    }
}
