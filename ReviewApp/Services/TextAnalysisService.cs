using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using ReviewApp.Cognitive.Client;
using ReviewApp.Data;
using ReviewApp.Domain.Views;

namespace ReviewApp.Services
{
    public class TextAnalysisService: ITextAnalysisService
    {
        private readonly ReviewContext _dbContext;
        private readonly ITextAnalysisClient _analysisClient;

        private readonly ILogger<TextAnalysisService> _logger;

        public TextAnalysisService(ReviewContext reviewContext, ITextAnalysisClient analysisClient, ILoggerFactory loggerFactory)
        {
            _dbContext = reviewContext;
            _analysisClient = analysisClient;
            _logger = loggerFactory.CreateLogger<TextAnalysisService>();
        }
        
        public TextAnalysis SaveContentAnalysis(string content, long reviewId)
        {
            _logger.LogInformation("performing text analysis for text: {0}", content);
            
            var languageHolder = _analysisClient.DetectLanguage(content);
            
            var textAnalysis = languageHolder.Map(some => 
                new TextAnalysis
                {
                    ReviewId = reviewId,
                    Language = some.Name,
                    LanguageScore = some.ConfidenceScore
                }
            ).Match(some => some, () => new TextAnalysis());
            
            var sentimentHolder = _analysisClient.DetectSentiment(content);

            var analysis = sentimentHolder.Map(some =>
                new TextAnalysis()
                {
                    ReviewId = textAnalysis.ReviewId,
                    Language = textAnalysis.Language,
                    LanguageScore = textAnalysis.LanguageScore,
                    Sentiment = some.Sentiment.ToString(),
                    NegativeScore = some.ConfidenceScores.Negative,
                    NeutralScore = some.ConfidenceScores.Neutral,
                    PositiveScore = some.ConfidenceScores.Positive
                }
            ).Match(some => some, () => new TextAnalysis());

            _dbContext.TextAnalyses.Add(analysis);
            _dbContext.SaveChanges();
            
            return analysis;
        }

        public string CalculateAverageSentiment(long productId)
        {
            var average = _dbContext.TextAnalyses.Where(analysis => analysis.Review.ProductId == productId)
                .ToList()
                .Select(analysis => ToNum(analysis.Sentiment))
                .Average(s => s);

            return ToText(average);
        }

        public IEnumerable<ProductAcceptanceView> GroupBySentiment(long productId)
        {
            return _dbContext.TextAnalyses.Where(analysis => analysis.Review.ProductId == productId)
                .ToList()
                .GroupBy(
                    analysis => analysis.Sentiment,
                    analysis => analysis.Sentiment,
                    (sentiment, groups) => 
                        new ProductAcceptanceView()
                        {
                            Sentiment = sentiment,
                            Count = groups.Count()
                        }
                    );
        }
        
        // ---------------------------------------------------------------------------------------------------

        private static string ToText(double sentimentNumber)
        {
            return sentimentNumber switch
            {
                0 => "Negative",
                1 => "Positive",
                _ => "Neutral"
            };
        }

        private static int ToNum(string sentiment)
        {
            return sentiment switch
            {
                "Negative" => 0,
                "Positive" => 1,
                _ => 2
            };
        }
    }
}