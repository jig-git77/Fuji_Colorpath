using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Implementation;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterBranch
    {

        private readonly IViewSyncDevice _ViewSyncDevice;

        public PresenterBranch(IViewSyncDevice ViewSyncDevice)
        {
            this._ViewSyncDevice = ViewSyncDevice;
        }
        public void GetBranchList()
        {
            IBranchBusiness task = (IBranchBusiness)new BusinessFactory<IBranchBusiness>().CreateObject();
            _ViewSyncDevice.GetBranchList = task.GetBranchList(_ViewSyncDevice.CompanyID);
        }
    }
}
