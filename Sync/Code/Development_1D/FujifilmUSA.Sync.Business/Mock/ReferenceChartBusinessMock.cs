using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class ReferenceChartBusinessMock : IReferenceChartBusiness
    {
        public int CreateReferenceChart(ReferenceChartType referenceChartType)
        {
            return 0;
        }

        public List<ReferenceChart> GetReferenceChartForLabCalculation(short sourceProfileId, short referenceChartTypeId)
        {
            return new List<ReferenceChart>();
        }

        public int InsertReferenceLabData(List<ReferenceChart> referenceCharts, byte[] sourceProfile, short sourceProfileId, int CompanyId)
        {
            return 0;
        }
        public List<ReferenceChartType> GetReferenceCharts(int RefChartID)
        {
            return new List<ReferenceChartType>();
        }
        public int UpdateReferenceChart(ReferenceChartType referenceChartType)
        {
            return 0;
        }

        public List<ReferenceChartType> GetReferenceChartsForXmf()
        {
            return new List<ReferenceChartType>();
        }

        public event ErrorHandler ErrorOccured;


        public ReferenceChartType GetReferenceChartDataForXmf(int refChartId)
        {
            return new ReferenceChartType();
        }
    }
}
