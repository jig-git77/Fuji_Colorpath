using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewTargetColorspace : IViewBase
    {
        List<Entity.CPSync1D.PrintStandardType> PrintStandardTypeList { get; set; }
        List<Entity.CPSync1D.ProjectTargetLAB> ProjectTargetLABList { get; set; }
        int PrintstandardTypeId { get; set; }
        bool IsSync1DType { get; }
        int ProjectTypeId { get; set; }
    }
}
