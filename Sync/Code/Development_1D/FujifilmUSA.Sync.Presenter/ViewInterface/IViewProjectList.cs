using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSpot;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewProjectList : IViewBase
    {
        List<Project> Projects { get; set; }
        int CompanyId { get; set; }
        Dictionary<int, string> SourceProfile { get; set; }
        int DeviceId { get; set; }
        int ProjectId { get; set; }
        int LicenseTypeID { get; set; }
        Project Project { get; set; }
        //List<Entity.CPSpot.SpotProjectLibrary> SpotProjectLibraries { get; set; }
        SpotProjectList SpotProjectList { get; set; }
        int SpotProjectId { get; set; }
        string PQId { get; set; }
    }
}
