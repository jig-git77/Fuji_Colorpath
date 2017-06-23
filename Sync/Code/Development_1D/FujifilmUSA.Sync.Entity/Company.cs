using System.Collections.Generic;
using System;
namespace FujifilmUSA.Sync.Entity
{
    [Serializable]
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? BrandId { get; set; }
        public short RegionId { get; set; }
        public bool IsActive { get; set; }
        public Region Region { get; set; }
    }
}