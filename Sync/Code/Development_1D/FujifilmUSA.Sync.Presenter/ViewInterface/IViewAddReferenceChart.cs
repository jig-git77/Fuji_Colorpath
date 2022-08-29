using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddReferenceChart : IViewBase
    {
        Dictionary<int, string> ProjectTypeList { get; set; }
        Dictionary<int, string> InstrumentTypeList { get; set; }
        ReferenceChartType ReferenceChartType { get; set; }
        Dictionary<int, string> DeviceFamilyList { get; set; }
    }
}
