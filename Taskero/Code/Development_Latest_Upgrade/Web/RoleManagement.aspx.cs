#region System
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Fuji.Common;
using Fuji.Business;
#endregion

public partial class RoleManagement : PageBase
{
    #region Declarations
    DataSet _DSRoleAndPermission;
    #endregion

    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //=========== Clear Error ==========//
            LblError.Text = "";

            //=========== Applying the CSS and Making Paging False ==========//
            Fuji.Common.CommonFunctions.SetGridViewDefaultSettings(GVRoles);
            GVRoles.AllowPaging = false;

            //=========== Calling the Method for Checking Page Rights==========//
            CheckRights();

            //=========== Check Login ==========//
            Sessions.CheckUserLogin();


            //=========== Default Settings ==========//
            if (IsPostBack == false)
            {

                //========== Fill Variables to Form ==========//
                SetLabels();

                //========== Check User Role and redirect to home page as per user role ===========//
                Fuji.Common.CommonFunctions.CheckUserRole();

                //=========== Calling the function for Binding the Grid ==========//
                BindRoleGridView();
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void GVRoles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //=========== Checking the data for null and values in the tables ==========//
                if (_DSRoleAndPermission != null && _DSRoleAndPermission.Tables.Count > 1 && _DSRoleAndPermission.Tables[1].Rows.Count > 0)
                {
                    //=========== Creating the Dataview for the Permission Table ==========//
                    DataView DVPermission = _DSRoleAndPermission.Tables[1].DefaultView;
                    DVPermission.RowFilter = "PermissionID = " + GVRoles.DataKeys[e.Row.RowIndex].Value.ToString();
                    if (DVPermission.ToTable().Rows.Count > 0)
                    {
                        for (int i = 0; i < DVPermission.ToTable().Rows.Count; i++)
                        {
                            //=========== Setting the Checkbox as per the RolePermissions ==========//
                            switch (DVPermission.ToTable().Rows[i][0].ToString())
                            {
                                case "2":
                                    CheckBox CHKCA = (CheckBox)e.Row.FindControl("ChkCA");
                                    CHKCA.Checked = true;
                                    break;
                                case "3":
                                    CheckBox CHKCMA = (CheckBox)e.Row.FindControl("ChkCMA");
                                    CHKCMA.Checked = true;
                                    break;
                                case "4":
                                    CheckBox CHKBA = (CheckBox)e.Row.FindControl("ChkBA");
                                    CHKBA.Checked = true;
                                    break;
                                case "5":
                                    CheckBox CHKPA = (CheckBox)e.Row.FindControl("ChkPA");
                                    CHKPA.Checked = true;
                                    break;
                                case "6":
                                    CheckBox CHKSA = (CheckBox)e.Row.FindControl("ChkSA");
                                    CHKSA.Checked = true;
                                    break;
                                case "7":
                                    CheckBox CHKSIA = (CheckBox)e.Row.FindControl("ChkSIA");
                                    CHKSIA.Checked = true;
                                    break;
                            }
                        }
                    }
                }

            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string _strXML = "";
            for (int i = 0; i < GVRoles.Rows.Count; i++)
            {
                CheckBox CHKCA = (CheckBox)GVRoles.Rows[i].FindControl("ChkCA");
                CheckBox CHKCMA = (CheckBox)GVRoles.Rows[i].FindControl("ChkCMA");
                CheckBox CHKBA = (CheckBox)GVRoles.Rows[i].FindControl("ChkBA");
                CheckBox CHKPA = (CheckBox)GVRoles.Rows[i].FindControl("ChkPA");
                CheckBox CHKSA = (CheckBox)GVRoles.Rows[i].FindControl("ChkSA");
                CheckBox CHKSIA = (CheckBox)GVRoles.Rows[i].FindControl("ChkSIA");

                if (CHKCA.Checked)
                {
                    if (_strXML.Trim().Length > 0)
                        _strXML += "<Permission RoleID='2' PermissionID='" + GVRoles.DataKeys[i].Value.ToString() + "'/>";
                    else
                        _strXML += "<RolePermissions><Permission RoleID='2' PermissionID='" + GVRoles.DataKeys[i].Value.ToString() + "' />";
                }
                if (CHKCMA.Checked)
                {
                    if (_strXML.Trim().Length > 0)
                        _strXML += "<Permission RoleID='3' PermissionID='" + GVRoles.DataKeys[i].Value.ToString() + "'/>";
                    else
                        _strXML += "<RolePermissions><Permission RoleID='3' PermissionID='" + GVRoles.DataKeys[i].Value.ToString() + "'/>";
                }
                if (CHKBA.Checked)
                {
                    if (_strXML.Trim().Length > 0)
                        _strXML += "<Permission RoleID='4' PermissionID='" + GVRoles.DataKeys[i].Value.ToString() + "'/>";
                    else
                        _strXML += "<RolePermissions><Permission RoleID='4' PermissionID='" + GVRoles.DataKeys[i].Value.ToString() + "'/>";
                }
                if (CHKPA.Checked)
                {
                    if (_strXML.Trim().Length > 0)
                        _strXML += "<Permission RoleID='5' PermissionID='" + GVRoles.DataKeys[i].Value.ToString() + "'/>";
                    else
                        _strXML += "<RolePermissions><Permission RoleID='5' PermissionID='" + GVRoles.DataKeys[i].Value.ToString() + "'/>";
                }
                if (CHKSA.Checked)
                {
                    if (_strXML.Trim().Length > 0)
                        _strXML += "<Permission RoleID='6' PermissionID='" + GVRoles.DataKeys[i].Value.ToString() + "'/>";
                    else
                        _strXML += "<RolePermissions><Permission RoleID='6' PermissionID='" + GVRoles.DataKeys[i].Value.ToString() + "'/>";
                }
                if (CHKSIA.Checked)
                {
                    if (_strXML.Trim().Length > 0)
                        _strXML += "<Permission RoleID='7' PermissionID='" + GVRoles.DataKeys[i].Value.ToString() + "'/>";
                    else
                        _strXML += "<RolePermissions><Permission RoleID='7' PermissionID='" + GVRoles.DataKeys[i].Value.ToString() + "'/>";
                }
            }
            if (_strXML.Trim().Length > 0)
            {
                _strXML += "</RolePermissions>";
            }
            RolePermissionData ObjRolePermission = new RolePermissionData();
            ObjRolePermission.RoleManagement = _strXML;
            int _Result = ObjRolePermission.uspInsertRolePermission();
            if (_Result == 2)
            {
                LblError.Text = "Permissions assigned sucessfully.";
                BindRoleGridView();
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    #endregion

    #region Methods and Functions
    //=========== Method for Fetching the RoleManagementData ==========//
    private DataSet GetRoleAndPermissionData()
    {
        try
        {
            //==========  Declaring Objects ==========//
            RolesData ObjRoles = new RolesData();
            _DSRoleAndPermission = ObjRoles.uspGetRoleManagementData();
            return _DSRoleAndPermission;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //=========== Binding the Role Data Grid==========//
    private void BindRoleGridView()
    {
        try
        {
            //========== Declaring Objects & Calling the function which is fetching the data from TaskerUser Table ==========//
            DataSet _DSRoles = GetRoleAndPermissionData();
            if (_DSRoles != null && _DSRoles.Tables.Count > 0 && _DSRoles.Tables[0].Rows.Count > 0)
            {
                GVRoles.DataSource = _DSRoles.Tables[0];
                GVRoles.DataBind();
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //=========== Method for Checking the Page rights ==========//
    private void CheckRights()
    {
        try
        {
            //========== Checking the Add Right for the User Management ===========//
            if (Fuji.Common.Sessions.RoleID != 1)
            {
                Fuji.Common.CommonFunctions.PageRedirect("Home", "");
                return;
            }
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
            Resources.RoleManagement.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));

            //========== Set Values ==========//
            LTRL_Title.Text = Resources.RoleManagement.LBL_Title_Text;
            LTRL_SubTitle.Text = Resources.RoleManagement.LBL_SubTitle_Text;
            BtnUpdate.Text = Resources.RoleManagement.BtnUpdate_Text;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #endregion
}
