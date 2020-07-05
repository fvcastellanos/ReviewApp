using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using ReviewApp.Domain.Upload;
using ReviewApp.Domain.Views;
using ReviewApp.Services;
using ReviewApp.Storage.Client;

namespace ReviewApp.Pages
{
    public class ProductBase: PageBase
    {
        [Inject]
        protected IProductService ProductService { get; set; }
        
        [Inject]
        protected ICompanyService CompanyService { get; set; }
        
        [Inject]
        protected SpacesClient SpacesClient { get; set; }

        protected IEnumerable<CompanyView> Companies;
        protected IEnumerable<ProductView> Products;
        protected ProductView ProductModel;
        protected FileUpload UploadedFile;
        
        protected override void OnInitialized()
        {
            GetProducts();
            GetCompanies();
        }
        
        protected void ShowAddModal()
        {
            ShowModal();
            ProductModel = new ProductView();
            UploadedFile = null;
        }

        protected void GetProduct(long id)
        {
            var result = ProductService.Get(id);

            result.Match(ShowEditModal, DisplayError);
        }

        protected void SaveChanges()
        {
            SaveUploadedFile();
            
            if (ModifyModal)
            {
                UpdateProduct();
                return;
            }
            
            AddProduct();
        }

        protected void DeleteProduct()
        {
            var result = ProductService.Delete(DeleteView.Id);

            result.Match(right =>
            {
                HideModalError();
                HideDeleteModal();
                GetProducts();
                GetCompanies();
            }, DisplayModalError);
        }

        protected async Task HandleSelectedFile(IFileListEntry[] files)
        {
            var file = files.FirstOrDefault();

            if (file != null)
            {
                var ms = new MemoryStream();
                await file.Data.CopyToAsync(ms);
                
                UploadedFile = new FileUpload()
                {
                    Name = file.Name,
                    Type = file.Type,
                    Data = ms.ToArray()
                };
            }
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
            UploadedFile = null;
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
            ProductModel.CompanyId = long.Parse(ProductModel.CompanyIdValue);
            var result = ProductService.Update(ProductModel);

            result.Match(right =>
            {
                HideModal();
                HideModalError();
                GetProducts();
                GetCompanies();

            }, DisplayModalError);
            
        }

        private void SaveUploadedFile()
        {
            if (UploadedFile != null)
            {
                var ms = new MemoryStream(UploadedFile.Data);
                var url = SpacesClient.PutObject(ms, UploadedFile.Type, UploadedFile.Name);

                if (!string.IsNullOrEmpty(url))
                {
                    ProductModel.ImageUrl = url;
                }
            }
        }
    }
}