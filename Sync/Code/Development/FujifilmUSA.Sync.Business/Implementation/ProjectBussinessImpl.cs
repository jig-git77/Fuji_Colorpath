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

        public List<Project> GetProjectsBySyncDeviceId(int syncDeviceId)
        {
            var objProjectList = new List<Project>();
            //========== Get data from data access=================//
            var dsProjectList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetAllSyncProjectBySyncDeviceID]", new object[] { syncDeviceId });

            if (dsProjectList != null && dsProjectList.Tables.Count > 0 && dsProjectList.Tables[0].Rows.Count > 0)
            {
                objProjectList = dsProjectList.Tables[0].AsEnumerable().
                    Select(x => new Project()
                                    {
                                        ProjectId = DataReader.ReadInt(x, "ProjectID"),
                                        ProjectName = DataReader.ReadString(x, "ProjectName"),
                                        CreatedDate = DataReader.ReadDate(x, "CreatedDate"),
                                        SyncDeviceId = DataReader.ReadInt(x, "SyncDeviceID"),
                                        ΔEType = DataReader.ReadInt(x, "ΔEType"),
                                        DeviceLinkProfileId = DataReader.ReadInt(x, "DLPID"),
                                        IsUsed = DataReader.ReadBoolean(x, "IsUsed"),
                                        ReTargetId = DataReader.ReadInt(x, "ReTargetID"),
                                        ProjectTypeId = DataReader.ReadInt(x, "ProjectTypeID"),
                                        OutputTypeId = DataReader.ReadInt(x, "OutPutTypeID"),
                                        ProjectStatus = (ECPSyncRunStatus)DataReader.ReadInt(x, "ProjStatus")
                                    }).ToList();
            }
            return objProjectList;
        }

        public Project InsertProjectAndMeasurementSetting(Project objProject, MeasurementSetting objMeasurementSetting)
        {
            var objProjectInPeramater = new List<Parameters>();
            var objProjectOutPeramater = new List<Parameters>();
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@ProjectName", Size = 0, Type = DbType.String, Value = objProject.ProjectName });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@SyncDeviceID", Size = 0, Type = DbType.Int16, Value = objProject.SyncDeviceId });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@ΔEType", Size = 0, Type = DbType.Int16, Value = objProject.ΔEType });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@DLPID", Size = 0, Type = DbType.Int16, Value = objProject.DeviceLinkProfileId });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@IsUse", Size = 0, Type = DbType.Boolean, Value = objProject.IsUsed });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@UseDLP", Size = 0, Type = DbType.Boolean, Value = objProject.UseDLP });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@OutputTypeID", Size = 0, Type = DbType.Int16, Value = objProject.OutputTypeId });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@StartDatetime", Size = 0, Type = DbType.DateTime, Value = objProject.StartDate });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@ProjectTypeID", Size = 0, Type = DbType.Int16, Value = objProject.ProjectTypeId });
            //objInPeramater.Add(new Parameters() { ParameterName = "@ReTargetID", Size = 0, Type = DbType.Int16, Value = objProject.ReTargetId });

            objProjectOutPeramater.Add(new Parameters() { ParameterName = "@ProjectID", Size = 4, Type = DbType.Int16 });





            DataAccessFactory.Instance.ExecuteProcedureWithTransaction(
                new List<Action>()
                    {
                        () =>
                            {
                                objProjectOutPeramater =
                                    DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspInsertProject]",
                                                                                objProjectInPeramater,
                                                                                objProjectOutPeramater);
                                objProject.ProjectId =
                                    Convert.ToInt16(
                                        objProjectOutPeramater.Single(x => x.ParameterName == "@ProjectID").Value);
                            },
                        () =>
                            {
                                DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspInsertMeasurementSetting]",
                                                                            new object[]
                                                                                {
                                                                                    objProject.ProjectId,
                                                                                    //objMeasurementSetting.ProjectId,
                                                                                    objMeasurementSetting.UsePlugin,
                                                                                    objMeasurementSetting.Observer,
                                                                                    objMeasurementSetting.Illuminant,
                                                                                    objMeasurementSetting.DensityType,
                                                                                    objMeasurementSetting.UVIncluded,
                                                                                    objMeasurementSetting.
                                                                                        ReferenceChartTypeId,
                                                                                    objMeasurementSetting.IlluminationMode,
                                                                                    objMeasurementSetting.InstrumentTypeId,
                                                                                    objMeasurementSetting.SerialNumber
                                                                                });

                            }
                    });

            return GetProject(objProject.ProjectId);

        }

        public int DeleteProject(int projectId)
        {
            return DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspDeleteProject]",
                                                               new object[] { projectId });
        }

        public Project GetProject(int projectId)
        {
            var objProject = new Project();
            //========== Get data from data access=================//
            var dsProject = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetSyncProjectByProjectID]", new object[] { projectId });

            if (dsProject != null && dsProject.Tables.Count > 0 && dsProject.Tables[0].Rows.Count > 0)
            {
                objProject = dsProject.Tables[0].AsEnumerable().
                    Select(x => new Project()
                                    {
                                        ProjectId = DataReader.ReadInt(x, "ProjectID"),
                                        ProjectName = DataReader.ReadString(x, "ProjectName"),
                                        CreatedDate = DataReader.ReadDate(x, "CreatedDate"),
                                        SyncDeviceId = DataReader.ReadInt(x, "SyncDeviceID"),
                                        ΔEType = DataReader.ReadInt(x, "ΔEType"),
                                        DeviceLinkProfileId = DataReader.ReadInt(x, "DLPID"),
                                        IsUsed = DataReader.ReadBoolean(x, "IsUsed"),
                                        ReTargetId = DataReader.ReadInt(x, "ReTargetID"),
                                        ProjectTypeId = DataReader.ReadInt(x, "ProjectTypeID"),
                                        OutputTypeId = DataReader.ReadInt(x, "OutPutTypeID"),
                                        ProjectStatus = (ECPSyncRunStatus)DataReader.ReadInt(x, "ProjStatus"),
                                        ReferenceChartTypeId = DataReader.ReadInt(x, "ReferenceChartTypeId"),
                                    }).FirstOrDefault();

                objProject.ProjectRuns = dsProject.Tables[1].AsEnumerable().
                    Select(x => new SyncRun()
                                    {
                                        SyncRunId = DataReader.ReadInt(x, "SyncRunID"),
                                        SyncRunName = DataReader.ReadString(x, "SyncRunName"),
                                        ProjectId = DataReader.ReadInt(x, "ProjectID"),
                                        Status = DataReader.ReadShort(x, "Status"),
                                        StartDateTime = DataReader.ReadDate(x, "StartDateTime"),
                                        EndDateTime = DataReader.ReadDate(x, "EndDateTime"),
                                        MasterSyncRunId = DataReader.ReadInt(x, "MasterSyncRunID"),
                                        //HasMeasurements = (DataReader.ReadInt(x, "MeasurementCount") > 0),
                                        HasProfile = DataReader.ReadBoolean(x, "HasProfile")
                                    }).ToList();
            }
            return objProject;
        }

        public bool CloseSyncRun(int syncRunId, int status, DateTime endDateTime)
        {
            DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspCloseSyncRun]",
                                                        new object[] { syncRunId, status, endDateTime });
            return true;
        }

        public int CreateSyncRun(int projectId, int status, DateTime createdDateTime)
        {
            return DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspCreateSyncRun]",
                                                        new object[] { projectId, status, createdDateTime });
        }

        public MeasurementSetting GetMeasurementSettingByProjectId(int ProjectId)
        {
            var objMeasurementSetting = new MeasurementSetting();
            //========== Get data from data access=================//
            var dsMeasurementSetting = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetMeasurementSettingByProjectId]", new object[] { ProjectId });

            if (dsMeasurementSetting != null && dsMeasurementSetting.Tables.Count > 0 && dsMeasurementSetting.Tables[0].Rows.Count > 0)
            {

                objMeasurementSetting = dsMeasurementSetting.Tables[0].AsEnumerable().                
                    Select(x => new MeasurementSetting()
                    {
                        ProjectId = DataReader.ReadInt(x, "ProjectID"),
                        MeasurementSettingId = DataReader.ReadInt(x, "MeasurementSettingID"),
                        Illuminant = DataReader.ReadInt(x, "Illuminant"),
                        DensityType = DataReader.ReadInt(x, "DensityType"),
                        SerialNumber=DataReader.ReadString(x,"SerialNumber"),
                        ReferenceChartTypeId = DataReader.ReadInt(x, "ReferenceChartTypeID")                        
                    }).FirstOrDefault();                
            }
            return objMeasurementSetting;
        }
        #endregion
    }
}