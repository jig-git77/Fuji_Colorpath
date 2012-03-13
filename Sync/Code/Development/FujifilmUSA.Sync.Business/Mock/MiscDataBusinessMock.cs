using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MiscDataBusinessMock : IMiscDataBusiness
    {
        #region IMiscDataBusiness Members

        public Dictionary<int, string> BranchList(int companyId)
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<int, string> CompanyList()
        {
            return new Dictionary<int, string>();
        }
        public Dictionary<int, string> SourceProfileByCompany(int companyId)
        {
            return new Dictionary<int, string>();
        }

        #endregion
    }
}
