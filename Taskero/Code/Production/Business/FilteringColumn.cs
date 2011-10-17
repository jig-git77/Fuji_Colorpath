#region 
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Fuji.Business
{
    public class FilteringColumn : Telerik.WebControls.GridTemplateColumn
    {  
        /// <summary>
        /// This Class is usefull for Bind Dynamic DropDown in Red Grid for Filter
        /// 
        /// </summary>
        
        #region SetupFilterControls

        protected override void SetupFilterControls(System.Web.UI.WebControls.TableCell cell)
        {
            //====  Create Object and Set Property of DropDown ====
            DropDownList DDLFilter = new DropDownList();
            DDLFilter.ID = this.UniqueName;
            DDLFilter.CssClass = "inputbox";
            DDLFilter.AutoPostBack = true;
            DDLFilter.DataTextField = this.DataField;
            DDLFilter.DataValueField = this.DataField;
            DDLFilter.SelectedIndexChanged += new EventHandler(DDLFilter_SelectedIndexChanged);
            DDLFilter.PreRender += new EventHandler(DDLFilter_PreRender);

            ListItem DDLiShowAll = new ListItem();
            DDLiShowAll.Text = "- All -";
            DDLiShowAll.Value = "";

            if (this.UniqueName.ToString() == "Status")
            {
                //====  Assign Status ====//
                DDLFilter.Items.Add(DDLiShowAll);
                DDLFilter.Items.Add(Fuji.Common.Enums.enumStatus.Failed.ToString());
                DDLFilter.Items.Add(Fuji.Common.Enums.enumStatus.Caution.ToString());
                DDLFilter.Items.Add(Fuji.Common.Enums.enumStatus.Passed.ToString());
            }
            else if (this.UniqueName.ToString() == "JobStatus")
            {
                //====  Assign Status ====//
                DDLFilter.Items.Add(DDLiShowAll);
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = "SELECT JobStatusDesc AS Status,JobStatusDesc AS Status FROM Press.Jobstatus";
                DDLFilter.DataSource = (new Fuji.DataAccess.Database()).GetDataTable(Cmd); 

                //====  Assign Status ====//
                //DDLFilter.Items.Add(DDLiShowAll);
                //DDLFilter.Items.Add(Fuji.Common.Enums.enumJobStatus.MakeReady.ToString());
                //DDLFilter.Items.Add(Fuji.Common.Enums.enumJobStatus.Production.ToString());
                //DDLFilter.Items.Add(Fuji.Common.Enums.enumJobStatus.Complete.ToString());
            }
            else if (this.UniqueName.ToString() == "DeviceStatus")
            {
                //====  Assign Status ====//
                DDLFilter.Items.Add(DDLiShowAll);
                DDLFilter.Items.Add(Fuji.Common.Enums.enumDeviceStatus.Green.ToString());
                DDLFilter.Items.Add(Fuji.Common.Enums.enumDeviceStatus.Yellow.ToString());
                DDLFilter.Items.Add(Fuji.Common.Enums.enumDeviceStatus.Orange.ToString());
                DDLFilter.Items.Add(Fuji.Common.Enums.enumDeviceStatus.Red.ToString());

            }
            else if (this.UniqueName.ToString() == "DeviceType")
            {
                DDLFilter.Items.Add(DDLiShowAll);

                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = " SELECT CPDeviceTypeID, CPDeviceTypeDesc FROM CPDeviceType";

                DDLFilter.DataSource = (new Fuji.DataAccess.Database()).GetDataTable(Cmd); 

            }
            else if (this.UniqueName.ToString() == "DongleType")
            {
                DDLFilter.Items.Add(DDLiShowAll);

                DDLFilter.Items.Add(Fuji.Common.Enums.DongleType.CPV.ToString());
                DDLFilter.Items.Add(Fuji.Common.Enums.DongleType.ITX.ToString());
                DDLFilter.Items.Add(Fuji.Common.Enums.DongleType.Sync.ToString());
                DDLFilter.Items.Add(Fuji.Common.Enums.DongleType.EFI.ToString());
                DDLFilter.Items.Add(Fuji.Common.Enums.DongleType.CTW.ToString());
                DDLFilter.Items.Add(Fuji.Common.Enums.DongleType.ColorGATE.ToString());
            }
            else if (this.UniqueName.ToString() == "DeviceTypeWithoutPress")
            {
                DDLFilter.Items.Add(DDLiShowAll);

                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = " SELECT CPDeviceTypeID, CPDeviceTypeDesc FROM CPDeviceType  Where CPDeviceTypeID <> 5";

                DDLFilter.DataSource = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);

            }
            //========== Gets All company List for the sorting ===========//
            else if (this.UniqueName.ToString() == "CompanyName")
            {
                DDLFilter.Items.Add(DDLiShowAll);

                //========== Assigns the company list to the dropdownlist ===========//                
                //========== Declare object of the user company data ===========//
                UserCompanyData ObjUserCompanyData = new UserCompanyData();

                //========== Assign the object value to get copmany list ===========//
                ObjUserCompanyData.TaskeroUserID = Fuji.Common.Sessions.UserID;
                ObjUserCompanyData.CompanyID = Fuji.Common.Sessions.RoleID;

                //========== Get company list and assign to dropdownlist ===========//
                DDLFilter.DataSource = ObjUserCompanyData.uspGetCompanyByUserID();
            }
            else if (this.UniqueName.ToString() == "MeasurementType")
            {
                DDLFilter.Items.Add(DDLiShowAll);

                //========== Get MeasurementType Data and assign to dropdownlist ===========//
                DDLFilter.DataSource = (new CPMeasurementTypeData().uspGetDistinctMeasurementTypeByUserID(Fuji.Common.Sessions.UserID, Fuji.Common.Sessions.RoleID));
            }
            else if (this.UniqueName.ToString() == "DDeviceType")
            {
                //DDLFilter.Items.Add(DDLiShowAll);

                //========== Get DeviceType Data and assign to dropdownlist ===========//
                DDLFilter.DataSource = (new CPDeviceTypeData().uspGetDistinctDeviceTypeByUserID(Fuji.Common.Sessions.UserID, Fuji.Common.Sessions.RoleID));
                DDLFilter.Items.Insert(0, DDLiShowAll);
            }
            cell.Controls.Add(DDLFilter);
        }
        
        #endregion

        #region ====  Set Current Filter Value To Control ====

        protected override void SetCurrentFilterValueToControl(TableCell cell)
        {
            if (this.CurrentFilterValue != "")
            {
                (cell.Controls[0] as DropDownList).Items.FindByText(this.CurrentFilterValue).Selected = true;
            }
        }
        #endregion

        #region ====  Get Current Filter Value From Control  ====
        protected override string GetCurrentFilterValueFromControl(TableCell cell)
        {
            string currentValue = (cell.Controls[0] as DropDownList).SelectedItem.Value;
            this.CurrentFilterFunction = (currentValue != "") ? Telerik.WebControls.GridKnownFunction.EqualTo : Telerik.WebControls.GridKnownFunction.NoFilter;
            return currentValue;
        }
        #endregion

        #region ====  DDLFilter SelectedIndexChanged  ====
        private void DDLFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Telerik.WebControls.GridFilteringItem)(((DropDownList)sender).Parent.Parent)).FireCommandEvent("Filter", new Pair());
        }
        #endregion

        #region ====  DDLFilter PreRender  ====
        private void DDLFilter_PreRender(object sender, EventArgs e)
        {
            DropDownList DDLFilter = (DropDownList)sender;
            if (DDLFilter != null)
            {
                if (DDLFilter.Items.FindByValue(string.Empty) == null)
                {
                    DDLFilter.Items.Insert(0, new ListItem("-- All --", ""));
                }
            }
        }
        #endregion

    }
}
