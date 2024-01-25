using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Taskero.Business;

namespace TaskeroMobile
{
    public class MasterBase:MasterPage
    {
        #region Constructor
        public MasterBase()
        {

        }
        #endregion

        #region Exection Handling
        public string HandleException(Exception Exc)
        {
            return (new ErrorLogData()).HandleException(Exc);
        }
        #endregion
    }
}