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
            Common.ImplementBusinessLogic(() =>
            {
                var dsBranchList = DataAccessFactory.Instance.GetDsResult("[uspGetCompany]",
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
            }, this, ErrorOccured);
            return dstBranchList;
        }

        public Dictionary<int, string> GetCompanyList()
        {
            var dstCompanyList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(() =>
            {
                var dsCompanyList = DataAccessFactory.Instance.GetDsResult("[uspGetCompany]", new object[] { 0 });
                if (dsCompanyList != null && dsCompanyList.Tables.Count > 0)
                {

                    dstCompanyList = (dsCompanyList.Tables[0].AsEnumerable().Select(
                        company => new
                        {
                            Value = DataReader.ReadInt(company, "CompanyID"),
                            Text = DataReader.ReadString(company, "CompanyName")
                        })).ToDictionary(company => company.Value, company => company.Text);
                }
            }, this, ErrorOccured);
            return dstCompanyList;
        }
        
        public Dictionary<int, string> GetDestinationProfiles(int syncDeviceId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetMeasurementDeviceListBySyncDeviceId(int syncDeviceId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, string> GetDongleList(int branchId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, string> GetMeasurementDeviceTypeList()
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId(int measurementDeviceId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, string> GetSourceProfileList()
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, string> GetProfilerOption()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion


       
    }
}
