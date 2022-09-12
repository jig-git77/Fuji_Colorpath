using FujifilmUSA.Sync.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IReferenceChartPresetBusiness : ITaskBase
    {
        Dictionary<int, string> GetReferenceChartType();
        Dictionary<int, string> GetMetricTargetPresetID(int ReferenceChartTypeID);
        List<int> GetReferenceChartPreset(int ReferenceChartTypeID);
        int InsertReferenceChartPreset(int ReferenceChartTypeID, List<Entity.CPSyncV.ReferenceChartPreset> ReferenceChartPresets);
    }
}
