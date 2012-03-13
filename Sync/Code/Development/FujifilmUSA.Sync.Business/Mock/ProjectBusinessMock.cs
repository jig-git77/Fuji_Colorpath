using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class ProjectBusinessMock:IProjectBusiness
    {
        #region IProjectBusiness Members

        public Project GetProjectDetail(int projectId)
        {
            return new Project();
        }

        public List<Project> GetProjectsByDeviceId(int deviceId)
        {
            return new List<Project>();
        }
        public int DeleteProject(int projectId)
        {
            return 0;
        }
        public Dictionary<int, string> GetSourceProfile()
        {
            return new Dictionary<int, string>();
        }

        #endregion
    }
}
