using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterMeasurementSetting
    {
        private readonly IViewMeasurementSetting _viewMeasurementSetting;

        public PresenterMeasurementSetting(IViewMeasurementSetting viewMeasurementSetting)
        {
            this._viewMeasurementSetting = viewMeasurementSetting;
        }

        public void GetObserverType()
        {
            var task = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewMeasurementSetting.Observer = task.GetObserverTypeList();
        }

        public void GetIlluminant()
        {
            var task = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewMeasurementSetting.Illumin = task.GetIlluminantList();
        }

        public void GetMeasurementDevice()
        {
            var task = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewMeasurementSetting.Measurement = task.GetMeasurementDeviceList(_viewMeasurementSetting.deviceId);
        }

        public void GetReferenceChartType()
        {
            var task = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewMeasurementSetting.ReferenceChartType = task.GetReferenceChartTypeList(_viewMeasurementSetting.MeasurementDeviceID);
        }

    }
}
