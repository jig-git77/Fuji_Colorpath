using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class ProjectBussinessMock : IProjectBussiness
    {
        #region IProjectBussiness Members

        public List<Project> GetProjectListBySyncDeviceId(int syncDeviceId)
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

        public Project GetProjectByProjectId(int projectId)
        {
            return new Project();
        }

        public void CloseSyncRun(int syncRunId, int status, System.DateTime endDateTime)
        {
            //Do Nothing...
        }

        public void CreateSyncRun(int projectId, int status, System.DateTime createdDateTime)
        {
            //Do Nothing...
        }
        public MeasurementSetting GetMeasurementSettingByProjectID(int projectID)
        {
            return new MeasurementSetting();
        }
        #endregion
    }
}
