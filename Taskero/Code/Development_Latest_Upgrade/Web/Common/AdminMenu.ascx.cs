#region Name Space
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

public partial class Common_AdminMenu : System.Web.UI.UserControl
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string URL = Request.Url.ToString();

            if (URL.IndexOf("AdminMain.aspx") < 0)
            {


                //========= Check Rights of the logged in user and show icons as per user role =========//
                if (Fuji.Common.Sessions.GetSession("AccessData") != null)
                {
                    DataTable TabAccess = new DataTable();
                    TabAccess = (DataTable)Fuji.Common.Sessions.GetSession("AccessData");
                    TabAccess.DefaultView.Sort = "ID ASC";
                    DL_AccessType.DataSource = TabAccess.DefaultView;
                    DL_AccessType.DataBind();
                }
                else { DL_AccessType.DataSource = null; DL_AccessType.DataBind(); }
            }
        }
        catch (Exception exc)
        {
            exc.Message.ToString();
        }
    }
    protected void DL_AccessType_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        //========== Proceed with the data for the item and alternating item of the datalist ===========//
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //========== Find the role tab for that item ===========//
            HtmlAnchor ANC_Tab = (HtmlAnchor)e.Item.FindControl("ANC_Tab");
            ANC_Tab.HRef = "../" + ((DataRowView)e.Item.DataItem)["Page"].ToString();
            //========== Check whether the tab is user rold management tab or not ===========//
            if (((DataRowView)e.Item.DataItem)["ID"].ToString().ToUpper().Equals("D"))
            {
                //========== Check whether the SuperAdmin is logged in then show/hide tab for that item ===========//

                if (Fuji.Common.Sessions.RoleID != 1)
                    ANC_Tab.Visible = false;
                else
                    ANC_Tab.Visible = true;
            }
        }
    }
    #endregion


}
