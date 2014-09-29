using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity.CPSync1D;
namespace FujifilmUSA.Sync.Business.Messages
{
    public class CurveDataResponse
    {
        public List<OptimizeRunCurve> CurveExportView { get; set; }
        public List<OptimizeRunCurve> CurveDeltaRefView { get; set; }
        public List<OptimizeRunCurve> CurveDeltaPrevRunView { get; set; }
    }
}
