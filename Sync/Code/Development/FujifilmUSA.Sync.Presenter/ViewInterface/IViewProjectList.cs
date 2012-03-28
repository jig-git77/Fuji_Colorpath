using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewProjectList : IViewError
    {
        List<Project> Projects { get; set; }
        int CompanyId { get; set; }
        Dictionary<int, string> SourceProfile { get; set; }
        int DeviceId { get; set; }
        int ProjectId { get; set; }
    }
}
