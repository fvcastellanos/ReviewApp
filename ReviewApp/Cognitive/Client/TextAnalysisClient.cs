using System;
using Azure;
using Azure.AI.TextAnalytics;
using LanguageExt;
using Microsoft.Extensions.Logging;

namespace ReviewApp.Cognitive.Client
{
    public class TextAnalysisClient: ITextAnalysisClient
    {
        private readonly ILogger<TextAnalysisClient> _logger;

        private readonly TextAnalyticsClient _textAnalyticsClient;
        // private static readonly AzureKeyCredential Credentials = GetAzureKeyCredential();
        // private static readonly Uri EndPoint = GetAzureServiceUri();

        public TextAnalysisClient(string azureKeyCredential, string azureServiceUrl, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TextAnalysisClient>();
            
            var credentials = new AzureKeyCredential(azureKeyCredential);
            var endPoint = new Uri(azureServiceUrl);
            _textAnalyticsClient = new TextAnalyticsClient(endPoint, credentials);
        }

        public Option<DetectedLanguage> DetectLanguage(string text)
        {
            try
            {
                _logger.LogInformation("trying to detect language for: {0}", text);
                var response = _textAnalyticsClient.DetectLanguage(text, "US");

                _logger.LogInformation("language detected: {0}", response.Value.Name);
                return Option<DetectedLanguage>.Some(response.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError("can't detect language - ", ex);

                return Option<DetectedLanguage>.None;
            }
        }

        public Option<DocumentSentiment> DetectSentiment(string text)
        {
            try
            {
                _logger.LogInformation("trying to detect sentiment for: {0}", text);
                var response = _textAnalyticsClient.AnalyzeSentiment(text);

                _logger.LogInformation("sentiment detected: {0}", response.Value.Sentiment);
                return Option<DocumentSentiment>.Some(response.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError("can't detect sentiment - ", ex);

                return Option<DocumentSentiment>.None;
            }
        }

        /*
        private static AzureKeyCredential GetAzureKeyCredential()
        {
            return new AzureKeyCredential(Environment.GetEnvironmentVariable("AZURE_KEY_CREDENTIAL") ?? "");
        }

        private static Uri GetAzureServiceUri()
        {
            return new Uri(Environment.GetEnvironmentVariable("AZURE_SERVICE_URL") ?? "");
        }
    */
    }
}