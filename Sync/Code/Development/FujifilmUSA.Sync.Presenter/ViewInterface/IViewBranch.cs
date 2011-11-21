using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewBranch
    {
        int CompanyId { get; set; }
        Dictionary<int, string> GetBranchList { get; set; }
    }
}
