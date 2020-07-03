using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using ReviewApp.Domain.Views;
using ReviewApp.Services;

namespace ReviewApp.Pages
{
    public class ProductBase: PageBase
    {
        [Inject]
        protected IProductService ProductService { get; set; }
        
        [Inject]
        protected ICompanyService CompanyService { get; set; }

        protected IEnumerable<CompanyView> Companies;
        protected IEnumerable<ProductView> Products;
        protected ProductView ProductModel;
        
        protected override void OnInitialized()
        {
            GetProducts();
            GetCompanies();
        }
        
        protected void ShowAddModal()
        {
            ShowModal();
            ProductModel = new ProductView();
        }

        protected void GetProduct(long id)
        {
            var result = ProductService.Get(id);

            result.Match(ShowEditModal, DisplayError);
        }

        protected void SaveChanges()
        {
            if (ModifyModal)
            {
                UpdateProduct();
                return;
            }
            
            AddProduct();
        }

        protected void DeleteProduct()
        {
            
        }
        
        // ------------------------------------------------------------------------------------
        
        private void GetProducts()
        {
            var result = ProductService.GetAll();
            HasError = false;
            DisplayModal = false;

            result.Match(right =>
            {
                Products = right;
                
            }, left =>
            {
                Products = new List<ProductView>();
                DisplayError(left);
            });
        }

        private void GetCompanies()
        {
            var result = CompanyService.GetAll();

            result.Match(right =>
            {
                Companies = right;
                
            }, left =>
            {
                Companies = new List<CompanyView>();
                DisplayError(left);
            });
        }

        private void ShowEditModal(ProductView productView)
        {
            ShowModal();
            ModifyModal = true;
            ProductModel = productView;
        }

        private void AddProduct()
        {
            ProductModel.CompanyId = long.Parse(ProductModel.CompanyIdValue);
            var result = ProductService.Add(ProductModel);

            result.Match(right =>
            {
                HideModal();
                HideModalError();
                GetProducts();
                GetCompanies();

            }, DisplayModalError);
        }

        private void UpdateProduct()
        {
            
        }
        
    }
}