using System.Linq;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterUserManagement 
    {
        private readonly IViewUserManagement _viewUserManagement;
        private readonly IViewAddUserManagement _viewAddUserManagement;

        public PresenterUserManagement(IViewUserManagement viewUserManagement)
        {
            _viewUserManagement = viewUserManagement;
        }
        public PresenterUserManagement(IViewAddUserManagement viewAddUserManagement)
        {
            _viewAddUserManagement = viewAddUserManagement;
        }

        public void GetUsers()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewUserManagement.ErrorOccured;
            _viewUserManagement.UserList = userBusiness.GetUser(0);
        }

        public void GetUser()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewAddUserManagement.ErrorOccured;
            _viewAddUserManagement.User = userBusiness.GetUser(_viewAddUserManagement.User.UserId).FirstOrDefault();
        }

        public void InsertUser()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewAddUserManagement.ErrorOccured;
            userBusiness.InsertUser(_viewAddUserManagement.User);
        }

        public void UpdateUser()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewAddUserManagement.ErrorOccured;
            userBusiness.UpdateUser(_viewAddUserManagement.User);
        }

        public void DeleteUser()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewUserManagement.ErrorOccured;
            userBusiness.DeleteUser(_viewUserManagement.User.UserId);
        }

        public void GetCompanies()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddUserManagement.ErrorOccured;
            _viewAddUserManagement.Compnaies = miscDataBusiness.GetCompanyList();
        }

        public void GetBranchByCompany()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddUserManagement.ErrorOccured;
            _viewAddUserManagement.Branchs = miscDataBusiness.GetBranchList(_viewAddUserManagement.CompanyId);
        }

        public void GetRegionByCompany()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddUserManagement.ErrorOccured;
            _viewAddUserManagement.Regions = miscDataBusiness.GetRegionListByCompany(_viewAddUserManagement.CompanyId);
        }

        public void GetUserRole()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddUserManagement.ErrorOccured;
            _viewAddUserManagement.UserRoleList = miscDataBusiness.GetUserRoleList();
        }
    }
}