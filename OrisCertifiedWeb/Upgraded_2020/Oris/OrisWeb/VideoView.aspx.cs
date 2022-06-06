using Oris.Business;
using Oris.Common;
using OrisWeb.Models;
using System;
using System.Data;
using System.Text;

namespace OrisWeb
{
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

                if (dv.Count > 0 && Sessions.GetSession("CategoryIDsObj") != null && Convert.ToString(Sessions.GetSession("CategoryIDsObj")).Trim() != "")
                {
                    if (Sessions.GetSession("KeyWordsObj") != null && Convert.ToString(Sessions.GetSession("KeyWordsObj")) != "")
                        dv.RowFilter = "CategoryID IN (" + Sessions.GetSession("CategoryIDsObj").ToString() + ") AND Keywords LIKE '%" + Sessions.GetSession("KeyWordsObj").ToString() + "%'";
                    else
                        dv.RowFilter = "CategoryID IN (" + Sessions.GetSession("CategoryIDsObj").ToString() + ")";
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
}