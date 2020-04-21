using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSpot;

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
        int BranchID { get; set; }
        int DeviceID { get; set; }
        int ReTargetID { get; set; }
        Dictionary<int, string> ReTargetSpotProjectList { get; set; }
        Dictionary<int, string> Branches { get; set; }
        List<Device> Devices { get; set; }
        SpotProjectLibrary RetargetSpotProject { get; set; }
    }
}
