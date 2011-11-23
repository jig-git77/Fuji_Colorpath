using System;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Entity
{
    public class TaskeroUser
    {
        public int TaskeroUserId { get; set; }
        public string LoginId { get; set; }
        public string LoginPass { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public Boolean ShowCPVData { get; set; }
        public Boolean IsActive { get; set; }
        public int LastModifiedUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int RoleId { get; set; }
        public string Customer { get; set; }
        public string AllowedIp { get; set; }
        public int BrandId { get; set; }
        public Boolean EmailWhenYellow { get; set; }
        public Boolean EmailWhenRed { get; set; }
        public Boolean SiteLicenseAgreement { get; set; }
        public Roles UserRoles { get; set; }
        public List<Permission> UserPermission { get; set; }
        public List<Report> UserReport { get; set; }
    }
}
