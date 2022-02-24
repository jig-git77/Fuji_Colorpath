using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IUpdateChangeLogListBusiness : ITaskBase
    {
        DataSet GetActiveUpdateChangeLogs(bool ShowActiveUpdate, bool ShowActiveAlert);
        int UserUpdateChangeLogsStatus(int userId, bool isUpdateAlert, bool isAlertPopup);
        int InsertUpdateChangeLogs(UpdateChangeLog updateLog);
        DataSet GetAllUpdateChangeLogs(int updateListID);
        int DeleteUpdateChangeLogs(int updateListID);
    }
}
