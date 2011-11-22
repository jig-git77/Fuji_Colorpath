using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class BranchBusinessMock : IBranchBusiness
    {
        #region IBranchBusiness Members

        public Dictionary<int, string> GetBranchList(int CompanyId)
        {
            return new Dictionary<int, string>();
        }

        #endregion
    }
}
