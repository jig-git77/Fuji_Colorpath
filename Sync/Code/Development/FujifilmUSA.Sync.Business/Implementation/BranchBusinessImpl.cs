using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class BranchBusinessImpl : IBranchBusiness
    {
        #region IBranchBusiness Members

        public Dictionary<int, string> GetBranchList(int companyId)
        {
            var dstBranchList = new Dictionary<int, string>();
            var dsBranchList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetCompanyList]",
                                                                        new object[] {companyId});
            if (dsBranchList != null && dsBranchList.Tables.Count > 0)
            {
                dstBranchList = (dsBranchList.Tables[0].AsEnumerable().Select(
                    branch => new
                                  {
                                      Value = DsReader.ReadInt(branch, "BranchID"),
                                      Text = DsReader.ReadString(branch, "BranchName")
                                  })).ToDictionary(branch => branch.Value, branch => branch.Text);
            }
            return dstBranchList;
        }

        #endregion
    }
}
