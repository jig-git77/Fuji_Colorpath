using System.Linq;
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
            var task = (IMeasurementBusiness)new BusinessFactory<IMeasurementBusiness>().CreateObject();
            var measurementIds = string.Join(",",
                                                                    task.GetMeasurement(
                                                                        _viewAddDestinationProfile.ProfileId,
                                                                        ProfileType.DestinationProfile).
                                                                        Select(x => x.MeasurementId).ToArray());

            var destTask = (IDestinationProfileBusiness)new BusinessFactory<IDestinationProfileBusiness>().CreateObject();
            destTask.CreateDestinationProfile(measurementIds, _viewAddDestinationProfile.ProfileId);
        }
    }
}
