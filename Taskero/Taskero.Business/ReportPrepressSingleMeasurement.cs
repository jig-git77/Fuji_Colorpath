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
    public abstract class ReportPrepressSingleMeasurement
    {
        #region Variables
        #endregion

        #region Properties        
        public string CPMeasurementGUID { get; set; }        
        #endregion
    }
}
