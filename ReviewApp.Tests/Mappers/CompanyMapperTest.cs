using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using ReviewApp.Mappers;
using ReviewApp.Data;
using ReviewApp.Domain.Views;

namespace ReviewApp.Tests.Mappers
{
    public class CompanyMapperTest
    {
        [Test]
        public void ConvertToModel()
        {
            var view = buildCompanyView();
            var modelExpected = buildCompany();

            var model = CompanyMapper.ToModel(view);

            model.Should().BeEquivalentTo(modelExpected);
        }

        public void ConvertToView()
        {
            var model = buildCompany();
            var viewExpected = buildCompanyView();

            var view = CompanyMapper.ToView(model);

            model.Should().BeEquivalentTo(viewExpected);
        }

        // -------------------------------------------------------------------------------------------

        private Company buildCompany()
        {
            return new Company()
            {
                Id = 1,
                Name = "Test Company",
                Description = "Test Company"
            };
        }

        private CompanyView buildCompanyView()
        {
            return new CompanyView()
            {
                Id = 1,
                Name = "Test Company",
                Description = "Test Company"
            };
        }
    }
}