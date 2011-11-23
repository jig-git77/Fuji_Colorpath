using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterReferenceChartType
    {
        private readonly IViewAddProject _viewAddProject;

        public PresenterReferenceChartType(IViewAddProject viewAddProject)
        {
            this._viewAddProject = viewAddProject;
        }

        public void GetReferenceChartTypeListByMeasurementDeviceId()
        {
            var task = (IReferenceChartTypeBusiness) new BusinessFactory<IReferenceChartTypeBusiness>().CreateObject();
            _viewAddProject.GetReferenceChartTypeListByMeasurementDeviceId = task.GetReferenceChartTypeListByMeasurementDeviceId(_viewAddProject.MeasurementDeviceId);
        }
    }
}
