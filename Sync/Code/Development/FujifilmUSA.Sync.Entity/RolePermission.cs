namespace FujifilmUSA.Sync.Entity
{
    public class RolePermission
    {
        public short RolePermissionId { get; set; }
        public short RoleId { get; set; }
        public short PermissionId { get; set; }
        public Permission Permission { get; set; }
        public Role Role { get; set; }
    }
}