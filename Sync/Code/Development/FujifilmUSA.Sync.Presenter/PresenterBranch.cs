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
         private readonly IViewBranch _ViewBranch;

        public PresenterBranch(IViewBranch ViewBranch)
        {
            this._ViewBranch = ViewBranch;
        }
        public void GetBranchList()
        {
            IBranchBusiness task = (IBranchBusiness)new BusinessFactory<BranchBusinessImpl>();
            _ViewBranch.GetBranchList = task.GetBranchList(_ViewBranch.CompanyId);
        }
    }
}
