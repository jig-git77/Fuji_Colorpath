using Oris.Business;
using System;
using System.Web.UI;


namespace OrisWeb.Models
{
    /// <summary>
    /// Summary description for ControlBase
    /// </summary>
    public class ControlBase : UserControl
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