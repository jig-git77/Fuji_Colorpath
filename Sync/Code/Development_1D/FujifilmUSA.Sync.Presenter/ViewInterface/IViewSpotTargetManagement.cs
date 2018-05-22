using FujifilmUSA.Sync.Entity.CPSpot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewSpotTargetManagement : IViewBase
    {
        List<SpotTarget> SpotTargetList { get; set; }
    }
}
