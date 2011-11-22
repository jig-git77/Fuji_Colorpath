using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System.Data;
using FujifilmUSA.Sync.DataAccess;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class SyncDeviceBusinessImpl : ISyncDeviceBusiness
    {
        #region ISyncDeviceBusiness Members

        public List<SyncDevice> GetSyncDeviceListByUserID(int TaskeroUserID, int RoleID, int CompanyID, int BranchID)
        {
            try
            {
                List<SyncDevice> ObjSyncDeviceList = new List<SyncDevice>();
                //========== Get data from data access=================//
                DataSet DSSyncDeviceList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetSyncDeviceListByUserID]", new object[] { TaskeroUserID, RoleID, CompanyID, BranchID });

                if (DSSyncDeviceList != null && DSSyncDeviceList.Tables.Count > 0 && DSSyncDeviceList.Tables[0].Rows.Count > 0)
                {
                    ObjSyncDeviceList = DSSyncDeviceList.Tables[0].AsEnumerable().
                    Select(x => new SyncDevice()
                    {
                        CPDongleID = int.Parse(x["CPDongleID"].ToString()),
                        CPSerialNUM = x["CPSerialNUM"].ToString(),
                        DeviceManufacturer = x["DeviceManufacturer"].ToString(),
                        DeviceName = x["DeviceName"].ToString(),
                        DeviceTypeID = int.Parse(x["DeviceTypeID"].ToString()),
                        NumOfInkUnits = int.Parse(x["NumOfInkUnits"].ToString()),
                        SyncDeviceID = int.Parse(x["SyncDeviceID"].ToString())
                    }).ToList();
                }
                return ObjSyncDeviceList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
