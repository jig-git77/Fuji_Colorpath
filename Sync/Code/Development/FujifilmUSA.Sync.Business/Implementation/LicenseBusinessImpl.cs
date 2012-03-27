using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class LicenseBusinessImpl : ILicenseBusiness
    {
        #region IDeviceBusiness Members

        public List<License> GetLicenses()
        {
            var objLicenseList = new List<License>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsLicenseList = DataAccessFactory.Instance.GetDsResult("[CP].[GetLicense]");
                    if (dsLicenseList != null && dsLicenseList.Tables.Count > 0 && dsLicenseList.Tables[0].Rows.Count > 0)
                    {
                        objLicenseList = dsLicenseList.Tables[0].AsEnumerable().
                            Select(x =>
                                   new License()
                                       {
                                           LicenseId = DataReader.ReadInt(x, "LicenseID"),
                                           BranchId = DataReader.ReadInt(x, "BranchID"),
                                           ActivationDate = DataReader.ReadDate(x, "ActivationDate"),
                                           ExpirationDate = DataReader.ReadDate(x, "ExpirationDate"),
                                           NumOfDevice = DataReader.ReadShort(x, "NumOfDevice"),
                                           LicenseDesc = DataReader.ReadString(x, "LicenseDesc"),
                                           LicenseTypeId = DataReader.ReadShort(x, "LicenseTypeID"),
                                           IsActive = DataReader.ReadBoolean(x, "IsActive"),
                                           LicenseType = new LicenseType()
                                                             {
                                                                 LicenseTypeId = DataReader.ReadShort(x, "LicenseTypeID"),
                                                                 Type = DataReader.ReadString(x, "LicenseType")
                                                             },
                                           Branch = new Branch()
                                                        {
                                                            BranchId = DataReader.ReadInt(x, "BranchId"),
                                                            BranchName = DataReader.ReadString(x, "BranchName"),
                                                            CompanyId = DataReader.ReadInt(x, "CompanyId")
                                                        }
                                       }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetLicenses"));
            return objLicenseList;
        }

        public int InsertLicense(License license)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[InsertLicense]",
                            new object[]
                                    {
                                        license.Branch.BranchId
                                        , license.ActivationDate
                                        , license.ExpirationDate
                                        , license.NumOfDevice
                                        , license.LicenseDesc
                                        , license.LicenseTypeId
                                        , license.IsActive
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertLicense"));
            return returnValue;
        }

        public int UpdateLicense(License license)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[UpdateLicense]",
                            new object[]
                                    {
                                        license.LicenseId
                                        , license.Branch.BranchId
                                        , license.ActivationDate
                                        , license.ExpirationDate
                                        , license.NumOfDevice
                                        , license.LicenseDesc
                                        , license.LicenseTypeId
                                        , license.IsActive
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateLicense"));
            return returnValue;
        }

        public bool DeleteLicense(int licenseId)
        {
            Common.ImplementBusinessLogic(
                () => DataAccessFactory.
                          Instance.
                          ExecuteProcedure(
                              "[CP].[DeleteLicense]",
                              new object[]
                                  {
                                      licenseId
                                  }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteLicense"));
            return true;
        }

        public int InsertLicenseType(LicenseType licenseType)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[InsertLicenseType]",
                            new object[]
                                    {
                                        licenseType.Type,
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertLicenseType"));
            return returnValue;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
