using System;
using FujifilmUSA.Sync.Entity;

using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddCompanyManagement : BaseControl, IViewAddCompanyManagement
    {
        private PresenterCompanyManagement _presenterCompanyManagement;

        #region Private variables - Globalization
        private const string _recAddCompany = "addCompanyText";
        private const string _recEditCompany = "editCompanyText";
        private const string _recSelectText = "selectText";
        #endregion

        #region Declaration
        private int CId
        {
            get
            {
                return (ViewState["companyId"] != null && !string.IsNullOrEmpty(ViewState["companyId"].ToString())) ? Convert.ToInt32(ViewState["companyId"]) : 0;
            }
            set { ViewState["companyId"] = value.ToString(); }
        }
        #endregion
        public AddCompanyManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterCompanyManagement = new PresenterCompanyManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack) return;

            lblHeader.Text = GetResourceString(_recAddCompany); //"Add Company";
            BindRegion();
            if (Request.QueryString["CompanyId"] != null)
            {
                var CompanyID = Request.QueryString["CompanyId"];
                var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(CompanyID, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                int _CompanyID;
                if (Int32.TryParse(id, out _CompanyID))
                {
                    Setcompany(_CompanyID);
                    lblHeader.Text = GetResourceString(_recEditCompany); //"Edit Company";
                }
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
                cmbRegionName.Items.Insert(0, new ListItem(GetResourceString(_recSelectText), string.Empty));
            }
        }
        private void Savecompany()
        {
            Company = new Company()
                          {
                              CompanyName = txtCompanyName.Text
                                ,
                              IsActive = chkIsActive.Checked
                                ,
                              RegionId = Convert.ToByte(cmbRegionName.SelectedValue)
                          };

            if (CId == 0) // Add New company
            {
                _presenterCompanyManagement.InsertCompany();
                if (IsErrorOccured) return;
                Response.Redirect("CompanyManagement.aspx");
            }
            else // Edit New company
            {
                Company.CompanyId = CId;
                _presenterCompanyManagement.UpdateCompany();
                if (IsErrorOccured) return;
                Response.Redirect("CompanyManagement.aspx");
            }


        }
        private void Setcompany(int companyId)
        {
            Company = new Company { CompanyId = companyId };
            _presenterCompanyManagement.GetCompany();
            if (!IsErrorOccured)
            {
                CId = companyId;
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
            Savecompany();
        }
        #endregion

        #region Implement Interface
        public Company Company { get; set; }
        public Dictionary<int, string> RegionList { get; set; }
        #endregion
    }
}