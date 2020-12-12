using AutomatedCertificateSender.Models;
using AutomatedCertificateSender.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCertificateSender.Test
{
    public class GoogleAuthTest
    {

        [Test]
        [TestCase("cliendIdgarbage")]
        [TestCase("code")]
        [TestCase("offline")]
        public void ShouldBeAbleToGenerateCorrectStartUrl(string expected)
        {
            var logger = new Mock<ILogger<GoogleAuth>>();
            var googleSettings = new Mock<IOptions<GoogleAuthSettings>>();
            googleSettings.Setup(x => x.Value).Returns(new GoogleAuthSettings
            {
                ClientId = "cliendIdgarbage",
                ClientSecret = "clientSecretgarbage",
            });

            var sut = new GoogleAuth(logger.Object, googleSettings.Object);

            var actual = sut.GenerateStartUrl();

            Assert.That(true, Is.EqualTo(actual.Contains(expected)));
        }
    }
}
