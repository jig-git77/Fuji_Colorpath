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
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Web
{
    public partial class BrowserTest : System.Web.UI.Page, IViewBrowserTest
    {
        public static string url;
        private string path = "";
        private PresenterBrowserTest presenterBrowserTest;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
              
                Panel_Grid.Visible = false;
                url = String.Format("UploadFiles/Report_PDF/BrowserTest/{0}.pdf", "BrowserTest" + "_" + Guid.NewGuid().ToString());
            }
            Page.Header.DataBind();
        
            path = Server.MapPath(url);

            presenterBrowserTest = new PresenterBrowserTest(this);
        }

        #region Control Events
        protected void btnServerSideTest_Click(object sender, EventArgs e)
        {
            //Step: 1
            presenterBrowserTest.Ping();

            DataTable table = new DataTable();
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Details", typeof(string));

            table.Rows.Add("Profiler Service Status", PingResponse == true ? "Active" : "Inactive");

            Panel_Grid.Visible = true;
            BindRadGrid(table);
        }

        protected void btn_LoadGrid_Click(object sender, EventArgs e)
        {
            Panel_Grid.Visible = true;      
            BindRadGrid(BrowserTable());
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
        public void BindRadGrid(DataTable result)
        {
            RadGrid_Test.DataSource = result;
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
            if (Hidden_PluginVersion.Value != ApplicationConfiguration.Instance.FujiFilmPluginVersion) //-----------For Giving option to download plugin, if it is outdated-----//
            {
                if (Hidden_PluginOS.Value == "Windows")  //-----For Windows PC
                {
                    table.Rows.Add("", "<a href='Download/Colorpath PCPlugin Installer.zip'><b>(Fujifilm Plugin is outdated, So please download latest version from here...)</b></a>");
                }
                else  //---For MAC PC
                {
                    table.Rows.Add("", "<a href='Download/Colorpath MacPlugin Installer.zip'><b>(Fujifilm Plugin is outdated, So please download latest version from here...)</b></a>");
                }
            }
            if (Hidden_OtherPlugins.Value != "") //---Not supported in IE
            {
                table.Rows.Add("Other Plugins:", Hidden_OtherPlugins.Value);
            }
           
            table.Rows.Add("Plugin Data Service status:", Hidden_PluginDataService.Value);
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
                    Attachment attach = new Attachment(path);
                    message.Attachments.Add(attach);
                    var mySmtpClient = new SmtpClient();
                    mySmtpClient.Send(message);
                    attach.Dispose();
                    lblMsg.Text = "Email has been sent!";

                    //DELETE THE FILE AFTER SENDING EMAIL
                    File.Delete(path);
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

        #region IViewProfilerService members

        public bool PingResponse { get; set; }

        public int CurrentUserId { get; set; }

        public void ErrorOccured(object sender, Entity.ErrorArgs e)
        {
            //do nothing
        }
        #endregion
    }
}