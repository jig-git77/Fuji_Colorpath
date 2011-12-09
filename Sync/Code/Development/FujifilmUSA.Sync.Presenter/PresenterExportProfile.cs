using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterExportProfile
    {
        private readonly IViewExportProfile _viewExportProfile;

        public PresenterExportProfile(IViewExportProfile viewExportProfile)
        {
            _viewExportProfile = viewExportProfile;
        }

        public Project CloseSyncRunProcess()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            task.CloseSyncRun(_viewExportProfile.SyncRunId, Convert.ToInt32(_viewExportProfile.Status), _viewExportProfile.EndDateTime);
            return task.GetProjectByProjectId(_viewExportProfile.ProjectId);
        }

        public void OptimizeProfile()
        {
            var task = (ISyncRunBusiness)new BusinessFactory<ISyncRunBusiness>().CreateObject();
            _viewExportProfile.MeasurementIds = string.Join(",", task.GetMeasurement(_viewExportProfile.SyncRunId).Select(x => x.MeasurementId).ToArray());
            task.OptimizeDeviceLinkProfile(_viewExportProfile.MeasurementIds, _viewExportProfile.SyncRunId, _viewExportProfile.ProjectId);
        }
    }
}
