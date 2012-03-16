using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterBranchManagement 
    {
        private readonly IViewBranchManagement _viewBranchManagement;
        private readonly IViewAddBranchManagement _viewAddBranchManagement;

        public PresenterBranchManagement(IViewBranchManagement viewBranchManagement)
        {
            this._viewBranchManagement = viewBranchManagement;
        }
        public PresenterBranchManagement(IViewAddBranchManagement viewAddBranchManagement)
        {
            this._viewAddBranchManagement = viewAddBranchManagement;
        }

        public void GetBranchs()
        {
            var task = BusinessFactory<IBranchBusiness>.CreateObject();
            _viewBranchManagement.BranchList = task.GetBranches();
        }

        public void InsertBranch()
        {
            var task = BusinessFactory<IBranchBusiness>.CreateObject();
            task.InsertBranch(_viewAddBranchManagement.Branch);
        }

        public void UpdateBranch()
        {
            var task = BusinessFactory<IBranchBusiness>.CreateObject();
            task.UpdateBranch(_viewAddBranchManagement.Branch);
        }

        public void DeleteBranch()
        {
            var task = BusinessFactory<IBranchBusiness>.CreateObject();
            task.DeleteBranch(_viewBranchManagement.Branch.BranchId);
        }

        public void GetCompanies()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewAddBranchManagement.Compnaies = task.CompanyList();
        }

    }
}
