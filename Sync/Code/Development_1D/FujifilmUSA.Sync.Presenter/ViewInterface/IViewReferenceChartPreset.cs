using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewReferenceChartPreset : IViewBase
    {
        #region IViewPrintStandardType Members
        int ReferenceChartTypeID { get; set; }
        Dictionary<int, string> lstReferenceChartTypeID { get; set; }
        Dictionary<int, string> lstMetricTargetPresetID { get; set; }
        List<int> lstReferenceChartPresetId { get; set; }
        List<Entity.CPSyncV.ReferenceChartPreset> lstReferenceChartPreset { get; set; }
        #endregion
    }
}
