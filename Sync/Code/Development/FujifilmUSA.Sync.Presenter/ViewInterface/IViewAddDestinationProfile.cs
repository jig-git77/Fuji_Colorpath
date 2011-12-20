using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddDestinationProfile
    {
        int DestinationId { get; set; }
        string MeasurementIds { get; set; }
    }
}
