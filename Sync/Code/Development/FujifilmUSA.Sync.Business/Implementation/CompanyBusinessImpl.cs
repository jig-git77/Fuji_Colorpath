using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class CompanyBusinessImpl : ICompanyBusiness
    {
        #region ICompanyBusiness Members

        public Dictionary<int, string> GetCompanyList()
        {
            var dstCompanyList = new Dictionary<int, string>(); 
            var dsCompanyList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetCompanyList]", new object[] { 0 });
            if (dsCompanyList != null && dsCompanyList.Tables.Count > 0)
            {
                dstCompanyList = (dsCompanyList.Tables[0].AsEnumerable().Select(
                    company => new
                                   {
                                       Value = DsReader.ReadInt(company, "CompanyID"),
                                       Text = DsReader.ReadString(company, "CompanyName")
                                   })).ToDictionary(company => company.Value, company => company.Text);
            }
            return dstCompanyList;
        }

        #endregion
    }
}
