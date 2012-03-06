using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class Branch
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string BranchName { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public List<License> Licenses { get; set; }
        public List<UserBranch> UserBranches { get; set; }
        public Company Company { get; set; }
    }
}