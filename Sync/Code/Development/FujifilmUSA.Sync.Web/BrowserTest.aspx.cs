using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace FujifilmUSA.Sync.Web
{
    public partial class BrowserTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                Panel_Grid.Visible = false;
            }
            Page.Header.DataBind();          
        }

        #region Control Events                
      
        protected void btn_LoadGrid_Click(object sender, EventArgs e)
        {
            Panel_Grid.Visible = true;
            BindRadGrid();
        }

        protected void btn_PDF_Click(object sender, EventArgs e)
        {
            RadGrid_Test.ExportSettings.OpenInNewWindow = true;
            RadGrid_Test.MasterTableView.ExportToPdf();
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

        #endregion

        protected void btn_SendEmail_Click(object sender, EventArgs e)
        {
           SendBrowserTestEmail("dpatel@fujifilm.com","Hi","How r u?","Dhruti Patel");
        }


        public static void SendBrowserTestEmail(string to, string subject, string body, string UserName)
        {
            var message = new MailMessage
            {
                Subject = FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Email_Subject_SendUserCreatedEmail"),
                IsBodyHtml = true,
                Body = "Hello," + "</br>" + body + "</br>" + UserName
            };
            message.To.Add(to);
            message.Bcc.Add("dpatel@fujifilm.com");
            message.From = new MailAddress("colorpath@taskero.com");
            
            var mySmtpClient = new SmtpClient();
            mySmtpClient.Send(message);
        }
    }
}