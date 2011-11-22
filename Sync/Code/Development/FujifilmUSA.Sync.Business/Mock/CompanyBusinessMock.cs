using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class CompanyBusinessMock : ICompanyBusiness
    {
        #region ICompanyBusiness Members

        public Dictionary<int, string> GetCompanyList()
        {
            return new Dictionary<int, string>();
        }

        #endregion
    }
}
