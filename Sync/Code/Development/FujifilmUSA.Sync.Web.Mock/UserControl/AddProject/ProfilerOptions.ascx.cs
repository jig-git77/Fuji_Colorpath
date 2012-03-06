using System;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProfilerOptions : System.Web.UI.UserControl
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
                BindPresets();
                this.Visible = (this.Parent != null && ((FujifilmUSA.Sync.Web.UserControl.AddProject)Parent).Stage == 3);   
            }
        }
        #endregion
      
        #region Control events
        //protected void lbtnMeasurementSetup_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("AddProject.aspx?Stage=4");
        //}
        #endregion

        

        #region Method
        private void BindPresets()
        {
            Dictionary<int, string> dic = new Dictionary<int,string>();
            dic.Add(1, "Preserved Black");
            cmbSelectedPresets.DataSource = dic;
            cmbSelectedPresets.DataTextField = "Value";
            cmbSelectedPresets.DataValueField = "Key";
            cmbSelectedPresets.DataBind();
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