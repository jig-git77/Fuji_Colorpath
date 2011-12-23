using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterAddDestinationProfile
    {
        private IViewAddDestinationProfile _viewAddDestinationProfile;

        public PresenterAddDestinationProfile(IViewAddDestinationProfile viewAddDestinationProfile)
        {
            _viewAddDestinationProfile = viewAddDestinationProfile;
        }

        public void CreateDestinationProfile()
        {
            var task = (IProfileBusiness)new BusinessFactory<IProfileBusiness>().CreateObject();
            _viewAddDestinationProfile.MeasurementIds = string.Join(",",
                                                                    task.GetMeasurement(_viewAddDestinationProfile.DestinationId, ProfileType.DestinationProfile).Select(
                                                                        x => x.MeasurementId).ToArray());

            var destTask = (IDestinationProfileBusiness)new BusinessFactory<IDestinationProfileBusiness>().CreateObject();
            _viewAddDestinationProfile.CreateResult = destTask.CreateDestinationProfile(_viewAddDestinationProfile.MeasurementIds, _viewAddDestinationProfile.DestinationId);
        }
    }
}
