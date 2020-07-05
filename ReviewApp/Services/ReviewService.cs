using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReviewApp.Data;
using ReviewApp.Domain.Views;
using ReviewApp.Mappers;

namespace ReviewApp.Services
{
    public class ReviewService: IReviewService
    {
        private readonly ILogger<ReviewService> _logger;
        private readonly ReviewContext _dbContext;

        private readonly ITextAnalysisService _textAnalysisService;

        public ReviewService(ReviewContext dbContext, ITextAnalysisService textAnalysisService, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ReviewService>();
            _dbContext = dbContext;
            _textAnalysisService = textAnalysisService;
        }
        
        public Either<string, IEnumerable<ReviewView>> GetProductReviews(long productId)
        {
            try
            {
                var reviews = _dbContext.Reviews
                    .Include(review => review.TextAnalysis)
                    .Filter(review => review.ProductId == productId)
                    .OrderByDescending(review => review.Id)
                    .ToList();

                return reviews.Select(ReviewMapper.ToView)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("can't get review list - ", ex);
                return "can't get review list";
            }
        }

        public Either<string, ReviewView> Get(long id)
        {
            try
            {
                var review = _dbContext.Reviews.Find(id);

                return ReviewMapper.ToView(review);
            }
            catch (Exception ex)
            {
                _logger.LogError("can't get review - ", ex);
                return "can't get review";
            }
        }

        public Either<string, ReviewView> Add(ReviewView reviewView)
        {
            try
            {
                var review = ReviewMapper.ToModel(reviewView);

                _dbContext.Reviews.Add(review);
                _dbContext.SaveChanges();

                _textAnalysisService.SaveContentAnalysis(review.Content, review.Id);

                return ReviewMapper.ToView(review);
            }
            catch (Exception ex)
            {
                _logger.LogError("can't add review - ", ex);
                return "can't add review";
            }
        }

        public Either<string, ReviewView> Update(ReviewView reviewView)
        {
            try
            {
                var review = _dbContext.Reviews.Find(reviewView.Id);

                if (review == null)
                {
                    return "review not found";
                }

                review.Title = reviewView.Title;
                review.Content = reviewView.Content;
                review.ReviewDate = DateTime.Now;
                review.Stars = reviewView.Stars;

                _dbContext.Reviews.Update(review);
                _dbContext.SaveChanges();

                return ReviewMapper.ToView(review);
            }
            catch (Exception ex)
            {
                _logger.LogError("can't update review - ", ex);
                return "can't update review";
            }
        }

        public Either<string, long> Delete(long id)
        {
            try
            {
                var review = _dbContext.Reviews.Find(id);

                if (review == null)
                {
                    return id;
                }

                _dbContext.Reviews.Remove(review);
                _dbContext.SaveChanges();

                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError("can't delete review - ", ex);
                return "can't delete review";
            }
        }
    }
}