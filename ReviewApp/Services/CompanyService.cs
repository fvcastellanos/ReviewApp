using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Microsoft.Extensions.Logging;
using ReviewApp.Mappers;
using ReviewApp.Data;
using ReviewApp.Domain.Views;

namespace ReviewApp.Services
{
    public class CompanyService: ICompanyService
    {
        private readonly ILogger _logger;

        private readonly ReviewContext _dbContext;

        public CompanyService(ReviewContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<CompanyService>();
        }

        public Either<string, IEnumerable<CompanyView>> GetAll()
        {
            try
            {
                _logger.LogInformation("get company list");

                return _dbContext.Companies.Select(company => CompanyMapper.ToView(company))
                    .ToList();

            }
            catch(Exception ex)
            {
                _logger.LogError("can't get company list: ", ex);
                return "can't get company list";
            }
        }

        public Either<string, CompanyView> Get(long id)
        {
            try
            {
                var company = _dbContext.Companies.Find(id);

                if (company == null)
                {
                    return "company not found";
                }

                return CompanyMapper.ToView(company);
            }
            catch (Exception ex)
            {
                _logger.LogError("can't get company with id: {0} - ", id, ex);
                return "can't get company";
            }
        }
        
        public Either<string, CompanyView> Add(CompanyView companyView)
        {
            try
            {
                var companyExists = _dbContext.Companies.Exists(company => company.Name.Equals(companyView.Name));

                if (companyExists) 
                {
                    return "company already exists";
                }

                var model = CompanyMapper.ToModel(companyView);
                var view = companyView;

                _dbContext.Companies.Add(model);
                _dbContext.SaveChanges();

                view.Id = model.Id;

                return view;
            }
            catch(Exception ex)
            {
                _logger.LogError("can't add company: {0} - ", companyView.Name, ex);
                return string.Format("can't add company");
            }
        }

        public Either<string, CompanyView> Update(CompanyView companyView)
        {
            try
            {
                var company = _dbContext.Companies.Find(companyView.Id);

                if (company == null)
                {
                    return string.Format("company not found");
                }

                company.Name = companyView.Name;
                company.Description = companyView.Description;

                _dbContext.Companies.Update(company);
                _dbContext.SaveChanges();

                return companyView;                
            }
            catch(Exception ex)
            {
                _logger.LogError("can't update company: {0} - ", companyView.Name, ex);
                return "can't update company";
            }
        }

        public Either<string, long> Delete(long id)
        {
            try
            {
                var company = _dbContext.Companies.Find(id);

                if (company != null)
                {
                    _dbContext.Companies.Remove(company);
                    _dbContext.SaveChanges();
                }

                return id;
            }
            catch(Exception ex)
            {
                _logger.LogError("can't delete company with id: {0} - ", id, ex);
                return "can't delete company";
            }
        }
    }
}