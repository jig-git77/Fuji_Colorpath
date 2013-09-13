using System;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterGlobalActivation
    {
        private readonly IViewGlobalActivation _viewGlobalActivation;

        public PresenterGlobalActivation(IViewGlobalActivation viewGlobalActivation)
        {
            _viewGlobalActivation = viewGlobalActivation;
        }

        public void GetUserAuthenticationInformation()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewGlobalActivation.ErrorOccured;
            _viewGlobalActivation.User = userBusiness.GetUserAuthenticationInformation(_viewGlobalActivation.User.AuthenticationKey);
        }

        public int UpdateUserAuthenticationInformation()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewGlobalActivation.ErrorOccured;
            return userBusiness.UpdateUserAuthenticationInformation(_viewGlobalActivation.User, _viewGlobalActivation.UpdateType);
        }

        public void GetSecurityQuestionList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewGlobalActivation.ErrorOccured;
            _viewGlobalActivation.SecurityQuestions = miscDataBusiness.GetSecurityQuestions();
        }

        public void UserLogin()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewGlobalActivation.ErrorOccured;
            _viewGlobalActivation.User = userBusiness.UserLogin(_viewGlobalActivation.User.LoginId, _viewGlobalActivation.User.LoginPass, UserType.WebSiteUser);
        }

        public void GetUserType()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewGlobalActivation.ErrorOccured;
            _viewGlobalActivation.User.UserType = userBusiness.GetUserType(_viewGlobalActivation.User.LoginId);
        }
    }
}
