using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? BrandId { get; set; }
        public byte RegionId { get; set; }
        public bool IsActive { get; set; }
        public List<SourceProfile> SourceProfiles { get; set; }
        public List<Branch> Branches { get; set; }
        public List<UserCompany> UserCompanies { get; set; }
        public Region Region { get; set; }
    }
}