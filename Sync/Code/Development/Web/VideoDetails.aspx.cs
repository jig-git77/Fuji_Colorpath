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

public partial class VideoDetails : PageBase
{
    public int VideoID = 0;
    public string VideoFilePath = "";
    public string strDescription = "";
    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //=========== Clear Error ==========//
            LblError.Text = "";

            //=========== Check Login ==========//
            //Sessions.CheckUserLogin();

            //========== Check User Role and redirect to home page as per user role ===========//
            //Fuji.Common.CommonFunctions.CheckUserRole();

            //=========== Bind the show button with script which show and hide the password fields ==========//
            //BTN_DisplayPasswordField.Attributes.Add("OnClick", "return display();");

            //=========== Default Settings ==========//
            if (IsPostBack == false)
            {

                //========== Fill Variables to Form ==========//
                SetLabels();

                //=========== Calling the Method for binding the User Grid ==========//
                VideoID = int.Parse(Request.QueryString["VideoID"].ToString());

                VideosData objVideo = new VideosData();
                objVideo.VideoID = VideoID;
                DataTable dt = objVideo.uspGetVideosByVideoID();
                if (dt.Rows.Count > 0)
                {
                    //LBL_VideoFile.Text = dt.Rows[0]["VideoFile"].ToString();
                    VideoFilePath = "Videos/" + dt.Rows[0]["VideoID"].ToString() + System.IO.Path.GetExtension(dt.Rows[0]["VideoFile"].ToString());
                    strDescription = dt.Rows[0]["Description"].ToString();
                }

                RecentlyVideosData objRecentlyVideos = new RecentlyVideosData();
                objRecentlyVideos.VideoID = VideoID;
                objRecentlyVideos.TaskeroUserID = Sessions.UserID;
                objRecentlyVideos.uspInsertRecentlyVideos();
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
        finally
        {
        }
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
            Resources.VideoDetails.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));

            //========== Set Values ==========//
            LTRL_Title.Text = Resources.VideoDetails.LTRL_Title_Text;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
}
