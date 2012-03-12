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

        #endregion
    }
}
