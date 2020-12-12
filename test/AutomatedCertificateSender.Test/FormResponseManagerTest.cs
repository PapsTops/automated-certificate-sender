using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace AutomatedCertificateSender.Test
{
    public class FormResponseManagerTest {
        
        [Test]
        public void ShouldGetListOfResponses() {
            
            var sut = new Mock<IFormResponseManager>();
            List<FormResponse> formResponses = new List<FormResponse>(); 
            sut.Setup(x => x.GetListOfFormResponses()).Returns(Task.FromResult(formResponses));
            var expectedCount = 0;

            var actual = sut.Object.GetListOfFormResponses();

            Assert.That(expectedCount, Is.SameAs(actual));
        }
    }    
}
