using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    [Serializable]
    public class SpotProjectChartSetting
    {
        public int SpotProjectChartSettingID { get; set; }
        public int SpotProjectID { get; set; }
        public string ChartName { get; set; }
        public int NumOfRows { get; set; }
        public int NumOfCols { get; set; }
        public decimal ChartSizeX { get; set; }
        public decimal ChartSizeY { get; set; }
        public decimal PatchSizeX { get; set; }
        public decimal PatchSizeY { get; set; }
        public string FirstSample { get; set; }
        public string ReadDirection { get; set; }
        public string ReadMode { get; set; }
        public int Pages { get; set; }
        public int RowsPerPage { get; set; }
        public int ChartBorder { get; set; }
        public int Vgap { get; set; }
        public int Hgap { get; set; }
        public int GapLeval { get; set; }
        public int PreviousL { get; set; }
        public int ChartRes { get; set; }
        public bool IsCustom { get; set; }
    }

    [Serializable]
    public class SpotTargetLAB
    {        
        public short SpotID { get; set; }
        public string SpotName { get; set; }
        public decimal Target_L { get; set; }
        public decimal Target_a { get; set; }
        public decimal Target_b { get; set; }
        public string HTMLColor { get; set; }        
    }

    //[Serializable]
    public class SpotProjectLAB
    {
        public int SpotProjectLABID { get; set; }
        public int SpotProjectID { get; set; }
        public short SpotID { get; set; }
        public string SpotName { get; set; }
        public decimal Target_L { get; set; }
        public decimal Target_a { get; set; }
        public decimal Target_b { get; set; }
        public decimal Predicted_L { get; set; }
        public decimal Predicted_a { get; set; }
        public decimal Predicted_b { get; set; }
        public decimal deltaE { get; set; }
        public string Predicted_HTMLColor { get; set; }
        public bool IsSelected { get; set; }
    }

    public class SpotProjectLabModel
    {
        public List<SpotProjectLAB> spotProjectLabs { get; set; }
    }
}
