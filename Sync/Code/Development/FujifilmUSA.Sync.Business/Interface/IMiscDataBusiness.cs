using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMiscDataBusiness : ITaskBase
    {
        Dictionary<int, string> BranchList(int companyId);
        Dictionary<int, string> CompanyList();
        Dictionary<int, string> SourceProfileByCompany(int companyId);
        Dictionary<int, string> RegionList();
        Dictionary<int, string> UserRoleList();
        Dictionary<int, string> RegionListByCompnay(int companyId);
    }
}
