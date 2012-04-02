using System;
using FujifilmUSA.Sync.Entity;

using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Collections.Generic;
using System.Web.UI.WebControls;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddCompanyManagement : BaseControl, IViewAddCompanyManagement
    {
        private PresenterCompanyManagement _presenterCompanyManagement;

        #region Declaration
        private int CId
        {
            get
            {
                return (ViewState["CompnayId"] != null && !string.IsNullOrEmpty(ViewState["CompnayId"].ToString())) ? Convert.ToInt32(ViewState["CompnayId"]) : 0;
            }
            set { ViewState["CompnayId"] = value.ToString(); }
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterCompanyManagement = new PresenterCompanyManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack) return;

            lblHeader.Text = "Add Company";
            BindRegion();
            if (Request.QueryString["CompanyId"] != null)
            {
                SetCompnay(Convert.ToInt32(Request.QueryString["CompanyId"]));
                lblHeader.Text = "Edit Company";
            }
        }
        #endregion

        #region Methods
        private void BindRegion()
        {
            RegionList = new Dictionary<int, string>();
            _presenterCompanyManagement.GetRegions();
            if (!IsErrorOccured)
            {
                cmbRegionName.DataTextField = "Value";
                cmbRegionName.DataValueField = "Key";
                cmbRegionName.DataSource = RegionList;
                cmbRegionName.DataBind();
                cmbRegionName.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
        }
        private void SaveCompnay()
        {
            Company = new Company()
                          {
                              CompanyName = txtCompanyName.Text
                                ,
                              IsActive = chkIsActive.Checked
                                ,
                              RegionId = Convert.ToByte(cmbRegionName.SelectedValue)
                          };

            if (CId == 0) // Add New Compnay
            {
                _presenterCompanyManagement.InsertCompany();
                if (IsErrorOccured) return;
                Response.Redirect("CompanyManagement.aspx");
            }
            else // Edit New Compnay
            {
                Company.CompanyId = CId;
                _presenterCompanyManagement.UpdateCompnay();
                if (IsErrorOccured) return;
                Response.Redirect("CompanyManagement.aspx");
            }


        }
        private void SetCompnay(int compnayId)
        {
            Company = new Company { CompanyId = compnayId };
            _presenterCompanyManagement.GetCompany();
            if (!IsErrorOccured)
            {
                CId = compnayId;
                txtCompanyName.Text = Company.CompanyName;
                cmbRegionName.SelectedValue = Company.RegionId.ToString();
                chkIsActive.Checked = Company.IsActive;
            }
        }
        #endregion

        #region Control Events
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SaveCompnay();
        }
        #endregion

        #region Implement Interface
        public Company Company { get; set; }
        public Dictionary<int, string> RegionList { get; set; }
        #endregion
    }
}