using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class ProjectBussinessMock : IProjectBussiness
    {
        #region IProjectBussiness Members

        public List<Project> GetProjectsBySyncDeviceId(int syncDeviceId)
        {
            return new List<Project>();
        }
        public Project InsertProjectAndMeasurementSetting(Project objProject, MeasurementSetting objMeasurementSetting)
        {
            return new Project();  
        }

        public int DeleteProject(int projectId)
        {
            return 0;
        }

        public Project GetProject(int projectId)
        {
            return new Project();
        }

        public bool CloseSyncRun(int syncRunId, int status, DateTime endDateTime)
        {
            return true;
        }

        public int CreateSyncRun(int projectId, int status, DateTime createdDateTime)
        {
            return 0;
        }
        public MeasurementSetting GetMeasurementSettingByProjectId(int projectId)
        {
            return new MeasurementSetting();
        }
        #endregion
    }
}
