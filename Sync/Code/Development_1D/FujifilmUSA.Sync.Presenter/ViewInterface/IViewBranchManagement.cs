using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewBranchManagement : IViewBase
    {
        Branch Branch { get; set; }
        List<Branch> BranchList { get; set; }
    }
}
