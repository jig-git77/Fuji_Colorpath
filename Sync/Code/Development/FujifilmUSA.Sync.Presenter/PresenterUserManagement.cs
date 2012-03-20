using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterUserManagement 
    {
        private readonly IViewUserManagement _viewUserManagement;
        private readonly IViewAddUserManagement _viewAddUserManagement;

        public PresenterUserManagement(IViewUserManagement viewUserManagement)
        {
            this._viewUserManagement = viewUserManagement;
        }
        public PresenterUserManagement(IViewAddUserManagement viewAddUserManagement)
        {
            this._viewAddUserManagement = viewAddUserManagement;
        }

        public void GetUsers()
        {
            var task = BusinessFactory<IUserBusiness>.CreateObject();
            _viewUserManagement.User = new Entity.User();
            _viewUserManagement.User.UserId = 0;
            _viewUserManagement.UserList = task.GetUsers(_viewUserManagement.User.UserId);
        }

        public void GetUserById()
        {
            var task = BusinessFactory<IUserBusiness>.CreateObject();
            _viewAddUserManagement.User = task.GetUsers(_viewAddUserManagement.User.UserId).FirstOrDefault();
        }

        public void InsertUser()
        {
            var task = BusinessFactory<IUserBusiness>.CreateObject();
            task.InsertUser(_viewAddUserManagement.User);
        }

        public void UpdateUser()
        {
            var task = BusinessFactory<IUserBusiness>.CreateObject();
            task.UpdateUser(_viewAddUserManagement.User);
        }

        public void DeleteUser()
        {
            var task = BusinessFactory<IUserBusiness>.CreateObject();
            task.DeleteUser(_viewUserManagement.User.UserId);
        }

        public void GetCompanies()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewAddUserManagement.Compnaies = task.CompanyList();
        }

        public void GetBranchByCompany()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewAddUserManagement.Branchs = task.BranchList(_viewAddUserManagement.CompanyId);
        }

        public void GetRegionByCompany()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewAddUserManagement.Regions = task.RegionListByCompnay(_viewAddUserManagement.CompanyId);
        }

        public void GetUserRole()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewAddUserManagement.UserRoleList = task.UserRoleList();
        }


    }
}
