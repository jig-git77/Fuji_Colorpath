using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewGetSourceDestinationCombo : IViewBaseSyncDevice
    {
        Dictionary<int, string> GetSourceProfiles { get; set; }
        Dictionary<int, string> GetDestinationProfiles { get; set; }
    }
}
