using FujifilmUSA.Sync.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewMToolVersionManagement : IViewBase
    {
        MToolVersionUpdateList MToolVersion { get; set; }
        List<MToolVersionUpdateList> MToolVersionList { get; set; }
        Dictionary<int, string> OSPlatformNameList { get; set; }
    }
}
