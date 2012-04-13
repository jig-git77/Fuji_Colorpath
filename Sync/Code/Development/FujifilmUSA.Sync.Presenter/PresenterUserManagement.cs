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
            _viewUserManagement.UserList = userBusiness.GetUser(0, _viewUserManagement.CurrentUserId);
        }

        public void GetUser()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewAddUserManagement.ErrorOccured;
            _viewAddUserManagement.UserInformation = userBusiness.GetUser(_viewAddUserManagement.UserInformation.UserId, _viewAddUserManagement.CurrentUserId).FirstOrDefault();
        }

        public void InsertUser()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewAddUserManagement.ErrorOccured;
            userBusiness.InsertUser(_viewAddUserManagement.UserInformation);
        }

        public void UpdateUser()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewAddUserManagement.ErrorOccured;
            userBusiness.UpdateUser(_viewAddUserManagement.UserInformation);
        }

        public void DeleteUser()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewUserManagement.ErrorOccured;
            userBusiness.DeleteUser(_viewUserManagement.User.UserId);
        }

        public void GetBranchByCompany()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddUserManagement.ErrorOccured;
            _viewAddUserManagement.Branches = miscDataBusiness.GetBranchList(_viewAddUserManagement.CompanyId,_viewAddUserManagement.CurrentUserId);
        }

        public void GetRegions()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddUserManagement.ErrorOccured;
            _viewAddUserManagement.Regions = miscDataBusiness.GetRegionList(_viewAddUserManagement.CurrentUserId);
        }

        public void GetUserRole()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddUserManagement.ErrorOccured;
            _viewAddUserManagement.UserRoleList = miscDataBusiness.GetUserRoleList(_viewAddUserManagement.CurrentUserId);
        }

        public void GetCompaniesByRegion()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddUserManagement.ErrorOccured;
            _viewAddUserManagement.Companies = miscDataBusiness.GetCompaniesByRegion(_viewAddUserManagement.RegionId, _viewAddUserManagement.CurrentUserId);
        }
    }
}