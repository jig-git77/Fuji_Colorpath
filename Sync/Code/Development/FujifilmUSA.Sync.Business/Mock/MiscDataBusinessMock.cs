using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MiscDataBusinessMock : IMiscDataBusiness
    {
        #region IMiscDataBusiness Members

        public Dictionary<int, string> GetBranchList(int companyId)
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<int, string> GetCompanyList()
        {
            return new Dictionary<int, string>();
        }

        #endregion
    }
}
