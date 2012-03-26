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

        public Dictionary<int, string> BranchList(int companyId)
        {
            var dstBranchList = new Dictionary<int, string>();
            var dsBranchList = DataAccessFactory.Instance.GetDsResult("[CP].[GetCompany]",
                                                                    new object[] { companyId });
            if (dsBranchList != null && dsBranchList.Tables.Count > 0)
            {
                dstBranchList = (dsBranchList.Tables[0].AsEnumerable().Select(
                    branch => new
                    {
                        Value = DataReader.ReadInt(branch, "BranchID"),
                        Text = DataReader.ReadString(branch, "BranchName")
                    })).ToDictionary(branch => branch.Value, branch => branch.Text);
            }
            return dstBranchList;
        }

        public Dictionary<int, string> CompanyList()
        {
            var dstCompanyList = new Dictionary<int, string>();
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
            return dstCompanyList;
        }

        public Dictionary<int, string> SourceProfileByCompany(int companyId)
        {
            var dsSourceProfileList = new Dictionary<int, string>();
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
            return dsSourceProfileList;
        }

        public Dictionary<int, string> RegionList()
        {
            var dstRegionList = new Dictionary<int, string>();
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
            return dstRegionList;
        }
        public Dictionary<int, string> RegionListByCompnay(int companyId)
        {
            var dstRegionList = new Dictionary<int, string>();
            var dsRegion = DataAccessFactory.Instance.GetDsResult("[CP].[GetRegionByCompany]", new object[] { companyId } );
            if (dsRegion != null && dsRegion.Tables.Count > 0)
            {
                dstRegionList = (dsRegion.Tables[0].AsEnumerable().Select(
                    region => new
                    {
                        Value = DataReader.ReadInt(region, "RegionID"),
                        Text = DataReader.ReadString(region, "Region")
                    })).ToDictionary(region => region.Value, region => region.Text);
            }
            return dstRegionList;
        }

        public Dictionary<int, string> UserRoleList()
        {
            var dstUserRoleList = new Dictionary<int, string>();
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
            return dstUserRoleList;
        }


        public Dictionary<int, string> LicenseList(int branchId)
        {
            var dstLicenseList = new Dictionary<int, string>();
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
            return dstLicenseList;
        }

        public Dictionary<int, string> DeviceFamilyList()
        {
            var dstDeviceFamilyList = new Dictionary<int, string>();
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
            return dstDeviceFamilyList;
        }

        public Dictionary<int, string> InstrumentTypeList()
        {
            var dstInstrumentTypeList = new Dictionary<int, string>();
            var dsInstrumentTypeList = DataAccessFactory.Instance.GetDsResult("[CP].[InstrumentType]", new object[] {  });
            if (dsInstrumentTypeList != null && dsInstrumentTypeList.Tables.Count > 0)
            {

                dstInstrumentTypeList = (dsInstrumentTypeList.Tables[0].AsEnumerable().Select(
                    InstrumentType => new
                    {
                        Value = DataReader.ReadInt(InstrumentType, "InstrumentTypeID"),
                        Text = DataReader.ReadString(InstrumentType, "InstrumentType")
                    })).ToDictionary(InstrumentType => InstrumentType.Value, InstrumentType => InstrumentType.Text);
            }
            return dstInstrumentTypeList;
        }

        public Dictionary<int, string> LicenseTypeList()
        {
            var licenseTypeList = new Dictionary<int, string>();
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
            return licenseTypeList;
        }

        #endregion
    }
}