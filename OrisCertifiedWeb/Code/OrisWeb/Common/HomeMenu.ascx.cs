using Oris.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrisWeb.Common
{
    public partial class HomeMenu : System.Web.UI.UserControl
    {
        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            //********* Calling the Method for Applying the Page Right ************//
            CheckRights();

        }
        #endregion

        #region Methods & Functions
        //********* Method for the Page Right ************//
        private void CheckRights()
        {
            //********* Setting Right for Job Tab************//
            if (Sessions.View_Jobs_Tab == false)
            { TDJob.Visible = false; TDJobSpacer.Visible = false; }
            else
            { TDJob.Visible = true; TDJobSpacer.Visible = true; }

            if (Sessions.View_Devices_Tab == false)
            { TDDevice.Visible = false; TDDeviceSpacer.Visible = false; }
            else
            { TDDevice.Visible = true; TDDeviceSpacer.Visible = true; }

            if (Sessions.View_Device_Status == false)
            { TdStatus.Visible = false; TDDeviceSpacer.Visible = false; }
            else
            { TdStatus.Visible = true; }

            if (Sessions.RoleDesc == Enums.RoleType.PrintBuyer)
            {
                TDJobSpacer.Visible = false; TDDevice.Visible = false;
                TDDeviceSpacer.Visible = false; TdStatus.Visible = false;
            }

        }
        #endregion
    }
}