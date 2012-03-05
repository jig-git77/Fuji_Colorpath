using System.Linq;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterAddSourceProfile
    {
        private IViewAddSourceProfile _viewAddSourceProfile;
        
        public PresenterAddSourceProfile(IViewAddSourceProfile viewAddSourceProfile)
        {
            _viewAddSourceProfile = viewAddSourceProfile;
        }
        public void CreateSourceProfile()
        {
            var task = (IMeasurementBusiness)new BusinessFactory<IMeasurementBusiness>().CreateObject();
            task.ErrorOccured += _viewAddSourceProfile.ErrorOccured;
            var measurementIds = string.Join(",",
                                                                    task.GetMeasurement(
                                                                        _viewAddSourceProfile.ProfileId,
                                                                        ProfileType.SourceProfile).
                                                                        Select(x => x.MeasurementId).ToArray());

            var destTask = (ISourceProfileBusiness)new BusinessFactory<ISourceProfileBusiness>().CreateObject();
            destTask.ErrorOccured += _viewAddSourceProfile.ErrorOccured;
            destTask.CreateSourceProfile(measurementIds, _viewAddSourceProfile.ProfileId, _viewAddSourceProfile.ProfilerOptionId);
        }
        public void GetProfilerOption()
        {
            var task = (IMiscDataBusiness)new BusinessFactory<IMiscDataBusiness>().CreateObject();
            task.ErrorOccured += _viewAddSourceProfile.ErrorOccured; 
            _viewAddSourceProfile.GetProfileOption = task.GetProfilerOption();
        }
        
    }
}
