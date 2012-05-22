namespace FujifilmUSA.Sync.Entity
{
    public class Branch
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string BranchName { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public short UserBranchId { get; set; }
        public Company Company { get; set; }
        public int SupportID { get; set; }
        public Region Region { get; set; }
    }
}