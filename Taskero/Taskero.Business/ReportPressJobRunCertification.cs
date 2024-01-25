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
    public abstract class ReportPressJobRunCertification
    {
        #region Variables
        #endregion

        #region Properties
            public int JobID { get; set; }
            public string DongleID { get; set; }
            public int TaskeroUserID { get; set; }
            public int CompanyID { get; set; }
        #endregion

    }
}
