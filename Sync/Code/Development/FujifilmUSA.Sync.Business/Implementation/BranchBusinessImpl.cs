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
    public class BranchBusinessImpl :  IBranchBusiness
    {
        #region  ICompnayBusiness Members

        public int InsertBranch(Branch branch)
        {
            int returnValue = 0;
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspInsertBranch]", new object[]{
                                                                                                                branch.BranchName,
                                                                                                                branch.Location ,
                                                                                                                branch.CompanyId,
                                                                                                                branch.IsActive 
                                                                                                });
            return returnValue;
        }

        public int UpdateBranch(Branch branch)
        {
            int returnValue = 0;
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspUpdateBranch]", new object[]{
                                                                                                                branch.BranchId,     
                                                                                                                branch.BranchName,
                                                                                                                branch.Location,
                                                                                                                branch.CompanyId,
                                                                                                                branch.IsActive,
                                                                                                 });
            return returnValue;
        }

        public bool DeleteBranch(int companyId)
        {
            DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspDeleteBranch]", new object[]{
             companyId
            });
            return true;
        }

        public List<Branch> GetBranches()
        {
            var branches = new List<Branch>();
            var dsbranches = DataAccessFactory.Instance.GetDsResult("[CP].[uspGetBranches]");
            if (dsbranches != null && dsbranches.Tables.Count > 0 && dsbranches.Tables[0].Rows.Count > 0)
            {
                branches = dsbranches.Tables[0].AsEnumerable().
                    Select(x => new Branch()
                    {
                        BranchId = DataReader.ReadInt(x, "BranchId"),
                        BranchName = DataReader.ReadString(x, "BranchName"),
                        Location = DataReader.ReadString(x, "BranchName"),
                        CompanyId = DataReader.ReadInt(x, "CompanyId"),
                        Company = new Company() { CompanyId = DataReader.ReadInt(x, "CompanyId"), CompanyName = DataReader.ReadString(x, "CompanyName") },
                        IsActive = DataReader.ReadBoolean(x, "IsActive")
                    }).ToList();
            }
            return branches;
        }

        #endregion
    }
}
