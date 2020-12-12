using System.Collections.Generic;
using System.Threading.Tasks;
using AutomatedCertificateSender.Models;
using AutomatedCertificateSender.Services;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AutomatedCertificateSender.Test
{
    public class FormResponseManagerTest {
        
        [Test]
        public async Task ShouldGetListOfResponseIfDataServiceReturnAValue() 
        {
            
            var logger = new Mock<ILogger<FormResponseManager>>();
            var dataService = new Mock<IDataService>();

            dataService.Setup(x => x.GetResponses(It.IsAny<string>()))
                .Returns(Task.FromResult(new List<FormResponse>()));

            var sut = new FormResponseManager(logger.Object, dataService.Object);

            var actual = await sut.GetFormResponses("my excel sheet");

            Assert.That(actual, Is.TypeOf(typeof(List<FormResponse>)));
        }
    }    
}
