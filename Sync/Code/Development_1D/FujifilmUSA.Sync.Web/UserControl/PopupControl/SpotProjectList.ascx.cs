using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System;
using System.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl.PopupControl
{
    public partial class SpotProjectList : BaseControl
    {
        public int JsRandomNumber { get { return new Random().Next(1, 100); } }
        public string JsRandomGuid { get { return Guid.NewGuid().ToString(); } }
        public string ProjectListDeleteSpotProjectConfirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("ProjectList_DeleteSpotProject_Confirmation");
            }
        }        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                locCreateLibrary.Text= ResourceManager.GetResourceMessage("CreateNewLibrary");
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Device;
                //set dlpsuboperation session to none everytime page loads fresh(!ispostback)...
                SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.None;
                hdnBaseServiceUrl.Value = Utilities.ReadConfigValue("BaseServiceURL");
                hdfHasDeletePermission.Value = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ModifyProject).ToString();
                hdfCurrentUser.Value = Convert.ToString(SessionManager.Instance.CurrentUser.UserId);
                hdfCurrentCulture.Value = getCurrentCultureLanguage();
                hdfAuthToken.Value = SessionManager.Instance.CurrentUser.APIAuthKey;
                hdfProjectID.Value = Convert.ToString(Request.QueryString["projectId"]);
                lblTitle.Text = Convert.ToString(Request.QueryString["projectName"]);

                lblTitle.Text = "\"" + lblTitle.Text + "\"" + " " + "Brand Color Libraries";
                //lblTitle.Text = "\"How to add doublequotes\""
            }
        }

        protected void btnCreateSpotProject_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
               int ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UniqId", "window.parent.location='RedirectFromProjectList.aspx?RedirectType='AddSpotProject"& ", true);
                string url = "RedirectFromProjectList.aspx?RedirectType=AddSpotProject&ProjectID="+ ProjectId.ToString();
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UniqId", "window.parent.location='RedirectFromProjectList.aspx?RedirectType=\"AddSpotProject\"&ProjectID='"+ProjectId+"';", true);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "UniqId", "window.parent.location='"+url+"';", true);
            }
        }
    }
}