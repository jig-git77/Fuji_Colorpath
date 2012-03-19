using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterPluginDataService
    {
        private readonly IViewPluginDataService _viewPluginDataService;
        public PresenterPluginDataService(IViewPluginDataService viewPluginDataService)
        {
            _viewPluginDataService = viewPluginDataService;
        }

        public void SaveMeasurement()
        {
            var task = Business.BusinessFactory<IMeasurementBusiness>.CreateObject();
            task.SaveMeasurement(_viewPluginDataService.Measurement);
        }

        public void GetReferenceChartData()
        {
            var task = Business.BusinessFactory<IProjectBusiness>.CreateObject();
            _viewPluginDataService.ReferenceChartData =  task.GetReferenceChartData(_viewPluginDataService.ProjectId);
        }
    }
}
