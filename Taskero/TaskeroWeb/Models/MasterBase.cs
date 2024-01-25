using System;
using System.Web.UI;
using Taskero.Business;

namespace TaskeroWeb.Models
{
    public class MasterBase : MasterPage
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