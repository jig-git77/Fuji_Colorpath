using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MiscDataBusinessMock : IMiscDataBusiness
    {
        #region IMiscDataBusiness Members

        public Dictionary<int, string> GetBranchList(int companyId)
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<int, string> GetCompanyList()
        {
            return new Dictionary<int, string>();
        }
        public Dictionary<int, string> GetSourceProfileByCompany(int companyId)
        {
            return new Dictionary<int, string>();
        }
        public Dictionary<int, string> GetLicenseList(int branchId)
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<int, string> GetRegionList()
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<int, string> GetInstrumentTypeList()
        {
            return new Dictionary<int, string>();
        }


        public  Dictionary<int, string> GetUserRoleList()
        {
            return new Dictionary<int, string>();
        }
        public Dictionary<int, string> GetRegionListByCompany(int companyId)
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<int, string> GetDeviceFamilyList()
        {
            return new Dictionary<int, string>();
        }
        public Dictionary<int, string> GetLicenseTypeList()
        {
            return new Dictionary<int, string>();
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
