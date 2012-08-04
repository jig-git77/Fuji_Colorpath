using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{

    protected void btn_DownloadPDF_Click(object sender, EventArgs e)
    {

        if (ddlCheckList.SelectedIndex > 0)
        {

            string path = Server.MapPath("PDF/" + ddlCheckList.SelectedValue.ToString());

            try
            {
                System.IO.FileStream fs = System.IO.File.OpenRead(path);
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.AddHeader("Content-Type", "application/*");
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + ddlCheckList.SelectedValue.ToString());
                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                fs.Close();
                System.Web.HttpContext.Current.Response.End();
            }
            catch
            {

            }

        }
        else
        {

            return;
        }

    }

   

    
    private void FillCheckList(int _CheckListType)
    {
        try
        {

            //========== Clear Items in DropDownList ===========//
            ddlCheckList.Items.Clear();
            DataTable DT_CheckList = new DataTable();
            BusinessLogic objBJ = new BusinessLogic();
            DT_CheckList = objBJ.GetCheckList(_CheckListType);
            if (DT_CheckList != null && DT_CheckList.Rows.Count > 0)
            {
                ddlCheckList.DataSource = DT_CheckList;
                ddlCheckList.DataValueField = "DownloadPath"; ddlCheckList.DataTextField = "CheckListName";
                ddlCheckList.DataBind();

            }
            else
            { ddlCheckList.DataSource = null; ddlCheckList.DataBind(); }

            ddlCheckList.Items.Insert(0, "--Select");
        }
        catch
        {

        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    protected void ddlChecklistType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlChecklistType.SelectedIndex != 0)
        {
            FillCheckList(Convert.ToInt32(ddlChecklistType.SelectedValue.Trim()));
        }
        else
        {
            FillCheckList(0);
        }
    }

    
}
