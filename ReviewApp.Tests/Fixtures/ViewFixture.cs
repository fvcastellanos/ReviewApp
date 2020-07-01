using System.Collections.Generic;
using ReviewApp.Domain.Views;

namespace ReviewApp.Tests.Fixtures
{
    public static class ViewFixture
    {
        public static CompanyView BuildCompanyView(string name)
        {
            return new CompanyView()
            {
                Id = 1,
                Name = name,
                Description = "Test",
                ProductViews = new List<ProductView>()
            };
        }
    }
}