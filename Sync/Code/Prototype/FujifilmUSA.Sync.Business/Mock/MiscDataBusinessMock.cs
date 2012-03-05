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

        public Dictionary<int, string> GetDestinationProfiles(int syncDeviceId)
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<string, string> GetMeasurementDeviceListBySyncDeviceId(int syncDeviceId)
        {
            return new Dictionary<string, string>();
        }

        public Dictionary<int, string> GetDongleList(int branchId)
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<int, string> GetMeasurementDeviceTypeList()
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId(int measurementDeviceId)
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<int, string> GetSourceProfileList()
        {
            return new Dictionary<int, string>();
        }

        public Dictionary<int, string> GetProfilerOption()
        {
            return new Dictionary<int, string>();
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion


    }
}
