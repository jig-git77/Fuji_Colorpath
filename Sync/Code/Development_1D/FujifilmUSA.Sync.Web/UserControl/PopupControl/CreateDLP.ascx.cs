using System;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class CreateDLP : BaseControl
    {
        public string DLPMessage
        {
            set { lblMessage.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}