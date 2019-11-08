using FujifilmUSA.Sync.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujiFilmUSA.Sync.Service.Models
{
    public class ServerFilteringGrid
    {
        public int TotalItemCount { get; set; }
        public object Items { get; set; }
        public ServerFilteringGrid(object data, int totalItemCount)
        {
            Items = data;
            TotalItemCount = totalItemCount;
        }
    }
    public class SortDescription
    {
        public string field { get; set; }
        public string dir { get; set; }
    }

    public class FilterContainer
    {
        public List<FilterDescription> filters { get; set; }
        public string logic { get; set; }
    }

    public class FilterDescription
    {
        public string @operator { get; set; }
        public string field { get; set; }
        public string value { get; set; }
        public string dtype { get; set; }
    }

    public class ExportViewModel
    {
        public int OptimizationRunID { get; set; }
        public int CurveType { get; set; }
        public bool isFirstDeltaRefList { get; set; }
        public SortDescription sort { get; set; }
        public FilterContainer filter { get; set; }
        public string SpotProjectName { get; set; }
    }
    public class ExportPatchViewModel
    {
        public int? MeasurementId { get; set; }
        public bool? IsVerificationProject { get; set; }
        public bool? IsSync1DTypeProject { get; set; }
        public string metricSumID { get; set; }
        //public int OptimizationRunID { get; set; }
        public int CurveType { get; set; }
        public bool isFirstDeltaRefList { get; set; }
        public SortDescription sort { get; set; }
        public FilterContainer filter { get; set; }
    }

    public class DymoMeasurement
    {
        public string LicenseType { get; set; }
        public string DeviceName { get; set; }
        public Measurement MeasurementSummary { get; set; }
    }
}