using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ReviewApp.Services;
using Moq.EntityFrameworkCore;
using ReviewApp.Tests.Fixtures;
using System.Collections.Generic;
using ReviewApp.Data;
using LanguageExt.UnitTesting;
using FluentAssertions;
using ReviewApp.Domain.Views;
using System;
using Moq;

namespace ReviewApp.Tests.Services
{
    [TestFixture]
    public class CompanyServiceTest : ServiceTestBase
    {
        private CompanyService _companyService;

        [SetUp]
        public void Setup()
        {
            _companyService = new CompanyService(DbContextMock.Object, new LoggerFactory());
        }

        [Test]
        public void TestGetCompanies()
        {
            DbContextMock.Setup(context => context.Companies)
                .ReturnsDbSet(BuildCompanyList());

            var expectedList = BuildCompanyViewList();

            var result = _companyService.GetAll();

            result.ShouldBeRight(right => 
                right.Should().BeEquivalentTo(expectedList)
            );

            DbContextMock.Verify(context => context.Companies);
        }

        [Test]
        public void TestGetCompaniesThrowsError()
        {
            DbContextMock.Setup(context => context.Companies)
                .Throws(new Exception("test exception"));

            var result = _companyService.GetAll();

            result.ShouldBeLeft(left => left.Should().Be("can't get company list"));

            DbContextMock.Verify(context => context.Companies);
        }

        [Test]
        public void TestGetCompanyNotFound()
        {
            const long id = 1;
            
            DbContextMock.Setup(context => context.Companies)
                .ReturnsDbSet(new List<Company>());
            
            var result = _companyService.Get(id);
            
            result.ShouldBeLeft(left => left.Should().Be("company not found"));
            
            DbContextMock.Verify(context => context.Companies);
        }

        [Test]
        public void TestGetCompanyThrowsException()
        {
            const long id = 1;

            DbContextMock.Setup(context => context.Companies)
                .Throws(new Exception("test exception"));
            
            var result = _companyService.Get(id);
            
            result.ShouldBeLeft(left => left.Should().Be("can't get company"));
            
            DbContextMock.Verify(context => context.Companies);
        }

        [Test]
        public void TestGetCompany()
        {
            const long id = 1;
            var company = DataFixture.BuildCompany("test");
            var view = ViewFixture.BuildCompanyView("test");

            DbContextMock.Setup(context => context.Companies.Find(It.IsAny<long>()))
                .Returns(company);

            var result = _companyService.Get(id);
            
            result.ShouldBeRight(right => right.Should().BeEquivalentTo(view));
            
            DbContextMock.Verify(context => context.Companies);
        }

        [Test]
        public void TestAddCompany()
        {
            DbContextMock.Setup(context => context.Companies)
                .ReturnsDbSet(new List<Company>());

            var view = ViewFixture.BuildCompanyView("test");

            var result = _companyService.Add(view);

            result.ShouldBeRight(right => right.Should().BeEquivalentTo(view));

            DbContextMock.Verify(context => context.Companies);
            DbContextMock.Verify(context => context.Companies.Add(It.IsAny<Company>()));
            DbContextMock.Verify(context => context.SaveChanges());
        }

        [Test]
        public void TestAddExistingCompany()
        {
            DbContextMock.Setup(context => context.Companies)
                .ReturnsDbSet(BuildCompanyList());

            var view = ViewFixture.BuildCompanyView("test");

            var result = _companyService.Add(view);

            result.ShouldBeLeft(left => left.Should().Be("company already exists"));

            DbContextMock.Verify(context => context.Companies);
        }

        [Test]
        public void TestAddThrowsException()
        {
            DbContextMock.Setup(context => context.Companies)
                .Throws(new Exception("test exception"));

            var view = ViewFixture.BuildCompanyView("test");

            var result = _companyService.Add(view);

            result.ShouldBeLeft(left => left.Should().Be("can't add company"));

            DbContextMock.Verify(context => context.Companies);
        }

        [Test]
        public void TestUpdateNonExistingCompany()
        {
            DbContextMock.Setup(context => context.Companies)
                .ReturnsDbSet(new List<Company>());

            var view = ViewFixture.BuildCompanyView("test");

            var result = _companyService.Update(view);

            result.ShouldBeLeft(left => left.Should().Be("company not found"));

            DbContextMock.Verify(context => context.Companies);
        }

        [Test]
        public void TestUpdateThrowsException()
        {
            DbContextMock.Setup(context => context.Companies)
                .Throws(new Exception("test exception"));

            var view = ViewFixture.BuildCompanyView("test");

            var result = _companyService.Update(view);

            result.ShouldBeLeft(left => left.Should().Be("can't update company"));

            DbContextMock.Verify(context => context.Companies);
        }

        [Test]
        public void TestUpdate()
        {
            var company = DataFixture.BuildCompany("test");
            var view = ViewFixture.BuildCompanyView("test");
            
            DbContextMock.Setup(context => context.Companies.Find(view.Id))
                .Returns(company);

            var result = _companyService.Update(view);

            result.ShouldBeRight(right => right.Should().BeEquivalentTo(view));

            DbContextMock.Verify(context => context.Companies);
            DbContextMock.Verify(context => context.Companies.Update(company));
            DbContextMock.Verify(context => context.SaveChanges());
        }

        [Test]
        public void TestDelete()
        {
            const int id = 1;
            var company = DataFixture.BuildCompany("test");
            
            DbContextMock.Setup(context => context.Companies.Find(It.IsAny<long>()))
                .Returns(company);

            var result = _companyService.Delete(id);

            result.ShouldBeRight(right => right.Should().Be(id));

            DbContextMock.Verify(context => context.Companies);
            DbContextMock.Verify(context => context.Companies.Remove(company));
            DbContextMock.Verify(context => context.SaveChanges());
        }

        [Test]
        public void TestDeleteThrowsException()
        {
            const int id = 1;
            var company = DataFixture.BuildCompany("test");

            DbContextMock.Setup(context => context.Companies.Find(It.IsAny<long>()))
                .Throws(new Exception("test exception"));

            var result = _companyService.Delete(id);

            result.ShouldBeLeft(left => left.Should().Be("can't delete company"));

            DbContextMock.Verify(context => context.Companies);
        }

        // ---------------------------------------------------------------------------------------------------------

        private static IEnumerable<Company> BuildCompanyList()
        {
            return new List<Company>()
            {
                DataFixture.BuildCompany("test")
            };
        }

        private static List<CompanyView> BuildCompanyViewList()
        {
            return new List<CompanyView>()
            {
                ViewFixture.BuildCompanyView("test")
            };
        }
    }
}