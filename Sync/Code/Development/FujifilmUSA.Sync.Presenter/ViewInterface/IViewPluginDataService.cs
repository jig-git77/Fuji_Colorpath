using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewPluginDataService
    {
        Measurement Measurement { get; set; }
        int ProjectId { get; set; }
        ReferenceChartData ReferenceChartData { get; set; }
    }
}
