using System.Collections.Generic;
using ReviewApp.Data;
using ReviewApp.Domain.Views;

namespace ReviewApp.Services
{
    public interface ITextAnalysisService
    {
        TextAnalysis SaveContentAnalysis(string content, long reviewId);
        string CalculateAverageSentiment(long productId);
        IEnumerable<ProductAcceptanceView> GroupBySentiment(long productId);
    }
}