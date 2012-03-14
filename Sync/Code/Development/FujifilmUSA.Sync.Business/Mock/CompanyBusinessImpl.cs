using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using System;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class CompanyBusinessMock : ICompanyBusiness
    {
        #region  ICompnayBusiness Members

        public int InsertCompany(Company company)
        {
            return 0;
        }

        public int UpdateCompnay(Company company)
        {
            return 0;
        }

        public bool DeleteCompany(int companyId)
        {
            return true;
        }

        public List<Company> GetCompanies()
        {
            return new List<Company>();
        }

        #endregion
    }
}
