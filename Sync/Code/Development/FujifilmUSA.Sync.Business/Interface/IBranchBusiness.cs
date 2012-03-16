using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IBranchBusiness : ITaskBase
    {
        int InsertBranch(Branch branch);
        int UpdateBranch(Branch branch);
        bool DeleteBranch(int branchId);
        List<Branch> GetBranches();
    }
}
