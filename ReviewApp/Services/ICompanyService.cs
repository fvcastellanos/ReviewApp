using System.Collections.Generic;
using LanguageExt;
using ReviewApp.Data;
using ReviewApp.Domain.Views;

namespace ReviewApp.Services
{
    public interface ICompanyService
    {
        Either<string, IEnumerable<CompanyView>> GetAll();
        Either<string, CompanyView> Get(long id);
        Either<string, CompanyView> Add(CompanyView companyView);
        Either<string, CompanyView> Update(CompanyView companyView);
        Either<string, long> Delete(long id);
    }
}