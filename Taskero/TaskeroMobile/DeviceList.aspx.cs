#region
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taskero.Business;
#endregion

#region Others
using Taskero.Common;
#endregion

namespace TaskeroMobile
{
    public partial class DeviceList : System.Web.UI.Page
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
                    //========= Clear the date for first time =========//
                    Sessions.SetSessions("dtDevicesPrepressMob", null);
                    Sessions.SetSessions("dtDevicesPrepressMobSer", null);
                    FillGrid();
                    BindStatusDDL();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Change Grid Page Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IbtnDeviceBack_Click(object sender, ImageClickEventArgs e)
        {
            if (DGDevices.CurrentPageIndex != 0)
            {
                DGDevices.CurrentPageIndex = DGDevices.CurrentPageIndex - 1;
                FillGrid();
            }
        }
        /// <summary>
        /// Change Grid Page Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IbtnDeviceNext_Click(object sender, ImageClickEventArgs e)
        {

            if (DGDevices.CurrentPageIndex != DGDevices.PageCount - 1)
            {
                DGDevices.CurrentPageIndex = DGDevices.CurrentPageIndex + 1;
                FillGrid();
            }
        }
        /// <summary>
        /// Device Tab Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IbtnDevice_Click(object sender, ImageClickEventArgs e)
        {
            //======= Clear the Search Session ==== //
            Sessions.SetSessions("dtDevicesPrepressMobSer", null);


            //======== Bind Device Grid ==== //
            FillGrid();

            //======= Clear All Search Field ====//
            DdlStatus.SelectedIndex = 0;
            TxtDevice.Text = "";
            TxtWorkstation.Text = "";
            TxtBranch.Text = "";

        }

        /// <summary>
        /// Device Tab search button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            string strSearch = " Status <> ''"; // Search String 

            //===== Copy All Device Data from Session ==== //
            DataTable dtDevice = ((DataTable)Sessions.GetSession("dtDevicesPrepressMob")).Copy();

            //==== Create Search Condition in string ===//
            if (DdlStatus.SelectedIndex > 0)
                strSearch += " And Status = '" + DdlStatus.SelectedValue.Trim() + "'";
            if (TxtDevice.Text.Trim() != "")
                strSearch += " And CPDeviceDesc like '%" + TxtDevice.Text.Trim() + "%'";
            if (TxtWorkstation.Text.Trim() != "")
                strSearch += " And DongleSerial like '%" + TxtWorkstation.Text.Trim() + "%'";
            if (TxtBranch.Text.Trim() != "")
                strSearch += " And BranchName like '%" + TxtBranch.Text.Trim() + "%'";

            dtDevice.DefaultView.RowFilter = strSearch;

            //==== Store Search Data into Search Session ===//
            Sessions.SetSessions("dtDevicesPrepressMobSer", dtDevice.DefaultView);

            //==== Bind Device Grid =====//
            FillGrid();
        }
        /// <summary>
        /// Device Grid Item Data Bound Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DGDevices_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (ListItemType.Item == e.Item.ItemType || ListItemType.AlternatingItem == e.Item.ItemType)
            {
                DataRowView DRV = (DataRowView)e.Item.DataItem;

                System.Web.UI.HtmlControls.HtmlImage imgStatus = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("imgStatus");
                Literal litDeviceName = (Literal)e.Item.FindControl("litDeviceName");
                System.Web.UI.HtmlControls.HtmlAnchor hrefDeviceDetail = (System.Web.UI.HtmlControls.HtmlAnchor)e.Item.FindControl("hrefDeviceDetail");
                hrefDeviceDetail.HRef = "DeviceDetail.aspx?GUID=" + DRV["GUID"].ToString();

                if (DRV["Status"].ToString() == Enums.enumDeviceStatus.Green.ToString())
                    imgStatus.Src = "images/status_green-lamp.gif"; //========  Failed Then Red Star  ========//            
                else if (DRV["Status"].ToString() == Enums.enumDeviceStatus.Yellow.ToString())
                    imgStatus.Src = "images/status_yellow-lamp.gif"; //========  Caution Then Yellow Star  ========//                
                else if (DRV["Status"].ToString() == Enums.enumDeviceStatus.Orange.ToString())
                    imgStatus.Src = "images/status_orange-lamp.gif"; //========  Pass Then Green Star  ========//
                else if (DRV["Status"].ToString() == Enums.enumDeviceStatus.Red.ToString())
                    imgStatus.Src = "images/status_red-lamp.gif"; //========  Pass Then Green Star  ========//

                litDeviceName.Text = DRV["CPDeviceDesc"].ToString();
            }
        }
        /// <summary>
        /// Device Grid Page Index Change Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void DGDevices_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGDevices.CurrentPageIndex = e.NewPageIndex;
            FillGrid();
        }
        #endregion

        #region Methods
        private void FillGrid()
        {
            try
            {
                DataTable dtDevicesPress = new DataTable();
                DataTable dtDevicesPrepress = new DataTable();

                //====  Check Session ====//
                //====  Get Data & Fill Session  ====//        
                if (Sessions.GetSession("dtDevicesPrepressMob") == null)
                    GetData(false);

                //====  Fill Data From Session  ====//      

                if ((DataView)Sessions.GetSession("dtDevicesPrepressMobSer") == null) // Check if Search Session null or not
                    dtDevicesPrepress = (DataTable)Sessions.GetSession("dtDevicesPrepressMob");
                else
                    dtDevicesPrepress = ((DataView)Sessions.GetSession("dtDevicesPrepressMobSer")).ToTable();





                //==== Bind Prepress Grid  =====//
                //====  Set Data Source  ====//
                DGDevices.DataSource = dtDevicesPrepress;

                //====  Bind Data Grid  ====//
                DGDevices.DataBind();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void GetData(bool Press)
        {
            try
            {
                //========== Declarations ==========//
                CPMeasurementData objCPMeasurement = new CPMeasurementData();
                if (Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin)
                    Sessions.SetSessions("dtDevicesPrepressMob", objCPMeasurement.uspGetDeviceReportData_Admin(Convert.ToInt32(0), Press));
                else
                    Sessions.SetSessions("dtDevicesPrepressMob", objCPMeasurement.uspGetDeviceReportData(Convert.ToInt32(Sessions.CompanyID), Sessions.UserID, Press));

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void BindStatusDDL()
        {
            DdlStatus.Items.Insert(0, new ListItem("All", ""));
            DdlStatus.Items.Insert(1, new ListItem(Enums.enumDeviceStatus.Green.ToString(), Enums.enumDeviceStatus.Green.ToString()));
            DdlStatus.Items.Insert(2, new ListItem(Enums.enumDeviceStatus.Yellow.ToString(), Enums.enumDeviceStatus.Yellow.ToString()));
            DdlStatus.Items.Insert(3, new ListItem(Enums.enumDeviceStatus.Orange.ToString(), Enums.enumDeviceStatus.Orange.ToString()));
            DdlStatus.Items.Insert(4, new ListItem(Enums.enumDeviceStatus.Red.ToString(), Enums.enumDeviceStatus.Red.ToString()));



        }
        #endregion
    }
}