using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class TaskeroUserBusinessImpl : ITaskeroUserBusiness
    {
        #region IBusTaskeroUser Members

        public TaskeroUser UserLogin(string UserName, string Password)
        {
            return new TaskeroUser();
        }

        #endregion
    }
}
