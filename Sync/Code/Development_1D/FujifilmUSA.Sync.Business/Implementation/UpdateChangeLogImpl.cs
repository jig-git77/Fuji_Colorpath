using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class UpdateChangeLogImpl : IUpdateChangeLogListBusiness
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShowActiveUpdate"></param>
        /// <param name="ShowActiveAlert"></param>
        /// <returns></returns>
        public DataSet GetActiveUpdateChangeLogs(bool ShowActiveUpdate, bool ShowActiveAlert)
        {
            DataSet objUpdateChangeLogList = new DataSet();

            Common.ImplementBusinessLogic(
                () =>
                {
                    objUpdateChangeLogList = DataAccessFactory.Instance.GetDsResult("[dbo].[GetActiveUpdateChangeLogs]",
                                                                       new object[] { ShowActiveUpdate, ShowActiveAlert });
                }, new BusinessHandlerRequest(this, ErrorOccured, "Error_GetActiveUpdateChangeLogs"));
            return objUpdateChangeLogList;
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
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[dbo].[UserUpdateChangeLogsStatus]",
                            new object[]
                                    {
                                       userId,
                                       isUpdateAlert,
                                       isAlertPopup
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UserUpdateChangeLogsStatus"));
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateLog"></param>
        /// <returns></returns>
        public int InsertUpdateChangeLogs(UpdateChangeLog updateLog)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[dbo].[InsertUpdateChangeLogs]",
                            new object[]
                                    {
                                      updateLog.UpdateListID,
                                      updateLog.HeaderLine,
                                      updateLog.SubHeaderLine,
                                      updateLog.Description,
                                      updateLog.EntryDate,
                                      updateLog.ExpirationDate,
                                      updateLog.IsActive,
                                      updateLog.IsAlert
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertUpdateChangeLogs"));
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllUpdateChangeLogs(int updateListID)
        {
            DataSet objUpdateChangeLogList = new DataSet();
            Common.ImplementBusinessLogic(
                () =>
                {
                    objUpdateChangeLogList = DataAccessFactory.Instance.GetDsResult("[dbo].[GetAllUpdateChangeLogs]", new object[] { updateListID });
                }, new BusinessHandlerRequest(this, ErrorOccured, "Error_GetAllUpdateChangeLogs"));
            return objUpdateChangeLogList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateListID"></param>
        /// <returns></returns>
        public int DeleteUpdateChangeLogs(int updateListID)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.Instance.ExecuteProcedure("[dbo].[DeleteUpdateChangeLogs]", new object[] { updateListID });
                }, new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteUpdateChangeLogs"));
            return returnValue;
        }

        #region ITaskBase Members
        public event ErrorHandler ErrorOccured;
        #endregion
    }
}
