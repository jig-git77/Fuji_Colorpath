using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewTaskeroUser : IViewError
    {
        string UserName { get; set; }
        string Password { get; set; }
        string AllowedIp { get; set; }
        TaskeroUser GetTaskeroUser { get; set; }         
    }
}
