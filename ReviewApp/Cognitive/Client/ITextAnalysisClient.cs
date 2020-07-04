using Azure.AI.TextAnalytics;
using LanguageExt;

namespace ReviewApp.Cognitive.Client
{
    public interface ITextAnalysisClient
    {
        Option<DetectedLanguage> DetectLanguage(string text);
        Option<DocumentSentiment> DetectSentiment(string text);
    }
}