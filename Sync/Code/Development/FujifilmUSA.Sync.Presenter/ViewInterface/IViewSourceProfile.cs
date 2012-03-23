using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewSourceProfile
    {
        int SourceProfileID { get; set; }
        int RegionID { get; set; }
        int CompanyID { get; set; }
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
