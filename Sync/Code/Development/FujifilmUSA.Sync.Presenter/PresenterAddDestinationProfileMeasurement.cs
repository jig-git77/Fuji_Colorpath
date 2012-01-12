using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterAddDestinationProfileMeasurement
    {
        private IViewAddDestinationProfileMeasurement _viewAddDestinationProfileMeasurement;

        public PresenterAddDestinationProfileMeasurement(IViewAddDestinationProfileMeasurement viewAddDestinationProfileMeasurement)
        {
            _viewAddDestinationProfileMeasurement = viewAddDestinationProfileMeasurement;
        }

        public void CreateDestinationProfile()
        {
            var task = (IMeasurementBusiness)new BusinessFactory<IMeasurementBusiness>().CreateObject();
            task.ErrorOccured += _viewAddDestinationProfileMeasurement.ErrorOccured;
            var measurementIds = string.Join(",",
                                                                    task.GetMeasurement(
                                                                        _viewAddDestinationProfileMeasurement.ProfileId,
                                                                        ProfileType.DestinationProfile).
                                                                        Select(x => x.MeasurementId).ToArray());

            var destTask = (IDestinationProfileBusiness)new BusinessFactory<IDestinationProfileBusiness>().CreateObject();
            destTask.ErrorOccured += _viewAddDestinationProfileMeasurement.ErrorOccured;
            destTask.CreateDestinationProfile(measurementIds, _viewAddDestinationProfileMeasurement.ProfileId, _viewAddDestinationProfileMeasurement.ProfilerOptionId);
        }
    }
}
