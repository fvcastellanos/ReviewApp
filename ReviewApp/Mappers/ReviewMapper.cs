using Microsoft.AspNetCore.Mvc.ModelBinding;
using ReviewApp.Data;
using ReviewApp.Domain.Views;

namespace ReviewApp.Mappers
{
    public class ReviewMapper
    {
        public static Review ToModel(ReviewView reviewView)
        {
            return new Review()
            {
                Id = reviewView.Id,
                ReviewDate = reviewView.ReviewDate,
                Content = reviewView.Content,
                ProductId = reviewView.ProductId,
            };
        }

        public static ReviewView ToView(Review review)
        {
            return new ReviewView()
            {
                Id = review.Id,
                ReviewDate = review.ReviewDate,
                Content = review.Content,
                ProductId = review.ProductId,
            };
        }
    }
}