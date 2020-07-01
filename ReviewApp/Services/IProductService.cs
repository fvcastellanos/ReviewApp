using System.Collections.Generic;
using LanguageExt;
using ReviewApp.Domain.Views;

namespace ReviewApp.Services
{
    public interface IProductService
    {
        Either<string, IEnumerable<ProductView>> GetAll();
        Either<string, ProductView> Get(long id);
        Either<string, ProductView> Add(ProductView productView);
        Either<string, ProductView> Update(ProductView productView);
        Either<string, long> Delete(long id);
    }
}