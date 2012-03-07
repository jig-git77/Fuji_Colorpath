using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMiscDataBusiness : ITaskBase
    {
        Dictionary<int, string> GetBranchList(int companyId);
        Dictionary<int, string> GetCompanyList();
        Dictionary<int, string> GetDestinationProfiles(int syncDeviceId);
        Dictionary<string, string> GetMeasurementDeviceListBySyncDeviceId(int syncDeviceId);
        Dictionary<int, string> GetDongleList(int branchId);
        Dictionary<int, string> GetMeasurementDeviceTypeList();
        Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId(int measurementDeviceId);
        Dictionary<int, string> GetSourceProfileList();
        Dictionary<int, string> GetProfilerOption();
    }
}
