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
    }
}
