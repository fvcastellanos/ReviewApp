using System;
using ReviewApp.Data;

namespace ReviewApp.Tests.Fixtures
{
    public static class DataFixture
    {
        public static Company BuildCompany(string name)
        {
            return new Company()
            {
                Id = 1,
                Name = name,
                Description = "Test"
            };
        }

        public static Product BuildProduct(string name)
        {
            return new Product()
            {
                Id = 1,
                Name = name,
                Description = "Test",
                CompanyId = 1,
                Company = BuildCompany(name)
            };
        }

        public static Review BuildReview(string name)
        {
            return new Review()
            {
                Id = 1,
                Content = "Test",
                ReviewDate = DateTime.MinValue,
                ProductId = 1
            };
        }
    }
}