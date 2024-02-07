using System;
using System.Data;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;


namespace TaskeroWeb
{
    public partial class SyncAddPressRunPopup : PageBase
    {
        #region Declaration
        #endregion

        #region Page Load and Events
        /// <summary>
        /// Page load event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LblError.Text = "";
                if (Request.QueryString["RootID"] != null && Request.QueryString["Level"] != null)
                {
                    //==========  Declaring Objects ==========//
                    CPSyncPressRunData objCPSyncPressRun = new CPSyncPressRunData();
                    DataTable DTPressRun = new DataTable();

                    DTPressRun = objCPSyncPressRun.uspGetSyncPressRunDetailByPressRunID(Convert.ToInt32(Request.QueryString["RootID"]));
                    if (DTPressRun != null && DTPressRun.Rows.Count > 0)
                        TxtPressRunName.Text = DTPressRun.Rows[0]["PressRunName"].ToString() + "-" + Convert.ToString(Convert.ToInt32(Request.QueryString["Level"].ToString()) + 1);

                }

            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }

        /// <summary>
        /// Save press Alignment information click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BTN_SavePressRun_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Validations For the new Device inputs ==========//
                if (TxtPressRunName.Text.Trim() == "") { LblError.Text = "Plase enter Press Run Name."; return; }

                short MasterPressRunID = 0;
                if (Request.QueryString["PressRunID"] != null)
                    MasterPressRunID = Convert.ToInt16(Request.QueryString["PressRunID"].ToString());


                //==========  Declaring Objects ==========//
                CPSyncPressRunData objCPSyncPressRun = new CPSyncPressRunData();

                //========== check the device id, if not exist then insert new dongle ===========//
                objCPSyncPressRun.PressRunName = TxtPressRunName.Text.Trim();
                objCPSyncPressRun.PressID = Convert.ToInt16(Sessions.GetSession("SelectedPressID"));
                objCPSyncPressRun.Status = 1;
                objCPSyncPressRun.ΔEType = 1;
                objCPSyncPressRun.StartDateTime = Convert.ToDateTime(ClientDatatime.Value.ToString());
                objCPSyncPressRun.MasterPressRunID = MasterPressRunID;
                objCPSyncPressRun.uspInsertCPSyncPressRun();

                #region Close this popup and refresh parent press grid
                string script = "<script>";
                script += "var modalPopupBehavior = parent.$find('programmaticModalPopupBehavior');    ";
                script += "modalPopupBehavior.hide();";
                script += "parent.CallPressRunBindGrid();";
                script += "</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "windowclose", script);
                #endregion
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }

        #endregion


    }
}