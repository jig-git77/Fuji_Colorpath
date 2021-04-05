using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using System.Data;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MiscDataBusinessMock : IMiscDataBusiness
    {
        #region IMiscDataBusiness Members

        public Dictionary<int, string> GetBranchList(int companyId, int currentUserId)
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<int, string> GetCompanyList(int currentUserId)
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

        public Dictionary<int, string> GetRegionList(int currentUserId)
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<int, string> GetInstrumentTypeList()
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<int, string> GetUserRoleList(int currentUserId)
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

        public Dictionary<int, string> GetCompaniesByRegion(int regionId, int currentUserId)
        {
            return new Dictionary<int, string>();
        }

        public List<Region> GetAllRegions()
        {
            return new List<Region>();
        }

        public Dictionary<int, string> GetProjectTypeList()
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<int, string> GetSecurityQuestions()
        {
            return new Dictionary<int, string>();
        }

        public List<Activity> GetPrintActivities(int RegionID)
        {
            return new List<Activity>();
        }

        public Dictionary<int, string> GetTemplate(int templateID)
        {
            return new Dictionary<int, string>();
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion

        #region CP.MulticolorChannel
        public DataSet GetMultiColorChannelList()
        {
            return new DataSet();
        }
        #endregion
    }
}
