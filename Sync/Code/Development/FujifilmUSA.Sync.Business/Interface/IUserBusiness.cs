using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IUserBusiness : ITaskBase
    {
        User UserLogin(string userName, string password, string allowedIp);
    }
}
