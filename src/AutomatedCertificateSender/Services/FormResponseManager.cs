using AutomatedCertificateSender.Services;

namespace AutomatedCertificateSender
{
    public class FormResponseManager : IFormResponseManager {
        public FormResponseManager(ILogger<FormResponseManager> logger, IFormResponseService formResponseService)
        {
            
        }
    }
}