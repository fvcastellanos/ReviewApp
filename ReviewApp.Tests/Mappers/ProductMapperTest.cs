using FluentAssertions;
using NUnit.Framework;
using ReviewApp.Data;
using ReviewApp.Domain.Views;
using ReviewApp.Mappers;
using ReviewApp.Tests.Fixtures;

namespace ReviewApp.Tests.Mappers
{
    [TestFixture]
    public class ProductMapperTest
    {
        private const string Name = "test";

        private static readonly Product Product = DataFixture.BuildProduct(Name);
        private static readonly ProductView ProductView = ViewFixture.BuildProductView(Name);
        
        [Test]
        public void ConvertToModel()
        {
            var model = ProductMapper.ToModel(ProductView);

            model.Should().BeEquivalentTo(Product);
        }

        [Test]
        public void ConvertToView()
        {
            var view = ProductMapper.ToView(Product);

            view.Should().BeEquivalentTo(ProductView);
        }
    }
}