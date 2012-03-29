using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
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

        public List<Company> GetCompany(int compnayId)
        {
            return new List<Company>();
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion


      
    }
}
