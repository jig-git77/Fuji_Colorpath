using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class ReportGrading
    {
        public bool IsAverageOfMeasurements { get; set; }
        public decimal CmykTolerance1 { get; set; }
        public decimal CmykScore1 { get; set; }
        public decimal CmykTolerance2 { get; set; }
        public decimal CmykScore2 { get; set; }
        public decimal CmykTolerance3 { get; set; }
        public decimal CmykScore3 { get; set; }
        public decimal CmykMaxScore { get; set; }

        public decimal RgbTolerance1 { get; set; }
        public decimal RgbScore1 { get; set; }
        public decimal RgbTolerance2 { get; set; }
        public decimal RgbScore2 { get; set; }
        public decimal RgbTolerance3 { get; set; }
        public decimal RgbScore3 { get; set; }
        public decimal RgbMaxScore { get; set; }

        public decimal CmyTolerance1 { get; set; }
        public decimal CmyScore1 { get; set; }
        public decimal CmyTolerance2 { get; set; }
        public decimal CmyScore2 { get; set; }
        public decimal CmyTolerance3 { get; set; }
        public decimal CmyScore3 { get; set; }
        public decimal CmyMaxScore { get; set; }

        public decimal PaperTolerance1 { get; set; }
        public decimal PaperScore1 { get; set; }
        public decimal PaperTolerance2 { get; set; }
        public decimal PaperScore2 { get; set; }
        public decimal PaperTolerance3 { get; set; }
        public decimal PaperScore3 { get; set; }
        public decimal PaperMaxScore { get; set; }
    }
}
