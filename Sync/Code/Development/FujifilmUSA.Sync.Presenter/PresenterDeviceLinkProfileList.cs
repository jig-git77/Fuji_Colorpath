using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterDeviceLinkProfileList
    {
        private readonly IViewDeviceLinkProfiles _viewDeviceLinkProfile;

        public PresenterDeviceLinkProfileList(IViewDeviceLinkProfiles viewDeviceLinkProfile)
        {
            this._viewDeviceLinkProfile = viewDeviceLinkProfile;
        }
        public void GetDeviceLinkProfiles()
        {
            var task = (IDeviceLinkProfileBusiness)new BusinessFactory<IDeviceLinkProfileBusiness>().CreateObject();
            _viewDeviceLinkProfile.DeviceLinkProfiles = task.GetDeviceLinkProfilesOnSyncDeviceId(_viewDeviceLinkProfile.SyncDeviceId);
        }
    }
}
