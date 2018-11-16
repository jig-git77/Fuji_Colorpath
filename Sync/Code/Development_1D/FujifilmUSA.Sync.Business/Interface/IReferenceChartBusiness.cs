using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IReferenceChartBusiness : ITaskBase
    {
        int CreateReferenceChart(ReferenceChartType referenceChartType);
        List<ReferenceChart> GetReferenceChartForLabCalculation(short sourceProfileId, short referenceChartTypeId);
        int InsertReferenceLabData(List<ReferenceChart> referenceCharts, byte[] sourceProfile, short sourceProfileId, int CompanyId, int ProjectId);
        List<ReferenceChartType> GetReferenceCharts(int RefChartID);
        int UpdateReferenceChart(ReferenceChartType referenceChartType);
        List<ReferenceChartType> GetReferenceChartsForXmf();
        ReferenceChartType GetReferenceChartDataForXmf(int refChartId);
    }
}
