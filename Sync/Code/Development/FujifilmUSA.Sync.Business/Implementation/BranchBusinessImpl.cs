using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class BranchBusinessImpl : IBranchBusiness
    {
        #region IBranchBusiness Members

        public Dictionary<int, string> GetBranchList(int CompanyId)
        {
            try
            {
                Dictionary<int, string> DstBranchList = new Dictionary<int, string>();
                DataSet DSBranchList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetCompanyList]", new object[] { CompanyId });
                if (DSBranchList != null && DSBranchList.Tables.Count > 0)
                {
                    DstBranchList = (from Branch in DSBranchList.Tables[0].AsEnumerable()
                                      select new
                                      {
                                          Value = int.Parse(Branch["BranchID"].ToString()),
                                          Text = Branch["BranchName"].ToString()
                                      }).ToDictionary(Branch => Branch.Value, Branch => Branch.Text);
                }
                return DstBranchList;

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
