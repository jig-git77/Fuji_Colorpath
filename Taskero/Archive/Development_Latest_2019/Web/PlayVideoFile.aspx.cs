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
#endregion

public partial class PlayVideoFile : PageBase
{
    public int VideoID = 0;
    public string VideoFilePath = "";
    public int IsUser = 0;
    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //=========== Default Settings ==========//
            if (IsPostBack == false)
            {

                //========== Fill Variables to Form ==========//
                SetLabels();

                //=========== Calling the Method for binding the User Grid ==========//
                VideoID = int.Parse(Convert.ToString(Request.QueryString["VideoID"]));

                VideosData objVideo = new VideosData();
                objVideo.VideoID = VideoID;
                DataTable dt = objVideo.uspGetVideosByVideoID();
                if (dt.Rows.Count > 0)
                {
                    VideoFilePath = "Videos/" + dt.Rows[0]["VideoID"].ToString() + System.IO.Path.GetExtension(dt.Rows[0]["VideoFile"].ToString());
                }
                IsUser = 0;
                if (Request.QueryString["u"] != null)
                {
                    if (Request.QueryString["u"].ToString().Equals("1"))
                    {
                        RecentlyVideosData objRecentlyVideos = new RecentlyVideosData();
                        objRecentlyVideos.VideoID = VideoID;
                        objRecentlyVideos.TaskeroUserID = Sessions.UserID;
                        objRecentlyVideos.uspInsertRecentlyVideos();
                        IsUser = 1;
                    }
                }
            }
        }
        catch (Exception Ex)
        { HandleException(Ex); }
    }
    #endregion

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
            Resources.UserManagement.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
}
