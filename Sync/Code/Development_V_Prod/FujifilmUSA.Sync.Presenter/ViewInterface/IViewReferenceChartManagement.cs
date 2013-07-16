using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;
namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewReferenceChartManagement : IViewBase
    {       
        ReferenceChartType ReferenceChartType { get; set; }
        List<ReferenceChartType> ReferenceCharts { get; set; }
    }
}
