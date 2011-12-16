using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterDestinationProfile
    {
        private readonly IViewDestinationProfile _viewDestinationProfile;

        public PresenterDestinationProfile(IViewDestinationProfile viewDestinationProfile)
        {
            this._viewDestinationProfile = viewDestinationProfile;
        }
        public void GetDestinationProfileList()
        {
            var task = (IDestinationProfileBusiness)new BusinessFactory<IDestinationProfileBusiness>().CreateObject();
            _viewDestinationProfile.DestinationProfileListing = task.GetDestinationProfileList(_viewDestinationProfile.SyncDeviceId);
        }
    }
}
