using System;
using System.Collections.Generic;
using FluentAssertions;
using LanguageExt.UnitTesting;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using ReviewApp.Data;
using ReviewApp.Domain.Views;
using ReviewApp.Services;
using ReviewApp.Tests.Fixtures;

namespace ReviewApp.Tests.Services
{
    [TestFixture]
    public class ProductServiceTest: ServiceTestBase
    {
        private IProductService _productService;

        private const string Name = "test";
        private static readonly IEnumerable<Product> ProductList = BuildProductList();
        private static readonly IEnumerable<ProductView> ProductViewList = BuildProductViewList();
        private static readonly Product Product = DataFixture.BuildProduct(Name);
        private static readonly ProductView ProductView = ViewFixture.BuildProductView(Name);
        
        [SetUp]
        public void SetUp()
        {
            _productService = new ProductService(DbContextMock.Object, new LoggerFactory());
        }

        [TearDown]
        public void TearDown()
        {
            DbContextMock.Reset();
        }

        [Test]
        public void TestGetProductsThrowsException()
        {
            DbContextMock.Setup(context => context.Products)
                .Throws(TestException());

            var result = _productService.GetAll();
            
            result.ShouldBeLeft(left => left.Should().Be("can't get product list"));
            
            DbContextMock.Verify(context => context.Products);
        }

        [Test]
        public void TestGetProducts()
        {
            DbContextMock.Setup(context => context.Products)
                .ReturnsDbSet(ProductList);

            var result = _productService.GetAll();
            
            result.ShouldBeRight(right => right.Should().BeEquivalentTo(ProductViewList));
            
            DbContextMock.Verify(context => context.Products);
        }

        [Test]
        public void TestGetProductNotFound()
        {
            DbContextMock.Setup(context => context.Products.Find(It.IsAny<long>()))
                .Returns(() => null);

            var result = _productService.Get(1);
            
            result.ShouldBeLeft(left => left.Should().Be("product not found"));
            
            DbContextMock.Verify(context => context.Products.Find(It.IsAny<long>()));
        }

        [Test]
        public void TestGetProductThrowsException()
        {
            DbContextMock.Setup(context => context.Products.Find(It.IsAny<long>()))
                .Throws(TestException());

            var result = _productService.Get(1);
            
            result.ShouldBeLeft(left => left.Should().Be("can't get product"));

            DbContextMock.Verify(context => context.Products.Find(It.IsAny<long>()));
        }

        [Test]
        public void TestGetProduct()
        {
            DbContextMock.Setup(context => context.Products.Find(It.IsAny<long>()))
                .Returns(Product);

            var result = _productService.Get(1);
            
            result.ShouldBeRight(right => right.Should().BeEquivalentTo(ProductView));

            DbContextMock.Verify(context => context.Products.Find(It.IsAny<long>()));
        }

        [Test]
        public void TestAddExistingProduct()
        {
            DbContextMock.Setup(context => context.Products)
                .ReturnsDbSet(ProductList);

            var result = _productService.Add(ProductView);
            
            result.ShouldBeLeft(left => left.Should().Be("product already exists"));
            
            DbContextMock.Verify(context => context.Products);
        }

        [Test]
        public void TestAddProductThrowsException()
        {
            DbContextMock.Setup(context => context.Products)
                .ReturnsDbSet(new List<Product>());

            DbContextMock.Setup(context => context.SaveChanges())
                .Throws(TestException());

            var result = _productService.Add(ProductView);
            
            result.ShouldBeLeft(left => left.Should().Be("can't add product"));
            
            DbContextMock.Verify(context => context.Products);
            DbContextMock.Verify(context => context.Products.Add(It.IsAny<Product>()));
            DbContextMock.Verify(context => context.SaveChanges());
        }

        [Test]
        public void TestAddProduct()
        {
            DbContextMock.Setup(context => context.Products)
                .ReturnsDbSet(new List<Product>());

            var result = _productService.Add(ProductView);
            
            result.ShouldBeRight(right => right.Should().BeOfType<ProductView>());
            
            DbContextMock.Verify(context => context.Products);
            DbContextMock.Verify(context => context.Products.Add(It.IsAny<Product>()));
            DbContextMock.Verify(context => context.SaveChanges());
        }

        [Test]
        public void TestUpdateProductNotFound()
        {
            DbContextMock.Setup(context => context.Products)
                .ReturnsDbSet(new List<Product>());
            
            var result = _productService.Update(ProductView);
            
            result.ShouldBeLeft(left => left.Should().Be("product not found"));

            DbContextMock.Verify(context => context.Products);
        }

        [Test]
        public void TestUpdateProductThrowsException()
        {
            DbContextMock.Setup(context => context.Products).
                Throws(TestException());

            var result = _productService.Update(ProductView);
            
            result.ShouldBeLeft(left => left.Should().Be("can't update product"));

            DbContextMock.Verify(context => context.Products);
        }

        [Test]
        public void TestUpdateProduct()
        {
            DbContextMock.Setup(context => context.Products.Find(It.IsAny<long>()))
                .Returns(Product);

            var result = _productService.Update(ProductView);
            
            result.ShouldBeRight(right => right.Should().BeEquivalentTo(ProductView));

            DbContextMock.Verify(context => context.Products.Find(It.IsAny<long>()));
            DbContextMock.Verify(context => context.Products.Update(It.IsAny<Product>()));
            DbContextMock.Verify(context => context.SaveChanges());
        }

        [Test]
        public void TestDeleteNonExistingProduct()
        {
            const long id = 1;
            
            DbContextMock.Setup(context => context.Products.Find(It.IsAny<long>()))
                .Returns(() => null);

            var result = _productService.Delete(id);
            
            result.ShouldBeRight(right => right.Should().Be(id));
            
            DbContextMock.Verify(context => context.Products.Find(It.IsAny<long>()));
        }

        [Test]
        public void TestDeleteThrowsException()
        {
            const long id = 1;
            
            DbContextMock.Setup(context => context.Products.Find(It.IsAny<long>()))
                .Returns(Product);

            DbContextMock.Setup(context => context.Products.Remove(It.IsAny<Product>()))
                .Throws(TestException());
            
            var result = _productService.Delete(id);
            
            result.ShouldBeLeft(left => left.Should().Be("can't delete product"));
            
            DbContextMock.Verify(context => context.Products.Find(It.IsAny<long>()));
            DbContextMock.Verify(context => context.Products.Remove(It.IsAny<Product>()));
        }

        [Test]
        public void TestDeleteProduct()
        {
            const long id = 1;
            
            DbContextMock.Setup(context => context.Products.Find(It.IsAny<long>()))
                .Returns(Product);

            var result = _productService.Delete(id);
            
            result.ShouldBeRight(right => right.Should().Be(id));
            
            DbContextMock.Verify(context => context.Products.Find(It.IsAny<long>()));
            DbContextMock.Verify(context => context.Products.Remove(It.IsAny<Product>()));
            DbContextMock.Verify(context => context.SaveChanges());
        }
        
        // ----------------------------------------------------------------------------------------
        
        private static IEnumerable<Product> BuildProductList()
        {
            return new List<Product>()
            {
                DataFixture.BuildProduct("test")

            };
        }

        private static IEnumerable<ProductView> BuildProductViewList()
        {
            return new List<ProductView>()
            {
                ViewFixture.BuildProductView("test")
            };
        }
    }
}