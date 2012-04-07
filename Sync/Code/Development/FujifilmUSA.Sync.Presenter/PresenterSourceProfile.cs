using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using System.Linq;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterSourceProfile
    {
        private readonly IViewSourceProfile _viewSourceProfile;

        public PresenterSourceProfile(IViewSourceProfile viewSourceProfile)
        {
            _viewSourceProfile = viewSourceProfile;
        }

        public void GetCompaniesByRegion()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewSourceProfile.ErrorOccured;
            _viewSourceProfile.CompanyList = miscDataBusiness.GetCompaniesByRegion(_viewSourceProfile.RegionId);
        }

        public void RegionList()
        {
            var sourceProfileBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            sourceProfileBusiness.ErrorOccured += _viewSourceProfile.ErrorOccured;
            _viewSourceProfile.RegionList = sourceProfileBusiness.GetRegionList();
        }

        public void GetSourceProfiles()
        {
            var sourceProfileBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            sourceProfileBusiness.ErrorOccured += _viewSourceProfile.ErrorOccured;
            _viewSourceProfile.SourceProfiles = sourceProfileBusiness.GetSourceProfile(0);
        }

        public void GetSourceProfile()
        {
            var sourceProfileBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            sourceProfileBusiness.ErrorOccured += _viewSourceProfile.ErrorOccured;
            _viewSourceProfile.SourceProfile = sourceProfileBusiness.GetSourceProfile(_viewSourceProfile.SourceProfile.SourceProfileId).FirstOrDefault();
        }

        public void InsertSourceProfile()
        {
            var sourceProfileBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            sourceProfileBusiness.ErrorOccured += _viewSourceProfile.ErrorOccured;
            sourceProfileBusiness.InsertSourceProfile(_viewSourceProfile.SourceProfile);
        }

        public void UpdateSourceProfile()
        {
            var sourceProfileBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            sourceProfileBusiness.ErrorOccured += _viewSourceProfile.ErrorOccured;
            sourceProfileBusiness.UpdateSourceProfile(_viewSourceProfile.SourceProfile);
        }

        public void DeleteSourceProfile()
        {
            var sourceProfileBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            sourceProfileBusiness.ErrorOccured += _viewSourceProfile.ErrorOccured;
            sourceProfileBusiness.DeleteSourceProfile(_viewSourceProfile.SourceProfile.SourceProfileId);
        }
        public void GetIccProfileBySourceProfile()
        {
            var sourceProfileBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            sourceProfileBusiness.ErrorOccured += _viewSourceProfile.ErrorOccured;
            _viewSourceProfile.SourceProfile.Profile = sourceProfileBusiness.GetIccProfileBySourceProfile(_viewSourceProfile.SourceProfile.SourceProfileId);
        }

    }
}
