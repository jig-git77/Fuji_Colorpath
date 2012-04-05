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
            _viewMeasurementSetting = viewMeasurementSetting;
        }

        public void GetObserverType()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            _viewMeasurementSetting.Observer = projectBusiness.GetObserverTypeList();
        }

        public void GetIlluminant()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            _viewMeasurementSetting.Illumin = projectBusiness.GetIlluminantList();
        }

        public void GetMeasurementDevice()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            _viewMeasurementSetting.Measurement = projectBusiness.GetMeasurementDeviceList(_viewMeasurementSetting.DeviceId);
        }

        public void GetReferenceChartType()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            _viewMeasurementSetting.ReferenceChartType = projectBusiness.GetReferenceChartTypeList(_viewMeasurementSetting.MeasurementDeviceId);
        }

        public void GetReferenceChartTypeImage()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            _viewMeasurementSetting.ReferenceChartTypeImage = projectBusiness.GetChartPreviewOnReferenceChartTypeId(_viewMeasurementSetting.ReferenceChartTypeId);
        }

    }
}
