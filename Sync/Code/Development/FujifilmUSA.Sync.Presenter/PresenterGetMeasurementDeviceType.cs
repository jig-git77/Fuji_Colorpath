using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterGetMeasurementDeviceType
    {
        private IViewGetMeasurementDeviceType _viewGetMeasurementDeviceType;

        public PresenterGetMeasurementDeviceType(IViewGetMeasurementDeviceType viewGetMeasurementDeviceType)
        {
            _viewGetMeasurementDeviceType = viewGetMeasurementDeviceType;
        }
        public void GetMeasurementDeviceListBySyncDeviceId()
        {
            var task = (IMiscDataBusiness)new BusinessFactory<IMiscDataBusiness>().CreateObject();
            task.ErrorOccured += _viewGetMeasurementDeviceType.ErrorOccured; 
            _viewGetMeasurementDeviceType.MeasurementDevices = task.GetMeasurementDeviceListBySyncDeviceId(_viewGetMeasurementDeviceType.SyncDeviceId);
        }
    }
}
