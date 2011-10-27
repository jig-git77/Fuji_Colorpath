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
using Fuji.Common;
using Fuji.Business;
using Telerik.WebControls;
using System.Text.RegularExpressions;
#endregion

public partial class BrandingName : PageBase
{
    #region Declarations
    string strCSSFile, strHeaderFile, strLargeFile;
    private const int _ConstDefaultPageSize = 25;
    #endregion

    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //=========== Clear Error ==========//
            LblError.Text = "";

            //=========== Check Login ==========//
            Sessions.CheckUserLogin();

            //=========== Bind Display input box with the value ==========//
            AssignJavascriptToShowColors(TXT_TopLine, TXT_Color_TopLine);
            AssignJavascriptToShowColors(TXT_MainTabMenuText, TXT_Color_MainTabMenuText);
            AssignJavascriptToShowColors(TXT_MainTabMenuTextOver, TXT_Color_MainTabMenuTextOver);
            AssignJavascriptToShowColors(TXT_MainTabMenuBack, TXT_Color_MainTabMenuBack);
            AssignJavascriptToShowColors(TXT_MainTabMenuBackOver, TXT_Color_MainTabMenuBackOver);
            AssignJavascriptToShowColors(TXT_MainTabMenuBorder, TXT_Color_MainTabMenuBorder);
            AssignJavascriptToShowColors(TXT_MainTabMenuBorderOver, TXT_Color_MainTabMenuBorderOver);
            AssignJavascriptToShowColors(TXT_MenuBottomLine, TXT_Color_MenuBottomLine);
            AssignJavascriptToShowColors(TXT_SubTabMenuText, TXT_Color_SubTabMenuText);
            AssignJavascriptToShowColors(TXT_SubTabMenuTextOver, TXT_Color_SubTabMenuTextOver);
            AssignJavascriptToShowColors(TXT_MainForm, TXT_Color_MainForm);
            AssignJavascriptToShowColors(TXT_Footer, TXT_Color_Footer);
            AssignJavascriptToShowColors(TXT_FooterText, TXT_Color_FooterText);

            AssignJavascriptToShowColors(TXT_PopUpTabMainColor, TXT_Color_PopUpTabMainColor);
            AssignJavascriptToShowColors(TXT_PopUpTabText, TXT_Color_PopUpTabText);
            AssignJavascriptToShowColors(TXT_PopUpTabSelectedText, TXT_Color_PopUpTabSelectedText);

            //========== Check User Role and redirect to home page as per user role ===========//
            Fuji.Common.CommonFunctions.CheckUserRole();

            //=========== Default Settings ==========//
            if (IsPostBack == false)
            {
                //========== Fill COmpany ==========//
                FillDDLCompany();

                //========== Set Default COlors ==========//
                SetDefaultColors();

                //========== Fill Variables to Form ==========//
                SetLabels();

                //=========== Calling the Method for binding the Brand Grid ==========//
                FillBrandGrid();

            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void BTN_AddBrand_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControls();
            HideOption();
            HFBrandID.Value = "";
            TRMainTable.Visible = true;
            TRGridTable.Visible = false;
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void BTN_Cancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControls();
            TRMainTable.Visible = false;
            TRGridTable.Visible = true;
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void BTN_AddSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (DDL_CompanyID.SelectedIndex == -1){ LblError.Text = "Company is required."; return; }

            if (HFBrandID.Value == "")
            {
                AddBrandData();
            }
            else
            {
                UpdateBrandData();
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void GridBrandData_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        //========== Applying the Datasource to RadGrid at the time of postback or calling any event ===========//
        try
        {
            if (Fuji.Common.Sessions.GetSession("Company_Brand_Data_DataTable") != null)
            {
                DataTable DSet = (DataTable)Fuji.Common.Sessions.GetSession("Company_Brand_Data_DataTable");
                if (DSet != null && DSet.Rows.Count > 0)
                {
                    GridBrandData.DataSource = DSet;
                }
                else
                {
                    FillBrandGrid();
                }
            }
            else
            {
                FillBrandGrid();
            }

        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void GridBrandData_SortCommand(object source, Telerik.WebControls.GridSortCommandEventArgs e)
    {
        //========== Sorting Grid Event ===========//
        try
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void GridBrandData_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            //========== Check CommandName for the Edit Event ===========//
            if (e.CommandName == "Edit")
            {
                //========== Filling the Controls when Edit Event is called ===========//
                HideOption();
                HFBrandID.Value = e.Item.Cells[2].Text;
                TXT_BrandingName.Text = e.Item.Cells[3].Text;
                LTRL_Css.Text = e.Item.Cells[4].Text;
                HFHeaderLogo.Value = e.Item.Cells[5].Text.Replace("&nbsp;", "");
                HFLargeImage.Value = e.Item.Cells[6].Text.Replace("&nbsp;", "");

                if (e.Item.Cells[5].Text.Replace("&nbsp;", "").Length > 6)
                {
                    LnkHeaderLogo.Visible = true;
                    chkHeaderLogo.Visible = true;
                    LnkHeaderLogo.NavigateUrl = "Branding/HeaderImage/" + e.Item.Cells[5].Text;
                    LTRL_ImagePath_Logo.Text = "<a href=\"" + LnkHeaderLogo.NavigateUrl + "\" class=\"LINKBUTTON2\" target=\"_blank\">" + Fuji.Common.WebSettings.Domain + LnkHeaderLogo.NavigateUrl + "</a>";
                }

                if (e.Item.Cells[6].Text.Replace("&nbsp;", "").Length > 6)
                {
                    LnkLargeImage.Visible = true;
                    chkLargeImage.Visible = true;
                    LnkLargeImage.NavigateUrl = "Branding/LargeImage/" + e.Item.Cells[6].Text;
                    LTRL_ImagePath_LargeImage.Text = "<a href=\"" + LnkLargeImage.NavigateUrl + "\" class=\"LINKBUTTON2\" target=\"_blank\">" + Fuji.Common.WebSettings.Domain + LnkLargeImage.NavigateUrl + "</a>";
                }

                //========== Added By Maulik for CSS Branding ==========//
                AssignColorsToTextBox(e.Item.Cells[7].Text.Replace("&nbsp;", ""), TXT_TopLine, TXT_Color_TopLine);
                AssignColorsToTextBox(e.Item.Cells[8].Text.Replace("&nbsp;", ""), TXT_MainTabMenuText, TXT_Color_MainTabMenuText);
                AssignColorsToTextBox(e.Item.Cells[9].Text.Replace("&nbsp;", ""), TXT_MainTabMenuTextOver, TXT_Color_MainTabMenuTextOver);
                AssignColorsToTextBox(e.Item.Cells[10].Text.Replace("&nbsp;", ""), TXT_MainTabMenuBack, TXT_Color_MainTabMenuBack);
                AssignColorsToTextBox(e.Item.Cells[11].Text.Replace("&nbsp;", ""), TXT_MainTabMenuBackOver, TXT_Color_MainTabMenuBackOver);
                AssignColorsToTextBox(e.Item.Cells[12].Text.Replace("&nbsp;", ""), TXT_MainTabMenuBorder, TXT_Color_MainTabMenuBorder);
                AssignColorsToTextBox(e.Item.Cells[13].Text.Replace("&nbsp;", ""), TXT_MainTabMenuBorderOver, TXT_Color_MainTabMenuBorderOver);
                AssignColorsToTextBox(e.Item.Cells[14].Text.Replace("&nbsp;", ""), TXT_MenuBottomLine, TXT_Color_MenuBottomLine);
                AssignColorsToTextBox(e.Item.Cells[15].Text.Replace("&nbsp;", ""), TXT_SubTabMenuText, TXT_Color_SubTabMenuText);
                AssignColorsToTextBox(e.Item.Cells[16].Text.Replace("&nbsp;", ""), TXT_SubTabMenuTextOver, TXT_Color_SubTabMenuTextOver);
                AssignColorsToTextBox(e.Item.Cells[17].Text.Replace("&nbsp;", ""), TXT_MainForm, TXT_Color_MainForm);
                AssignColorsToTextBox(e.Item.Cells[18].Text.Replace("&nbsp;", ""), TXT_Footer, TXT_Color_Footer);
                AssignColorsToTextBox(e.Item.Cells[19].Text.Replace("&nbsp;", ""), TXT_FooterText, TXT_Color_FooterText);

                try { DDL_CompanyID.SelectedValue = e.Item.Cells[20].Text.Replace("&nbsp;", ""); }
                catch { DDL_CompanyID.SelectedIndex = 0; }

                AssignColorsToTextBox(e.Item.Cells[22].Text.Replace("&nbsp;", ""), TXT_PopUpTabMainColor, TXT_Color_PopUpTabMainColor);
                AssignColorsToTextBox(e.Item.Cells[23].Text.Replace("&nbsp;", ""), TXT_PopUpTabText, TXT_Color_PopUpTabText);
                AssignColorsToTextBox(e.Item.Cells[24].Text.Replace("&nbsp;", ""), TXT_PopUpTabSelectedText, TXT_Color_PopUpTabSelectedText);

                AssignBackgroundColor();

                CHK_ConfirmCssChange.Checked = true;
                //========== Added By Maulik for CSS Branding ==========//

                TRGridTable.Visible = false;
                TRMainTable.Visible = true;
                TRMessage1.Visible = true;
                BTN_AddSave.Text = Resources.BrandingName.BTN_AddSave_Save;
            }
            //========== Check CommandName for the Delete Event ===========//
            if (e.CommandName == "Delete")
            {
                //========== Object Declaration for the Deletion ===========//
                CompanyBrandingData ObjBrandingData = new CompanyBrandingData();
                ObjBrandingData.BrandID = Convert.ToInt32(e.Item.Cells[2].Text);
                //========== Calling the Method for Delete the Recored ===========//
                int Result = ObjBrandingData.uspDeleteCompanyBrand();
                if (Result == 1)
                {
                    LblError.Text = "Company Brand data deleted successfully.";
                    FillBrandGrid();
                }
            }

        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void GridBrandData_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                //========== Declaring the ImageButton and Casting it from Grid ===========//
                ImageButton IMGBTN_Delete = (ImageButton)e.Item.FindControl("IMGBTN_Delete");

                //========== Applying the Javascript to the ImageButton ===========//                
                IMGBTN_Delete.Attributes.Add("OnClick", "javascript : if(confirm('" + Resources.Message.DeleteConfirmMessage_Text + "') == true) return true; else return false;");

            }
            //========== Check for the pager style function ===========//
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
            {
                #region Pager Style

                Label LBL_RecordText = (Label)e.Item.FindControl("LBL_RecordText");
                int iFirstIndexInPage = 0, iLastIndexInPage = 0, iDataSourceCount = 0;

                iFirstIndexInPage = GridBrandData.MasterTableView.PagingManager.FirstIndexInPage;
                iLastIndexInPage = GridBrandData.MasterTableView.PagingManager.LastIndexInPage + 1;
                iDataSourceCount = GridBrandData.MasterTableView.PagingManager.DataSourceCount;

                if (iDataSourceCount > 0)
                {
                    iFirstIndexInPage++;
                }

                LBL_RecordText.Text = "Record(s) : " + iFirstIndexInPage + " to " + iLastIndexInPage + " of " + iDataSourceCount + " | ";

                ImageButton lbFirst = (ImageButton)e.Item.FindControl("lbFirst");
                ImageButton lbPrev = (ImageButton)e.Item.FindControl("lbPrev");
                ImageButton lbNext = (ImageButton)e.Item.FindControl("lbNext");
                ImageButton lbLast = (ImageButton)e.Item.FindControl("lbLast");


                if (GridBrandData.MasterTableView.PagingManager.IsFirstPage)
                {
                    lbFirst.ImageUrl = "Images/Nav_FirstPage_off.gif";
                    lbPrev.ImageUrl = "Images/Nav_PreviousPage_off.gif";
                    lbFirst.Style.Add("cursor", "");
                    lbPrev.Style.Add("cursor", "");
                    lbFirst.Attributes.Add("disabled", "true");
                    lbPrev.Attributes.Add("disabled", "true");
                }
                else
                {
                    lbFirst.ImageUrl = "Images/Nav_FirstPage.gif";
                    lbPrev.ImageUrl = "Images/Nav_PreviousPage.gif";
                    lbFirst.Style.Add("cursor", "pointer");
                    lbPrev.Style.Add("cursor", "pointer");
                }
                if (GridBrandData.MasterTableView.PagingManager.IsLastPage == true)
                {
                    lbNext.ImageUrl = "Images/Nav_NextPage_off.gif";
                    lbLast.ImageUrl = "Images/Nav_LastPage_off.gif";
                    lbNext.Style.Add("cursor", "");
                    lbLast.Style.Add("cursor", "");
                    lbNext.Attributes.Add("disabled", "true");
                    lbLast.Attributes.Add("disabled", "true");
                }
                else
                {
                    lbNext.ImageUrl = "Images/Nav_NextPage.gif";
                    lbLast.ImageUrl = "Images/Nav_LastPage.gif";
                    lbNext.Style.Add("cursor", "pointer");
                    lbLast.Style.Add("cursor", "pointer");
                }
                #endregion
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void GridBrandData_ItemCreated(object sender, GridItemEventArgs e)
    {
        try
        {
            //========== Check and create events for the pager selection  ===========//
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
            {
                DropDownList DDlRecordPage = (DropDownList)e.Item.FindControl("DDlRecordPage");
                if (DDlRecordPage != null)
                {
                    DDlRecordPage.SelectedIndexChanged += new EventHandler(DDlRecordPage_SelectedIndexChanged);

                    if (!IsPostBack)
                    {
                        DDlRecordPage.SelectedValue = _ConstDefaultPageSize.ToString();
                    }
                    else
                    {
                        if (ViewState["RecordPerPage"] != null)
                        {
                            DDlRecordPage.SelectedIndex = -1;
                            DDlRecordPage.Items.FindByValue(ViewState["RecordPerPage"].ToString()).Selected = true;
                        }
                    }
                }

                //For Reset Filter Option
                Button btnReset = (Button)e.Item.FindControl("btnReset");
                btnReset.Text = Resources.UserManagement.btnReset_Text;
                if (btnReset != null)
                {
                    btnReset.Click += new EventHandler(btnReset_Click);
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
            string sSelectedValue;
            sSelectedValue = ((DropDownList)sender).SelectedItem.Value;
            ViewState["RecordPerPage"] = sSelectedValue;

            GridBrandData.PageSize = Convert.ToInt32(sSelectedValue);

            if (Fuji.Common.Sessions.GetSession("Company_Brand_Data_DataTable") != null)
            {
                GridBrandData.DataSource = Fuji.Common.Sessions.GetSession("Company_Brand_Data_DataTable");
            }

            GridBrandData.DataBind();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    private void btnReset_Click(object sender, EventArgs e)
    {
        //========== Event for Clearing all the Filter in the Grid ===========//
        try
        {
            //========== Check grid column in the grid ===========//
            foreach (Telerik.WebControls.GridColumn column in GridBrandData.MasterTableView.Columns)
            {
                //========== set all column filter value to empty ===========//
                column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                column.CurrentFilterValue = String.Empty;
            }
            Fuji.Common.Sessions.SetSessions("StartDate", null);
            //========== set all filter expression to empty ===========//
            GridBrandData.MasterTableView.FilterExpression = String.Empty;
            GridBrandData.MasterTableView.Rebind();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    #endregion

    #region Methods and Events
    //========== Assign Javascript ==========//
    private void AssignJavascriptToShowColors(TextBox TXT, TextBox TXT_Colors)
    { TXT.Attributes.Add("OnBlur", "FillColor(this,'" + TXT_Colors.ClientID + "')"); }
    //========== Set Colors ==========//
    private void AssignColorsToTextBox(string _Val, TextBox TXT, TextBox TXT_Colors) { if (_Val != "") { TXT.Text = _Val; TXT_Colors.Style.Add("background-color", _Val); } }
    //========== Set Labels ==========//
    private void SetLabels()
    {
        try
        {
            //========== Set Culture ==========//
            Resources.Message.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));
            Resources.UserManagement.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));

            ////========== Set Values ==========//
            LBL_Title.Text = Resources.BrandingName.LBL_Title_Text;
            LBL_SubTitle.Text = Resources.BrandingName.LBL_SubTitle_Text;
            LBL_BrandingName.Text = Resources.BrandingName.LBL_BrandingName_Text;
            LBL_HeaderLogo.Text = Resources.BrandingName.LBL_HeaderLogo_Text;
            LBL_LargeImage.Text = Resources.BrandingName.LBL_LargeImage_Text;

            BTN_AddBrand.Text = Resources.BrandingName.BTN_AddBrand_Text;
            BTN_AddSave.Text = Resources.BrandingName.BTN_AddSave_Add;
            BTN_Cancel.Text = Resources.BrandingName.BTN_Cancel_Text;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //========== Set Default Colors ==========//
    private void SetDefaultColors()
    {
        try
        {
            //========== Set Default Colors ==========//
            CHK_ConfirmCssChange.Checked = true;

            LTRL_TopLine.Text = TXT_TopLine.Text = "#38a886";
            LTRL_MainTabMenuText.Text = TXT_MainTabMenuText.Text = "#072d21";
            LTRL_MainTabMenuTextOver.Text = TXT_MainTabMenuTextOver.Text = "#FFFFFF";
            LTRL_MainTabMenuBack.Text = TXT_MainTabMenuBack.Text = "#40aa8a";
            LTRL_MainTabMenuBackOver.Text = TXT_MainTabMenuBackOver.Text = "#0c7f5c";
            LTRL_MainTabMenuBorder.Text = TXT_MainTabMenuBorder.Text = "#157453";
            LTRL_MainTabMenuBorderOver.Text = TXT_MainTabMenuBorderOver.Text = "#157453";
            LTRL_MenuBottomLine.Text = TXT_MenuBottomLine.Text = "#30a17e";
            LTRL_SubTabMenuText.Text = TXT_SubTabMenuText.Text = "#505050";
            LTRL_SubTabMenuTextOver.Text = TXT_SubTabMenuTextOver.Text = "#f4021e";
            LTRL_MainForm.Text = TXT_MainForm.Text = "#b4ded8";
            LTRL_Footer.Text = TXT_Footer.Text = "#119c7a";
            LTRL_FooterText.Text = TXT_FooterText.Text = "#FFFFFF";

            LTRL_PopUpTabMainColor.Text = TXT_PopUpTabMainColor.Text = "#30a17e";
            LTRL_PopUpTabText.Text = TXT_PopUpTabText.Text = "#FFFFFF";
            LTRL_PopUpTabSelectedText.Text = TXT_PopUpTabSelectedText.Text = "#000000";

            //========== Set Default Colors ==========//
            AssignBackgroundColor();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //=========== Method for Clearing the Controls ==========//
    private void ClearControls()
    {
        try
        {
            SetDefaultColors();
            TXT_BrandingName.Text = "";
            chkHeaderLogo.Checked = false;
            chkLargeImage.Checked = false;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //=========== Method Used for Uploading the Branding Images ==========//
    public Boolean UploadFile(FileUpload FILEUPLDER, string strFolderPath, string Filename)
    {
        try
        {
            if (FILEUPLDER.HasFile)
            {
                string TimeStamp = System.DateTime.Now.ToString();
                string FileExt = FILEUPLDER.PostedFile.FileName;
                FileExt = FileExt.Substring(FileExt.IndexOf("."));
                TimeStamp = TimeStamp.Replace("/", ""); TimeStamp = TimeStamp.Replace(":", ""); TimeStamp = TimeStamp.Replace(" ", "");
                TimeStamp = TimeStamp + FileExt;
                strFolderPath = strFolderPath + TimeStamp;
                string filepath = FILEUPLDER.PostedFile.FileName;
                string pat = @"\\(?:.+)\\(.+)\.(.+)";
                Regex r = new Regex(pat);
                Match m = r.Match(filepath);
                if (Filename == "CSSFile")
                    strCSSFile = TimeStamp;
                else if (Filename == "HeaderFile")
                    strHeaderFile = TimeStamp;
                else if (Filename == "LargeFile")
                    strLargeFile = TimeStamp;
                FILEUPLDER.PostedFile.SaveAs(Server.MapPath(".\\") + strFolderPath);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //=========== Method Used for Add Branding Informations ==========//
    protected void AddBrandData()
    {
        try
        {
            //========= Upload New Files ==========//
            if (FUPLD_HeaderLogo.HasFile == true)
                UploadFile(FUPLD_HeaderLogo, "Branding\\HeaderImage\\", "HeaderFile");
            else
                strHeaderFile = "";

            if (FUPLD_LargeImage.HasFile == true)
                UploadFile(FUPLD_LargeImage, "Branding\\LargeImage\\", "LargeFile");
            else
                strLargeFile = "";
            //========= Upload New Files ==========//

            //========= Create New Css Template ==========//        
            CreateCssFileAndAssignColors(ref strCSSFile);
            //========= Create New Css Template ==========//

            InsertBrandDetail();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //=========== Method Used for Insertng the Branding Data ==========//
    protected void InsertBrandDetail()
    {
        try
        {
            //========== Declarations ==========//
            CompanyBrandingData objBrandData = new CompanyBrandingData();

            objBrandData.BrandName = TXT_BrandingName.Text.Trim();
            objBrandData.Css = strCSSFile;
            objBrandData.LargeImage = strLargeFile;
            objBrandData.HeaderLogo = strHeaderFile;

            objBrandData.TopLine = TXT_TopLine.Text;
            objBrandData.MainTabMenuText = TXT_MainTabMenuText.Text;
            objBrandData.MainTabMenuTextOver = TXT_MainTabMenuTextOver.Text;
            objBrandData.MainTabMenuBack = TXT_MainTabMenuBack.Text;
            objBrandData.MainTabMenuBackOver = TXT_MainTabMenuBackOver.Text;
            objBrandData.MainTabMenuBorder = TXT_MainTabMenuBorder.Text;
            objBrandData.MainTabMenuBorderOver = TXT_MainTabMenuBorderOver.Text;
            objBrandData.MenuBottomLine = TXT_MenuBottomLine.Text;
            objBrandData.SubTabMenuText = TXT_SubTabMenuText.Text;
            objBrandData.SubTabMenuTextOver = TXT_SubTabMenuTextOver.Text;
            objBrandData.MainForm = TXT_MainForm.Text;
            objBrandData.Footer = TXT_Footer.Text;
            objBrandData.FooterText = TXT_FooterText.Text;
            objBrandData.CompanyID = Convert.ToInt32(DDL_CompanyID.SelectedValue);

            objBrandData.PopUpTabMainColor = TXT_PopUpTabMainColor.Text;
            objBrandData.PopUpTabText = TXT_PopUpTabText.Text;
            objBrandData.PopUpTabSelectedText = TXT_PopUpTabSelectedText.Text;

            //========== Inserting for Taskero User and its Details Table ==========//
            int _Result = objBrandData.uspInsertCompanyBrandingData();
            //==========  Checking for the Flag ==========//
            if (_Result == 1)
            {
                LblError.Text = "Brand Data Updated successfully.";
                BTN_AddBrand.Visible = true;
                TRGridTable.Visible = true;
                TRMainTable.Visible = false;
                ClearControls();
                FillBrandGrid();
            }
            else
            {
                LblError.Text = "Brand Data not Updated successfully.";
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //=========== Method Used for Get All the Branding Data ==========//
    protected void FillBrandGrid()
    {
        try
        {
            //==========  Declaring Objects ==========//
            CompanyBrandingData ObjCompanyBrandingData = new CompanyBrandingData();

            //==========  Fetching the Data From Taskero User ==========//
            DataTable _DSet = ObjCompanyBrandingData.uspGetCompanyBrandingData(Sessions.UserID);

            //=========== Setting the Session with Datatable of User Data==========//
            Fuji.Common.Sessions.SetSessions("Company_Brand_Data_DataTable", _DSet);

            if (_DSet != null && _DSet.Rows.Count > 0)
            {
                GridBrandData.Visible = true;
                GridBrandData.DataSource = _DSet;
                GridBrandData.DataBind();
            }
            else
            {
                GridBrandData.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //=========== Method Used for Update the Branding Data ==========//
    protected void UpdateBrandData()
    {
        try
        {
            //=========== Check Header Image File ==========//
            if (chkHeaderLogo.Checked == true)
            {
                DeleteFile("\\Branding\\HeaderImage\\" + HFHeaderLogo.Value);
                strHeaderFile = "";
            }
            else if (FUPLD_HeaderLogo.HasFile == true)
                UploadFile(FUPLD_HeaderLogo, "Branding\\HeaderImage\\", "HeaderFile");
            else
                strHeaderFile = HFHeaderLogo.Value;
            //=========== Check Large Image File ==========//
            if (chkLargeImage.Checked == true)
            {
                DeleteFile("\\Branding\\LargeImage\\" + HFLargeImage.Value);
                strLargeFile = "";
            }
            else if (FUPLD_LargeImage.HasFile == true)
                UploadFile(FUPLD_LargeImage, "Branding\\LargeImage\\", "LargeFile");
            else
                strLargeFile = HFLargeImage.Value;

            //========= Create New Css Template ==========//
            CreateCssFileAndAssignColors(ref strCSSFile);
            //========= Create New Css Template ==========//


            //========== Declarations ==========//
            CompanyBrandingData objBrandData = new CompanyBrandingData();

            objBrandData.BrandID = Convert.ToInt32(HFBrandID.Value);
            objBrandData.BrandName = TXT_BrandingName.Text.Trim();
            objBrandData.Css = strCSSFile;
            objBrandData.LargeImage = strLargeFile;
            objBrandData.HeaderLogo = strHeaderFile;

            objBrandData.TopLine = TXT_TopLine.Text;
            objBrandData.MainTabMenuText = TXT_MainTabMenuText.Text;
            objBrandData.MainTabMenuTextOver = TXT_MainTabMenuTextOver.Text;
            objBrandData.MainTabMenuBack = TXT_MainTabMenuBack.Text;
            objBrandData.MainTabMenuBackOver = TXT_MainTabMenuBackOver.Text;
            objBrandData.MainTabMenuBorder = TXT_MainTabMenuBorder.Text;
            objBrandData.MainTabMenuBorderOver = TXT_MainTabMenuBorderOver.Text;
            objBrandData.MenuBottomLine = TXT_MenuBottomLine.Text;
            objBrandData.SubTabMenuText = TXT_SubTabMenuText.Text;
            objBrandData.SubTabMenuTextOver = TXT_SubTabMenuTextOver.Text;
            objBrandData.MainForm = TXT_MainForm.Text;
            objBrandData.Footer = TXT_Footer.Text;
            objBrandData.FooterText = TXT_FooterText.Text;
            objBrandData.CompanyID = Convert.ToInt32(DDL_CompanyID.SelectedValue);

            objBrandData.PopUpTabMainColor = TXT_PopUpTabMainColor.Text;
            objBrandData.PopUpTabText = TXT_PopUpTabText.Text;
            objBrandData.PopUpTabSelectedText = TXT_PopUpTabSelectedText.Text;


            //========== Updating for Taskero User and its Details Table ==========//
            int _Result = objBrandData.uspUpdateCompanyBrandingData();
            //==========  Checking for the Flag ==========//
            if (_Result == 1)
            {
                LblError.Text = "Brand Data updated successfully.";
                BTN_AddBrand.Visible = true;
                TRGridTable.Visible = true;
                TRMainTable.Visible = false;
                ClearControls();
                FillBrandGrid();
            }
            else
            {
                LblError.Text = "Brand Data not Inserted successfully.";
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //=========== Method Used for Hide the Brand Remove Button ==========//
    protected void HideOption()
    {
        try
        {
            TRMessage1.Visible = false;
            chkHeaderLogo.Visible = false;
            chkLargeImage.Visible = false;
            LnkHeaderLogo.Visible = false;
            LnkLargeImage.Visible = false;
            LTRL_ImagePath_Logo.Text = "";
            LTRL_ImagePath_LargeImage.Text = "";
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //=========== Method Used for Delete the Branding Images and Css File ==========//
    protected void DeleteFile(string FileName)
    {
        try
        {
            if (System.IO.File.Exists(MapPath(".") + FileName))
                System.IO.File.Delete(MapPath(".") + FileName);
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    //=========== Create New Css and Style ==========//
    private void CreateCssFileAndAssignColors(ref string strCSSFile)
    {
        try
        {
            if (CHK_ConfirmCssChange.Checked == true)
            {
                //========= Assign To LTRL ==========//
                LTRL_TopLine.Text = TXT_TopLine.Text;
                LTRL_MainTabMenuText.Text = TXT_MainTabMenuText.Text;
                LTRL_MainTabMenuTextOver.Text = TXT_MainTabMenuTextOver.Text;
                LTRL_MainTabMenuBack.Text = TXT_MainTabMenuBack.Text;
                LTRL_MainTabMenuBackOver.Text = TXT_MainTabMenuBackOver.Text;
                LTRL_MainTabMenuBorder.Text = TXT_MainTabMenuBorder.Text;
                LTRL_MainTabMenuBorderOver.Text = TXT_MainTabMenuBorderOver.Text;
                LTRL_MenuBottomLine.Text = TXT_MenuBottomLine.Text;
                LTRL_SubTabMenuText.Text = TXT_SubTabMenuText.Text;
                LTRL_SubTabMenuTextOver.Text = TXT_SubTabMenuTextOver.Text;
                LTRL_MainForm.Text = TXT_MainForm.Text;
                LTRL_Footer.Text = TXT_Footer.Text;
                LTRL_FooterText.Text = TXT_FooterText.Text;

                LTRL_PopUpTabMainColor.Text = TXT_PopUpTabMainColor.Text;
                LTRL_PopUpTabText.Text = TXT_PopUpTabText.Text;
                LTRL_PopUpTabSelectedText.Text = TXT_PopUpTabSelectedText.Text;

                //========= Read the Template Css ==========//
                System.IO.StreamReader objSR = new System.IO.StreamReader(Server.MapPath("Css/Template.css"));
                string TemplateCss = objSR.ReadToEnd();
                objSR.Close();

                //========= Replace The Values ==========//
                TemplateCss = TemplateCss.Replace("#~TopLine~#", LTRL_TopLine.Text);
                TemplateCss = TemplateCss.Replace("#~MainTabMenuText~#", LTRL_MainTabMenuText.Text);
                TemplateCss = TemplateCss.Replace("#~MainTabMenuTextOver~#", LTRL_MainTabMenuTextOver.Text);
                TemplateCss = TemplateCss.Replace("#~MainTabMenuBack~#", LTRL_MainTabMenuBack.Text);
                TemplateCss = TemplateCss.Replace("#~MainTabMenuBackOver~#", LTRL_MainTabMenuBackOver.Text);
                TemplateCss = TemplateCss.Replace("#~MainTabMenuBorder~#", LTRL_MainTabMenuBorder.Text);
                TemplateCss = TemplateCss.Replace("#~MainTabMenuBorderOver~#", LTRL_MainTabMenuBorderOver.Text);
                TemplateCss = TemplateCss.Replace("#~MenuBottomLine~#", LTRL_MenuBottomLine.Text);
                TemplateCss = TemplateCss.Replace("#~SubTabMenuText~#", LTRL_SubTabMenuText.Text);
                TemplateCss = TemplateCss.Replace("#~SubTabMenuTextOver~#", LTRL_SubTabMenuTextOver.Text);
                TemplateCss = TemplateCss.Replace("#~MainForm~#", LTRL_MainForm.Text);
                TemplateCss = TemplateCss.Replace("#~Footer~#", LTRL_Footer.Text);
                TemplateCss = TemplateCss.Replace("#~FooterText~#", LTRL_FooterText.Text);

                TemplateCss = TemplateCss.Replace("#~PopUpTabMainColor~#", LTRL_PopUpTabMainColor.Text);
                TemplateCss = TemplateCss.Replace("#~PopUpTabText~#", LTRL_PopUpTabText.Text);
                TemplateCss = TemplateCss.Replace("#~PopUpTabSelectedText~#", LTRL_PopUpTabSelectedText.Text);

                //========= Create New Css Template ==========//
                strCSSFile = TXT_BrandingName.Text.Replace(" ", "-") + "-" + System.DateTime.Now.Second.ToString() + "-" + System.DateTime.Now.Millisecond.ToString() + ".css";

                //========= Write In a file ==========//
                System.IO.StreamWriter objSW = new System.IO.StreamWriter(Server.MapPath("Css/" + strCSSFile));
                objSW.Write(TemplateCss);
                objSW.Close();

            }
            else
            { strCSSFile = LTRL_Css.Text; }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //=========== Assign Background Color ==========//
    private void AssignBackgroundColor()
    {
        try
        {
            TXT_Color_TopLine.Style.Add("background-color", TXT_TopLine.Text);
            TXT_Color_MainTabMenuText.Style.Add("background-color", TXT_MainTabMenuText.Text);
            TXT_Color_MainTabMenuTextOver.Style.Add("background-color", TXT_MainTabMenuTextOver.Text);
            TXT_Color_MainTabMenuBack.Style.Add("background-color", TXT_MainTabMenuBack.Text);
            TXT_Color_MainTabMenuBackOver.Style.Add("background-color", TXT_MainTabMenuBackOver.Text);
            TXT_Color_MainTabMenuBorder.Style.Add("background-color", TXT_MainTabMenuBorder.Text);
            TXT_Color_MainTabMenuBorderOver.Style.Add("background-color", TXT_MainTabMenuBorderOver.Text);
            TXT_Color_MenuBottomLine.Style.Add("background-color", TXT_MenuBottomLine.Text);
            TXT_Color_SubTabMenuText.Style.Add("background-color", TXT_SubTabMenuText.Text);
            TXT_Color_SubTabMenuTextOver.Style.Add("background-color", TXT_SubTabMenuTextOver.Text);
            TXT_Color_MainForm.Style.Add("background-color", TXT_MainForm.Text);
            TXT_Color_Footer.Style.Add("background-color", TXT_Footer.Text);
            TXT_Color_FooterText.Style.Add("background-color", TXT_FooterText.Text);

            TXT_Color_PopUpTabMainColor.Style.Add("background-color", TXT_PopUpTabMainColor.Text);
            TXT_Color_PopUpTabText.Style.Add("background-color", TXT_PopUpTabText.Text);
            TXT_Color_PopUpTabSelectedText.Style.Add("background-color", TXT_PopUpTabSelectedText.Text);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillDDLCompany()
    {
        try
        {
            //==========  Declaring Objects ==========//
            UserCompanyData objUserCompany = new UserCompanyData();
            objUserCompany.TaskeroUserID = Sessions.UserID;
            objUserCompany.CompanyID = Sessions.RoleID;

            //==========  Get Data Table and Bind ==========//spCompanySelectByUserID
            DataTable DT_Company = new DataTable();
            DT_Company = objUserCompany.uspGetCompanyByUserID();

            //========== Fill DDL ==========//
            DDL_CompanyID.DataSource = DT_Company;
            DDL_CompanyID.DataTextField = "CompanyName";
            DDL_CompanyID.DataValueField = "CompanyID";
            DDL_CompanyID.DataBind();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
}

