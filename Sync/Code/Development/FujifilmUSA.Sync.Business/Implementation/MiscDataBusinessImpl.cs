using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;
using System.Data;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class MiscDataBusinessImpl : IMiscDataBusiness
    {
        #region IMiscDataBusiness Members

        public Dictionary<int, string> GetBranchList(int companyId,int currentUserId)
        {
            var dstBranchList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsBranchList = DataAccessFactory.
                        Instance.
                        GetDsResult(
                            "[CP].[GetBranchByCompany]",
                            new object[]
                                    {
                                        companyId,
                                        currentUserId
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

        public Dictionary<int, string> GetCompanyList(int currentUserId)
        {
            var dstCompanyList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsCompanyList = DataAccessFactory.Instance.GetDsResult("[CP].[GetCompany]", new object[] { currentUserId });
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

        public Dictionary<int, string> GetRegionList(int currentUserId)
        {
            var dstRegionList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsRegion = DataAccessFactory.Instance.GetDsResult("[CP].[GetRegion]", new object[] { currentUserId });
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

        public Dictionary<int, string> GetCompaniesByRegion(int regionId,int currentUserId)
        {
            var dstCompnayList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsCompnay = DataAccessFactory.Instance.GetDsResult("[CP].[GetCompanyByRegion]", new object[] { regionId, currentUserId });
                    if (dsCompnay != null && dsCompnay.Tables.Count > 0)
                    {
                        dstCompnayList = (dsCompnay.Tables[0].AsEnumerable().Select(
                            region => new
                            {
                                Value = DataReader.ReadInt(region, "CompanyID"),
                                Text = DataReader.ReadString(region, "CompanyName")
                            })).ToDictionary(region => region.Value, region => region.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetRegionListByCompany"));
            return dstCompnayList;
        }

        public List<Region> GetAllRegions()
        {
            var regions = new List<Region>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsRegion = DataAccessFactory.Instance.GetDsResult("[CP].[GetAllRegions]");
                    if (dsRegion != null && dsRegion.Tables.Count > 0)
                    {
                        regions = dsRegion.Tables[0].AsEnumerable().Select(
                            region => new Region()
                                          {
                                              RegionId = DataReader.ReadShort(region, "RegionID"),
                                              RegionName = DataReader.ReadString(region, "Region")
                                          }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetAllRegions"));
            return regions;
        }

        public Dictionary<int, string> GetUserRoleList(int currentUserId)
        {
            var dstUserRoleList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsUserRole = DataAccessFactory.Instance.GetDsResult("[CP].[GetRoles]", new object[] { currentUserId });
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