using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddProject : IViewGetSourceDestinationCombo, IViewBaseProject
    {
        List<DeviceLinkProfile> DeviceLinkProfiles { get; set; }

        int SourceProfileId { get; set; }
        int DestinationProfileId { get; set; }
        int MeasurementDeviceId { get; set; }

        MeasurementSetting MeasurementSetting { get; set; }

        DeviceLinkProfile deviceLinkProfile { get; set; }
    }
}
