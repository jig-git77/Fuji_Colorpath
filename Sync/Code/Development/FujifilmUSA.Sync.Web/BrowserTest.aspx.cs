using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;

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
        protected void btn_ServerSide_Click(object sender, EventArgs e)
        {
            RunServerSideTest();
        }           
      
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

        public void RunServerSideTest()
        {
            System.Web.HttpBrowserCapabilities browser = Request.Browser;
            bool cookies = false;
            Response.Cookies["test"].Value = "Hi";

            if (Request.Cookies["test"].Value == "Hi")
            {
                cookies = true;
            }

            string s = "<b>Browser Capabilities</b><br />"
                + "Type = " + browser.Type + "<br />"
                + "Name = " + browser.Browser + "<br />"
                + "Version = " + browser.Version + "<br />"
                + "Major Version = " + browser.MajorVersion + "<br />"
                + "Minor Version = " + browser.MinorVersion + "<br />"
                + "Platform = " + browser.Platform + "<br />"
                + "Is Beta = " + browser.Beta + "<br />"
                + "Is Crawler = " + browser.Crawler + "<br />"
                + "Is AOL = " + browser.AOL + "<br />"
                + "Is Win16 = " + browser.Win16 + "<br />"
                + "Is Win32 = " + browser.Win32 + "<br />"
                + "Supports Frames = " + browser.Frames + "<br />"
                + "Supports Tables = " + browser.Tables + "<br />"
                + "Supports Cookies = " + cookies + "<br />"
                + "Supports VBScript = " + browser.VBScript + "<br />"
                + "Supports JavaScript = " + browser.JavaScript + "<br />"
                + "Supports Java Applets = " + browser.JavaApplets + "<br />"
                + "Supports ActiveX Controls = " + browser.ActiveXControls + "<br />"
                + "Supports JavaScript Version = " +
                    browser["JavaScriptVersion"] + "<br />";

            lbl_Result.Text = s;
        }

        #endregion
    }
}