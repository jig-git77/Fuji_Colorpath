using System;
using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class ProjectBussinessImpl : IProjectBussiness
    {
        #region IProjectBussiness Members

        public List<Project> GetProjectListBySyncDeviceId(int syncDeviceId)
        {
            var objProjectList = new List<Project>();
            //========== Get data from data access=================//
            var dsProjectList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetAllSyncProjectBySyncDeviceID]", new object[] { syncDeviceId });

            if (dsProjectList != null && dsProjectList.Tables.Count > 0 && dsProjectList.Tables[0].Rows.Count > 0)
            {
                objProjectList = dsProjectList.Tables[0].AsEnumerable().
                    Select(x => new Project()
                                    {
                                        ProjectId = DsReader.ReadInt(x,"ProjectID"),
                                        ProjectName = DsReader.ReadString(x,",ProjectName"),
                                        CreatedDate = DsReader.ReadDate(x,"CreatedDate"),
                                        SyncDeviceId = DsReader.ReadInt(x,"SyncDeviceID"),
                                        ΔEType = DsReader.ReadInt(x,"ΔEType"),
                                        DLPId = DsReader.ReadInt(x,"DLPID"),
                                        IsUsed = DsReader.ReadBoolean(x,"IsUsed"),
                                        ReTargetId = DsReader.ReadInt(x,"ReTargetID"),
                                        ProjectTypeId = DsReader.ReadInt(x,"ProjectTypeID"),
                                        OutputTypeId = DsReader.ReadInt(x,"OutPutTypeID"),
                                        ProjectStatus = (Enums.CPSyncRunStatus)DsReader.ReadInt(x,"ProjStatus")
                                    }).ToList();
            }
            return objProjectList;
        }

        public Project InsertProject(Project objProject)
        {
            var objInPeramater = new List<Parameters>();
            var objOutPeramater = new List<Parameters>();
            objInPeramater.Add(new Parameters() { ParameterName = "@ProjectName", Size = 0, Type = DbType.String, Value = objProject.ProjectName });
            objInPeramater.Add(new Parameters() { ParameterName = "@SyncDeviceID", Size = 0, Type = DbType.Int16, Value = objProject.SyncDeviceId });
            objInPeramater.Add(new Parameters() { ParameterName = "@ΔEType", Size = 0, Type = DbType.Int16, Value = objProject.ΔEType });
            objInPeramater.Add(new Parameters() { ParameterName = "@DLPID", Size = 0, Type = DbType.Int16, Value = objProject.DLPId });
            objInPeramater.Add(new Parameters() { ParameterName = "@IsUse", Size = 0, Type = DbType.Boolean, Value = objProject.IsUsed });
            objInPeramater.Add(new Parameters() { ParameterName = "@UseDLP", Size = 0, Type = DbType.Boolean, Value = objProject.UseDLP});
            objInPeramater.Add(new Parameters() { ParameterName = "@OutputTypeID", Size = 0, Type = DbType.Int16, Value = objProject.OutputTypeId });
            objInPeramater.Add(new Parameters() { ParameterName = "@StartDatetime", Size = 0, Type = DbType.DateTime, Value = objProject.StartDate });
            objInPeramater.Add(new Parameters() { ParameterName = "@ProjectTypeID", Size = 0, Type = DbType.Int16, Value = objProject.ProjectTypeId });
            //objInPeramater.Add(new Parameters() { ParameterName = "@ReTargetID", Size = 0, Type = DbType.Int16, Value = objProject.ReTargetId });

            objOutPeramater.Add(new Parameters() { ParameterName = "@ProjectID", Size = 4, Type = DbType.Int16, Value = objProject.ProjectId });
            objOutPeramater.Add(new Parameters() { ParameterName = "@SyncRunID", Size = 4, Type = DbType.Int16, Value = objProject.SyncRunId });

            objOutPeramater = DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspInsertProject]", objInPeramater, objOutPeramater);

            //objProject.ProjectId = 

            DataAccessFactory.Instance.ExecuteProcedureWithTransaction(new List<Action>()
                                                                           {
                                                                               () =>
                                                                                   {
                                                                                       DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspInsertProject]", objInPeramater,
                                                                                                                                   objOutPeramater);
                                                                                   },
                                                                               () =>
                                                                                   {
                                                                                       DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspInsertProject]", objInPeramater,
                                                                                                                                   objOutPeramater);
                                                                                   }
                                                                           });
            return objProject;

        }

        #endregion
    }
}
