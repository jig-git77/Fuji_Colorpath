using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddBranchManagement 
    {
        Branch Branch { get; set; }
        Dictionary<int, string> Compnaies { get; set; }
    }
}
