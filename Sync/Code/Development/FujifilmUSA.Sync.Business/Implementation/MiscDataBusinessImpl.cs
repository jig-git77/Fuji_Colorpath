using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var dsBranchList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetCompanyList]",
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

        public Dictionary<int, string> GetCompanyList()
        {
            var dstCompanyList = new Dictionary<int, string>();
            var dsCompanyList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetCompanyList]", new object[] { 0 });
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

        public Dictionary<int, string> GetDestinationProfiles(int syncDeviceId)
        {
            var dstDestinationProfileList = new Dictionary<int, string>();
            var dsDestinationProfileList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetDestinationProfiles]", new object[] { syncDeviceId });
            if (dsDestinationProfileList != null && dsDestinationProfileList.Tables.Count > 0)
            {
                dstDestinationProfileList = (dsDestinationProfileList.Tables[0].AsEnumerable().Select(
                    destinationProfile => new
                    {
                        Value = DataReader.ReadInt(destinationProfile, "DestinationProfileID"),
                        Text = DataReader.ReadString(destinationProfile, "DestinationProfileName")
                    })).ToDictionary(destinationProfile => destinationProfile.Value, destinationProfile => destinationProfile.Text);
            }
            return dstDestinationProfileList;
        }

        public Dictionary<string, string> GetMeasurementDeviceListBySyncDeviceId(int syncDeviceId)
        {
            var dstMeasurementDeviceList = new Dictionary<string, string>();
            //========== Get data from data access=================//
            var dsMeasurementDeviceList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetMeasurementDeviceBySyncDeviceID]", new object[] { syncDeviceId });


            if (dsMeasurementDeviceList != null && dsMeasurementDeviceList.Tables.Count > 0)
            {
                dstMeasurementDeviceList = (dsMeasurementDeviceList.Tables[0].AsEnumerable().
                    Select(measurementDevice => new
                    {
                        Value =
                        DataReader.ReadInt(measurementDevice, "MeasurementDeviceTypeID") +
                        " - " +
                        DataReader.ReadInt(measurementDevice, "MeasurementDeviceID"),
                        Text =
                        DataReader.ReadString(measurementDevice, "MeasurementDeviceTypeName") + " - " + DataReader.ReadString(measurementDevice, "SerialNumber")
                    })).ToDictionary(measurementDevice => measurementDevice.Value, measurementDevice => measurementDevice.Text);

            }
            return dstMeasurementDeviceList;
        }

        public Dictionary<int, string> GetDongleList(int branchId)
        {
            var dongleList = new Dictionary<int, string>();
            var dsDongleList = DataAccessFactory.Instance.GetDsResult("[CPV].[uspGetDongleListByBranchID]", new object[] { 
                branchId
            });
            if (dsDongleList != null && dsDongleList.Tables.Count > 0 && dsDongleList.Tables[0].Rows.Count > 0)
            {
                dongleList = dsDongleList.Tables[0].AsEnumerable().ToDictionary(x => DataReader.ReadInt(x, "CPDongleID"), x => DataReader.ReadString(x, "DongleDescription"));
            }
            return dongleList;
        }

        public Dictionary<int, string> GetMeasurementDeviceTypeList()
        {
            var measurementDeviceTypeList = new Dictionary<int, string>();
            var dsMeasurementDeviceTypeList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetSyncMeasurementDeviceType]");
            if (dsMeasurementDeviceTypeList != null && dsMeasurementDeviceTypeList.Tables.Count > 0 && dsMeasurementDeviceTypeList.Tables[0].Rows.Count > 0)
            {
                measurementDeviceTypeList = dsMeasurementDeviceTypeList.Tables[0].AsEnumerable().ToDictionary(x => DataReader.ReadInt(x, "MeasurementDeviceTypeID"), x => DataReader.ReadString(x, "MeasurementDeviceTypeName"));
            }
            return measurementDeviceTypeList;
        }

        public Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId(int measurementDeviceId)
        {
            var dstReferenceChartTypeList = new Dictionary<int, string>();
            var dsReferenceChartTypeList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetReferenceChartTypeByMeasurementDeviceID]", new object[] { measurementDeviceId });

            if (dsReferenceChartTypeList != null && dsReferenceChartTypeList.Tables.Count > 0)
            {
                dstReferenceChartTypeList = (dsReferenceChartTypeList.Tables[0].AsEnumerable().
                    Select(referenceChartType => new
                    {
                        Value = DataReader.ReadInt(referenceChartType, "ReferenceChartTypeID"),
                        Text = DataReader.ReadString(referenceChartType, "ChartName")
                    })).ToDictionary(referenceChartType => referenceChartType.Value, referenceChartType => referenceChartType.Text);

            }
            return dstReferenceChartTypeList;
        }

        public Dictionary<int, string> GetSourceProfileList()
        {
            var dstSourceProfileList = new Dictionary<int, string>();
            var dsSourceProfileList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetSourceProfileList]");
            if (dsSourceProfileList != null && dsSourceProfileList.Tables.Count > 0)
            {
                dstSourceProfileList = (from sourceProfile in dsSourceProfileList.Tables[0].AsEnumerable()
                                        select new
                                        {
                                            Value = DataReader.ReadInt(sourceProfile, "SourceProfileID"),
                                            Text = DataReader.ReadString(sourceProfile, "SourceProfileName"),
                                        }).ToDictionary(sourceProfile => sourceProfile.Value, sourceProfile => sourceProfile.Text);
            }
            return dstSourceProfileList;
        }

        #endregion
    }
}
