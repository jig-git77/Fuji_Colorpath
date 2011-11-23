using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;


namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterMeasurementDevice
    {
        private readonly IViewAddProject _viewAddProject;

        public PresenterMeasurementDevice(IViewAddProject viewAddProject)
        {
            this._viewAddProject = viewAddProject;
        }

        public void  GetMeasurementDeviceListBySyncDeviceId()
        {
            var task = (IMeasurementDeviceBusiness) new BusinessFactory<IMeasurementDeviceBusiness>().CreateObject();
            _viewAddProject.GetMeasurementDeviceListBySyncDeviceId = task.GetMeasurementDeviceListBySyncDeviceId(_viewAddProject.SyncDeviceId);
        }
    }
}
