using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface ICompanyBusiness : ITaskBase
    {
        int InsertCompany(Company company);
        int UpdateCompany(Company company);
        bool DeleteCompany(int companyId);
        List<Company> GetCompany(int companyId);
    }
}
