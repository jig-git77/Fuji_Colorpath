using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IProjectBussiness : ITaskBase
    {
        List<Project> GetProjectListBySyncDeviceId(int syncDeviceId);
        Project InsertProjectAndMeasurementSetting(Project objProject,MeasurementSetting objMeasurementSetting);
        int DeleteProject(int projectId);
        Project GetProjectByProjectId(int projectId);

    }
}
