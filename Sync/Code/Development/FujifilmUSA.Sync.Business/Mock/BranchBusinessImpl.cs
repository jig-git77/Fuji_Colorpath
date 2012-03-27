using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class BranchBusinessImpl  : IBranchBusiness
    {
        #region  ICompnayBusiness Members

        public int InsertBranch(Branch branch)
        {
            return 0;
        }

        public int UpdateBranch(Branch branch)
        {
            return 0;
        }

        public bool DeleteBranch(int companyId)
        {
            return true;
        }

        public List<Branch> GetBranches()
        {
            return new List<Branch>();
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
