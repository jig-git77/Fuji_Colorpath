using System.Collections.Generic;

namespace FujifilmUSA.Sync.Entity
{
    public class Roles
    {
        public int RoleId { get; set; }
        public string RoleDesc { get; set; }
        public int RoleLevel { get; set; }
        public List<Permission> RolePermission { get; set; }
    }
}
