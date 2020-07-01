using System;
using Moq;
using ReviewApp.Data;

namespace ReviewApp.Tests.Services
{
    public abstract class ServiceTestBase
    {
        protected readonly Mock<ReviewContext> DbContextMock = new Mock<ReviewContext>();

        protected Exception TestException()
        {
            return new Exception("test exception");
        }
    }
}