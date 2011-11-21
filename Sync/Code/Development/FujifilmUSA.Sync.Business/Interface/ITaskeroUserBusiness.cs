using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface ITaskeroUserBusiness : ITaskBase
    {
        TaskeroUser UserLogin(string UserName, string Password, string AllowedIP);
    }
}
