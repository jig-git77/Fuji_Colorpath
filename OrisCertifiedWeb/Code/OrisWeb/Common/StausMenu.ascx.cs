using Oris.Common;
using OrisWeb.Models;
using System;

namespace OrisWeb.Common
{
    public partial class StausMenu : ControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Check User Login ==========//
                Sessions.CheckUserLogin();

                //********* Method for the Page Right ************//
                CheckRights();

                //========== Default Settings ==========//
                if (IsPostBack == false)
                {

                    //========== Set The Active Tab of Status ==========//
                    string URL = Request.Url.ToString();
                    if (URL.IndexOf("ProofandPlates.aspx") > 0)
                    {
                        ProofandPlates.Attributes.Add("class", "active");
                        DataMonitor.Attributes.Add("class", "");

                    }
                    else if (URL.IndexOf("DataMonitor.aspx") > 0)
                    {
                        DataMonitor.Attributes.Add("class", "active");
                        ProofandPlates.Attributes.Add("class", "");

                    }

                }
            }
            catch (Exception Exc)
            {
                HandleException(Exc).ToString();
            }

        }
        #region Methods & Functions
        //********* Method for the Page Right ************//
        private void CheckRights()
        {


            if (Sessions.View_Data_Monitor == false)
            { DataMonitor.Visible = false; }
            else
            { DataMonitor.Visible = true; }
        }
        #endregion
    }
}