namespace FujifilmUSA.Sync.Entity
{
    public class MeasuredColorChar
    {
        public int MeasuredColorCharId { get; set; }
        public int MeasurementPatchId { get; set; }
        public byte ColorCharId { get; set; }
        public decimal? ColorCharValue { get; set; }
        public MeasurementPatch MeasurementPatch { get; set; }
        public ColorChar ColorChar { get; set; }
    }
}