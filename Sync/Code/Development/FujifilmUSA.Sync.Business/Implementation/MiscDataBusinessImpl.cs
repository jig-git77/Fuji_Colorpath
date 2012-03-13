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
            return dstBranchList;
        }

        public Dictionary<int, string> CompanyList()
        {
            var dstCompanyList = new Dictionary<int, string>();
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
            return dstCompanyList;
        }
        #endregion
    }
}