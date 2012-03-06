using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class Permission
    {
        public byte PermissionId { get; set; }
        public string PermissionDesc { get; set; }
        public string Notes { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
    }
}