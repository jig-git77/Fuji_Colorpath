using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class CompanyBusinessMock : ICompanyBusiness
    {
        #region  IcompanyBusiness Members

        public int InsertCompany(Company company)
        {
            return 0;
        }

        public int UpdateCompany(Company company)
        {
            return 0;
        }

        public bool DeleteCompany(int companyId)
        {
            return true;
        }

        public List<Company> GetCompany(int companyId,int currentUserId)
        {
            return new List<Company>();
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion


      
    }
}
