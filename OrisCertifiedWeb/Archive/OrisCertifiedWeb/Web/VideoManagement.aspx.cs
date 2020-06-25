
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
using Fuji.Business;
using Fuji.Common;
using Telerik.WebControls;
#endregion

public partial class VideoManagement : PageBase
{
    #region Declaration
    //========== Declare the constant page size for the grid ===========//
    private const int ConstDefaultPageSize = 25;
    #endregion

    #region Page Load and Events
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Error ===========//
            LBL_Error.Text = "";

            // ==== Set ProgressArea properties ==== //
            ProgressIndicator();
            if (IsPostBack == false)
            {
                
                FU_VideoFile.AllowedFileExtensions = new string[] { ".mov", ".flv",".wmv" };
                
                //RadProgressArea1.ProgressIndicators &= ~ProgressIndicators.SelectedFilesCount;

                RadProgressArea1.Localization["UploadedFiles"] = "Processed ";
                RadProgressArea1.Localization["UploadedFiles"] = "Processed ";
                RadProgressArea1.Localization["TotalFiles"] = "";
                RadProgressArea1.Localization["CurrentFileName"] = "File: ";

                RadProgressContext progress = RadProgressContext.Current;
                progress["PrimaryTotal"] = "0";
                progress["PrimaryValue"] = "0";
                progress["PrimaryPercent"] = "0";
                progress["SecondaryTotal"] = "0";

                RDO_UploadTo.SelectedIndex = 0;

                //========== Check Session ===========//
                Fuji.Common.Sessions.CheckUserLogin();

                //========== Check User Role and redirect to home page as per user role ===========//
                Fuji.Common.CommonFunctions.CheckUserRole();

                //========== Clear the Session On first view of the page ===========//
                //Fuji.Common.Sessions.SetSessions("BranchData", null);
                //Fuji.Common.Sessions.SetSessions("AssignSupportUserBranchData", null);

                //========== Fill Variables to Form ==========//
                SetLabels();

                //=========== Bind Categories ==========//
                BindCategories();
                BindCategoriesDropDown();

                //=========== Set Css ==========//
                RG_Categories.PageSize = 25;

                //=========== Bind Categories ==========//
                BindVideoCategoriesDropDown();
                BindVideos();

                //divCategories.Style.Add("display", "none");
                //hdnNavID.Value = "1";
            }
            //if (hdnNavID.Value == "1")
            //{
            //    divCategories.Style.Add("display", "none");
            //    divVideo.Style.Add("display", "block");
            //}
            //else
            //{
            //    divCategories.Style.Add("display", "block");
            //    divVideo.Style.Add("display", "none");
            //}

            TXT_VideoDescription.Attributes.Add("onKeyUp", "return CountCharacter('Description',2000);");
            TXT_KeyWords.Attributes.Add("onKeyUp", "return CountCharacter('KeyWords',300);");


        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }
    //============ This funciton set progress Area properties =============//
    protected void ProgressIndicator()
    {
        try
        {
            //==== Show just upper progressBar, FileName and related property ====//
            ProgressIndicators indicators = ProgressIndicators.None;
            indicators |= ProgressIndicators.TotalProgressBar;
            indicators |= ProgressIndicators.TotalProgress;
            indicators |= ProgressIndicators.TotalProgressPercent;
            indicators |= ProgressIndicators.RequestSize;
            indicators |= ProgressIndicators.CurrentFileName;

            //==== And assign those all properties to ProgressArea ====//
            RadProgressArea1.ProgressIndicators = indicators;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void AddChildDataToDropDown(DataTable DT_Categories, int CategoryID, int intSpaces)
    {
        try
        {
            string strDescription = "";
            string strCategoryID = "";
            DataTable DT_CategoriesCopy = DT_Categories.Copy();
            DataView dv = DT_Categories.DefaultView;
            dv.RowFilter = "ParentID = " + CategoryID;
            for (int i = 0; i < dv.Count; i++)
            {
                strDescription = dv[i]["Description"].ToString();
                strCategoryID = dv[i]["CategoryID"].ToString();
                RadComboBoxItem item = new Telerik.WebControls.RadComboBoxItem();
                DataView dvCopy = DT_CategoriesCopy.DefaultView;
                if (int.Parse(dv[i]["CountVideos"].ToString()) > 0)
                {
                    item.Enabled = false;
                    item.Font.Bold = true;
                    item.Font.Italic = true;
                }
                else
                {
                    item.Enabled = true;
                    item.Font.Bold = false;
                    item.Font.Italic = false;
                }
                //dvCopy.RowFilter = "ParentID = " + int.Parse(dv[i]["CategoryID"].ToString());
                //if (dvCopy.Count > 0)
                //{
                //    item.Enabled = false;
                //    item.Font.Bold = true;
                //    item.Font.Italic = true;
                //}
                //else
                //{
                //    item.Enabled = true;
                //    item.Font.Bold = false;
                //    item.Font.Italic = false;
                //}
                //intSpaces = intSpaces + 10;
                item.Style.Add("padding-left", (intSpaces + 20).ToString() + "px");
                item.Text = dv[i]["Description"].ToString();
                item.Value = dv[i]["CategoryID"].ToString();
                DDL_Categories.Items.Add(item);
                dvCopy.RowFilter = "";
                AddChildDataToDropDown(DT_CategoriesCopy, int.Parse(dv[i]["CategoryID"].ToString()), intSpaces + 20);
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    #endregion

    #region Functions And Methods

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
            Resources.VideoManagement.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));

            //========== Set Controls Text ==========//
            LTRL_Title.Text = Resources.VideoManagement.LTRL_Title_Text;
            LTRL_Categories.Text = Resources.VideoManagement.LTRL_Categories_Text;
            LTRL_CategorySubCategory.Text = Resources.VideoManagement.LTRL_CategorySubCategory_Text;
            LTRL_CategoryDescription.Text = Resources.VideoManagement.LTRL_CategoryDescription_Text;
            BTN_AddCategory.Text = Resources.VideoManagement.BTN_AddCategory_Text;
            LTRL_CategoriesList.Text = Resources.VideoManagement.LTRL_CategoriesList_Text;
            LTRL_Videos.Text = Resources.VideoManagement.LTRL_Videos_Text;
            LTRL_VideoCategorySubCategory.Text = Resources.VideoManagement.LTRL_VideoCategorySubCategory_Text;
            LTRL_LinkText.Text = Resources.VideoManagement.LTRL_LinkText_Text;
            LTRL_VideoFile.Text = Resources.VideoManagement.LTRL_VideoFile_Text;
            LTRL_UploadTo.Text = Resources.VideoManagement.LTRL_UploadTo_Text;
            LTRL_VideoDescription.Text = Resources.VideoManagement.LTRL_VideoDescription_Text;
            LTRL_Keywords.Text = Resources.VideoManagement.LTRL_Keywords_Text;
            BTN_AddVideo.Text = Resources.VideoManagement.BTN_AddVideo_Text;
            LTRL_DescriptionLimit.Text = Resources.VideoManagement.LTRL_DescriptionLimit_Text;
            LTRL_KeywordsLimit.Text = Resources.VideoManagement.LTRL_KeywordsLimit_Text;
            LTRL_VideosList.Text = Resources.VideoManagement.LTRL_VideosList_Text;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #endregion

    #region Validate Input Fields
    /// <summary>
    /// Used to Validate the input fields of the Category form
    /// </summary>
    /// <returns></returns>
    private bool ValidateCategoriesInputFields()
    {
        try
        {
            //========== Check Parent Category is selected or not, show message if not ===========//
            if (DDL_Categories.SelectedIndex == -1)
            {
                LBL_Error.Text = Resources.VideoManagement.ParentCategoryIsRequired;
                return false;
            }

            //========== Check Category Description is entered or not, show message if not ===========//
            if (TXT_CategoryDescription.Text.Trim().Length == 0)
            {
                LBL_Error.Text = Resources.VideoManagement.CategoryDescriptionIsRequired;
                return false;
            }

            return true;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    /// <summary>
    /// Used to Validate the input fields of the Video form
    /// </summary>
    /// <returns></returns>
    private bool ValidateVideosInputFields()
    {
        try
        {
            //========== Check Parent Category is selected or not, show message if not ===========//
            if (DDL_VideoCategorySubCategory.SelectedIndex == -1)
            { LBL_Error.Text = Resources.VideoManagement.CategorySubCategoryIsRequired; return false; }

            if (DDL_VideoCategorySubCategory.SelectedValue == "0")
            { LBL_Error.Text = Resources.VideoManagement.CategorySubCategoryIsRequired; return false; }

            //========== Check Link Text is entered or not, show message if not ===========//
            if (TXT_LinkText.Text.Trim().Length == 0)
            { LBL_Error.Text = Resources.VideoManagement.LinkTextIsRequired; return false; }

            return true;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region "Categories"
    protected void BTN_AddCategory_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateCategoriesInputFields())
            {
                //========== Declare the object of the Categories ===========//
                CategoriesData ObjCategory = new CategoriesData();
                int intResult;

                //========== Assign the properties to the object of the Category ===========//
                if (LTRL_CategoryID.Text.Trim().Equals(""))
                    ObjCategory.CategoryID = 0;
                else
                    ObjCategory.CategoryID = int.Parse(LTRL_CategoryID.Text.Trim());

                ObjCategory.Description = TXT_CategoryDescription.Text.Trim();
                ObjCategory.ParentID = Convert.ToInt32(DDL_Categories.SelectedValue);
                ObjCategory.CreatedBy = Sessions.UserID;
                ObjCategory.ModifiedBy = Sessions.UserID;

                //========== Check That Operation is insert or not by id ===========//
                if (ObjCategory.CategoryID == 0)
                {
                    //========== Insertion new category by calling insert method ===========//
                    intResult = ObjCategory.uspInsertCategory();
                    if (intResult == 0)
                    {
                        //========== Show insertion message ===========//
                        LBL_Error.Text = Resources.Message.BranchNameAlreadyExists; return;
                    }
                    else
                        //========== Show insertion message ===========//
                        LBL_Error.Text = "New Category inserted successfully";
                }
                else
                {
                    //========== Update Category by calling update method ===========//
                    intResult = ObjCategory.uspUpdateCategory();
                    if (intResult == 0)
                    {
                        //========== Show insertion message ===========//
                        LBL_Error.Text = Resources.Message.BranchNameAlreadyExists; return;
                    }
                    else
                    {
                        //========== Show updation message ===========//
                        LBL_Error.Text = "Category updated successfully";

                        //========== Set Branch ID to initial Zero null ===========//
                        LTRL_CategoryID.Text = "0";
                    }
                }

                divAddCategory.Style.Add("display", "none");
                //========== Clear the Session On first view of the page ===========//
                DDL_Categories.SelectedIndex = 0;
                TXT_CategoryDescription.Text = "";

                //========== Clear the Session On first view of the page ===========//
                Fuji.Common.Sessions.SetSessions("CategoriesData", null);

                ////========== Show Categories List ===========//
                BindCategories();
                BindCategoriesDropDown();
                BindVideoCategoriesDropDown();

                LTRL_CategoryID.Text = "0";
            }
            else
            {
                divAddCategory.Style.Add("display", "block");
            }
        }
        catch (Exception exc)
        {
            divAddCategory.Style.Add("display", "block");
            LBL_Error.Text = HandleException(exc);
        }
    }

    protected void RG_Categories_PageIndexChanged(object source, Telerik.WebControls.GridPageChangedEventArgs e)
    {
        try
        {
            //========== Assign new page index to current page indes ===========//
            RG_Categories.CurrentPageIndex = e.NewPageIndex;

            //========== Bind Categories Data ===========//
            BindCategories();
        }
        catch (Exception exc)
        { LBL_Error.Text = HandleException(exc); }
    }

    protected void RG_Categories_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        try
        {
            //========== Check Categories Data in session if exist then bind it ===========//
            if (Fuji.Common.Sessions.GetSession("CategoriesData") != null)
            { RG_Categories.DataSource = Fuji.Common.Sessions.GetSession("CategoriesData"); }
        }
        catch (Exception exc)
        { LBL_Error.Text = HandleException(exc); }
    }

    protected void RG_Categories_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            //========== If Command Name is Edit then comes to this end of code ===========//
            if (e.CommandName == "EditCategory")
            {
                LTRL_CategoryID.Text = e.Item.Cells[2].Text.Trim();
                if (DDL_Categories.FindItemByValue(e.Item.Cells[4].Text.Trim()) != null)
                    DDL_Categories.SelectedValue = e.Item.Cells[4].Text.Trim();
                else
                    DDL_Categories.SelectedIndex = 0;
                TXT_CategoryDescription.Text = e.Item.Cells[5].Text.Trim();

                divAddCategory.Style.Add("display", "block");
                
            }
            //========== If Command Name is Delete then comes to this end of code ===========//
            if (e.CommandName == "Delete")
            {
                //==========  Declaring Objects of Category==========//
                CategoriesData objCategory = new CategoriesData();

                //========== Assign value to Category object ===========//
                objCategory.CategoryID = Convert.ToInt32(e.Item.Cells[2].Text.Trim());
                objCategory.uspDeleteCategory();

                //========== Show successful deletion message ===========//
                LBL_Error.Text = "Category deleted successfully.";

                //========== Clear all filtering options , if any ===========//
                btnReset_Click(null, null);

                //========== Clear the Session On first view of the page ===========//
                Fuji.Common.Sessions.SetSessions("CategoriesData", null);

                ////========== Show Categories List ===========//
                BindCategories();
                BindCategoriesDropDown();

                LTRL_CategoryID.Text = "0";
            }
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void RG_Categories_SortCommand(object source, Telerik.WebControls.GridSortCommandEventArgs e)
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

    protected void RG_Categories_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            //========== Check The Item is item or alternating item then comes to this end of code ===========//
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                DataRowView drv = (DataRowView)(e.Item.DataItem);

                ImageButton IMGBTN_Delete = (ImageButton)e.Item.FindControl("IMGBTN_Delete");
                IMGBTN_Delete.Attributes.Add("OnClick", "javascript : if(confirm('" + Resources.Message.DeleteConfirmMessage_Text + "') == true) return true; else return false;");

                //========== Find the ImageButton to assign category to user permissions ===========//
                ImageButton IMGBTN_Permissions = (ImageButton)e.Item.FindControl("IMGBTN_Permissions");
                IMGBTN_Permissions.Attributes.Add("OnClick", "OpenUserPermissionWindow('CategoryUserManagement.aspx?CategoryID=" + ((DataRowView)e.Item.DataItem)["CategoryID"].ToString() + "');");
                if (int.Parse(drv["IsParent"].ToString()) == 1)
                    IMGBTN_Permissions.Visible = false;
                else
                    IMGBTN_Permissions.Visible = true;
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
                IntFirstIndexInPage = RG_Categories.MasterTableView.PagingManager.FirstIndexInPage;
                IntLastIndexInPage = RG_Categories.MasterTableView.PagingManager.LastIndexInPage + 1;
                IntDataSourceCount = RG_Categories.MasterTableView.PagingManager.DataSourceCount;

                if (IntDataSourceCount > 0)
                {
                    IntFirstIndexInPage++;
                }

                LTRL_RecordText.Text = "Record(s) : " + IntFirstIndexInPage + " to " + IntLastIndexInPage + " of " + IntDataSourceCount + " | ";

                if (RG_Categories.MasterTableView.PagingManager.IsFirstPage)
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
                if (RG_Categories.MasterTableView.PagingManager.IsLastPage == true)
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

    protected void RG_Categories_ItemCreated(object sender, GridItemEventArgs e)
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
            RG_Categories.PageSize = Convert.ToInt32(StrSelectedValue);
            RG_Categories.CurrentPageIndex = 0;
            //========== Check for branch data in session and assign if exist ===========//
            if (Fuji.Common.Sessions.GetSession("CategoriesData") != null)
            {
                //========== Assign the branch data fro the session value ===========//
                RG_Categories.DataSource = Fuji.Common.Sessions.GetSession("CategoriesData");
            }

            //========== Bind the grid ===========//
            RG_Categories.DataBind();
        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }

    private void BindCategories()
    {
        try
        {
            //==========  Declaring Objects ==========//
            CategoriesData objCategory = new CategoriesData();

            //==========  Get Data Table and Bind ==========//
            DataTable DT_Categories = new DataTable();
            if (Fuji.Common.Sessions.GetSession("CategoriesData") == null)
            {
                DT_Categories = objCategory.uspGetAllCategories();

                //============ Updated By Manish : 12-03-2009 NOTE : This logic is applied at database level
                //DT_Categories.Columns.Add("ParentDescription");
                //DT_Categories.Columns.Add("IsParent");

                //DataTable DT_CategoriesCopy = DT_Categories.Copy();
                //for (int i = 0; i < DT_Categories.Rows.Count; i++)
                //{
                //    DT_Categories.Rows[i]["ParentDescription"] = GetParentDesription(int.Parse(DT_Categories.Rows[i]["ParentID"].ToString()), DT_CategoriesCopy);
                //    DT_Categories.Rows[i]["IsParent"] = CheckIsParent(int.Parse(DT_Categories.Rows[i]["CategoryID"].ToString()), DT_CategoriesCopy);
                //}



                Fuji.Common.Sessions.SetSessions("CategoriesData", DT_Categories);
            }

            //========== Get Categories data from the session ===========//
            DT_Categories = (DataTable)Fuji.Common.Sessions.GetSession("CategoriesData");

            //========== Assign the session data to Grid ===========//
            if (DT_Categories != null && DT_Categories.Rows.Count > 0)
            { RG_Categories.DataSource = DT_Categories; RG_Categories.DataBind(); }
            else
            { RG_Categories.DataSource = null; RG_Categories.DataBind(); }

            //========== Show the pager style if page count is greater than 1 ===========//
            if (RG_Categories.Items.Count == 0)
            { RG_Categories.PagerStyle.Visible = false; RG_Categories.MasterTableView.AllowFilteringByColumn = false; RG_Categories.AllowSorting = false; }
            else
            { RG_Categories.PagerStyle.Visible = true; RG_Categories.MasterTableView.AllowFilteringByColumn = true; RG_Categories.AllowSorting = true; }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void BindCategoriesDropDown()
    {
        try
        {
            DDL_Categories.Items.Clear();

            RadComboBoxItem item = new Telerik.WebControls.RadComboBoxItem();
            item.Text = "  ";
            item.Value = "0";
            DDL_Categories.Items.Add(item);

            //========== Get Categories data from the session ===========//
            CategoriesData objCategory = new CategoriesData();
            DataTable DT_Categories = objCategory.uspGetAllCategories();
            //DataTable DT_Categories = (DataTable)Fuji.Common.Sessions.GetSession("CategoriesData");
            DataView dv = DT_Categories.DefaultView;
            DataTable DT_CategoriesCopy = DT_Categories.Copy();
            dv.RowFilter = "ParentID = 0";
            for (int i = 0; i < dv.Count; i++)
            {
                item = new Telerik.WebControls.RadComboBoxItem();
                DataView dvCopy = DT_CategoriesCopy.DefaultView;
                if (int.Parse(dv[i]["CountVideos"].ToString()) > 0)
                {
                    item.Enabled = false;
                    item.Font.Bold = true;
                    item.Font.Italic = true;
                }
                else
                {
                    item.Enabled = true;
                    item.Font.Bold = false;
                    item.Font.Italic = false;
                }
                //dvCopy.RowFilter = "ParentID = " + int.Parse(dv[i]["CategoryID"].ToString());
                //if (dvCopy.Count > 0)
                //{
                //    item.Enabled = false;
                //    item.Font.Bold = true;
                //    item.Font.Italic = true;
                //}
                //else
                //{
                //    item.Enabled = true;
                //    item.Font.Bold = false;
                //    item.Font.Italic = false;
                //}
                item.Text = dv[i]["Description"].ToString();
                item.Value = dv[i]["CategoryID"].ToString();
                DDL_Categories.Items.Add(item);
                dvCopy.RowFilter = "";
                AddChildDataToDropDown(DT_CategoriesCopy, int.Parse(dv[i]["CategoryID"].ToString()), 0);
            }
            dv.RowFilter = "";
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    //private string GetParentDesription(int ParentID, DataTable DT_Categories)
    //{
    //    DataTable DT_CategoriesCopy = DT_Categories.Copy();
    //    DataView dv = DT_Categories.DefaultView;
    //    dv.RowFilter = "CategoryID = " + ParentID;
    //    string strDescription = "";
    //    if (dv.Count > 0)
    //    {
    //        if (!dv[0]["ParentID"].ToString().Equals("0"))
    //        {
    //            //dv.RowFilter = "";
    //            string strDesc = dv[0]["Description"].ToString();
    //            int intParentID = (dv[0]["ParentID"]) == null ? 0 : int.Parse(dv[0]["ParentID"].ToString());

    //            strDescription = GetParentDesription(intParentID, DT_CategoriesCopy) + " > " + strDesc;
    //        }
    //        else
    //            strDescription = dv[0]["Description"].ToString();
    //    }
    //    return strDescription;
    //}

    //private int CheckIsParent(int CategoryID, DataTable DT_Categories)
    //{
    //    DataTable DT_CategoriesCopy = DT_Categories.Copy();
    //    DataView dv = DT_CategoriesCopy.DefaultView;
    //    dv.RowFilter = "ParentID = " + CategoryID;
    //    if (dv.Count > 0)
    //    {
    //        return 1;
    //    }
    //    else
    //        return 0;
    //}

    private void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Check grid column in the grid ===========//
            foreach (Telerik.WebControls.GridColumn column in RG_Categories.MasterTableView.Columns)
            {
                //========== set all column filter value to empty ===========//
                column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                column.CurrentFilterValue = String.Empty;

            }

            //========== set all filter expression to empty ===========//
            RG_Categories.MasterTableView.FilterExpression = String.Empty;
            RG_Categories.MasterTableView.Rebind();
        }
        catch (Exception exc)
        { LBL_Error.Text = HandleException(exc); }
    }

    
    #endregion

    #region "Videos"
    protected void BTN_AddVideo_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateVideosInputFields())
            {
                
                UploadedFile file = RadUploadContext.Current.UploadedFiles[FU_VideoFile.UniqueID];
                if (file != null)
                {
                    const int total = 100;
                    RadProgressContext progress = RadProgressContext.Current;

                    for (int i = 0; i < total; i++)
                    {
                        progress["SecondaryTotal"] = total.ToString();
                        progress["SecondaryValue"] = i.ToString();
                        progress["SecondaryPercent"] = i.ToString();
                        progress["CurrentOperationText"] = file.GetName() + " is being processed...";

                        if (!Response.IsClientConnected)
                        {
                            //Cancel button was clicked or the browser was closed, so stop processing
                            break;
                        }

                        //Stall the current thread for 0.1 seconds
                        System.Threading.Thread.Sleep(100);
                    }

                }
                

                //========== Declare the object of the Videos ===========//
                VideosData ObjVideo = new VideosData();
                int intResult;

                //========== Assign the properties to the object of the Video ===========//
                if (LTRL_VideoID.Text.Trim().Equals(""))
                    ObjVideo.VideoID = 0;
                else
                    ObjVideo.VideoID = int.Parse(LTRL_VideoID.Text.Trim());

                ObjVideo.CategoryID = Convert.ToInt32(DDL_VideoCategorySubCategory.SelectedValue);
                ObjVideo.LinkText = TXT_LinkText.Text.Trim();
                if (FU_VideoFile.UploadedFiles.Count > 0)
                {
                    ObjVideo.VideoFile = System.IO.Path.GetFileName(FU_VideoFile.UploadedFiles[0].FileName);
                }
                else
                {
                    if (!LTRL_VideoFileName.Text.Trim().Equals(""))
                        ObjVideo.VideoFile = LTRL_VideoFileName.Text.Trim();
                    else
                        ObjVideo.VideoFile = "";
                }

                ObjVideo.UploadTo = Convert.ToInt32(RDO_UploadTo.SelectedValue);
                ObjVideo.Description = TXT_VideoDescription.Text.Trim();
                ObjVideo.Keywords = TXT_KeyWords.Text.Trim();

                ObjVideo.UploadedBy = Sessions.UserID;
                ObjVideo.ModifiedBy = Sessions.UserID;

                //========== Check That Operation is insert or not by id ===========//
                if (ObjVideo.VideoID == 0)
                {
                    //========== Insertion new category by calling insert method ===========//
                    intResult = ObjVideo.uspInsertVideo();
                    if (intResult == 0)
                    {
                        //========== Show insertion message ===========//
                        LBL_Error.Text = Resources.Message.BranchNameAlreadyExists; return;
                    }
                    else
                    {
                        if (FU_VideoFile.UploadedFiles.Count>0)
                        {
                            if (!System.IO.Directory.Exists(Server.MapPath("~") + "/Videos"))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~") + "/Videos");
                            }
                            FU_VideoFile.UploadedFiles[0].SaveAs(Server.MapPath("~") + "/Videos/" + intResult.ToString() + System.IO.Path.GetExtension(FU_VideoFile.UploadedFiles[0].FileName));

                            CreateThumbImage(Server.MapPath("~") + "/Videos/" + intResult.ToString() + System.IO.Path.GetExtension(FU_VideoFile.UploadedFiles[0].FileName));
                        }

                        //========== Show insertion message ===========//
                        LBL_Error.Text = "New Video inserted successfully";
                    }
                }
                else
                {
                    //========== Update Category by calling update method ===========//
                    intResult = ObjVideo.uspUpdateVideo();
                    if (intResult == 0)
                    {
                        //========== Show insertion message ===========//
                        LBL_Error.Text = Resources.Message.BranchNameAlreadyExists; return;
                    }
                    else
                    {
                        if (FU_VideoFile.UploadedFiles.Count > 0)
                        {
                            if (!System.IO.Directory.Exists(Server.MapPath("~") + "/Videos"))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~") + "/Videos");
                            }
                            FU_VideoFile.UploadedFiles[0].SaveAs(Server.MapPath("~") + "/Videos/" + ObjVideo.VideoID.ToString() + System.IO.Path.GetExtension(FU_VideoFile.UploadedFiles[0].FileName));

                            CreateThumbImage(Server.MapPath("~") + "/Videos/" + ObjVideo.VideoID.ToString() + System.IO.Path.GetExtension(FU_VideoFile.UploadedFiles[0].FileName));
                        }

                        //========== Show updation message ===========//
                        LBL_Error.Text = "Video updated successfully";
                        
                       


                        //========== Set Branch ID to initial Zero null ===========//
                        LTRL_VideoID.Text = "0";
                    }
                }

                divAddVideo.Style.Add("display", "none");
                //========== Clear the Session On first view of the page ===========//
                DDL_VideoCategorySubCategory.SelectedValue = "0";
                TXT_LinkText.Text = "";
                TXT_VideoDescription.Text = "";
                TXT_KeyWords.Text = "";
                RDO_UploadTo.SelectedIndex = 0;
                LTRL_VideoFileName.Text = "";
                //========== Clear the Session On first view of the page ===========//
                Fuji.Common.Sessions.SetSessions("AllVideosData", null);

                ////========== Show Categories List ===========//
                BindCategoriesDropDown();
                BindVideos();
                BindVideoCategoriesDropDown();

                LTRL_VideoID.Text = "0";
            }
            else
            {
                divAddVideo.Style.Add("display", "block");
            }
        }
        catch (Exception exc)
        {
            divAddVideo.Style.Add("display", "block");
            LBL_Error.Text = HandleException(exc);
        }
    }

    protected void RG_Videos_PageIndexChanged(object source, Telerik.WebControls.GridPageChangedEventArgs e)
    {
        try
        {
            //========== Assign new page index to current page indes ===========//
            RG_Videos.CurrentPageIndex = e.NewPageIndex;

            //========== Bind Categories Data ===========//
            BindVideos();
        }
        catch (Exception exc)
        { LBL_Error.Text = HandleException(exc); }
    }

    protected void RG_Videos_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        try
        {
            //========== Check Categories Data in session if exist then bind it ===========//
            if (Fuji.Common.Sessions.GetSession("AllVideosData") != null)
            { RG_Videos.DataSource = Fuji.Common.Sessions.GetSession("AllVideosData"); }
        }
        catch (Exception exc)
        { LBL_Error.Text = HandleException(exc); }
    }

    protected void RG_Videos_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            //========== If Command Name is Edit then comes to this end of code ===========//
            if (e.CommandName == "EditVideo")
            {
                LTRL_VideoID.Text = e.Item.Cells[2].Text.Trim();
                VideosData objVideo = new VideosData();
                objVideo.VideoID = int.Parse(LTRL_VideoID.Text);
                DataTable dt = objVideo.uspGetVideosByVideoID();

                if (dt.Rows.Count > 0)
                {
                    if (DDL_VideoCategorySubCategory.FindItemByValue(dt.Rows[0]["CategoryID"].ToString()) != null)
                        DDL_VideoCategorySubCategory.SelectedValue = dt.Rows[0]["CategoryID"].ToString();
                    else
                        DDL_VideoCategorySubCategory.SelectedIndex = 0;

                    RDO_UploadTo.SelectedValue = dt.Rows[0]["UploadTo"].ToString();
                    TXT_LinkText.Text = dt.Rows[0]["LinkText"].ToString();
                    TXT_VideoDescription.Text = dt.Rows[0]["Description"].ToString();
                    TXT_KeyWords.Text = dt.Rows[0]["KeyWords"].ToString();
                    LTRL_VideoFileName.Text = dt.Rows[0]["VideoFile"].ToString();
                }

                divAddVideo.Style.Add("display", "block");
            }
            //========== If Command Name is Delete then comes to this end of code ===========//
            if (e.CommandName == "Delete")
            {
                //==========  Declaring Objects of Category==========//
                VideosData objVideo = new VideosData();

                //========== Assign value to Category object ===========//
                objVideo.VideoID = Convert.ToInt32(e.Item.Cells[2].Text.Trim());
                objVideo.uspDeleteVideo();

                if(System.IO.File.Exists(Server.MapPath("~") + "/Videos/" + objVideo.VideoID.ToString() + System.IO.Path.GetExtension(e.Item.Cells[5].Text.Trim())))
                {
                    System.IO.File.Delete(Server.MapPath("~") + "/Videos/" + objVideo.VideoID.ToString() + System.IO.Path.GetExtension(e.Item.Cells[5].Text.Trim()));
                }
                if(System.IO.File.Exists(Server.MapPath("~") + "/Videos/VideosThumb/" + objVideo.VideoID.ToString() + ".jpg"))
                {
                    System.IO.File.Delete(Server.MapPath("~") + "/Videos/VideosThumb/" + objVideo.VideoID.ToString() + ".jpg");
                }

                

                //========== Show successful deletion message ===========//
                LBL_Error.Text = "Video deleted successfully.";

                //========== Clear all filtering options , if any ===========//
                btnResetVideo_Click(null, null);

                //========== Clear the Session On first view of the page ===========//
                Fuji.Common.Sessions.SetSessions("AllVideosData", null);

                ////========== Show Categories List ===========//
                BindCategories();
                BindCategoriesDropDown();
                BindVideos();
                BindVideoCategoriesDropDown();

                LTRL_VideoID.Text = "0";
            }
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void RG_Videos_SortCommand(object source, Telerik.WebControls.GridSortCommandEventArgs e)
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

    protected void RG_Videos_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            //========== Check The Item is item or alternating item then comes to this end of code ===========//
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                ImageButton IMGBTN_Delete = (ImageButton)e.Item.FindControl("IMGBTN_Delete");
                IMGBTN_Delete.Attributes.Add("OnClick", "javascript : if(confirm('" + Resources.Message.DeleteConfirmMessage_Text + "') == true) return true; else return false;");

                //========== Find the ImageButton to assign VideoFile ===========//
                ImageButton IMGBTN_Video = (ImageButton)e.Item.FindControl("IMGBTN_Video");
                IMGBTN_Video.Attributes.Add("OnClick", "OpenVideoFile('PlayVideoFile.aspx?VideoID=" + ((DataRowView)e.Item.DataItem)["VideoID"].ToString() + "');");

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
                IntFirstIndexInPage = RG_Videos.MasterTableView.PagingManager.FirstIndexInPage;
                IntLastIndexInPage = RG_Videos.MasterTableView.PagingManager.LastIndexInPage + 1;
                IntDataSourceCount = RG_Videos.MasterTableView.PagingManager.DataSourceCount;

                if (IntDataSourceCount > 0)
                {
                    IntFirstIndexInPage++;
                }

                LTRL_RecordText.Text = "Record(s) : " + IntFirstIndexInPage + " to " + IntLastIndexInPage + " of " + IntDataSourceCount + " | ";

                if (RG_Videos.MasterTableView.PagingManager.IsFirstPage)
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
                if (RG_Videos.MasterTableView.PagingManager.IsLastPage == true)
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

    protected void RG_Videos_ItemCreated(object sender, GridItemEventArgs e)
    {
        try
        {
            //========== Check and create events for the pager selection  ===========//
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
            {
                //========== Find the Record page dropdownlist ===========//
                DropDownList DDL_RecordPage = (DropDownList)e.Item.FindControl("DDL_RecordPageVideo");
                if (DDL_RecordPage != null)
                {
                    //========== Declare its event ===========//
                    DDL_RecordPage.SelectedIndexChanged += new EventHandler(DDlRecordPageVideo_SelectedIndexChanged);

                    if (!IsPostBack)
                    {
                        //========== Assign record page selection value ===========//
                        DDL_RecordPage.SelectedValue = ConstDefaultPageSize.ToString();
                    }
                    else
                    {
                        if (ViewState["RecordPerPageVideo"] != null)
                        {
                            DDL_RecordPage.SelectedIndex = -1;
                            DDL_RecordPage.Items.FindByValue(ViewState["RecordPerPageVideo"].ToString()).Selected = true;
                        }
                    }
                }

                //========== Find the Reset button in the pager style ===========//
                Button BTN_Reset = (Button)e.Item.FindControl("BTN_Reset");
                BTN_Reset.Text = Resources.MasterCompany.BTN_Reset_Text;

                if (BTN_Reset != null)
                {
                    //========== Declate the reset button event ===========//
                    BTN_Reset.Click += new EventHandler(btnResetVideo_Click);
                }
            }
        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }

    private void DDlRecordPageVideo_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            //========== Declare the variable for the selection value of the record page dropdownlist ===========//
            string StrSelectedValue;
            StrSelectedValue = ((DropDownList)sender).SelectedItem.Value;
            ViewState["RecordPerPageVideo"] = StrSelectedValue;

            //========== Set the paze size ===========//
            RG_Videos.PageSize = Convert.ToInt32(StrSelectedValue);
            RG_Videos.CurrentPageIndex = 0;
            //========== Check for branch data in session and assign if exist ===========//
            if (Fuji.Common.Sessions.GetSession("AllVideosData") != null)
            {
                //========== Assign the branch data fro the session value ===========//
                RG_Videos.DataSource = Fuji.Common.Sessions.GetSession("AllVideosData");
            }

            //========== Bind the grid ===========//
            RG_Videos.DataBind();
        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }

    private void btnResetVideo_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Check grid column in the grid ===========//
            foreach (Telerik.WebControls.GridColumn column in RG_Videos.MasterTableView.Columns)
            {
                //========== set all column filter value to empty ===========//
                column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                column.CurrentFilterValue = String.Empty;

            }

            //========== set all filter expression to empty ===========//
            RG_Videos.MasterTableView.FilterExpression = String.Empty;
            RG_Videos.MasterTableView.Rebind();
        }
        catch (Exception exc)
        { LBL_Error.Text = HandleException(exc); }
    }

    private void BindVideos()
    {
        try
        {
            //==========  Declaring Objects ==========//
            VideosData objVideo = new VideosData();

            //==========  Get Data Table and Bind ==========//
            DataTable DT_Videos = new DataTable();
            if (Fuji.Common.Sessions.GetSession("AllVideosData") == null)
            {
                DT_Videos = objVideo.uspGetAllVideos();
                DT_Videos.Columns.Add("CatDescription");

                CategoriesData objCategory = new CategoriesData();
                DataTable DT_Categories = objCategory.uspGetAllCategories();
                //DataTable DT_Categories = (DataTable)Fuji.Common.Sessions.GetSession("CategoriesData");

                for (int i = 0; i < DT_Videos.Rows.Count; i++)
                {
                    string strDescription = GetVideoCategoryDesription(int.Parse(DT_Videos.Rows[i]["ParentID"].ToString()), DT_Categories);
                    if (!strDescription.Equals(""))
                        strDescription = strDescription + " > " + DT_Videos.Rows[i]["CategoryDescription"].ToString();
                    else
                        strDescription = DT_Videos.Rows[i]["CategoryDescription"].ToString();

                    DT_Videos.Rows[i]["CatDescription"] = strDescription;
                }


                Fuji.Common.Sessions.SetSessions("AllVideosData", DT_Videos);
            }

            //========== Get Categories data from the session ===========//
            DT_Videos = (DataTable)Fuji.Common.Sessions.GetSession("AllVideosData");

            //========== Assign the session data to Grid ===========//
            if (DT_Videos != null && DT_Videos.Rows.Count > 0)
            { RG_Videos.DataSource = DT_Videos; RG_Videos.DataBind(); }
            else
            { RG_Videos.DataSource = null; RG_Videos.DataBind(); }

            //========== Show the pager style if page count is greater than 1 ===========//
            if (RG_Videos.Items.Count == 0)
            { RG_Videos.PagerStyle.Visible = false; RG_Videos.MasterTableView.AllowFilteringByColumn = false; RG_Videos.AllowSorting = false; }
            else
            { RG_Videos.PagerStyle.Visible = true; RG_Videos.MasterTableView.AllowFilteringByColumn = true; RG_Videos.AllowSorting = true; }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private string GetVideoCategoryDesription(int ParentID, DataTable DT_Videos)
    {
        try
        {
            DataTable DT_VideosCopy = DT_Videos.Copy();
            DataView dv = DT_Videos.DefaultView;
            dv.RowFilter = "CategoryID = " + ParentID;
            string strDescription = "";
            if (dv.Count > 0)
            {
                if (!dv[0]["ParentID"].ToString().Equals("0"))
                {
                    //dv.RowFilter = "";
                    string strDesc = dv[0]["Description"].ToString();
                    strDescription = GetVideoCategoryDesription(int.Parse(dv[0]["ParentID"].ToString()), DT_VideosCopy) + " > " + strDesc;
                }
                else
                    strDescription = dv[0]["Description"].ToString();
            }
            return strDescription;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void BindVideoCategoriesDropDown()
    {
        try
        {
            DDL_VideoCategorySubCategory.Items.Clear();
            RadComboBoxItem item = new Telerik.WebControls.RadComboBoxItem();
            item.Text = "Select One";
            item.Value = "0";
            DDL_VideoCategorySubCategory.Items.Add(item);

            //========== Get Categories data from the session ===========//
            CategoriesData objCategory = new CategoriesData();
            DataTable DT_Categories = objCategory.uspGetAllCategories();
            //DataTable DT_Categories = (DataTable)Fuji.Common.Sessions.GetSession("CategoriesData");
            DataView dv = DT_Categories.DefaultView;
            DataTable DT_CategoriesCopy = DT_Categories.Copy();
            dv.RowFilter = "ParentID = 0";
            for (int i = 0; i < dv.Count; i++)
            {
                item = new Telerik.WebControls.RadComboBoxItem();
                DataView dvCopy = DT_CategoriesCopy.DefaultView;
                //if (int.Parse(dv[i]["CountVideos"].ToString()) > 0)
                //{
                //    item.Enabled = false;
                //    item.Font.Bold = true;
                //    item.Font.Italic = true;
                //}
                //else
                //{
                //    item.Enabled = true;
                //    item.Font.Bold = false;
                //    item.Font.Italic = false;
                //}
                dvCopy.RowFilter = "ParentID = " + int.Parse(dv[i]["CategoryID"].ToString());
                if (dvCopy.Count > 0)
                {
                    item.Enabled = false;
                    item.Font.Bold = true;
                    item.Font.Italic = true;
                }
                else
                {
                    item.Enabled = true;
                    item.Font.Bold = false;
                    item.Font.Italic = false;
                }
                item.Text = dv[i]["Description"].ToString();
                item.Value = dv[i]["CategoryID"].ToString();
                DDL_VideoCategorySubCategory.Items.Add(item);
                dvCopy.RowFilter = "";
                AddChildDataToVideoDropDown(DT_CategoriesCopy, int.Parse(dv[i]["CategoryID"].ToString()), 0);
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void AddChildDataToVideoDropDown(DataTable DT_Categories, int CategoryID, int intSpaces)
    {
        try
        {
            string strDescription = "";
            string strCategoryID = "";
            DataTable DT_CategoriesCopy = DT_Categories.Copy();
            DataView dv = DT_Categories.DefaultView;
            dv.RowFilter = "ParentID = " + CategoryID;
            for (int i = 0; i < dv.Count; i++)
            {
                strDescription = dv[i]["Description"].ToString();
                strCategoryID = dv[i]["CategoryID"].ToString();
                RadComboBoxItem item = new Telerik.WebControls.RadComboBoxItem();
                DataView dvCopy = DT_CategoriesCopy.DefaultView;
                //if (int.Parse(dv[i]["CountVideos"].ToString()) > 0)
                //{
                //    item.Enabled = false;
                //    item.Font.Bold = true;
                //    item.Font.Italic = true;
                //}
                //else
                //{
                //    item.Enabled = true;
                //    item.Font.Bold = false;
                //    item.Font.Italic = false;
                //}
                dvCopy.RowFilter = "ParentID = " + int.Parse(dv[i]["CategoryID"].ToString());
                if (dvCopy.Count > 0)
                {
                    item.Enabled = false;
                    item.Font.Bold = true;
                    item.Font.Italic = true;
                }
                else
                {
                    item.Enabled = true;
                    item.Font.Bold = false;
                    item.Font.Italic = false;
                }
                //intSpaces = intSpaces + 10;
                item.Style.Add("padding-left", (intSpaces + 20).ToString() + "px");
                item.Text = dv[i]["Description"].ToString();
                item.Value = dv[i]["CategoryID"].ToString();
                DDL_VideoCategorySubCategory.Items.Add(item);
                dvCopy.RowFilter = "";
                AddChildDataToVideoDropDown(DT_CategoriesCopy, int.Parse(dv[i]["CategoryID"].ToString()), intSpaces + 20);
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion


    private string CreateThumbImage(string FLVFilePath)
    {
        try
        {
            string ThumbImageFilePath = Server.MapPath("~") + "/Videos/VideosThumb/" + System.IO.Path.GetFileNameWithoutExtension(FLVFilePath) + ".jpg";
            //string strFLVFilePath = Server.MapPath("~") + "/Videos/" + FLVFilePath;
            //string ThumbImageFilePath = FLVFilePath.ToLower().Replace(".flv", ".jpg").Replace("\\movies\\", "\\movieimages\\");
            string ExeFilePath = Server.MapPath("~") + "\\ffmpeg\\ffmpeg.exe";
            string imgargs = " -i \"" + FLVFilePath + "\" -f image2 -ss 1 -vframes 1 -s 150x150 -an \"" + ThumbImageFilePath + "\"";
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = ExeFilePath;
            proc.StartInfo.Arguments = imgargs;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.RedirectStandardOutput = false;
            proc.Start();
            //////////////
            ///////////////

            while (!proc.HasExited)
            {
                DateTime StartTime = DateTime.Now;
                if (StartTime.AddSeconds(10) < DateTime.Now && !proc.HasExited)
                {
                    return "";
                }
            }

            return ThumbImageFilePath.Substring(ThumbImageFilePath.LastIndexOf("\\") + 1);
        }
        catch (Exception Exc)
        {
            throw (Exc);
        }
    } 
}
