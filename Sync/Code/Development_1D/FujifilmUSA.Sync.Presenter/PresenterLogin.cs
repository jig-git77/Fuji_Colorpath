using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Data;

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

        public void UserLoginFromApplicationBySerial()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewLogin.ErrorOccured;
            _viewLogin.UserReferenceChartApp = userBusiness.UserLoginReferenceChartBySerial(_viewLogin.DeviceSerialNo, _viewLogin.InstrumentTypeID);
        }
        public void UserLogInByDeviceLAB()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewLogin.ErrorOccured;
            _viewLogin.UserReferenceChartApp = userBusiness.UserLogInByDeviceLAB(_viewLogin.DeviceSerialNo, _viewLogin.InstrumentTypeID);
        }

        public int ValidateToken()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewLogin.ErrorOccured;
            return userBusiness.validateUserByAuthKey(_viewLogin.EncyptedTocken);
        }
        public void UserLogInByDeviceLAB_Xmf()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewLogin.ErrorOccured;
            _viewLogin.UserReferenceChartApp = userBusiness.UserLogInByDeviceLAB_Xmf(_viewLogin.DeviceSerialNo, _viewLogin.InstrumentTypeID,_viewLogin.UserId);
        }

        #region RevoriaUser Methods
        public DataSet InserTempProjectRevoriaUser(RevoriaUser user)
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewLogin.ErrorOccured;
            return userBusiness.InserTempProjectRevoriaUser(_viewLogin.RevoriaUser);
        }
        #endregion

    }
}
