using System.Collections.Generic;
using System.Linq;
using ReviewApp.Data;
using ReviewApp.Domain.Views;

namespace ReviewApp.Mappers
{
    public static class CompanyMapper
    {
        public static Company ToModel(CompanyView companyView)
        {
            return new Company()
            {
                Id = companyView.Id,
                Name = companyView.Name,
                Description = companyView.Description
            };
        }

        public static CompanyView ToView(Company company)
        {
            return new CompanyView()
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                ProductViews = BuildProductViewList(company.Products)
            };
        }
        
        // ---------------------------------------------------------------------------------------

        private static List<ProductView> BuildProductViewList(IEnumerable<Product> products)
        {
            return products == null ? new List<ProductView>() 
                : products.Select(ProductMapper.ToView)
                    .ToList();
        }
    }
}