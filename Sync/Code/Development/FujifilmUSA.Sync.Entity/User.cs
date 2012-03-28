using System.Collections.Generic;
using System;
using System.Linq;
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
        public Role Role { get; set; }
        public byte? RegionId { get; set; }
        public int LastModifiedUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public UserCompany UserCompany { get; set; }
        public Region Region { get; set; }

        public List<UserLicense> UserLicenses { get; set; }
        public List<UserBranch> UserBranches { get; set; }

        public bool HasPermission<T>(T t)
        {
            if (this.Role != null && this.Role.RoleId == 0)
                return true;
            if (this.Role != null && this.Role.RolePermissions != null && this.Role.RolePermissions.Count > 0)
                return this.Role.RolePermissions.Any(p => p.Permission.Equals(t) && p.Allow);
            else
                return false;
        }

        //Get Multiple Branchs
        public string BranchName
        {
            get
            {
                string branchName = string.Empty;
                if (this.UserBranches != null && this.UserBranches.Count > 0)
                {
                    foreach (UserBranch ub in this.UserBranches)
                    {
                        if (ub.Branch != null)
                            branchName += ub.Branch.BranchName + " </Br> ";
                    }
                }
                return branchName;
            }
        }
    }
}