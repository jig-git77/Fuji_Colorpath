using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class MeasurementPatchApp
    {
        public int MeasurementPatchId { get; set; }
        public int MeasurementId { get; set; }        
        public int SampleID { get; set; }
        public string SampleName { get; set; }
        public CieXYZ Ciexyz { get; set; }
        public CieLAB Cielab { get; set; }
        //public CMYK Cmyk { get; set; }

        public string Δe { get; set; }
        public string Δe94 { get; set; }
        public string Δe2000 { get; set; }
        public string HtmlColor { get; set; }
        public DensityApp Density { get; set; }
        public SpectralApp Spectral { get; set; }

        //for spot color measurement pathches properties
        public int SpotId { get; set; }
        public string SpotName { get; set; }

        public int PrintStandardTypeID { get; set; }
        public int InstrumentTypeID { get; set; }
        public int ReferenceChartTypeID { get; set; }
    }

    public class CMYK
    {
        public string C { get; set; }
        public string M { get; set; }
        public string Y { get; set; }
        public string K { get; set; }
    }

    public class CieXYZ
    {
        public string X { get; set; }
        public string Y { get; set; }
        public string Z { get; set; }
    }
    public class CieLAB
    {
        public string L { get; set; }
        public string A { get; set; }
        public string B { get; set; }
    }

    public class DensityApp
    {
        public string Red { get; set; }
        public string Green { get; set; }
        public string Blue { get; set; }
        public string Visual { get; set; }
    }

    public class SpectralApp
    {
        public string nm380 { get; set; }
        public string nm390 { get; set; }
        public string nm400 { get; set; }
        public string nm410 { get; set; }
        public string nm420 { get; set; }
        public string nm430 { get; set; }
        public string nm440 { get; set; }
        public string nm450 { get; set; }
        public string nm460 { get; set; }
        public string nm470 { get; set; }
        public string nm480 { get; set; }
        public string nm490 { get; set; }
        public string nm500 { get; set; }
        public string nm510 { get; set; }
        public string nm520 { get; set; }
        public string nm530 { get; set; }
        public string nm540 { get; set; }
        public string nm550 { get; set; }
        public string nm560 { get; set; }
        public string nm570 { get; set; }
        public string nm580 { get; set; }
        public string nm590 { get; set; }
        public string nm600 { get; set; }
        public string nm610 { get; set; }
        public string nm620 { get; set; }
        public string nm630 { get; set; }
        public string nm640 { get; set; }
        public string nm650 { get; set; }
        public string nm660 { get; set; }
        public string nm670 { get; set; }
        public string nm680 { get; set; }
        public string nm690 { get; set; }
        public string nm700 { get; set; }
        public string nm710 { get; set; }
        public string nm720 { get; set; }
        public string nm730 { get; set; }
    }
}
