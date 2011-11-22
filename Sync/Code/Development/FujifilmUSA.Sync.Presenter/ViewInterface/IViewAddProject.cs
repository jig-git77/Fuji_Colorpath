using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddProject
    {
        Dictionary<int, string> GetSourceProfileList { get; set; }
        Dictionary<int, string> GetDestinationProfileList { get; set; }
        List<DLP> GetDLPList { get; set; }
    }
}
