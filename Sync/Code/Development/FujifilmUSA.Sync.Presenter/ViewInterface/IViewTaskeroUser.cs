using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewTaskeroUser
    {
        string UserName { get; set; }
        string Password { get; set; }
        string AllowedIP { get; set; }
        TaskeroUser GetTaskeroUser { get; set; }         
    }
}
