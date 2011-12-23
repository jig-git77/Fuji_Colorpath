using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterSyncRunAnalyze
    {
        private readonly IViewSyncRunAnalyze _viewSyncRunAnalyze;

        public PresenterSyncRunAnalyze(IViewSyncRunAnalyze viewSyncRunAnalyze)
        {
            _viewSyncRunAnalyze = viewSyncRunAnalyze;
        }

        public void OptimizeProfile()
        {
            var task = (IMeasurementBusiness)new BusinessFactory<IMeasurementBusiness>().CreateObject();
            _viewSyncRunAnalyze.MeasurementIds = string.Join(",", task.GetMeasurement(_viewSyncRunAnalyze.SyncRunId).Select(x => x.MeasurementId).ToArray());

            var optimizeProfileTask = (ISyncRunBusiness)new BusinessFactory<ISyncRunBusiness>().CreateObject();
            optimizeProfileTask.OptimizeDeviceLinkProfile(_viewSyncRunAnalyze.MeasurementIds, _viewSyncRunAnalyze.SyncRunId, _viewSyncRunAnalyze.ProjectId);
        }
    }
}
