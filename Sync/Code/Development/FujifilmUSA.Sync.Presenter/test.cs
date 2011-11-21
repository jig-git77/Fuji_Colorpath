using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Implementation;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Mock;

namespace FujifilmUSA.Sync.Presenter
{
    class test
    {
        public TaskeroUser GetUser()
        {
            var taskeroUser = (ITaskeroUserBusiness)new BusinessFactory<TaskeroUserBusinessMock>();
            return taskeroUser.UserLogin("test", "test","test");
        }
    }
}
