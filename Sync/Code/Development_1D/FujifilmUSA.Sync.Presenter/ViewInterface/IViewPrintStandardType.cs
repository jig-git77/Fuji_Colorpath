using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewPrintStandardType : IViewBase
    {
        short PrintStandardTypeID { get; set; }
        string PrintStandard { get; set; }
        bool IsSync1DType { get; set; }

        Entity.CPSync1D.PrintStandardType PrintStandardType { get; set; }
        List<Entity.CPSync1D.PrintStandardType> PrintStandardTypes { get; set; }
        Dictionary<int, string> SourceProfileList { get; set; }       
    }
}
