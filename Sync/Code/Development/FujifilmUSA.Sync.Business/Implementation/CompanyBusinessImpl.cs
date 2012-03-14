using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using System;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class CompanyBusinessImpl : ICompanyBusiness
    {
        #region  ICompnayBusiness Members

        public int InsertCompany(Company company)
        {
            int returnValue = 0;
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspInsertCompany]", new object[]{
                                                                                                                company.CompanyName,
                                                                                                                company.BrandId,
                                                                                                                company.RegionId,
                                                                                                                company.IsActive
                                                                                                });
            return returnValue;
        }

        public int UpdateCompnay(Company company)
        {
            int returnValue = 0;
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspUpdateCompany]", new object[]{
                                                                                                                company.CompanyId,
                                                                                                                company.CompanyName,
                                                                                                                company.BrandId,
                                                                                                                company.RegionId,
                                                                                                 });
            return returnValue;
        }

        public bool DeleteCompany(int companyId)
        {
            DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspDeleteCompany]", new object[]{
             companyId
            });
            return true;
        }

        public List<Company> GetCompanies()
        {
            var companies = new List<Company>();
            var dscompanies = DataAccessFactory.Instance.GetDsResult("[CP].[uspGetCompanies]");
            if (dscompanies != null && dscompanies.Tables.Count > 0 && dscompanies.Tables[0].Rows.Count > 0)
            {
                companies = dscompanies.Tables[0].AsEnumerable().
                    Select(x => new Company()
                    {
                        CompanyId = DataReader.ReadInt(x, "CompanyId"),
                        CompanyName = DataReader.ReadString(x, "CompanyName"),
                        BrandId = DataReader.ReadInt(x, "BrandId"),
                        RegionId = DataReader.ReadByte(x, "RegionId"),
                        Region = new Region() { RegionId = DataReader.ReadByte(x, "RegionID"), RegionName = DataReader.ReadString(x, "Region") },
                        IsActive = DataReader.ReadBoolean(x, "IsActive")
                    }).ToList();
            }
            return companies;
        }

        #endregion
    }
}
