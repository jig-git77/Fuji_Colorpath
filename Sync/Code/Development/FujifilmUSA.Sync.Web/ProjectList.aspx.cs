using System;

namespace FujifilmUSA.Sync.Web
{
    public partial class ProjectList : BasePage
    {
        public void SetAddProjectVisibility()
        {
            ucProjectList.SetAddProjectVisibility();
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}