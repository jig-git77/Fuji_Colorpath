using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterUpdateChangeLog
    {
        private readonly IViewUpdateChangeLog _viewPresenterUpdateChangeLog;


        public PresenterUpdateChangeLog(IViewUpdateChangeLog viewPresenterUpdateChangeLog)
        {
            _viewPresenterUpdateChangeLog = viewPresenterUpdateChangeLog;
        }

        public void GetActiveUpdateChangeLogs(bool ShowActiveUpdate, bool ShowActiveAlert)
        {
            var userChangeLogBusiness = BusinessFactory<IUpdateChangeLogListBusiness>.CreateObject();
            userChangeLogBusiness.ErrorOccured += _viewPresenterUpdateChangeLog.ErrorOccured;
            _viewPresenterUpdateChangeLog.resultData = userChangeLogBusiness.GetActiveUpdateChangeLogs(ShowActiveUpdate, ShowActiveAlert);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isUpdateAlert"></param>
        /// <param name="isAlertPopup"></param>
        /// <returns></returns>
        public int UserUpdateChangeLogsStatus(int userId, bool isUpdateAlert, bool isAlertPopup)
        {
            var userChangeLogBusiness = BusinessFactory<IUpdateChangeLogListBusiness>.CreateObject();
            userChangeLogBusiness.ErrorOccured += _viewPresenterUpdateChangeLog.ErrorOccured;
            return userChangeLogBusiness.UserUpdateChangeLogsStatus(userId, isUpdateAlert, isAlertPopup);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateLog"></param>
        /// <returns></returns>
        public int InsertUpdateChangeLogs()
        {
            var userChangeLogBusiness = BusinessFactory<IUpdateChangeLogListBusiness>.CreateObject();
            userChangeLogBusiness.ErrorOccured += _viewPresenterUpdateChangeLog.ErrorOccured;
            return userChangeLogBusiness.InsertUpdateChangeLogs(_viewPresenterUpdateChangeLog.objUpdateChangeLog);
        }
        public void GetAllUpdateChangeLogs()
        {
            var userChangeLogBusiness = BusinessFactory<IUpdateChangeLogListBusiness>.CreateObject();
            userChangeLogBusiness.ErrorOccured += _viewPresenterUpdateChangeLog.ErrorOccured;
            _viewPresenterUpdateChangeLog.resultData = userChangeLogBusiness.GetAllUpdateChangeLogs(_viewPresenterUpdateChangeLog.objUpdateChangeLog.UpdateListID);
        }
        public int DeleteUpdateChangeLogs()
        {
            var userChangeLogBusiness = BusinessFactory<IUpdateChangeLogListBusiness>.CreateObject();
            userChangeLogBusiness.ErrorOccured += _viewPresenterUpdateChangeLog.ErrorOccured;
            return userChangeLogBusiness.DeleteUpdateChangeLogs(_viewPresenterUpdateChangeLog.objUpdateChangeLog.UpdateListID);
        }
    }
}
