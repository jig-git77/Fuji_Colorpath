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
#endregion

#region Using Application
using Telerik.WebControls;
using Fuji.Common;
using Fuji.Business;
#endregion

public partial class MasterCompany : PageBase
{
    #region Private Variable Declaration
    //========== Declare the constant page size for the grid ===========//
    private const int ConstDefaultPageSize = 25;
    #endregion

    #region PageLoad and Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //=========== Clear Error ==========//
            LBL_Error.Text = "";

            //=========== Default Settings ==========//
            if (IsPostBack == false)
            {
                //=========== Check Login ==========//
                Sessions.CheckUserLogin();

                //========== Check User Role and redirect to home page as per user role ===========//
                Fuji.Common.CommonFunctions.CheckUserRole();

                //========== Fill Variables to Form ==========//
                SetLabels();

                //=========== Bind CompanyList ==========//                
                BindMasterCompany();
                
                //=========== Fill Brand List ==========//                
                FillBrandList();

                //=========== Set Css ==========//
                //Fuji.Common.CommonFunctions.SetGridViewDefaultSettings(GV_MasterCompany);
                RG_MasterCompany.PageSize = 25;

                //=========== Clear Form ==========//
                BTN_Cancel_Click(null, null);

                //========== Check User rights for the company management===========//
                CheckRights();
            }
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void BTN_AddCompany_Click(object sender, EventArgs e)
    {
        try
        {
            TBLFORM.Visible = true;
            TBLGRID.Visible = false;
            BTN_AddCompany.Visible = false;
            LTRL_SubTitle.Text = Resources.MasterCompany.LBL_SubTitle_Text_Add;
        }
        catch (Exception Ex)
        {
            LBL_Error.Text = HandleException(Ex);
        }
    }

    protected void BTN_AddSave_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Validations ==========//
            if (TXT_CompanyName.Text.Trim() == "") { LBL_Error.Text = Resources.Message.CompanyNameIsRequired; return; }
            //if (DDL_BrandName.SelectedIndex == 0) { LBL_Error.Text = Resources.Message.BrandingNameRequired; return; }

            //==========  Declaring Objects ==========//
            CompanyData objCompany = new CompanyData();

            //========== Declare DataTable Object  ===========//
            DataTable DT_Company = new DataTable();

            //========== Assign Value to Company object ===========//
            objCompany.CompanyName = TXT_CompanyName.Text.Trim();
            objCompany.BrandID = Convert.ToInt32(DDL_BrandName.SelectedValue);
            objCompany.CompanyID = Convert.ToInt32(LTRL_CompanyID.Text.Trim());

            //========== Check For duplicate company name ===========//
            DT_Company = objCompany.uspGetCheckDuplicateCompany();

            //========== Show existing company message ===========//
            if (DT_Company != null && DT_Company.Rows.Count > 0)
            { LBL_Error.Text = Resources.Message.CompanyAlreadyExists; return; }

            //========== If company id not exist then insert new company ===========//
            if (LTRL_CompanyID.Text.Trim() == "0")
            {
                objCompany.CompanyName = TXT_CompanyName.Text.Trim();
                objCompany.BrandID = Convert.ToInt32(DDL_BrandName.SelectedValue);
                objCompany.uspInsertCompany();

                //========== Show successful insertion message ===========//
                LBL_Error.Text = "Company added successfully.";
            }

            //========== If company id exist then update existing company ===========//
            if (LTRL_CompanyID.Text.Trim() != "0")
            {
                objCompany.CompanyID = Convert.ToInt32(LTRL_CompanyID.Text.Trim());
                objCompany.CompanyName = TXT_CompanyName.Text.Trim();
                objCompany.BrandID = Convert.ToInt32(DDL_BrandName.SelectedValue);
                objCompany.uspUpdateCompany();

                //========== Show successful updation message ===========//
                LBL_Error.Text = "Company updated successfully.";
            }

            //=========== Default Settings ==========//
            BTN_Cancel_Click(null, null); Fuji.Common.Sessions.SetSessions("CompanyData", null);

            //========== Bind Master Company Data ===========//
            BindMasterCompany();
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void BTN_Cancel_Click(object sender, EventArgs e)
    {
        try
        {
            BTN_AddSave.Text = Resources.MasterCompany.BTN_AddSave_Add;
            LTRL_CompanyID.Text = "0"; TXT_CompanyName.Text = "";
            BTN_AddCompany.Text = Resources.MasterCompany.BTN_AddCompany_Text; TBLFORM.Visible = false;
            TBLGRID.Visible = true; BTN_AddCompany.Visible = true;
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void RG_MasterCompany_PageIndexChanged(object source, Telerik.WebControls.GridPageChangedEventArgs e)
    {
        try
        {
            //========== Assign new page index to current page indes ===========//
            RG_MasterCompany.CurrentPageIndex = e.NewPageIndex;

            //========== Bind CompanyData ===========//
            BindMasterCompany();
        }
        catch (Exception exc)
        { LBL_Error.Text = HandleException(exc); }
    }

    protected void RG_MasterCompany_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        try
        {
            //========== Check Company Data in session if exist then bind it ===========//
            if (Fuji.Common.Sessions.GetSession("CompanyData") != null)
            { RG_MasterCompany.DataSource = Fuji.Common.Sessions.GetSession("CompanyData"); }
        }
        catch (Exception exc)
        { LBL_Error.Text = HandleException(exc); }
    }

    protected void RG_MasterCompany_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            //========== If Command Name is Edit then comes to this end of code ===========//
            if (e.CommandName == "EditCompany")
            {
                LTRL_CompanyID.Text = e.Item.Cells[2].Text.Trim();                
                TXT_CompanyName.Text = e.Item.Cells[4].Text.Trim();
                if (DDL_BrandName.Items.FindByValue(e.Item.Cells[3].Text.Trim()) != null)
                    DDL_BrandName.SelectedValue = e.Item.Cells[3].Text.Trim();
                else
                    DDL_BrandName.SelectedIndex = 0;
                BTN_AddSave.Text = Resources.MasterCompany.BTN_AddSave_Save; LTRL_SubTitle.Text = Resources.MasterCompany.LBL_SubTitle_Text_Edit;
                TBLFORM.Visible = true; TBLGRID.Visible = false; BTN_AddCompany.Visible = false;
            }
            //========== If Command Name is Delete then comes to this end of code ===========//
            if (e.CommandName == "Delete")
            {
                //==========  Declaring Objects of Company==========//
                CompanyData objCompany = new CompanyData();

                //========== Assign value to company object ===========//
                objCompany.CompanyID = Convert.ToInt32(e.Item.Cells[2].Text.Trim());
                objCompany.uspDeleteCompany();

                //========== Show successful deletion message ===========//
                LBL_Error.Text = "Company deleted successfully.";

                //========== Fill Company DAta to session object of company ===========//
                Fuji.Common.Sessions.SetSessions("CompanyData", null);

                //========== Clear all filtering options , if any ===========//
                btnReset_Click(null, null);

                //========== Bind the Company Data ===========//
                BindMasterCompany();
            }
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void RG_MasterCompany_SortCommand(object source, Telerik.WebControls.GridSortCommandEventArgs e)
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
        { LBL_Error.Text = HandleException(exc); }
    }

    protected void RG_MasterCompany_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            //========== Check The Item is item or alternating item then comes to this end of code ===========//
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                ImageButton IMGBTN_Delete = (ImageButton)e.Item.FindControl("IMGBTN_Delete");
                IMGBTN_Delete.Attributes.Add("OnClick", "javascript : if(confirm('" + Resources.Message.DeleteConfirmMessage_Text + "') == true) return true; else return false;");
            }
            //========== Check for the pager style function ===========//
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
            {
                #region Pager Style

                //========== Find the label and link button inthe pager ===========//
                Literal LTRL_RecordText = (Literal)e.Item.FindControl("LTRL_RecordText");
                ImageButton LBTN_First = (ImageButton)e.Item.FindControl("LBTN_First");
                ImageButton LBTN_Prev = (ImageButton)e.Item.FindControl("LBTN_Prev");
                ImageButton LBTN_Next = (ImageButton)e.Item.FindControl("LBTN_Next");
                ImageButton LBTN_Last = (ImageButton)e.Item.FindControl("LBTN_Last");

                //========== Declare the variable fot the pages and assign to initial value ===========//
                int IntFirstIndexInPage = 0, IntLastIndexInPage = 0, IntDataSourceCount = 0;

                //========== Assign Grid value to Variable ===========//
                IntFirstIndexInPage = RG_MasterCompany.MasterTableView.PagingManager.FirstIndexInPage;
                IntLastIndexInPage = RG_MasterCompany.MasterTableView.PagingManager.LastIndexInPage + 1;
                IntDataSourceCount = RG_MasterCompany.MasterTableView.PagingManager.DataSourceCount;

                if (IntDataSourceCount > 0)
                {
                    IntFirstIndexInPage++;
                }

                LTRL_RecordText.Text = "Record(s) : " + IntFirstIndexInPage + " to " + IntLastIndexInPage + " of " + IntDataSourceCount + " | ";

                if (RG_MasterCompany.MasterTableView.PagingManager.IsFirstPage)
                {
                    LBTN_First.ImageUrl = "Images/Nav_FirstPage_off.gif";
                    LBTN_Prev.ImageUrl = "Images/Nav_PreviousPage_off.gif";
                    LBTN_First.Style.Add("cursor", "");
                    LBTN_Prev.Style.Add("cursor", "");
                    LBTN_First.Attributes.Add("disabled", "true");
                    LBTN_Prev.Attributes.Add("disabled", "true");
                }
                else
                {
                    LBTN_First.ImageUrl = "Images/Nav_FirstPage.gif";
                    LBTN_Prev.ImageUrl = "Images/Nav_PreviousPage.gif";
                    LBTN_First.Style.Add("cursor", "pointer");
                    LBTN_Prev.Style.Add("cursor", "pointer");
                }
                if (RG_MasterCompany.MasterTableView.PagingManager.IsLastPage == true)
                {
                    LBTN_Next.ImageUrl = "Images/Nav_NextPage_off.gif";
                    LBTN_Last.ImageUrl = "Images/Nav_LastPage_off.gif";
                    LBTN_Next.Style.Add("cursor", "");
                    LBTN_Last.Style.Add("cursor", "");
                    LBTN_Next.Attributes.Add("disabled", "true");
                    LBTN_Last.Attributes.Add("disabled", "true");
                }
                else
                {
                    LBTN_Next.ImageUrl = "Images/Nav_NextPage.gif";
                    LBTN_Last.ImageUrl = "Images/Nav_LastPage.gif";
                    LBTN_Next.Style.Add("cursor", "pointer");
                    LBTN_Last.Style.Add("cursor", "pointer");
                }
                #endregion
            }
        }
        catch (Exception exc)
        { LBL_Error.Text = HandleException(exc); }
    }

    protected void RG_MasterCompany_ItemCreated(object sender, GridItemEventArgs e)
    {
        try
        {
            //========== Check and create events for the pager selection  ===========//
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
            {
                //========== Find the Record page dropdownlist ===========//
                DropDownList DDL_RecordPage = (DropDownList)e.Item.FindControl("DDL_RecordPage");
                if (DDL_RecordPage != null)
                {
                    //========== Declare its event ===========//
                    DDL_RecordPage.SelectedIndexChanged += new EventHandler(DDlRecordPage_SelectedIndexChanged);

                    if (!IsPostBack)
                    {
                        //========== Assign record page selection value ===========//
                        DDL_RecordPage.SelectedValue = ConstDefaultPageSize.ToString();
                    }
                    else
                    {
                        if (ViewState["RecordPerPage"] != null)
                        {
                            DDL_RecordPage.SelectedIndex = -1;
                            DDL_RecordPage.Items.FindByValue(ViewState["RecordPerPage"].ToString()).Selected = true;
                        }
                    }
                }

                //========== Find the Reset button in the pager style ===========//
                Button BTN_Reset = (Button)e.Item.FindControl("BTN_Reset");
                BTN_Reset.Text = Resources.MasterCompany.BTN_Reset_Text;

                if (BTN_Reset != null)
                {
                    //========== Declate the reset button event ===========//
                    BTN_Reset.Click += new EventHandler(btnReset_Click);
                }
            }
        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }

    private void DDlRecordPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            //========== Declare the variable for the selection value of the record page dropdownlist ===========//
            string StrSelectedValue;
            StrSelectedValue = ((DropDownList)sender).SelectedItem.Value;
            ViewState["RecordPerPage"] = StrSelectedValue;

            //========== Set the paze size ===========//
            RG_MasterCompany.PageSize = Convert.ToInt32(StrSelectedValue);
            RG_MasterCompany.CurrentPageIndex = 0;
            //========== Check for branch data in session and assign if exist ===========//
            if (Fuji.Common.Sessions.GetSession("CompanyData") != null)
            {
                //========== Assign the branch data fro the session value ===========//
                RG_MasterCompany.DataSource = Fuji.Common.Sessions.GetSession("CompanyData");
            }

            //========== Bind the grid ===========//
            RG_MasterCompany.DataBind();
        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Check grid column in the grid ===========//
            foreach (Telerik.WebControls.GridColumn column in RG_MasterCompany.MasterTableView.Columns)
            {
                //========== set all column filter value to empty ===========//
                column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                column.CurrentFilterValue = String.Empty;

            }

            //========== set all filter expression to empty ===========//
            RG_MasterCompany.MasterTableView.FilterExpression = String.Empty;
            RG_MasterCompany.MasterTableView.Rebind();
        }
        catch (Exception exc)
        { LBL_Error.Text = HandleException(exc); }
    }

    #endregion

    #region Methods and Events
    #region Set Labels
    /// <summary>
    /// Used to set labels of the form
    /// </summary>
    private void SetLabels()
    {
        try
        {
            //========== Set Culture ==========//
            Resources.Message.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));
            Resources.MasterCompany.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));

            //========== Set Values ==========//
            LTRL_Title.Text = Resources.MasterCompany.LBL_Title_Text;
            LTRL_SubTitle.Text = Resources.MasterCompany.LBL_SubTitle_Text_Add;
            LTRL_CompanyName.Text = Resources.MasterCompany.LBL_CompanyName_Text;
            LTRL_BrandName.Text = Resources.MasterCompany.LBL_BrandName_Text;

            BTN_AddSave.Text = Resources.MasterCompany.BTN_AddSave_Add;
            BTN_Cancel.Text = Resources.MasterCompany.BTN_Cancel_Text;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Bind Maseter Company
    /// <summary>
    /// Used to bind the master company data
    /// </summary>
    private void BindMasterCompany()
    {
        try
        {
            //==========  Declaring Objects ==========//
            CompanyData objCompany = new CompanyData();

            //==========  Get Data Table and Bind ==========//
            DataTable DT_Company = new DataTable();
            if (Fuji.Common.Sessions.GetSession("CompanyData") == null)
            {
                //DT_Company = objCompany.uspGetAllCompany();
                DT_Company = objCompany.uspGetAllCompanyByUserID(Sessions.UserID, Sessions.RoleID);
                Fuji.Common.Sessions.SetSessions("CompanyData", DT_Company);
            }

            //========== Get Company data from the session ===========//
            DT_Company = (DataTable)Fuji.Common.Sessions.GetSession("CompanyData");

            //========== Assign the session data to Grid ===========//
            if (DT_Company != null && DT_Company.Rows.Count > 0)
            { RG_MasterCompany.DataSource = DT_Company; RG_MasterCompany.DataBind(); }
            else
            { RG_MasterCompany.DataSource = null; RG_MasterCompany.DataBind(); }

            //========== Show the pager style if page count is greater than 1 ===========//
            if (RG_MasterCompany.Items.Count == 0)
            { RG_MasterCompany.PagerStyle.Visible = false; RG_MasterCompany.MasterTableView.AllowFilteringByColumn = false; RG_MasterCompany.AllowSorting = false; }
            else
            { RG_MasterCompany.PagerStyle.Visible = true; RG_MasterCompany.MasterTableView.AllowFilteringByColumn = true; RG_MasterCompany.AllowSorting = true; }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Check User Rights
    /// <summary>
    /// Used to check user rights for the company management
    /// </summary>
    public void CheckRights()
    {
        try
        {
            //========== Check Whether the logged in user has the permission to Add New Device, if not then hide Add new Devicebutton ===========//
            if (Fuji.Common.Sessions.Add_Company == true)
                BTN_AddCompany.Visible = true;
            else BTN_AddCompany.Visible = false;

            //========== Check Whether the logged in user has the permission to Edit Device, if not then hide Edit Column from the Device list ===========//
            if (Fuji.Common.Sessions.Add_Company == true)
                RG_MasterCompany.Columns[2].Visible = true;
            else RG_MasterCompany.Columns[2].Visible = false;

            //========== Check Whether the logged in user has the permission to Delete Device, if not then hide Delete Column from the Device list ===========//
            //if (Fuji.Common.Sessions.Delete_Company == true)
            //    RG_MasterCompany.Columns[3].Visible = true;
            //else 
            RG_MasterCompany.Columns[3].Visible = false;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Fill Brand List
    /// <summary>
    /// fill the dropdown of Brand Name
    /// </summary>
    protected void FillBrandList()
    {
        try
        {
            //==========  Declaring Objects ==========//
            CompanyBrandingData ObjCompanyBrandingData = new CompanyBrandingData();
            
            //==========  Fetching the Data From Taskero User ==========//            
            DataTable _DT = ObjCompanyBrandingData.uspGetCompanyBrandingData(Sessions.UserID);

            DDL_BrandName.DataSource = _DT;
            DDL_BrandName.DataTextField = "BrandName";
            DDL_BrandName.DataValueField = "BrandID";
            DDL_BrandName.DataBind();

            //========== Insert Items DropDownList in Brand ===========//
            DDL_BrandName.Items.Insert(0, new ListItem("Default", "0"));            
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }
    #endregion
    #endregion
}