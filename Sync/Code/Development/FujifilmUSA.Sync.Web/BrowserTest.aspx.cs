using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.IO;
using System.Text;

namespace FujifilmUSA.Sync.Web
{
    public partial class BrowserTest : System.Web.UI.Page
    {
        private string url;
        private string path = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                Panel_Grid.Visible = false;
            }
            Page.Header.DataBind();
            url = String.Format("UploadFiles/Report_PDF/BrowserTest/{0}.pdf", "BrowserTest");
            path = Server.MapPath(url);
        }

        #region Control Events                
      
        protected void btn_LoadGrid_Click(object sender, EventArgs e)
        {
            Panel_Grid.Visible = true;
            BindRadGrid();
        }

        protected void btn_PDF_Click(object sender, EventArgs e)
        {
            //------------Export RadGrid in pdf file first------------//
            RadGrid_Test.ExportSettings.OpenInNewWindow = true;
            RadGrid_Test.ExportSettings.FileName = "BrowserTest";
            RadGrid_Test.MasterTableView.ExportToPdf();
        }

        protected void RadGrid_Test_GridExporting(object source, GridExportingArgs e)
        {
            if (File.Exists(path))
            {
                //DELETE THE FILE IF ALREADY EXISTS  
                File.Delete(path);
            }
            using (FileStream fs = File.Create(path))
            {
                Byte[] info = System.Text.Encoding.Default.GetBytes(e.ExportOutput);
                //SAVE IT ON THE SERVER  
                fs.Write(info, 0, info.Length);
                fs.Close();
                fs.Dispose();               
            }
           
            //Response.Redirect("~/BrowserTest.aspx");
        }

        #endregion

        #region Methods
        public void BindRadGrid()
        {
            RadGrid_Test.DataSource = BrowserTable();
            RadGrid_Test.DataBind();           
        }

        public DataTable BrowserTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Details", typeof(string));

            table.Rows.Add("Browser:", Hidden_Browser.Value);
            table.Rows.Add("FullVersion:", Hidden_BrowserVersion.Value);
            table.Rows.Add("JavaScript Enabled:", Hidden_Javascript.Value);
            table.Rows.Add("Session/Cookies Enabled:", Hidden_Cookies.Value);
            table.Rows.Add("HTML5 Supported:", Hidden_HTML5.Value);
            table.Rows.Add("Fujifilm Plugin:", Hidden_PluginMessage.Value);
            if (Hidden_OtherPlugins.Value != "") //---Not supported in IE
            {
                table.Rows.Add("Other Plugins:", Hidden_OtherPlugins.Value);
            }
            return table;
        }

        protected void btn_SendEmail_Click(object sender, EventArgs e)
        {
            SendBrowserTestEmail(txtEmail.Text, txtSubject.Text, txtBody.Text, txtUserName.Text);
        }


        public void SendBrowserTestEmail(string to, string subject, string body, string UserName)
        {
            try
            {              
                //---------After creating file, send email with attachment-----------//
                if (File.Exists(path))
                {
                    var message = new MailMessage
                    {
                        Subject = subject,
                        IsBodyHtml = true,
                        Body = "Sub: Browser Compability Check Report From " + UserName + "<br/><br/>" + "Hello," + "<br/><br/>" + body + "<br/><br/>-Thanks,<br/>" + UserName
                    };
                    message.To.Add(to);                
                    message.Attachments.Add(new Attachment(path));
                    var mySmtpClient = new SmtpClient();
                    mySmtpClient.Send(message);

                    lblMsg.Text = "Email has been sent!";

                    //if (File.Exists(path))
                    //{
                        ////DELETE THE FILE IF ALREADY EXISTS  
                        File.Delete(path);
                    //}
                }
                else
                {
                    lblMsg.Text = "First Export the PDF file, then send the Email!";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        #endregion

       
    }
}