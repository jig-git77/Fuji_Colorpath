using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Linq;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterBranchManagement 
    {
        private readonly IViewBranchManagement _viewBranchManagement;
        private readonly IViewAddBranchManagement _viewAddBranchManagement;

        public PresenterBranchManagement(IViewBranchManagement viewBranchManagement)
        {
            _viewBranchManagement = viewBranchManagement;
        }
        public PresenterBranchManagement(IViewAddBranchManagement viewAddBranchManagement)
        {
            _viewAddBranchManagement = viewAddBranchManagement;
        }

        public void GetBranches()
        {
            var branchBusiness = BusinessFactory<IBranchBusiness>.CreateObject();
            branchBusiness.ErrorOccured += _viewBranchManagement.ErrorOccured;
            _viewBranchManagement.BranchList = branchBusiness.GetBranch(0,_viewBranchManagement.CurrentUserId);
        }

        public void GetBranch()
        {
            var branchBusiness = BusinessFactory<IBranchBusiness>.CreateObject();
            branchBusiness.ErrorOccured += _viewAddBranchManagement.ErrorOccured;
            _viewAddBranchManagement.Branch = branchBusiness.GetBranch(_viewAddBranchManagement.Branch.BranchId,_viewBranchManagement.CurrentUserId).FirstOrDefault();
        }


        public void InsertBranch()
        {
            var branchBusiness = BusinessFactory<IBranchBusiness>.CreateObject();
            branchBusiness.ErrorOccured += _viewAddBranchManagement.ErrorOccured;
            branchBusiness.InsertBranch(_viewAddBranchManagement.Branch);
        }

        public void UpdateBranch()
        {
            var branchBusiness = BusinessFactory<IBranchBusiness>.CreateObject();
            branchBusiness.ErrorOccured += _viewAddBranchManagement.ErrorOccured;
            branchBusiness.UpdateBranch(_viewAddBranchManagement.Branch);
        }

        public void DeleteBranch()
        {
            var branchBusiness = BusinessFactory<IBranchBusiness>.CreateObject();
            branchBusiness.ErrorOccured += _viewBranchManagement.ErrorOccured;
            branchBusiness.DeleteBranch(_viewBranchManagement.Branch.BranchId);
        }

        public void GetCompanies()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddBranchManagement.ErrorOccured;
            _viewAddBranchManagement.Compnaies = miscDataBusiness.GetCompanyList(_viewAddBranchManagement.CurrentUserId);
        }

    }
}
