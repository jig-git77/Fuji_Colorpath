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


public partial class TempRHDeviceManagement : PageBase
{
    #region Declarations
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

            //========== Check User Role and redirect to home page as per user role ===========//
            Fuji.Common.CommonFunctions.CheckUserRole();

            //=========== Default Settings ==========//
            if (IsPostBack == false)
            {
                TRMainTable.Visible = false;
                TRGridTable.Visible = true;
                BTN_AddTempRHDEVICES.Visible = true;

                //========== Fill Variables to Form ==========//
                SetLabels();

                //=========== Clear the Session with Datatable of User Data==========//
                Fuji.Common.Sessions.SetSessions("RHDevice_DataTable", null);
                Fuji.Common.Sessions.SetSessions("TempRHDeviceUser_Data", null);

                //=========== Calling the Method for binding the RH Device Grid ==========//
                BindingRHDeviceGrid();

                //=========== Calling the Method for Checking User's Rights==========//
                CheckRights();
            }

        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    protected void BTN_AddSave_Click(object sender, EventArgs e)
    {
        try
        {//========== Checking the Lable for making the descision of Add or Update===========//
            if (LTRL_ID.Text.Trim().Equals("0", StringComparison.CurrentCultureIgnoreCase))
            {
                //========== Calling the Add Method ===========//
                AddRHDevice();
            }
            else
            {
                //========== Calling the Update Method ===========//
                UpdateRHDevice();
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    protected void BTN_Cancel_Click(object sender, EventArgs e)
    {
        try
        { //========== Calling the Method for clearing the Controls ===========//            
            ClearControls();

            //========== Enabling the Grid ===========//
            BTN_AddTempRHDEVICES.Visible = true;
            TRGridTable.Visible = true;
            TRMainTable.Visible = false;
            Fuji.Common.Sessions.SetSessions("TempRHDeviceUser_Data", null);
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    protected void BTN_AddTempRHDEVICES_Click(object sender, EventArgs e)
    {
        try
        {
            //=========== Enabling the Controls and Disabling the Grid ==========//

            BTN_AddTempRHDEVICES.Visible = false;
            TRGridTable.Visible = false;
            TRMainTable.Visible = true;

            //=========== Clearing the All Controls ==========//
            ClearControls();

            //========== Fill Variables to Form ==========//
            //SetLabels();

            //=========== Fill DDl Company ==========//
            FillCompany();

            //=========== Fill DDl TRHDeviceType==========//
            FillTRHDeviceType();
            DDL_Branch.Items.Insert(0, (new ListItem("---Select Branch---", "0")));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            //========== Check CommandName for the Edit Event ===========//
            if (e.CommandName == "EditTempRhDevice")
            {
                //========== Filling the Controls when Edit Event is called ===========//
                EditRHDevice(Convert.ToInt16(e.Item.Cells[2].Text));
                TRGridTable.Visible = false;
                TRMainTable.Visible = true;
                BTN_AddTempRHDEVICES.Visible = false;
                LTRL_SubTitle.Text = Resources.TempRHDeviceManagement.LBL_SubTitle_Text_Edit;
                BTN_AddSave.Text = Resources.TempRHDeviceManagement.BTN_AddSave_Save;
            }
            //========== Check CommandName for the Delete Event ===========//
            if (e.CommandName == "Delete")
            {
                //========== Object Declaration for the Deletion ===========//                
                TempRHDeviceData ObjRHDevice = new TempRHDeviceData();
                ObjRHDevice.TempRHDeviceID = Convert.ToInt16(e.Item.Cells[2].Text);
                //========== Calling the Method for Delete the Recored ===========//
                int Result = ObjRHDevice.uspDeleteTempRHDevice();
                if (Result == 2)
                {
                    LblError.Text = "Temp RH Device deleted successfully.";
                    BindingRHDeviceGrid();
                }
                else LblError.Text = "Reference exist. Can't delete TempRHDevice!";
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
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

                Literal LTRL_RecordText = (Literal)e.Item.FindControl("LTRL_RecordText");
                int iFirstIndexInPage = 0, iLastIndexInPage = 0, iDataSourceCount = 0;

                iFirstIndexInPage = RadGrid1.MasterTableView.PagingManager.FirstIndexInPage;
                iLastIndexInPage = RadGrid1.MasterTableView.PagingManager.LastIndexInPage + 1;
                iDataSourceCount = RadGrid1.MasterTableView.PagingManager.DataSourceCount;

                if (iDataSourceCount > 0)
                {
                    iFirstIndexInPage++;
                }

                LTRL_RecordText.Text = "Record(s) : " + iFirstIndexInPage + " to " + iLastIndexInPage + " of " + iDataSourceCount + " | ";

                ImageButton lbFirst = (ImageButton)e.Item.FindControl("lbFirst");
                ImageButton lbPrev = (ImageButton)e.Item.FindControl("lbPrev");
                ImageButton lbNext = (ImageButton)e.Item.FindControl("lbNext");
                ImageButton lbLast = (ImageButton)e.Item.FindControl("lbLast");


                if (RadGrid1.MasterTableView.PagingManager.IsFirstPage)
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
                if (RadGrid1.MasterTableView.PagingManager.IsLastPage == true)
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
        { LblError.Text = HandleException(Exc); }
    }

    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
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
                btnReset.Text = Resources.TempRHDeviceManagement.btnReset_Text;
                if (btnReset != null)
                {
                    btnReset.Click += new EventHandler(btnReset_Click);
                }
            }
        }
        catch (Exception exc)
        {
            LblError.Text = HandleException(exc);
        }
    }

    protected void RadGrid1_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        //========== Applying the Datasource to RadGrid at the time of postback or calling any event ===========//
        try
        {
            //if (TRMainTable.Visible == false)
            //{
            if (Fuji.Common.Sessions.GetSession("RHDevice_DataTable") != null)
            {
                DataTable DSet = (DataTable)Fuji.Common.Sessions.GetSession("RHDevice_DataTable");
                if (DSet != null && DSet.Rows.Count > 0)
                {
                    RadGrid1.DataSource = DSet;
                }
                else
                {
                    DataTable Dset = GetRHDeviceData();
                    if (DSet != null && DSet.Rows.Count > 0)
                        RadGrid1.DataSource = Dset;
                }
            }
            //}
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    protected void RadGrid1_SortCommand(object source, Telerik.WebControls.GridSortCommandEventArgs e)
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
        { LblError.Text = HandleException(Exc); }


    }

    protected void DDL_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //==========  Declaring Objects ==========//
            UserBranchData objBranch = new UserBranchData();

            //==========  Get Data Table and Fill ==========//
            objBranch.TaskeroUserID = Sessions.UserID;
            objBranch.CompanyID = Convert.ToInt32(DDL_Company.SelectedValue);

            DataTable DT_Branches = objBranch.uspGetBranchOfCompanyByUserID(Sessions.RoleID);
            DDL_Branch.DataSource = DT_Branches;
            DDL_Branch.DataTextField = "BranchName";
            DDL_Branch.DataValueField = "BranchID";
            DDL_Branch.DataBind();
            //==========  Fill Default Values ==========//
            DDL_Branch.Items.Insert(0, (new ListItem("---Select Branch---", "0")));
            DDL_Branch.SelectedIndex = -1;
            DDL_Branch_SelectedIndexChanged(null, null);
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
        finally { }
    }

    private void DDlRecordPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            string sSelectedValue;
            sSelectedValue = ((DropDownList)sender).SelectedItem.Value;
            ViewState["RecordPerPage"] = sSelectedValue;

            RadGrid1.PageSize = Convert.ToInt32(sSelectedValue);
            RadGrid1.CurrentPageIndex = 0;
            if (Fuji.Common.Sessions.GetSession("RHDevice_DataTable") != null)
            {
                RadGrid1.DataSource = Fuji.Common.Sessions.GetSession("RHDevice_DataTable");
            }

            RadGrid1.DataBind();
        }
        catch (Exception ex)
        {
            LblError.Text = HandleException(ex);
        }
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        //========== Event for Clearing all the Filter in the Grid ===========//
        try
        {
            foreach (Telerik.WebControls.GridColumn column in RadGrid1.MasterTableView.Columns)
            {
                column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                column.CurrentFilterValue = String.Empty;
            }
            RadGrid1.MasterTableView.FilterExpression = String.Empty;
            RadGrid1.MasterTableView.Rebind();
        }
        catch (Exception ex)
        { LblError.Text = HandleException(ex); }
    }

    protected void DDL_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDL_Branch.SelectedIndex == 0)
            {PNL_Devices.Height = 0; PNL_Devices.Visible = false;}
            else 
            {
                FillDevice();
                if (!LTRL_ID.Text.Trim().Equals("0", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (Fuji.Common.Sessions.GetSession("TempRHDeviceUser_Data") != null)
                    {
                        DataSet DT_DeviceData = (DataSet)Fuji.Common.Sessions.GetSession("TempRHDeviceUser_Data");
                        CheckDeviceData(DT_DeviceData.Tables[1], CHK_Devices);
                    }
                }
            }
        }
        catch (Exception ex)
        { LblError.Text = HandleException(ex); }
    }
    #endregion

    #region Methods and Events

    //=========== Method for Fetching the Devices ==========//
    private void FillDevice()
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPDeviceData ObjDevice = new CPDeviceData();

            //==========  Get Data Table and Bind ==========//
            DataTable DT_Device = new DataTable();

            //==========  Fetching Data from Table ==========//
            //DT_Device = ObjDevice.uspGetAllDeviceByUserID(Fuji.Common.Sessions.UserID, Fuji.Common.Sessions.RoleID);
            //DT_Device.DefaultView.RowFilter = "Distinct CPDeviceDesc ASC";
            if (DDL_Company.SelectedIndex == -1 || DDL_Company.SelectedIndex == 0)
                ObjDevice.CompanyID = 0;
            else
                ObjDevice.CompanyID = Convert.ToInt32(DDL_Company.SelectedValue);
            if (DDL_Branch.SelectedIndex == -1 || DDL_Branch.SelectedIndex == 0)
                ObjDevice.BranchID = 0;
            else
                ObjDevice.BranchID = Convert.ToInt32(DDL_Branch.SelectedValue);
            DT_Device = ObjDevice.uspSelectDistinctDevices(Fuji.Common.Sessions.UserID, Fuji.Common.Sessions.RoleID);
            if (DT_Device != null && DT_Device.Rows.Count > 0)
            {
                CHK_Devices.DataSource = DT_Device;
                CHK_Devices.DataTextField = "CPDeviceDesc";//DongleDescription
                CHK_Devices.DataValueField = "CPDeviceID";
                CHK_Devices.DataBind();
                if (CHK_Devices.Items.Count == 0)
                { PNL_Devices.Height = 0; PNL_Devices.Visible = false; }
                else if (CHK_Devices.Items.Count >= 5)
                { PNL_Devices.Height = 120; PNL_Devices.Visible = true; }
                else if (CHK_Devices.Items.Count <= 2)
                { PNL_Devices.Height = 48; PNL_Devices.Visible = true; }
                else
                { PNL_Devices.Height = 75; PNL_Devices.Visible = true; }
            }
            else { PNL_Devices.Height = 0; PNL_Devices.Visible = false; }
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
            //=========== Clear Controls ==========//
            DDL_Company.SelectedIndex = -1;
            DDL_Branch.Items.Clear();
            CHK_Devices.Items.Clear();
            PNL_Devices.Visible = false;
            TXT_TRHDeviceSerialNum.Text = "";
            TXT_TargetRH.Text = "";
            TXT_TargetTemp.Text = "";
            DDL_TRHDeviceType.SelectedValue = "1";
            LTRL_ID.Text = "0";
            LTRL_SubTitle.Text = Resources.TempRHDeviceManagement.LBL_SubTitle_Text_Add;
            BTN_AddSave.Text = Resources.TempRHDeviceManagement.BTN_AddSave_Add;

            DDL_Company.Enabled = true;
            DDL_Branch.Enabled = true;
            CHK_Devices.Enabled = true;
            TXT_TRHDeviceSerialNum.Enabled = true;
            DDL_TRHDeviceType.Enabled = true;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    #region Set Labels
    /// <summary>
    /// Used to set the labels of the form
    /// </summary>
    private void SetLabels()
    {
        try
        {
            //========== Set Culture ==========//
            Resources.Message.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));
            Resources.TempRHDeviceManagement.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));

            //========== Set Values ==========//
            LTRL_Title.Text = Resources.TempRHDeviceManagement.LBL_Title_Text;
            LTRL_SubTitle.Text = Resources.TempRHDeviceManagement.LBL_SubTitle_Text_Add;
            LTRL_CompanyName.Text = Resources.TempRHDeviceManagement.LBL_CompanyName_Text;
            LTRL_BranchName.Text = Resources.TempRHDeviceManagement.LBL_BranchName_Text;
            LTRL_TRHDeviceSerialNum.Text = Resources.TempRHDeviceManagement.LBL_TRHDeviceSerialNum_Text;
            LTRL_TRHDeviceType.Text = Resources.TempRHDeviceManagement.LBL_TRHDeviceType;
            LTRL_TargetTemp.Text = Resources.TempRHDeviceManagement.LBL_TargetTemp;
            LTRL_TargetRH.Text = Resources.TempRHDeviceManagement.LBL_TargetRH;
            LTRL_Devices.Text = Resources.TempRHDeviceManagement.LBLTRHDeviceName;

            //=========== Set Button Text ==========//
            BTN_AddTempRHDEVICES.Text = Resources.TempRHDeviceManagement.BTN_AddTempRHDEVICES_Text;
            BTN_AddSave.Text = Resources.TempRHDeviceManagement.BTN_AddSave_Add;
            BTN_Cancel.Text = Resources.TempRHDeviceManagement.BTN_Cancel_Text;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    //=========== Fill Company DropDown ==========//
    private void FillCompany()
    {
        try
        {
            //==========  Declaring Objects ==========//
            UserCompanyData objCompany = new UserCompanyData();

            //==========  Get Data Table and Fill ==========//
            objCompany.TaskeroUserID = Fuji.Common.Sessions.UserID;
            objCompany.CompanyID = Fuji.Common.Sessions.RoleID;
            DDL_Company.DataSource = objCompany.uspGetCompanyByUserID();
            DDL_Company.DataTextField = "CompanyName";
            DDL_Company.DataValueField = "CompanyID";
            DDL_Company.DataBind();

            //==========  Fill Default Values ==========//
            DDL_Company.Items.Insert(0, (new ListItem("---Select---", "0")));
            DDL_Company.SelectedIndex = 0;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    //=========== Method for Fetching the RH Device Data ==========//
    private DataTable GetRHDeviceData()
    {
        try
        {
            //==========  Declaring Objects ==========//
            TempRHDeviceData ObjRHDevice = new TempRHDeviceData();


            //==========  Fetching the Data From RH Device ==========//
            ObjRHDevice.TempRHDeviceID = 0;
            DataTable _DTRHDevice = ObjRHDevice.uspGetTempRHDeviceData1(Sessions.BranchIDs);

            //=========== Setting the Session with Datatable of User Data==========//
            Fuji.Common.Sessions.SetSessions("RHDevice_DataTable", _DTRHDevice);

            return _DTRHDevice;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    //=========== Binding the RH Device Data Grid==========//
    private void BindingRHDeviceGrid()
    {
        try
        {
            //==========  Declaring Objects & calling the function which is fetching the data from Temp RH Device Table ==========//
            DataTable _Dset = GetRHDeviceData();
            if (_Dset != null && _Dset.Rows.Count > 0)
            {
                RadGrid1.Visible = true;
                RadGrid1.DataSource = _Dset;
                RadGrid1.DataBind();
            }
            else
            {
                //RadGrid1.Visible = false;
                RadGrid1.DataSource = _Dset;
                RadGrid1.DataBind();
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    //=========== Method Used for Checking the rights of the User logged in==========//
    private void CheckRights()
    {
        try
        {
            //========== Checking the Add Right for the Temp_RH_Device ===========//
            if (Sessions.Add_Temp_RH_Device == false)
                BTN_AddTempRHDEVICES.Visible = false;
            else
                BTN_AddTempRHDEVICES.Visible = true;
            //========== Checking the Edit Right for the Temp_RH_Device===========//
            if (Sessions.Update_Temp_RH_Device == false)
            { RadGrid1.Columns[6].Visible = false; }
            else
            { RadGrid1.Columns[6].Visible = true; }
            //========== Checking the Delete Right for the Temp_RH_Device===========//
            //if (Sessions.Delete_Temp_RH_Device == false)
            //{ RadGrid1.Columns[7].Visible = false; }
            //else
            //{ RadGrid1.Columns[7].Visible = true; }
            RadGrid1.Columns[7].Visible = false;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    //=========== Edit the RH Device Data Value ==========//
    private void EditRHDevice(Int16 TempRHDeviceID)
    {
        try
        {
            //==========  Filling the Controls at the time of Editing so calling the Default Add method ==========//
            BTN_AddTempRHDEVICES_Click(null, null);

            //==========  Declaring Objects ==========//
            TempRHDeviceData ObjRHDevice = new TempRHDeviceData();


            //==========  Fetching the Data From RH Device ==========//
            ObjRHDevice.TempRHDeviceID = TempRHDeviceID;
            DataSet _DTRHDevice = ObjRHDevice.uspGetTempRHDeviceData();
            if (_DTRHDevice != null && _DTRHDevice.Tables.Count > 0 && _DTRHDevice.Tables[0].Rows.Count > 0)
            {
                try
                {
                    Fuji.Common.Sessions.SetSessions("TempRHDeviceUser_Data", _DTRHDevice);
                    if (DDL_Company.Items.FindByValue(_DTRHDevice.Tables[0].Rows[0]["CompanyID"].ToString()) != null)
                    {
                        DDL_Company.SelectedValue = _DTRHDevice.Tables[0].Rows[0]["CompanyID"].ToString();
                        DDL_Company_SelectedIndexChanged(null, null);
                    }
                    //else DDL_Branch.Items.Insert(0, (new ListItem("---Select Branch---", "0")));
                    if (DDL_Branch.Items.FindByValue(_DTRHDevice.Tables[0].Rows[0]["BranchID"].ToString()) != null)
                    {
                        DDL_Branch.SelectedValue = _DTRHDevice.Tables[0].Rows[0]["BranchID"].ToString();
                        DDL_Branch_SelectedIndexChanged(null, null);
                    }
                    TXT_TRHDeviceSerialNum.Text = _DTRHDevice.Tables[0].Rows[0]["TRHDeviceSerialNum"].ToString();
                    TXT_TargetRH.Text = _DTRHDevice.Tables[0].Rows[0]["TargetRH"].ToString();
                    TXT_TargetTemp.Text = _DTRHDevice.Tables[0].Rows[0]["TargetTemp"].ToString();
                    if (DDL_TRHDeviceType.Items.FindByValue(_DTRHDevice.Tables[0].Rows[0]["TRHDeviceTypeID"].ToString()) != null)
                        DDL_TRHDeviceType.SelectedValue = _DTRHDevice.Tables[0].Rows[0]["TRHDeviceTypeID"].ToString();
                    LTRL_ID.Text = TempRHDeviceID.ToString();

                    //==========  Setting the value of Device CheckBox ==========//
                    if (_DTRHDevice.Tables[1].Rows.Count > 0)
                    {
                        if (CHK_Devices.Items.Count > 0)
                        {
                            for (int i = 0; i < _DTRHDevice.Tables[1].Rows.Count; i++)
                            {
                                for (int j = 0; j < CHK_Devices.Items.Count; j++)
                                {
                                    if (CHK_Devices.Items[j].Value == _DTRHDevice.Tables[1].Rows[i]["CPDeviceID"].ToString())
                                    {
                                        CHK_Devices.Items[j].Selected = true;
                                    }
                                }
                            }
                        }
                    }

                    if (DDL_Company.SelectedIndex > 0)
                    {
                        DDL_Company.Enabled = false;
                        DDL_Branch.Enabled = false;
                        CHK_Devices.Enabled = true;
                    }
                    TXT_TRHDeviceSerialNum.Enabled = false;
                    DDL_TRHDeviceType.Enabled = false;
                }
                catch (Exception Exc)
                { LblError.Text = HandleException(Exc); }
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    //=========== Fill TempRHDeviceType DropDown ==========//
    private void FillTRHDeviceType()
    {
        try
        {
            //==========  Declaring Objects ==========//
            TempRHDeviceTypeData ObjRHDeviceType = new TempRHDeviceTypeData();
            //==========  Get Data Table and Fill ==========//
            ArrayList _ArrRoles = ObjRHDeviceType.uspGetTempRHDeviceType();
            if (_ArrRoles != null && _ArrRoles.Count > 0)
            {
                DDL_TRHDeviceType.DataTextField = "TRHDeviceTypeDesc";
                DDL_TRHDeviceType.DataValueField = "TRHDeviceTypeID";
                DDL_TRHDeviceType.DataSource = _ArrRoles;
                DDL_TRHDeviceType.DataBind();
            }
            //==========  Fill Default Values ==========//
            DDL_TRHDeviceType.Items.Insert(0, new ListItem("---Select Temp RHDevice Type---", "R"));
            DDL_TRHDeviceType.SelectedIndex = -1;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    //=========== Method Used for Insertng the Temp_RH_Device==========//
    private void AddRHDevice()
    {
        try
        {
            //========== Validations ==========//
            if (DDL_Company.SelectedIndex == 0) { LblError.Text = Resources.Message.CompanyNameIsRequired; return; }
            if (DDL_Branch.SelectedValue == "0") { LblError.Text = Resources.Message.BranchNameIsRequired; return; }
            if (CHK_Devices.SelectedIndex == -1) { LblError.Text = Resources.Message.DeviceNameIsRequired; return; }
            if (TXT_TRHDeviceSerialNum.Text.Trim() == "") { LblError.Text = Resources.Message.TempRHDeviceSerialNoIsRequired; return; }
            if (DDL_TRHDeviceType.SelectedValue.Equals("R", StringComparison.CurrentCultureIgnoreCase)) { LblError.Text = Resources.Message.TempRHDeviceTypeIsRequired; return; }

            if (TXT_TargetTemp.Text.Trim() == "") { LblError.Text = Resources.Message.TargetTemperatureIsRequired; return; }
            else if (!Fuji.Common.Validations.IsDecimal(TXT_TargetTemp.Text.Trim()))
            { LblError.Text = "Target Temperature should be Decimal in type."; return; }
            if (TXT_TargetRH.Text.Trim() == "") { LblError.Text = Resources.Message.TargetRHIsRequired; return; }
            else if (!Fuji.Common.Validations.IsDecimal(TXT_TargetRH.Text.Trim()))
            { LblError.Text = "Target RH should be Decimal in type."; return; }
            //========== Creating XML for BranchID ==========//
            string _StrTemp = "";
            for (int i = 0; i < CHK_Devices.Items.Count; i++)
            {
                if (CHK_Devices.Items[i].Selected)
                {
                    if (_StrTemp.Trim().Length > 0)
                    {
                        _StrTemp += "<DeviceID>" + CHK_Devices.Items[i].Value + "</DeviceID>";
                    }
                    else
                    {
                        _StrTemp = "<Device><DeviceID>" + CHK_Devices.Items[i].Value + "</DeviceID>";
                    }
                }
            }

            if (_StrTemp.Trim().Length > 0)
            {
                _StrTemp += "</Device>";
            }

            //========== Declarations ==========//
            TempRHDeviceData objRHDevice = new TempRHDeviceData();
            objRHDevice.BranchID = Convert.ToInt32(DDL_Branch.SelectedValue);
            objRHDevice.TRHDeviceSerialNum = TXT_TRHDeviceSerialNum.Text.Trim();
            objRHDevice.TRHDeviceTypeID = Convert.ToInt16(DDL_TRHDeviceType.SelectedValue);
            objRHDevice.TargetTemp = Convert.ToDecimal(TXT_TargetTemp.Text.Trim());
            objRHDevice.TargetRH = Convert.ToDecimal(TXT_TargetRH.Text.Trim());
            objRHDevice.Device = _StrTemp;
            //========== Inserting for TempRHDevice ==========//
            int _Result = objRHDevice.uspInsertTempRHDevice();
            //==========  Checking for the Flag ==========//
            if (_Result == 1)
            {
                LblError.Text = Resources.Message.DeviceSerialNumberAlreadyExists;
            }
            else if (_Result == 2)
            {
                LblError.Text = "Device Added successfully.";
                ClearControls();
                BTN_AddTempRHDEVICES.Visible = true;
                TRGridTable.Visible = true;
                TRMainTable.Visible = false;
                BindingRHDeviceGrid();
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    //=========== Method Used for Updating the Temp_RH_Device==========//
    private void UpdateRHDevice()
    {
        try
        {
            //========== Validations ==========//
            if (DDL_Company.SelectedIndex == 0) { LblError.Text = Resources.Message.CompanyNameIsRequired; return; }
            if (DDL_Branch.SelectedValue == "0") { LblError.Text = Resources.Message.BranchNameIsRequired; return; }
            if (CHK_Devices.SelectedIndex == -1) { LblError.Text = Resources.Message.DeviceNameIsRequired; return; }
            if (TXT_TRHDeviceSerialNum.Text == "") { LblError.Text = Resources.Message.TempRHDeviceSerialNoIsRequired; return; }
            if (DDL_TRHDeviceType.SelectedValue.Equals("R", StringComparison.CurrentCultureIgnoreCase)) { LblError.Text = Resources.Message.TempRHDeviceTypeIsRequired; return; }

            if (TXT_TargetTemp.Text == "") { LblError.Text = Resources.Message.TargetTemperatureIsRequired; return; }
            else if (!Fuji.Common.Validations.IsDecimal(TXT_TargetTemp.Text.Trim()))
            { LblError.Text = "Target Temperature should be Decimal in type."; return; }
            if (TXT_TargetRH.Text == "") { LblError.Text = Resources.Message.TargetRHIsRequired; return; }
            else if (!Fuji.Common.Validations.IsDecimal(TXT_TargetRH.Text.Trim()))
            { LblError.Text = "Target RH should be Decimal in type."; return; }
            //========== Creating XML for BranchID ==========//
            string _StrTemp = "";
            for (int i = 0; i < CHK_Devices.Items.Count; i++)
            {
                if (CHK_Devices.Items[i].Selected)
                {
                    if (_StrTemp.Trim().Length > 0)
                    {
                        _StrTemp += "<DeviceID>" + CHK_Devices.Items[i].Value + "</DeviceID>";
                    }
                    else
                    {
                        _StrTemp = "<Device><DeviceID>" + CHK_Devices.Items[i].Value + "</DeviceID>";
                    }
                }
            }

            if (_StrTemp.Trim().Length > 0)
            {
                _StrTemp += "</Device>";
            }

            //========== Declarations ==========//
            TempRHDeviceData objRHDevice = new TempRHDeviceData();
            objRHDevice.TempRHDeviceID = Convert.ToInt16(LTRL_ID.Text.Trim());
            objRHDevice.BranchID = Convert.ToInt32(DDL_Branch.SelectedValue);
            objRHDevice.TRHDeviceSerialNum = TXT_TRHDeviceSerialNum.Text.Trim();
            objRHDevice.TRHDeviceTypeID = Convert.ToInt16(DDL_TRHDeviceType.SelectedValue);
            objRHDevice.TargetTemp = Convert.ToDecimal(TXT_TargetTemp.Text.Trim());
            objRHDevice.TargetRH = Convert.ToDecimal(TXT_TargetRH.Text.Trim());
            objRHDevice.Device = _StrTemp;
            //========== Inserting for TempRHDevice ==========//
            int _Result = objRHDevice.uspUpdateTempRHDevice();
            //==========  Checking for the Flag ==========//
            if (_Result == 1)
            {
                LblError.Text = LblError.Text = Resources.Message.DeviceSerialNumberAlreadyExists;

            }
            else if (_Result == 2)
            {
                LblError.Text = "Row Updated successfully.";
                ClearControls();
                BTN_AddTempRHDEVICES.Visible = true;
                TRGridTable.Visible = true;
                TRMainTable.Visible = false;
                Fuji.Common.Sessions.SetSessions("TempRHDeviceUser_Data", null);
                BindingRHDeviceGrid();
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    #region Check Device Data
    /// <summary>
    /// Used to check the device data
    /// </summary>
    /// <param name="DT_Device">Device list</param>
    private void CheckDeviceData(DataTable DT_Device, CheckBoxList CHK_My_List)
    {
        try
        {
            for (int i = 0; i < DT_Device.Rows.Count; i++)
            {
                for (int j = 0; j < CHK_My_List.Items.Count; j++)
                {
                    if (CHK_My_List.Items[j].Value == DT_Device.Rows[i][1].ToString())
                    {
                        CHK_My_List.Items[j].Selected = true;
                    }
                }

            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #endregion    
}
