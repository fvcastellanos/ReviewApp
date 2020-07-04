using System;
using Azure.AI.TextAnalytics;
using FluentAssertions;
using LanguageExt.UnitTesting;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ReviewApp.Cognitive.Client;

namespace ReviewApp.Tests.Cognitive.Client
{
    [TestFixture]
    public class TextAnalysisClientTest
    {
        private ITextAnalysisClient _analysisClient;

        [SetUp]
        public void SetUp()
        {
            var key = Environment.GetEnvironmentVariable("AZURE_KEY_CREDENTIAL") ?? "";
            var url = Environment.GetEnvironmentVariable("AZURE_SERVICE_URL") ?? "";
            var loggerFactory = new LoggerFactory();
                
            _analysisClient = new TextAnalysisClient(key, url, loggerFactory);
        }

        [Test]
        public void DetectLanguage()
        {
            var responseHolder = _analysisClient.DetectLanguage("hola mundo!");
            
            responseHolder.ShouldBeSome(some =>
            {
                some.Name.Should().Be("Spanish");
            });
        }

        [Test]
        public void DetectSentiment()
        {
            var responseHolder = _analysisClient.DetectSentiment("la lluvia gris tambien es parte del paisaje");
            
            responseHolder.ShouldBeSome(some =>
            {
                some.Sentiment.Should().Be(TextSentiment.Negative);
            });
        }
    }
}