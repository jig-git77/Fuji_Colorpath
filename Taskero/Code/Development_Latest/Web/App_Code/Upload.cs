using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for Upload
/// </summary>
public class Upload : IHttpHandler, IRequiresSessionState
{   
    public Upload()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region "IHttpHandler Members"
    public bool IsReusable
    {
        get { return true; }
    }
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string uploadPath = context.Server.MapPath(context.Request.ApplicationPath + "/UploadFiles");
            HttpPostedFile uploadFile = context.Request.Files[0];
            string FileName = uploadFile.FileName.ToString();
            string Ext = Path.GetExtension(FileName);
            FileName = Path.GetFileNameWithoutExtension(FileName);

            FileName = FileName + "_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + Ext;

            int PressRunID = Convert.ToInt16(context.Request.QueryString["pressrunid"].ToString());
            string SerialNo = context.Request.QueryString["serialno"].ToString();
            DateTime LocalDate = Convert.ToDateTime(context.Request.QueryString["localdate"].ToString().Replace("C", "/").Replace("D", ":").Replace("S", " "));

            if (Directory.Exists(uploadPath + "/" + SerialNo.ToString()))
                uploadPath = uploadPath + "/" + SerialNo.ToString();
            else
            {
                Directory.CreateDirectory(uploadPath + "/" + SerialNo.ToString());
                uploadPath = uploadPath + "/" + SerialNo.ToString();
            }

            if (Directory.Exists(uploadPath + "/" + PressRunID.ToString()))
                uploadPath = uploadPath + "/" + PressRunID.ToString();
            else
            {
                Directory.CreateDirectory(uploadPath + "/" + PressRunID.ToString());
                uploadPath = uploadPath + "/" + PressRunID.ToString();
            }

            uploadFile.SaveAs(Path.Combine(uploadPath, FileName));

            string strFileContent;
            strFileContent = System.IO.File.ReadAllText(Path.Combine(uploadPath, FileName));

            CPSync.ParseMeasurementFile obj = new CPSync.ParseMeasurementFile();
            string uploadstatus;
            uploadstatus = obj.ParseMeasurment(PressRunID, strFileContent, FileName, LocalDate);

            if (uploadstatus != "Success")
            {

                //send the file as email attachment
                //send the file as email attachment
                if (Fuji.Common.WebSettings.ErrorLogType == "E" || Fuji.Common.WebSettings.ErrorLogType == "B")
                {

                    Exception exc = new Exception(uploadstatus);
                    Fuji.Common.WebMails.SendErrorEmailWithAttachment(exc, strFileContent);

                }
                
                HttpContext.Current.Response.Write("false - Error - " + uploadstatus.ToString() + ".");
                System.IO.File.Delete(Path.Combine(uploadPath, FileName));
               
            }
            else
            {
                HttpContext.Current.Response.Write("true - File uploaded - " + uploadstatus.ToString() + ".");
                
                //now delete the file from the file system
                System.IO.File.Delete(Path.Combine(uploadPath, FileName));
            }
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write("false - Exception - " + ex.Message + ".");
        }
    }   
    #endregion
}
