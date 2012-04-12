using System.Collections.Generic;
using System;
using System.Linq;
namespace FujifilmUSA.Sync.Entity
{
    public class User
    {
        #region Ctor
        public User()
        {
            Role = new Role();
            Region = new Region();
            UserCompany = new UserCompany();
            UserBranches = new List<UserBranch>();
        }
        #endregion

        #region Public Properties
        public int UserId { get; set; }
        public string LoginId { get; set; }
        public string LoginPass { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public short RegionId { get; set; }
        public int LastModifiedUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public UserCompany UserCompany { get; set; }
        public Region Region { get; set; }
        public List<UserBranch> UserBranches { get; set; }
        #endregion

        #region extension Methods
        public bool HasPermission(ERolePermission rolePermission)
        {
            if (Role.RoleId == 1) return true;
            if (Role.RolePermissions.Count == 0) return false;
            return Role.RolePermissions.Any(x => x.Permission == rolePermission && x.Allow);
        }
        public bool HasAdminPermission()
        {
            if (Role.RoleId == 1) return true;
            if (Role.RoleId == 1 || Role.RolePermissions.Count == 0) return false;
            return Role.RolePermissions.Where(x =>
                                              (x.Permission == ERolePermission.ManageBranch && x.Allow) ||
                                              (x.Permission == ERolePermission.ManageCompany && x.Allow) ||
                                              (x.Permission == ERolePermission.ManageDevice && x.Allow) ||
                                              (x.Permission == ERolePermission.ManageLicense && x.Allow) ||
                                              (x.Permission == ERolePermission.ManageMeasurementDevice && x.Allow) ||
                                              (x.Permission == ERolePermission.ManageRegion && x.Allow) ||
                                              (x.Permission == ERolePermission.ManageSourceProfile && x.Allow) ||
                                              (x.Permission == ERolePermission.ManageUser && x.Allow)).Count() > 0;
        }
        public bool HasProjectPermission()
        {
            if (Role.RoleId == 1) return true;
            if (Role.RoleId == 1 || Role.RolePermissions.Count == 0) return false;
            return Role.RolePermissions.Where(x =>
                                              (x.Permission == ERolePermission.CreateDeleteProject && x.Allow) ||
                                              (x.Permission == ERolePermission.ModifyProject && x.Allow)).Count() > 0;
        }
        public string BranchNames
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
        #endregion
    }
}