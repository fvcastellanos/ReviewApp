using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using ReviewApp.Domain.Views;
using ReviewApp.Services;

namespace ReviewApp.Pages
{
    public class ReviewBase: PageBase
    {
        [Parameter]
        public long Id { get; set; }

        [Inject]
        protected IProductService ProductService { get; set; }
        
        [Inject]
        protected IReviewService ReviewService { get; set; }
        
        [Inject]
        protected ITextAnalysisService TextAnalysisService { get; set; }

        protected ProductView ProductModel;
        protected IEnumerable<ReviewView> Reviews = new List<ReviewView>();
        protected ReviewView ReviewModel;
        protected IEnumerable<ProductAcceptanceView> ProductAcceptanceModel;

        protected override void OnInitialized()
        {
            GetProduct(Id);
            GetReviews(Id);
            GetProductAcceptance(Id);
        }

        protected void ShowAddModal()
        {
            ShowModal();
            ReviewModel = new ReviewView();
        }

        protected void SaveChanges()
        {
            if (ModifyModal)
            {
                UpdateReview();
                return;
            }
            
            AddReview();
        }

        protected void DeleteReview(long id)
        {
            var result = ReviewService.Delete(id);

            result.Match(right =>
            {
                HideModalError();
                GetProduct(Id);
                GetReviews(Id);
                GetProductAcceptance(Id);
            }, DisplayError);
        }
        
        protected void ShowEditModal(ReviewView reviewView)
        {
            ShowModal();
            ModifyModal = true;
            ReviewModel = reviewView;
        }
        
        // --------------------------------------------------------------------------------------------------

        private void GetProduct(long id)
        {
            var result = ProductService.Get(id);

            result.Match(right =>
            {
                ProductModel = right;
            }, DisplayError);
        }

        private void GetReviews(long id)
        {
            var result = ReviewService.GetProductReviews(id);

            result.Match(right =>
            {
                Reviews = right;
            }, left =>
            {
                DisplayError(left);
                Reviews = new List<ReviewView>();
            });
        }

        private void AddReview()
        {
            ReviewModel.ProductId = Id;
            ReviewModel.Stars = 1; // Set to one
            var result = ReviewService.Add(ReviewModel);

            result.Match(right =>
            {
                HideModal();
                HideModalError();
                GetProduct(Id);
                GetReviews(Id);
                GetProductAcceptance(Id);
            }, DisplayModalError);
        }

        private void UpdateReview()
        {
            var result = ReviewService.Update(ReviewModel);

            result.Match(right =>
            {
                HideModal();
                HideModalError();
                GetProduct(Id);
                GetReviews(Id);
            }, DisplayModalError);
        }

        private void GetProductAcceptance(long productId)
        {
            ProductAcceptanceModel = new List<ProductAcceptanceView>();
            ProductAcceptanceModel = TextAnalysisService.GroupBySentiment(productId);
        }
    }
}