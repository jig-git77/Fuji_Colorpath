using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewProjectList : IViewError
    {
        List<Project> Projects { get; set; }
        Project Project { get; set; }
        int CompanyId { get; set; }
        Dictionary<int, string> SourceProfile { get; set; }
        Dictionary<int, string> ProfilerOptionsPreset { get; set; }
        int ProjectId { get; set; }
    }
}
