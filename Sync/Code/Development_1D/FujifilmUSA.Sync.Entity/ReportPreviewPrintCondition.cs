namespace FujifilmUSA.Sync.Entity
{
    public class ReportPreviewPrintCondition
    {

        public string Description_Media { get; set; }
        public string Description_Colorant { get; set; }
        public string Description_RipSettings { get; set; }
        public string Description_Notes { get; set; }
        public string Description_TechincalName { get; set; }
        public string Metric_TargetPrintCond { get; set; }
        public string Metric_SourceProfile { get; set; }
        public string Metric_RelativePaperScaling { get; set; }
        public string Metric_PaperLabValues { get; set; }
        public string Metric_MinLPatchValues { get; set; }
        public string Metric_SelectedPereset { get; set; }
        public string Metric_PresetDescription { get; set; }
        public string Measurement_Device { get; set; }
        public string Measurement_Illuminat { get; set; }
        public string Measurement_Observer { get; set; }
        public string Measurement_DensityType { get; set; }
        public string Measurement_UVMode { get; set; }
        public string Measurement_BaseChart { get; set; }
        public string Measurement_OptimizationChart { get; set; }
        public string Validation_Metric { get; set; }
        public string Validation_MetricDescription { get; set; }
        public string Validation_MeasurementDevice { get; set; }
        public string Validation_UVMode { get; set; }
        public string Validation_ValidationChart { get; set; }
        public string OffSetPrinting_InkOrder { get; set; }
        public string OffSetPrinting_FountainSolution { get; set; }
        public string OffSetPrinting_Blanket { get; set; }
        public string OffSetPrinting_BlanketAge { get; set; }
        public string OffSetPrinting_Screening { get; set; }
        public string OffSetPrinting_Coating { get; set; }
        public string OffSetPrinting_ProjectType { get; set; }
        public string Retargeting_Device { get; set; }
        public string Retargeting_PrintCondition { get; set; }
        public bool Show_ValidationBox { get; set; }
        public bool Show_OffSetPrintingBox { get; set; }
        public bool Show_RetargetingBox { get; set; }

        public int BaseChartId { get; set; }
        public int OptimizationChartId { get; set; }
        public int ValidationChartId { get; set; }

        public string BaseChart_image { get; set; }      
        public string OptimizationChart_image { get; set; }
        public string ValidationChart_image { get; set; }
    }
}
