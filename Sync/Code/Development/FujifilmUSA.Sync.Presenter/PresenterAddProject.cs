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
            var task = (IMiscDataBusiness)new BusinessFactory<IMiscDataBusiness>().CreateObject();
            _viewAddProject.GetSourceProfileList = task.GetSourceProfileList();
        }

        public void GetDestinationProfiles()
        {
            var task = (IMiscDataBusiness)new BusinessFactory<IMiscDataBusiness>().CreateObject();
            _viewAddProject.GetDestinationProfiles = task.GetDestinationProfiles(_viewAddProject.SyncDeviceId);
        }

        public void GetDeviceLinkProfileList()
        {
            var task = (IDeviceLinkProfileBusiness)new BusinessFactory<IDeviceLinkProfileBusiness>().CreateObject();
            _viewAddProject.GetDeviceLinkProfileList = task.GetDeviceLinkProfiles(_viewAddProject.SourceProfileId, _viewAddProject.DestinationProfileId);

        }

        public void GetMeasurementDeviceListBySyncDeviceId()
        {
            var task = (IMiscDataBusiness)new BusinessFactory<IMiscDataBusiness>().CreateObject();
            _viewAddProject.GetMeasurementDeviceListBySyncDeviceId = task.GetMeasurementDeviceListBySyncDeviceId(_viewAddProject.SyncDeviceId);
        }

        public void GetReferenceChartTypeListByMeasurementDeviceId()
        {
            var task = (IMiscDataBusiness)new BusinessFactory<IMiscDataBusiness>().CreateObject();
            _viewAddProject.GetReferenceChartTypeListByMeasurementDeviceId = task.GetReferenceChartTypeListByMeasurementDeviceId(_viewAddProject.MeasurementDeviceId);
        }

        public void InsertProjectAndMeasurementSetting()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            _viewAddProject.InsertProjectAndMeasurementSetting = task.InsertProjectAndMeasurementSetting(_viewAddProject.ObjProject, _viewAddProject.ObjMeasurementSetting);
        }       
    }
}
