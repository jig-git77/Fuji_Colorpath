using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSpot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddSpotTarget : IViewBase
    {
        SpotTarget SpotTarget { get; set; }
        int CompanyId { get; set; }
        Dictionary<int, string> Compnaies { get; set; }
    }
}
