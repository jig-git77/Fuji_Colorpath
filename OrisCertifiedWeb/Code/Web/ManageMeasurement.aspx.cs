#region System
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
#endregion

#region Others
using System.Data.SqlClient;
using Fuji.Common;
using Telerik.WebControls;
using Fuji.Business;
#endregion

public partial class ManageMeasurement : PageBase
{
    #region Declaration

    //====  This is set for Default Page Size ====//
    private const int _DefaultPageSize = 25;
    //====  Set Grid Column Number ====//
    private enum enumGridColumn
    {
        DeleteCheckBox = 2,
        CompanyName = 3,
        BranchName = 4,
        DongleDescription = 5,
        DeviceDesc = 6,
        DateTime = 7,
        GMTDateTime = 8,
        Date = 9,
        TIME = 10,
        Status = 11,
        hdnSatus = 12,
        View = 13,
        Job = 14,
        PrintTarget = 15,
        DeviceType = 16,
        GUID = 17,
        CPMeasurementID = 18,
        CompanyID = 19,
        BranchID = 20,
        DongleID = 21,
        DeviceID = 22,
        CPDevicePrintTargetID = 23,
        Customer = 24,
        CPDeviceTypeID = 25,
        StatusID = 26

    }

    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Error ==========//   
            LblError.Text = "";

            //========== Sets Default Labels ==========//   
            SetLabels();

            //========== Bind Grid ==========//     
            if (IsPostBack == false)
            {
                //========== Check Login ==========//    
                Sessions.CheckUserLogin();

                //====  Get Data & Fill Session  ====//
                GetData();

                //========= Clear the date for first time =========//
                Fuji.Common.Sessions.SetSessions("StartDate", null);
                ViewState["ARG"] = null;
                //========== Check Login ==========//
                FillGrid();
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void GridJobReport_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        try
        {
            //========  Clear State ========//
            ViewState["SessKey"] = DateTime.Now.ToString("yyyyMMddHHmmss");

            //========  Check Session  ========//
            if (Fuji.Common.Sessions.GetSession("dtJobReport") != null)
            {
                //========  Session Store in DataTable  ========//
                DataTable dtJobReport = (DataTable)Fuji.Common.Sessions.GetSession("dtJobReport");

                //========  Grid Bind  ========//
                GridJobReport.DataSource = dtJobReport;


            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void GridJobReport_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
            {
                #region ====  Set Pager Style ====

                //====  Set Label of Record Text  ====//
                Literal LTRL_RecordText = (Literal)e.Item.FindControl("LTRL_RecordText");

                //==== Varible Initiate  ====//
                int _FirstIndexInPage = 0, _LastIndexInPage = 0, _DataSourceCount = 0;

                //====  Assign Values ====//
                _FirstIndexInPage = GridJobReport.MasterTableView.PagingManager.FirstIndexInPage;
                _LastIndexInPage = GridJobReport.MasterTableView.PagingManager.LastIndexInPage + 1;
                _DataSourceCount = GridJobReport.MasterTableView.PagingManager.DataSourceCount;

                //====  When Record more then 1  ====//
                if (_DataSourceCount > 0)
                {
                    _FirstIndexInPage++;
                }

                //====  Fill Value in Record Label  ====//
                LTRL_RecordText.Text = "Record(s) : " + _FirstIndexInPage + " to " + _LastIndexInPage + " of " + _DataSourceCount + " | ";

                //==== Create Objects For First, Last, Prev, Next Button  ====//
                ImageButton IBTN_First = (ImageButton)e.Item.FindControl("IBTN_First");
                ImageButton IBTN_Prev = (ImageButton)e.Item.FindControl("IBTN_Prev");
                ImageButton IBTN_Next = (ImageButton)e.Item.FindControl("IBTN_Next");
                ImageButton IBTN_Last = (ImageButton)e.Item.FindControl("IBTN_Last");

                //====  Here when Click on First then Set Images means ====//
                //====  Which i enable and which disable  ====//
                if (GridJobReport.MasterTableView.PagingManager.IsFirstPage)
                {
                    IBTN_First.ImageUrl = "Images/Nav_FirstPage_off.gif";
                    IBTN_Prev.ImageUrl = "Images/Nav_PreviousPage_off.gif";
                    IBTN_First.Style.Add("cursor", "");
                    IBTN_Prev.Style.Add("cursor", "");
                    IBTN_First.Attributes.Add("disabled", "true");
                    IBTN_Prev.Attributes.Add("disabled", "true");
                }
                else
                {
                    IBTN_First.ImageUrl = "Images/Nav_FirstPage.gif";
                    IBTN_Prev.ImageUrl = "Images/Nav_PreviousPage.gif";
                    IBTN_First.Style.Add("cursor", "pointer");
                    IBTN_Prev.Style.Add("cursor", "pointer");
                }

                //====  Here when Click on Last then Set Images means ====//
                if (GridJobReport.MasterTableView.PagingManager.IsLastPage == true)
                {
                    IBTN_Next.ImageUrl = "Images/Nav_NextPage_off.gif";
                    IBTN_Last.ImageUrl = "Images/Nav_LastPage_off.gif";
                    IBTN_Next.Style.Add("cursor", "");
                    IBTN_Last.Style.Add("cursor", "");
                    IBTN_Next.Attributes.Add("disabled", "true");
                    IBTN_Last.Attributes.Add("disabled", "true");
                }
                else
                {
                    IBTN_Next.ImageUrl = "Images/Nav_NextPage.gif";
                    IBTN_Last.ImageUrl = "Images/Nav_LastPage.gif";
                    IBTN_Next.Style.Add("cursor", "pointer");
                    IBTN_Last.Style.Add("cursor", "pointer");
                }
                #endregion

                //========  Initiate Object For Delete Button  ========//
                Button BTN_Delete = (Button)e.Item.FindControl("btnDelete");
                BTN_Delete.Attributes.Add("onClick", "return CheckCheckBoxes('cbSelect')");

            }

            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Header)
            {
                CheckBox cbSelectAll = (CheckBox)e.Item.FindControl("cbSelectAll");
                cbSelectAll.Attributes.Add("onClick", "CheckAllDataGridCheckBoxes('" + cbSelectAll.ClientID + "','cbSelect')");
            }

            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Item || e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem)
            {
                #region ====  Status Settings  ====

                //========  Initiate Object For Status Image  ========//
                System.Web.UI.HtmlControls.HtmlImage ibtnStatus;
                ibtnStatus = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("ibtnStatus");

                if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim() == Fuji.Common.Enums.enumStatus.Failed.ToString())
                {
                    //========  Set Status Image Path And Title  ========//
                    ibtnStatus.Src = "Images/RedStar.gif"; //========  Failed Then Red Star  ========//
                    ibtnStatus.Attributes.Add("title", Fuji.Common.Enums.enumStatus.Failed.ToString());
                }
                else if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim() == Fuji.Common.Enums.enumStatus.Caution.ToString())
                {
                    //========  Set Status Image Path And Title  ========//
                    ibtnStatus.Src = "Images/YellowStar.gif";//========  Caution Then Yellow Star  ========//
                    ibtnStatus.Attributes.Add("title", Fuji.Common.Enums.enumStatus.Caution.ToString());
                }
                else if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim() == Fuji.Common.Enums.enumStatus.Passed.ToString())
                {
                    //========  Set Status Image Path And Title  ========//
                    ibtnStatus.Src = "Images/GreenStar.gif";//========  Passed Then Green Star  ========//
                    ibtnStatus.Attributes.Add("title", Fuji.Common.Enums.enumStatus.Passed.ToString());
                }

                #endregion

                CheckBox cbSelect = (CheckBox)e.Item.FindControl("cbSelect");
                string GUIDs = e.Item.Cells[(int)enumGridColumn.GUID].Text.Trim();
                cbSelect.Attributes.Add("KEY", GUIDs);

                #region ====  View Setting  ====

                string _MeasurementStr = "";
                //========  Bind String For QueryString's Parameters ========//
                if ((Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Color))
                   || (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Monitor))
                   || (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.WideFormat)))
                {
                    _MeasurementStr = "DataHeaderColor.aspx";
                }
                else if (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Plate))
                {
                    _MeasurementStr = "DataHeaderPlate.aspx";
                }

                _MeasurementStr += "?CPMeasurementGUID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.GUID)].Text ;

                //========  Initiate Object For View Image  ========//
                System.Web.UI.HtmlControls.HtmlImage ImgView;
                ImgView = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("ImgView");

                System.Web.UI.HtmlControls.HtmlImage ImgView_New;
                ImgView_New = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("ImgView_New");

                string _NewId = e.Item.Cells[Convert.ToInt32(enumGridColumn.GUID)].Text.Replace("-", "");

                if ((Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Color))
                    || (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Monitor))
                    || (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.WideFormat))
                    || (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Plate)))
                {
                    //========  if CPDeviceType = "Color" then Open popup for Detail  ========//
                    ImgView.Attributes.Add("onclick", "OpenMeasurementWindow('" + _MeasurementStr + "', true, false , '" + _NewId + "');");
                    ImgView_New.Attributes.Add("onclick", "OpenMeasurementWindow('" + _MeasurementStr + "', true, true,'" + _NewId + "');");
                }
                else
                {
                    //========  Show Message  ========//
                    ImgView.Attributes.Add("onclick", "OpenMeasurementWindow('" + _MeasurementStr + "', false, false,'" + _NewId + "');");
                    ImgView_New.Attributes.Add("onclick", "OpenMeasurementWindow('" + _MeasurementStr + "', false, false,'" + _NewId + "');");
                }
                ImgView.Visible = false;
                #endregion
            }
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.FilteringItem)
            {
                //=========== Check whether the view state for the date picker is null or not ==========//
                if(ViewState["ARG"] !=null && ViewState["ARG"].ToString().Length != 0)
                {
                    //=========== find the cell from the grid ==========//
                    TableCell cell1 = ((Telerik.WebControls.GridFilteringItem)e.Item)[ViewState["ARG"].ToString()];
                    //=========== Find the panel from the cell ==========//
                    Panel Pnl_Date_Picker1 = (Panel)cell1.Controls[0].FindControl("PNL_DATE_PICKER");
                    //=========== find the date picker from the panel and assign the value of the date picker ==========//
                    RadDatePicker RDP_Date1 =(RadDatePicker)Pnl_Date_Picker1.FindControl("RadDatePicker1");
                    RDP_Date1.SelectedDate = null;
                }
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void GridJobReport_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "SelectDate")
            {
                //=========== Find the cell, panel and date picker from the cell ==========//
                TableCell cell = ((Telerik.WebControls.GridFilteringItem)e.Item)[e.CommandArgument.ToString()];
                Panel Pnl_Date_Picker = (Panel)cell.Controls[0].FindControl("PNL_DATE_PICKER");
                RadDatePicker RDP_Date = (RadDatePicker)Pnl_Date_Picker.FindControl("RadDatePicker1");
                //=========== Set the selected date of the date picker to session ==========//
                Fuji.Common.Sessions.SetSessions("StartDate", RDP_Date.SelectedDate);
                //=========== Set the view state value of the unique name ==========//
                if (e.CommandArgument.ToString().Equals("DateTime"))
                    ViewState["ARG"] = "GMTDateTime";
                else
                    ViewState["ARG"] = "DateTime";
                //e.Item.OwnerTableView.FilterExpression = String.Format("DateTime > '{0}'", RDP_Date.SelectedDate.ToString());
                e.Item.OwnerTableView.FilterExpression = String.Format(e.CommandArgument.ToString() + " > '{0}' AND " + e.CommandArgument.ToString() + " < '{1}'", RDP_Date.SelectedDate.ToString(), Convert.ToDateTime(RDP_Date.SelectedDate.ToString()).AddDays(1).ToString());
                e.Item.OwnerTableView.Rebind();
            }
            else if (e.CommandName == "Delete")
            {
                doDelete(GridJobReport);

                //====  Get Data & Fill Session  ====//
                GetData();

                //========== Check Login ==========//
                FillGrid();

            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void GridJobReport_ItemCreated(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
            {
                //====  Initiate the object for Record DropDown  ====//
                DropDownList DDL_RecordPage = (DropDownList)e.Item.FindControl("DDL_RecordPage");

                if (DDL_RecordPage != null)
                {
                    //====  Set Event Handler ====//
                    DDL_RecordPage.SelectedIndexChanged += new EventHandler(DDlRecordPage_SelectedIndexChanged);

                    if (!IsPostBack)
                    {
                        //====  Set Default Value  ====//
                        DDL_RecordPage.SelectedValue = _DefaultPageSize.ToString();
                    }
                    else
                    {
                        if (ViewState["RecordPerPage"] != null)
                        {
                            //==== Find How many Records Show per Page and show it ====//
                            DDL_RecordPage.SelectedIndex = -1;
                            DDL_RecordPage.Items.FindByValue(ViewState["RecordPerPage"].ToString()).Selected = true;
                        }
                    }
                }

                //====  For Reset Filter Option  ====//
                Button BTN_Reset = (Button)e.Item.FindControl("BTN_Reset");
                if (BTN_Reset != null)
                {
                    //====  Set Event Handler  ====//
                    BTN_Reset.Click += new EventHandler(btnReset_Click);
                }

            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    private void DDlRecordPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            string _SelectedValue;
            _SelectedValue = ((DropDownList)sender).SelectedItem.Value;
            ViewState["RecordPerPage"] = _SelectedValue;

            //====  Set No of Record Per Page  ====//
            GridJobReport.PageSize = Convert.ToInt32(_SelectedValue);
            GridJobReport.CurrentPageIndex = 0;
            //====  Check Session of Data  ====//
            if (Fuji.Common.Sessions.GetSession("dtJobReport") != null)
            {
                DataTable Dt = (DataTable)Fuji.Common.Sessions.GetSession("dtJobReport");
                GridJobReport.DataSource = Dt;
            }

            //====  Bind Grid  ====//
            GridJobReport.DataBind();


        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    private void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            //====  Clear Filter Controls  ====//
            foreach (Telerik.WebControls.GridColumn column in GridJobReport.MasterTableView.Columns)
            {
                column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                column.CurrentFilterValue = String.Empty;
            }
            Fuji.Common.Sessions.SetSessions("StartDate", null);
            ViewState["ARG"] = null;
            //====  Set Filter Empty  ====//
            GridJobReport.MasterTableView.FilterExpression = String.Empty;

            //====  Rebind Master View  ====//
            GridJobReport.MasterTableView.Rebind();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    #endregion

    #region User Functions

    private void SetLabels()
    {
        try
        {
            //========== Set Culture ==========//
            Resources.Message.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));
            Resources.MasterCompany.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));

            //========== Set Values ==========//
            LTRL_Title.Text = Resources.ManageMeasurement.LBL_Manage_Measurement_Title;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void FillGrid()
    {
        try
        {
            DataTable dtJobReport = new DataTable();

            //====  Check Session ====//

            if (Fuji.Common.Sessions.GetSession("dtJobReport") == null)
            {
                //====  Get Data & Fill Session  ====//
                GetData();
            }

            //====  Fill Data From Session  ====// 
            dtJobReport = (DataTable)Fuji.Common.Sessions.GetSession("dtJobReport");

            //====  Set Session Key  ====// 
            //ViewState["SessKey"] = DateTime.Now.ToString("yyyyMMddHHmmss");

            //====  Set Data Source  ====//
            GridJobReport.DataSource = dtJobReport;

            //====  Bind Data Grid  ====//
            GridJobReport.DataBind();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void GetData()
    {
        try
        {
            //========== Declarations ==========//
            CPMeasurementData objCPMeasurement = new CPMeasurementData();
            Fuji.Common.Sessions.SetSessions("dtJobReport", objCPMeasurement.uspGetJobReportData_Admin(0));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void doDelete(Telerik.WebControls.RadGrid GridJobReport)
    {
        try
        {
            string GUIDs = "";

            foreach (Telerik.WebControls.GridItem gItem in GridJobReport.Items)
            {
                if (gItem.ItemType == Telerik.WebControls.GridItemType.Item || gItem.ItemType == Telerik.WebControls.GridItemType.AlternatingItem)
                {
                    CheckBox cbSelect = (CheckBox)gItem.FindControl("cbSelect");
                    if (cbSelect != null)
                    {
                        if (cbSelect.Checked)
                        {
                            if (GUIDs != "")
                            {
                                GUIDs += ",";
                            }
                            GUIDs += cbSelect.Attributes["KEY"].ToString();
                        }
                    }
                }
            }

            if (GUIDs.Trim() != "")
            {

                //========== Declarations ==========//
                CPMeasurementData objCPMeasurement = new CPMeasurementData();
                objCPMeasurement.CPMeasurementGUID = GUIDs;
                objCPMeasurement.uspDeleteMeasurementByGUIDs();
                LblError.Text = "Mesurement deleted successfully !";
            }

        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }

    #endregion
}

