using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class SyncDeviceBusinessImpl : ISyncDeviceBusiness
    {
        #region ISyncDeviceBusiness Members

        public List<Device> GetSyncDevicesByUserId(int taskeroUserId, int roleId, int companyId, int branchId)
        {
            var objSyncDeviceList = new List<Device>();
            var dsSyncDeviceList = DataAccessFactory.Instance.GetDsResult("[uspGetSyncDeviceList]", new object[] { companyId, branchId });
            if (dsSyncDeviceList != null && dsSyncDeviceList.Tables.Count > 0 && dsSyncDeviceList.Tables[0].Rows.Count > 0)
            {
                objSyncDeviceList = dsSyncDeviceList.Tables[0].AsEnumerable().
                    Select(x => new Device()
                    {
                        //CpDongleId = DataReader.ReadInt(x, "CPDongleID"),
                        //CpSerialNum = DataReader.ReadString(x, "CPSerialNUM"),
                        //DeviceManufacturer = DataReader.ReadString(x, "DeviceManufacturer"),
                        DeviceId = DataReader.ReadInt(x, "DeviceID"),
                        DeviceName = DataReader.ReadString(x, "DeviceName"),
                        //DeviceTypeId = DataReader.ReadInt(x, "DeviceTypeID"),
                        //NumOfInkUnits = DataReader.ReadInt(x, "NumOfInkUnits"),
                        //SyncDeviceId = DataReader.ReadInt(x, "SyncDeviceID")
                    }).ToList();
            }
            return objSyncDeviceList;
        }

        #endregion
    }
}
