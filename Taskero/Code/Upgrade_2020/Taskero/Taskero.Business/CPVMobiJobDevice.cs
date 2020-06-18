using System;
using System.Collections.Generic;
using System.Text;

namespace Taskero.Business
{
    public abstract class CPVMobiJobDevice
    {
        #region Variables
        #endregion

        #region Properties
        public string CPMeasurementGUID { get; set; }
        public bool PrintTarget { get; set; }
        #endregion
    }
}
