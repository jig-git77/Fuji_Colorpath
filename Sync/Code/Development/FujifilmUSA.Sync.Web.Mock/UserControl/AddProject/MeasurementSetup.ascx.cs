using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using System.Linq;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.Mock.UserControl
{
    public partial class MeasurementSetup : System.Web.UI.UserControl
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
                BindMeasurementDevice();
                this.Visible = (this.Parent != null && ((FujifilmUSA.Sync.Web.Mock.UserControl.AddProject)Parent).Stage == 4);   
            }
        }
        #endregion
      
        #region Control events.
        //protected void lbtnComfirmSave_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("AddProject.aspx?Stage=5");
        //}
        #endregion

        

        #region Method
        private void BindMeasurementDevice()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(1, "i1-Pro - 873613");

            cmbMeasurementDevice.DataSource = dic;
            cmbMeasurementDevice.DataTextField = "Value";
            cmbMeasurementDevice.DataValueField = "Key";
            cmbMeasurementDevice.DataBind();
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