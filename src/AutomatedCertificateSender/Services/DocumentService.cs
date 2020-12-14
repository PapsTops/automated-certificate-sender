using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Threading = System.Threading.Tasks;

namespace AutomatedCertificateSender.Services
{
    public class DocumentService : IDocumentService
    {
        public Threading.Task<string> GenerateCertificate(string documentPath, Dictionary<string, string> keywordsMap, string fileName)
        {
            return Threading.Task.Run(() =>
            {

                Application app = new Application();
                Document doc = app.Documents.Open(documentPath);

                foreach (var map in keywordsMap)
                {
                    app.Selection.Find.ClearFormatting();

                    object textToFind = map.Key;

                    if (app.Selection.Find.Execute(ref textToFind))
                    {
                        app.Selection.Text = map.Value;
                    }
                }

                var currentWorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                var filePathToSave = Path.Combine(currentWorkingDirectory, fileName + Guid.NewGuid().ToString() + ".pdf");
                doc.SaveAs2(filePathToSave, WdSaveFormat.wdFormatPDF);
                doc.Close();
                app.Quit();

                return string.Empty;
            });
            
        }

    }
}
