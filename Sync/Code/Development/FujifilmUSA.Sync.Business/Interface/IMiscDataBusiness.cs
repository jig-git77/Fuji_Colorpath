using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMiscDataBusiness : ITaskBase
    {
        Dictionary<int, string> GetBranchList(int companyId, int currentUserId);
        Dictionary<int, string> GetCompanyList(int currentUserId);
        Dictionary<int, string> GetLicenseList(int branchId);
        Dictionary<int, string> GetInstrumentTypeList();
        Dictionary<int, string> GetSourceProfileByCompany(int companyId);
        Dictionary<int, string> GetRegionList(int currentUserId);
        Dictionary<int, string> GetUserRoleList(int currentUserId);
        Dictionary<int, string> GetDeviceFamilyList();
        Dictionary<int, string> GetLicenseTypeList();
        Dictionary<int, string> GetCompaniesByRegion(int regionId,int currentUserId);
    }
}
