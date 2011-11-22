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

        #endregion
    }
}
