using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IBranchBusiness : ITaskBase
    {
        Dictionary<int, string> GetBranchList(int CompanyId);
    }
}
