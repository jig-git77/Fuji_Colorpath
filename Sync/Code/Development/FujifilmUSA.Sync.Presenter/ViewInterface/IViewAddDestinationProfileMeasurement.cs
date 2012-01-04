using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddDestinationProfileMeasurement
    {
        int ProfileId { get; set; }
        int ProfilerOptionId { get; set; }        
    }
}
