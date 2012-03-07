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
            throw new System.NotImplementedException();
        }

        public Dictionary<string, string> GetMeasurementDeviceListBySyncDeviceId(int syncDeviceId)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<int, string> GetDongleList(int branchId)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<int, string> GetMeasurementDeviceTypeList()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId(int measurementDeviceId)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<int, string> GetSourceProfileList()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<int, string> GetProfilerOption()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion


       
    }
}
