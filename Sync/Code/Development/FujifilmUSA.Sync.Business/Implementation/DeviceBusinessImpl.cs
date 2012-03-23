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

        public List<Device> DeviceList()
        {
            var objDeviceList = new List<Device>();
            var dsDeviceList = DataAccessFactory.Instance.GetDsResult("[CP].[uspGetDevices]");
            if (dsDeviceList != null && dsDeviceList.Tables.Count > 0 && dsDeviceList.Tables[0].Rows.Count > 0)
            {
                objDeviceList = dsDeviceList.Tables[0].AsEnumerable().
                                    Select(x => new Device()
                                    {
                                        DeviceId = DataReader.ReadInt(x, "DeviceID"),
                                        DeviceName = DataReader.ReadString(x, "DeviceName"),
                                        DeviceFamilyId = DataReader.ReadByte(x, "DeviceFamilyID"),
                                        IsActive = DataReader.ReadBoolean(x, "IsActive"),
                                        LicenseId = DataReader.ReadInt(x, "LicenseId"),
                                        DeviceFamily = new DeviceFamily()
                                        {
                                            DeviceFamilyId = DataReader.ReadShort(x, "DeviceFamilyID"),
                                            DeviceFamilyDesc = DataReader.ReadString(x, "DeviceFamilyDesc")
                                        },
                                        License = new License() 
                                        {
                                            LicenseId = DataReader.ReadInt(x, "LicenseId") ,
                                            LicenseType = new LicenseType()
                                                              {
                                                                  Type = DataReader.ReadString(x, "LicenseType"),
                                                              },
                                            Branch = new Branch()
                                                         {
                                                             BranchId = DataReader.ReadInt(x, "BranchId"),
                                                             BranchName = DataReader.ReadString(x, "BranchName"),
                                                             Company = new Company()
                                                                           {
                                                                               CompanyId = DataReader.ReadInt(x, "CompanyId"),
                                                                               CompanyName = DataReader.ReadString(x, "CompanyName")
                                                                           }
                                                         }
                                        }
                                    }).ToList();
            }
            return objDeviceList;
        }

        public int InsertDevice(Device device)
        {
            int returnValue = 0;
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspInsertDevice]", new object[]{
                                                                                                                device.LicenseId,
                                                                                                                device.DeviceName,
                                                                                                                device.DeviceFamily.DeviceFamilyId,
                                                                                                                device.IsActive 
                                                                                                });
            return returnValue;
        }

        public int UpdateDevice(Device device)
        {
            int returnValue = 0;
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspUpdateDevice]", new object[]{
                                                                                                                device.DeviceId,     
                                                                                                                device.LicenseId,
                                                                                                                device.DeviceName,
                                                                                                                device.DeviceFamily.DeviceFamilyId,
                                                                                                                device.IsActive 
                                                                                                 });
            return returnValue;
        }

        public bool DeleteDevice(int deviceId)
        {
            DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspDeleteDevice]", new object[]{
             deviceId
            });
            return true;
        }

        public int InsertDeviceFamily(DeviceFamily deviceFamily)
        {
            int returnValue = 0;
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspInsertDeviceFamily]", new object[]{
                                                                                                                deviceFamily.DeviceFamilyDesc,
                                                                                                                deviceFamily.IsActive,
                                                                                                });
            return returnValue;
        }

        #endregion
    }
}
