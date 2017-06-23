using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddBranchManagement : IViewBase
    {
        Branch Branch { get; set; }
        Dictionary<int, string> Companies { get; set; }
        Dictionary<int, string> RegionList { get; set; }
        int RegionId { get; set; }
    }
}
