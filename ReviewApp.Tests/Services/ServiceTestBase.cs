using System;
using Moq;
using ReviewApp.Data;
using ReviewApp.Tests.Data;

namespace ReviewApp.Tests.Services
{
    public abstract class ServiceTestBase
    {
        protected readonly Mock<TestReviewContext> DbContextMock = new Mock<TestReviewContext>();

        protected static Exception TestException()
        {
            return new Exception("test exception");
        }
    }
}