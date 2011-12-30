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
        public void GetProfilerOption()
        {
            var task = (IMiscDataBusiness)new BusinessFactory<IMiscDataBusiness>().CreateObject();
            _viewAddSourceProfile.GetProfileOption = task.GetProfilerOption();
        }
        
    }
}
