using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewSourceProfile : IViewError
    {
        int SourceProfileId { get; set; }
        int RegionId { get; set; }
        int CompanyId { get; set; }
        string ProfileName { get; set; }
        string ProfileDesc { get; set; }
        byte[] Profile { get; set; }
        bool IsUsed { get; set; }

        SourceProfile SourceProfile { get; set; }
        List<SourceProfile> SourceProfiles { get; set; }
        Dictionary<int, string> CompanyList { get; set; }
        Dictionary<int, string> RegionList { get; set; }

    }
}
