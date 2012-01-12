using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewDestinationProfiles : IViewBaseSyncDevice, IViewError
    {
        int DestinationProfileId { get; set; }
        List<DestinationProfile> DestinationProfiles { get; set; }
    }
}
