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

public partial class VideosAdvancedSearch : PageBase
{
    DataTable dtMain = null;
    DataTable dtChild = null;
    string strUserAccessCategoryIDs = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //=========== Default Settings ==========//
            if (!Page.IsPostBack)
            {
                ////=========== Check Login ==========//
                //Sessions.CheckUserLogin();

                //========== Fill Variables to Form ==========//
                SetLabels();

                //=========== Filling the User's Category==========//
                GetUserCategories();
            }
            
            //GenerateAccessCategories();
            
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
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
            Resources.VideosAdvancedSearch.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));

            //========== Set Values ==========//
            LTRL_Title.Text = Resources.VideosAdvancedSearch.LTRL_Title_Text;
            LTRL_Category.Text = Resources.VideosAdvancedSearch.LTRL_Category_Text;
            LTRL_KeyWords.Text = Resources.VideosAdvancedSearch.LTRL_KeyWords_Text;
            BTN_Search.Text = Resources.VideosAdvancedSearch.BTN_Search_Text;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion


    private void GetUserCategories()
    {
        try
        {
            CategoriesData objCategory = new CategoriesData();
            //dtMain = objCategory.uspGetCategoriesByUser(Sessions.UserID);
            dtMain = objCategory.uspGetAllCategories();
            dtChild = dtMain.Copy();

            DataView dv = dtMain.DefaultView;
            dv.RowFilter = "ParentID = 0";

            //DL_CategoryList.DataSource = dv;
            //DL_CategoryList.DataBind();

            dlCategoryList.DataSource = dtChild;
            dlCategoryList.DataBind();

            GenerateDynamicTable(dv);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void GenerateAccessCategories()
    {
        try
        {
            CategoriesData objCategory = new CategoriesData();
            DataTable dtUserCategories = objCategory.uspGetCategoriesByUser(Sessions.UserID);
            DataTable dtCategories = objCategory.uspGetAllCategories();
            dtChild = dtCategories.Copy();
            for (int i = 0; i < dtUserCategories.Rows.Count; i++)
            {
                GetCategoryID(dtCategories, int.Parse(dtUserCategories.Rows[i]["ParentID"].ToString()));
                if (strUserAccessCategoryIDs == "")
                    strUserAccessCategoryIDs = dtUserCategories.Rows[i]["CategoryID"].ToString();
                else
                    strUserAccessCategoryIDs = strUserAccessCategoryIDs + "," + dtUserCategories.Rows[i]["CategoryID"].ToString();
            }

            for (int i = 0; i < dtCategories.Rows.Count; i++)
            {
                if (dtCategories.Rows[i]["ParentID"].ToString().Equals("0"))
                {
                    dtCategories.Rows[i]["ParentID"] = DBNull.Value;
                }
            }

            DataView dv = dtCategories.DefaultView;
            dv.RowFilter = " CategoryID IN ( " + strUserAccessCategoryIDs + " ) ";

            GenerateDynamicTable(dv);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void GetCategoryID(DataTable dtCategories, int ParentID)
    {
        try
        {
            DataTable dtCategoriesCopy = dtCategories.Copy();
            DataView dv = dtCategories.DefaultView;
            dv.RowFilter = " CategoryID = " + ParentID;
            if (dv.Count > 0)
            {
                GetCategoryID(dtCategoriesCopy, int.Parse(dv[0]["ParentID"].ToString()));
                if (strUserAccessCategoryIDs == "")
                    strUserAccessCategoryIDs = dv[0]["CategoryID"].ToString();
                else
                    strUserAccessCategoryIDs = strUserAccessCategoryIDs + "," + dv[0]["CategoryID"].ToString();
            }
            return;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //protected void DL_CategoryList_ItemDataBound(object sender, DataListItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        DataRowView drv = (DataRowView)(e.Item.DataItem);

    //        CheckBox CHK_MainCategory = (CheckBox)(e.Item.FindControl("CHK_MainCategory"));
    //        CHK_MainCategory.Text = drv["Description"].ToString();

    //        DataView dv = dtChild.Copy().DefaultView;
    //        dv.RowFilter = "ParentID = " + drv["CategoryID"].ToString();
    //        CheckBoxList CHK_ChildCategory = (CheckBoxList)(e.Item.FindControl("CHK_ChildCategory"));
    //        CHK_ChildCategory.DataTextField = "Description";
    //        CHK_ChildCategory.DataValueField = "CategoryID";
    //        CHK_ChildCategory.DataSource = dv;
    //        CHK_ChildCategory.DataBind();
    //    }
    //}

    protected void BTN_Search_Click(object sender, EventArgs e)
    {
        try
        {
            //string strCategoryIDs = GetSelectedCategories();
            //for (int i = 0; i < DL_CategoryList.Items.Count; i++)
            //{
            //    CheckBox CHK_MainCategory = (CheckBox)(DL_CategoryList.Items[i].FindControl("CHK_MainCategory"));

            //    if (CHK_MainCategory.Checked == true)
            //    {
            //        if (strCategoryIDs == "")
            //            strCategoryIDs = DL_CategoryList.DataKeys[i].ToString() ;
            //        else
            //            strCategoryIDs = strCategoryIDs + "," + DL_CategoryList.DataKeys[i].ToString();
            //    }

            //    CheckBoxList CHK_ChildCategory = (CheckBoxList)(DL_CategoryList.Items[i].FindControl("CHK_ChildCategory"));
            //    for (int j = 0; j < CHK_ChildCategory.Items.Count; j++)
            //    {
            //        if (CHK_ChildCategory.Items[j].Selected == true)
            //        {
            //            if (strCategoryIDs == "")
            //                strCategoryIDs = CHK_ChildCategory.Items[j].Value.ToString();
            //            else
            //                strCategoryIDs = strCategoryIDs + "," + CHK_ChildCategory.Items[j].Value.ToString();
            //        }
            //    }
            //}

            string strCategoryIDs = "";
            foreach (DataListItem itm in dlCategoryList.Items)
            {
                CheckBox chkCategory = (CheckBox)itm.FindControl("chkCategory");
                if (chkCategory.Checked)
                {
                    Literal ltrlCategoryID = (Literal)itm.FindControl("ltrlCategoryID");
                    if (strCategoryIDs == "")
                        strCategoryIDs = ltrlCategoryID.Text.Trim();
                    else
                        strCategoryIDs = strCategoryIDs + "," + ltrlCategoryID.Text.Trim();
                }
            }

            
            

            if (strCategoryIDs == "")
            {
                Fuji.Common.Sessions.SetSessions("CategoryIDs", null);
                Fuji.Common.Sessions.SetSessions("CategoryIDsObj", null);
            }
            else
            {
                Fuji.Common.Sessions.SetSessions("CategoryIDs", strCategoryIDs);
                Fuji.Common.Sessions.SetSessions("CategoryIDsObj", strCategoryIDs);
            }

            if (TXT_KeyWords.Text.Trim().Equals(""))
            {
                Fuji.Common.Sessions.SetSessions("KeyWords", null);
                Fuji.Common.Sessions.SetSessions("KeyWordsObj", null);
            }
            else
            {
                Fuji.Common.Sessions.SetSessions("KeyWords", TXT_KeyWords.Text.Trim());
                Fuji.Common.Sessions.SetSessions("KeyWordsObj", TXT_KeyWords.Text.Trim());
            }

            Fuji.Common.Sessions.SetSessions("VideosData", null);

            Fuji.Common.Sessions.SetSessions("GridListPageIndex", null);
            Fuji.Common.Sessions.SetSessions("DataListPageIndex", null);

            string script = "<script language='javascript' type='text/javascript'>parent.hidePopup();</script>";
            Page.RegisterClientScriptBlock("closewindow", script);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void GenerateDynamicTable(DataView dv)
    {
        try
        {
            HtmlTable htTableMain = new HtmlTable();
            HtmlTableRow trMain = new HtmlTableRow();
            for (int i = 0; i < dv.Count; i++)
            {
                if (i % 2 == 0)
                    trMain = new HtmlTableRow();

                HtmlTableCell tcMain = new HtmlTableCell();
                tcMain.VAlign = "top";
                //tcMain.Style.Add("valign", "top");

                HtmlTable htTable = new HtmlTable();
                HtmlTableRow tr = new HtmlTableRow();
                HtmlTableCell tc = new HtmlTableCell();
                CheckBox chk = new CheckBox();
                chk.ID = "chk" + dv[i]["CategoryID"].ToString();
                //if (chk.Checked == true)
                //{
                //    Response.Write(chk.Attributes["value"].ToString());
                //}
                chk.Text = dv[i]["Description"].ToString();
                chk.Attributes.Add("value", dv[i]["CategoryID"].ToString());
                tc.Controls.Add(chk);
                //tc.Style.Add("valign", "top");
                tc.VAlign = "top";
                tr.Cells.Add(tc);
                htTable.Rows.Add(tr);
                GenerateChildTable(int.Parse(dv[i]["CategoryID"].ToString()), dtChild, htTable, "20");

                tcMain.Controls.Add(htTable);
                trMain.Cells.Add(tcMain);

                if (i % 2 == 1)
                    htTableMain.Controls.Add(trMain);
            }
            divDynamicCategories.Controls.Add(htTableMain);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void GenerateChildTable(int ParentID, DataTable dtChild,HtmlTable htTable, string Spacing )
    {
        try
        {
            DataTable dtChildCopy = dtChild.Copy();
            DataView dvChildCopy = dtChildCopy.DefaultView;
            //dvChildCopy.RowFilter = " CategoryID IN ( " + strUserAccessCategoryIDs + " ) ";
            dvChildCopy.RowFilter = "ParentID = " + ParentID;
            //dvChildCopy.RowFilter = " CategoryID IN ( " + strUserAccessCategoryIDs + " ) AND ParentID = " + ParentID;
            if (dvChildCopy.Count > 0)
            {
                for (int i = 0; i < dvChildCopy.Count; i++)
                {
                    HtmlTableRow tr = new HtmlTableRow();
                    HtmlTableCell tc = new HtmlTableCell();
                    tc.Style.Add("padding-left", Spacing + "px");
                    CheckBox chk = new CheckBox();
                    //if (chk.Checked == true)
                    //{
                    //    Response.Write(chk.Attributes["value"].ToString());
                    //}
                    chk.ID = "chk" + dvChildCopy[i]["CategoryID"].ToString();
                    chk.Text = dvChildCopy[i]["Description"].ToString();
                    chk.Attributes.Add("value", dvChildCopy[i]["CategoryID"].ToString());
                    tc.Controls.Add(chk);
                    tr.Cells.Add(tc);
                    htTable.Rows.Add(tr);
                    GenerateChildTable(int.Parse(dvChildCopy[i]["CategoryID"].ToString()), dtChild, htTable, Convert.ToString(int.Parse(Spacing) + 20));
                }
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private string GetSelectedCategories()
    {
        try
        {
            string strCategoryIDs = "";
            for (int i = 0; i < dtChild.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)(divDynamicCategories.FindControl("chk" + dtChild.Rows[i]["CategoryID"].ToString()));
                if (chk != null && chk.Checked == true)
                {
                    if (strCategoryIDs == "")
                        strCategoryIDs = chk.Attributes["value"].ToString();
                    else
                        strCategoryIDs = strCategoryIDs + "," + chk.Attributes["value"].ToString();
                }
            }
            return strCategoryIDs;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}
