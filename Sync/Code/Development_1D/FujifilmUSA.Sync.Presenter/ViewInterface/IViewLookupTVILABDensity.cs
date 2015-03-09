using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewLookupTVILABDensity : IViewBase
    {
        short PrintStandardTypeID { get; set; }
        Dictionary<int, string> PrintStandardTypeList { get; set; }
        List<Entity.CPSync1D.PrintStandardTVI> PrintStandardTVI { get; set; }
        List<Entity.CPSync1D.ProjectTargetLAB> PrintStandardLAB { get; set; }
        List<Entity.CPSync1D.ProjectTargetDensity> PrintStandardDensity { get; set; }
        short ProjectTypeID { get; set; }
    }
}
