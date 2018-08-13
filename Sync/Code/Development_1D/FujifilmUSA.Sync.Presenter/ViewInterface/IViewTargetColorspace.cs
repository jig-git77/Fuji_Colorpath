using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewTargetColorspace : IViewBase
    {
        List<Entity.CPSync1D.PrintStandardType> PrintStandardTypeList { get; set; }
        List<Entity.CPSync1D.ProjectTargetLAB> ProjectTargetLABList { get; set; }
        List<Entity.CPSpot.SpotTarget> SpotTargetList { get; set; }
        int PrintstandardTypeId { get; set; }
        bool IsSync1DType { get; }
        bool IsSpotTargetType { get; }
        int ProjectTypeId { get; set; }
        int TargetPrintCategory { get; set; }
        int CompanyID { get; set; }
        Dictionary<int, string> ReTargetSpotProjectList { get; set; }
    }
}
