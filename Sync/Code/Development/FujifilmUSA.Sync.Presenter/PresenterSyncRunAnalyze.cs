using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;

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
            task.ErrorOccured += _viewSyncRunAnalyze.ErrorOccured;  
            _viewSyncRunAnalyze.MeasurementIds = string.Join(",", task.GetMeasurement(_viewSyncRunAnalyze.SyncRunId).Select(x => x.MeasurementId).ToArray());

            var optimizeProfileTask = (ISyncRunBusiness)new BusinessFactory<ISyncRunBusiness>().CreateObject();
            optimizeProfileTask.ErrorOccured += _viewSyncRunAnalyze.ErrorOccured;  
            optimizeProfileTask.OptimizeDeviceLinkProfile(_viewSyncRunAnalyze.MeasurementIds, _viewSyncRunAnalyze.SyncRunId, _viewSyncRunAnalyze.ProjectId);
        }

        public void CertifySyncRunProcess()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            task.ErrorOccured += _viewSyncRunAnalyze.ErrorOccured; 
            task.CertifySyncRun(_viewSyncRunAnalyze.SyncRunId, Convert.ToInt32(_viewSyncRunAnalyze.Status), _viewSyncRunAnalyze.EndDateTime);
        }
    }
}
