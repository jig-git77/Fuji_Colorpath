#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Taskero.DataAccess;
#endregion


namespace Taskero.Business
{
    public abstract class ReportPressMakeReady
    {
        #region Variables
        private int _MakeReadyReportID = -1;
        #endregion

        #region Properties
        public int TaskeroUserID { get; set; }
        public int CompanyID { get; set; }
        public int RoleID { get; set; }
        public string BranchID { get; set; }
        public string CPDeviceID { get; set; }
        public string JobName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MakeReadyReportID
        {
            get { return _MakeReadyReportID; }
            set { _MakeReadyReportID = value; }
        }
        #endregion
    }
}
