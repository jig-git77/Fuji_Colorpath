using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterDeviceFamilyProfilerOption
    {
        private readonly IViewDeviceFamilyProfilerOption _viewDeviceFamilyProfilerOption;

        public PresenterDeviceFamilyProfilerOption(IViewDeviceFamilyProfilerOption viewDeviceFamilyProfilerOption)
        {
            this._viewDeviceFamilyProfilerOption = viewDeviceFamilyProfilerOption;
        }

        public void GetDeviceFamilyProfilerOption()
        {
            var task = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewDeviceFamilyProfilerOption.ProfileOption = task.GetDeviceFamilyProfilerOptionPreset(_viewDeviceFamilyProfilerOption.DeviceFamilyId);
        }
    }
}
