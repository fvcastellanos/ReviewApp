using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using ReviewApp.Mappers;
using ReviewApp.Data;
using ReviewApp.Domain.Views;

namespace ReviewApp.Tests.Mappers
{
    [TestFixture]
    public class CompanyMapperTest
    {
        [Test]
        public void ConvertToModel()
        {
            var view = BuildCompanyView();
            var modelExpected = BuildCompany();

            var model = CompanyMapper.ToModel(view);

            model.Should().BeEquivalentTo(modelExpected);
        }

        [Test]
        public void ConvertToView()
        {
            var model = BuildCompany();
            var viewExpected = BuildCompanyView();

            var view = CompanyMapper.ToView(model);

            view.Should().BeEquivalentTo(viewExpected);
        }

        // -------------------------------------------------------------------------------------------

        private static Company BuildCompany()
        {
            return new Company()
            {
                Id = 1,
                Name = "Test Company",
                Description = "Test Company"
            };
        }

        private static CompanyView BuildCompanyView()
        {
            return new CompanyView()
            {
                Id = 1,
                Name = "Test Company",
                Description = "Test Company",
                ProductViews = new List<ProductView>()
            };
        }
    }
}