using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterDeviceLinkProfile
    {
        private readonly IViewDeviceLinkProfile _viewDeviceLinkProfile;

        public PresenterDeviceLinkProfile(IViewDeviceLinkProfile viewDeviceLinkProfile)
        {
            this._viewDeviceLinkProfile = viewDeviceLinkProfile;
        }
        public void GetDeviceLinkProfileListOnSyncDeviceId()
        {
            var task = (IDeviceLinkProfileBusiness)new BusinessFactory<IDeviceLinkProfileBusiness>().CreateObject();
            _viewDeviceLinkProfile.GetDeviceLinkProfileList = task.GetDeviceLinkProfilesOnSyncDeviceId(_viewDeviceLinkProfile.SyncDeviceId);

        }
        public void GetDeviceLinkProfileListByDLPID()
        {
            var task = (IDeviceLinkProfileBusiness)new BusinessFactory<IDeviceLinkProfileBusiness>().CreateObject();
            _viewDeviceLinkProfile.GetDeviceLinkProfileListByDLPID = task.GetDeviceLinkProfile(_viewDeviceLinkProfile.DLPID);
        }
    }
}
