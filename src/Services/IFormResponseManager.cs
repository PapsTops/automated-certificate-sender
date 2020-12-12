namespace AutomatedCertificateSender
{
    public interface IFormResponseManager
    {
        Task<List<FormResponse>> GetListFormResponse();    
    }
    
}