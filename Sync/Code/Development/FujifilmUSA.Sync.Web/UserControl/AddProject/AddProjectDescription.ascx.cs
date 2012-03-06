using System;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddProjectDescription : System.Web.UI.UserControl
    {
        #region "Declaration"
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionManager.CheckLogin();
            if (!IsPostBack)
            {
                this.Visible = (this.Parent != null && ((FujifilmUSA.Sync.Web.UserControl.AddProject)Parent).Stage == 1);   
            }
        }
        #endregion

     

      
        #region Control events
        //protected void lbtnTargetColorSpace_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("AddProject.aspx?Stage=2");
        //}
        #endregion

        #region Method
       
        #endregion

        #region Error Handling Method
        //public void ErrorOccured(object sender, Business.Interface.ErrorArgs e)
        //{
        //    ucStatusbar1.DisplayMessage(e.ErrorMessage, false);
        //}
        #endregion

        //*******************************************************************//
        //#region IViewProjectList Members
        //public List<Project> Projects { get; set;}
        //#endregion

        //#region IViewBaseProject Members
        //public Project Project{ get; set;}
        //#endregion
    }
}