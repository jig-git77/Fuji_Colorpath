using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Entity;
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
            var task = (ISyncRunBusiness)new BusinessFactory<ISyncRunBusiness>().CreateObject();
            _viewSyncRunAnalyze.MeasurementIds = string.Join(",", task.GetMeasurement(_viewSyncRunAnalyze.SyncRunId).Select(x => x.MeasurementId).ToArray());
            task.OptimizeDeviceLinkProfile(_viewSyncRunAnalyze.MeasurementIds, _viewSyncRunAnalyze.SyncRunId, _viewSyncRunAnalyze.ProjectId);
        }
    }
}
