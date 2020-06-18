using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taskero.Common;

namespace TaskeroMobile
{
    public partial class DeviceDetail : PageBase
    {
        #region Page Load and Events
        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["GUID"] != null && Convert.ToString(Request.QueryString["GUID"]) != "")
                FillData(Convert.ToString(Request.QueryString["GUID"]));
        }
        #endregion

        #region Methods
        private void FillData(string GUID)
        {
            DataTable dtJobReportDevice = new DataTable();
            dtJobReportDevice = ((DataTable)Sessions.GetSession("dtDevicesPrepressMob")).Copy();

            if (dtJobReportDevice != null && dtJobReportDevice.Rows.Count > 0)
            {
                dtJobReportDevice.DefaultView.RowFilter = "GUID ='" + GUID + "'";

                if (dtJobReportDevice.DefaultView.Count > 0)
                {

                    if (dtJobReportDevice.DefaultView[0]["Status"].ToString() == Enums.enumDeviceStatus.Green.ToString())
                        imgStatus.Src = "images/status_green-lamp.gif"; //========  Failed Then Red Star  ========//            
                    else if (dtJobReportDevice.DefaultView[0]["Status"].ToString() == Enums.enumDeviceStatus.Yellow.ToString())
                        imgStatus.Src = "images/status_yellow-lamp.gif"; //========  Caution Then Yellow Star  ========//                
                    else if (dtJobReportDevice.DefaultView[0]["Status"].ToString() == Enums.enumDeviceStatus.Orange.ToString())
                        imgStatus.Src = "images/status_orange-lamp.gif"; //========  Pass Then Green Star  ========//
                    else if (dtJobReportDevice.DefaultView[0]["Status"].ToString() == Enums.enumDeviceStatus.Red.ToString())
                        imgStatus.Src = "images/status_red-lamp.gif"; //========  Pass Then Green Star  ========//               

                    LblDevice.Text = Convert.ToString(dtJobReportDevice.DefaultView[0]["CPDeviceDesc"]);
                    LblWorkStation.Text = Convert.ToString(dtJobReportDevice.DefaultView[0]["DongleSerial"]);
                    LblBranchName.Text = Convert.ToString(dtJobReportDevice.DefaultView[0]["BranchName"]);
                    LblDeviceType.Text = Convert.ToString(dtJobReportDevice.DefaultView[0]["CPDeviceTypeDesc"]);
                    LblDate.Text = Convert.ToString(dtJobReportDevice.DefaultView[0]["DateTime"]);
                }
            }




        }
        #endregion
    }
}