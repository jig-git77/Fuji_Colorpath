using Oris.Business;
using Oris.Common;
using OrisWeb.Models;
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace OrisWeb
{
    public partial class Videos : PageBase
    {
        #region Declaration
        //========== Declare the constant page size for the grid ===========//
        private const int ConstDefaultPageSizeForGridList = 2;
        private const int ConstDefaultPageSizeForDataList = 8;
        private const int ConstDefaultPageSizeForRecently = 7;

        DataTable dtMain = null;
        DataTable dtChild = null;
        string strUserAccessCategoryIDs = "";
        #endregion

        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //=========== Clear Error ==========//
                //LblError.Text = "";

                //=========== Default Settings ==========//
                if (!Page.IsPostBack)
                {



                    ////=========== Check Login ==========//
                    Sessions.CheckUserLogin();

                    //========== Fill Variables to Form ==========//
                    SetLabels();


                    //
                    // === Initialise All sessions as null when page is load first time === //
                    Sessions.SetSessions("UserCategoriesData", null);
                    Sessions.SetSessions("GridListPageIndex", null);
                    Sessions.SetSessions("DataListPageIndex", null);
                    Sessions.SetSessions("CategoryIDs", null);
                    Sessions.SetSessions("KeyWords", null);

                    //====== This 2 sessions are for Filtering in data in object/Rotate(First) view ======//
                    Sessions.SetSessions("CategoryIDsObj", null);
                    Sessions.SetSessions("KeyWordsObj", null);

                    Sessions.SetSessions("VideosData", null);
                    Sessions.SetSessions("RecentlyPageIndex", null);
                    Sessions.SetSessions("RecentlyVideosDataCount", null);
                    //=========== Filling the User's Category==========//
                    GetUserCategories();
                    GetCategories();

                    BindListVideos();
                    //=========== Recently Viewed Videos =============//
                    BindRecentlyVideos();

                    //rtv_CategoryList.ExpandAllNodes();

                }
                BTN_AdvanceSearch.OnClientClick = "return OpenAdvanceSearch('VideosAdvancedSearch.aspx');";
                //RadSliderVideo.Value = 100;
            }
            catch (Exception Exc)
            {
                LBL_Error.Text = HandleException(Exc);
            }
        }
        #endregion

        #region Methods and Events
        #region Set Labels
        /// <summary>
        /// Used to set the labels of the form
        /// </summary>
        private void SetLabels()
        {
            try
            {
                //========== Set Culture ==========//
                Resources.Message.Culture = (new System.Globalization.CultureInfo(Sessions.Language));
                Resources.Videos.Culture = (new System.Globalization.CultureInfo(Sessions.Language));

                //========== Set Values ==========//
                LTRL_ViewVideos.Text = Resources.Videos.LTRL_ViewVideos_Text;

                LTRL_RecentlyViewed.Text = Resources.Videos.LTRL_RecentlyViewed_Text;
                BTN_Search.Text = Resources.Videos.BTN_Search_Text;
                BTN_ListPrev.Text = Resources.Videos.BTN_ListPrev_Text;
                BTN_ListNext.Text = Resources.Videos.BTN_ListNext_Text;
                BTN_GridPrev.Text = Resources.Videos.BTN_GridPrev_Text;
                BTN_GridNext.Text = Resources.Videos.BTN_GridNext_Text;
                //BTN_AdvanceSearch.Text = Resources.Videos.BTN_AdvanceSearch_Text;
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
                DataTable DT_Categories = null;
                if (Sessions.GetSession("UserCategoriesData") == null)
                {
                    //DT_Categories = objCategory.uspGetCategoriesByUser(Sessions.UserID);
                    DT_Categories = objCategory.uspGetAllCategories();
                    Sessions.SetSessions("UserCategoriesData", DT_Categories);
                }
                if (DT_Categories == null)
                {
                    DT_Categories = (DataTable)(Sessions.GetSession("UserCategoriesData"));
                }

                DataTable dtCategory = DT_Categories.Copy();
                for (int i = 0; i < dtCategory.Rows.Count; i++)
                {
                    if (dtCategory.Rows[i]["ParentID"].ToString().Equals("0"))
                    {
                        dtCategory.Rows[i]["ParentID"] = DBNull.Value;
                    }
                }
                //rtv_CategoryList.DataFieldID ="CategoryID";
                //rtv_CategoryList.DataFieldParentID = "ParentID";
                //rtv_CategoryList.DataValueField = "CategoryID";
                //rtv_CategoryList.DataTextField = "Description";
                ////DataFieldID="CategoryID" DataFieldParentID="ParentID" DataValueField="CategoryID" DataTextField="Description"
                //rtv_CategoryList.DataSource = dtCategory;
                //rtv_CategoryList.DataBind();

                DataView dv = DT_Categories.DefaultView;
                dv.RowFilter = "ParentID = 0";

                Chk_CategoryList.DataTextField = "Description";
                Chk_CategoryList.DataValueField = "CategoryID";
                Chk_CategoryList.DataSource = dv;
                Chk_CategoryList.DataBind();

                GenerateAccessCategories();
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
                //DataTable dtCategories = objCategory.uspGetAllCategories();
                //for (int i = 0; i < dtUserCategories.Rows.Count; i++)
                //{
                //    GetCategoryID(dtCategories, int.Parse(dtUserCategories.Rows[i]["ParentID"].ToString()));
                //    if(strUserAccessCategoryIDs == "")
                //        strUserAccessCategoryIDs = dtUserCategories.Rows[i]["CategoryID"].ToString();
                //    else
                //        strUserAccessCategoryIDs = strUserAccessCategoryIDs + "," + dtUserCategories.Rows[i]["CategoryID"].ToString();
                //}

                //for (int i = 0; i < dtCategories.Rows.Count; i++)
                //{
                //    if (dtCategories.Rows[i]["ParentID"].ToString().Equals("0"))
                //    {
                //        dtCategories.Rows[i]["ParentID"] = DBNull.Value;
                //    }
                //}

                //DataView dv = dtCategories.DefaultView;
                //if (strUserAccessCategoryIDs.Trim() != "")
                //    dv.RowFilter = " CategoryID IN ( " + strUserAccessCategoryIDs + " ) ";

                //rtv_CategoryList.DataFieldID = "CategoryID";
                //rtv_CategoryList.DataFieldParentID = "ParentID";
                //rtv_CategoryList.DataValueField = "CategoryID";
                //rtv_CategoryList.DataTextField = "Description";
                //rtv_CategoryList.DataSource = dtUserCategories;
                //rtv_CategoryList.DataBind();

                dlCategoryList.DataSource = dtUserCategories;
                dlCategoryList.DataBind();
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
        private void BindGridVideos()
        {
            try
            {
                //==========  Declaring Objects ==========//
                VideosData objVideo = new VideosData();
                //==========  Get Data Table and Bind ==========//
                DataTable DT_Videos = new DataTable();
                if (Sessions.GetSession("VideosData") == null)
                {
                    //DT_Videos = objVideo.uspGetAllVideos();
                    DT_Videos = objVideo.uspGetAllVideosByUserID(Sessions.UserID);
                    Sessions.SetSessions("VideosData", DT_Videos);
                }
                if (Sessions.GetSession("GridListPageIndex") == null)
                {
                    Sessions.SetSessions("GridListPageIndex", 0);
                }

                DataTable DT_Filter = null;
                if (Sessions.GetSession("CategoryIDs") != null && Convert.ToString(Sessions.GetSession("CategoryIDs")).Trim() != "")
                {
                    DT_Videos = (DataTable)Sessions.GetSession("VideosData");
                    DataView dv = DT_Videos.DefaultView;
                    if (Sessions.GetSession("KeyWords") != null && Convert.ToString(Sessions.GetSession("KeyWords")) != "")
                        dv.RowFilter = "CategoryID IN (" + Sessions.GetSession("CategoryIDs").ToString() + ") AND Keywords LIKE '%" + Sessions.GetSession("KeyWords").ToString() + "%'";
                    else
                        dv.RowFilter = "CategoryID IN (" + Sessions.GetSession("CategoryIDs").ToString() + ")";

                    DT_Filter = CreateTable(dv);
                    Sessions.SetSessions("VideosData", DT_Filter);
                    Sessions.SetSessions("CategoryIDs", null);
                }
                else if (Sessions.GetSession("KeyWords") != null && Convert.ToString(Sessions.GetSession("KeyWords")) != "")
                {
                    DT_Videos = (DataTable)Sessions.GetSession("VideosData");
                    DataView dv = DT_Videos.DefaultView;
                    dv.RowFilter = "Keywords LIKE '%" + Sessions.GetSession("KeyWords").ToString() + "%'";

                    DT_Filter = CreateTable(dv);
                    Sessions.SetSessions("VideosData", DT_Filter);
                    Sessions.SetSessions("CategoryIDs", null);
                }

                //========== Get Categories data from the session ===========//
                DT_Videos = (DataTable)Sessions.GetSession("VideosData");

                int CurrentPageIndex = (int)(Sessions.GetSession("GridListPageIndex"));

                int intStartPosition = (CurrentPageIndex * ConstDefaultPageSizeForGridList);
                int intEndPosition = (CurrentPageIndex * ConstDefaultPageSizeForGridList) + ConstDefaultPageSizeForGridList;
                if (intEndPosition > DT_Videos.Rows.Count)
                    intEndPosition = DT_Videos.Rows.Count;

                DataTable DT_Temp = DT_Videos.Clone();

                if (DT_Videos.Rows.Count != 0)
                {
                    for (int i = intStartPosition; i < intEndPosition; i++)
                    {
                        DT_Temp.ImportRow(DT_Videos.Rows[i]);
                    }

                    DL_Videos3.DataSource = DT_Temp;
                    DL_Videos3.DataBind();
                    DL_Videos3.Visible = true;
                    BTN_GridNext.Visible = true;
                    BTN_GridPrev.Visible = true;
                    //divViewVideos3.Visible = true;
                }
                else
                {
                    DL_Videos3.DataSource = null;
                    DL_Videos3.DataBind();
                    DL_Videos3.Visible = false;
                    BTN_GridNext.Visible = false;
                    BTN_GridPrev.Visible = false;
                    //divViewVideos3.Visible = false;
                }
                SetNavigationButton(intStartPosition, intEndPosition, DT_Videos.Rows.Count, BTN_GridPrev, BTN_GridNext);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void BindListVideos()
        {
            try
            {
                //==========  Declaring Objects ==========//
                VideosData objVideo = new VideosData();
                //==========  Get Data Table and Bind ==========//
                DataTable DT_Videos = new DataTable();
                if (Sessions.GetSession("VideosData") == null)
                {
                    //DT_Videos = objVideo.uspGetAllVideos();
                    DT_Videos = objVideo.uspGetAllVideosByUserID(Sessions.UserID);
                    Sessions.SetSessions("VideosData", DT_Videos);
                }
                if (Sessions.GetSession("DataListPageIndex") == null)
                {
                    Sessions.SetSessions("DataListPageIndex", 0);
                }

                DataTable DT_Filter = null;
                if (Sessions.GetSession("CategoryIDs") != null && Convert.ToString(Sessions.GetSession("CategoryIDs")) != "")
                {
                    DT_Videos = (DataTable)Sessions.GetSession("VideosData");
                    DataView dv = DT_Videos.DefaultView;
                    if (Sessions.GetSession("KeyWords") != null && Convert.ToString(Sessions.GetSession("KeyWords")) != "")
                        dv.RowFilter = "CategoryID IN (" + Sessions.GetSession("CategoryIDs").ToString() + ") AND Keywords LIKE '%" + Sessions.GetSession("KeyWords").ToString() + "%'";
                    else
                        dv.RowFilter = "CategoryID IN (" + Sessions.GetSession("CategoryIDs").ToString() + ")";

                    DT_Filter = CreateTable(dv);
                    Sessions.SetSessions("VideosData", DT_Filter);
                    Sessions.SetSessions("CategoryIDs", null);
                }
                else if (Sessions.GetSession("KeyWords") != null && Convert.ToString(Sessions.GetSession("KeyWords")) != "")
                {
                    DT_Videos = (DataTable)Sessions.GetSession("VideosData");
                    DataView dv = DT_Videos.DefaultView;
                    dv.RowFilter = "Keywords LIKE '%" + Sessions.GetSession("KeyWords").ToString() + "%'";

                    DT_Filter = CreateTable(dv);
                    Sessions.SetSessions("VideosData", DT_Filter);
                    Sessions.SetSessions("CategoryIDs", null);
                }

                //========== Get Categories data from the session ===========//
                DT_Videos = (DataTable)Sessions.GetSession("VideosData");

                int CurrentPageIndex = (int)(Sessions.GetSession("DataListPageIndex"));

                int intStartPosition = (CurrentPageIndex * ConstDefaultPageSizeForDataList);
                int intEndPosition = (CurrentPageIndex * ConstDefaultPageSizeForDataList) + ConstDefaultPageSizeForDataList;
                if (intEndPosition > DT_Videos.Rows.Count)
                    intEndPosition = DT_Videos.Rows.Count;

                DataTable DT_Temp = DT_Videos.Clone();

                if (DT_Videos.Rows.Count != 0)
                {
                    for (int i = intStartPosition; i < intEndPosition; i++)
                    {
                        DT_Temp.ImportRow(DT_Videos.Rows[i]);
                    }
                    DL_Videos2.DataSource = DT_Temp;
                    DL_Videos2.DataBind();
                    DL_Videos2.Visible = true;
                    BTN_ListNext.Visible = true;
                    BTN_ListPrev.Visible = true;
                    //divViewVideos2.Visible = true;
                }
                else
                {
                    DL_Videos2.DataSource = null;
                    DL_Videos2.DataBind();
                    DL_Videos2.Visible = false;
                    BTN_ListNext.Visible = false;
                    BTN_ListPrev.Visible = false;
                    //divViewVideos2.Visible = false;
                }
                SetNavigationButton(intStartPosition, intEndPosition, DT_Videos.Rows.Count, BTN_ListPrev, BTN_ListNext);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void SetNavigationButton(int intStartPosition, int intEndPosition, int intTotal, Button Pre, Button Next)
        {
            try
            {
                // === Set Next Previouse Buttons Visible trye/false based on data available === //
                Pre.Visible = (intStartPosition == 0 ? false : true);
                Next.Visible = (intEndPosition >= intTotal ? false : true);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void SetNavigationButton(int intStartPosition, int intEndPosition, int intTotal, ImageButton Pre, ImageButton Next)
        {
            try
            {
                // === Set Next Previouse Buttons Visible trye/false based on data available === //
                Pre.Visible = (intStartPosition == 0 ? false : true);
                Next.Visible = (intEndPosition >= intTotal ? false : true);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        protected void BTN_GridPrev_Click(object sender, EventArgs e)
        {
            try
            {
                int intPrev = (int)(Sessions.GetSession("GridListPageIndex"));
                intPrev = intPrev - 1;
                DataTable DT_Videos = (DataTable)Sessions.GetSession("VideosData");
                int intTotalPage = (DT_Videos.Rows.Count / ConstDefaultPageSizeForGridList);
                if ((DT_Videos.Rows.Count % ConstDefaultPageSizeForGridList) > 0)
                    intTotalPage = intTotalPage + 1;
                if (intPrev <= 0)
                    intPrev = 0;
                Sessions.SetSessions("GridListPageIndex", intPrev);

                BindGridVideos();
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }
        protected void BTN_GridNext_Click(object sender, EventArgs e)
        {
            try
            {
                int intNext = (int)(Sessions.GetSession("GridListPageIndex"));
                intNext = intNext + 1;
                DataTable DT_Videos = (DataTable)Sessions.GetSession("VideosData");
                int intTotalPage = (DT_Videos.Rows.Count / ConstDefaultPageSizeForGridList);
                if ((DT_Videos.Rows.Count % ConstDefaultPageSizeForGridList) > 0)
                    intTotalPage = intTotalPage + 1;
                if (intNext >= intTotalPage)
                    intNext = intTotalPage - 1;
                Sessions.SetSessions("GridListPageIndex", intNext);

                BindGridVideos();
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }
        protected void BTN_ListPrev_Click(object sender, EventArgs e)
        {
            try
            {
                int intPrev = (int)(Sessions.GetSession("DataListPageIndex"));
                intPrev = intPrev - 1;
                DataTable DT_Videos = (DataTable)Sessions.GetSession("VideosData");
                int intTotalPage = (DT_Videos.Rows.Count / ConstDefaultPageSizeForDataList);
                if ((DT_Videos.Rows.Count % ConstDefaultPageSizeForDataList) > 0)
                    intTotalPage = intTotalPage + 1;
                if (intPrev <= 0)
                    intPrev = 0;
                Sessions.SetSessions("DataListPageIndex", intPrev);

                BindListVideos();
                //RadSliderVideo.Value = 100;
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }
        protected void BTN_ListNext_Click(object sender, EventArgs e)
        {
            try
            {
                int intNext = (int)(Sessions.GetSession("DataListPageIndex"));
                intNext = intNext + 1;
                DataTable DT_Videos = (DataTable)Sessions.GetSession("VideosData");
                int intTotalPage = (DT_Videos.Rows.Count / ConstDefaultPageSizeForDataList);
                if ((DT_Videos.Rows.Count % ConstDefaultPageSizeForDataList) > 0)
                    intTotalPage = intTotalPage + 1;
                if (intNext >= intTotalPage)
                    intNext = intTotalPage - 1;
                Sessions.SetSessions("DataListPageIndex", intNext);

                BindListVideos();
                //RadSliderVideo.Value = 100;
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }

        public string GetVideoPath(string strID, string strFileName)
        {
            try
            {
                return "Videos/VideosThumb/" + strID + System.IO.Path.GetExtension(strFileName);
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        // === To Move On First Tab(SlidShow View) === //
        protected void ibtnSlideShowView_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ibtnSlideShowView.ImageUrl = "~/Images/icon_slide_show_on.gif";
                ibtnThumbView.ImageUrl = "~/Images/icon_thumb_view_off.gif";
                ibtnGridView.ImageUrl = "~/Images/icon_grid_view_off.gif"; ;
                divViewVideos1.Visible = true;
                divViewVideos2.Visible = false;
                divViewVideos3.Visible = false;
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }
        // === To Move On Secong Tab(Thumb View) === //
        protected void ibtnThumbView_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ibtnSlideShowView.ImageUrl = "~/Images/icon_slide_show_off.gif";
                ibtnThumbView.ImageUrl = "~/Images/icon_thumb_view_on.gif";
                ibtnGridView.ImageUrl = "~/Images/icon_grid_view_off.gif";


                divViewVideos1.Visible = false;
                divViewVideos2.Visible = true;
                divViewVideos3.Visible = false;
                BindListVideos();
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }
        // === To Move On Third Tab(Grid View) === //
        protected void ibtnGridView_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ibtnSlideShowView.ImageUrl = "~/Images/icon_slide_show_off.gif";
                ibtnThumbView.ImageUrl = "~/Images/icon_thumb_view_off.gif";
                ibtnGridView.ImageUrl = "~/Images/icon_grid_view_on.gif";
                divViewVideos1.Visible = false;
                divViewVideos2.Visible = false;
                divViewVideos3.Visible = true;
                BindGridVideos();
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }

        protected void UP_Video_Load(object sender, EventArgs e)
        {
            try
            {
                if (divViewVideos3.Visible == true)
                    BindGridVideos();
                else if (divViewVideos2.Visible == true)
                    BindListVideos();

                BindRecentlyVideos();
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }

        protected void BTN_ClearSearch_Click(object sender, EventArgs e)
        {
            try
            {
                TXT_Search.Text = "";
                foreach (DataListItem itm in dlCategoryList.Items)
                    ((CheckBox)itm.FindControl("chkCategory")).Checked = false;

                Sessions.SetSessions("CategoryIDs", null);
                Sessions.SetSessions("KeyWords", null);

                Sessions.SetSessions("CategoryIDsObj", null);
                Sessions.SetSessions("KeyWordsObj", null);

                Sessions.SetSessions("VideosData", null);

                Sessions.SetSessions("GridListPageIndex", null);
                Sessions.SetSessions("DataListPageIndex", null);

                if (divViewVideos3.Visible == true)
                    BindGridVideos();
                else if (divViewVideos2.Visible == true)
                    BindListVideos();

            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }
        protected void BTN_Search_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DT_Categories = (DataTable)(Sessions.GetSession("UserCategoriesData"));
                string strCategoryIDs = "";

                // === using CheckBoxList === //
                //strCategoryIDs = "";
                //for (int i = 0; i < Chk_CategoryList.Items.Count; i++)
                //{
                //    if (Chk_CategoryList.Items[i].Selected == true)
                //    {
                //        if(strCategoryIDs == "")
                //            strCategoryIDs = Chk_CategoryList.Items[i].Value;
                //        else
                //            strCategoryIDs = strCategoryIDs + "," + Chk_CategoryList.Items[i].Value;

                //        string strChildCategoryIDs = GetChildCategoryIDs(int.Parse(Chk_CategoryList.Items[i].Value), DT_Categories);
                //        if (strChildCategoryIDs != "")
                //        {
                //            if (strCategoryIDs == "")
                //                strCategoryIDs = Chk_CategoryList.Items[i].Value;
                //            else
                //                strCategoryIDs = strCategoryIDs + "," + strChildCategoryIDs;
                //        }
                //        //DataView dv = DT_Categories.DefaultView;
                //        //dv.RowFilter = "ParentID = " + Chk_CategoryList.Items[i].Value;
                //        //if (dv.Count > 0)
                //        //{
                //        //    for (int j = 0; j < dv.Count; j++)
                //        //    {
                //        //        if (strCategoryIDs == "")
                //        //            strCategoryIDs = dv[j]["CategoryID"].ToString();
                //        //        else
                //        //            strCategoryIDs = strCategoryIDs + "," + dv[j]["CategoryID"].ToString();
                //        //    }
                //        //}
                //        //dv.RowFilter = "";
                //    }
                //}


                //// === This is just using by treeview, Category and subCategory === //
                //strCategoryIDs = "";
                //for (int i = 0; i < rtv_CategoryList.Nodes.Count; i++)
                //{
                //    if (rtv_CategoryList.Nodes[i].Checked == true)
                //    {
                //        if (strCategoryIDs == "")
                //            strCategoryIDs = rtv_CategoryList.Nodes[i].Value;
                //        else
                //            strCategoryIDs = strCategoryIDs + "," + rtv_CategoryList.Nodes[i].Value;
                //    }
                //    if (rtv_CategoryList.Nodes[i].Nodes.Count > 0)
                //    {
                //        string strChildCategoryIDs = GetChildNodeCategoryIDs(rtv_CategoryList.Nodes[i]);
                //        if (strChildCategoryIDs != "")
                //        {
                //            if (strCategoryIDs == "")
                //                strCategoryIDs = rtv_CategoryList.Nodes[i].Value;
                //            else
                //                strCategoryIDs = strCategoryIDs + "," + strChildCategoryIDs;
                //        }
                //    }
                //}


                // === Using DataList, and only those categoryId which are assigned === //
                strCategoryIDs = "";
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
                    Sessions.SetSessions("CategoryIDs", null);
                    Sessions.SetSessions("KeyWords", null);

                    Sessions.SetSessions("CategoryIDsObj", null);
                    Sessions.SetSessions("KeyWordsObj", null);

                }
                else
                {
                    Sessions.SetSessions("CategoryIDs", strCategoryIDs);
                    Sessions.SetSessions("CategoryIDsObj", strCategoryIDs);
                    if (TXT_Search.Text.Trim().Equals(""))
                    {
                        Sessions.SetSessions("KeyWords", null);
                        Sessions.SetSessions("KeyWordsObj", null);
                    }
                    else
                    {
                        Sessions.SetSessions("KeyWords", TXT_Search.Text.Trim());
                        Sessions.SetSessions("KeyWordsObj", TXT_Search.Text.Trim());
                    }
                }

                //if(TXT_Search.Text.Trim().Equals(""))
                //    Sessions.SetSessions("KeyWords", null);
                //else
                //    Sessions.SetSessions("KeyWords", TXT_Search.Text.Trim());

                //if(strCategoryIDs == "" )
                Sessions.SetSessions("VideosData", null);

                Sessions.SetSessions("GridListPageIndex", null);
                Sessions.SetSessions("DataListPageIndex", null);

                if (divViewVideos3.Visible == true)
                    BindGridVideos();
                else if (divViewVideos2.Visible == true)
                    BindListVideos();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private string GetChildNodeCategoryIDs(RadTreeNode node)
        {
            try
            {
                string strCategoryIDs = "";
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    if (node.Nodes[i].Checked == true)
                    {
                        if (strCategoryIDs == "")
                            strCategoryIDs = node.Nodes[i].Value;
                        else
                            strCategoryIDs = strCategoryIDs + "," + node.Nodes[i].Value;
                    }
                    if (node.Nodes[i].Nodes.Count > 0)
                    {
                        string strChildCategoryIDs = GetChildNodeCategoryIDs(node.Nodes[i]);
                        if (strChildCategoryIDs != "")
                            strCategoryIDs = strCategoryIDs + "," + strChildCategoryIDs;
                    }
                }
                return strCategoryIDs;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private string GetChildCategoryIDs(int ParentID, DataTable DT_Categories)
        {
            try
            {
                DataTable DT_CategoriesCopy = DT_Categories.Copy();
                DataView dv = DT_Categories.DefaultView;
                dv.RowFilter = "ParentID = " + ParentID;
                string strCategoryIDs = "";
                if (dv.Count > 0)
                {
                    for (int i = 0; i < dv.Count; i++)
                    {
                        if (strCategoryIDs == "")
                            strCategoryIDs = dv[i]["CategoryID"].ToString();
                        else
                            strCategoryIDs = strCategoryIDs + "," + dv[i]["CategoryID"].ToString();

                        string strChildCategoryIDs = GetChildCategoryIDs(int.Parse(dv[0]["CategoryID"].ToString()), DT_CategoriesCopy);
                        if (strChildCategoryIDs != "")
                            strCategoryIDs = strCategoryIDs + "," + strChildCategoryIDs;
                    }
                    //if (!dv[0]["ParentID"].ToString().Equals("0"))
                    //{
                    //    //dv.RowFilter = "";
                    //    string strDesc = dv[0]["Description"].ToString();
                    //    strDescription = GetParentDesription(int.Parse(dv[0]["ParentID"].ToString()), DT_CategoriesCopy) + " > " + strDesc;
                    //}
                    //else
                    //    strDescription = dv[0]["Description"].ToString();
                }
                return strCategoryIDs;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public DataTable CreateTable(DataView obDataView)
        {
            try
            {
                DataTable obNewDt = obDataView.Table.Clone();
                int idx = 0;
                string[] strColNames = new string[obNewDt.Columns.Count];
                foreach (DataColumn col in obNewDt.Columns)
                {
                    strColNames[idx++] = col.ColumnName;
                }

                IEnumerator viewEnumerator = obDataView.GetEnumerator();
                while (viewEnumerator.MoveNext())
                {
                    DataRowView drv = (DataRowView)viewEnumerator.Current;
                    DataRow dr = obNewDt.NewRow();
                    try
                    {
                        foreach (string strName in strColNames)
                        {
                            dr[strName] = drv[strName];
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    obNewDt.Rows.Add(dr);
                }

                return obNewDt;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void GetCategories()
        {
            try
            {
                CategoriesData objCategory = new CategoriesData();
                dtMain = objCategory.uspGetCategoriesByUser(Sessions.UserID);
                dtChild = dtMain.Copy();

                DataView dv = dtMain.DefaultView;
                dv.RowFilter = "ParentID = 0";

                DL_CategoryList.DataSource = dv;
                DL_CategoryList.DataBind();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        protected void DL_CategoryList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView drv = (DataRowView)(e.Item.DataItem);

                    CheckBox CHK_MainCategory = (CheckBox)(e.Item.FindControl("CHK_MainCategory"));
                    CHK_MainCategory.Text = drv["Description"].ToString();

                    DataView dv = dtChild.Copy().DefaultView;
                    dv.RowFilter = "ParentID = " + drv["CategoryID"].ToString();
                    CheckBoxList CHK_ChildCategory = (CheckBoxList)(e.Item.FindControl("CHK_ChildCategory"));
                    CHK_ChildCategory.DataTextField = "Description";
                    CHK_ChildCategory.DataValueField = "CategoryID";
                    CHK_ChildCategory.DataSource = dv;
                    CHK_ChildCategory.DataBind();
                }
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {

        }

        protected void BTN_AdvancedSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strCategoryIDs = "";
                for (int i = 0; i < DL_CategoryList.Items.Count; i++)
                {
                    CheckBox CHK_MainCategory = (CheckBox)(DL_CategoryList.Items[i].FindControl("CHK_MainCategory"));

                    if (CHK_MainCategory.Checked == true)
                    {
                        if (strCategoryIDs == "")
                            strCategoryIDs = DL_CategoryList.DataKeys[i].ToString();
                        else
                            strCategoryIDs = strCategoryIDs + "," + DL_CategoryList.DataKeys[i].ToString();
                    }

                    CheckBoxList CHK_ChildCategory = (CheckBoxList)(DL_CategoryList.Items[i].FindControl("CHK_ChildCategory"));
                    for (int j = 0; j < CHK_ChildCategory.Items.Count; j++)
                    {
                        if (CHK_ChildCategory.Items[j].Selected == true)
                        {
                            if (strCategoryIDs == "")
                                strCategoryIDs = CHK_ChildCategory.Items[j].Value.ToString();
                            else
                                strCategoryIDs = strCategoryIDs + "," + CHK_ChildCategory.Items[j].Value.ToString();
                        }
                    }
                }

                if (strCategoryIDs == "")
                    Sessions.SetSessions("CategoryIDs", null);
                else
                    Sessions.SetSessions("CategoryIDs", strCategoryIDs);

                if (TXT_KeyWords.Text.Trim().Equals(""))
                    Sessions.SetSessions("KeyWords", null);
                else
                    Sessions.SetSessions("KeyWords", TXT_KeyWords.Text.Trim());

                Sessions.SetSessions("GridListPageIndex", null);
                Sessions.SetSessions("DataListPageIndex", null);

                if (divViewVideos3.Visible == true)
                    BindGridVideos();
                else if (divViewVideos2.Visible == true)
                    BindListVideos();
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }

        private void BindRecentlyVideos()
        {
            try
            {
                //==========  Declaring Objects ==========//
                RecentlyVideosData objVideo = new RecentlyVideosData();
                objVideo.TaskeroUserID = Sessions.UserID;
                //==========  Get Data Table and Bind ==========//
                DataTable DT_Videos = new DataTable();
                //if (Sessions.GetSession("RecentlyVideosData") == null)
                //{
                DT_Videos = objVideo.uspGetAllRecentlyVideoByUserID();
                //Sessions.SetSessions("RecentlyVideosData", DT_Videos);
                Sessions.SetSessions("RecentlyVideosDataCount", DT_Videos.Rows.Count);
                //}
                if (Sessions.GetSession("RecentlyPageIndex") == null)
                {
                    Sessions.SetSessions("RecentlyPageIndex", 0);
                }

                //========== Get Categories data from the session ===========//
                //DT_Videos = (DataTable)Sessions.GetSession("RecentlyVideosData");

                int CurrentPageIndex = (int)(Sessions.GetSession("RecentlyPageIndex"));

                int intStartPosition = (CurrentPageIndex * ConstDefaultPageSizeForRecently);
                int intEndPosition = (CurrentPageIndex * ConstDefaultPageSizeForRecently) + ConstDefaultPageSizeForRecently;
                if (intEndPosition > DT_Videos.Rows.Count)
                    intEndPosition = DT_Videos.Rows.Count;

                DataTable DT_Temp = DT_Videos.Clone();

                if (DT_Videos.Rows.Count != 0)
                {
                    for (int i = intStartPosition; i < intEndPosition; i++)
                    {
                        DT_Temp.ImportRow(DT_Videos.Rows[i]);
                    }

                    DL_RecentlyVideos.DataSource = DT_Temp;
                    DL_RecentlyVideos.DataBind();
                    DL_RecentlyVideos.Visible = true;
                    ibtn_RecentlyNext.Visible = true;
                    ibtn_RecentlyPrevious.Visible = true;
                }
                else
                {
                    DL_RecentlyVideos.DataSource = null;
                    DL_RecentlyVideos.DataBind();
                    DL_RecentlyVideos.Visible = false;
                    ibtn_RecentlyNext.Visible = false;
                    ibtn_RecentlyPrevious.Visible = false;
                }
                SetNavigationButton(intStartPosition, intEndPosition, DT_Videos.Rows.Count, ibtn_RecentlyPrevious, ibtn_RecentlyNext);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void ibtn_RecentlyNext_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int intNext = (int)(Sessions.GetSession("RecentlyPageIndex"));
                intNext = intNext + 1;

                //DataTable DT_Videos = (DataTable)Sessions.GetSession("RecentlyVideosData");

                int intRowCount = (int)(Sessions.GetSession("RecentlyVideosDataCount"));

                //int intTotalPage = (DT_Videos.Rows.Count / ConstDefaultPageSizeForRecently);
                //if ((DT_Videos.Rows.Count % ConstDefaultPageSizeForRecently) > 0)
                //    intTotalPage = intTotalPage + 1;

                int intTotalPage = (intRowCount / ConstDefaultPageSizeForRecently);
                if ((intRowCount % ConstDefaultPageSizeForRecently) > 0)
                    intTotalPage = intTotalPage + 1;

                if (intNext >= intTotalPage)
                    intNext = intTotalPage - 1;
                Sessions.SetSessions("RecentlyPageIndex", intNext);

                BindRecentlyVideos();
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }

        protected void ibtn_RecentlyPrevious_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int intPrev = (int)(Sessions.GetSession("RecentlyPageIndex"));
                intPrev = intPrev - 1;
                //DataTable DT_Videos = (DataTable)Sessions.GetSession("RecentlyVideosData");

                int intRowCount = (int)(Sessions.GetSession("RecentlyVideosDataCount"));

                //int intTotalPage = (DT_Videos.Rows.Count / ConstDefaultPageSizeForRecently);
                //if ((DT_Videos.Rows.Count % ConstDefaultPageSizeForRecently) > 0)
                //    intTotalPage = intTotalPage + 1;
                int intTotalPage = (intRowCount / ConstDefaultPageSizeForRecently);
                if ((intRowCount % ConstDefaultPageSizeForRecently) > 0)
                    intTotalPage = intTotalPage + 1;
                if (intPrev <= 0)
                    intPrev = 0;
                Sessions.SetSessions("RecentlyPageIndex", intPrev);

                BindRecentlyVideos();
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }

        public string GetDescription(string Description)
        {
            try
            {
                if (Description.Trim().Length > 400)
                    return Description.Substring(0, 400);
                else
                    return Description;
            }
            catch (Exception)
            {
                return "";
            }
        }
        protected void DL_Videos2_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView drv = (DataRowView)(e.Item.DataItem);
                    RadToolTip rttVideo = (RadToolTip)e.Item.FindControl("rttVideo");
                    Image imgVideoFile = (Image)e.Item.FindControl("imgVideoFile");
                    rttVideo.TargetControlID = imgVideoFile.ClientID.ToString();
                    imgVideoFile.Style.Add("cursor", "pointer");
                    imgVideoFile.Attributes.Add("onclick", "return OpenMovieFile('PlayVideoFile.aspx?u=1&VideoID=" + drv["VideoID"].ToString() + "');");
                }
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }
        protected void DL_Videos3_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView drv = (DataRowView)(e.Item.DataItem);
                    //RadToolTip rttVideo = (RadToolTip)e.Item.FindControl("rttVideo");
                    Image imgVideoFile = (Image)e.Item.FindControl("imgVideoFile");
                    //rttVideo.TargetControlID = imgVideoFile.ClientID.ToString();
                    imgVideoFile.Style.Add("cursor", "pointer");
                    imgVideoFile.Attributes.Add("onclick", "return OpenMovieFile('PlayVideoFile.aspx?u=1&VideoID=" + drv["VideoID"].ToString() + "');");
                }
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }
        protected void DL_RecentlyVideos_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView drv = (DataRowView)(e.Item.DataItem);
                    //RadToolTip rttVideo = (RadToolTip)e.Item.FindControl("rttVideo");
                    Image imgVideoFile = (Image)e.Item.FindControl("imgVideoFile");
                    //rttVideo.TargetControlID = imgVideoFile.ClientID.ToString();
                    imgVideoFile.Style.Add("cursor", "pointer");
                    imgVideoFile.Attributes.Add("onclick", "return OpenMovieFile('PlayVideoFile.aspx?u=1&VideoID=" + drv["VideoID"].ToString() + "');");
                }
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }

        }

        protected void btnRefreshRecentVideo_Click(object sender, EventArgs e)
        {
            BindRecentlyVideos();
        }

        protected void btnRefreshMainDisplayArea_Click(object sender, EventArgs e)
        {
            //Page_Load(null, null);
            //BTN_Search_Click(null, null);
            if (divViewVideos3.Visible == true)
                BindGridVideos();
            else if (divViewVideos2.Visible == true)
                BindListVideos();
        }




    }
}