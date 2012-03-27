using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterSourceProfile
    {
        private readonly IViewSourceProfile _viewSourceProfile;

        public PresenterSourceProfile(IViewSourceProfile viewSourceProfile)
        {
            _viewSourceProfile = viewSourceProfile;
        }

        public void CompanyList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewSourceProfile.ErrorOccured;
            _viewSourceProfile.CompanyList = miscDataBusiness.GetCompanyList();
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
            _viewSourceProfile.SourceProfiles = sourceProfileBusiness.GetSourceProfiles();
        }

        public void GetSourceProfile()
        {
            var sourceProfileBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            sourceProfileBusiness.ErrorOccured += _viewSourceProfile.ErrorOccured;
            _viewSourceProfile.SourceProfile = sourceProfileBusiness.GetSourceProfile(_viewSourceProfile.SourceProfile.SourceProfileId);
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
    }
}
