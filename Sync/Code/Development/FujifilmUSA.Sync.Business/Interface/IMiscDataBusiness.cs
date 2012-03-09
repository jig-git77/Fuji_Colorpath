using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMiscDataBusiness : ITaskBase
    {
        Dictionary<int, string> GetBranchList(int companyId);
        Dictionary<int, string> GetCompanyList();
    }
}
