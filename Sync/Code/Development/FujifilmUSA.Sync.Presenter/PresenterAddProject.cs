using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterAddProject
    {
        private readonly IViewAddProject _viewAddProject;
        public PresenterAddProject(IViewAddProject viewAddProject)
        {
            this._viewAddProject = viewAddProject;
        }

        public void GetSourceProfileList()
        {
            var task = (ISourceProfileBusiness)new BusinessFactory<ISourceProfileBusiness>().CreateObject();
            _viewAddProject.GetSourceProfileList = task.GetSourceProfileList();
        }

        public void GetDestinationProfileList()
        {
            var task = (IDestinationProfileBusiness)new BusinessFactory<IDestinationProfileBusiness>().CreateObject();
            _viewAddProject.GetDestinationProfileList = task.GetDestinationProfileList();
        }

        public void GetDeviceLinkProfileList()
        {
            var task = (IDeviceLinkProfileBusiness)new BusinessFactory<IDeviceLinkProfileBusiness>().CreateObject();
            _viewAddProject.GetDeviceLinkProfileList = task.GetDeviceLinkProfileList();

        }

        public void GetMeasurementDeviceListBySyncDeviceId()
        {
            var task = (IMeasurementDeviceBusiness)new BusinessFactory<IMeasurementDeviceBusiness>().CreateObject();
            _viewAddProject.GetMeasurementDeviceListBySyncDeviceId = task.GetMeasurementDeviceListBySyncDeviceId(_viewAddProject.SyncDeviceId);
        }

        public void GetReferenceChartTypeListByMeasurementDeviceId()
        {
            var task = (IReferenceChartBusiness)new BusinessFactory<IReferenceChartBusiness>().CreateObject();
            _viewAddProject.GetReferenceChartTypeListByMeasurementDeviceId = task.GetReferenceChartTypeListByMeasurementDeviceId(_viewAddProject.MeasurementDeviceId);
        }

        public void InsertProjectAndMeasurementSetting()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            _viewAddProject.InsertProjectAndMeasurementSetting = task.InsertProjectAndMeasurementSetting(_viewAddProject.ObjProject, _viewAddProject.ObjMeasurementSetting);
        }
    }
}
