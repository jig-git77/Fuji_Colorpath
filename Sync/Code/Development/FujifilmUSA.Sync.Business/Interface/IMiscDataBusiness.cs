using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMiscDataBusiness : ITaskBase
    {
        Dictionary<int, string> GetBranchList(int companyId);
        Dictionary<int, string> GetCompanyList();
        Dictionary<int, string> GetLicenseList(int branchId);
        Dictionary<int, string> GetInstrumentTypeList();
        Dictionary<int, string> GetSourceProfileByCompany(int companyId);
        Dictionary<int, string> GetRegionList();
        Dictionary<int, string> GetUserRoleList();
        Dictionary<int, string> GetDeviceFamilyList();
        Dictionary<int, string> GetLicenseTypeList();
        Dictionary<int, string> GetCompaniesByRegion(int regionId);
    }
}
