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
        public Dictionary<string, string> GetProjectOption(int projectId, EProfileType type)
        {
            return new Dictionary<string, string>();
        }
        public Dictionary<int, string> GetSourceProfileList()
        {
            return new Dictionary<int, string>();
        }
        public Dictionary<int, string> GetDeviceFamilyProfilerOptionPreset()
        {
            return new Dictionary<int, string>();
        }

        public byte[] GetChartPreviewOnProjectId(int projectId)
        {
            return new byte[1];
        }

        public byte[] GetSourceProfile(int projectId)
        {
            return new byte[1];
        }

        public byte[] GetDestinationProfile(int projectId)
        {
            return new byte[1];
        }

        public byte[] GetLastOptimizedDlp(int projectId)
        {
            return new byte[1];
        }

        #endregion
    }
}
