using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.ComponentModel;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ModelWindowContainer : System.Web.UI.UserControl
    {
        #region Declaration
        public int ModelControlId
        {
            get
            { return Convert.ToInt32(Session["ModelControlId"]); }
            set { Session["ModelControlId"] = Convert.ToString(value); }
        }

        //[Browsable(false)]
        //public string Title 
        //{ 
        //    get { return hdTitle.Value.ToString(); } 
        //    set { hdTitle.Value = value; } 
        //}
        
        //public int MWidth { get { return Convert.ToInt32(hdWidth.Value); } set { hdWidth.Value = Convert.ToString(value); } }
        //public int MHeight { get { return Convert.ToInt32(hdHeight.Value); } set { hdHeight.Value = Convert.ToString(value); } }
        #endregion

        #region Page events
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["ModelControlId"] != null)
            {
                AddModelWindow();
            }
        }
        #endregion

        #region "Methods"
        public void AddModelWindow()
        {
            //ph.Controls.Clear();

            switch (Request.QueryString["ModelControlId"].ToString())
            {
                case "1":
                    this.ModelControlId = 1;
                    //var openPrintCondition = ((FujifilmUSA.Sync.Web.UserControl.OpenPrintCondition)this.LoadControl("~/UserControl/ProjectList/OpenPrintCondition.ascx"));
                    //ph.Controls.Add(openPrintCondition);
                    break;
                case "2":
                    this.ModelControlId = 2;
                    //var closePrintCondition = ((FujifilmUSA.Sync.Web.UserControl.ClosePrintCondition)this.LoadControl("~/UserControl/ProjectList/ClosePrintCondition.ascx"));
                    //ph.Controls.Add(closePrintCondition);
                    break;
                case "3":
                    this.ModelControlId = 3;
                    //var closePrintCondition = ((FujifilmUSA.Sync.Web.UserControl.ClosePrintCondition)this.LoadControl("~/UserControl/ProjectList/ClosePrintCondition.ascx"));
                    //ph.Controls.Add(closePrintCondition);
                    break;
            }
        }
        #endregion

    }
}