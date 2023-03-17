namespace FujifilmUSA.Sync.Entity
{
    public class DeviceFamily
    {
        public short DeviceFamilyId { get; set; }
        public string DeviceFamilyDesc { get; set; }
        public bool IsActive { get; set; }
        public bool IsSupportCloudFlow { get; set; }
    }
}