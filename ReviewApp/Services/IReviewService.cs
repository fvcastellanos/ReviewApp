using System.Collections.Generic;
using LanguageExt;
using ReviewApp.Domain.Views;

namespace ReviewApp.Services
{
    public interface IReviewService
    {
        Either<string, IEnumerable<ReviewView>> GetProductReviews(long productId);
        Either<string, ReviewView> Get(long id);
        Either<string, ReviewView> Add(ReviewView reviewView);
        Either<string, ReviewView> Update(ReviewView reviewView);
        Either<string, long> Delete(long id);
    }
}