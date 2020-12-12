using System.Collections.Generic;
using System.Threading.Tasks;
using AutomatedCertificateSender.Models;
using AutomatedCertificateSender.Services;
using Castle.Core.Logging;
using Moq;
using NUnit.Framework;

namespace AutomatedCertificateSender.Test
{
    public class FormResponseManagerTest {
        
        [Test]
        public async Task ShouldGetListOfResponses() {
            
            var logger = new Mock<ILogger>();
            var formResponseService = new Mock<IFormReponseService>();

            var sut = new FormResponseManager(logger, formResponseService);

            List<FormResponse> formResponses = new List<FormResponse>(); 
            sut.Setup(x => x.GetListOfFormResponses()).Returns(Task.FromResult(formResponses));
            var expectedCount = 1;

            var actual = await sut.Object.GetListOfFormResponses();

            Assert.That(expectedCount, Is.SameAs(actual.Count));
        }
    }    
}
