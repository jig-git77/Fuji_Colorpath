using Oris.Business;
using System;
using System.Web.UI;


namespace OrisWeb.Models
{
    /// <summary>
    /// Summary description for ControlBase
    /// </summary>
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