using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web
{
    public partial class BrowserTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
            if (!Page.IsPostBack)
            {
               
                
            }
        }             

        protected void btn_ServerSide_Click(object sender, EventArgs e)
        {
            RunServerSideTest();
        }

        public void RunClientSideTest()
        {

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
    }
}