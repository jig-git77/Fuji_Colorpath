using System;
using System.Data;
using Taskero.Common;

namespace TaskeroMobile
{
    public partial class JobPressDetail : PageBase
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
            DataTable dtJobReportPress = new DataTable();
            dtJobReportPress = ((DataTable)Sessions.GetSession("dtJobReportPressMob")).Copy();

            if (dtJobReportPress != null && dtJobReportPress.Rows.Count > 0)
            {
                dtJobReportPress.DefaultView.RowFilter = "GUID ='" + GUID + "'";
                if (dtJobReportPress.DefaultView.Count > 0)
                {
                    if (dtJobReportPress.DefaultView[0]["Status"].ToString().Replace(" ", "").Trim() == Enums.enumJobStatus.MakeReady.ToString())
                        imgStatus.Src = "images/status_makeready.png"; //========  MakeReady Then Red Star  ========//            
                    else if (dtJobReportPress.DefaultView[0]["Status"].ToString() == Enums.enumJobStatus.Production.ToString())
                        imgStatus.Src = "images/status_production.png"; //========  Production Then Yellow Star  ========//                
                    else if (dtJobReportPress.DefaultView[0]["Status"].ToString() == Enums.enumJobStatus.Complete.ToString())
                        imgStatus.Src = "images/status_complete.png"; //========  Complete Then Green Star  ========//

                    LblJobNo.Text = Convert.ToString(dtJobReportPress.DefaultView[0]["Job"]);
                    LblFirstSheetDate.Text = Convert.ToString(dtJobReportPress.DefaultView[0]["FirstSheet"]);
                    LblLastSheetDate.Text = Convert.ToString(dtJobReportPress.DefaultView[0]["LastSheet"]);
                    LblDevice.Text = Convert.ToString(dtJobReportPress.DefaultView[0]["CPDeviceDesc"]);
                    LblWorkStation.Text = Convert.ToString(dtJobReportPress.DefaultView[0]["DongleSerial"]);
                    LblBranchName.Text = Convert.ToString(dtJobReportPress.DefaultView[0]["BranchName"]);
                }
            }
        }
        #endregion
    }
}