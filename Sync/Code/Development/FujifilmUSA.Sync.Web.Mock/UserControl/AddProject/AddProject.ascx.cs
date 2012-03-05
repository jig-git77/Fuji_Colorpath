using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using System.Linq;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.Mock.UserControl
{
    public partial class AddProject : System.Web.UI.UserControl
    {
        #region "Declaration"
        public int Stage
        {
            get
            {
                return Session["Stage"] == null ? 0 : Convert.ToInt32(Session["Stage"]);
            }
            set
            {
                Session["Stage"] = value;
            }
        }
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            this.Stage = Request.QueryString["Stage"] != null ? Convert.ToInt32(Request.QueryString["Stage"]) : 0;
        }
        public void Page_PreRender(Object o, EventArgs e)
        {
            //if (!IsPostBack && Request.QueryString["Stage"] != null)
            //{
            //    //ph.Controls.Clear();
            //var addProjectDescription = ((AddProjectDescription)this.LoadControl("~/UserControl/AddProjectDescription.ascx"));
            //ph.Controls.Add(addProjectDescription);

            switch (Convert.ToString(Request.QueryString["Stage"]))
            {
                case "1":
                    hTitle.InnerText = "New Print Condition - Description";
                    lbtnCreateNewPrintCondition.ValidationGroup = "vgDescription";
                    lbtnCreateNewPrintCondition.Text = "<span><img id='imgAdd' src='Images/new_project_icon.png'/>Traget ColorSpace<span>";
                    break;
                case "2":
                    hTitle.InnerText = "New Print Condition - Target Colorspace";
                    lbtnCreateNewPrintCondition.ValidationGroup = "";
                    lbtnCreateNewPrintCondition.Text = "<span><img id='imgAdd' src='Images/new_project_icon.png'/>Profiler Options<span>";
                    break;
                case "3":
                    hTitle.InnerText = "New Print Condition - Profiler Options";
                    lbtnCreateNewPrintCondition.ValidationGroup = "";
                    lbtnCreateNewPrintCondition.Text = "<span><img id='imgAdd' src='Images/new_project_icon.png'/>Measurement Setup<span>";
                    break;
                case "4":
                    hTitle.InnerText = "New Print Condition - Measurement Setup";
                    lbtnCreateNewPrintCondition.ValidationGroup = "vgMeasurementSetup";
                    lbtnCreateNewPrintCondition.Text = "<span><img id='imgAdd' src='Images/new_project_icon.png'/>Comfirm Save<span>";
                    break;
                case "5":
                    hTitle.InnerText = "New Print Condition - Save Print Condition";
                    lbtnCreateNewPrintCondition.ValidationGroup = "";
                    lbtnCreateNewPrintCondition.Text = "<span><img id='imgAdd' src='Images/new_project_icon.png'/>Save<span>";
                    lbtnCreateNewPrintCondition.Attributes.Add("onclick", "javascript: return OpenInformationBox('" + ResolveUrl("~/PrintCondition.aspx?ModelControlId=3") + "')");
                    break;

            }
            //}
        }
        #endregion

        #region Control events
        protected void lbtnCreateNewPrintCondition_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddProject.aspx?Stage=" + (this.Stage + 1).ToString());
        }
        #endregion

        
    }
}