using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class ProjectBussinessMock : IProjectBussiness
    {

        #region IProjectBussiness Members

        public List<Project> GetProjectListBySyncDeviceID(int SyncDeviceID)
        {
            return new List<Project>();
        }

        #endregion
    }
}
