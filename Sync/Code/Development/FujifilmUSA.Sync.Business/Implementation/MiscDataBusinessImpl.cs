using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;
using System.Data;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class MiscDataBusinessImpl : IMiscDataBusiness
    {
        #region IMiscDataBusiness Members

        public Dictionary<int, string> GetBranchList(int companyId)
        {
            var dstBranchList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsBranchList = DataAccessFactory.
                        Instance.
                        GetDsResult(
                            "[CP].[GetCompany]",
                            new object[]
                                    {
                                        companyId
                                    });
                    if (dsBranchList != null && dsBranchList.Tables.Count > 0)
                    {
                        dstBranchList = (dsBranchList.Tables[0].AsEnumerable().Select(
                            branch => new
                                          {
                                              Value = DataReader.ReadInt(branch, "BranchID"),
                                              Text = DataReader.ReadString(branch, "BranchName")
                                          })).ToDictionary(branch => branch.Value, branch => branch.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetBranchList"));
            return dstBranchList;
        }

        public Dictionary<int, string> GetCompanyList()
        {
            var dstCompanyList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsCompanyList = DataAccessFactory.Instance.GetDsResult("[CP].[GetCompany]", new object[] { 0 });
                    if (dsCompanyList != null && dsCompanyList.Tables.Count > 0)
                    {

                        dstCompanyList = (dsCompanyList.Tables[0].AsEnumerable().Select(
                            company => new
                                           {
                                               Value = DataReader.ReadInt(company, "CompanyID"),
                                               Text = DataReader.ReadString(company, "CompanyName")
                                           })).ToDictionary(company => company.Value, company => company.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetCompanyList"));
            return dstCompanyList;
        }

        public Dictionary<int, string> GetSourceProfileByCompany(int companyId)
        {
            var dsSourceProfileList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsCompanyList = DataAccessFactory.Instance.GetDsResult("[CP].[GetSourceProfileByCompany]", new object[] { companyId });
                    if (dsCompanyList != null && dsCompanyList.Tables.Count > 0)
                    {
                        dsSourceProfileList = (dsCompanyList.Tables[0].AsEnumerable().Select(
                            company => new
                                           {
                                               Value = DataReader.ReadInt(company, "SourceProfileID"),
                                               Text = DataReader.ReadString(company, "ProfileName")
                                           })).ToDictionary(company => company.Value, company => company.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSourceProfileByCompany"));
            return dsSourceProfileList;
        }

        public Dictionary<int, string> GetRegionList()
        {
            var dstRegionList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsRegion = DataAccessFactory.Instance.GetDsResult("[CP].[GetRegion]");
                    if (dsRegion != null && dsRegion.Tables.Count > 0)
                    {
                        dstRegionList = (dsRegion.Tables[0].AsEnumerable().Select(
                            region => new
                                          {
                                              Value = DataReader.ReadInt(region, "RegionID"),
                                              Text = DataReader.ReadString(region, "Region")
                                          })).ToDictionary(region => region.Value, region => region.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetRegionList"));
            return dstRegionList;
        }
        public Dictionary<int, string> GetRegionListByCompany(int companyId)
        {
            var dstRegionList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsRegion = DataAccessFactory.Instance.GetDsResult("[CP].[GetRegionByCompany]", new object[] { companyId });
                    if (dsRegion != null && dsRegion.Tables.Count > 0)
                    {
                        dstRegionList = (dsRegion.Tables[0].AsEnumerable().Select(
                            region => new
                                          {
                                              Value = DataReader.ReadInt(region, "RegionID"),
                                              Text = DataReader.ReadString(region, "Region")
                                          })).ToDictionary(region => region.Value, region => region.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetRegionListByCompany"));
            return dstRegionList;
        }

        public Dictionary<int, string> GetUserRoleList()
        {
            var dstUserRoleList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsUserRole = DataAccessFactory.Instance.GetDsResult("[CP].[GetRoles]");
                    if (dsUserRole != null && dsUserRole.Tables.Count > 0)
                    {
                        dstUserRoleList = (dsUserRole.Tables[0].AsEnumerable().Select(
                            region => new
                                          {
                                              Value = DataReader.ReadInt(region, "RoleID"),
                                              Text = DataReader.ReadString(region, "RoleDesc")
                                          })).ToDictionary(region => region.Value, region => region.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetUserRoleList"));
            return dstUserRoleList;
        }

        public Dictionary<int, string> GetLicenseList(int branchId)
        {
            var dstLicenseList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsLicenseList = DataAccessFactory.Instance.GetDsResult("[CP].[GetLicenses]",
                                                                               new object[] { branchId });
                    if (dsLicenseList != null && dsLicenseList.Tables.Count > 0)
                    {
                        dstLicenseList = (dsLicenseList.Tables[0].AsEnumerable().Select(
                            license => new
                                           {
                                               Value = DataReader.ReadInt(license, "LicenseID"),
                                               Text = DataReader.ReadString(license, "LicenseDesc")
                                           })).ToDictionary(license => license.Value, license => license.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetLicenseList"));
            return dstLicenseList;
        }

        public Dictionary<int, string> GetDeviceFamilyList()
        {
            var dstDeviceFamilyList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsDeviceFamilyList = DataAccessFactory.Instance.GetDsResult("[CP].[GetDeviceFamilys]");
                    if (dsDeviceFamilyList != null && dsDeviceFamilyList.Tables.Count > 0)
                    {
                        dstDeviceFamilyList = (dsDeviceFamilyList.Tables[0].AsEnumerable().Select(
                            license => new
                                           {
                                               Value = DataReader.ReadInt(license, "DeviceFamilyID"),
                                               Text = DataReader.ReadString(license, "DeviceFamilyDesc")
                                           })).ToDictionary(license => license.Value, license => license.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetDeviceFamilyList"));
            return dstDeviceFamilyList;
        }

        public Dictionary<int, string> GetInstrumentTypeList()
        {
            var dstInstrumentTypeList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsInstrumentTypeList = DataAccessFactory.Instance.GetDsResult("[CP].[GetInstrumentType]", new object[] { });
                    if (dsInstrumentTypeList != null && dsInstrumentTypeList.Tables.Count > 0)
                    {

                        dstInstrumentTypeList = (dsInstrumentTypeList.Tables[0].AsEnumerable().Select(
                            instrumentType => new
                                                  {
                                                      Value = DataReader.ReadInt(instrumentType, "InstrumentTypeID"),
                                                      Text = DataReader.ReadString(instrumentType, "InstrumentType")
                                                  })).
                            ToDictionary(instrumentType => instrumentType.Value, instrumentType => instrumentType.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetInstrumentTypeList"));
            return dstInstrumentTypeList;
        }

        public Dictionary<int, string> GetLicenseTypeList()
        {
            var licenseTypeList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsLicenseTypeList = DataAccessFactory.Instance.GetDsResult("[CP].[GetLicenseType]");
                    if (dsLicenseTypeList != null && dsLicenseTypeList.Tables.Count > 0)
                    {
                        licenseTypeList = (dsLicenseTypeList.Tables[0].AsEnumerable().Select(
                            licensetype => new
                                               {
                                                   Value = DataReader.ReadInt(licensetype, "LicenseTypeID"),
                                                   Text = DataReader.ReadString(licensetype, "LicenseType")
                                               })).ToDictionary(licensetype => licensetype.Value, licensetype => licensetype.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetLicenseTypeList"));
            return licenseTypeList;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}