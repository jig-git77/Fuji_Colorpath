using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class TaskeroUserBusinessMock : ITaskeroUserBusiness
    {
        #region IBusTaskeroUser Members

        public TaskeroUser UserLogin(string UserName, string Password, string AllowedIP)
        {
            return new TaskeroUser();
        }

        #endregion
    }
}
