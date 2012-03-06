using System;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class TargetColorSpace : System.Web.UI.UserControl
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
                BindProfileSource();
                this.Visible = (this.Parent != null && ((FujifilmUSA.Sync.Web.UserControl.AddProject)Parent).Stage == 2);   
            }
        }
        #endregion
      
        #region Control events
        //protected void lbtnProfilerOptions_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("AddProject.aspx?Stage=3");
        //}
        #endregion

        

        #region Method
        private void BindProfileSource()
        {
            Dictionary<int, string> dic = new Dictionary<int,string>();
            dic.Add(1, "Gracol v02");
            dic.Add(2, "Swop");
            cmbSourceProfile.DataSource = dic;
            cmbSourceProfile.DataTextField = "Value";
            cmbSourceProfile.DataValueField = "Key";
            cmbSourceProfile.DataBind();
        }
      
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