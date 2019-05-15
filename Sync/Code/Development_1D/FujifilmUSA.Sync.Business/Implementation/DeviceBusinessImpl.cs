using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Business.CustomExceptions;
using System.Xml.Linq;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class DeviceBusinessImpl : IDeviceBusiness
    {
        #region IDeviceBusiness Members

        public List<Device> GetDeviceListById(int companyId, int branchId, int currentUserId)
        {
            var objDeviceList = new List<Device>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsDeviceList = DataAccessFactory.Instance.GetDsResult("[CP].[GetDeviceList]",
                                                                              new object[]
                                                                                      {
                                                                                          companyId, branchId,
                                                                                          currentUserId
                                                                                      });
                    if (dsDeviceList != null && dsDeviceList.Tables.Count == 2 && dsDeviceList.Tables[0].Rows.Count > 0)
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
                                },
                                BranchName = DataReader.ReadString(x, "BranchName")
                            }).ToList();
                        if (dsDeviceList.Tables[1].Rows.Count > 0)
                        {
                            var licenseDetails = dsDeviceList.Tables[1].
                                AsEnumerable().
                                Select(x => new
                                {
                                    DeviceId = DataReader.ReadInt(x, "DeviceID"),
                                    licenseTypeId = DataReader.ReadShort(x, "LicenseTypeID"),
                                    NumOfProject = DataReader.ReadInt(x, "NumOfProject")
                                }).ToList();
                            objDeviceList.ForEach(x =>
                                                      {
                                                          if (licenseDetails.Where(y => y.DeviceId == x.DeviceId).Count() > 0)
                                                          {
                                                              licenseDetails.Where(y => y.DeviceId == x.DeviceId).ToList().
                                                                  ForEach(z =>
                                                                          x.LicenseTypeProject.Add(
                                                                              (ELicenseType)System.Enum.Parse(typeof(ELicenseType), z.licenseTypeId.ToString()),
                                                                              z.NumOfProject));
                                                          }
                                                      });
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetDeviceListById"));
            return objDeviceList;
        }

        public List<Device> GetDevice(int deviceId, int currentUserId)
        {
            var objDeviceList = new List<Device>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsDeviceList = DataAccessFactory.Instance.GetDsResult("[CP].[GetDevices]", new object[] { deviceId, currentUserId });
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
                                       IsValidation = DataReader.ReadBoolean(x, "IsValidation"),
                                       IsFlexo = DataReader.ReadBoolean(x, "IsFlexo"),
                                       LicenseId = DataReader.ReadInt(x, "LicenseId"),
                                       DeviceFamily = new DeviceFamily()
                                       {
                                           DeviceFamilyId = DataReader.ReadShort(x, "DeviceFamilyID"),
                                           DeviceFamilyDesc = DataReader.ReadString(x, "DeviceFamilyDesc")
                                       },
                                       License = new License()
                                       {
                                           LicenseId = DataReader.ReadInt(x, "LicenseId"),
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

                        if (dsDeviceList.Tables[1].Rows.Count > 0)
                        {
                            var licenseDetails = dsDeviceList.Tables[1].
                                AsEnumerable().
                                Select(x => new
                                {
                                    DeviceId = DataReader.ReadInt(x, "DeviceID"),
                                    licenseTypeId = DataReader.ReadShort(x, "LicenseTypeID"),
                                    NumOfProject = DataReader.ReadInt(x, "NumOfProject")
                                }).ToList();
                            objDeviceList.ForEach(x =>
                            {
                                if (licenseDetails.Where(y => y.DeviceId == x.DeviceId).Count() > 0)
                                {
                                    licenseDetails.Where(y => y.DeviceId == x.DeviceId).ToList().
                                        ForEach(z =>
                                                x.LicenseTypeProject.Add(
                                                    (ELicenseType)System.Enum.Parse(typeof(ELicenseType), z.licenseTypeId.ToString()),
                                                    z.NumOfProject));
                                }
                            });
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetDevice"));
            return objDeviceList;
        }

        public int InsertDevice(Device device)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                    {
                        var licenseDetails = new XElement("LicenseDetails",
                                                          device.LicenseTypeProject.
                                                              Select(x =>
                                                                     new XElement("LicenseDetail",
                                                                                  new XAttribute("LicenseTypeID", System.Convert.ToInt32(x.Key)),
                                                                                  new XAttribute("NumOfProject", x.Value)
                                                                         )).ToList());

                        returnValue = DataAccessFactory.
                            Instance.
                            ExecuteProcedure(
                                "[CP].[InsertDevice]",
                                new object[]
                                    {
                                        device.LicenseId,
                                        device.DeviceName,
                                        device.DeviceFamily.DeviceFamilyId,
                                        device.IsActive,
                                        device.IsValidation,
                                        licenseDetails.ToString(),
                                        device.IsFlexo
                                    });
                        if (returnValue == -1)
                        {
                            throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertDevice" };
                        }
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
                        var licenseDetails = new XElement("LicenseDetails",
                                                          device.LicenseTypeProject.
                                                              Select(x =>
                                                                     new XElement("LicenseDetail",
                                                                                  new XAttribute("LicenseTypeID", System.Convert.ToInt32(x.Key)),
                                                                                  new XAttribute("NumOfProject", x.Value)
                                                                         )).ToList());

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
                                        device.IsActive,
                                        device.IsValidation,
                                        licenseDetails.ToString(),
                                        device.IsFlexo
                                    });
                        if (returnValue == -1)
                        {
                            throw new DuplicateException() { UserFriendlyMessage = "Duplicate_UpdateDevice" };
                        }
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
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertDeviceFamily" };
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertDeviceFamily"));
            return returnValue;
        }

        public int InsertDeviceFamilyProfilerOption(DeviceFamilyProfilerOption deviceFamilyProfilerOption)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    var profilerOptionPresets = new XElement("ProfilerOptionPresetData",
                                                       deviceFamilyProfilerOption.ProfilerOptionPresets.
                                                             Select(x =>
                                                                    new XElement("ProfilerOptionPreset",
                                                                                 new XAttribute("ProfileTypeID", x.ProfileTypeId),
                                                                                 new XAttribute("ProfilerOptionID", x.ProfilerOption.ProfilerOptionId),
                                                                                 new XAttribute("ProfilerOptionValue", x.ProfilerOptionValue)
                                                                        )).ToList());

                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[InsertDeviceFamilyProfilerOption]",
                            new object[]
                                    {
                                        deviceFamilyProfilerOption.DeviceFamilyId,
                                        deviceFamilyProfilerOption.ProfilerOptionsName,
                                        deviceFamilyProfilerOption.ProfilerOptionsDesc,
                                        deviceFamilyProfilerOption.ProjectTypeID,
                                        profilerOptionPresets.ToString()

                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertDeviceFamilyProfilerOption" };
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertDeviceFamilyProfilerOption"));
            return returnValue;
        }
  
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
