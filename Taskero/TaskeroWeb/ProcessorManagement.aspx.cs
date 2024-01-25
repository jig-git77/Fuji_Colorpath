using System;
using System.Data;
using System.Web.UI.WebControls;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;
using Telerik.WebControls;


namespace TaskeroWeb
{
    public partial class ProcessorManagement : PageBase
    {
        //====  This is set for Default Page Size ====//
        private const int _DefaultPageSize = 25;

        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Bind Grid ==========//     
                if (IsPostBack == false)
                {
                    #region ====  Bind Company List  ====

                    DataTable dtCompany = new DataTable();

                    //===  For Show All as a text in DropDwon  ====
                    ListItem DDLiShowAll = new ListItem();
                    DDLiShowAll.Text = " - - -All - - -";
                    DDLiShowAll.Value = "0";

                    //==== Add All Text in DropDwon List ====//
                    DDL_CompanyList.Items.Add(DDLiShowAll);

                    //====  Get Data For All Company List  ====//
                    dtCompany = (new CompanyData().uspGetAllCompanyByUserID(Sessions.UserID, Sessions.RoleID));
                    if (dtCompany != null && dtCompany.Rows.Count > 0)
                    {
                        DDL_CompanyList.DataTextField = "CompanyName";
                        DDL_CompanyList.DataValueField = "CompanyID";
                        DDL_CompanyList.DataSource = dtCompany;
                        DDL_CompanyList.DataBind();
                        DDL_CompanyList.Items.Insert(0, DDLiShowAll);
                    }

                    ////==== Create Object of List Itme  ====//
                    //ListItem DDLItem;

                    //if (dtCompany != null)
                    //{
                    //    for (int i = 0; i < dtCompany.Rows.Count; i++)
                    //    {
                    //        //==== Create List Item  ====//
                    //        DDLItem = new ListItem();

                    //        //====  Set Company Name as a Text  ====//
                    //        DDLItem.Text = dtCompany.Rows[i]["CompanyName"].ToString();

                    //        //====  Set Company Id as a Value  ====//
                    //        DDLItem.Value = dtCompany.Rows[i]["CompanyID"].ToString();

                    //        //====  Add List Item In DropDown  ====//
                    //        DDL_CompanyList.Items.Add(DDLItem);

                    //    }
                    //}

                    #endregion

                    #region Set Panel


                    #endregion

                    //========== Check Login ==========//    
                    Sessions.CheckUserLogin();

                    //====  Get Data & Fill Session  ====//
                    //GetData();
                    //========= Clear the date for first time =========//
                    Sessions.SetSessions("StartDate", null);
                    Sessions.SetSessions("StartDate2", null);

                    //Page.RegisterClientScriptBlock("onload", "<script>alert('ss')<script>");

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #region Event Prepress Grid
        protected void GridJobReportPrepress_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            try
            {
                FillGrid();
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        protected void GridJobReportPrepress_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
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
                    _FirstIndexInPage = GridJobReportPrepress.MasterTableView.PagingManager.FirstIndexInPage;
                    _LastIndexInPage = GridJobReportPrepress.MasterTableView.PagingManager.LastIndexInPage + 1;
                    _DataSourceCount = GridJobReportPrepress.MasterTableView.PagingManager.DataSourceCount;

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
                    if (GridJobReportPrepress.MasterTableView.PagingManager.IsFirstPage)
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
                    if (GridJobReportPrepress.MasterTableView.PagingManager.IsLastPage == true)
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

                }

                if (e.Item.ItemType == Telerik.WebControls.GridItemType.Item || e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem)
                {

                    #region ====  View Setting  ====

                    string _MeasurementStr = "";
                    string strDongleID = ((Label)e.Item.FindControl("lblDongleID")).Text;
                    string strZProcID = ((Label)e.Item.FindControl("lblZProcID")).Text;
                    string strLocation = ((Label)e.Item.FindControl("lblLocation")).Text;
                    string strDevice = ((Label)e.Item.FindControl("lblDevice")).Text;

                    _MeasurementStr = "ProcessorTrend.aspx?DongleID=" + strDongleID + "&ZProcID=" + strZProcID + "&Location=" + strLocation + "&Device=" + strDevice;

                    //========  Initiate Object For View Image  ========//
                    System.Web.UI.HtmlControls.HtmlImage ImgView;
                    ImgView = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("ImgView");

                    System.Web.UI.HtmlControls.HtmlImage ImgView_New;
                    ImgView_New = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("ImgView_New");

                    string _NewId = "1";
                    //_NewId = e.Item.Cells[Convert.ToInt32(enumGridColumn.GUID)].Text.Replace("-", "");

                    //========  if CPDeviceType = "Color" then Open popup for Detail  ========//
                    ImgView.Attributes.Add("onclick", "OpenMeasurementWindow('" + _MeasurementStr + "&ModalDialog=Y', true, false , '" + _NewId + "');");
                    ImgView_New.Attributes.Add("onclick", "OpenMeasurementWindow('" + _MeasurementStr + "', true, true,'" + _NewId + "');");

                    #endregion

                }
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        protected void GridJobReportPrepress_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SelectDate")
                {
                    TableCell cell = ((Telerik.WebControls.GridFilteringItem)e.Item)["DateTime"];
                    Panel Pnl_Date_Picker = (Panel)cell.Controls[0].FindControl("PNL_DATE_PICKER");
                    RadDatePicker RDP_Date = (RadDatePicker)Pnl_Date_Picker.FindControl("RadDatePicker1");

                    Sessions.SetSessions("StartDate", RDP_Date.SelectedDate);

                    //e.Item.OwnerTableView.FilterExpression = String.Format("DateTime > '{0}'", RDP_Date.SelectedDate.ToString());
                    e.Item.OwnerTableView.FilterExpression = String.Format("DateTime > '{0}' AND DateTime < '{1}'", RDP_Date.SelectedDate.ToString(), Convert.ToDateTime(RDP_Date.SelectedDate.ToString()).AddDays(1).ToString());
                    e.Item.OwnerTableView.Rebind();
                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void GridJobReportPrepress_ItemCreated(object sender, Telerik.WebControls.GridItemEventArgs e)
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
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        #endregion


        private void DDlRecordPage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                string _SelectedValue;
                _SelectedValue = ((DropDownList)sender).SelectedItem.Value;
                ViewState["RecordPerPage"] = _SelectedValue;

                //====  Set No of Record Per Page for Prepress Grid  ====//
                GridJobReportPrepress.PageSize = Convert.ToInt32(_SelectedValue);
                GridJobReportPrepress.CurrentPageIndex = 0;
                //====  Check Session of Data  ====//

                // Set to Prepress Grid
                if (Sessions.GetSession("dtJobReportPrepress") != null)
                {
                    DataTable DtPrepress = (DataTable)Sessions.GetSession("dtJobReportPrepress");
                    GridJobReportPrepress.DataSource = DtPrepress;

                }

                //====  Bind Prepress Grid  ====//
                GridJobReportPrepress.DataBind();
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                //====  Clear Filter Controls for Prepress Grid  ====//
                foreach (Telerik.WebControls.GridColumn column in GridJobReportPrepress.MasterTableView.Columns)
                {
                    column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                    column.CurrentFilterValue = String.Empty;
                }

                //====  Set Filter Empty  ====//
                GridJobReportPrepress.MasterTableView.FilterExpression = String.Empty;

                //====  Rebind Master View  ====//
                GridJobReportPrepress.MasterTableView.Rebind();
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void DDL_CompanyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridJobReportPrepress.Rebind();
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        #endregion

        #region Methods and Functions
        private void FillGrid()
        {
            try
            {
                ProcessorData objProcessor = new ProcessorData();
                objProcessor.TaskeroUserID = Sessions.UserID;
                objProcessor.RoleID = Sessions.RoleID;

                DataTable dt = objProcessor.GetZProcList();

                if (DDL_CompanyList.SelectedValue.Equals("0"))
                {
                    if (dt != null && dt.Rows.Count > 0)
                        GridJobReportPrepress.DataSource = dt;
                    else
                        GridJobReportPrepress.DataSource = null;
                }
                else
                {
                    dt.DefaultView.RowFilter = "CompanyID = " + DDL_CompanyList.SelectedValue;
                    if (dt != null && dt.Rows.Count > 0)
                        GridJobReportPrepress.DataSource = dt.DefaultView;
                    else
                        GridJobReportPrepress.DataSource = null;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private string GetFilterFormat(Type columnDataType)
        {
            try
            {
                string retVal = String.Empty;

                if (columnDataType.Name.ToString() == "String")
                {
                    retVal = "[{0}] = '{1}'";
                }
                else
                {
                    retVal = "[{0}] = {1}";
                }

                return retVal;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion
    }
}