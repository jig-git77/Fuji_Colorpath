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
    }
}