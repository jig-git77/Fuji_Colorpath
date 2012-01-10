using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewSourceProfiles
    {
        int SourceProfileID {get;set;}
        List<SourceProfile> GetSourceProfiles { get; set; }
    }
}
