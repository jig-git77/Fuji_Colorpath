using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using System.Linq;


namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterDeviceProfilerSettings
    {
        private readonly IViewDeviceProfilerSettings _viewDeviceProfilerSettings;

        public PresenterDeviceProfilerSettings(IViewDeviceProfilerSettings viewAddProfilerSettings)
        {
            _viewDeviceProfilerSettings = viewAddProfilerSettings;
        }

        public void InsertDeviceFamilyProfilerOption()
        {
            var profilerSettingsBusiness = BusinessFactory<IDeviceBusiness>.CreateObject();
            profilerSettingsBusiness.ErrorOccured += _viewDeviceProfilerSettings.ErrorOccured;
           _viewDeviceProfilerSettings.DeviceFamilyProfilerOptionID = profilerSettingsBusiness.InsertDeviceFamilyProfilerOption(_viewDeviceProfilerSettings.DeviceFamilyProfilerOption);
        } 

        public void GetDeviceFamilyList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewDeviceProfilerSettings.ErrorOccured;
            _viewDeviceProfilerSettings.DeviceFamilyList = miscDataBusiness.GetDeviceFamilyList();
        }

        public void GetProjectTypeList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewDeviceProfilerSettings.ErrorOccured;
            _viewDeviceProfilerSettings.ProjectTypeList = miscDataBusiness.GetProjectTypeList();
        }
    }
}
