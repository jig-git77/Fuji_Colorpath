using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class ProjectBusinessImpl:IProjectBusiness
    {
        #region IProjectBusiness Members

        public Project GetProjectDetail(int projectId)
        {
            var project = new Project();
            return project;
        }

        #endregion
    }
}
