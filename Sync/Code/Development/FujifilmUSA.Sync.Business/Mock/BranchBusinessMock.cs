using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class BranchBusinessMock : IBranchBusiness
    {
        #region IBranchBusiness Members

        public Dictionary<int, string> GetBranchList(int companyId)
        {
            return new Dictionary<int, string>();
        }

        #endregion
    }
}
