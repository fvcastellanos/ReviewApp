using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Microsoft.Extensions.Logging;
using ReviewApp.Data;
using ReviewApp.Domain.Views;
using ReviewApp.Mappers;

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

                return products.Select(ProductMapper.ToView)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("can't get product list - ", ex);
                return "can't get product list";
            }
        }

        public Either<string, ProductView> Get(long id)
        {
            try
            {
                var product = _dbContext.Products.Find(id);

                if (product == null)
                {
                    return "product not found";
                }

                return ProductMapper.ToView(product);
            }
            catch (Exception ex)
            {
                _logger.LogError("can't get product with id: {0} - ", id, ex);
                return "can't get product";
            }
        }

        public Either<string, ProductView> Add(ProductView productView)
        {
            try
            {
                var productExists = _dbContext.Products.Exists(p => p.Name.Equals(productView.Name));

                if (productExists)
                {
                    return "product already exists";
                }

                var product = ProductMapper.ToModel(productView);
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();

                return ProductMapper.ToView(product);
            }
            catch (Exception ex)
            {
                _logger.LogError("can't add product - ", ex);
                return "can't add product";
            }
        }

        public Either<string, ProductView> Update(ProductView productView)
        {
            try
            {
                var product = _dbContext.Products.Find(productView.Id);

                if (product == null)
                {
                    return "product not found";
                }

                _dbContext.Products.Update(product);
                _dbContext.SaveChanges();

                return ProductMapper.ToView(product);
            }
            catch (Exception ex)
            {
                _logger.LogError("can't update product - ", ex);
                return "can't update product";
            }
            
        }

        public Either<string, long> Delete(long id)
        {
            try
            {
                var product = _dbContext.Products.Find(id);

                if (product == null)
                {
                    return id;
                }

                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();

                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError("can't delete product - ", ex);
                return "can't delete product";
            }
        }
    }
}