using System;
using Taskero.Business;
namespace TaskeroWeb.Models
{
    public class ControlBase : System.Web.UI.UserControl
    {
        #region Constructor
        public ControlBase()
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