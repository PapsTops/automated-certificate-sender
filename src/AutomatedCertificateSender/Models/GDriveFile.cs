using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedCertificateSender.Models
{
    public class GDriveFile
    {
        public string MimeType { get; set; }
        public string Kind { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
    }

    public class GDriveQueryResult
    {
        public List<GDriveFile> Files { get; set; }
    }
}
