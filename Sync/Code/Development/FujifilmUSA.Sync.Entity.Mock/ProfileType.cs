using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class ProfileType
    {
        public byte ProfileTypeId { get; set; }
        public string Type { get; set; }
        public List<ProjectOption> ProjectOptions { get; set; }
    }
}