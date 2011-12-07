using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterExportProfile
    {
        private readonly IViewExportProfile _viewExportProfile;

        public PresenterExportProfile(IViewExportProfile viewExportProfile)
        {
            _viewExportProfile = viewExportProfile;
        }

        public void CloseSyncRunProcess()
        {
            var task = (IProjectBussiness) new BusinessFactory<IProjectBussiness>().CreateObject();
            task.CloseSyncRunProcess(_viewExportProfile.SyncRunId, _viewExportProfile.Status, _viewExportProfile.EndDateTime);
        }
    }
}
