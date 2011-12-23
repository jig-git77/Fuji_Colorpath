using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using System;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IProjectBussiness : ITaskBase
    {
        List<Project> GetProjectsBySyncDeviceId(int syncDeviceId);
        Project InsertProjectAndMeasurementSetting(Project objProject, MeasurementSetting objMeasurementSetting);
        int DeleteProject(int projectId);
        Project GetProject(int projectId);
        bool CloseSyncRun(int syncRunId, int status, DateTime endDateTime);
        int CreateSyncRun(int projectId, int status, DateTime createdDateTime);
        MeasurementSetting GetMeasurementSettingByProjectId(int projectId);
    }
}
