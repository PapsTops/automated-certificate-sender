using System.Collections.Generic;

namespace AutomatedCertificateSender.Models
{
    public class FormResponse
    {
        public string Range { get; set; }
        public string MajorDimension { get; set; }
        public List<List<string>> Values { get; set; }
    }
}