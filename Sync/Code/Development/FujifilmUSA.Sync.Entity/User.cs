using System.Collections.Generic;
using System;
namespace FujifilmUSA.Sync.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string LoginId { get; set; }
        public string LoginPass { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public short RoleId { get; set; }
        public byte? RegionId { get; set; }
        public int LastModifiedUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
        //public List<UserLicense> UserLicenses { get; set; }
        //public List<UserBranch> UserBranches { get; set; }
        //public List<UserCompany> UserCompanies { get; set; }
    }
}