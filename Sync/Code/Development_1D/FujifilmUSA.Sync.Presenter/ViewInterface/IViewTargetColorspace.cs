using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewTargetColorspace : IViewBase
    {
        List<Entity.CPSync1D.PrintStandardType> PrintStandardTypeList { get; set; }
        bool IsSync1DType { get; }
    }
}
