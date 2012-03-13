using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewProjectList 
    {
        List<Project> Projects { get; set; }
        Project Project { get; set; }
        int CompnayId { get; set; }
        Dictionary<int, string> SourceProfile{ get; set; }

    }
}
