using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterLogin
    {
        private readonly IViewLogin _viewLogin;
        public PresenterLogin(IViewLogin viewLogin)
        {
            _viewLogin = viewLogin;
        }

        public void UserLogin()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewLogin.ErrorOccured;
            _viewLogin.CurrentUser = userBusiness.UserLogin(_viewLogin.UserName, _viewLogin.Password, UserType.WebSiteUser);
        }

        public void InsertLoginActivityLogs()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewLogin.ErrorOccured;
            userBusiness.InsertLoginActivityLogs(_viewLogin.UserId, _viewLogin.OSName, _viewLogin.BrowserName, _viewLogin.EntryDate);
        }

        public bool ValidateUser()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewLogin.ErrorOccured;
            return userBusiness.ValidateUser(_viewLogin.EncyptedTocken);
        }

        public void UserLoginFromApplication()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewLogin.ErrorOccured;
            _viewLogin.UserReferenceChart = userBusiness.UserLoginReferenceChart(_viewLogin.UserName, _viewLogin.Password, UserType.WebSiteUser);
        }
    }
}
