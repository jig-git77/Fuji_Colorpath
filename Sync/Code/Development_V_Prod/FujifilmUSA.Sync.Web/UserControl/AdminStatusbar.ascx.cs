using System;
using System.Drawing;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AdminStatusbar : BaseControl
    {
        #region Declaration
        public string Message { get; set; }
        public bool Status { get; set; }
        #endregion

        #region Constructor
        public AdminStatusbar()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
            this.PreRender += (sender, e) => this.ExceptionHandling(Page_PreRender, sender, e);
        } 
        #endregion

        #region Page events
        protected void Page_Load(object sender, EventArgs e)
        {
            //Note : Every time this event fire and control set visible = false
            this.Visible = (this.Message != null);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            lblMessage.Text = this.Message;
            lblMessage.ForeColor = (this.Status) ? Color.Green : Color.Red;
        }
        #endregion

        #region "Methods"

        public void DisplayMessage(string message, bool status)
        {
            this.Message = message;
            this.Status = status;
            this.Visible = true;
        }

        #endregion
    }
}