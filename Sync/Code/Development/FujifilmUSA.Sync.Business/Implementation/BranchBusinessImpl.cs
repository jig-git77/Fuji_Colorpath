using System;
using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.CustomExceptions;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class BranchBusinessImpl : IBranchBusiness
    {
        #region  IcompanyBusiness Members

        public int InsertBranch(Branch branch)
        {
            int returnValue = 0;
            Common.
                ImplementBusinessLogic(
                    () =>
                    {
                        returnValue = DataAccessFactory.
                            Instance.
                            ExecuteProcedure(
                                "[CP].[InsertBranch]",
                                new object[]
                                        {
                                            branch.CompanyId,
                                            branch.BranchName,
                                            branch.Location,
                                            branch.SupportID,
                                            branch.IsActive
                                        });
                        if(returnValue == -1)
                        {
                            throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertBranch" };
                        }
                    },
                    new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertBranch"));
            return returnValue;
        }

        public int UpdateBranch(Branch branch)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[UpdateBranch]",
                            new object[]
                                    {
                                        branch.BranchId,
                                        branch.CompanyId,
                                        branch.BranchName,
                                        branch.Location,
                                        branch.SupportID,
                                        branch.IsActive,
                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertBranch" };
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateBranch"));
            return returnValue;
        }

        public bool DeleteBranch(int companyId)
        {
            Common.ImplementBusinessLogic(
                () =>
                DataAccessFactory.
                    Instance.
                    ExecuteProcedure(
                        "[CP].[DeleteBranch]",
                        new object[]
                            {
                                companyId
                            }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteBranch"));
            return true;
        }

        public List<Branch> GetBranch(int branchId,int currentUserId)
        {
            var branches = new List<Branch>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsbranches = DataAccessFactory.Instance.GetDsResult("[CP].[GetBranches]", new object[] { branchId, currentUserId });
                    if (dsbranches != null && dsbranches.Tables.Count > 0 && dsbranches.Tables[0].Rows.Count > 0)
                    {
                        branches = dsbranches.Tables[0].AsEnumerable().
                            Select(x => new Branch()
                                            {
                                                BranchId = DataReader.ReadInt(x, "BranchId"),
                                                BranchName = DataReader.ReadString(x, "BranchName"),
                                                Location = DataReader.ReadString(x, "Location"),
                                                CompanyId = DataReader.ReadInt(x, "CompanyId"),
                                                Company = new Company() { CompanyId = DataReader.ReadInt(x, "CompanyId"), CompanyName = DataReader.ReadString(x, "CompanyName") },
                                                Region = new Region() { RegionId = DataReader.ReadShort(x, "RegionId"), RegionName = DataReader.ReadString(x, "Region") },
                                                SupportID = DataReader.ReadInt(x, "SupportID"),
                                                IsActive = DataReader.ReadBoolean(x, "IsActive")
                                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetBranches"));
            return branches;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}