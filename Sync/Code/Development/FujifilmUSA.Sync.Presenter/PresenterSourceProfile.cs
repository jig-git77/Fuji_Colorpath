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
            this._viewSourceProfile = viewSourceProfile;
        }

        public void CompanyList()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewSourceProfile.CompanyList = task.CompanyList();
        }

        public void RegionList()
        {
            var task = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            _viewSourceProfile.RegionList = task.RegionList();
        }

        public void GetSourceProfiles()
        {
            var task = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            _viewSourceProfile.SourceProfiles = task.GetSourceProfiles();
        }

        public void GetSourceProfile()
        {
            var task = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            _viewSourceProfile.SourceProfile = task.GetSourceProfile(_viewSourceProfile.SourceProfile.SourceProfileId);
        }

        public void InsertSourceProfile()
        {
            var task = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            task.InsertSourceProfiel(_viewSourceProfile.SourceProfile);
        }

        public void UpdateSourceProfile()
        {
            var task = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            task.UpdateSourceProfile(_viewSourceProfile.SourceProfile);
        }

        public void DeleteSourceProfile()
        {
            var task = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            task.DeleteSourceProfile(_viewSourceProfile.SourceProfile.SourceProfileId);
        }


    }
}
