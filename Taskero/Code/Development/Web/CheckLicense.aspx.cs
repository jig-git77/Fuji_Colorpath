#region
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
using Fuji.DataAccess;
#endregion

public partial class CheckLicense : PageBase
{
    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                //========== Check Session ===========//
                Fuji.Common.Sessions.CheckUserLogin();
              

                if (Fuji.Common.Sessions.RoleID.ToString() != null)
                {
                    if (Fuji.Common.Sessions.RoleID != 1)   // do not check license agreement for taskero admin
                    {
                        if (Fuji.Common.Sessions.SiteLicenseAgreed) 
                        {
                            //redirect to the video page
                            Fuji.Common.CommonFunctions.PageRedirect("Videos", "");
                        } else 
                        {
                           ModalPopup.Show();
                        }    
                                         
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            HandleException(Ex);
        }
    }
    #endregion


    //License agreement button is clicked
    protected void AgreeButton_Click(object sender, EventArgs e)
    {
        //handle agree button click event in the database
        SiteLicenseData objSiteLicenseData = new SiteLicenseData();
        objSiteLicenseData.RemoteHost = Request.UserHostAddress;
        objSiteLicenseData.SiteLicenseAgreed = true;
        objSiteLicenseData.SiteLicenseTypeID = 1;
        objSiteLicenseData.TaskeroUserID = Sessions.UserID;
        objSiteLicenseData.AcceptedDate = DateTime.Now;
        objSiteLicenseData.uspUpdateSiteLicenseAgreement();

        //redirect to the video page
        Fuji.Common.CommonFunctions.PageRedirect("Videos", "");
    }

    //Clicked on license disagree button
    protected void DisAgreeButton_Click(object sender, EventArgs e)
    {
        //redirect to the home page
         Fuji.Common.CommonFunctions.PageRedirect("Home", "");
    }
}
