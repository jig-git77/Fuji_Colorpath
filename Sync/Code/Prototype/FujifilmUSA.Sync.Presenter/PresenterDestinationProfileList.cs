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
            task.ErrorOccured += _viewDestinationProfile.ErrorOccured;
            _viewDestinationProfile.DestinationProfiles = task.GetDestinationProfiles(_viewDestinationProfile.SyncDeviceId);
        }

        public int DeleteDestinationProfile()
        {
            var task = (IDestinationProfileBusiness)new BusinessFactory<IDestinationProfileBusiness>().CreateObject();
            task.ErrorOccured += _viewDestinationProfile.ErrorOccured;
            return task.DeleteDestinationProfile(_viewDestinationProfile.DestinationProfileId);
        }

        public void GetProfileByDestinationProfileId()
        {
            var task = (IDestinationProfileBusiness)new BusinessFactory<IDestinationProfileBusiness>().CreateObject();
            task.ErrorOccured += _viewDestinationProfile.ErrorOccured;
            _viewDestinationProfile.Profile = new OptimizeProfile
            {
                Data =
                {
                    Profile = task.GetProfileByDestinationProfileId(_viewDestinationProfile.DestinationProfileId)
                }
            };
        }
    }
}
