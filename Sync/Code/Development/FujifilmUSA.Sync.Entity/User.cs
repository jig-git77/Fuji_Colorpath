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
        
        public UserCompany UserCompanies { get; set; }
        public Region Region { get; set; }

        public List<UserLicense> UserLicenses { get; set; }
        public List<UserBranch> UserBranches { get; set; }

        //UseTo Fill the Grid Columns
        public string CompnayName
        {
               get 
               {
                   return (this.UserCompanies != null && this.UserCompanies.Company != null) ? this.UserCompanies.Company.CompanyName : string.Empty;
               } 
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