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
                CompanyId = productView.CompanyId,
                ImageUrl = productView.ImageUrl
            };
        }

        public static ProductView ToView(Product product)
        {
            var p = new ProductView()
            {
                Id = product.Id,
                Name = product.Name,
                CompanyId = product.CompanyId,
                CompanyIdValue = product.CompanyId.ToString(),
                Description = product.Description,
                ReviewViews = BuildReviewViewList(product.Reviews),
                ImageUrl = product.ImageUrl
            };

            if (product.Company != null)
            {
                p.CompanyName = product.Company.Name;
            }

            return p;
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