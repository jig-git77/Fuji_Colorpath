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

public partial class AdminMain : PageBase
{
    #region Declaration
    int Count = 0;
    #endregion
    
    #region "Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Error ===========//
            LblError.Text = "";

            if (IsPostBack == false)
            {
                //========== Check Session ===========//
                Fuji.Common.Sessions.CheckUserLogin();

                //========== Check User Role and redirect to home page as per user role ===========//
                Fuji.Common.CommonFunctions.CheckUserRole();

                //========= Check Rights of the logged in user and show icons as per user role =========//
                if (Fuji.Common.Sessions.GetSession("AccessData") != null)
                {
                    //========== Get the access data for the admin panel ===========//
                    DataTable TabAccess = (DataTable)Fuji.Common.Sessions.GetSession("AccessData");
                    //========== Bind the data list with the user access tabs ===========//
                    TabAccess.DefaultView.Sort = "ID ASC";
                    DL_AccessType.DataSource = TabAccess.DefaultView;
                    DL_AccessType.DataBind();
                    //========== Find the last item for the datalist and hide it ===========//
                    HtmlTableCell TD_divider = (HtmlTableCell)DL_AccessType.Items[DL_AccessType.Items.Count - 1].FindControl("TD_divider");
                    TD_divider.Visible = false;
                }
                else { DL_AccessType.DataSource = null; DL_AccessType.DataBind(); }
            }
        }
        catch (Exception exc)
        {
            LblError.Text = HandleException(exc);
        }
    }

    protected void DL_AccessType_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            //========== Proceed with the data for the item and alternating item of the datalist ===========//
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //========== Find the divider for the item ===========//
                HtmlTableCell TD_divider = (HtmlTableCell)e.Item.FindControl("TD_divider");
                Count = Count + 1;

                //========== Check whether the datalist item and show the divider for that item ===========//
                if (DL_AccessType.Items.Count != 5)
                    TD_divider.Visible = true;

                //========== Check whether the tab is user rold management tab or not ===========//
                if (((DataRowView)e.Item.DataItem)["ID"].ToString().ToUpper().Equals("D"))
                {
                    //========== Find the role tab for that item ===========//
                    HtmlTableCell TD_Role = (HtmlTableCell)e.Item.FindControl("TD_Role");
                    //========== Check whether the SuperAdmin is logged in then show/hide tab for that item ===========//
                    if (Fuji.Common.Sessions.RoleID != 1)
                    { TD_Role.Visible = false; TD_divider.Visible = false; }
                    else
                    { TD_Role.Visible = true; TD_divider.Visible = true; }
                }
            }
        }
        catch (Exception exc)
        {
            LblError.Text = HandleException(exc);
        }
    }
    #endregion

}
