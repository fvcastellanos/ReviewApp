using Microsoft.EntityFrameworkCore;
using ReviewApp.Data;

namespace ReviewApp.Tests.Data
{
    public class TestReviewContext : ReviewContext
    {
        public TestReviewContext() : base(new DbContextOptions<ReviewContext>())
        {
        }
    }
}