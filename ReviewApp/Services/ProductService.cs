using System;
using System.Collections.Generic;
using LanguageExt;
using Microsoft.Extensions.Logging;
using ReviewApp.Data;
using ReviewApp.Domain.Views;

namespace ReviewApp.Services
{
    public class ProductService: IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly ReviewContext _dbContext;

        public ProductService(ReviewContext dbContext, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ProductService>();
            _dbContext = dbContext;
        }
        
        public Either<string, IEnumerable<ProductView>> GetAll()
        {
            try
            {
                var products = _dbContext.Products;
                
                
                
            }
            catch (Exception ex)
            {
                _logger.LogError("can't get product list - ", ex);
                return "can't get product list";
            }
            throw new System.NotImplementedException();
        }

        public Either<string, ProductView> Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public Either<string, ProductView> Add(ProductView productView)
        {
            throw new System.NotImplementedException();
        }

        public Either<string, ProductView> Update(ProductView productView)
        {
            throw new System.NotImplementedException();
        }

        public Either<string, long> Delete(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}