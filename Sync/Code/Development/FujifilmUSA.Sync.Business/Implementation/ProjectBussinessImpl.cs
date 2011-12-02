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
                                        DeviceLinkProfileId = DsReader.ReadInt(x,"DLPID"),
                                        IsUsed = DsReader.ReadBoolean(x,"IsUsed"),
                                        ReTargetId = DsReader.ReadInt(x,"ReTargetID"),
                                        ProjectTypeId = DsReader.ReadInt(x,"ProjectTypeID"),
                                        OutputTypeId = DsReader.ReadInt(x,"OutPutTypeID"),
                                        ProjectStatus = (ECPSyncRunStatus)DsReader.ReadInt(x,"ProjStatus")
                                    }).ToList();
            }
            return objProjectList;
        }

        public Project InsertProjectAndMeasurementSetting(Project objProject, MeasurementSetting objMeasurementSetting)
        {
            var objProjectInPeramater = new List<Parameters>();
            var objProjectOutPeramater = new List<Parameters>();
            objProjectInPeramater.Add(new Parameters() {ParameterName = "@ProjectName", Size = 0, Type = DbType.String, Value = objProject.ProjectName});
            objProjectInPeramater.Add(new Parameters() {ParameterName = "@SyncDeviceID", Size = 0, Type = DbType.Int16, Value = objProject.SyncDeviceId});
            objProjectInPeramater.Add(new Parameters() {ParameterName = "@ΔEType", Size = 0, Type = DbType.Int16, Value = objProject.ΔEType});
            objProjectInPeramater.Add(new Parameters() {ParameterName = "@DLPID", Size = 0, Type = DbType.Int16, Value = objProject.DeviceLinkProfileId});
            objProjectInPeramater.Add(new Parameters() {ParameterName = "@IsUse", Size = 0, Type = DbType.Boolean, Value = objProject.IsUsed});
            objProjectInPeramater.Add(new Parameters() {ParameterName = "@UseDLP", Size = 0, Type = DbType.Boolean, Value = objProject.UseDLP});
            objProjectInPeramater.Add(new Parameters() {ParameterName = "@OutputTypeID", Size = 0, Type = DbType.Int16, Value = objProject.OutputTypeId});
            objProjectInPeramater.Add(new Parameters() {ParameterName = "@StartDatetime", Size = 0, Type = DbType.DateTime, Value = objProject.StartDate});
            objProjectInPeramater.Add(new Parameters() {ParameterName = "@ProjectTypeID", Size = 0, Type = DbType.Int16, Value = objProject.ProjectTypeId});
            //objInPeramater.Add(new Parameters() { ParameterName = "@ReTargetID", Size = 0, Type = DbType.Int16, Value = objProject.ReTargetId });

            objProjectOutPeramater.Add(new Parameters() {ParameterName = "@ProjectID", Size = 4, Type = DbType.Int16, Value = objProject.ProjectId});
            objProjectOutPeramater.Add(new Parameters() {ParameterName = "@SyncRunID", Size = 4, Type = DbType.Int16, Value = objProject.SyncRunId});

            


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
                                        objProjectOutPeramater.Where(x => x.ParameterName == "@ProjectID").Select(
                                            x => x.Value).
                                            FirstOrDefault());
                                objProject.SyncRunId =
                                    Convert.ToInt16(
                                        objProjectOutPeramater.Where(x => x.ParameterName == "@SyncRunID").Select(
                                            x => x.Value).
                                            FirstOrDefault());
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
            return objProject;

        }

        #endregion
    }
}
