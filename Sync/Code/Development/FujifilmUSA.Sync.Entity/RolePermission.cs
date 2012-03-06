namespace FujifilmUSA.Sync.Entity
{
    public class RolePermission
    {
        public short RolePermissionId { get; set; }
        public byte RoleId { get; set; }
        public byte PermissionId { get; set; }
        public Permission Permission { get; set; }
        public Role Role { get; set; }
    }
}