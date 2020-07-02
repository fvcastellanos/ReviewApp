using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using ReviewApp.Domain.Views;
using ReviewApp.Services;

namespace ReviewApp.Pages
{
    public class CompanyBase : PageBase
    {
        [Inject]
        protected ICompanyService CompanyService { get; set; }

        protected IEnumerable<CompanyView> Companies;
        protected CompanyView CompanyModel;
        
        protected override void OnInitialized()
        {
            GetCompanies();
        }

        protected void SaveChanges()
        {
            if (ModifyModal)
            {
                UpdateCompany();
                return;
            }
            
            AddCompany();
        }
        
        protected void ShowAddModal()
        {
            ShowModal();
            CompanyModel = new CompanyView();
        }
        
        protected void GetCompany(long id)
        {
            var result = CompanyService.Get(id);

            result.Match(ShowEditModal, DisplayError);
        }

        protected void DeleteCompany()
        {
            var result = CompanyService.Delete(DeleteView.Id);

            result.Match(right =>
            {
                HideModalError();
                HideDeleteModal();
                GetCompanies();
                
            }, DisplayModalError);
        }
        
        // ------------------------------------------------------------------------------------

        private void AddCompany()
        {
            var result = CompanyService.Add(CompanyModel);

            result.Match(right =>
            {
                HideModal();
                HideModalError();
                GetCompanies();

            }, DisplayModalError);
        }

        private void UpdateCompany()
        {
            var result = CompanyService.Update(CompanyModel);

            result.Match(right =>
            {
                HideModal();
                HideModalError();
                GetCompanies();
            }, DisplayModalError);
        }
        
        private void ShowEditModal(CompanyView companyView)
        {
            ShowModal();
            ModifyModal = true;
            CompanyModel = companyView;
        }
        
        private void GetCompanies()
        {
            var result = CompanyService.GetAll();
            HasError = false;
            DisplayModal = false;

            result.Match(right =>
            {
                Companies = right;
                return Companies;
            }, left =>
            {
                DisplayError(left);
                Companies = new List<CompanyView>();
                return Companies;
            });
        }
    }
}