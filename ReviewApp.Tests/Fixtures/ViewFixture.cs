using System;
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

        public static ProductView BuildProductView(string name)
        {
            return new ProductView()
            {
                Id = 1,
                Name = name,
                Description = "Test",
                CompanyId = 1,
                CompanyIdValue = "1",
                CompanyName = name,
                ReviewViews = new List<ReviewView>()
            };
        }

        public static ReviewView BuildReviewView(string name)
        {
            return new ReviewView()
            {
                Id = 1,
                Content = "Test",
                ReviewDate = DateTime.MinValue,
                ProductId = 1
            };
        }
    }
}