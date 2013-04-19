#region Using System
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
using System.Collections.Generic;
using System.Security;
using System.Linq;
#endregion

#region Using Application
using Telerik.WebControls;
using Fuji.Common;
using Fuji.Business;
using System.Text;
using Dundas.Charting.WebControl;
using System.Drawing;
using System.IO;
#endregion

public partial class SyncDetail : PageBase
{
    #region Private Variable Declaration
    public string appurl = "";
    public string uploadurl = "";
    public string projectid = "";
    public string serialno = "";
    public string projectclose = "";
    public string pressrunid = "";
    public string pressrunclose = "";
    public string pressRunName = "";
    #endregion


    #region PageLoad and Events
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //=========== Clear Error ==========//
            LblError.Text = "";
            //BtnGenerateReport.Attributes.Add("onclick", "return OpenGamutWindow('SyncCertifyPressRunPopup.aspx','750px','530px')");


            appurl = WebSettings.ImageURL.ToString(); // Set flex variable
            uploadurl = WebSettings.ImageURL.ToString() + "Upload.axd"; // Set flex variable
            //txtScreeningDotSize.Attributes.Add("onkeypress", "return isNumberKey(event)"); 


            /** for back button issue 12th December 2012 Do we need this -- Chirag?? **/ 
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();

            //=========== Default Settings ==========//
            if (IsPostBack == false)
            {
                
                
                Fuji.Common.Sessions.SetSessions("SyncPressData", null); // Clear SyncPressData Session
                Fuji.Common.Sessions.SetSessions("SyncPressRunData", null); // Clear SyncPressRunData Session

                //=========== Check Login ==========//
                Sessions.CheckUserLogin();

                //========= If Login user is admin then he/she can filter press data by compnay and branch wise==//
                if (Fuji.Common.Sessions.RoleID == 1)
                {
                    TrCompanyBranch.Visible = true; // Admin then filter option is visible
                    BindCompanyDropDown();
                    ddlBranch.Items.Insert(0, new ListItem("--- Select Branch ---", "0"));
                }
                else if (Fuji.Common.Sessions.RoleID == 3)
                {
                    TrCompanyBranch.Visible = true; // other user can not be filter 
                    BindCompanyDropDown();

                    ddlBranch.Items.Insert(0, new ListItem("--- Select Branch ---", "0"));
                    //=========== Bind Press List ==========//                
                    //BindSyncPress();
                }
                else
                {
                    TrCompanyBranch.Visible = false; // other user can not be filter 
                    //=========== Bind Press List ==========//                
                    BindSyncPress();
                }
                //=========== Bind Load Time Left Menu
                LoadTimeLeftMenu();
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }

    }
    #endregion

    #region Press Tab Event
    /// <summary>
    /// Company drop down selected index change event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Fuji.Common.Sessions.SetSessions("SyncPressData", null);
            //========== Check Company Dropdownlist and its index and bind branch based on selected company ===========//
            if (ddlCompany.SelectedIndex != 0)
            {
                BindBranchDropDown(Convert.ToInt32(ddlCompany.SelectedValue.Trim()));

                //set session variable for selected company    
                Fuji.Common.Sessions.SetSessions("SyncCompanyID", ddlCompany.SelectedValue.Trim());
                BindSyncPress();
            }
            else
            { BindBranchDropDown(0); }

           
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    /// <summary>
    /// Branch drop down selected index change event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Fuji.Common.Sessions.SetSessions("SyncPressData", null);
            Fuji.Common.Sessions.SetSessions("SyncBranchID", ddlBranch.SelectedValue.Trim());
            BindSyncPress();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    /// <summary>
    /// Sync press rad grid need data sourece event for bind grid
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void RG_SyncPress_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        try
        {
            //========== Check Company Data in session if exist then bind it ===========//
            if (Fuji.Common.Sessions.GetSession("SyncPressData") != null)
            { RG_SyncPress.DataSource = Fuji.Common.Sessions.GetSession("SyncPressData"); }
        }
        catch (Exception exc)
        { LblError.Text = HandleException(exc); }
    }
    /// <summary>
    /// Sync press rad grid sort command event for sort data
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void RG_SyncPress_SortCommand(object source, Telerik.WebControls.GridSortCommandEventArgs e)
    {
        try
        {
            //========== Check Whether the table contains the passed sort expression or not ===========//
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                //========== Declare the sort expression object ===========//
                GridSortExpression SortExpr = new GridSortExpression();

                //========== Assign the sort expression value to its object ===========//
                SortExpr.FieldName = e.SortExpression;
                SortExpr.SortOrder = GridSortOrder.Ascending;

                //========== Add sort expression object to Table ===========//
                e.Item.OwnerTableView.SortExpressions.AddSortExpression(SortExpr);
            }
        }
        catch (Exception exc)
        { LblError.Text = HandleException(exc); }
    }
    /// <summary>
    /// Sync press rad grid item data bound event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RG_SyncPress_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            //========== Check The Item is item or alternating item then comes to this end of code ===========//
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                DataRowView DR = (DataRowView)e.Item.DataItem;
                LinkButton LbtnPressName = (LinkButton)e.Item.FindControl("LbtnPressName"); // Find the link button 
                LbtnPressName.Attributes.Add("PressID", DR["PressID"].ToString());  // Set the attributes in link button               
            }
        }
        catch (Exception exc)
        { LblError.Text = HandleException(exc); }
    }
    /// <summary>
    /// Rad grid link button press name click event for go to next navigation
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LbtnPressName_Click(object sender, EventArgs e)
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPSyncPressData ObjSyncPress = new CPSyncPressData();
            //==========  Get Data Table and Bind ==========//
            DataTable DT_SyncPress = new DataTable();
            DT_SyncPress = ObjSyncPress.uspGetSyncPressDetailByPressID(Convert.ToInt16(((LinkButton)(sender)).Attributes["PressID"]));
            //========== Assign the  data  ===========//
            if (DT_SyncPress != null && DT_SyncPress.Rows.Count > 0)
            {
                //============= Get meft menu from session =======================//
                List<string> obj = (List<string>)Fuji.Common.Sessions.GetSession("SyncLeftMenu");

                string PressLeftManuData = "";
                //====== Create Print Menu with javascript click function ============//
                PressLeftManuData = "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE' style='cursor:pointer' onclick='CallPressBindGrid()'><tr><td>Select Device :</td></tr><tr><td>" + DT_SyncPress.Rows[0]["PressName"].ToString() + "</td></tr></table>";
                obj.Insert(0, PressLeftManuData);
                //====== Create Print Run Menu with out javascript click function ============//
                obj.Insert(1, "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table><table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE'><tr><td>Select Run :</td></tr><tr><td>NON SELECTED</td></tr></table>");
                //========== Set Press And Press Run  step in left menu ================//
                LtrLeftNevigation.Text = obj[0] + obj[1];

                //=========== Set list menu object in to session =====================//
                Fuji.Common.Sessions.SetSessions("SyncLeftMenu", obj);

                //=========== Set Press ID in to session =====================//
                Fuji.Common.Sessions.SetSessions("SelectedPressID", ((LinkButton)(sender)).Attributes["PressID"].ToString());

                Fuji.Common.Sessions.SetSessions("SelectedSerialNo", DT_SyncPress.Rows[0]["CPSerialNUM"].ToString());

                //========= Visible PrintRun step =======//
                TableVisibleTrueFalse(TblPressRun);

                //=========== Set session null =====================//
                Fuji.Common.Sessions.SetSessions("SyncPressRunData", null);
                BindSyncProject(true);
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    /// <summary>
    /// This button click event fire from javascript function "CallPressBindGrid()" for come back to this step.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBindPressGrid_Click(object sender, EventArgs e)
    {
        try
        {
            //========= Visible Print step =======//
            TableVisibleTrueFalse(TblPress);

            //========= Load Default Left Menu ========//
            LoadTimeLeftMenu();

            //========= Set session null ========//
            Fuji.Common.Sessions.SetSessions("SyncPressData", null);

            //========= If Login user is admin then he/she can filter press data by compnay and branch wise==//
            if (Fuji.Common.Sessions.RoleID == 1 || Fuji.Common.Sessions.RoleID == 3)
            {
                TrCompanyBranch.Visible = true; // Admin then filter option is visible
                BindCompanyDropDown();
                ddlBranch.Items.Insert(0, new ListItem("--- Select Branch ---", "0"));
            }
            else
            {
                TrCompanyBranch.Visible = false; // other user can not be filter 
                //======== Bind Press Grid =========//
                BindSyncPress();
            }


        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    #endregion

    #region Press Run Tab Event
    /// <summary>
    /// Sync press run rad grid need data sourece event for bind grid
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void RG_SyncProject_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        try
        {
            BindSyncProject(false);
        }
        catch (Exception exc)
        { LblError.Text = HandleException(exc); }
    }
    /// <summary>
    /// Sync press run rad grid sort command event for sort data
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void RG_SyncProject_SortCommand(object source, Telerik.WebControls.GridSortCommandEventArgs e)
    {
        try
        {
            //========== Check Whether the table contains the passed sort expression or not ===========//
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                //========== Declare the sort expression object ===========//
                GridSortExpression SortExpr = new GridSortExpression();

                //========== Assign the sort expression value to its object ===========//
                SortExpr.FieldName = e.SortExpression;
                SortExpr.SortOrder = GridSortOrder.Ascending;

                //========== Add sort expression object to Table ===========//
                e.Item.OwnerTableView.SortExpressions.AddSortExpression(SortExpr);
            }
        }
        catch (Exception exc)
        { LblError.Text = HandleException(exc); }
    }
    /// <summary>
    /// Sync press run rad grid item data bound event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RG_SyncProject_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            //========== Check The Item is item or alternating item then comes to this end of code ===========//
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {


                DataRowView DR = (DataRowView)e.Item.DataItem;
                if (!e.Item.OwnerID.Contains("Detail"))
                {

                    ImageButton ibtnEdit = (ImageButton)e.Item.FindControl("ibtnEdit");
                    ibtnEdit.Attributes.Add("ProjectID", DR["ProjectID"].ToString());
                    ibtnEdit.Attributes.Add("ProjectClose", DR["ProjStatusID"].ToString());

                    ImageButton ibtnDelete = (ImageButton)e.Item.FindControl("ibtnDelete");
                    ibtnDelete.Attributes.Add("ProjectID", DR["ProjectID"].ToString());
                    ibtnDelete.Attributes.Add("ProjectClose", DR["ProjStatusID"].ToString());
                    //Delete confirmation added by CSS on 24th Feb 2011
                    ibtnDelete.Attributes.Add("OnClick", "javascript : if(confirm('" + Resources.Message.DeleteConfirmMessage_Text + "') == true) return true; else return false;");


                    Button BtnCreate = (Button)e.Item.FindControl("BtnCreate");

                    if (DR["level"].ToString() == "0")
                    {
                        BtnCreate.Visible = true;
                        BtnCreate.Attributes.Add("ProjectID", DR["ProjectID"].ToString());
                    }
                    else
                    {
                        BtnCreate.Visible = false;
                    }

                    if (DR["ReTargProject"] != DBNull.Value)
                    {

                        System.Web.UI.WebControls.Image imgReTargB = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgReTarg");
                        imgReTargB.AlternateText = DR["ReTargProject"].ToString();
                        imgReTargB.ToolTip = DR["ReTargProject"].ToString();
                        imgReTargB.Visible = true;
                    }
                }
                else
                {


                    HiddenField HDParentLevel = (HiddenField)e.Item.OwnerTableView.ParentItem.FindControl("HDLevel");


                    LinkButton LbtnPressRunName = (LinkButton)e.Item.FindControl("LbtnPressRunName");
                    LbtnPressRunName.Attributes.Add("ProjectID", DR["ProjectID"].ToString());
                    LbtnPressRunName.Attributes.Add("PressRunID", DR["PressRunID"].ToString());
                    LbtnPressRunName.Attributes.Add("PressRunClose", DR["Status"].ToString());
                    LbtnPressRunName.Attributes.Add("ProjectTypeID", DR["ProjectTypeID"].ToString());
                    LbtnPressRunName.Attributes.Add("ProjectUseMeasurementPlugin", Convert.ToBoolean(DR["UsePlugin"].ToString()).ToString());
                    LbtnPressRunName.Attributes.Add("ChartTypeID", Convert.ToInt16(DR["ChartTypeID"].ToString()).ToString());


                    Button BtnCreate = (Button)e.Item.FindControl("BtnCreate");

                    if (Convert.ToInt16(DR["level"].ToString()) < 4 && DR["Status"].ToString() == "2")
                        if (HDParentLevel != null && Convert.ToInt16(DR["level"].ToString()) == Convert.ToInt16(HDParentLevel.Value))
                        {
                            BtnCreate.Visible = true;
                            BtnCreate.Attributes.Add("ProjectID", DR["ProjectID"].ToString());
                            BtnCreate.Attributes.Add("ProjectTypeID", DR["ProjectTypeID"].ToString());
                            BtnCreate.Attributes.Add("ProjectUseMeasurementPlugin", Convert.ToBoolean(DR["UsePlugin"].ToString()).ToString());
                            BtnCreate.Attributes.Add("ChartTypeID", Convert.ToInt16(DR["ChartTypeID"].ToString()).ToString());
                        }
                        else
                            BtnCreate.Visible = false;
                    else
                        BtnCreate.Visible = false;



                }
            }
        }
        catch (Exception exc)
        { LblError.Text = HandleException(exc); }
    }

    protected void BtnCreate_Click(object sender, EventArgs e)
    {

        try
        {

           
                CPSyncPressRunData objCPSyncPressRun = new CPSyncPressRunData();
                string projID = ((Button)sender).Attributes["ProjectID"];
                string projTypeID = ((Button)sender).Attributes["ProjectTypeID"];
                string UsePlugin = ((Button)sender).Attributes["ProjectUseMeasurementPlugin"];
                string ChartTypeID = ((Button)sender).Attributes["ChartTypeID"];
                if (!string.IsNullOrEmpty(projID))
                {
                    objCPSyncPressRun.ProjectID = Convert.ToInt16(projID);
                    objCPSyncPressRun.Status = 1;
                    objCPSyncPressRun.StartDateTime = Convert.ToDateTime(PRClientDatatime.Value.ToString());
                    objCPSyncPressRun.uspInsertCPSyncPressRun();

                    LinkButton btnPR = new LinkButton();
                    btnPR.Attributes.Add("ProjectID", objCPSyncPressRun.ProjectID.ToString());
                    btnPR.Attributes.Add("PressRunID", objCPSyncPressRun.PressRunID.ToString());
                    btnPR.Attributes.Add("PressRunClose", "1");
                    btnPR.Attributes.Add("ProjectTypeID", projTypeID);  //Added By Chirag 8th May 2011 gettin an error while creating next level press run
                    btnPR.Attributes.Add("ProjectUseMeasurementPlugin", Convert.ToBoolean(UsePlugin).ToString());
                    btnPR.Attributes.Add("ChartTypeID", Convert.ToInt16(ChartTypeID).ToString());
                    LbtnPressRun_Click(btnPR, null);
                }
            
        }
        catch (Exception Ex)
        {
            LblError.Text = Ex.Message;
        }

    }

    protected void RG_SyncProject_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ExpandCollapse")
            {
                Fuji.Common.Sessions.SetSessions("SelectedProjectID", e.Item.Cells[2].Text);

                //Added for When Item is Collapse Button Create is not appear
                if (!e.Item.OwnerID.Contains("Detail") && e.Item.Expanded)
                {

                    ImageButton ibtnEdit = (ImageButton)e.Item.FindControl("ibtnEdit");
                    Button BtnCreate = (Button)e.Item.FindControl("BtnCreate");

                    //if (ibtnEdit.Attributes["ProjectClose"].ToString() == Convert.ToInt16(Enums.CPSyncPressRunStatus.Closed).ToString())
                    //{
                    //    BtnCreate.Visible = true;
                    //    BtnCreate.Attributes.Add("ProjectID", ibtnEdit.Attributes["ProjectID"].ToString());
                    //}
                    //else
                    //{
                    BtnCreate.Visible = false;
                    //}
                }
            }
        }
        catch (Exception Ex)
        {
            LblError.Text = Ex.Message;
        }
    }

    protected void RG_SyncProject_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;

        //==========  Declaring Objects ==========//
        CPSyncPressRunData ObjSyncPressRun = new CPSyncPressRunData();
        switch (e.DetailTableView.Name)
        {

            case "PressRun":
                {
                    //==========  Get Data Table and Bind ==========//
                    DataTable DT_SyncPressRun = new DataTable();
                    DT_SyncPressRun = ObjSyncPressRun.uspGetAllSyncPressRunByProjectID(Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedProjectID")));
                    e.DetailTableView.DataSource = DT_SyncPressRun;
                    break;

                }
            default:
                break;
        }

    }

    protected void RG_SyncProject_PageIndexChanged(object source, Telerik.WebControls.GridPageChangedEventArgs e)
    {
        try
        {
            //========== Assign new page index to current page indes ===========//
            RG_SyncProject.CurrentPageIndex = e.NewPageIndex;

            //========== Bind CompanyData ===========//
            BindSyncProject(true);
        }
        catch (Exception exc)
        { LblError.Text = HandleException(exc); }
    }

    private void DDlRecordPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            string _SelectedValue;
            _SelectedValue = ((DropDownList)sender).SelectedItem.Value;
            ViewState["RecordPerPage"] = _SelectedValue;

            //====  Set No of Record Per Page for Press Grid  ====//
            RG_SyncProject.PageSize = Convert.ToInt32(_SelectedValue);
            RG_SyncProject.CurrentPageIndex = 0;

            //====  Bind Press Grid  ====//
            RG_SyncProject.Rebind();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void RG_SyncProject_ItemCreated(object sender, Telerik.WebControls.GridItemEventArgs e)
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
                        DDL_RecordPage.SelectedValue = "25";
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
            }
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }

    /// <summary>
    /// Rad grid link button press name click event for go to next navigation
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPSyncPressRunData ObjSyncPressRun = new CPSyncPressRunData();
            //==========  Get Data Table and Bind in left manu==========//
            DataTable DT_SyncPressRun = new DataTable();
            DT_SyncPressRun = ObjSyncPressRun.uspGetSyncPressRunDetailByProjectID(Convert.ToInt16(((ImageButton)(sender)).Attributes["ProjectID"]));
            //========== Assign the  data  ===========//
            if (DT_SyncPressRun != null && DT_SyncPressRun.Rows.Count > 0)
            {
                TxtProjectName.Text = DT_SyncPressRun.Rows[0]["ProjectName"].ToString();
                lblProjectHeader.Text = "Project Information";
                TxtProjectName.Enabled = false;
                ddlMeasurentDeviceType.Enabled = true;

                DDLProjectType.SelectedValue = DT_SyncPressRun.Rows[0]["ProjectTypeID"].ToString();
                DDLOutputType.SelectedValue = DT_SyncPressRun.Rows[0]["OutputTypeID"].ToString();
                DDLProjectType.Enabled = false;
                DDLOutputType.Enabled = false;

                VisiblePrintConditionDetail((Enums.OutputType)Enum.Parse(typeof(Enums.OutputType), DDLOutputType.SelectedValue.ToString()));
                if (DDLProjectType.SelectedValue.ToString() == Convert.ToInt16(Enums.ProjectType.G7).ToString())
                {
                    trG7.Visible = true;
                    trISO.Visible = false;
                }
                else
                {
                    trG7.Visible = false;
                    trISO.Visible = true;
                }



                //============= Get meft menu from session =======================//
                List<string> obj = (List<string>)Fuji.Common.Sessions.GetSession("SyncLeftMenu");
                string PressLeftManuData = "";

                //====== Create Project Menu with javascript click function ============//
                PressLeftManuData = "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table>";
                PressLeftManuData += "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE' onclick='CallPressRunBindGrid()' style='cursor:pointer'><tr><td>Select Project :</td></tr><tr><td>" + DT_SyncPressRun.Rows[0]["ProjectName"].ToString() + "</td></tr></table>";
                obj.Insert(1, PressLeftManuData);

                //====== Create Print target Menu with out javascript click function ============//
                obj.Insert(2, "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table><table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE'><tr><td>Print Target & Condition:</td></tr><tr><td></td></tr></table>");

                //========== Set Press,Press Run and Print Target step in left menu ================//
                LtrLeftNevigation.Text = obj[0] + obj[1] + obj[2];

                //=========== Set list menu object in to session =====================//
                Fuji.Common.Sessions.SetSessions("SyncLeftMenu", obj);

                //=========== Set session null=====================//
                Fuji.Common.Sessions.SetSessions("SelectedProjectID", ((ImageButton)(sender)).Attributes["ProjectID"].ToString());
                Fuji.Common.Sessions.SetSessions("SelectedProjectClose", ((ImageButton)(sender)).Attributes["ProjectClose"].ToString());

                #region Print Target Bind
                if (DT_SyncPressRun.Rows[0]["ReTargetID"] != DBNull.Value)
                {
                    certrun.Visible = true;
                    tdProjectTypeTitle.Visible = false;
                    tdProjectTypeDDL.Visible = false;
                    standardpt.Visible = false;
                    ddlCertifiedProject.Visible = true;
                    FillCertifiedProjectDropDown(Convert.ToInt32(DT_SyncPressRun.Rows[0]["ReTargetID"]));
                    chkDLP.Enabled = false;

                }
                else
                {
                    certrun.Visible = false;
                    tdProjectTypeTitle.Visible = true;
                    tdProjectTypeDDL.Visible = true;
                    standardpt.Visible = true;
                    ddlCertifiedProject.Visible = false;
                    FillSyncPrintStandardTypeDropDown(); // Fill drop down control.
                }


                //==========  Declaring Objects ==========//
                CPSyncPrintTargetData ObjSyncPrintTarget = new CPSyncPrintTargetData();

                //==========  Get Data Table and Bind ==========//
                DataTable DT_SyncPrintTarget = new DataTable();
                DT_SyncPrintTarget = ObjSyncPrintTarget.uspGetPrintTargetByProjectID(Convert.ToInt32(((ImageButton)(sender)).Attributes["ProjectID"].ToString()));
                if (DT_SyncPrintTarget != null && DT_SyncPrintTarget.Rows.Count > 0) //Check data allready save in data base or not
                {
                    FillPrintTargetInfo(DT_SyncPrintTarget); // Fill print target data form Print Target table
                }
                else
                {
                    DdlStanderdsPreset_SelectedIndexChanged(null, null); // Call drop down selected index change event for deafult selection
                }
                #endregion

                #region Print Condition Bind
                //========== Fill Print Condition data from database ======//
                FillPrintCondition();
                #endregion

                #region Measurement Setup
                FillProjectSetting();
                #endregion

                if (Fuji.Common.Sessions.GetSession("SelectedProjectClose").ToString() == Convert.ToInt16(Enums.CPSyncPressRunStatus.Certify).ToString())
                {
                    BtnSavePrintTargetCondition.Enabled = false;
                }
                ddlMeasurentDeviceType_SelectedIndexChanged(null, null);
                //========= Visible PrintTarget step =======//
                TableVisibleTrueFalse(TblPrintTargetCondition);
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void ibtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPSyncPressRunData ObjSyncPressRun = new CPSyncPressRunData();
            ObjSyncPressRun.ProjectID = Convert.ToInt16(((ImageButton)(sender)).Attributes["ProjectID"]);
            ObjSyncPressRun.Status = Convert.ToInt16(Enums.CPSyncPressRunStatus.NotUsed);
            ObjSyncPressRun.uspUpdateProjectStatus();
            BindSyncProject(true);
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    protected void LbtnPressRun_Click(object sender, EventArgs e)
    {
        try
        {
            //=========== Set session null=====================//
            Fuji.Common.Sessions.SetSessions("SelectedProjectID", ((LinkButton)(sender)).Attributes["ProjectID"].ToString());
            Fuji.Common.Sessions.SetSessions("SelectedPressRunID", ((LinkButton)(sender)).Attributes["PressRunID"].ToString());
            Fuji.Common.Sessions.SetSessions("SelectedPressRunClose", ((LinkButton)(sender)).Attributes["PressRunClose"].ToString());
            Fuji.Common.Sessions.SetSessions("SelectedProjectTypeID", ((LinkButton)(sender)).Attributes["ProjectTypeID"].ToString());
            Fuji.Common.Sessions.SetSessions("SelectedProjectUseMeasurementPlugin", ((LinkButton)(sender)).Attributes["ProjectUseMeasurementPlugin"].ToString());
            Fuji.Common.Sessions.SetSessions("SelectedChartTypeID", ((LinkButton)(sender)).Attributes["ChartTypeID"].ToString());


            pressrunid = Fuji.Common.Sessions.GetSession("SelectedPressRunID").ToString(); // Set flex variable 
            serialno = Fuji.Common.Sessions.GetSession("SelectedSerialNo").ToString();
            pressrunclose = Fuji.Common.Sessions.GetSession("SelectedPressRunClose").ToString();

            #region Build Project and PressRun Menu
            //==========  Declaring Objects ==========//
            CPSyncPressRunData ObjSyncPressRun = new CPSyncPressRunData();
            //==========  Get Data Table and Bind in left manu==========//
            DataTable DT_SyncPressRun = new DataTable();
            DT_SyncPressRun = ObjSyncPressRun.uspGetSyncPressRunDetailByProjectID(Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString()));
            //============= Get meft menu from session =======================//
            List<string> obj = (List<string>)Fuji.Common.Sessions.GetSession("SyncLeftMenu");
            //========== Assign the  data  ===========//
            if (DT_SyncPressRun != null && DT_SyncPressRun.Rows.Count > 0)
            {
                string PressLeftManuData = "";
                //====== Create Project Menu with javascript click function ============//
                PressLeftManuData = "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table>";
                PressLeftManuData += "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE' onclick='CallPressRunBindGrid()' style='cursor:pointer'><tr><td>Select Project :</td></tr><tr><td>" + DT_SyncPressRun.Rows[0]["ProjectName"].ToString() + "</td></tr></table>";
                obj.Insert(1, PressLeftManuData);
            }

            DT_SyncPressRun = new DataTable();
            DT_SyncPressRun = ObjSyncPressRun.uspGetSyncPressRunDetailByPressRunID(Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressRunID").ToString()));
            //========== Assign the  data  ===========//
            if (DT_SyncPressRun != null && DT_SyncPressRun.Rows.Count > 0)
            {
                string PressLeftManuData = "";
                //====== Create Project Menu with javascript click function ============//
                PressLeftManuData = "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table>";
                HDPressRunName.Value = DT_SyncPressRun.Rows[0]["PressRunName"].ToString();
                PressLeftManuData += "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE' onclick='CallPressRunBindGrid()' style='cursor:pointer'><tr><td>Select Press Run :</td></tr><tr><td>" + DT_SyncPressRun.Rows[0]["PressRunName"].ToString() + "</td></tr></table>";
                obj.Insert(2, PressLeftManuData);
            }

            //========== Set all step in left menu ================//
            LtrLeftNevigation.Text = obj[0] + obj[1] + obj[2];

            //=========== Set list menu object in to session ===========================//
            Fuji.Common.Sessions.SetSessions("SyncLeftMenu", obj);
            #endregion

            if (pressrunclose == Convert.ToInt32(Fuji.Common.Enums.CPSyncPressRunStatus.Closed).ToString() || pressrunclose == Convert.ToInt32(Fuji.Common.Enums.CPSyncPressRunStatus.Certify).ToString())
            {
                btnBindAnalyze_Click(sender, e);
            }
            else
            {
                btnBindMeasurement_Click(sender, e);
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }

    /// <summary>
    /// This button click event fire from javascript function "CallPressRunBindGrid()" for come back to this step.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBindPressRunGrid_Click(object sender, EventArgs e)
    {
        try
        {
            //========= Visible PrintRun step =======//
            TableVisibleTrueFalse(TblPressRun);

            //============= Get meft menu from session =======================//
            List<string> obj = (List<string>)Fuji.Common.Sessions.GetSession("SyncLeftMenu");

            //====== Create Print Run Menu with out javascript click function ============//
            obj.Insert(1, "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table><table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE'><tr><td>Select Project :</td></tr><tr><td>NON SELECTED</td></tr></table>");

            //========== Set Press And Press Run  step in left menu ================//
            LtrLeftNevigation.Text = obj[0] + obj[1];
            //=========== Set list menu object in to session =====================//
            Fuji.Common.Sessions.SetSessions("SyncLeftMenu", obj);
            //=========== Set session null =====================//
            Fuji.Common.Sessions.SetSessions("SyncPressRunData", null);
            //=========== Bind Press Run Grid =====================//
            BindSyncProject(true);
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }

    protected void BTN_AddProject_Click(object sender, EventArgs e)
    {
        try
        {
            TxtProjectName.Text = string.Empty;
            TxtProjectName.Enabled = true;
            ddlMeasurentDeviceType.Enabled = true;
            certrun.Visible = false;
            tdProjectTypeTitle.Visible = true;
            tdProjectTypeDDL.Visible = true;

            //DDLProjectType.Enabled = true;
            //DDLOutputType.Enabled = true;

            standardpt.Visible = true;
            lblProjectHeader.Text = "Add Project";
            Fuji.Common.Sessions.SetSessions("SelectedProjectID", ((Button)(sender)).Attributes["ProjectID"].ToString());
            //============= Get meft menu from session =======================//
            List<string> obj = (List<string>)Fuji.Common.Sessions.GetSession("SyncLeftMenu");
            string PressLeftManuData = "";

            //====== Create Project Menu with javascript click function ============//
            PressLeftManuData = "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table>";
            PressLeftManuData += "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE' onclick='CallPressRunBindGrid()' style='cursor:pointer'><tr><td>Select Project :</td></tr><tr><td>New Project</td></tr></table>";
            obj.Insert(1, PressLeftManuData);

            //========== Set Press,Press Run and Print Target step in left menu ================//
            LtrLeftNevigation.Text = obj[0] + obj[1];

            //=========== Set list menu object in to session =====================//
            Fuji.Common.Sessions.SetSessions("SyncLeftMenu", obj);

            BtnSavePrintTargetCondition.Enabled = true;

            //========= Visible PrintTarget step =======//
            TableVisibleTrueFalse(TblPrintTargetCondition);

            #region Print Target Bind
            FillSyncPrintStandardTypeDropDown(); // Fill drop down control.

            //==========  Declaring Objects ==========//
            CPSyncPrintTargetData ObjSyncPrintTarget = new CPSyncPrintTargetData();

            //==========  Get Data Table and Bind ==========//
            DataTable DT_SyncPrintTarget = new DataTable();
            DT_SyncPrintTarget = ObjSyncPrintTarget.uspGetPrintTargetByProjectID(Convert.ToInt32(Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString()));
            if (DDLProjectType.SelectedValue.ToString() == Convert.ToInt16(Enums.ProjectType.G7).ToString())
            {
                trG7.Visible = true;
                trISO.Visible = false;
            }
            else
            {
                trG7.Visible = false;
                trISO.Visible = true;
            }
            VisiblePrintConditionDetail((Enums.OutputType)Enum.Parse(typeof(Enums.OutputType), DDLOutputType.SelectedValue.ToString()));
            if (DT_SyncPrintTarget != null && DT_SyncPrintTarget.Rows.Count > 0) //Check data allready save in data base or not
            {
                FillPrintTargetInfo(DT_SyncPrintTarget); // Fill print target data form Print Target table
            }
            else
            {
                DdlStanderdsPreset_SelectedIndexChanged(null, null); // Call drop down selected index change event for deafult selection
            }
            #endregion

            #region Print Condition Bind
            //========== Fill Print Condition data from database ======//
            FillPrintCondition();
            #endregion

            #region Measurement Setup
            FillProjectSetting();
            #endregion
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    protected void BTN_ReTargetProject_Click(object sender, EventArgs e)
    {
        try
        {
            TxtProjectName.Text = string.Empty;
            TxtProjectName.Enabled = true;
            ddlMeasurentDeviceType.Enabled = true;

            //DDLProjectType.Visible = false;
            certrun.Visible = true;
            tdProjectTypeTitle.Visible = false;
            tdProjectTypeDDL.Visible = false;
            standardpt.Visible = false;
            chkDLP.Enabled = false;
            lblProjectHeader.Text = "Add Project";
            Fuji.Common.Sessions.SetSessions("SelectedProjectID", ((Button)(sender)).Attributes["ProjectID"].ToString());
            //============= Get meft menu from session =======================//
            List<string> obj = (List<string>)Fuji.Common.Sessions.GetSession("SyncLeftMenu");
            string PressLeftManuData = "";

            //====== Create Project Menu with javascript click function ============//
            PressLeftManuData = "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table>";
            PressLeftManuData += "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE' onclick='CallPressRunBindGrid()' style='cursor:pointer'><tr><td>Select Project :</td></tr><tr><td>New Project</td></tr></table>";
            obj.Insert(1, PressLeftManuData);

            //========== Set Press,Press Run and Print Target step in left menu ================//
            LtrLeftNevigation.Text = obj[0] + obj[1];

            //=========== Set list menu object in to session =====================//
            Fuji.Common.Sessions.SetSessions("SyncLeftMenu", obj);

            BtnSavePrintTargetCondition.Enabled = true;

            //========= Visible PrintTarget step =======//
            TableVisibleTrueFalse(TblPrintTargetCondition);

            #region Certified Project Run
            //FillSyncPrintStandardTypeDropDown(); // Fill drop down control.

            FillCertifiedProjectDropDown(0); // Fill drop down control.

            //==========  Declaring Objects ==========//
            CPSyncPrintTargetData ObjSyncPrintTarget = new CPSyncPrintTargetData();

            //==========  Get Data Table and Bind ==========//
            //DataTable DT_SyncPrintTarget = new DataTable();
            //DT_SyncPrintTarget = ObjSyncPrintTarget.uspGetPrintTargetByProjectID(Convert.ToInt32(Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString()));
            //if (DT_SyncPrintTarget != null && DT_SyncPrintTarget.Rows.Count > 0) //Check data allready save in data base or not
            //{
            //    FillPrintTargetInfo(DT_SyncPrintTarget); // Fill print target data form Print Target table
            //}
            //else
            //{
            //DdlStanderdsPreset_SelectedIndexChanged(null, null); // Call drop down selected index change event for deafult selection
            // }
            #endregion

            #region Visible False All Box in print target and print condition
            trG7.Visible = false;
            trISO.Visible = false;
            TrOffsett.Visible = false;
            TrScreen.Visible = false;
            TrWideFormat.Visible = false;
            #endregion

            #region Print Condition Bind
            //========== Fill Print Condition data from database ======//
            FillPrintCondition();
            #endregion

            #region Measurement Setup
            FillProjectSetting();
            #endregion
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    #endregion

    #region Print Target , Condition & Measurement Setup Tab
    /// <summary>
    /// Next Print Condition Button Click Event For Go To Next Tab
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnSavePrintTargetCondition_Click(object sender, EventArgs e)
    {
        try
        {
            bool NewProject = false;
            if (ddlCertifiedProject.Visible == true)
            {
                if (ddlCertifiedProject.SelectedIndex == 0) { LblError.Text = "Select Existing Project."; return; }

            }
            if (ChkUseMeasurementPlugin.Checked)
            {
                if (ddlMeasurentDeviceType.SelectedValue == "")
                {
                    LblError.Text = "Plase select measurement device type.";
                    return;
                }
            }
            if (Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString() == "-1")
            {
                NewProject = true;
                #region Project Save
                //========== Validations For the new Device inputs ==========//
                if (TxtProjectName.Text.Trim() == "") { LblError.Text = "Plase enter Project Name."; return; }

                //==========  Declaring Objects ==========//
                CPSyncPressRunData objCPSyncPressRun = new CPSyncPressRunData();

                //========== check the device id, if not exist then insert new dongle ===========//
                objCPSyncPressRun.ProjectName = TxtProjectName.Text.Trim();
                objCPSyncPressRun.PressID = Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressID"));
                objCPSyncPressRun.Status = Convert.ToInt16(Enums.CPSyncPressRunStatus.Open);
                objCPSyncPressRun.ΔEType = 1;
                objCPSyncPressRun.StartDateTime = Convert.ToDateTime(ClientDatatime.Value.ToString());
                objCPSyncPressRun.UseDLP = Convert.ToBoolean(chkDLP.Checked);
                objCPSyncPressRun.ProjectTypeID = Convert.ToInt32(DDLProjectType.SelectedValue);
                objCPSyncPressRun.OutputTypeID = Convert.ToInt32(DDLOutputType.SelectedValue);
                if (certrun.Visible == true) objCPSyncPressRun.ReTargetID = Convert.ToInt32(ddlCertifiedProject.SelectedValue);


                //check project with the similar name already exists
                System.Data.SqlClient.SqlDataReader DR = objCPSyncPressRun.CheckProjectName();
                if (DR.HasRows == true) { LblError.Text = "Project with similar name already exists for this press. Change project name"; return; }


                KeyValuePair<int, int> data = objCPSyncPressRun.uspInsertCPSyncProject();
                Fuji.Common.Sessions.SetSessions("SelectedProjectID", data.Key);
                Fuji.Common.Sessions.SetSessions("SelectedPressRunID", data.Value);
                Fuji.Common.Sessions.SetSessions("SelectedProjectTypeID", DDLProjectType.SelectedValue);
                Fuji.Common.Sessions.SetSessions("SelectedProjectClose", Convert.ToInt16(Enums.CPSyncPressRunStatus.Open).ToString());
                Fuji.Common.Sessions.SetSessions("SelectedPressRunClose", Convert.ToInt16(Enums.CPSyncPressRunStatus.Open).ToString());
                if (ddlMeasurentAlignmentChart.SelectedValue != "")
                    Fuji.Common.Sessions.SetSessions("SelectedChartTypeID", Convert.ToInt16(ddlMeasurentAlignmentChart.SelectedValue));

                #endregion
            }
            else
            {
                NewProject = false;
                //update project

                //==========  Declaring Objects ==========//
                CPSyncPressRunData objCPSyncPressRun = new CPSyncPressRunData();

                //========== update if DLP needed to be used for iteration ===========//
                objCPSyncPressRun.ProjectID = Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedProjectID"));
                objCPSyncPressRun.UseDLP = Convert.ToBoolean(chkDLP.Checked);
                objCPSyncPressRun.ProjectTypeID = Convert.ToInt16(DDLProjectType.SelectedValue);
                objCPSyncPressRun.OutputTypeID = Convert.ToInt16(DDLOutputType.SelectedValue);
                objCPSyncPressRun.uspUpdateProject();
            }

            #region Print Condition Save
            //========= Create XML For insert/update multipal record for one transction to SQL ==============//
            if (DDLProjectType.SelectedValue.ToString() == Convert.ToInt16(Enums.ProjectType.G7).ToString())
            {
                #region Create XML String Save Detail into database for G7
                StringBuilder SBPrintTarget = new StringBuilder("<PrintTarget>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtPaperTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtPaperTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtPaperTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtCyanTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtCyanTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtCyanTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtMagendaTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtMagendaTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtMagendaTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtYellowTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtYellowTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtYellowTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtBlackTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtBlackTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtBlackTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtRedTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtRedTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtRedTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtGreenTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtGreenTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtGreenTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtBlueTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtBlueTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtBlueTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt3CTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt3CTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt3CTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Gray_C_M_Y).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaF).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("1") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtDeltaFTH.Text) + "\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtCyanDT.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtCyanDTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtMagendaDT.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtMagendaDTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtYellowDT.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtMagendaDTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtBalckDT.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtBalckDTH.Text) + "\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt25TC.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(Txt25TH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt25TM.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(Txt25TH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt25TY.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(Txt25TH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt25TK.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(Txt25TH.Text) + "\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt50TC.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(Txt50TH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt50TM.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(Txt50TH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt50TY.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(Txt50TH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt50TK.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(Txt50TH.Text) + "\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt75TC.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(Txt75TH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt75TM.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(Txt75TH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt75TY.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(Txt75TH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(Txt75TK.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(Txt75TH.Text) + "\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtPaperTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtCyanTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtMagendaTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtYellowTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtBlackTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtRedTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtGreenTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtBlueTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(Txt3CTH.Text) + "\"></PrintStandard>");

                SBPrintTarget.Append("</PrintTarget>");

                //==========  Declaring Objects ==========//
                CPSyncPrintTargetData objCPSyncPrintTarget = new CPSyncPrintTargetData();
                objCPSyncPrintTarget.uspInsertCPSyncPrintTarget(Convert.ToInt32(Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString()), SBPrintTarget);
                #endregion
            }
            else
            {
                #region Create XML String Save Detail into database for ISO
                StringBuilder SBPrintTarget = new StringBuilder("<PrintTarget>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOCyanTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOCyanTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOCyanTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOMagendaTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOMagendaTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOMagendaTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOYellowTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOYellowTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOYellowTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOBlackTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOBlackTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOBlackTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISORedTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISORedTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISORedTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOGreenTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOGreenTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOGreenTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOBlueTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOBlueTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOBlueTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO3CTL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO3CTa.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO3CTb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Gray_25).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOGray25TL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                //SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Gray_25).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOGray25Ta.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                //SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Gray_25).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOGray25Tb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Gray_50).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOGray50TL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                //SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Gray_50).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOGray50Ta.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                //SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Gray_50).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOGray50Tb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Gray_75).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOGray75TL.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                //SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Gray_75).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOGray75Ta.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                //SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Gray_75).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOGray75Tb.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.L).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOAPPaperTL.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOTHPaperTL.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.a).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOAPPaperTa.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOTHPaperTa.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.b).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOAPPaperTb.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOTHPaperTb.Text) + "\"></PrintStandard>");


                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO25CA.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISO25CT.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO25MA.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISO25MT.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO25YA.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISO25YT.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO25KA.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISO25KT.Text) + "\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO50CA.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISO50CT.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO50MA.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISO50MT.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO50YA.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISO50YT.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO50KA.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISO50KT.Text) + "\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO75CA.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISO75CT.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO75MA.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISO75MT.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO75YA.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISO75YT.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO75KA.Text) + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISO75KT.Text) + "\"></PrintStandard>");



                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOQuarterTone.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOQuarterTone.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOQuarterTone.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOQuarterTone.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOMidTone.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOMidTone.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOMidTone.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOMidTone.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO3QuarterTone.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO3QuarterTone.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO3QuarterTone.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISO3QuarterTone.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");

                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOCyanDT.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOMagendaDT.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOYellowDT.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString() + "\"" + " " + "Target=\"" + Convert.ToString(TxtISOBalckDT.Text) + "\"" + " " + "ToleranceLimit=\"\"></PrintStandard>");


                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOCyanTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOMagendaTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOYellowTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOBlackTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISORedTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOGreenTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOBlueTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISO3CTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Gray_25).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOGray25TH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Gray_50).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOGray50TH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Gray_75).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOGray75TH.Text) + "\"></PrintStandard>");


                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaH).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOCyanHTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaH).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOMagendaHTH.Text) + "\"></PrintStandard>");
                SBPrintTarget.Append("<PrintStandard" + " " + "PatchTypeID=\"" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + "\"" + " " + "ColorCharID=\"" + Convert.ToInt16(Enums.CPSyncColorChar.DeltaH).ToString() + "\"" + " " + "Target=\"" + Convert.ToString("0") + "\"" + " " + "ToleranceLimit=\"" + Convert.ToString(TxtISOYellowHTH.Text) + "\"></PrintStandard>");


                SBPrintTarget.Append("</PrintTarget>");



                //==========  Declaring Objects ==========//
                CPSyncPrintTargetData objCPSyncPrintTarget = new CPSyncPrintTargetData();
                objCPSyncPrintTarget.uspInsertCPSyncPrintTarget(Convert.ToInt32(Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString()), SBPrintTarget);
                #endregion
            }

            ////========= Fill print condition detail when user come back =======//
            //FillPrintCondition();
            #endregion

            #region Target Condition Save

            #region Create XML String Save Print Condition Detail into database
            //Chirag Added this to on 21st  July fix invalid character in XML "<" and ">" and "&" are considered invalid using System.Security.SecurityElement.Escape method

            StringBuilder SBPrintCondition = new StringBuilder("<PrintCondition>");
            if (TxtGDSubstrateDesc.Text.Trim() != string.Empty)
                SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtGDSubstrateDesc.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + System.Security.SecurityElement.Escape(TxtGDSubstrateDesc.Text.Trim()) + "\" ></PrintCond>");
            if (TxtGDInk.Text.Trim() != string.Empty)
                SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtGDInk.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + System.Security.SecurityElement.Escape(TxtGDInk.Text.Trim()) + "\" ></PrintCond>");

            if (DDLOutputType.SelectedValue.ToString() == Convert.ToInt16(Enums.OutputType.Offset).ToString())
            {
                if (TxtOPDISO2846Ink.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtOPDISO2846Ink.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtOPDISO2846Ink.Text.Trim()) + "\" ></PrintCond>");

                SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + DdlOPDInkOrder.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + DdlOPDInkOrder.Text.Trim() + "\" ></PrintCond>");

                if (TxtOPDFountainSolution.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtOPDFountainSolution.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtOPDFountainSolution.Text.Trim()) + "\" ></PrintCond>");
                if (TxtOPDBlanket.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtOPDBlanket.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtOPDBlanket.Text.Trim()) + "\" ></PrintCond>");
                if (TxtOPDScreening.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtOPDScreening.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtOPDScreening.Text.Trim()) + "\" ></PrintCond>");
                if (TxtOPDBlanketAge.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtOPDBlanketAge.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtOPDBlanketAge.Text.Trim()) + "\" ></PrintCond>");
            }
            if (DDLOutputType.SelectedValue.ToString() == Convert.ToInt16(Enums.OutputType.Screen).ToString())
            {
                if (TxtSPDStencil.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtSPDStencil.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtSPDStencil.Text.Trim()) + "\" ></PrintCond>");
                if (TxtSPDCyan.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtSPDCyan.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtSPDCyan.Text.Trim()) + "\" ></PrintCond>");
                if (TxtSPDMagenta.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtSPDMagenta.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtSPDMagenta.Text.Trim()) + "\" ></PrintCond>");
                if (TxtSPDYellow.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtSPDYellow.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtSPDYellow.Text.Trim()) + "\" ></PrintCond>");
                if (TxtSPDBlack.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtSPDBlack.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtSPDBlack.Text.Trim()) + "\" ></PrintCond>");
                if (TxtSPDRip.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtSPDRip.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtSPDRip.Text.Trim()) + "\" ></PrintCond>");
                if (TxtSPDCTSFilmDevice.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtSPDCTSFilmDevice.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtSPDCTSFilmDevice.Text.Trim()) + "\" ></PrintCond>");
            }

            if (DDLOutputType.SelectedValue.ToString() == Convert.ToInt16(Enums.OutputType.Wideformat).ToString())
            {
                if (TxtWFRip.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtWFRip.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtWFRip.Text.Trim()) + "\" ></PrintCond>");
                if (TxtWFPrintMode.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtWFPrintMode.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtWFPrintMode.Text.Trim()) + "\" ></PrintCond>");
                if (TxtWFScreeningType.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtWFScreeningType.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtWFScreeningType.Text.Trim()) + "\" ></PrintCond>");
                if (TxtWFUVLampSettings.Text.Trim() != string.Empty)
                    SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtWFUVLampSettings.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtWFUVLampSettings.Text.Trim()) + "\" ></PrintCond>");
            }

            if (TxtNotes.Text.Trim() != string.Empty)
                SBPrintCondition.Append("<PrintCond" + " " + "PrintConditionTypeID=\"" + TxtNotes.Attributes["PCTypeId"].ToString() + "\"" + " " + "PrintCondition=\"" + SecurityElement.Escape(TxtNotes.Text.Trim()) + "\" ></PrintCond>");

            SBPrintCondition.Append("</PrintCondition>");

            if (SBPrintCondition.ToString().Trim() != string.Empty)
            {
                CPSyncPrintConditionData objCPSyncPrintCondition = new CPSyncPrintConditionData();
                objCPSyncPrintCondition.uspInsertCPSyncPrintCond(Convert.ToInt32(Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString()), SBPrintCondition);
            }
            #endregion

            #endregion

            #region Measurement Setup
            //==========  Declaring Objects ==========//
            CPSyncProjectSettingData objCPSyncProjectSettingData = new CPSyncProjectSettingData();

            //========== check the device id, if not exist then insert new dongle ===========//
            objCPSyncProjectSettingData.ProjectID = Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedProjectID"));
            objCPSyncProjectSettingData.UsePlugin = ChkUseMeasurementPlugin.Checked;
            objCPSyncProjectSettingData.Observer = Convert.ToInt16(ddlMeasurentObserver.SelectedValue);
            objCPSyncProjectSettingData.Illuminant = Convert.ToInt16(ddlMeasurentIlluminant.SelectedValue);
            objCPSyncProjectSettingData.DensityType = Convert.ToInt16(ddlMeasurentDensityType.SelectedValue);
            objCPSyncProjectSettingData.IlluminationMode = 1;
            if (ddlMeasurentAlignmentChart.SelectedValue != "")
                objCPSyncProjectSettingData.ChartTypeID = Convert.ToInt16(ddlMeasurentAlignmentChart.SelectedValue);
            else
                objCPSyncProjectSettingData.ChartTypeID = null;
            if (ddlMeasurentDeviceType.SelectedValue != "")
            {
                objCPSyncProjectSettingData.InstrumentTypeID = Convert.ToInt32(ddlMeasurentDeviceType.SelectedValue.Split('-')[0].Trim());
                objCPSyncProjectSettingData.SerialNumber = ddlMeasurentDeviceType.SelectedValue.Split('-')[1].Trim();
            }
            else
            {
                objCPSyncProjectSettingData.InstrumentTypeID = null;
                objCPSyncProjectSettingData.SerialNumber = "";
            }
            objCPSyncProjectSettingData.uspInsertProjectSetting();

            Fuji.Common.Sessions.SetSessions("SelectedProjectUseMeasurementPlugin", Convert.ToBoolean(ChkUseMeasurementPlugin.Checked).ToString());

            #endregion

            projectid = Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString(); // Set flex variable 
            serialno = Fuji.Common.Sessions.GetSession("SelectedSerialNo").ToString();
            projectclose = Fuji.Common.Sessions.GetSession("SelectedProjectClose").ToString();

            if (NewProject == true)  // If new record then goto meseaurement page
            {
                #region Build Project and PressRun Menu
                //==========  Declaring Objects ==========//
                CPSyncPressRunData ObjSyncPressRun = new CPSyncPressRunData();
                //==========  Get Data Table and Bind in left manu==========//
                DataTable DT_SyncPressRun = new DataTable();
                DT_SyncPressRun = ObjSyncPressRun.uspGetSyncPressRunDetailByProjectID(Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString()));
                //============= Get meft menu from session =======================//
                List<string> obj = (List<string>)Fuji.Common.Sessions.GetSession("SyncLeftMenu");
                //========== Assign the  data  ===========//
                if (DT_SyncPressRun != null && DT_SyncPressRun.Rows.Count > 0)
                {
                    string PressLeftManuData = "";
                    //====== Create Project Menu with javascript click function ============//
                    PressLeftManuData = "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table>";
                    PressLeftManuData += "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE' onclick='CallPressRunBindGrid()' style='cursor:pointer'><tr><td>Select Project :</td></tr><tr><td>" + DT_SyncPressRun.Rows[0]["ProjectName"].ToString() + "</td></tr></table>";
                    obj.Insert(1, PressLeftManuData);
                }

                DT_SyncPressRun = new DataTable();
                DT_SyncPressRun = ObjSyncPressRun.uspGetSyncPressRunDetailByPressRunID(Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressRunID").ToString()));
                //========== Assign the  data  ===========//
                if (DT_SyncPressRun != null && DT_SyncPressRun.Rows.Count > 0)
                {
                    string PressLeftManuData = "";
                    //====== Create Project Menu with javascript click function ============//
                    PressLeftManuData = "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table>";
                    HDPressRunName.Value = DT_SyncPressRun.Rows[0]["PressRunName"].ToString();
                    PressLeftManuData += "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE' onclick='CallPressRunBindGrid()' style='cursor:pointer'><tr><td>Select Press Run :</td></tr><tr><td>" + DT_SyncPressRun.Rows[0]["PressRunName"].ToString() + "</td></tr></table>";
                    obj.Insert(2, PressLeftManuData);
                }

                //========== Set all step in left menu ================//
                LtrLeftNevigation.Text = obj[0] + obj[1] + obj[2];

                //=========== Set list menu object in to session ===========================//
                Fuji.Common.Sessions.SetSessions("SyncLeftMenu", obj);
                #endregion

                btnBindMeasurement_Click(sender, e);

            }
            else // Else back to press run page.
            {
                #region Bind Projects and show that table...
                //==========  Declaring Objects ==========//
                CPSyncPressData ObjSyncPress = new CPSyncPressData();
                //==========  Get Data Table and Bind ==========//
                DataTable DT_SyncPress = new DataTable();
                DT_SyncPress = ObjSyncPress.uspGetSyncPressDetailByPressID(Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressID").ToString()));
                //========== Assign the  data  ===========//
                if (DT_SyncPress != null && DT_SyncPress.Rows.Count > 0)
                {
                    //============= Get meft menu from session =======================//
                    List<string> obj = (List<string>)Fuji.Common.Sessions.GetSession("SyncLeftMenu");

                    string PressLeftManuData = "";
                    //====== Create Print Menu with javascript click function ============//
                    PressLeftManuData = "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE' style='cursor:pointer' onclick='CallPressBindGrid()'><tr><td>Select Press :</td></tr><tr><td>" + DT_SyncPress.Rows[0]["PressName"].ToString() + "</td></tr></table>";
                    //PressLeftManuData += "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SUBSYNCTABLELEFTTABLE'><tr><td><b>Press Setup</b></td></tr><tr><td> SN:" + DT_SyncPress.Rows[0]["CPSerialNUM"].ToString() + "</td></tr><tr><td><b>" + DT_SyncPress.Rows[0]["NumOfInkUnits"].ToString() + " Units</b></td></tr></table>";
                    obj.Insert(0, PressLeftManuData);
                    //====== Create Print Run Menu with out javascript click function ============//
                    obj.Insert(1, "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table><table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE'><tr><td>Select Run :</td></tr><tr><td>NON SELECTED</td></tr></table>");
                    //========== Set Press And Press Run  step in left menu ================//
                    LtrLeftNevigation.Text = obj[0] + obj[1];

                    //=========== Set list menu object in to session =====================//
                    Fuji.Common.Sessions.SetSessions("SyncLeftMenu", obj);

                    //========= Visible PrintRun step =======//
                    TableVisibleTrueFalse(TblPressRun);

                    //=========== Set session null =====================//
                    Fuji.Common.Sessions.SetSessions("SyncPressRunData", null);
                    BindSyncProject(true);
                }
                #endregion
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    /// <summary>
    /// Get Print Standerd Data for selected 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DdlStanderdsPreset_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPSyncPrintStandardData ObjCPSyncPrintStandard = new CPSyncPrintStandardData();
            //==========  Get Data Table and Bind ==========//
            DataTable DT_CPSyncPrintStandard = new DataTable();
            DT_CPSyncPrintStandard = ObjCPSyncPrintStandard.uspGetPrintStandard(Convert.ToInt16(DdlStanderdsPreset.SelectedValue.ToString()));
            //========== Assign the  data  ===========//
            if (DDLProjectType.SelectedValue.ToString() == Convert.ToInt16(Enums.ProjectType.G7).ToString())
            {
                trG7.Visible = true;
                trISO.Visible = false;
                #region Fill Data from database
                if (DT_CPSyncPrintStandard != null && DT_CPSyncPrintStandard.Rows.Count > 0)
                {
                    #region Fill from database
                    for (int i = 0; i < DT_CPSyncPrintStandard.Rows.Count; i++)
                    {
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtPaperTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtPaperTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtPaperTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtPaperTH.Text = "5";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtCyanTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtCyanTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtCyanTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtCyanTH.Text = "5";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtMagendaTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtMagendaTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtMagendaTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtMagendaTH.Text = "5";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtYellowTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtYellowTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtYellowTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtYellowTH.Text = "5";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtBlackTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtBlackTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtBlackTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtBlackTH.Text = "5";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtRedTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtRedTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtRedTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtRedTH.Text = "6";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtGreenTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtGreenTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtGreenTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtGreenTH.Text = "6";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtBlueTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtBlueTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtBlueTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();

                        TxtBlueTH.Text = "6";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            Txt3CTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            Txt3CTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            Txt3CTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        Txt3CTH.Text = "5";

                        TxtDeltaFTH.Text = "1.5";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                            TxtCyanDT.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtCyanDTH.Text = "0.1";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                            TxtMagendaDT.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtMagendaDTH.Text = "0.1";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                            TxtYellowDT.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtYellowDTH.Text = "0.1";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                            TxtBalckDT.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtBalckDTH.Text = "0.1";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            Txt25TC.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            Txt25TM.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            Txt25TY.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            Txt25TK.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        Txt25TH.Text = "5";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            Txt50TC.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            Txt50TM.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            Txt50TY.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            Txt50TK.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        Txt50TH.Text = "5";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            Txt75TC.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            Txt75TM.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            Txt75TY.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            Txt75TK.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        Txt75TH.Text = "5";

                    }
                    #endregion
                }
                else
                {
                    #region Fill blank value
                    CleanG7Detail(); //Clear all control in G7 detail tab.
                    #endregion
                }
                #endregion
            }
            else
            {
                trG7.Visible = false;
                trISO.Visible = true;
                #region Fill ISO Data from database
                if (DT_CPSyncPrintStandard != null && DT_CPSyncPrintStandard.Rows.Count > 0)
                {
                    for (int i = 0; i < DT_CPSyncPrintStandard.Rows.Count; i++)
                    {
                        #region Fill data from database
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtISOCyanTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtISOCyanTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtISOCyanTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISOCyanTH.Text = "5";
                        TxtISOCyanHTH.Text = "2.50";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtISOMagendaTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtISOMagendaTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtISOMagendaTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISOMagendaTH.Text = "5";
                        TxtISOMagendaHTH.Text = "2.50";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtISOYellowTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtISOYellowTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtISOYellowTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISOYellowTH.Text = "5";
                        TxtISOYellowHTH.Text = "2.50";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtISOBlackTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtISOBlackTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtISOBlackTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISOBlackTH.Text = "5";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtISORedTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtISORedTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtISORedTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISORedTH.Text = "10";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtISOGreenTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtISOGreenTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtISOGreenTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISOGreenTH.Text = "10";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtISOBlueTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtISOBlueTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtISOBlueTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISOBlueTH.Text = "10";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtISO3CTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtISO3CTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtISO3CTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISO3CTH.Text = "10";


                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_25).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtISOGray25TL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_25).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtISOGray25Ta.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_25).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtISOGray25Tb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISOGray25TH.Text = "2";


                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_50).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtISOGray50TL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_50).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtISOGray50Ta.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_50).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtISOGray50Tb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISOGray50TH.Text = "2";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_75).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtISOGray75TL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_75).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtISOGray75Ta.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_75).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtISOGray75Tb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISOGray75TH.Text = "2";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                            TxtISOAPPaperTL.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                            TxtISOAPPaperTa.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                            TxtISOAPPaperTb.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISOTHPaperTL.Text = "3";
                        TxtISOTHPaperTa.Text = "2";
                        TxtISOTHPaperTb.Text = "2";


                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            TxtISO25CA.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISO25CT.Text = "3";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            TxtISO25MA.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISO25MT.Text = "3";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            TxtISO25YA.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISO25YT.Text = "3";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            TxtISO25KA.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISO25KT.Text = "3";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            TxtISO50CA.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISO50CT.Text = "4";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            TxtISO50MA.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISO50MT.Text = "4";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            TxtISO50YA.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISO50YT.Text = "4";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            TxtISO50KA.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISO50KT.Text = "4";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            TxtISO75CA.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISO75CT.Text = "3";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            TxtISO75MA.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISO75MT.Text = "3";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            TxtISO75YA.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISO75YT.Text = "3";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                            TxtISO75KA.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        TxtISO75KT.Text = "3";


                        TxtISOQuarterTone.Text = "3";
                        TxtISOMidTone.Text = "5";
                        TxtISO3QuarterTone.Text = "3";

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                            TxtISOCyanDT.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                            TxtISOMagendaDT.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                            TxtISOYellowDT.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();

                        if (DT_CPSyncPrintStandard.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_CPSyncPrintStandard.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                            TxtISOBalckDT.Text = DT_CPSyncPrintStandard.Rows[i]["Target"].ToString();
                        #endregion
                    }
                }
                else
                {
                    #region Fill blank value
                    CleanISODetail(); // Clear all control in ISO detail tab.                    
                    #endregion
                }
                #endregion
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }

    /// <summary>
    /// Get Print Standerd Data for selected 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCertifiedProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCertifiedProject.SelectedIndex != 0)
            {
                //==========  Declaring Objects ==========//
                CPSyncPrintTargetData ObjSyncPrintTarget = new CPSyncPrintTargetData();

                //==========  Get Data Table and Bind ==========//
                DataTable DT_SyncPrintTarget = new DataTable();
                DT_SyncPrintTarget = ObjSyncPrintTarget.uspGetPrintTargetByProjectID(Convert.ToInt32(ddlCertifiedProject.SelectedValue));
                if (DT_SyncPrintTarget != null && DT_SyncPrintTarget.Rows.Count > 0) //Check data allready save in data base or not
                {
                    DDLProjectType.SelectedValue = DT_SyncPrintTarget.Rows[0]["ProjectTypeID"].ToString();
                    VisiblePrintConditionDetail((Enums.OutputType)Enum.Parse(typeof(Enums.OutputType), DDLOutputType.SelectedValue.ToString()));
                    FillPrintTargetInfo(DT_SyncPrintTarget); // Fill print target data form Print Target table
                }
                else
                {
                    DdlStanderdsPreset_SelectedIndexChanged(null, null); // Call drop down selected index change event for deafult selection
                }
            }
            else
            {
                #region Visible False All Box in print target and print condition
                trG7.Visible = false;
                trISO.Visible = false;
                TrOffsett.Visible = false;
                TrScreen.Visible = false;
                TrWideFormat.Visible = false;
                #endregion
                CleanG7Detail(); //Clear all control in G7 detail tab.
                CleanISODetail(); //Clear all control in ISO detail tab.
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DDLProjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLProjectType.SelectedValue.ToString() == Convert.ToInt16(Enums.ProjectType.G7).ToString())
        {
            trG7.Visible = true;
            trISO.Visible = false;
            FillSyncPrintStandardTypeDropDown();
            DdlStanderdsPreset_SelectedIndexChanged(null, null);
        }
        else
        {
            trG7.Visible = false;
            trISO.Visible = true;
            FillSyncPrintStandardTypeDropDown();
            DdlStanderdsPreset_SelectedIndexChanged(null, null);
        }

    }

    protected void DDLOutputType_SelectedIndexChanged(object sender, EventArgs e)
    {

        //Visible panal as per project type
        VisiblePrintConditionDetail((Enums.OutputType)Enum.Parse(typeof(Enums.OutputType), DDLOutputType.SelectedValue.ToString()));

    }


    /// <summary>
    /// This button click event fire from javascript function "CallPrintConditionBind()" for come back to this step.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBindPrintTargetCondition_Click(object sender, EventArgs e)
    {
        try
        {
            #region Print Target Bind
            FillSyncPrintStandardTypeDropDown(); // Fill drop down control.

            //==========  Declaring Objects ==========//
            CPSyncPrintTargetData ObjSyncPrintTarget = new CPSyncPrintTargetData();

            //==========  Get Data Table and Bind ==========//
            DataTable DT_SyncPrintTarget = new DataTable();
            DT_SyncPrintTarget = ObjSyncPrintTarget.uspGetPrintTargetByProjectID(Convert.ToInt32(Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString()));
            if (DT_SyncPrintTarget != null && DT_SyncPrintTarget.Rows.Count > 0) //Check data allready save in data base or not
                FillPrintTargetInfo(DT_SyncPrintTarget);  // Fill print target data form Print Target table 
            else
                DdlStanderdsPreset_SelectedIndexChanged(null, null); // Call drop down selected index change event for deafult selection
            #endregion

            #region Prepare Menu
            //============= Get meft menu from session =======================//
            List<string> obj = (List<string>)Fuji.Common.Sessions.GetSession("SyncLeftMenu");

            //====== Create Print Condition Menu with out javascript click function ============//
            obj.Insert(2, "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table><table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE'><tr><td>Print Target & Condition :</td></tr><tr><td></td></tr></table>");

            //========== Set Press,Press Run,Print Target and Print Condition step in left menu ================//
            LtrLeftNevigation.Text = obj[0] + obj[1] + obj[2];

            //=========== Set list menu object in to session =====================//
            Fuji.Common.Sessions.SetSessions("SyncLeftMenu", obj);
            #endregion

            #region Print Condition Bind
            //========== Fill Print Condition data from database ======//
            FillPrintCondition();
            #endregion

            #region Measurement Setup
            FillProjectSetting();
            #endregion

            //========= Visible PrintCondition step =======//
            TableVisibleTrueFalse(TblPrintTargetCondition);
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }

    #region Measurement Setup
    protected void ddlMeasurentDeviceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMeasurentDeviceType.SelectedValue != "")
                BindAlignmentChart(Convert.ToInt16(ddlMeasurentDeviceType.SelectedValue.Split('-')[0].Trim()));
        }
        catch (Exception Exc)
        {
            LblError.Text = Exc.Message;
        }
    }
    private void BindMeasurementDeviceType()
    {
        try
        {
            ddlMeasurentDeviceType.Items.Clear();
            //==========  Declaring Objects ==========//
            CPSyncMeasurementDeviceData ObjCPSyncMeasurementDeviceData = new CPSyncMeasurementDeviceData();
            DataTable DTCPSyncMeasurementDeviceData = new DataTable();
            ObjCPSyncMeasurementDeviceData.PressID = Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressID")); ;
            DTCPSyncMeasurementDeviceData = ObjCPSyncMeasurementDeviceData.uspGetMeasurementDeviceTypeByPressID();
            if (DTCPSyncMeasurementDeviceData.Rows.Count > 0)
            {
                TabPanMeasurementSetup.HeaderText = "Measurement Setup";
                TabPanMeasurementSetup.Visible = true;
            }
            else
            {
                TabPanMeasurementSetup.HeaderText = "";
                TabPanMeasurementSetup.Visible = false;
            }
            ddlMeasurentDeviceType.DataSource = DTCPSyncMeasurementDeviceData;
            ddlMeasurentDeviceType.DataValueField = "DeviceTypeID";
            ddlMeasurentDeviceType.DataTextField = "DeviceTypeName";
            ddlMeasurentDeviceType.DataBind();

        }
        catch (Exception Ex)
        {

            LblError.Text = HandleException(Ex);
        }


    }
    private void BindAlignmentChart(int InstrumentTypeID)
    {
        try
        {

            //==========  Declaring Objects ==========//
            DataTable DTAlignmentChart = new DataTable();
            CPSyncProjectSettingData ObjCPSyncProjectSettingData = new CPSyncProjectSettingData();
            ObjCPSyncProjectSettingData.InstrumentTypeID = InstrumentTypeID;
            DTAlignmentChart = ObjCPSyncProjectSettingData.uspGetChartByInstrument();


            ddlMeasurentAlignmentChart.DataSource = DTAlignmentChart;
            ddlMeasurentAlignmentChart.DataValueField = "ReferenceChartTypeID";
            ddlMeasurentAlignmentChart.DataTextField = "ChartName";
            ddlMeasurentAlignmentChart.DataBind();

        }
        catch (Exception Ex)
        {

            LblError.Text = HandleException(Ex);
        }


    }

    #endregion

    #endregion


    #region Add Measurement

    /// <summary>
    /// This click button event is fire from javascript function "CallMeasurementBind()" when user come back or click on Add Mesurement link in left menu.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBindMeasurement_Click(object sender, EventArgs e)
    {
        try
        {
            lblAnalyzeMsg.Text = "";
            //========= Visible Add Measurement step =======//
            TableVisibleTrueFalse(TblMeasurement);

            if (Convert.ToBoolean(Fuji.Common.Sessions.GetSession("SelectedProjectUseMeasurementPlugin").ToString()))
            {
                TrPlugin.Visible = true;
                IFramePlugin.Attributes.Add("src", "SyncMeasurement.aspx");
                IFramePlugin.Style.Add("display", "");
                TrFlexObject.Visible = false;
            }
            else
            {
                TrPlugin.Visible = false;
                TrFlexObject.Visible = true;
            }

            pressrunid = Fuji.Common.Sessions.GetSession("SelectedPressRunID").ToString(); // Set flex variable 
            serialno = Fuji.Common.Sessions.GetSession("SelectedSerialNo").ToString();
            pressrunclose = Fuji.Common.Sessions.GetSession("SelectedPressRunClose").ToString();

            //============= Get meft menu from session =======================//
            List<string> obj = (List<string>)Fuji.Common.Sessions.GetSession("SyncLeftMenu");
            string PressLeftManuData = "";

            //=========== Create 1. Add Measurement(Active),2. Analyze,3. Export Curves step in Left menu and call javascript function ========//
            PressLeftManuData = "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table><table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE'><tr><td>Select Action :</td></tr><tr><td></td></tr></table>";
            PressLeftManuData += "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SUBSYNCTABLELEFTTABLE'><tr><td><br/><b>1. Add Measurement</b></td></tr><tr><td><br/>2. Analyze</td></tr><tr><td><br/>3. Export Curves</td></tr><tr><td><br/>&nbsp;</td></tr></table>";
            obj.Insert(3, PressLeftManuData);

            //========== Set all step in left menu ================//
            LtrLeftNevigation.Text = obj[0] + obj[1] + obj[2] + obj[3];

            //=========== Set list menu object in to session ===========================//
            Fuji.Common.Sessions.SetSessions("SyncLeftMenu", obj);
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }

    protected void BtnNextAnalyze_Click(object sender, EventArgs e)
    {
        pressrunid = Fuji.Common.Sessions.GetSession("SelectedPressRunID").ToString(); // Set flex variable 
        //check for measurement list and if list is empty then don't let user to go to analyze screen...
        CPSyncMeasurementData ObjSyncMeasurement = new CPSyncMeasurementData();
        DataTable DT_SyncMeasurement = new DataTable();
        DataSet DS = new DataSet();
        ObjSyncMeasurement.PressRunID = Convert.ToInt32(pressrunid);
        DS = ObjSyncMeasurement.uspGetMeasurementByPressRunID();
        if (DS != null && DS.Tables.Count != 0 && DS.Tables[0].Rows.Count != 0)
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "BookMarkScript", "document.getElementById('" + BtnNextAnalyze1.ClientID + "').click();", true);
        else
            lblAnalyzeMsg.Text = "Please upload at least one measurement file.";
    }
    /// <summary>
    /// "Next Analyze" button in Add Measurement step for goto next analyze step.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnNextAnalyze1_Click(object sender, EventArgs e)
    {
        try
        {
            //============= Get meft menu from session =======================//
            List<string> obj = (List<string>)Fuji.Common.Sessions.GetSession("SyncLeftMenu");

            string PressLeftManuData = "";

            //=========== Create 1. Add Measurement,2. Analyze (Active),3. Export Curves step in Left menu and call javascript function ========//
            PressLeftManuData = "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table><table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE'><tr><td>Select Action :</td></tr><tr><td></td></tr></table>";
            PressLeftManuData += "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SUBSYNCTABLELEFTTABLE'><tr><td onclick='CallMeasurementBind()' style='cursor:pointer'><br/><b>1. Add Measurement</b></td></tr><tr><td><br/><b>2. Analyze</b></td></tr><tr><td><br/>3. Export Curves</td></tr><tr><td><br/>&nbsp;</td></tr></table>";
            obj.Insert(3, PressLeftManuData);

            //========== Set all step in left menu ================//
            LtrLeftNevigation.Text = obj[0] + obj[1] + obj[2] + obj[3];

            //=========== Set list menu object in to session ===========================//
            Fuji.Common.Sessions.SetSessions("SyncLeftMenu", obj);


            //changed by chirag on 15th Oct 2010, if it is fourth iteration do not show create curves button
            //the fourth iteration is only for verification 
            DataTable DT_SyncPressRun = new DataTable();
            int currInteration = -1;
            CPSyncPressRunData ObjSyncPressRun = new CPSyncPressRunData();
            DT_SyncPressRun = ObjSyncPressRun.uspGetProjectStatus(Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString()));
            if (DT_SyncPressRun != null && DT_SyncPressRun.Rows.Count > 0)
            {
                currInteration = Convert.ToInt32(DT_SyncPressRun.Rows[0]["Iteration"]);
                if (currInteration == 4) this.BtnNextCreateCurves.Visible = false;
            }



            //========= Visible Analyze step =======//
            TableVisibleTrueFalse(TblAnalyze);

            HDCurrentMeasurement.Value = "";

            //======== Bind Trend Line Delta Chart =======//
            FillTrendLineDeltaEChart();



        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    #endregion

    #region Analyze
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBindAnalyze_Click(object sender, EventArgs e)
    {
        try
        {
            //============= Get meft menu from session =======================//
            List<string> obj = (List<string>)Fuji.Common.Sessions.GetSession("SyncLeftMenu");

            string PressLeftManuData = "";

            //=========== Create 1. Add Measurement,2. Analyze (Active),3. Export Curves step in Left menu and call javascript function ========//
            PressLeftManuData = "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table><table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE'><tr><td>Select Action :</td></tr><tr><td></td></tr></table>";
            //PressLeftManuData += "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SUBSYNCTABLELEFTTABLE'><tr><td onclick='CallMeasurementBind()' style='cursor:pointer'><br/><b>1. Add Measurement</b></td></tr><tr><td><br/><b>2. Analyze</b></td></tr><tr><td><br/>3. Export Curves</td></tr><tr><td><br/>&nbsp;</td></tr></table>";
            PressLeftManuData += "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SUBSYNCTABLELEFTTABLE'><tr><td><br/><b>1. Add Measurement</b></td></tr><tr><td><br/><b>2. Analyze</b></td></tr><tr><td><br/>3. Export Curves</td></tr><tr><td><br/>&nbsp;</td></tr></table>";
            obj.Insert(3, PressLeftManuData);

            //========== Set all step in left menu ================//
            LtrLeftNevigation.Text = obj[0] + obj[1] + obj[2] + obj[3];

            //=========== Set list menu object in to session ===========================//
            Fuji.Common.Sessions.SetSessions("SyncLeftMenu", obj);

            //========= Visible Analyze step =======//
            TableVisibleTrueFalse(TblAnalyze);



            HDCurrentMeasurement.Value = "";

            //======== Bind Trend Line Delta Chart =======//
            FillTrendLineDeltaEChart();

        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    /// <summary>
    /// This click button event fire from javascript function when click on point or check box in chart.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void HDButCurrentMeasurement_Click(object sender, EventArgs e)
    {
        try
        {

            //======== Bind Trend Line E Chart =================//
            FillTrendLineDeltaEChart();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    /// <summary>
    /// This click button event fire from javascript function when click on point or check box in chart.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void HDButTrendDelta_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Declaring Objects ==========//
            CPSyncMeasurementData objCPSyncMeasurement = new CPSyncMeasurementData();

            //========== Updateing IsUsed Value ==========//
            objCPSyncMeasurement.MeasurementID = Convert.ToInt32(HDTrendDelta.Value);
            objCPSyncMeasurement.uspUpdateIsUsedMeasurement();

            //======== Bind Trend Line E Chart =================//
            FillTrendLineDeltaEChart();



        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnNextCreateCurves_Click(object sender, EventArgs e)
    {
        string SelectedMeasurementIDs = string.Empty;
        try
        {
            if (HDTrendDeltaAll.Value.Trim() != "")
            {
                SelectedMeasurementIDs = HDTrendDeltaAll.Value.ToString().Replace("[", "").Replace("]", "");
                SelectedMeasurementIDs = SelectedMeasurementIDs.Substring(0, SelectedMeasurementIDs.Length - 1);
            }
            else
            {
                return;
            }
            if (Fuji.Common.Sessions.GetSession("SelectedPressRunID") != null)
            {
                if (Fuji.Common.Sessions.GetSession("SelectedPressRunClose").ToString() == Convert.ToInt16(Enums.CPSyncPressRunStatus.Open).ToString())
                {
                    CPSync.GetCurve objGetCurve = null;
                    int PressRunID = Convert.ToInt32(Fuji.Common.Sessions.GetSession("SelectedPressRunID"));

                    if (SelectedMeasurementIDs != String.Empty)
                    {
                        objGetCurve = new CPSync.GetCurve(SelectedMeasurementIDs, PressRunID);
                        if (Fuji.Common.Sessions.GetSession("SelectedProjectTypeID").ToString() == Convert.ToInt16(Enums.ProjectType.G7).ToString())
                            objGetCurve = new CPSync.GetCurve(SelectedMeasurementIDs, PressRunID);
                        else
                        {
                            objGetCurve = new CPSync.GetCurve(SelectedMeasurementIDs, PressRunID);
                            objGetCurve.ProjectID = Convert.ToInt32(Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString());
                        }
                    }

                    //delete existing curve Data
                    objGetCurve.DeleteCurve();

                    string status = string.Empty;
                    //Create and save curve data
                    if (Fuji.Common.Sessions.GetSession("SelectedProjectTypeID").ToString() == Convert.ToInt16(Enums.ProjectType.G7).ToString())
                        status = objGetCurve.CreateCurve();
                    else
                        status = objGetCurve.CreateISOCurve();

                    //Status should be “OK”; anything else we need to display error message.
                    if (status != "OK")
                    {
                        //put a label on the page and display the error message  (i.e. status)
                        return;
                    }
                }

                //============= Get meft menu from session =======================//
                List<string> obj = (List<string>)Fuji.Common.Sessions.GetSession("SyncLeftMenu");

                string PressLeftManuData = "";

                //=========== Create 1. Add Measurement,2. Analyze (Active),3. Export Curves step in Left menu and call javascript function ========//
                PressLeftManuData = "<table border='0' cellpadding='0' cellspacing='7' width='100%'><tr><td align='center'><img src='Images/leftnavbottomarrow.gif' /></td></tr></table><table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE'><tr><td>Select Action :</td></tr><tr><td></td></tr></table>";
                PressLeftManuData += "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SUBSYNCTABLELEFTTABLE'><tr><td onclick='CallMeasurementBind()' style='cursor:pointer'><br/><b>1. Add Measurement</b></td></tr><tr><td onclick='CallAnalyzetBind()' style='cursor:pointer'><br/><b>2. Analyze</b></td></tr><tr><td><br/><b>3. Export Curves</b></td></tr><tr><td><br/>&nbsp;</td></tr></table>";
                obj.Insert(3, PressLeftManuData);

                //========== Set all step in left menu ================//
                LtrLeftNevigation.Text = obj[0] + obj[1] + obj[2] + obj[3];

                //=========== Set list menu object in to session ===========================//
                Fuji.Common.Sessions.SetSessions("SyncLeftMenu", obj);

                //========= Visible Analyze step =======//
                TableVisibleTrueFalse(TblExportCurves);

                BindSyncMeasurementData();
                BindSyncCurveDataAndChart(Convert.ToInt16(DdlCurveType.SelectedValue));

                if (Fuji.Common.Sessions.GetSession("SelectedPressRunClose").ToString() == Convert.ToInt16(Enums.CPSyncPressRunStatus.Closed).ToString() || Fuji.Common.Sessions.GetSession("SelectedPressRunClose").ToString() == Convert.ToInt16(Enums.CPSyncPressRunStatus.Certify).ToString())
                    BtnClosePrintRun.Visible = false;
                else
                    BtnClosePrintRun.Visible = true;
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnGenerateReport_Click(object sender, EventArgs e)
    {
        //========= Visible Analyze step =======//
        TableVisibleTrueFalse(TblGeneratePDFReport);

        FRMGeneratePDFReport.Attributes.Add("src", "SyncCertifyPressRunPopup.aspx");
        FRMGeneratePDFReport.Style.Add("display", "");
    }


    /// <summary>
    /// This click Analyze Average Of Measurements checkbox to set agerage measurements.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkAnalyzeAverageOfMeasurements_Click(object sender, EventArgs e)
    {
        try
        {
            //======== Bind Trend Line E Chart =================//
            FillTrendLineDeltaEChart();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    #endregion

    #region Export Curves
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DdlCurveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSyncCurveDataAndChart(Convert.ToInt16(DdlCurveType.SelectedValue));
        }
        catch (Exception Exc)
        {
            LblError.Text = Exc.Message;
        }
    }
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void Chk_LikeAveragesChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        FillTrendLineDeltaEChart();
    //    }
    //    catch (Exception Exc)
    //    {
    //        LblError.Text = Exc.Message;
    //    }

    //}


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Chk_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            BindSyncCurveDataAndChart(Convert.ToInt16(DdlCurveType.SelectedValue));
            UpdPaDeltaView.Update();
        }
        catch (Exception Exc)
        {
            LblError.Text = Exc.Message;
        }

    }
    /// <summary>
    /// Export Device Link Profile (.icc file)
    /// </summary>    
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportProfileExportView_Click(object sender, EventArgs e)
    {
        try
        {
            CPSyncPressRunData objCPSyncPressRun = new CPSyncPressRunData();
            System.Data.SqlClient.SqlDataReader objDR = null;
            byte[] Profile = null;

            objCPSyncPressRun.PressRunID = Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressRunID"));
            objDR = objCPSyncPressRun.uspGetICCProfile();
            if (objDR != null && objDR.HasRows == true)
            {
                objDR.Read();
                Profile = (byte[])objDR["ICCProfile"];

            }


            //Prompt for download file
            if (Profile != null && Profile.Length > 0)
            {
                Fuji.Common.CommonFunctions.DownloadFile(Profile, "Profile.icc");
            }

        }
        catch (Exception Exc)
        {
            LblError.Text = Exc.Message;
        }
    }
    /// <summary>
    /// Download curve txt file for 1%,5%,10% and Rampage.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnExportCurvesExportView_Click(object sender, EventArgs e)
    {
        try
        {
            BindSyncCurveDataAndChart(Convert.ToInt16(DdlCurveType.SelectedValue));
            if (Fuji.Common.Sessions.GetSession("DTCurvesDataExportView") != null)
            {
                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile")))
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile"));

                //==========  Get Data Table and Bind ==========//
                DataTable DT_CPSyncPressRunCurve = new DataTable();
                DT_CPSyncPressRunCurve = (DataTable)Fuji.Common.Sessions.GetSession("DTCurvesDataExportView");

                if (DdlCurveType.SelectedValue == "1" || DdlCurveType.SelectedValue == "5" || DdlCurveType.SelectedValue == "10")
                {
                    #region Create file and download txt file for 1%,5% and 10% 
                    System.IO.StreamWriter sw;
                    sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_CurvesFile.txt");
                    //const string quote = "\"";
                    sw.WriteLine("\"Fujifilm Colorpath Sync 1D\"");
                    sw.WriteLine("BEGIN_DATA_FORMAT");
                    //sw.WriteLine("Reference\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K");
                    sw.WriteLine("SampleID\tSampleName\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K");
                    sw.WriteLine("END_DATA_FORMAT");
                    sw.WriteLine("NUMBER_OF_SETS\t" + DT_CPSyncPressRunCurve.Rows.Count.ToString());
                    sw.WriteLine("BEGIN_DATA");
                    for (int i = 0; i < DT_CPSyncPressRunCurve.Rows.Count; i++)
                    {
                        sw.WriteLine("A" + i.ToString() + "\t\"" + DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"].ToString() + ".00" + "\"\t" + DT_CPSyncPressRunCurve.Rows[i]["CMYK_C"].ToString() + "\t" + DT_CPSyncPressRunCurve.Rows[i]["CMYK_M"].ToString() + "\t" + DT_CPSyncPressRunCurve.Rows[i]["CMYK_Y"].ToString() + "\t" + DT_CPSyncPressRunCurve.Rows[i]["CMYK_K"].ToString());
                    }
                    sw.WriteLine("END_DATA");
                    sw.Close();

                    Fuji.Common.CommonFunctions.DownlaodFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), Sessions.UserID + "_CurvesFile.txt", Sessions.UserID + "_CurvesFile.txt");
                    #endregion
                }
                else if (DdlCurveType.SelectedValue == "15")  // Rampage
                {
                    #region Create Zip file and download file for rampage
                    System.IO.StreamWriter swC;
                    System.IO.StreamWriter swM;
                    System.IO.StreamWriter swY;
                    System.IO.StreamWriter swK;
                    swC = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_C.oli");
                    swC.WriteLine("2");
                    swC.WriteLine("0.0000000000");
                    swC.WriteLine("0.0000000000");
                    swC.WriteLine("25");

                    swM = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_M.oli");
                    swM.WriteLine("2");
                    swM.WriteLine("0.0000000000");
                    swM.WriteLine("0.0000000000");
                    swM.WriteLine("25");

                    swY = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Y.oli");
                    swY.WriteLine("2");
                    swY.WriteLine("0.0000000000");
                    swY.WriteLine("0.0000000000");
                    swY.WriteLine("25");

                    swK = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_K.oli");
                    swK.WriteLine("2");
                    swK.WriteLine("0.0000000000");
                    swK.WriteLine("0.0000000000");
                    swK.WriteLine("25");

                    for (int i = 0; i < DT_CPSyncPressRunCurve.Rows.Count; i++)
                    {
                        swC.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.0") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_C"]).ToString("0.0"));
                        swM.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.0") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_M"]).ToString("0.0"));
                        swY.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.0") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_Y"]).ToString("0.0"));
                        swK.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.0") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_K"]).ToString("0.0"));
                    }
                    swC.WriteLine("Version: 2.0");
                    swC.Close();

                    swM.WriteLine("Version: 2.0");
                    swM.Close();

                    swY.WriteLine("Version: 2.0");
                    swY.Close();

                    swK.WriteLine("Version: 2.0");
                    swK.Close();

                    Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_CMYKFile.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_C.oli");
                    Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_CMYKFile.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_M.oli");
                    Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_CMYKFile.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Y.oli");
                    Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_CMYKFile.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_K.oli");

                    Fuji.Common.CommonFunctions.DownlaodFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), Sessions.UserID + "_CMYKFile.zip", Sessions.UserID + "_CMYKFile.zip");
                    #endregion
                }
                else if (DdlCurveType.SelectedValue == "35")  // Harmony (Prinergy)
                {
                    #region Create Zip file and download file for rampage
                    System.IO.StreamWriter swC;
                    System.IO.StreamWriter swM;
                    System.IO.StreamWriter swY;
                    System.IO.StreamWriter swK;

                    //cyan
                    swC = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_C.txt");

                    //magenta
                    swM = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_M.txt");

                    //yellow    
                    swY = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Y.txt");


                    //black
                    swK = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_K.txt");


                    for (int i = 0; i < DT_CPSyncPressRunCurve.Rows.Count; i++)
                    {
                        swC.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.00") + "\t" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_C"]).ToString("0.00"));
                        swM.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.00") + "\t" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_M"]).ToString("0.00"));
                        swY.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.00") + "\t" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_Y"]).ToString("0.00"));
                        swK.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.00") + "\t" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_K"]).ToString("0.00"));
                    }

                    swC.Close();
                    swM.Close();
                    swY.Close();
                    swK.Close();

                    Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Harmony.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_C.txt");
                    Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Harmony.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_M.txt");
                    Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Harmony.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Y.txt");
                    Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Harmony.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_K.txt");

                    Fuji.Common.CommonFunctions.DownlaodFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), Sessions.UserID + "_Harmony.zip", Sessions.UserID + "_Harmony.zip");
                    #endregion
                }
                else if (DdlCurveType.SelectedValue == "25")  // for XMF added by chirag on 11/02/2010
                {
                    #region Create file and download txt file for XMF
                    System.IO.StreamWriter sw;
                    sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_CurvesFile.csv");
                    sw.WriteLine("Cyan;Magenta;Yellow;Black");
                    for (int i = 0; i < DT_CPSyncPressRunCurve.Rows.Count; i++)
                    {
                        sw.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_C"]).ToString("0.0000") + ";" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_M"]).ToString("0.0000") + ";" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_Y"]).ToString("0.0000") + ";" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_K"]).ToString("0.0000"));
                    }
                    sw.Close();

                    Fuji.Common.CommonFunctions.DownlaodFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), Sessions.UserID + "_CurvesFile.csv", Sessions.UserID + "_CurvesFile.csv");
                    #endregion

                }
                else if (DdlCurveType.SelectedValue == "45")  // nexus
                {
                    #region Create Zip file and download file for nexus

                    //check if file exists and delete it
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Nexus.zip") == true)
                        System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Nexus.zip");

                    System.IO.StreamWriter swC;
                    System.IO.StreamWriter swM;
                    System.IO.StreamWriter swY;
                    System.IO.StreamWriter swK;
                    swC = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Nexus_Cya.txt");
                    swM = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Nexus_Mag.txt");
                    swY = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Nexus_Yel.txt");
                    swK = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Nexus_Bla.txt");


                    for (int i = 0; i < DT_CPSyncPressRunCurve.Rows.Count; i++)
                    {
                        swC.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.0000") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_C"]).ToString("0.0000"));
                        swM.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.0000") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_M"]).ToString("0.0000"));
                        swY.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.0000") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_Y"]).ToString("0.0000"));
                        swK.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.0000") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_K"]).ToString("0.0000"));
                    }

                    swC.Close();
                    swM.Close();
                    swY.Close();
                    swK.Close();

                    Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Nexus.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Nexus_Cya.txt");
                    Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Nexus.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Nexus_Mag.txt");
                    Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Nexus.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Nexus_Yel.txt");
                    Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Nexus.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_Nexus_Bla.txt");

                    Fuji.Common.CommonFunctions.DownlaodFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), Sessions.UserID + "_Nexus.zip", Sessions.UserID + "_Nexus.zip");
                    #endregion
                }
                else if (DdlCurveType.SelectedValue == "55")  // for sericol UK Caldera
                {
                    #region Create file and download txt file for sericol UK
                    System.IO.StreamWriter sw;
                    sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_CurvesFile.ppc");
                    sw.WriteLine("Device = CMYK");
                    sw.WriteLine("Max ink = 400");
                    sw.WriteLine("Max ink reduction = preserve black");

                    string strCyan = "";
                    for (int i = 0; i < DT_CPSyncPressRunCurve.Rows.Count; i++)
                    {

                        strCyan = strCyan + "(" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.0") + " " + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_C"]).ToString("0.0") + ") , ";

                    }
                    strCyan = strCyan.Remove(strCyan.Length - 3, 3);
                    sw.WriteLine("Cyan curve = " + strCyan);

                    string strMagenta = "";
                    for (int i = 0; i < DT_CPSyncPressRunCurve.Rows.Count; i++)
                    {

                        strMagenta = strMagenta + "(" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.0") + " " + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_M"]).ToString("0.0") + ") , ";

                    }
                    strMagenta = strMagenta.Remove(strMagenta.Length - 3, 3);
                    sw.WriteLine("Magenta curve = " + strMagenta);

                    string strYellow = "";
                    for (int i = 0; i < DT_CPSyncPressRunCurve.Rows.Count; i++)
                    {

                        strYellow = strYellow + "(" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.0") + " " + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_Y"]).ToString("0.0") + ") , ";

                    }
                    strYellow = strYellow.Remove(strYellow.Length - 3, 3);
                    sw.WriteLine("Yellow curve = " + strYellow);

                    string strBlack = "";
                    for (int i = 0; i < DT_CPSyncPressRunCurve.Rows.Count; i++)
                    {

                        strBlack = strBlack + "(" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"]).ToString("0.0") + " " + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[i]["CMYK_K"]).ToString("0.0") + ") , ";

                    }
                    strBlack = strBlack.Remove(strBlack.Length - 3, 3);
                    sw.WriteLine("Black curve = " + strBlack);


                    sw.Close();

                    Fuji.Common.CommonFunctions.DownlaodFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), Sessions.UserID + "_CurvesFile.ppc", Sessions.UserID + "_CurvesFile.ppc");
                    #endregion

                }
                else if (DdlCurveType.SelectedValue == "65")  // ΔRef
                {
                    #region Create file and download txt file for 1%,5% and 10%
                    System.IO.StreamWriter sw;
                    sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_CurvesFile.txt");
                    sw.WriteLine("BEGIN_DATA_FORMAT");
                    sw.WriteLine("Reference\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K");
                    sw.WriteLine("END_DATA_FORMAT");
                    sw.WriteLine("BEGIN_DATA");
                    for (int i = 0; i < DT_CPSyncPressRunCurve.Rows.Count; i++)
                    {
                        sw.WriteLine(DT_CPSyncPressRunCurve.Rows[i]["ReferenceNo"].ToString() + "\t" + DT_CPSyncPressRunCurve.Rows[i]["CMYK_C"].ToString() + "\t" + DT_CPSyncPressRunCurve.Rows[i]["CMYK_M"].ToString() + "\t" + DT_CPSyncPressRunCurve.Rows[i]["CMYK_Y"].ToString() + "\t" + DT_CPSyncPressRunCurve.Rows[i]["CMYK_K"].ToString());
                    }
                    sw.WriteLine("END_DATA");
                    sw.Close();

                    Fuji.Common.CommonFunctions.DownlaodFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), Sessions.UserID + "_CurvesFile.txt", Sessions.UserID + "_CurvesFile.txt");
                    #endregion

                }
                else if (DdlCurveType.SelectedValue == "100")  // for ColorGate-TVI
                {
                    #region Create file and download .ctvx file for ColorGate-TVI

                    //-------For curvetype=100-------Make .ctvx XML file for ColorGate-TVI (Dhruti's Code)-------------------//
                   
                    //-----------------Pass few parameters in "" marks to .ctvx xml file------------------//
                    string str0 = "\"" + "1.0" + "\"";
                    string str1 = "\"" + "UTF-8" + "\"";
                    string str2 = "\"" + "urn:schemas-colorgate-com:tonalvalues" + "\"";
                    string str3 = "\"" + "1" + "\"";

                    //-----------------Make .ctvx xml file------------------//
                    System.IO.StreamWriter sw3;
                    sw3 = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_CurveDelta.ctvx");
                    sw3.WriteLine("<?xml version=" + str0 + " encoding=" + str1 + "?>");
                    sw3.WriteLine("<TonalValues xmlns=" + str2 + ">");

                    if (DT_CPSyncPressRunCurve.Rows.Count > 19)
                    {
                        sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>0</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[2]["CMYK_K"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[2]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[5]["CMYK_K"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[5]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[10]["CMYK_K"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[10]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[15]["CMYK_K"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[15]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[18]["CMYK_K"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[18]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>0</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                        sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>1</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[2]["CMYK_C"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[2]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[5]["CMYK_C"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[5]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[10]["CMYK_C"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[10]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[15]["CMYK_C"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[15]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[18]["CMYK_C"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[18]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>0</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                        sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>2</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[2]["CMYK_M"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[2]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[5]["CMYK_M"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[5]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[10]["CMYK_M"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[10]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[15]["CMYK_M"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[15]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[18]["CMYK_M"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[18]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>0</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                        sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>3</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[2]["CMYK_Y"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[2]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[5]["CMYK_Y"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[5]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[10]["CMYK_Y"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[10]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[15]["CMYK_Y"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[15]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[18]["CMYK_Y"].ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[18]["ReferenceNo"].ToString())) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>0</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                    }
                    sw3.WriteLine("</TonalValues>");
                    sw3.Close();

                    Fuji.Common.CommonFunctions.DownlaodFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), Sessions.UserID + "_CurveDelta.ctvx", Sessions.UserID + "_CurveDelta.ctvx");
                    
                    #endregion
                }
                else if (DdlCurveType.SelectedValue == "101")  // for ColorGate-CGCX
                {
                    #region Create file and download .cgcx file for ColorGate-CGCX

                    //-------For curvetype=101-------Make .cgcx XML file for ColorGate-CGCX (Dhruti's Code)-------------------//

                    //-----------------Pass few parameters in "" marks to .cgcx xml file------------------//
                    string str0 = "\"" + "1.0" + "\"";
                    string str1 = "\"" + "UTF-8" + "\"";
                    string str2 = "\"" + "urn:schemas-colorgate-com:gradation" + "\"";
                    string str3 = "\"" + "1" + "\"";

                    //-----------------Make .cgcx xml file------------------//
                    System.IO.StreamWriter sw3;
                    sw3 = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_CurveDelta.cgcx");
                    sw3.WriteLine("<?xml version=" + str0 + " encoding=" + str1 + "?>");
                    sw3.WriteLine("<GradationCurves xmlns=" + str2 + ">");

                    if (DT_CPSyncPressRunCurve.Rows.Count > 19)
                    {
                        sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>0</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[2]["CMYK_K"].ToString()) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[5]["CMYK_K"].ToString()) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[10]["CMYK_K"].ToString()) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[15]["CMYK_K"].ToString()) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[18]["CMYK_K"].ToString()) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                        sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>1</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[2]["CMYK_C"].ToString()) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[5]["CMYK_C"].ToString()) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[10]["CMYK_C"].ToString()) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[15]["CMYK_C"].ToString()) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[18]["CMYK_C"].ToString()) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                        sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>2</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[2]["CMYK_M"].ToString()) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[5]["CMYK_M"].ToString()) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[10]["CMYK_M"].ToString()) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[15]["CMYK_M"].ToString()) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[18]["CMYK_M"].ToString()) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                        sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>3</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[2]["CMYK_Y"].ToString()) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[5]["CMYK_Y"].ToString()) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[10]["CMYK_Y"].ToString()) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[15]["CMYK_Y"].ToString()) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve.Rows[18]["CMYK_Y"].ToString()) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                        sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>4</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer><ColorTansfer version=" + str3 + "><ChannelID>5</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer><ColorTansfer version=" + str3 + "><ChannelID>6</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer><ColorTansfer version=" + str3 + "><ChannelID>7</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer><ColorTansfer version=" + str3 + "><ChannelID>8</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                    }
                    sw3.WriteLine("</GradationCurves>");
                    sw3.Close();

                    Fuji.Common.CommonFunctions.DownlaodFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), Sessions.UserID + "_CurveDelta.cgcx", Sessions.UserID + "_CurveDelta.cgcx");

                    #endregion
                }
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = Exc.Message;
        }
    }
    /// <summary>
    /// Export Device Link Profile (.icc file)
    /// </summary>    
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportProfileDeltaView_Click(object sender, EventArgs e)
    {
        try
        {
            CPSyncPressRunData objCPSyncPressRun = new CPSyncPressRunData();
            System.Data.SqlClient.SqlDataReader objDR = null;
            byte[] Profile = null;

            objCPSyncPressRun.PressRunID = Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressRunID"));
            objDR = objCPSyncPressRun.uspGetICCProfile();
            if (objDR != null && objDR.HasRows == true)
            {
                objDR.Read();
                Profile = (byte[])objDR["ICCProfile"];

            }


            //Prompt for download file
            if (Profile != null && Profile.Length > 0)
            {
                Fuji.Common.CommonFunctions.DownloadFile(Profile, "Profile.icc");
            }

        }
        catch (Exception Exc)
        {
            LblError.Text = Exc.Message;
        }
    }
    /// <summary>
    /// Download curve txt file for 1%,5%,10% and Rampage.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnExportCurvesDeltaView_Click(object sender, EventArgs e)
    {
        try
        {
            //Chirag To do
            //first find which radio button is clicked 

            DataTable DT_CurveDelta = new DataTable();
            if (rbtnDeltaPrev.Enabled = true && rbtnDeltaPrev.Checked == true)
            {
                DT_CurveDelta = (DataTable)Fuji.Common.Sessions.GetSession("DTDeltaPrev");
            }
            else { DT_CurveDelta = (DataTable)Fuji.Common.Sessions.GetSession("DTDeltaRef"); }


            if (DT_CurveDelta != null)
            {
                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile")))
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile"));

                //---------------For all Curve Types -------------------//
                System.IO.StreamWriter sw;
                sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + Sessions.UserID + "_CurveDelta.txt");
                sw.WriteLine("BEGIN_DATA_FORMAT");
                sw.WriteLine("Reference\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K");
                sw.WriteLine("END_DATA_FORMAT");
                sw.WriteLine("BEGIN_DATA");
                for (int i = 0; i < DT_CurveDelta.Rows.Count; i++)
                {
                    sw.WriteLine(DT_CurveDelta.Rows[i]["ReferenceNo"].ToString() + "\t" + DT_CurveDelta.Rows[i]["CMYK_C"].ToString() + "\t" + DT_CurveDelta.Rows[i]["CMYK_M"].ToString() + "\t" + DT_CurveDelta.Rows[i]["CMYK_Y"].ToString() + "\t" + DT_CurveDelta.Rows[i]["CMYK_K"].ToString());
                }
                sw.WriteLine("END_DATA");
                sw.Close();

                Fuji.Common.CommonFunctions.DownlaodFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), Sessions.UserID + "_CurveDelta.txt", Sessions.UserID + "_CurveDelta.txt");
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = Exc.Message;
        }
    }

    /// <summary>
    /// Close print run button in curve tab for update status of press run.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnClosePrintRun_Click(object sender, EventArgs e)
    {
        try
        {
            //============ Create object of CPSyncPressRunData =============//
            CPSyncPressRunData objCPSyncPressRun = new CPSyncPressRunData();
            objCPSyncPressRun.PressRunID = Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressRunID"));
            objCPSyncPressRun.Status = 2;
            objCPSyncPressRun.EndDateTime = Convert.ToDateTime(HDClientDatatime.Value.ToString());
            objCPSyncPressRun.uspUpdatePressRunStatus(); // Update status=2 for close press run.
            btnBindPressRunGrid_Click(null, null); // Go to press run tab

        }
        catch (Exception Exc)
        {
            LblError.Text = Exc.Message;
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Used to bind the Sync Press Grid
    /// </summary>
    private void BindSyncMeasurementData()
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPSyncMeasurementData ObjSyncMeasurement = new CPSyncMeasurementData();

            //==========  Get Data Table and Bind ==========//
            DataSet DT_SyncMeasurement = new DataSet();

            ObjSyncMeasurement.PressRunID = Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressRunID"));
            DT_SyncMeasurement = ObjSyncMeasurement.uspGetMeasurementByPressRunID();

            //========== Assign the session data to Grid ===========//
            if (DT_SyncMeasurement != null && DT_SyncMeasurement.Tables[0].Rows.Count > 0)
            { RG_SyncMeasurement.DataSource = DT_SyncMeasurement; RG_SyncMeasurement.DataBind(); }
            else
            { RG_SyncMeasurement.DataSource = null; RG_SyncMeasurement.DataBind(); }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    /// <summary>
    /// Bind Curves CMYK grid and chart.
    /// </summary>
    private void BindSyncCurveDataAndChart(int _CurveTypeID)
    {
        try
        {
                        //-------Start-----Pass ColorGate same as 5% data--------------//
                        if (_CurveTypeID == 100 || _CurveTypeID == 101)
                        {
                            _CurveTypeID = 5;
                        }
                        //-----------------End-----------//

            //==========  Declaring Objects ==========//
            CPSyncPressRunCurveData ObjCPSyncPressRunCurve = new CPSyncPressRunCurveData();

            //==========  Get Data Table and Bind ==========//
            DataSet DS_CPSyncPressRunCurve = new DataSet();
            DataTable DTExportView = new DataTable();
            DataTable DTDeltaRef = new DataTable();
            DataTable DTDeltaPrev = new DataTable();
            Int64 MaxDeltaRef = 0;
            Int64 MinDeltaRef = 0;
            Int64 MaxDeltaPrev = 0;
            Int64 MinDeltaPrev = 0;


            ObjCPSyncPressRunCurve.PressRunID = Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressRunID"));
            DS_CPSyncPressRunCurve = ObjCPSyncPressRunCurve.uspGetCurveData(_CurveTypeID);


            rbtnDetlaRef.Checked = true;
            rbtnDeltaPrev.Checked = false;
            if (DS_CPSyncPressRunCurve != null)
            {
                if (DS_CPSyncPressRunCurve.Tables.Count > 0 && DS_CPSyncPressRunCurve.Tables[0] != null)
                {
                    DTExportView = DS_CPSyncPressRunCurve.Tables[0];
                }
                if (DS_CPSyncPressRunCurve.Tables.Count > 1 && DS_CPSyncPressRunCurve.Tables[1] != null)
                {
                    DTDeltaRef = DS_CPSyncPressRunCurve.Tables[1];
                    Fuji.Common.Sessions.SetSessions("DTDeltaRef", DTDeltaRef);
                }
                if (DS_CPSyncPressRunCurve.Tables.Count > 2 && DS_CPSyncPressRunCurve.Tables[2] != null)
                {
                    rbtnDeltaPrev.Enabled = true;
                    DTDeltaPrev = DS_CPSyncPressRunCurve.Tables[2];
                    Fuji.Common.Sessions.SetSessions("DTDeltaPrev", DTDeltaPrev);
                }
                else
                {
                    rbtnDeltaPrev.Enabled = false;
                }
            }

            #region Export View
            Fuji.Common.Sessions.SetSessions("DTCurvesDataExportView", DTExportView);
            //========== Assign the session data to Grid ===========//
            if (DTExportView != null && DTExportView.Rows.Count > 0)
            {

                if (_CurveTypeID != 65)
                {
                    #region BindChart
                    for (int i = 0; i < DTExportView.Rows.Count; i++)
                    {

                        if (Chk_C.Checked == true)
                            AddPointChartWithColor(i, CHRT_ExportCurve.Series["Series1"], DTExportView.Rows[i]["ReferenceNo"].ToString(), Convert.ToInt64(DTExportView.Rows[i]["CMYK_C"]).ToString(), Color.Cyan.Name.ToString(), true);
                        if (Chk_M.Checked == true)
                            AddPointChartWithColor(i, CHRT_ExportCurve.Series["Series2"], DTExportView.Rows[i]["ReferenceNo"].ToString(), Convert.ToInt64(DTExportView.Rows[i]["CMYK_M"]).ToString(), Color.Magenta.Name.ToString(), true);
                        if (Chk_Y.Checked == true)
                            AddPointChartWithColor(i, CHRT_ExportCurve.Series["Series3"], DTExportView.Rows[i]["ReferenceNo"].ToString(), Convert.ToInt64(DTExportView.Rows[i]["CMYK_Y"]).ToString(), Color.Yellow.Name.ToString(), true);
                        if (Chk_K.Checked == true)
                            AddPointChartWithColor(i, CHRT_ExportCurve.Series["Series4"], DTExportView.Rows[i]["ReferenceNo"].ToString(), Convert.ToInt64(DTExportView.Rows[i]["CMYK_K"]).ToString(), Color.Black.Name.ToString(), true);
                    }
                    #endregion
                }
                RGPressRunCurve.DataSource = DTExportView;
                RGPressRunCurve.DataBind();
            }
            else
            { RGPressRunCurve.DataSource = null; RGPressRunCurve.DataBind(); }
            #endregion

            #region Delta Ref
            //========== Assign the session data to Grid ===========//
            if (DTDeltaRef != null && DTDeltaRef.Rows.Count > 0)
            {
                #region Fine Max Min
                if (Convert.ToInt64(DTDeltaRef.Compute("Max(CMYK_C)", string.Empty)) > MaxDeltaRef)
                    MaxDeltaRef = Convert.ToInt64(DTDeltaRef.Compute("Max(CMYK_C)", string.Empty));
                if (Convert.ToInt64(DTDeltaRef.Compute("Max(CMYK_M)", string.Empty)) > MaxDeltaRef)
                    MaxDeltaRef = Convert.ToInt64(DTDeltaRef.Compute("Max(CMYK_M)", string.Empty));
                if (Convert.ToInt64(DTDeltaRef.Compute("Max(CMYK_Y)", string.Empty)) > MaxDeltaRef)
                    MaxDeltaRef = Convert.ToInt64(DTDeltaRef.Compute("Max(CMYK_Y)", string.Empty));
                if (Convert.ToInt64(DTDeltaRef.Compute("Max(CMYK_K)", string.Empty)) > MaxDeltaRef)
                    MaxDeltaRef = Convert.ToInt64(DTDeltaRef.Compute("Max(CMYK_K)", string.Empty));

                if (Convert.ToInt64(DTDeltaRef.Compute("Min(CMYK_C)", string.Empty)) < MinDeltaRef)
                    MinDeltaRef = Convert.ToInt64(DTDeltaRef.Compute("Min(CMYK_C)", string.Empty));
                if (Convert.ToInt64(DTDeltaRef.Compute("Min(CMYK_M)", string.Empty)) < MinDeltaRef)
                    MinDeltaRef = Convert.ToInt64(DTDeltaRef.Compute("Min(CMYK_M)", string.Empty));
                if (Convert.ToInt64(DTDeltaRef.Compute("Min(CMYK_Y)", string.Empty)) < MinDeltaRef)
                    MinDeltaRef = Convert.ToInt64(DTDeltaRef.Compute("Min(CMYK_Y)", string.Empty));
                if (Convert.ToInt64(DTDeltaRef.Compute("Min(CMYK_K)", string.Empty)) < MinDeltaRef)
                    MinDeltaRef = Convert.ToInt64(DTDeltaRef.Compute("Min(CMYK_K)", string.Empty));

                MinDeltaRef = (MinDeltaRef * -1);

                MaxDeltaRef = MaxDeltaRef + 1;
                MinDeltaRef = MinDeltaRef + 1;

                if (MaxDeltaRef > MinDeltaRef)
                {
                    MinDeltaRef = (MaxDeltaRef * -1);
                }
                else
                {
                    MaxDeltaRef = MinDeltaRef;
                    MinDeltaRef = (MinDeltaRef * -1);
                }
                CHRT_ExportCurveDeltaRef.ChartAreas["Default"].AxisY.Minimum = MinDeltaRef;
                CHRT_ExportCurveDeltaRef.ChartAreas["Default"].AxisY.Maximum = MaxDeltaRef;
                #endregion
                if (_CurveTypeID != 65)
                {
                    #region BindChart
                    for (int i = 0; i < DTDeltaRef.Rows.Count; i++)
                    {

                        if (Chk_C.Checked == true)
                            AddPointChartWithColor(i, CHRT_ExportCurveDeltaRef.Series["Series1"], DTDeltaRef.Rows[i]["ReferenceNo"].ToString(), Convert.ToInt64(DTDeltaRef.Rows[i]["CMYK_C"]).ToString(), Color.Cyan.Name.ToString(), false);
                        if (Chk_M.Checked == true)
                            AddPointChartWithColor(i, CHRT_ExportCurveDeltaRef.Series["Series2"], DTDeltaRef.Rows[i]["ReferenceNo"].ToString(), Convert.ToInt64(DTDeltaRef.Rows[i]["CMYK_M"]).ToString(), Color.Magenta.Name.ToString(), false);
                        if (Chk_Y.Checked == true)
                            AddPointChartWithColor(i, CHRT_ExportCurveDeltaRef.Series["Series3"], DTDeltaRef.Rows[i]["ReferenceNo"].ToString(), Convert.ToInt64(DTDeltaRef.Rows[i]["CMYK_Y"]).ToString(), Color.Yellow.Name.ToString(), false);
                        if (Chk_K.Checked == true)
                            AddPointChartWithColor(i, CHRT_ExportCurveDeltaRef.Series["Series4"], DTDeltaRef.Rows[i]["ReferenceNo"].ToString(), Convert.ToInt64(DTDeltaRef.Rows[i]["CMYK_K"]).ToString(), Color.Black.Name.ToString(), false);
                    }
                    #endregion
                }
                RGPressRunCurveDeltaRef.DataSource = DTDeltaRef;
                RGPressRunCurveDeltaRef.DataBind();
            }
            else
            { RGPressRunCurveDeltaRef.DataSource = null; RGPressRunCurveDeltaRef.DataBind(); }
            #endregion

            #region Delta Prev
            //========== Assign the session data to Grid ===========//
            if (DTDeltaPrev != null && DTDeltaPrev.Rows.Count > 0)
            {
                #region Fine Max Min
                if (Convert.ToInt64(DTDeltaPrev.Compute("Max(CMYK_C)", string.Empty)) > MaxDeltaPrev)
                    MaxDeltaPrev = Convert.ToInt64(DTDeltaPrev.Compute("Max(CMYK_C)", string.Empty));
                if (Convert.ToInt64(DTDeltaPrev.Compute("Max(CMYK_M)", string.Empty)) > MaxDeltaPrev)
                    MaxDeltaPrev = Convert.ToInt64(DTDeltaPrev.Compute("Max(CMYK_M)", string.Empty));
                if (Convert.ToInt64(DTDeltaPrev.Compute("Max(CMYK_Y)", string.Empty)) > MaxDeltaPrev)
                    MaxDeltaPrev = Convert.ToInt64(DTDeltaPrev.Compute("Max(CMYK_Y)", string.Empty));
                if (Convert.ToInt64(DTDeltaPrev.Compute("Max(CMYK_K)", string.Empty)) > MaxDeltaPrev)
                    MaxDeltaPrev = Convert.ToInt64(DTDeltaPrev.Compute("Max(CMYK_K)", string.Empty));

                if (Convert.ToInt64(DTDeltaPrev.Compute("Min(CMYK_C)", string.Empty)) < MinDeltaPrev)
                    MinDeltaPrev = Convert.ToInt64(DTDeltaPrev.Compute("Min(CMYK_C)", string.Empty));
                if (Convert.ToInt64(DTDeltaPrev.Compute("Min(CMYK_M)", string.Empty)) < MinDeltaPrev)
                    MinDeltaPrev = Convert.ToInt64(DTDeltaPrev.Compute("Min(CMYK_M)", string.Empty));
                if (Convert.ToInt64(DTDeltaPrev.Compute("Min(CMYK_Y)", string.Empty)) < MinDeltaPrev)
                    MinDeltaPrev = Convert.ToInt64(DTDeltaPrev.Compute("Min(CMYK_Y)", string.Empty));
                if (Convert.ToInt64(DTDeltaPrev.Compute("Min(CMYK_K)", string.Empty)) < MinDeltaPrev)
                    MinDeltaPrev = Convert.ToInt64(DTDeltaPrev.Compute("Min(CMYK_K)", string.Empty));

                MinDeltaPrev = (MinDeltaPrev * -1);

                MaxDeltaPrev = MaxDeltaPrev + 1;
                MinDeltaPrev = MinDeltaPrev + 1;

                if (MaxDeltaPrev > MinDeltaPrev)
                {
                    MinDeltaPrev = (MaxDeltaPrev * -1);
                }
                else
                {
                    MaxDeltaPrev = MinDeltaPrev;
                    MinDeltaPrev = (MinDeltaPrev * -1);
                }
                CHRT_ExportCurveDeltaPrev.ChartAreas["Default"].AxisY.Minimum = MinDeltaPrev;
                CHRT_ExportCurveDeltaPrev.ChartAreas["Default"].AxisY.Maximum = MaxDeltaPrev;
                #endregion
                if (_CurveTypeID != 65)
                {
                    #region BindChart
                    for (int i = 0; i < DTDeltaPrev.Rows.Count; i++)
                    {

                        if (Chk_C.Checked == true)
                            AddPointChartWithColor(i, CHRT_ExportCurveDeltaPrev.Series["Series1"], DTDeltaPrev.Rows[i]["ReferenceNo"].ToString(), Convert.ToInt64(DTDeltaPrev.Rows[i]["CMYK_C"]).ToString(), Color.Cyan.Name.ToString(), false);
                        if (Chk_M.Checked == true)
                            AddPointChartWithColor(i, CHRT_ExportCurveDeltaPrev.Series["Series2"], DTDeltaPrev.Rows[i]["ReferenceNo"].ToString(), Convert.ToInt64(DTDeltaPrev.Rows[i]["CMYK_M"]).ToString(), Color.Magenta.Name.ToString(), false);
                        if (Chk_Y.Checked == true)
                            AddPointChartWithColor(i, CHRT_ExportCurveDeltaPrev.Series["Series3"], DTDeltaPrev.Rows[i]["ReferenceNo"].ToString(), Convert.ToInt64(DTDeltaPrev.Rows[i]["CMYK_Y"]).ToString(), Color.Yellow.Name.ToString(), false);
                        if (Chk_K.Checked == true)
                            AddPointChartWithColor(i, CHRT_ExportCurveDeltaPrev.Series["Series4"], DTDeltaPrev.Rows[i]["ReferenceNo"].ToString(), Convert.ToInt64(DTDeltaPrev.Rows[i]["CMYK_K"]).ToString(), Color.Black.Name.ToString(), false);
                    }
                    #endregion
                }
                RGPressRunCurveDeltaPrev.DataSource = DTDeltaPrev;
                RGPressRunCurveDeltaPrev.DataBind();
            }
            else
            { RGPressRunCurveDeltaPrev.DataSource = null; RGPressRunCurveDeltaPrev.DataBind(); }
            #endregion
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }
    //Add Point with color
    private void AddPointChartWithColor(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName, bool ShowMarker)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
            if (ShowMarker)
            {
                _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
                _objSeries.Points[_Index].MarkerSize = 2;
                _objSeries.Points[_Index].BorderWidth = 1;
                _objSeries.Points[_Index].MarkerBorderColor = Color.Black;
            }
            else
            {
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.None;
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    /// <summary>
    /// Used to bind the Sync Press Grid
    /// </summary>
    private void BindSyncPress()
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPSyncPressData ObjSyncPress = new CPSyncPressData();

            //==========  Get Data Table and Bind ==========//
            DataTable DT_SyncPress = new DataTable();
            if (Fuji.Common.Sessions.GetSession("SyncPressData") == null)
            {
                if (Fuji.Common.Sessions.RoleID == 1) // If user is admin then he can be allow to do filter press
                    DT_SyncPress = ObjSyncPress.uspGetAllSyncPress(Fuji.Common.Sessions.UserID, Fuji.Common.Sessions.RoleID, Convert.ToInt32(Fuji.Common.Sessions.GetSession("SyncCompanyID")), Convert.ToInt32(Fuji.Common.Sessions.GetSession("SyncBranchID")));
                else if (Fuji.Common.Sessions.RoleID == 3)
                    DT_SyncPress = ObjSyncPress.uspGetAllSyncPress(Fuji.Common.Sessions.UserID, 1, Fuji.Common.Sessions.CompanyID, Convert.ToInt32(Fuji.Common.Sessions.GetSession("SyncBranchID")));
                else
                    DT_SyncPress = ObjSyncPress.uspGetAllSyncPress(Fuji.Common.Sessions.UserID, Fuji.Common.Sessions.RoleID, 0, 0);
                Fuji.Common.Sessions.SetSessions("SyncPressData", DT_SyncPress);
            }

            //========== Get Company data from the session ===========//
            DT_SyncPress = (DataTable)Fuji.Common.Sessions.GetSession("SyncPressData");

            //========== Assign the session data to Grid ===========//
            if (DT_SyncPress != null && DT_SyncPress.Rows.Count > 0)
            { RG_SyncPress.DataSource = DT_SyncPress; RG_SyncPress.DataBind(); }
            else
            { RG_SyncPress.DataSource = null; RG_SyncPress.DataBind(); }

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    /// <summary>
    /// Fill Compnay drop down for only Admin
    /// </summary>
    private void BindCompanyDropDown()
    {
        try
        {
            //========== Clear Items in DropDownList ===========//
            ddlCompany.Items.Clear();

            //==========  Declaring Objects ==========//
            CPSyncPressData ObjSyncPress = new CPSyncPressData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Company = new DataTable();
            if (Fuji.Common.Sessions.RoleID == 1)
            {
                DT_Company = ObjSyncPress.uspGetCompanyListAndBranchList(0);
            }
            else if (Fuji.Common.Sessions.RoleID == 3)
            {
                DT_Company = ObjSyncPress.uspGetCompanyListAndBranchList(Fuji.Common.Sessions.CompanyID);
            }

            else
            {
                { ddlCompany.DataSource = null; ddlCompany.DataBind(); }
            }

            //========== Insert Items DropDownList in Company ===========//
            ddlCompany.Items.Insert(0, new ListItem("--- Select Company ---", "0"));


            //get distinct rows from datatable or datarow
            //http://blog.wolffmyren.com/2009/02/24/c-corner-how-to-get-distinct-rows-from-a-dataset-or-datatable/         

            DT_Company = DT_Company.DefaultView.ToTable(true, new String[] { "CompanyName", "CompanyID" });
            if (DT_Company != null && DT_Company.Rows.Count > 0)
            {
                ddlCompany.DataSource = DT_Company;
                ddlCompany.DataValueField = "CompanyID"; ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataBind();
            }
            else
            { ddlCompany.DataSource = null; ddlCompany.DataBind(); }

            //========== Insert Items DropDownList in Company ===========//
            ddlCompany.Items.Insert(0, new ListItem("--- Select Company ---", "0"));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void BindBranchDropDown(int CompanyID)
    {

        try
        {
            //========== Clear Items in DropDownList ===========//
            ddlBranch.Items.Clear();

            //==========  Declaring Objects ==========//
            CPSyncPressData ObjSyncPress = new CPSyncPressData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Branch = new DataTable();
            DT_Branch = ObjSyncPress.uspGetCompanyListAndBranchList(CompanyID);
            if (DT_Branch != null && DT_Branch.Rows.Count > 0)
            {
                ddlBranch.DataSource = DT_Branch;
                ddlBranch.DataValueField = "BranchID"; ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataBind();
            }
            else
            { ddlBranch.DataSource = null; ddlBranch.DataBind(); }

            //========== Insert Items DropDownList in Company ===========//
            ddlBranch.Items.Insert(0, new ListItem("--- Select Branch ---", "0"));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    /// <summary>
    /// Used to bind the Sync Press Run Grid
    /// </summary>
    private void BindSyncProject(bool IsDataBind)
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPSyncPressRunData ObjSyncPressRun = new CPSyncPressRunData();

            //==========  Get Data Table and Bind ==========//
            DataTable DT_SyncPressRun = new DataTable();


            DT_SyncPressRun = ObjSyncPressRun.uspGetAllSyncProjectByPressID(Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressID")));


            //========== Assign the session data to Grid ===========//
            if (DT_SyncPressRun != null && DT_SyncPressRun.Rows.Count > 0)
            {
                RG_SyncProject.DataSource = DT_SyncPressRun;

                if (IsDataBind)
                {
                    RG_SyncProject.DataBind();
                }
            }
            else
            {
                RG_SyncProject.DataSource = null;

                if (IsDataBind)
                {
                    RG_SyncProject.DataBind();
                }
            }

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    /// <summary>
    /// Load left menu navigation at load time
    /// </summary>
    private void LoadTimeLeftMenu()
    {
        try
        {
            //============= Create left menu and set blank at first time =========//
            List<string> obj = new List<string>(); // Create string list object.
            obj.Add(""); // Fisrt step of Select Press.
            obj.Add(""); // Second step of Select Press Run .
            obj.Add(""); // Third step of Select Target & Condition.
            obj.Add(""); // Fifth step of 1. Add Measurement.
            obj.Add(""); // Fifth step of 2. Analyse            

            //=========== Set first step "Select Press " =========================//
            obj.Insert(0, "<table border='0' cellpadding='0' cellspacing='4' width='100%' class='SYNCTABLELEFTTABLE'><tr><td>Select Device :</td></tr><tr><td>NON SELECTED</td></tr></table>");

            //=========== Set First step from list object in to Letral control ====================//
            LtrLeftNevigation.Text = obj[0];

            //=========== Set list menu object in to session ===========================//
            Fuji.Common.Sessions.SetSessions("SyncLeftMenu", obj);
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    /// <summary>
    /// Table Visible True False Function
    /// </summary>
    private void TableVisibleTrueFalse(HtmlTable HT)
    {
        try
        {
            //============== Visible false all step table ==============//
            TblPress.Visible = false;
            TblPressRun.Visible = false;
            TblPrintTargetCondition.Visible = false;
            TblMeasurement.Visible = false;
            TblAnalyze.Visible = false;
            TblGeneratePDFReport.Visible = false;
            TblExportCurves.Visible = false;

            //============= Visible true selected step table=============//
            HT.Visible = true;
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    /// <summary>
    ///  fill project that are already marked certified
    /// </summary>
    private void FillCertifiedProjectDropDown(int ProjectID)
    {
        try
        {
            //========== Declare the object of the Print Standard Typ ===========//
            CPSyncPrintStandardTypeData objCPSyncPrintStandardType = new CPSyncPrintStandardTypeData();
            //========== Declare DataTable ===========//
            DataTable DTCertifiedProjectList;

            //========== Fetch the data from Print Standard Type  ===========//
            DTCertifiedProjectList = objCPSyncPrintStandardType.uspGetCertifiedProjectForPress(Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressID")));


            //========== Check Whether the data is returned or not===========//
            if (DTCertifiedProjectList != null && DTCertifiedProjectList.Rows.Count > 0)
            {
                //========== Assign the Print Standard Type list ===========//
                ddlCertifiedProject.DataSource = DTCertifiedProjectList;
                ddlCertifiedProject.DataValueField = "ProjectID"; ddlCertifiedProject.DataTextField = "ProjectName";
                ddlCertifiedProject.DataBind();
            }
            else
            {
                //========== Bind null values to the null ===========//
                ddlCertifiedProject.DataSource = null;
                ddlCertifiedProject.DataBind();
            }

            ddlCertifiedProject.Items.Insert(0, new ListItem("--- Select Project ---", "0"));

            if (ProjectID == 0) ddlCertifiedProject.SelectedIndex = 0;
            else
            {
                ddlCertifiedProject.SelectedValue = ProjectID.ToString();
                ddlCertifiedProject.Enabled = false;
            }

        }
        catch (Exception exc)
        {
            throw exc;
        }
    }
    /// <summary>
    ///  Used to fill thesync print standard type drop down
    /// </summary>
    private void FillSyncPrintStandardTypeDropDown()
    {
        try
        {
            //========== Declare the object of the Print Standard Typ ===========//
            CPSyncPrintStandardTypeData objCPSyncPrintStandardType = new CPSyncPrintStandardTypeData();
            //========== Declare DataTable ===========//
            DataTable DTCPSyncPrintStandardType;
            DataView DV = new DataView();
            //========== Fetch the data from Print Standard Type  ===========//
            DTCPSyncPrintStandardType = objCPSyncPrintStandardType.uspGetAllSyncPrintStandardType();


            //========== Check Whether the data is returned or not===========//
            if (DTCPSyncPrintStandardType != null && DTCPSyncPrintStandardType.Rows.Count > 0)
            {
                if (DDLProjectType.SelectedValue.ToString() == Convert.ToInt16(Enums.ProjectType.G7).ToString())
                {
                    DTCPSyncPrintStandardType.DefaultView.RowFilter = "(PrintStandardTypeID in (" + Convert.ToInt16(Enums.PrintStandardType.GRACoL).ToString() + "," + Convert.ToInt16(Enums.PrintStandardType.SWOP_3).ToString() + "," + Convert.ToInt16(Enums.PrintStandardType.SWOP_5).ToString() + "))";
                    DV = DTCPSyncPrintStandardType.DefaultView;
                }
                else
                {
                    DTCPSyncPrintStandardType.DefaultView.RowFilter = "(PrintStandardTypeID in (" + Convert.ToInt16(Enums.PrintStandardType.PSO_12647_2).ToString() + "," + Convert.ToInt16(Enums.PrintStandardType.PSO_Uncoated).ToString() + "," + Convert.ToInt16(Enums.PrintStandardType.Fogra_45).ToString() + "," + Convert.ToInt16(Enums.PrintStandardType.ISO_P3_Web).ToString() + "))";
                    DV = DTCPSyncPrintStandardType.DefaultView;
                }
                //========== Assign the Print Standard Type list ===========//
                DdlStanderdsPreset.DataSource = DV;   //DTCPSyncPrintStandardType;
                DdlStanderdsPreset.DataValueField = "PrintStandardTypeID"; DdlStanderdsPreset.DataTextField = "PrintStandard";
                DdlStanderdsPreset.DataBind();
            }
            else
            {
                //========== Bind null values to the null ===========//
                DdlStanderdsPreset.DataSource = null;
                DdlStanderdsPreset.DataBind();
            }

        }
        catch (Exception exc)
        {
            throw exc;
        }
    }
    /// <summary>
    /// Fill Print target information when user come back print target step.
    /// </summary>
    /// <param name="DT_SyncPrintTarget">Pass Data Table For Sync Print Target</param>
    private void FillPrintTargetInfo(DataTable DT_SyncPrintTarget)
    {
        try
        {
            if (DDLProjectType.SelectedValue.ToString() == Convert.ToInt16(Enums.ProjectType.G7).ToString())
            {
                trG7.Visible = true;
                trISO.Visible = false;
                #region Fill G7 value
                for (int i = 0; i < DT_SyncPrintTarget.Rows.Count; i++)
                {
                    //============== Fill Print target data from database =========================//

                    #region Fill Paper L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtPaperTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtPaperTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtPaperTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtPaperTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Cyan  L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtCyanTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtCyanTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtCyanTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtCyanTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();

                    #endregion

                    #region Fill Magenda L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtMagendaTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtMagendaTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtMagendaTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtMagendaTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Yellow L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtYellowTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtYellowTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtYellowTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtYellowTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Black L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtBlackTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtBlackTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtBlackTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtBlackTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Red (M+Y) L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtRedTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtRedTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtRedTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtRedTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Green (C+Y) L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtGreenTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtGreenTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtGreenTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtGreenTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Blue (C+M) L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtBlueTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtBlueTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtBlueTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtBlueTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill 3-C (C+M+Y) L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        Txt3CTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        Txt3CTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        Txt3CTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        Txt3CTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Delta F Value
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_C_M_Y).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaF).ToString())
                    {
                        TxtDeltaFTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    }
                    #endregion

                    #region Fill Density Value
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                    {
                        TxtCyanDT.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtCyanDTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    }

                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                    {
                        TxtMagendaDT.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtMagendaDTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    }
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                    {
                        TxtYellowDT.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtYellowDTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    }

                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                    {
                        TxtBalckDT.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtBalckDTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    }
                    #endregion

                    #region Dotgain Target Value
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        Txt25TC.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        Txt25TM.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        Txt25TY.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        Txt25TK.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        Txt25TH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    }

                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        Txt50TC.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        Txt50TM.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        Txt50TY.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        Txt50TK.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        Txt50TH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    }

                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        Txt75TC.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        Txt75TM.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        Txt75TY.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        Txt75TK.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        Txt75TH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                trG7.Visible = false;
                trISO.Visible = true;
                #region Fill ISO value
                for (int i = 0; i < DT_SyncPrintTarget.Rows.Count; i++)
                {
                    //============== Fill Print target data from database =========================//                   

                    #region Fill Cyan  L,a,b, Delta E And Delta H
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtISOCyanTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtISOCyanTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtISOCyanTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtISOCyanTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();

                    //============ Delta H Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaH).ToString())
                        TxtISOCyanHTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();

                    #endregion

                    #region Fill Magenda L,a,b, Delta E And Delta H
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtISOMagendaTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtISOMagendaTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtISOMagendaTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtISOMagendaTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();

                    //============ Delta H Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaH).ToString())
                        TxtISOMagendaHTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Yellow L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtISOYellowTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtISOYellowTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtISOYellowTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtISOYellowTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();

                    //============ Delta H Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaH).ToString())
                        TxtISOYellowHTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Black L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtISOBlackTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtISOBlackTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtISOBlackTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtISOBlackTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Red (M+Y) L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtISORedTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtISORedTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtISORedTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtISORedTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Green (C+Y) L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtISOGreenTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtISOGreenTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtISOGreenTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtISOGreenTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Blue (C+M) L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtISOBlueTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtISOBlueTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtISOBlueTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtISOBlueTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill 3-C (C+M+Y) L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtISO3CTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtISO3CTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtISO3CTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtISO3CTH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Gray 25 L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_25).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtISOGray25TL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_25).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtISOGray25Ta.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_25).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtISOGray25Tb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_25).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtISOGray25TH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Gray 50 L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_50).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtISOGray50TL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_50).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtISOGray50Ta.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_50).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtISOGray50Tb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_50).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtISOGray50TH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Gray 75 L,a,b and Delta E
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_75).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        TxtISOGray75TL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_75).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        TxtISOGray75Ta.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_75).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        TxtISOGray75Tb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    //============ Delta E Value =================//
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_75).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DeltaE).ToString())
                        TxtISOGray75TH.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    #endregion

                    #region Fill Paper L,a and b
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                    {
                        TxtISOAPPaperTL.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISOTHPaperTL.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();

                    }
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                    {
                        TxtISOAPPaperTa.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISOTHPaperTa.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    }
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                    {
                        TxtISOAPPaperTb.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISOTHPaperTb.Text = DT_SyncPrintTarget.Rows[i]["ToleranceLimit"].ToString();
                    }
                    #endregion

                    #region Fill TVI
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        TxtISO25CA.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISO25CT.Text = Convert.ToInt16(DT_SyncPrintTarget.Rows[i]["ToleranceLimit"]).ToString();
                    }
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        TxtISO25MA.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISO25MT.Text = Convert.ToInt16(DT_SyncPrintTarget.Rows[i]["ToleranceLimit"]).ToString();
                    }
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        TxtISO25YA.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISO25YT.Text = Convert.ToInt16(DT_SyncPrintTarget.Rows[i]["ToleranceLimit"]).ToString();
                    }
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        TxtISO25KA.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISO25KT.Text = Convert.ToInt16(DT_SyncPrintTarget.Rows[i]["ToleranceLimit"]).ToString();
                    }

                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        TxtISO50CA.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISO50CT.Text = Convert.ToInt16(DT_SyncPrintTarget.Rows[i]["ToleranceLimit"]).ToString();
                    }
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        TxtISO50MA.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISO50MT.Text = Convert.ToInt16(DT_SyncPrintTarget.Rows[i]["ToleranceLimit"]).ToString();
                    }
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        TxtISO50YA.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISO50YT.Text = Convert.ToInt16(DT_SyncPrintTarget.Rows[i]["ToleranceLimit"]).ToString();
                    }
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        TxtISO50KA.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISO50KT.Text = Convert.ToInt16(DT_SyncPrintTarget.Rows[i]["ToleranceLimit"]).ToString();
                    }

                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        TxtISO75CA.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISO75CT.Text = Convert.ToInt16(DT_SyncPrintTarget.Rows[i]["ToleranceLimit"]).ToString();
                    }
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        TxtISO75MA.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISO75MT.Text = Convert.ToInt16(DT_SyncPrintTarget.Rows[i]["ToleranceLimit"]).ToString();
                    }
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        TxtISO75YA.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISO75YT.Text = Convert.ToInt16(DT_SyncPrintTarget.Rows[i]["ToleranceLimit"]).ToString();
                    }
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                    {
                        TxtISO75KA.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                        TxtISO75KT.Text = Convert.ToInt16(DT_SyncPrintTarget.Rows[i]["ToleranceLimit"]).ToString();
                    }
                    #endregion

                    #region TVI Spreads
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString())
                        TxtISOQuarterTone.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString())
                        TxtISOMidTone.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI_Spread).ToString())
                        TxtISO3QuarterTone.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    /*TxtISOQuarterTone.Text = "3";
                    TxtISOMidTone.Text = "5";
                    TxtISO3QuarterTone.Text = "3";*/
                    #endregion

                    #region Fill Density Value
                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                        TxtISOCyanDT.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                        TxtISOMagendaDT.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                        TxtISOYellowDT.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();

                    if (DT_SyncPrintTarget.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && DT_SyncPrintTarget.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                        TxtISOBalckDT.Text = DT_SyncPrintTarget.Rows[i]["Target"].ToString();
                    #endregion


                }
                #endregion
            }
        }
        catch (Exception Exc)
        {
            throw Exc;
        }

    }
    /// <summary>
    /// Fill Print condition information when user come back print condition step.
    /// </summary>
    private void FillPrintCondition()
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPSyncPrintConditionData ObjSyncPrintCondition = new CPSyncPrintConditionData();

            //==========  Get Data Table and Bind ==========//
            DataTable DT_SyncPrintCondition = new DataTable();
            DT_SyncPrintCondition = ObjSyncPrintCondition.uspGetPrintCond(Convert.ToInt32(Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString()));
            CleanPrintConditionDetail(); //Clean Print Conditon all text box
            //Fill Print Condition detail from database

            if (DT_SyncPrintCondition != null && DT_SyncPrintCondition.Rows.Count > 0)
            {
                #region General Data
                DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtGDSubstrateDesc.Attributes["PCTypeId"].ToString();
                if (DT_SyncPrintCondition.DefaultView.Count > 0)
                    TxtGDSubstrateDesc.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtGDInk.Attributes["PCTypeId"].ToString();
                if (DT_SyncPrintCondition.DefaultView.Count > 0)
                    TxtGDInk.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();
                #endregion

                #region Offsett Data
                if (DDLOutputType.SelectedValue.ToString() == Convert.ToInt16(Enums.OutputType.Offset).ToString())
                {
                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtOPDISO2846Ink.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtOPDISO2846Ink.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + DdlOPDInkOrder.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        DdlOPDInkOrder.SelectedValue = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtOPDFountainSolution.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtOPDFountainSolution.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtOPDBlanket.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtOPDBlanket.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtOPDScreening.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtOPDScreening.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtOPDBlanketAge.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtOPDBlanketAge.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                }
                #endregion

                #region Screen Data
                if (DDLOutputType.SelectedValue.ToString() == Convert.ToInt16(Enums.OutputType.Screen).ToString())
                {
                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtSPDStencil.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtSPDStencil.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtSPDCyan.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtSPDCyan.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtSPDMagenta.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtSPDMagenta.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtSPDYellow.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtSPDYellow.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtSPDBlack.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtSPDBlack.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtSPDRip.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtSPDRip.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtSPDCTSFilmDevice.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtSPDCTSFilmDevice.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();
                }
                #endregion

                #region Wide Format Data
                if (DDLOutputType.SelectedValue.ToString() == Convert.ToInt16(Enums.OutputType.Wideformat).ToString())
                {
                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtWFRip.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtWFRip.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtWFPrintMode.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtWFPrintMode.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtWFScreeningType.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtWFScreeningType.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();

                    DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtWFUVLampSettings.Attributes["PCTypeId"].ToString();
                    if (DT_SyncPrintCondition.DefaultView.Count > 0)
                        TxtWFUVLampSettings.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();
                }

                #endregion

                #region Note
                DT_SyncPrintCondition.DefaultView.RowFilter = "PrintConditionTypeID = " + TxtNotes.Attributes["PCTypeId"].ToString();
                if (DT_SyncPrintCondition.DefaultView.Count > 0)
                    TxtNotes.Text = DT_SyncPrintCondition.DefaultView[0]["PrintCondition"].ToString();
                #endregion
            }


            #region Old Code
            //DT_SyncPrintCondition = ObjSyncPrintCondition.uspGetPrintCondition(Convert.ToInt32(Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString()));
            //if (DT_SyncPrintCondition != null && DT_SyncPrintCondition.Rows.Count > 0)
            //{
            //    TxtPaperManufacturer.Text = DT_SyncPrintCondition.Rows[0]["PaperManufacturer"].ToString();
            //    TxtPaperName.Text = DT_SyncPrintCondition.Rows[0]["PaperName"].ToString();
            //    TxtPaperFinish.Text = DT_SyncPrintCondition.Rows[0]["PaperFinish"].ToString();
            //    TxtPaperGrade.Text = DT_SyncPrintCondition.Rows[0]["PaperGrade"].ToString();
            //    TxtPaperWeight.Text = DT_SyncPrintCondition.Rows[0]["PaperWeight"].ToString();
            //    TxtPaperWhite.Text = DT_SyncPrintCondition.Rows[0]["PaperWhite"].ToString();
            //    TxtInkManufacturer.Text = DT_SyncPrintCondition.Rows[0]["InkManufacturer"].ToString();
            //    TxtInkSetName.Text = DT_SyncPrintCondition.Rows[0]["InkSetName"].ToString();
            //    TxtBlanketsManufacturer.Text = DT_SyncPrintCondition.Rows[0]["BlanketManufacturer"].ToString();
            //    TxtBlanketsName.Text = DT_SyncPrintCondition.Rows[0]["BlanketName"].ToString();
            //    TxtBlanketsAge.Text = DT_SyncPrintCondition.Rows[0]["BlanketAge"].ToString();
            //    DdlISO.SelectedValue = DT_SyncPrintCondition.Rows[0]["ISO2846Compliant"].ToString();
            //    TxtFountainManufacturer.Text = DT_SyncPrintCondition.Rows[0]["FountainSolutionMfg"].ToString();
            //    TxtFountainName.Text = DT_SyncPrintCondition.Rows[0]["FountainSolutionName"].ToString();
            //    DdlInk1.SelectedValue = DT_SyncPrintCondition.Rows[0]["Ink1"].ToString();
            //    DdlInk2.SelectedValue = DT_SyncPrintCondition.Rows[0]["Ink2"].ToString();
            //    DdlInk3.SelectedValue = DT_SyncPrintCondition.Rows[0]["Ink3"].ToString();
            //    DdlInk4.SelectedValue = DT_SyncPrintCondition.Rows[0]["Ink4"].ToString();

            //    if(DT_SyncPrintCondition.Rows[0]["ScreeningType"] !=null && DT_SyncPrintCondition.Rows[0]["ScreeningType"].ToString()!="")
            //        ddlScreeningType.SelectedValue = DT_SyncPrintCondition.Rows[0]["ScreeningType"].ToString();

            //    txtScreeningDotSize.Text = DT_SyncPrintCondition.Rows[0]["DotSize"].ToString();
            //    txtScreeningFamily.Text = DT_SyncPrintCondition.Rows[0]["DotShape"].ToString();

            //}
            //else
            //{
            //    TxtPaperManufacturer.Text = "";
            //    TxtPaperName.Text = "";
            //    TxtPaperFinish.Text = "";
            //    TxtPaperGrade.Text = "";
            //    TxtPaperWeight.Text = "";
            //    TxtPaperWhite.Text = "";
            //    TxtInkManufacturer.Text = "";
            //    TxtInkSetName.Text = "";
            //    TxtBlanketsManufacturer.Text = "";
            //    TxtBlanketsName.Text = "";
            //    TxtBlanketsAge.Text = "";
            //    DdlISO.SelectedValue = "";
            //    TxtFountainManufacturer.Text = "";
            //    TxtFountainName.Text = "";
            //    DdlInk1.SelectedValue = "";
            //    DdlInk2.SelectedValue = "";
            //    DdlInk3.SelectedValue = "";
            //    DdlInk4.SelectedValue = "";

            //    txtScreeningFamily.Text = "";
            //    txtScreeningDotSize.Text = "";
            //    ddlScreeningType.SelectedIndex = 0;

            //}
            #endregion

        }
        catch (Exception Exc)
        {
            throw Exc;
        }

    }


    /// <summary>
    /// Fill project setting information when user come back project setting step.
    /// </summary>
    private void FillProjectSetting()
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPSyncProjectSettingData ObjSyncProjectSetting = new CPSyncProjectSettingData();

            //==========  Get Data Table and Bind ==========//
            DataTable DT_SyncProjectSetting = new DataTable();
            ObjSyncProjectSetting.ProjectID = Convert.ToInt32(Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString());
            DT_SyncProjectSetting = ObjSyncProjectSetting.uspGetProjectSetting();
            //Fill Project Setting detail from database

            BindMeasurementDeviceType();

            if (DT_SyncProjectSetting != null && DT_SyncProjectSetting.Rows.Count > 0)
            {
                ChkUseMeasurementPlugin.Checked = Convert.ToBoolean(DT_SyncProjectSetting.Rows[0]["UsePlugin"].ToString());
                ddlMeasurentIlluminant.SelectedValue = DT_SyncProjectSetting.Rows[0]["Illuminant"].ToString();
                ddlMeasurentObserver.SelectedValue = DT_SyncProjectSetting.Rows[0]["Observer"].ToString();
                ddlMeasurentDensityType.SelectedValue = DT_SyncProjectSetting.Rows[0]["DensityType"].ToString();

                if (ddlMeasurentDeviceType.Items.FindByText(DT_SyncProjectSetting.Rows[0]["DeviceTypeName"].ToString()) != null)
                {
                    ddlMeasurentDeviceType.SelectedIndex =
                        ddlMeasurentDeviceType.Items.IndexOf(ddlMeasurentDeviceType.Items.FindByText(DT_SyncProjectSetting.Rows[0]["DeviceTypeName"].ToString()));
                }
                else
                {
                    ddlMeasurentDeviceType.Items.Insert(0, new ListItem(DT_SyncProjectSetting.Rows[0]["DeviceTypeName"].ToString(),
                                                                        DT_SyncProjectSetting.Rows[0]["InstrumentTypeID"].ToString() + " - " +
                                                                        DT_SyncProjectSetting.Rows[0]["DeviceTypeName"].ToString().Split('-')[1].Trim()));
                }

                ddlMeasurentDeviceType_SelectedIndexChanged(null, null);
                ddlMeasurentAlignmentChart.SelectedValue = DT_SyncProjectSetting.Rows[0]["ChartTypeID"].ToString();
            }
            else
            {
                ChkUseMeasurementPlugin.Checked = false;
                ddlMeasurentDeviceType_SelectedIndexChanged(null, null);
            }
            //if (ChkUseMeasurementPlugin.Checked)
            //    TrTestDevice.Style.Add("display", "inline");
            //else
            //    TrTestDevice.Style.Add("display", "none");
        }
        catch (Exception Exc)
        {
            throw Exc;
        }

    }



    /// <summary>
    /// Bind Trend Line DeltaE Chart
    /// </summary>
    private void FillTrendLineDeltaEChart()
    {
        try
        {
            HDTrendDeltaAll.Value = "";
            //========== Declaring Objects ==========//
            CPSyncMeasurementData objCPSyncMeasurement = new CPSyncMeasurementData();

            //========== Get The Data Table ==========//           
            objCPSyncMeasurement.PressRunID = Convert.ToInt32(Fuji.Common.Sessions.GetSession("SelectedPressRunID").ToString());
            DataTable DT = objCPSyncMeasurement.uspGetChartTrendData();

            //========== Check DataTable Rows ==========//
            if (DT.Rows.Count < 240)
                CHRT_TrendLineDeltaE.Width = 750;
            else
                CHRT_TrendLineDeltaE.Width = (DT.Rows.Count * 25);

            //======== Data Point Index ==========//
            int PatchTypeID1 = 0;
            int PatchTypeID2 = 0;
            int PatchTypeID3 = 0;
            int PatchTypeID4 = 0;
            int PatchTypeID5 = 0;
            int PatchTypeID6 = 0;
            int PatchTypeID7 = 0;
            int PatchTypeID8 = 0;

            //====== Selecte Default last point in trend line chart ========//
            if (DT != null && DT.Rows.Count > 0)
                if (HDCurrentMeasurement.Value.Trim() == "")
                    HDCurrentMeasurement.Value = DT.Rows[DT.Rows.Count - 1]["MeasurementID"].ToString();

            //========== Defualt set empty Measurement Name ==========//        
            HDCurrentMeasurementName.Value = string.Empty;

            //========== Bind the Chart ==========//        
            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                #region Get Measurement Name for ISO chart title
                if (string.IsNullOrEmpty(HDCurrentMeasurementName.Value) && !string.IsNullOrEmpty(HDCurrentMeasurement.Value))
                {
                    if(Convert.ToInt32(DT.Rows[i]["MeasurementID"]) == Convert.ToInt32(HDCurrentMeasurement.Value.Trim()))
                    {
                        var measurementName = DT.Rows[i]["MeasurementName"].ToString();
                        if (measurementName != null && measurementName.Length > 0) measurementName = System.IO.Path.GetFileNameWithoutExtension(measurementName);  //Changed By Chirag on 20th Feb 2012

                        HDCurrentMeasurementName.Value = (measurementName.Contains("_") ? measurementName.Substring(0, measurementName.LastIndexOf('_')) : measurementName);
                    }
                }
                #endregion
                #region Bind point in chart in "PatchTypeID" wise
                if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString())
                {
                    AddPointToTrendDeltaE(PatchTypeID1, CHRT_TrendLineDeltaE.Series["Series0"], DT.Rows[i]["MeasurementNo"].ToString(), DT.Rows[i]["ΔE"].ToString(), DT.Rows[i]["MeasurementName"].ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["ΔELimit"].ToString(), DT.Rows[i]["MeasurementID"].ToString());
                    PatchTypeID1++;
                }
                else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString())
                {
                    AddPointToTrendDeltaE(PatchTypeID2, CHRT_TrendLineDeltaE.Series["Series1"], DT.Rows[i]["MeasurementNo"].ToString(), DT.Rows[i]["ΔE"].ToString(), DT.Rows[i]["MeasurementName"].ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["ΔELimit"].ToString(), DT.Rows[i]["MeasurementID"].ToString());
                    PatchTypeID2++;
                }
                else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString())
                {
                    AddPointToTrendDeltaE(PatchTypeID3, CHRT_TrendLineDeltaE.Series["Series2"], DT.Rows[i]["MeasurementNo"].ToString(), DT.Rows[i]["ΔE"].ToString(), DT.Rows[i]["MeasurementName"].ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["ΔELimit"].ToString(), DT.Rows[i]["MeasurementID"].ToString());
                    PatchTypeID3++;
                }
                else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString())
                {
                    AddPointToTrendDeltaE(PatchTypeID4, CHRT_TrendLineDeltaE.Series["Series3"], DT.Rows[i]["MeasurementNo"].ToString(), DT.Rows[i]["ΔE"].ToString(), DT.Rows[i]["MeasurementName"].ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["ΔELimit"].ToString(), DT.Rows[i]["MeasurementID"].ToString());
                    PatchTypeID4++;
                }
                else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString())
                {
                    AddPointToTrendDeltaE(PatchTypeID5, CHRT_TrendLineDeltaE.Series["Series4"], DT.Rows[i]["MeasurementNo"].ToString(), DT.Rows[i]["ΔE"].ToString(), DT.Rows[i]["MeasurementName"].ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["ΔELimit"].ToString(), DT.Rows[i]["MeasurementID"].ToString());
                    PatchTypeID5++;
                }
                else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString())
                {
                    AddPointToTrendDeltaE(PatchTypeID6, CHRT_TrendLineDeltaE.Series["Series5"], DT.Rows[i]["MeasurementNo"].ToString(), DT.Rows[i]["ΔE"].ToString(), DT.Rows[i]["MeasurementName"].ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["ΔELimit"].ToString(), DT.Rows[i]["MeasurementID"].ToString());
                    PatchTypeID6++;
                }
                else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString())
                {
                    AddPointToTrendDeltaE(PatchTypeID7, CHRT_TrendLineDeltaE.Series["Series6"], DT.Rows[i]["MeasurementNo"].ToString(), DT.Rows[i]["ΔE"].ToString(), DT.Rows[i]["MeasurementName"].ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["ΔELimit"].ToString(), DT.Rows[i]["MeasurementID"].ToString());
                    PatchTypeID7++;
                }
                else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString())
                {
                    AddPointToTrendDeltaE(PatchTypeID8, CHRT_TrendLineDeltaE.Series["Series7"], DT.Rows[i]["MeasurementNo"].ToString(), DT.Rows[i]["ΔE"].ToString(), DT.Rows[i]["MeasurementName"].ToString(), "#736369", DT.Rows[i]["ΔELimit"].ToString(), DT.Rows[i]["MeasurementID"].ToString());
                    PatchTypeID8++;
                }
                #endregion

                //========= Put check or uncheck check box and call javascript function for rebind chart ===/
                CustomLabel c = new CustomLabel(i + 1, i + 1 + 0.001, "", 1, LabelMark.None);

                if (Convert.ToBoolean(DT.Rows[i]["IsUsed"].ToString())) // Check this ID is in hidden fild formmat "[ID],"
                {
                    c.Image = "Images/checkbox_2.gif"; // True value check box image

                    if (!HDTrendDeltaAll.Value.ToString().Contains("[" + DT.Rows[i]["MeasurementID"].ToString() + "],"))
                        HDTrendDeltaAll.Value = HDTrendDeltaAll.Value.ToString() + "[" + DT.Rows[i]["MeasurementID"].ToString() + "],";
                }
                else
                    c.Image = "Images/checkbox_1.gif"; // False value check box image

                if (Fuji.Common.Sessions.GetSession("SelectedPressRunClose").ToString() == Convert.ToInt16(Enums.CPSyncPressRunStatus.Open).ToString())
                    c.ImageMapAreaAttributes = "onclick=\"SetValueTrendDelta('" + DT.Rows[i]["MeasurementID"].ToString() + "');\""; // Bind javascript function 

                CHRT_TrendLineDeltaE.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(c); // Add Custom Label
            }

            if (HDCurrentMeasurement.Value.Trim() != "")
            {
                //if (ChkAVG.Checked == true)
                //{
                //    if(Fuji.Common.Sessions.GetSession("SelectedProjectTypeID").ToString() == "1")
                //        FRM1.Attributes.Add("src", "SyncDetailMeasurementChart.aspx?MeasurementID=" + HDCurrentMeasurement.Value.Trim() + "&PressRunID=" + Fuji.Common.Sessions.GetSession("SelectedPressRunID").ToString());
                //    else if (Fuji.Common.Sessions.GetSession("SelectedProjectTypeID").ToString() == "2")
                //        FRM1.Attributes.Add("src", "SyncDetailISOMeasurementChart.aspx?MeasurementID=" + HDCurrentMeasurement.Value.Trim() + "&PressRunID=" + Fuji.Common.Sessions.GetSession("SelectedPressRunID").ToString());

                //}
                //else
                //{
                if (Fuji.Common.Sessions.GetSession("SelectedProjectTypeID").ToString() == Convert.ToInt16(Enums.ProjectType.G7).ToString())
                {
                    tblAverageOfMeasurements.Visible = false;
                    FRM1.Attributes.Add("src", "SyncDetailMeasurementChart.aspx?MeasurementID=" + HDCurrentMeasurement.Value.Trim());
                }
                else
                {
                    tblAverageOfMeasurements.Visible = true;
                    CHRT_TrendLineDeltaE.Enabled = chkAnalyzeAverageOfMeasurements.Checked;
                   
                    string queryString = string.Empty;
                    if (chkAnalyzeAverageOfMeasurements.Checked)
                    {
                        CHRT_TrendLineDeltaE.MapAreas.Clear();
                        CHRT_TrendLineDeltaE.MapEnabled = false;

                        queryString = string.Format("&PressRunID={0}", Fuji.Common.Sessions.GetSession("SelectedPressRunID").ToString());
                        if (!string.IsNullOrEmpty(HDPressRunName.Value))
                            queryString = string.Format("{0}&PressRunName={1}", queryString, HDPressRunName.Value);
                    }

                    if (!string.IsNullOrEmpty(HDCurrentMeasurementName.Value))
                        queryString = string.Format("{0}&CurrentMeasurementName={1}", queryString, HDCurrentMeasurementName.Value);
                    
                    FRM1.Attributes.Add("src", "SyncDetailISOMeasurementChart.aspx?MeasurementID=" + HDCurrentMeasurement.Value.Trim() + queryString);
                }
                // }

                FRM1.Style.Add("display", "");
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }

    }
    /// <summary>
    /// Bind point in chart
    /// </summary>
    /// <param name="_Index">Index Value Intger</param>
    /// <param name="_objSeries">Series</param>
    /// <param name="_XValue">X Value</param>
    /// <param name="_YValue">Y Value</param>
    /// <param name="_Tooltip">Tooltip</param>
    /// <param name="_strHTMLColor">Html Color for line</param>
    /// <param name="_ELimit">Delta E Limit</param>
    private void AddPointToTrendDeltaE(int _Index, Series _objSeries, string _XValue, string _YValue, string _Tooltip, string _strHTMLColor, string _ELimit, string _MeasurementID)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;  // Set series type 
            _objSeries.Points.AddXY(_XValue, _YValue); // Add XY point 
            _objSeries.Color = System.Drawing.ColorTranslator.FromHtml(_strHTMLColor); // Set HTML color to series         
            _objSeries.Points[_Index].ToolTip = (_Tooltip.Contains("_") ? _Tooltip.Substring(0, _Tooltip.LastIndexOf('_')) : _Tooltip); //Set Tooltip for perticuler point.
            //====== Call javascript function for change current selected point and rebind chart ==================//
            _objSeries.Points[_Index].MapAreaAttributes = "OnClick=\"javascript:SetCurrentMeasurement( '" + _MeasurementID + "');\"";


            if (HDCurrentMeasurement.Value.Trim() != "" && Convert.ToInt32(_MeasurementID) == Convert.ToInt32(HDCurrentMeasurement.Value)) // Check current selected point
            {
                _objSeries.Points[_Index].MarkerSize = 4;
                _objSeries.Points[_Index].MarkerColor = Color.FromArgb(200, 255, 0, 0);
                _objSeries.Points[_Index].MarkerImage = "Images/MarkerRed_Circle.gif";  // Set red circle point image.
            }
            else if (Convert.ToDecimal(_YValue) > Convert.ToDecimal(_ELimit)) // Check limt of value
            {
                _objSeries.Points[_Index].MarkerSize = 4;
                _objSeries.Points[_Index].MarkerColor = Color.FromArgb(200, 255, 0, 0);
                _objSeries.Points[_Index].MarkerImage = "Images/MarkerRed.gif"; // Set tringle image
            }
            else
            {
                _objSeries.Points[_Index].MarkerSize = 4;
                _objSeries.Points[_Index].MarkerColor = Color.Gray;
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    /// <summary>
    /// This function use in html side for reomve some charecter in datename.
    /// </summary>
    /// <param name="FileName"></param>
    /// <returns></returns>
    public string OriginalFileNameReturn(string FileName)
    {
        try
        {
            if (FileName.Contains("_") && FileName.Contains("."))
                return FileName.Substring(0, FileName.LastIndexOf('_')) + FileName.Substring(FileName.LastIndexOf('.'));
            else
                return FileName;
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    /// <summary>
    /// This funtion is use in html side for dispaly isused image in grid.
    /// </summary>
    /// <param name="Status"></param>
    /// <returns></returns>
    public string UsedImageName(string Status)
    {
        try
        {
            if (Convert.ToBoolean(Status))
                return "Images/right-icon.png";

            return "Images/dot.png";
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    /// <summary>
    /// Visible true / false print condition as per output type
    /// </summary>
    /// <param name="objProjectType"></param>
    public void VisiblePrintConditionDetail(Enums.OutputType objOutputType)
    {
        switch (objOutputType)
        {

            case Enums.OutputType.Offset:
                TrOffsett.Visible = true;
                TrScreen.Visible = false;
                TrWideFormat.Visible = false;
                break;
            case Enums.OutputType.Screen:
                TrOffsett.Visible = false;
                TrScreen.Visible = true;
                TrWideFormat.Visible = false;
                break;
            case Enums.OutputType.Wideformat:
                TrOffsett.Visible = false;
                TrScreen.Visible = false;
                TrWideFormat.Visible = true;
                break;
        }
    }
    /// <summary>
    /// Clear all control in print condition detail 
    /// </summary>
    private void CleanPrintConditionDetail()
    {

        TxtGDSubstrateDesc.Text = string.Empty;
        TxtGDInk.Text = string.Empty;
        TxtOPDISO2846Ink.Text = string.Empty;
        TxtOPDFountainSolution.Text = string.Empty;
        TxtOPDBlanket.Text = string.Empty;
        TxtOPDScreening.Text = string.Empty;
        TxtOPDBlanketAge.Text = string.Empty;
        TxtSPDStencil.Text = string.Empty;
        TxtSPDCyan.Text = string.Empty;
        TxtSPDMagenta.Text = string.Empty;
        TxtSPDYellow.Text = string.Empty;
        TxtSPDBlack.Text = string.Empty;
        TxtSPDRip.Text = string.Empty;
        TxtSPDCTSFilmDevice.Text = string.Empty;
        TxtWFRip.Text = string.Empty;
        TxtWFPrintMode.Text = string.Empty;
        TxtWFScreeningType.Text = string.Empty;
        TxtWFUVLampSettings.Text = string.Empty;
        TxtNotes.Text = string.Empty;
    }
    /// <summary>
    /// Clear all control in G7 detail.
    /// </summary>
    private void CleanG7Detail()
    {
        TxtPaperTL.Text = "";
        TxtPaperTa.Text = "";
        TxtPaperTb.Text = "";
        TxtPaperTH.Text = "5";
        TxtCyanTL.Text = "";
        TxtCyanTa.Text = "";
        TxtCyanTb.Text = "";
        TxtCyanTH.Text = "5";
        TxtMagendaTL.Text = "";
        TxtMagendaTa.Text = "";
        TxtMagendaTb.Text = "";
        TxtMagendaTH.Text = "5";
        TxtYellowTL.Text = "";
        TxtYellowTa.Text = "";
        TxtYellowTb.Text = "";
        TxtYellowTH.Text = "5";
        TxtBlackTL.Text = "";
        TxtBlackTa.Text = "";
        TxtBlackTb.Text = "";
        TxtBlackTH.Text = "5";
        TxtRedTL.Text = "";
        TxtRedTa.Text = "";
        TxtRedTb.Text = "";
        TxtRedTH.Text = "6";
        TxtGreenTL.Text = "";
        TxtGreenTa.Text = "";
        TxtGreenTb.Text = "";
        TxtGreenTH.Text = "6";
        TxtBlueTL.Text = "";
        TxtBlueTa.Text = "";
        TxtBlueTb.Text = "";
        TxtBlueTH.Text = "6";
        Txt3CTL.Text = "";
        Txt3CTa.Text = "";
        Txt3CTb.Text = "";
        Txt3CTH.Text = "5";
        TxtDeltaFTH.Text = "1.5";
        TxtCyanDT.Text = "";
        TxtCyanDTH.Text = "0.1";
        TxtMagendaDT.Text = "";
        TxtMagendaDTH.Text = "0.1";
        TxtYellowDT.Text = "";
        TxtYellowDTH.Text = "0.1";
        TxtBalckDT.Text = "";
        TxtBalckDTH.Text = "0.1";
        Txt25TC.Text = "";
        Txt25TM.Text = "";
        Txt25TY.Text = "";
        Txt25TK.Text = "";
        Txt25TH.Text = "5";
        Txt50TC.Text = "";
        Txt50TM.Text = "";
        Txt50TY.Text = "";
        Txt50TK.Text = "";
        Txt50TH.Text = "5";
        Txt75TC.Text = "";
        Txt75TM.Text = "";
        Txt75TY.Text = "";
        Txt75TK.Text = "";
        Txt75TH.Text = "5";
    }
    /// <summary>
    /// Clear all control in ISO detail.
    /// </summary>
    private void CleanISODetail()
    {
        TxtISOCyanTL.Text = "";
        TxtISOCyanTa.Text = "";
        TxtISOCyanTb.Text = "";
        TxtISOCyanTH.Text = "5";
        TxtISOCyanHTH.Text = "2.50";
        TxtISOMagendaTL.Text = "";
        TxtISOMagendaTa.Text = "";
        TxtISOMagendaTb.Text = "";
        TxtISOMagendaTH.Text = "5";
        TxtISOMagendaHTH.Text = "2.50";
        TxtISOYellowTL.Text = "";
        TxtISOYellowTa.Text = "";
        TxtISOYellowTb.Text = "";
        TxtISOYellowTH.Text = "5";
        TxtISOYellowHTH.Text = "2.50";
        TxtISOBlackTL.Text = "";
        TxtISOBlackTa.Text = "";
        TxtISOBlackTb.Text = "";
        TxtISOBlackTH.Text = "5";
        TxtISORedTL.Text = "";
        TxtISORedTa.Text = "";
        TxtISORedTb.Text = "";
        TxtISORedTH.Text = "10";
        TxtISOGreenTL.Text = "";
        TxtISOGreenTa.Text = "";
        TxtISOGreenTb.Text = "";
        TxtISOGreenTH.Text = "10";
        TxtISOBlueTL.Text = "";
        TxtISOBlueTa.Text = "";
        TxtISOBlueTb.Text = "";
        TxtISOBlueTH.Text = "10";
        TxtISO3CTL.Text = "";
        TxtISO3CTa.Text = "";
        TxtISO3CTb.Text = "";
        TxtISO3CTH.Text = "10";
        TxtISOGray25TL.Text = "";
        TxtISOGray25Ta.Text = "";
        TxtISOGray25Tb.Text = "";
        TxtISOGray25TH.Text = "2";
        TxtISOGray50TL.Text = "";
        TxtISOGray50Ta.Text = "";
        TxtISOGray50Tb.Text = "";
        TxtISOGray50TH.Text = "2";
        TxtISOGray75TL.Text = "";
        TxtISOGray75Ta.Text = "";
        TxtISOGray75Tb.Text = "";
        TxtISOGray75TH.Text = "2";

        TxtISOAPPaperTL.Text = "";
        TxtISOAPPaperTa.Text = "";
        TxtISOAPPaperTb.Text = "";
        TxtISOTHPaperTL.Text = "3";
        TxtISOTHPaperTa.Text = "2";
        TxtISOTHPaperTb.Text = "2";


        TxtISO25CA.Text = "";
        TxtISO25CT.Text = "3";
        TxtISO25MA.Text = "";
        TxtISO25MT.Text = "3";
        TxtISO25YA.Text = "";
        TxtISO25YT.Text = "3";
        TxtISO25KA.Text = "";
        TxtISO25KT.Text = "3";

        TxtISO50CA.Text = "";
        TxtISO50CT.Text = "4";
        TxtISO50MA.Text = "";
        TxtISO50MT.Text = "4";
        TxtISO50YA.Text = "";
        TxtISO50YT.Text = "4";
        TxtISO50KA.Text = "";
        TxtISO50KT.Text = "4";

        TxtISO75CA.Text = "";
        TxtISO75CT.Text = "3";
        TxtISO75MA.Text = "";
        TxtISO75MT.Text = "3";
        TxtISO75YA.Text = "";
        TxtISO75YT.Text = "3";
        TxtISO75KA.Text = "";
        TxtISO75KT.Text = "3";


        TxtISOQuarterTone.Text = "3";
        TxtISOMidTone.Text = "5";
        TxtISO3QuarterTone.Text = "3";

        TxtISOCyanDT.Text = "";
        TxtISOMagendaDT.Text = "";
        TxtISOYellowDT.Text = "";
        TxtISOBalckDT.Text = "";
    }
    #endregion




}
