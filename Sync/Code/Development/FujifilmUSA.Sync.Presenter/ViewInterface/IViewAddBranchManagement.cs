using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddBranchManagement : IViewError
    {
        Branch Branch { get; set; }
        Dictionary<int, string> Compnaies { get; set; }
    }
}
