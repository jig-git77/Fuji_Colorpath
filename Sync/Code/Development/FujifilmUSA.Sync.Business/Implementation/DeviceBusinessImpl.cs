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

        public List<Device> GetDeviceListById(int companyId, int branchId)
        {
            var objDeviceList = new List<Device>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsDeviceList = DataAccessFactory.Instance.GetDsResult("[CP].[GetDeviceList]", new object[] { companyId, branchId });
                    if (dsDeviceList != null && dsDeviceList.Tables.Count > 0 && dsDeviceList.Tables[0].Rows.Count > 0)
                    {
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
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetDeviceListById"));
            return objDeviceList;
        }

        public List<Device> GetDeviceList()
        {
            var objDeviceList = new List<Device>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsDeviceList = DataAccessFactory.Instance.GetDsResult("[CP].[GetDevices]");
                    if (dsDeviceList != null && dsDeviceList.Tables.Count > 0 && dsDeviceList.Tables[0].Rows.Count > 0)
                    {
                        objDeviceList = dsDeviceList.Tables[0].AsEnumerable().
                            Select(x =>
                                   new Device()
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
                                                             LicenseId = DataReader.ReadInt(x, "LicenseId"),
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
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetDeviceList"));
            return objDeviceList;
        }

        public int InsertDevice(Device device)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[InsertDevice]",
                            new object[]
                                    {
                                        device.LicenseId,
                                        device.DeviceName,
                                        device.DeviceFamily.DeviceFamilyId,
                                        device.IsActive
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertDevice"));
            return returnValue;
        }

        public int UpdateDevice(Device device)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[UpdateDevice]",
                            new object[]
                                    {
                                        device.DeviceId,
                                        device.LicenseId,
                                        device.DeviceName,
                                        device.DeviceFamily.DeviceFamilyId,
                                        device.IsActive
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateDevice"));
            return returnValue;
        }

        public bool DeleteDevice(int deviceId)
        {
            Common.ImplementBusinessLogic(
                () => DataAccessFactory.
                          Instance.
                          ExecuteProcedure(
                              "[CP].[DeleteDevice]",
                              new object[]
                                  {
                                      deviceId
                                  }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteDevice"));
            return true;
        }

        public int InsertDeviceFamily(DeviceFamily deviceFamily)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[InsertDeviceFamily]",
                            new object[]
                                    {
                                        deviceFamily.DeviceFamilyDesc,
                                        deviceFamily.IsActive,
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertDeviceFamily"));
            return returnValue;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
