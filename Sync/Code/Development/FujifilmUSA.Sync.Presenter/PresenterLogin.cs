using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
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
            _viewLogin.CurrentUser = userBusiness.UserLogin(_viewLogin.UserName, _viewLogin.Password);
        }
    }
}
