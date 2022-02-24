using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FujifilmUSA.Sync.Business.Mock
{
    public class UpdateChangeLogMock : IUpdateChangeLogListBusiness
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShowActiveUpdate"></param>
        /// <param name="ShowActiveAlert"></param>
        /// <returns></returns>
        public DataSet GetActiveUpdateChangeLogs(bool ShowActiveUpdate, bool ShowActiveAlert)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateLog"></param>
        /// <returns></returns>
        public int InsertUpdateChangeLogs(UpdateChangeLog updateLog)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateListID"></param>
        /// <returns></returns>
        public DataSet GetAllUpdateChangeLogs(int updateListID)
        {
            throw new NotImplementedException();
        }

        public int DeleteUpdateChangeLogs(int updateListID)
        {
            throw new NotImplementedException();
        }


        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
