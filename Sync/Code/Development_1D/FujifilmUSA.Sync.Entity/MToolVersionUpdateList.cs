namespace FujifilmUSA.Sync.Entity
{
    public class MToolVersionUpdateList
    {
        public short OSPlatformDetailsID { get; set; }
        public short OSPlatformID { get; set; }
        public string OSPlatformName { get; set; }
        public string LatestVersion { get; set; }
        public string MinReqVersion { get; set; }
        public string URLlocation { get; set; }
        public string UpdateInfo { get; set; }
        public bool IsUsed { get; set; }
    }
}
