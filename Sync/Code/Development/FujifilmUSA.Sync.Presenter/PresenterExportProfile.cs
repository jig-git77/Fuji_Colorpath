using System;
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
            return task.GetProject(_viewExportProfile.ProjectId);
        }

        public void GetIccProfileBySyncRunId()
        {
            var task = (ISyncRunBusiness)new BusinessFactory<ISyncRunBusiness>().CreateObject();
            _viewExportProfile.Profile = new OptimizeProfile
                                             {
                                                 Data =
                                                     {
                                                         Profile = task.GetIccProfileBySyncRunId(_viewExportProfile.SyncRunId)
                                                     }
                                             };
        }
    }
}
