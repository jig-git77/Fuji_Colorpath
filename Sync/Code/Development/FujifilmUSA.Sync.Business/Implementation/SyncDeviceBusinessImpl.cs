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
            var dsSyncDeviceList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetSyncDeviceListByUserID]", new object[] { taskeroUserId, roleId, companyId, branchId });

            if (dsSyncDeviceList != null && dsSyncDeviceList.Tables.Count > 0 && dsSyncDeviceList.Tables[0].Rows.Count > 0)
            {
                objSyncDeviceList = dsSyncDeviceList.Tables[0].AsEnumerable().
                    Select(x => new SyncDevice()
                                    {
                                        CpDongleId = DataReader.ReadInt(x,"CPDongleID"),
                                        CpSerialNum = DataReader.ReadString(x,"CPSerialNUM"),
                                        DeviceManufacturer = DataReader.ReadString(x,"DeviceManufacturer"),
                                        DeviceName = DataReader.ReadString(x,"DeviceName"),
                                        DeviceTypeId = DataReader.ReadInt(x, "DeviceTypeID"),
                                        NumOfInkUnits = DataReader.ReadInt(x,"NumOfInkUnits"),
                                        SyncDeviceId = DataReader.ReadInt(x,"SyncDeviceID")
                                    }).ToList();
            }
            return objSyncDeviceList;
        }

        #endregion
    }
}
