using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class CompanyBusinessImpl : ICompanyBusiness
    {
        #region ICompanyBusiness Members

        public Dictionary<int, string> GetCompanyList()
        {
            try
            {
                Dictionary<int, string> DstCompanyList = new Dictionary<int, string>(); 
                DataSet DSCompanyList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetCompanyList]", new object[] { 0 });
                if (DSCompanyList != null && DSCompanyList.Tables.Count > 0)
                {
                    DstCompanyList = (from Company in DSCompanyList.Tables[0].AsEnumerable() 
                                      select new {
                                          Value = int.Parse(Company["CompanyID"].ToString()),
                                          Text = Company["CompanyName"].ToString()
                                      }).ToDictionary(Company => Company.Value, Company => Company.Text);
                }
                return DstCompanyList;
                
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        #endregion
    }
}
