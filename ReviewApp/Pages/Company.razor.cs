using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using ReviewApp.Domain.Views;
using ReviewApp.Services;

namespace ReviewApp.Pages
{
    public class CompanyBase : ComponentBase
    {
        [Inject]
        protected ICompanyService CompanyService { get; set; }

        protected IEnumerable<CompanyView> Companies;
        protected string ErrorMessage;
        protected bool HasError;

        protected CompanyView CompanyModel;
        protected bool DisplayAddModal;
        
        protected override void OnInitialized()
        {
            var result = CompanyService.GetAll();
            HasError = false;
            DisplayAddModal = false;

            result.Match(right =>
            {
                Companies = right;
                return Companies;
            }, left =>
            {
                HasError = true;
                ErrorMessage = left;
                
                Companies = new List<CompanyView>();
                return Companies;
            });

        }

        protected void ShowAddModal()
        {
            DisplayAddModal = true;
            CompanyModel = new CompanyView();
        }
    }
}