#region Using
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
#endregion

#region Using Others
using Fuji.Common;
using Fuji.Business;
using Fuji.DataAccess;
#endregion

public partial class Common_StausMenu : ControlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Check User Login ==========//
            Sessions.CheckUserLogin();

            //********* Method for the Page Right ************//
            CheckRights();

            //========== Default Settings ==========//
            if (IsPostBack == false)
            {
                
                //========== Set The Active Tab of Status ==========//
                string URL = Request.Url.ToString();
                if (URL.IndexOf("ProofandPlates.aspx") > 0)
                {
                    ProofandPlates.Attributes.Add("class", "active");
                    DataMonitor.Attributes.Add("class", "");
                    Press.Attributes.Add("class", "");
                }
                else if (URL.IndexOf("DataMonitor.aspx") > 0)
                {
                    DataMonitor.Attributes.Add("class", "active");
                    ProofandPlates.Attributes.Add("class", "");
                    Press.Attributes.Add("class", "");
                }
                else if (URL.IndexOf("Press.aspx") > 0)
                {
                    Press.Attributes.Add("class", "active");
                    ProofandPlates.Attributes.Add("class", "");
                    DataMonitor.Attributes.Add("class", "");
                }
            }
        }
        catch (Exception Exc)
        {
            HandleException(Exc).ToString();
        }

    }
    #region Methods & Functions
    //********* Method for the Page Right ************//
    private void CheckRights()
    {
        //********* Setting Right for Job Tab************//
        if (Fuji.Common.Sessions.View_Press_Data == false)
        { Press.Visible = false; }
        else
        { Press.Visible = true; }

        if (Fuji.Common.Sessions.View_Data_Monitor == false)
        { DataMonitor.Visible = false; }
        else
        { DataMonitor.Visible = true; }
    }
    #endregion
}
