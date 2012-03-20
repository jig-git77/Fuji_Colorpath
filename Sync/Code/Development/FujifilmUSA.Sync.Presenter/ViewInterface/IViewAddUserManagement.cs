using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddUserManagement 
    {
        User User { get; set; }
        int CompanyId { get; set; }
        Dictionary<int, string> Compnaies { get; set; }
        Dictionary<int, string> Branchs { get; set; }
        Dictionary<int, string> Regions { get; set; }
        Dictionary<int, string> UserRoleList { get; set; }
    }
}
