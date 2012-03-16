using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewBranchManagement 
    {
        Branch Branch { get; set; }
        List<Branch> BranchList { get; set; }
    }
}
