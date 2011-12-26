using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterDestinationProfileList
    {
        private readonly IViewDestinationProfiles _viewDestinationProfile;

        public PresenterDestinationProfileList(IViewDestinationProfiles viewDestinationProfile)
        {
            this._viewDestinationProfile = viewDestinationProfile;
        }

        public void GetDestinationProfileList()
        {
            var task = (IDestinationProfileBusiness)new BusinessFactory<IDestinationProfileBusiness>().CreateObject();
            _viewDestinationProfile.DestinationProfiles = task.GetDestinationProfiles(_viewDestinationProfile.SyncDeviceId);
        }

        public int DeleteDestinationProfile()
        {
            var task = (IDestinationProfileBusiness)new BusinessFactory<IDestinationProfileBusiness>().CreateObject();
            return task.DeleteDestinationProfile(_viewDestinationProfile.DestinationProfileId);
        }


    }
}
