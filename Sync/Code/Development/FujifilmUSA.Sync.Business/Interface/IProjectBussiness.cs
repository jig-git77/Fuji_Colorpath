using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using System;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IProjectBussiness : ITaskBase
    {
        List<Project> GetProjectListBySyncDeviceId(int syncDeviceId);
        Project InsertProjectAndMeasurementSetting(Project objProject,MeasurementSetting objMeasurementSetting);
        int DeleteProject(int projectId);
        Project GetProjectByProjectId(int projectId);
        void CloseSyncRun(int syncRunId, int status, DateTime endDateTime);
        void CreateSyncRun(int projectId, int status, DateTime createdDateTime);
    }
}
