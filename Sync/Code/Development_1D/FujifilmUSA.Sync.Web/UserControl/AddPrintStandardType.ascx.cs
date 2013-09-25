using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using System.IO;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddPrintStandardType : BaseControl, IViewPrintStandardType
    {
        #region "Declaration"
        private PresenterPrintStandardType _presenterprintstandardtype;
       
        private const string _recAddPrintStandardType = "lblHeaderResource1.Text";
        private const string _recEditPrintStandardType = "editPrintStandardType";
        private const string _recSelectSourceProfileText = "selectSourceProfileText";
        private const string _recPlzSelectSourceProfile = "plzSelectSourceProfile";

        #endregion

        public AddPrintStandardType()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presenterprintstandardtype = new PresenterPrintStandardType(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack) return;

            lblHeader.Text = GetResourceString(_recAddPrintStandardType); //"Add PrintStandardType";            
            BindSourceProfileDropDown();
          
            if (Request.QueryString["PrintStandardTypeId"] != null)
            {
                lblHeader.Text = GetResourceString(_recEditPrintStandardType);//"Edit PrintStandardType";
                LoadPrintStandardType(Convert.ToInt32(Request.QueryString["PrintStandardTypeId"]));
            }
        }
        #endregion

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (cmbSourceProfile.SelectedIndex == 0)
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recPlzSelectSourceProfile), false);//Please select Source Profile.
                return;
            }          

            PrintStandardType = new Entity.CPSync1D.PrintStandardType()
            {
                PrintStandard = txtPrintStandardType.Text,
                IsSync1DType = chkIsSync1DType.Checked,
                SourceProfile = new SourceProfile()
                {
                    SourceProfileId = Convert.ToInt16(cmbSourceProfile.SelectedValue)
                }                
            };

            if (hdnPrintStandardTypeID.Value == "0")  // Add New Print Standard Type
            {
                _presenterprintstandardtype.InsertPrintStandardType();
                if (IsErrorOccured) return;
            }
            else // Edit Print Standard Type
            {
                PrintStandardType.PrintStandardTypeID = (short)Convert.ToInt32(hdnPrintStandardTypeID.Value);
                _presenterprintstandardtype.UpdatePrintStandardType();
                if (IsErrorOccured) return;
            }
            Response.Redirect("PrintStandardTypeManagement.aspx");
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {

        }

        #region Methods
        private void BindSourceProfileDropDown()
        {
            cmbSourceProfile.Items.Clear();

            _presenterprintstandardtype.SourceProfileList();
            if (!IsErrorOccured)
            {
                cmbSourceProfile.DataTextField = "Value";
                cmbSourceProfile.DataValueField = "Key";
                cmbSourceProfile.DataSource = SourceProfileList;
                cmbSourceProfile.DataBind();
            }

            cmbSourceProfile.Items.Insert(0, new ListItem(GetResourceString(_recSelectSourceProfileText), "0"));//-- Select Source Profile --
        }

        private void LoadPrintStandardType(int iPrintStandardTypeId)
        {
             PrintStandardType = new Entity.CPSync1D.PrintStandardType { PrintStandardTypeID = (short)iPrintStandardTypeId };
            _presenterprintstandardtype.GetPrintStandardType();
            if (!IsErrorOccured)
            {
                hdnPrintStandardTypeID.Value = Convert.ToString(iPrintStandardTypeId);
                txtPrintStandardType.Text = PrintStandardType.PrintStandard;
                cmbSourceProfile.SelectedValue = Convert.ToString(PrintStandardType.SourceProfile.SourceProfileId);  
                chkIsSync1DType.Checked = (PrintStandardType.IsSync1DType == true ? true : false);
            }
        }
        #endregion

        #region IViewPrintStandardType Members
        public short PrintStandardTypeID { get; set; }
        public string PrintStandard { get; set; }
        public bool IsSync1DType { get; set; }
        public Entity.CPSync1D.PrintStandardType PrintStandardType { get; set; }
        public List<Entity.CPSync1D.PrintStandardType> PrintStandardTypes { get; set; }
        public Dictionary<int, string> SourceProfileList { get; set; }
        #endregion
    }
}