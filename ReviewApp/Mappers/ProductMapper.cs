using System.Collections.Generic;
using System.Linq;
using ReviewApp.Data;
using ReviewApp.Domain.Views;

namespace ReviewApp.Mappers
{
    public static class ProductMapper
    {
        public static Product ToModel(ProductView productView)
        {
            return new Product()
            {
                Id = productView.Id,
                Name = productView.Name,
                Description = productView.Description,
                CompanyId = productView.CompanyId
            };
        }

        public static ProductView ToView(Product product)
        {
            return new ProductView()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ReviewViews = BuildReviewViewList(product.Reviews)
            };
        }
        
        // ------------------------------------------------------------------------------------

        private static List<ReviewView> BuildReviewViewList(IEnumerable<Review> reviewList)
        {
            return reviewList == null ? new List<ReviewView>()
                : reviewList.Select(ReviewMapper.ToView)
                    .ToList();
        }
    }
}