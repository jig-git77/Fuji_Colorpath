using Oris.Business;
using Oris.Common;
using OrisWeb.Models;
using System;

namespace OrisWeb
{
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
                    Sessions.CheckUserLogin();


                    if (Sessions.RoleID.ToString() != null)
                    {
                        if (Sessions.RoleID != 1)   // do not check license agreement for taskero admin
                        {
                            if (Sessions.SiteLicenseAgreed)
                            {
                                //redirect to the video page
                                CommonFunctions.PageRedirect("Videos", "");
                            }
                            else
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
            CommonFunctions.PageRedirect("Videos", "");
        }

        //Clicked on license disagree button
        protected void DisAgreeButton_Click(object sender, EventArgs e)
        {
            //redirect to the home page
            CommonFunctions.PageRedirect("Home", "");
        }
    }
}