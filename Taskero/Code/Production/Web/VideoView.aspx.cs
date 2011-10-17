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
using System.Text;

public partial class VideoView : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //==========  Declaring Objects ==========//
            VideosData objVideo = new VideosData();
            //==========  Get Data Table and Bind ==========//
            DataTable DT_Videos = new DataTable();
            DT_Videos = objVideo.uspGetAllVideosByUserID(Sessions.UserID);
            DataView dv = DT_Videos.DefaultView;

            if (dv.Count > 0 && Fuji.Common.Sessions.GetSession("CategoryIDsObj") != null && Convert.ToString(Fuji.Common.Sessions.GetSession("CategoryIDsObj")).Trim() != "")
            {
                if (Fuji.Common.Sessions.GetSession("KeyWordsObj") != null && Convert.ToString(Fuji.Common.Sessions.GetSession("KeyWordsObj")) != "")
                    dv.RowFilter = "CategoryID IN (" + Fuji.Common.Sessions.GetSession("CategoryIDsObj").ToString() + ") AND Keywords LIKE '%" + Fuji.Common.Sessions.GetSession("KeyWordsObj").ToString() + "%'";
                else
                    dv.RowFilter = "CategoryID IN (" + Fuji.Common.Sessions.GetSession("CategoryIDsObj").ToString() + ")";
            }

            //==== Generate XML Data for Rotate view and rendare it ======== //
            StringBuilder strXML = getXML(dv);
            Response.Write(Server.HtmlDecode(strXML.ToString()).Replace("=\\", ""));
        }
        catch (Exception Ex)
        {
            HandleException(Ex);
        }
    }

    protected StringBuilder getXML(DataView dv)
    {
        StringBuilder strXML = new StringBuilder();
        try
        {
            
            strXML.Append("<?xml version='1.0' encoding='utf-8'?>");
            strXML.Append("<rotatorItems>");
            if (dv.Count > 0)
            {
                for (int i = 0; i < dv.Count; i++)
                {
                    strXML.Append("<rotator>");

                    strXML.Append("<id>");
                    strXML.Append(Convert.ToString(dv[i]["VideoID"]).Trim());
                    strXML.Append("</id>");

                    strXML.Append("<detailUrl>");
                    strXML.Append("http://www.google.com");
                    strXML.Append("</detailUrl>");

                    strXML.Append("<videoUrl>");
                    strXML.Append("PlayVideoFile.aspx?u=1&VideoID=" + Convert.ToString(dv[i]["VideoID"]).Trim());
                    strXML.Append("</videoUrl>");

                    strXML.Append("<imageUrl>");
                    strXML.Append("../Videos/VideosThumb/" + Convert.ToString(dv[i]["VideoID"]).Trim() + ".jpg");
                    strXML.Append("</imageUrl>");

                    strXML.Append("<title>");
                    strXML.Append(Convert.ToString(dv[i]["LinkText"]).Trim());
                    strXML.Append("</title>");

                    strXML.Append("<dec>");
                    strXML.Append(Convert.ToString(dv[i]["Description"]).Trim());
                    strXML.Append("</dec>");

                    strXML.Append("<keywords>");
                    strXML.Append(Convert.ToString(dv[i]["keywords"]).Trim());
                    strXML.Append("</keywords>");

                    strXML.Append("<videoid>");
                    strXML.Append(Convert.ToString(dv[i]["VideoID"]).Trim());
                    strXML.Append("</videoid>");

                    strXML.Append("</rotator>");
                }
            }
            strXML.Append("</rotatorItems>");

            
        }
        catch (Exception Ex)
        {
            HandleException(Ex);
        }
        return strXML;
    }

}
