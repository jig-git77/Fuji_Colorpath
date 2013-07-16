using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewActivityPrintCondition : IViewBase
    {
        List<Activity> ActivityPrintList { get; set; }
        int RegionID { get; set; }
    }
}
