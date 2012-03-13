using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class DeviceBusinessImpl : IDeviceBusiness
    {
        #region IDeviceBusiness Members

        public List<Device> DeviceListById(int companyId, int branchId)
        {
            var objDeviceList = new List<Device>();
            var dsDeviceList = DataAccessFactory.Instance.GetDsResult("[uspGetDeviceList]", new object[] { companyId, branchId });
            if (dsDeviceList != null && dsDeviceList.Tables.Count > 0 && dsDeviceList.Tables[0].Rows.Count > 0)
            {
                DeviceFamily objDeviceFamily = new DeviceFamily();

                objDeviceList = dsDeviceList.Tables[0].AsEnumerable().
                Select(x => new Device()
                    {
                        DeviceId = DataReader.ReadInt(x, "DeviceID"),
                        DeviceName = DataReader.ReadString(x, "DeviceName"),
                        DeviceFamily = new DeviceFamily()
                        {
                            DeviceFamilyId = DataReader.ReadShort(x, "DeviceFamilyID"),
                            DeviceFamilyDesc = DataReader.ReadString(x, "DeviceFamilyDesc")
                        }
                    }).ToList();
            }
            return objDeviceList;
        }

        #endregion
    }
}
