using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMiscDataBusiness : ITaskBase
    {
        Dictionary<int, string> BranchList(int companyId);
        Dictionary<int, string> CompanyList();
    }
}
