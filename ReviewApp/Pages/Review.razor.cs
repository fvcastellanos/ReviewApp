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

        protected ProductView ProductModel;
        protected IEnumerable<ReviewView> Reviews = new List<ReviewView>();

        protected override void OnInitialized()
        {
            GetProduct(Id);
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
    }
}