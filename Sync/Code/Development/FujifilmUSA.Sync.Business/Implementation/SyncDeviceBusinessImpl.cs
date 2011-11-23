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

        public List<SyncDevice> GetSyncDeviceListByUserId(int taskeroUserId, int roleId, int companyId, int branchId)
        {
            var objSyncDeviceList = new List<SyncDevice>();
            //========== Get data from data access=================//
            var dsSyncDeviceList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetSyncDeviceListByUserID]", new object[] { taskeroUserId, roleId, companyId, branchId });

            if (dsSyncDeviceList != null && dsSyncDeviceList.Tables.Count > 0 && dsSyncDeviceList.Tables[0].Rows.Count > 0)
            {
                objSyncDeviceList = dsSyncDeviceList.Tables[0].AsEnumerable().
                    Select(x => new SyncDevice()
                                    {
                                        CpDongleId = DsReader.ReadInt(x,"CPDongleID"),
                                        CpSerialNum = DsReader.ReadString(x,"CPSerialNUM"),
                                        DeviceManufacturer = DsReader.ReadString(x,"DeviceManufacturer"),
                                        DeviceName = DsReader.ReadString(x,"DeviceName"),
                                        DeviceTypeId = DsReader.ReadInt(x, "DeviceTypeID"),
                                        NumOfInkUnits = DsReader.ReadInt(x,"NumOfInkUnits"),
                                        SyncDeviceId = DsReader.ReadInt(x,"SyncDeviceID")
                                    }).ToList();
            }
            return objSyncDeviceList;
        }

        #endregion
    }
}
