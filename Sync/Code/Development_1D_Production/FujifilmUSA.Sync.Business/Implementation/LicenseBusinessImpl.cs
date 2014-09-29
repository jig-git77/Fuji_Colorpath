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
    public class LicenseBusinessImpl : ILicenseBusiness
    {
        #region IDeviceBusiness Members

        public List<License> GetLicense(int licensesId, int currentUserId)
        {
            var objLicenseList = new List<License>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsLicenseList = DataAccessFactory.Instance.GetDsResult("[CP].[GetLicense]", new object[] { licensesId, currentUserId });
                    if (dsLicenseList != null && dsLicenseList.Tables.Count == 2 && dsLicenseList.Tables[0].Rows.Count > 0)
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
                                           IsActive = DataReader.ReadBoolean(x, "IsActive"),
                                           Branch = new Branch()
                                                        {
                                                            BranchId = DataReader.ReadInt(x, "BranchId"),
                                                            BranchName = DataReader.ReadString(x, "BranchName"),
                                                            CompanyId = DataReader.ReadInt(x, "CompanyId")
                                                        }
                                       }).ToList();

                        if (dsLicenseList.Tables[1].Rows.Count > 0)
                        {
                            var licenseDetail = dsLicenseList.Tables[1].AsEnumerable().
                                Select(x => new
                                                {
                                                    LicenseID = DataReader.ReadInt(x, "LicenseID"),
                                                    LicenseTypeID = DataReader.ReadShort(x, "LicenseTypeID"),
                                                }).ToList();

                            objLicenseList.
                                ForEach(lic =>
                                            {
                                                if (licenseDetail.Where(x => x.LicenseID == lic.LicenseId).Count() > 0)
                                                {
                                                    licenseDetail.Where(x => x.LicenseID == lic.LicenseId).ToList().
                                                        ForEach(
                                                            type =>
                                                            lic.LicenseType.Add((ELicenseType)System.Enum.Parse(typeof(ELicenseType), type.LicenseTypeID.ToString())));
                                                }
                                            });
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetLicense"));
            return objLicenseList;
        }

        public int InsertLicense(License license)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    var licenseDetails = new XElement("LicenseDetails",
                                                      license.LicenseType.
                                                          Select(x =>
                                                                 new XElement("LicenseDetail",
                                                                              new XAttribute("LicenseTypeID", System.Convert.ToInt32(x))
                                                                     )).ToList());

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
                                        , license.IsActive
                                        , licenseDetails.ToString()
                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertLicense" };
                    }
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
                    var licenseDetails = new XElement("LicenseDetails",
                                                      license.LicenseType.
                                                          Select(x =>
                                                                 new XElement("LicenseDetail",
                                                                              new XAttribute("LicenseTypeID", System.Convert.ToInt32(x))
                                                                     )).ToList());

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
                                        , license.IsActive
                                        , licenseDetails.ToString()
                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_UpdateLicense" };
                    }
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
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertLicenseType" };
                    }
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
