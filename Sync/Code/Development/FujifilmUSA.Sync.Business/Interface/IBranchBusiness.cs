using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IBranchBusiness : ITaskBase
    {
        Dictionary<int, string> GetBranchList(int companyId);
    }
}
