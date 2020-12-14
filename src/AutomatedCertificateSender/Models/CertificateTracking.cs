using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedCertificateSender.Models
{
    public class CertificateTracking
    {
        public List<Certificate> Certificates { get; set; }
    }

    public class Certificate
    {
        public string EmailAddress { get; set; }
        public string FilePath { get; set; }
        public DeliveryState DeliveryState { get; set; }
    }

    public enum DeliveryState
    {
        Pending,
        Success
    }
}
