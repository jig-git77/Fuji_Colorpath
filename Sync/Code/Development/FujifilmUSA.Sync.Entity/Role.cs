using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class Role
    {
        public short RoleId { get; set; }
        public string RoleDesc { get; set; }
        public short RoleLevel { get; set; }
        public bool IsWebEnabled { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
    }
}