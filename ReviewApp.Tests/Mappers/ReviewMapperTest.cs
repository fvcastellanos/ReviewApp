using FluentAssertions;
using NUnit.Framework;
using ReviewApp.Data;
using ReviewApp.Domain.Views;
using ReviewApp.Mappers;
using ReviewApp.Tests.Fixtures;

namespace ReviewApp.Tests.Mappers
{
    [TestFixture]
    public class ReviewMapperTest
    {
        private const string Name = "test";

        private static readonly Review  Review = DataFixture.BuildReview(Name);
        private static readonly ReviewView ReviewView = ViewFixture.BuildReviewView(Name);
        
        [Test]
        public void ConvertToModel()
        {
            var model = ReviewMapper.ToModel(ReviewView);

            model.Should().BeEquivalentTo(Review);
        }

        [Test]
        public void ConvertToView()
        {
            var view = ReviewMapper.ToView(Review);

            view.Should().BeEquivalentTo(ReviewView);
        }
        
    }
}