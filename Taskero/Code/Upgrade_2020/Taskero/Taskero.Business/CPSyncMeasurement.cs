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
    public abstract class CPSyncMeasurement
    {
        #region Variables
        #endregion

        #region Properties
        public int PressRunID { get; set; }
        public int MeasurementID { get; set; }
        public string MeasurementIDs { get; set; }
        #endregion
    }
}
