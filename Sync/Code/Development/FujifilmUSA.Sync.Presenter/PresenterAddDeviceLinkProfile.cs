using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterAddDeviceLinkProfile
    {
        private IViewAddDeviceLinkProfile _viewAddDeviceLinkProfile;

        public PresenterAddDeviceLinkProfile(IViewAddDeviceLinkProfile viewAddDeviceLinkProfile)
        {
            _viewAddDeviceLinkProfile = viewAddDeviceLinkProfile;

        }
        public void CreateDeviceLinkProfile()
        {

            var deviceLinkTask = (IDeviceLinkProfileBusiness)new BusinessFactory<IDeviceLinkProfileBusiness>().CreateObject();
            _viewAddDeviceLinkProfile.CreateResult = deviceLinkTask.CreateDeviceLinkProfile(_viewAddDeviceLinkProfile.ObjDeviceLinkProfile);
        }
    }
}
