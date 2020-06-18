using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taskero.Common;
using Taskero.DataAccess;

namespace TaskeroMobile
{
    public partial class JobList : PageBase
    {

        #region Declaration

        #endregion

        #region Page Load and Events
        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Bind Grid ==========//     
                if (IsPostBack == false)
                {
                    //======= Clear the All Session ==== //
                    Sessions.SetSessions("dtJobReportPressMob", null);
                    Sessions.SetSessions("dtJobReportPrepressMob", null);

                    Sessions.SetSessions("dtJobReportPressMobSer", null);
                    Sessions.SetSessions("dtJobReportPrepressMobSer", null);

                    PanelVisiblity(panPrepress, "Prepress");
                    FillPrepressGrid();
                    BindStatusDDL();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #region Prepress Events
        /// <summary>
        /// Change Grid Page Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IbtnPrepressBack_Click(object sender, ImageClickEventArgs e)
        {
            if (DGPrepress.CurrentPageIndex != 0)
            {
                DGPrepress.CurrentPageIndex = DGPrepress.CurrentPageIndex - 1;
                FillPrepressGrid();
            }
        }
        /// <summary>
        /// Change Grid Page Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IbtnPrepressNext_Click(object sender, ImageClickEventArgs e)
        {

            if (DGPrepress.CurrentPageIndex != DGPrepress.PageCount - 1)
            {
                DGPrepress.CurrentPageIndex = DGPrepress.CurrentPageIndex + 1;
                FillPrepressGrid();
            }
        }
        /// <summary>
        /// Prepress Tab Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IbtnPrepress_Click(object sender, ImageClickEventArgs e)
        {
            //======= Clear the Search Session ==== //
            Sessions.SetSessions("dtJobReportPressMobSer", null);
            Sessions.SetSessions("dtJobReportPrepressMobSer", null);

            //======== Bind Prepress Grid ==== //
            FillPrepressGrid();

            //======== Visible Prepress tab === //
            PanelVisiblity(panPrepress, "Prepress");


            //======= Clear All Search Field ====//
            DdlPrepressStatus.SelectedIndex = 0;
            TxtPrepressJobNo.Text = "";
            TxtPrepressDate.Text = "";
            TxtPrepressBranch.Text = "";

        }

        /// <summary>
        /// Prepress Tab search button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IbtnPrepressSearch_Click(object sender, ImageClickEventArgs e)
        {
            string strSearch = " Status <> ''"; // Search String 

            //===== Copy All Prepress Data from Session ==== //
            DataTable dtJobReportPrepress = ((DataTable)Sessions.GetSession("dtJobReportPrepressMob")).Copy();

            //==== Create Search Condition in string ===//
            if (DdlPrepressStatus.SelectedIndex > 0)
                strSearch += " And Status = '" + DdlPrepressStatus.SelectedValue.Trim() + "'";
            if (TxtPrepressJobNo.Text.Trim() != "")
                strSearch += " And Job like '%" + TxtPrepressJobNo.Text.Trim() + "%'";
            if (TxtPrepressDate.Text.Trim() != "" && Validations.IsDateTime(TxtPrepressDate.Text))
                strSearch += " And Date = '" + TxtPrepressDate.Text.Trim() + "'";
            if (TxtPrepressBranch.Text.Trim() != "")
                strSearch += " And BranchName like '%" + TxtPrepressBranch.Text.Trim() + "%'";
            if (TxtPrepressDevice.Text.Trim() != "")
                strSearch += " And CPDeviceDesc like '%" + TxtPrepressDevice.Text.Trim() + "%'";

            dtJobReportPrepress.DefaultView.RowFilter = strSearch;

            //==== Store Search Data into Search Session ===//
            Sessions.SetSessions("dtJobReportPrepressMobSer", dtJobReportPrepress.DefaultView);

            //==== Bind Prepress Grid =====//
            FillPrepressGrid();
        }
        /// <summary>
        /// Prepress Grid Item Data Bound Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DGPrepress_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (ListItemType.Item == e.Item.ItemType || ListItemType.AlternatingItem == e.Item.ItemType)
            {
                DataRowView DRV = (DataRowView)e.Item.DataItem;

                System.Web.UI.HtmlControls.HtmlImage imgStatus = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("imgStatus");
                Literal litJobName = (Literal)e.Item.FindControl("litJobName");
                Literal litTimeStamp = (Literal)e.Item.FindControl("litTimeStamp");
                Literal litDevice = (Literal)e.Item.FindControl("litDevice");
                System.Web.UI.HtmlControls.HtmlAnchor hrefPrepressDetail = (System.Web.UI.HtmlControls.HtmlAnchor)e.Item.FindControl("hrefPrepressDetail");
                hrefPrepressDetail.HRef = "JobPrepressDetail.aspx?GUID=" + DRV["GUID"].ToString();
                if (DRV["Status"].ToString() == Enums.enumStatus.Failed.ToString())
                    imgStatus.Src = "images/status_failed.png"; //========  Failed Then Red Star  ========//            
                else if (DRV["Status"].ToString() == Enums.enumStatus.Caution.ToString())
                    imgStatus.Src = "images/status_caution.png"; //========  Caution Then Yellow Star  ========//                
                else if (DRV["Status"].ToString() == Enums.enumStatus.Passed.ToString())
                    imgStatus.Src = "images/status_passed.png"; //========  Pass Then Green Star  ========//

                litJobName.Text = DRV["Job"].ToString();
                litTimeStamp.Text = DRV["DateTime"].ToString();
                litDevice.Text = DRV["CPDeviceDesc"].ToString();
            }
        }
        /// <summary>
        /// Prepress Grid Page Index Change Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void DGPrepress_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGPrepress.CurrentPageIndex = e.NewPageIndex;
            FillPrepressGrid();
        }
        #endregion

        #region Press Events
        /// <summary>
        /// Change Grid Page Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IbtnPressBack_Click(object sender, ImageClickEventArgs e)
        {
            if (DGPress.CurrentPageIndex != 0)
            {
                DGPress.CurrentPageIndex = DGPress.CurrentPageIndex - 1;
                FillPressGrid();
            }
        }
        /// <summary>
        /// Change Grid Page Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IbtnPressNext_Click(object sender, ImageClickEventArgs e)
        {

            if (DGPress.CurrentPageIndex != DGPress.PageCount - 1)
            {
                DGPress.CurrentPageIndex = DGPress.CurrentPageIndex + 1;
                FillPressGrid();
            }
        }
        /// <summary>
        /// Press Tab Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IbtnPress_Click(object sender, ImageClickEventArgs e)
        {
            //======= Clear the Search Session ==== //
            Sessions.SetSessions("dtJobReportPressMobSer", null);
            Sessions.SetSessions("dtJobReportPrepressMobSer", null);

            //======== Bind Press Grid ==== //        
            FillPressGrid();

            //======== Visible Press tab === //
            PanelVisiblity(panPress, "Press");

            //======= Clear All Search Field ====//
            DdlPressStatus.SelectedIndex = 0;
            TxtPressJobNo.Text = "";
            TxtPressWorkstation.Text = "";
            TxtPressBranch.Text = "";
        }
        /// <summary>
        /// Press Tab search button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IbtnPressSearch_Click(object sender, ImageClickEventArgs e)
        {
            string strSearch = " Status <> ''"; // Search String 

            //===== Copy All Prepress Data from Session ==== //
            DataTable dtJobReportPress = ((DataTable)Sessions.GetSession("dtJobReportPressMob")).Copy();

            //==== Create Search Condition in string ===//
            if (DdlPressStatus.SelectedIndex > 0)
                strSearch += " And Status = '" + DdlPressStatus.SelectedValue.Trim() + "'";
            if (TxtPressJobNo.Text.Trim() != "")
                strSearch += " And Job like '%" + TxtPressJobNo.Text.Trim() + "%'";
            if (TxtPressWorkstation.Text.Trim() != "")
                strSearch += " And DongleSerial like '%" + TxtPressWorkstation.Text.Trim() + "%'";
            if (TxtPressBranch.Text.Trim() != "")
                strSearch += " And BranchName like '%" + TxtPressBranch.Text.Trim() + "%'";


            dtJobReportPress.DefaultView.RowFilter = strSearch;

            //==== Store Search Data into Search Session ===//
            Sessions.SetSessions("dtJobReportPressMobSer", dtJobReportPress.DefaultView);

            //==== Bind Press Grid =====//
            FillPressGrid();


        }
        /// <summary>
        /// Press Grid Item Data Bound Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DGPress_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (ListItemType.Item == e.Item.ItemType || ListItemType.AlternatingItem == e.Item.ItemType)
            {
                DataRowView DRV = (DataRowView)e.Item.DataItem;

                System.Web.UI.HtmlControls.HtmlImage imgStatus = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("imgStatus");
                Literal litJobName = (Literal)e.Item.FindControl("litJobName");
                System.Web.UI.HtmlControls.HtmlAnchor hrefPressDetail = (System.Web.UI.HtmlControls.HtmlAnchor)e.Item.FindControl("hrefPressDetail");
                hrefPressDetail.HRef = "JobPressDetail.aspx?GUID=" + DRV["GUID"].ToString();

                if (DRV["Status"].ToString().Replace(" ", "").Trim() == Enums.enumJobStatus.MakeReady.ToString())
                    imgStatus.Src = "images/status_makeready.png"; //========  MakeReady Then Red Star  ========//            
                else if (DRV["Status"].ToString() == Enums.enumJobStatus.Production.ToString())
                    imgStatus.Src = "images/status_production.png"; //========  Production Then Yellow Star  ========//                
                else if (DRV["Status"].ToString() == Enums.enumJobStatus.Complete.ToString())
                    imgStatus.Src = "images/status_complete.png"; //========  Complete Then Green Star  ========//

                litJobName.Text = DRV["Job"].ToString();
            }

        }
        /// <summary>
        /// Press Grid Page Index Change Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void DGPress_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGPress.CurrentPageIndex = e.NewPageIndex;
            FillPressGrid();
        }
        #endregion


        #endregion

        #region Methods
        private void FillPressGrid()
        {
            try
            {
                //Response.Write("<script language='javascript'>setWidth();</script>");
                DataTable dtJobReportPress = new DataTable();


                //====  Check Session ====//

                //====  Get Data & Fill Session  ====//
                if (Sessions.GetSession("dtJobReportPressMob") == null)
                    GetData(true);

                //====  Fill Data From Session  ====// 
                if ((DataView)Sessions.GetSession("dtJobReportPressMobSer") == null) // Check if Search Session null or not
                    dtJobReportPress = (DataTable)Sessions.GetSession("dtJobReportPressMob");
                else
                    dtJobReportPress = ((DataView)Sessions.GetSession("dtJobReportPressMobSer")).ToTable();




                //====  Set Session Key  ====// 
                //ViewState["SessKey"] = DateTime.Now.ToString("yyyyMMddHHmmss");

                // ==== Fill Press Grid ====//

                //====  Set Data Source  ====//
                DGPress.DataSource = dtJobReportPress;

                //====  Bind Data Grid  ====//
                DGPress.DataBind();


                // ==== Set 100% Width of tab ==== //
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script language='javascript'>setWidth();</script>", false);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillPrepressGrid()
        {
            try
            {
                //Response.Write("<script language='javascript'>setWidth();</script>");            
                DataTable dtJobReportPrepress = new DataTable();

                //====  Check Session ====//

                //====  Get Data & Fill Session  ====//            
                if (Sessions.GetSession("dtJobReportPrepressMob") == null)
                    GetData(false);


                //====  Fill Data From Session  ====//      
                if ((DataView)Sessions.GetSession("dtJobReportPrepressMobSer") == null) // Check if Search Session null or not
                    dtJobReportPrepress = (DataTable)Sessions.GetSession("dtJobReportPrepressMob");
                else
                    dtJobReportPrepress = ((DataView)Sessions.GetSession("dtJobReportPrepressMobSer")).ToTable();

                //====  Set Session Key  ====// 
                //ViewState["SessKey"] = DateTime.Now.ToString("yyyyMMddHHmmss");

                // ==== Fill Prepress Grid ====//

                //====  Set Data Source  ====//
                DGPrepress.DataSource = dtJobReportPrepress;

                //====  Bind Data Grid  ====//
                DGPrepress.DataBind();

                // ==== Set 100% Width of tab ==== //
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script language='javascript'>setWidth();</script>", false);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void PanelVisiblity(Panel Pan, string TabName)
        {
            panPrepress.Visible = false;
            panPress.Visible = false;
            Pan.Visible = true;

            IbtnPrepress.ImageUrl = "~/images/prepress.png";
            IbtnPress.ImageUrl = "~/images/press.png";
            if (TabName == "Prepress")
                IbtnPrepress.ImageUrl = "~/images/prepress_h.png";
            else
                IbtnPress.ImageUrl = "~/images/press_h.png";

        }
        private void GetData(Boolean Press)
        {
            try
            {
                //====  Set Command For Get Data  ====//
                SqlCommand CmdJobReport = new SqlCommand();

                //====  Command Type is StoreProcedure  ====//
                CmdJobReport.CommandType = CommandType.StoredProcedure;

                if (Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin)
                {
                    //====  Set Command Text  ====//
                    CmdJobReport.CommandText = "CPV.uspGetJobReportData_Admin";

                    //===  Taskero Admin Select Any one Company ====//
                    CmdJobReport.Parameters.AddWithValue("@CompanyID", Convert.ToInt32(0));
                    CmdJobReport.Parameters.AddWithValue("@Press", Press);
                }
                else
                {
                    //====  Set Command Text  ====//
                    CmdJobReport.CommandText = "CPV.uspGetJobReportData";

                    //==== Taskero UserID  ====// 
                    CmdJobReport.Parameters.AddWithValue("@TaskeroUserID", Sessions.UserID);

                    //===  Taskero Admin Select Any one Company ====//
                    CmdJobReport.Parameters.AddWithValue("@CompanyID", Convert.ToInt32(Sessions.CompanyID));

                    CmdJobReport.Parameters.AddWithValue("@Press", Press);

                }

                //===  Fill Data in Session  ====//
                if (Press)
                    Sessions.SetSessions("dtJobReportPressMob", (new Database()).GetDataTable(CmdJobReport));
                else
                    Sessions.SetSessions("dtJobReportPrepressMob", (new Database()).GetDataTable(CmdJobReport));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void BindStatusDDL()
        {
            DdlPrepressStatus.Items.Insert(0, new ListItem("All", ""));
            DdlPrepressStatus.Items.Insert(1, new ListItem(Enums.enumStatus.Failed.ToString(), Enums.enumStatus.Failed.ToString()));
            DdlPrepressStatus.Items.Insert(2, new ListItem(Enums.enumStatus.Caution.ToString(), Enums.enumStatus.Caution.ToString()));
            DdlPrepressStatus.Items.Insert(3, new ListItem(Enums.enumStatus.Passed.ToString(), Enums.enumStatus.Passed.ToString()));

            DdlPressStatus.Items.Insert(0, new ListItem("All", ""));
            DdlPressStatus.Items.Insert(1, new ListItem(Enums.enumJobStatus.MakeReady.ToString(), "Make Ready"));
            DdlPressStatus.Items.Insert(2, new ListItem(Enums.enumJobStatus.Production.ToString(), Enums.enumJobStatus.Production.ToString()));
            DdlPressStatus.Items.Insert(3, new ListItem(Enums.enumJobStatus.Complete.ToString(), Enums.enumJobStatus.Complete.ToString()));

        }
        #endregion


    }
}