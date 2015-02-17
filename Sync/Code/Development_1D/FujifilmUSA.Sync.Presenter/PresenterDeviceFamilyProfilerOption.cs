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
            _viewDeviceFamilyProfilerOption = viewDeviceFamilyProfilerOption;
        }

        public void GetDeviceFamilyProfilerOption()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewDeviceFamilyProfilerOption.ErrorOccured;
            _viewDeviceFamilyProfilerOption.ProfileOption = projectBusiness.GetDeviceFamilyProfilerOptionPreset(_viewDeviceFamilyProfilerOption.DeviceFamilyId);
        }

        public void GetMetricSumTargetValues()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewDeviceFamilyProfilerOption.ErrorOccured;
            _viewDeviceFamilyProfilerOption.MetricSumTargetValues = projectBusiness.GetMetricSumTargetValues(_viewDeviceFamilyProfilerOption.MetricTargetPresetId);
        }

        public void GetPrintStandardTVI()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewDeviceFamilyProfilerOption.ErrorOccured;
            _viewDeviceFamilyProfilerOption.PrintStandardTVI = projectBusiness.GetPrintStandardTVI(_viewDeviceFamilyProfilerOption.PrintStandardTypeId);
        }

        public void GetPrintStandardDensity()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewDeviceFamilyProfilerOption.ErrorOccured;
            _viewDeviceFamilyProfilerOption.ProjectTargetDensity = projectBusiness.GetPrintStandardDensity(_viewDeviceFamilyProfilerOption.PrintStandardTypeId);
        }
    }
}
