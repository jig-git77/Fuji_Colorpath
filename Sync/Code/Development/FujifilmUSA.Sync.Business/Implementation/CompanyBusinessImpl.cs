using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class CompanyBusinessImpl : ICompanyBusiness
    {
        #region  ICompnayBusiness Members

        public int InsertCompany(Company company)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[InsertCompany]",
                            new object[]
                                    {
                                        company.CompanyName,
                                        company.BrandId,
                                        company.RegionId,
                                        company.IsActive
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertCompany"));
            return returnValue;
        }

        public int UpdateCompnay(Company company)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[UpdateCompany]",
                            new object[]
                                    {
                                        company.CompanyId,
                                        company.CompanyName,
                                        company.BrandId,
                                        company.RegionId,
                                        company.IsActive 
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateCompany"));
            return returnValue;
        }

        public bool DeleteCompany(int companyId)
        {
            Common.ImplementBusinessLogic(
                () => DataAccessFactory.
                          Instance.
                          ExecuteProcedure(
                              "[CP].[DeleteCompany]",
                              new object[]
                                  {
                                      companyId
                                  }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteCompany"));
            return true;
        }

        public List<Company> GetCompany(int companyId)
        {
            var companies = new List<Company>();
            Common.ImplementBusinessLogic(
                () =>
                    {
                        var dscompanies = DataAccessFactory.Instance.GetDsResult("[CP].[GetCompanies]",new object[] { companyId} );
                        if (dscompanies != null && dscompanies.Tables.Count > 0 && dscompanies.Tables[0].Rows.Count > 0)
                        {
                            companies = dscompanies.Tables[0].AsEnumerable().
                                Select(x => new Company()
                                                {
                                                    CompanyId = DataReader.ReadInt(x, "CompanyId"),
                                                    CompanyName = DataReader.ReadString(x, "CompanyName"),
                                                    BrandId = DataReader.ReadInt(x, "BrandId"),
                                                    RegionId = DataReader.ReadByte(x, "RegionId"),
                                                    Region = new Region() {RegionId = DataReader.ReadByte(x, "RegionID"), RegionName = DataReader.ReadString(x, "Region")},
                                                    IsActive = DataReader.ReadBoolean(x, "IsActive")
                                                }).ToList();
                        }
                    },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetCompanies"));
            return companies;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
