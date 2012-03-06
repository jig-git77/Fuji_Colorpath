namespace FujifilmUSA.Sync.Entity
{
    public class UserBranch
    {
        public short UserBranchId { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public User User { get; set; }
    }
}