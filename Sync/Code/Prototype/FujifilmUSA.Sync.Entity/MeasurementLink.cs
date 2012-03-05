namespace FujifilmUSA.Sync.Entity
{
    public class MeasurementLink
    {
        public int MeasurementLinkId { get; set; }
        public Measurement Measurement { get; set; }
        public ProfileType TypeId { get; set; }
        public int ProfileId { get; set; }
    }
}
