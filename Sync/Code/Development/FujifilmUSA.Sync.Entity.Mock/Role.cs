using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity.Mock
{
    public class Role
    {
        public byte RoleId { get; set; }
        public string RoleDesc { get; set; }
        public byte RoleLevel { get; set; }
        public bool IsWebEnabled { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
    }
}