namespace FujifilmUSA.Sync.Entity
{
    public class ReferenceChartOrder
    {
        public int ReferenceChartOrderId { get; set; }
        public short SampleId { get; set; }
        public short SortOrder { get; set; }
        public byte? SortTypeId { get; set; }
    }
}