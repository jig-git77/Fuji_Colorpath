using System;
using System.Data;
using System.Web.UI.WebControls;
using Taskero.Common;
using Taskero.Business;
using TaskeroWeb.Models;


namespace TaskeroWeb
{
    public partial class SyncAddPressPopup : PageBase
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
                if (IsPostBack == false)
                {

                    //========= Fill branch drop down =============//
                    FillBranchDropDown();
                    FillPressTypeDropDown();
                    DdlSerialNumber.Items.Insert(0, new ListItem("-- Select Serial Number --", "0"));
                }
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }

        /// <summary>
        /// Save press information click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BTN_SavePress_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Validations For the new Device inputs ==========//
                if (DdlBranchs.SelectedIndex == 0) { LblError.Text = "Pelase select branch."; return; }
                if (DdlSerialNumber.SelectedIndex == 0) { LblError.Text = "Pelase select serial number."; return; }
                if (TxtPressName.Text.Trim() == "") { LblError.Text = "Plase enter Press Name."; return; }
                if (TxtManufature.Text.Trim() == "") { LblError.Text = "Plase enter Press Manufature."; return; }


                //==========  Declaring Objects ==========//
                CPSyncPressData objCPSyncPress = new CPSyncPressData();

                //========== check the device id, if not exist then insert new dongle ===========//
                objCPSyncPress.CPDongleID = Convert.ToInt16(DdlSerialNumber.SelectedValue.ToString());
                objCPSyncPress.PressName = TxtPressName.Text.Trim();
                objCPSyncPress.PressManufacturer = TxtManufature.Text.Trim();
                objCPSyncPress.NumOfInkUnits = Convert.ToInt16(DdlInkUnit.SelectedValue.ToString());
                objCPSyncPress.PressTypeID = Convert.ToInt16(DdlPressType.SelectedValue.ToString());
                objCPSyncPress.uspInsertCPSyncPress();


                #region Close this popup and refresh parent press grid
                string script = "<script>";
                script += "var modalPopupBehavior = parent.$find('programmaticModalPopupBehavior');    ";
                script += "modalPopupBehavior.hide();   ";
                script += "parent.CallPressBindGrid();";
                script += "</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "windowclose", script);
                #endregion
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        /// <summary>
        /// Drop down branch selected index change event to fill serial number drop down control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DdlBranchs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DdlBranchs.SelectedIndex > 0)
                {
                    FillSerialNumberDropDown(Convert.ToInt32(DdlBranchs.SelectedValue)); // fill serial number drop dwon control
                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        #endregion

        #region Methods
        /// <summary>
        ///  Used to fill the branch list based on the company id passed
        /// </summary>
        private void FillBranchDropDown()
        {
            try
            {
                //========== Declare the object of the UserBrach ===========//
                BranchData objBranch = new BranchData();
                //========== Declare DataTable ===========//
                DataTable DTBranch;

                //========== Fetch the data from branch  ===========//
                if (Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin) // Check login user is admin or not
                {
                    DTBranch = objBranch.uspGetAllBranch();
                }
                else
                {
                    DTBranch = objBranch.uspGetAllBranchWithCompanyName(Sessions.UserID, Sessions.RoleID);
                }

                //========== Check Whether the data is returned or not===========//
                if (DTBranch != null && DTBranch.Rows.Count > 0)
                {
                    //========== Assign the Branch list ===========//
                    DdlBranchs.DataSource = DTBranch;
                    DdlBranchs.DataValueField = "BranchID"; DdlBranchs.DataTextField = "BranchName";
                    DdlBranchs.DataBind();
                }
                else
                {
                    //========== Bind null values to the null ===========//
                    DdlBranchs.DataSource = null;
                    DdlBranchs.DataBind();
                }

                //========== Check Fill the first item as Select Branch===========//
                DdlBranchs.Items.Insert(0, new ListItem("-- Select Branch --", "0"));

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        /// <summary>
        /// Use to fill press type list 
        /// </summary>
        private void FillPressTypeDropDown()
        {
            try
            {
                //========== Clear Items in DropDownList ===========//
                DdlPressType.Items.Clear();

                //========== Declare the Object of the PressType ===========//
                PressTypeData objPressType = new PressTypeData();

                //==========  Get Data Table and Fill ==========//        
                DataTable DT_PressType = new DataTable();
                DT_PressType = objPressType.uspGetAllPressType();
                if (DT_PressType != null && DT_PressType.Rows.Count > 0)
                {
                    DdlPressType.DataSource = DT_PressType;
                    DdlPressType.DataValueField = "PressTypeID"; DdlPressType.DataTextField = "PressType";
                    DdlPressType.DataBind();
                }
                else
                { DdlPressType.DataSource = null; DdlPressType.DataBind(); }



            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        ///  Used to fill the branch list based on the company id passed
        /// </summary>
        private void FillSerialNumberDropDown(int BranchID)
        {
            try
            {
                //========== Clear Items in DropDownList ===========//
                DdlSerialNumber.Items.Clear();

                //========== Declare the object of the CP Dongal Data ===========//            
                CPDongleData objCPDongleData = new CPDongleData();

                //========== Declare DataTable ===========//
                DataTable DTDongle;

                //========== Fetch the data from dongle  ===========//

                DTDongle = objCPDongleData.uspGetAllDongleByBranch(BranchID);


                //========== Check Whether the data is returned or not===========//
                if (DTDongle != null && DTDongle.Rows.Count > 0)
                {
                    //========== Assign the Branch list ===========//
                    DdlSerialNumber.DataSource = DTDongle;
                    DdlSerialNumber.DataValueField = "CPDongleID"; DdlSerialNumber.DataTextField = "CPSerialNUM";
                    DdlSerialNumber.DataBind();
                }
                else
                {
                    //========== Bind null values to the null ===========//
                    DdlSerialNumber.DataSource = null;
                    DdlSerialNumber.DataBind();
                }

                //========== Check Fill the first item as Select Dongle===========//
                DdlSerialNumber.Items.Insert(0, new ListItem("-- Select Serial Number --", "0"));

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion


    }
}