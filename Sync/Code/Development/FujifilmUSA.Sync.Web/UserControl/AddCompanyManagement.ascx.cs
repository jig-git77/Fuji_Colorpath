using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Collections.Generic;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddCompanyManagement : System.Web.UI.UserControl, IViewAddCompanyManagement
    {
        private PresenterCompanyManagement _presenterCompanyManagement;
        
        #region Declaration
        private int CId
        {
            get { 
                     return  (ViewState["CompnayId"] != null && !string.IsNullOrEmpty(ViewState["CompnayId"].ToString())) ? Convert.ToInt32(ViewState["CompnayId"]) : 0; 
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
            if (!Page.IsPostBack)
            {
                BindRegion();
                if (Request.QueryString["CompanyId"] == null) //Add New Compnay 
                {
                }
                else //Edit Compnay
                {
                    SetCompnay(Convert.ToInt32(Request.QueryString["CompanyId"]), 
                                Request.QueryString["CompanyName"].ToString(), 
                                Convert.ToInt32(Request.QueryString["RegionId"]),
                                Convert.ToBoolean(Request.QueryString["IsActive"]));
                }
            }
        }
        #endregion

        #region Methods
        private void BindRegion()
        {
            RegionList = new Dictionary<int, string>();
            _presenterCompanyManagement.GetRegions();
            cmbRegionName.DataTextField = "Value";
            cmbRegionName.DataValueField = "Key";
            cmbRegionName.DataSource = RegionList;
            cmbRegionName.DataBind();
        }
        private void SaveCompnay()
        {
            Company = new Company();
            Company.CompanyName = txtCompanyName.Text;
            Company.IsActive = chkIsActive.Checked;
            Company.RegionId = Convert.ToByte(cmbRegionName.SelectedValue);
            if (CId == 0) // Add New Compnay
            {
                _presenterCompanyManagement.InsertCompany();
                Response.Redirect("CompanyManagement.aspx");
            }
            else // Edit New Compnay
            {
                Company.CompanyId = CId;
                _presenterCompanyManagement.UpdateCompnay();
                Response.Redirect("CompanyManagement.aspx");
            }


        }
        private void SetCompnay(int compnayId,string companyName, int regionId,bool isActive)
        {
            CId = compnayId;
            txtCompanyName.Text = companyName;
            cmbRegionName.SelectedValue = regionId.ToString();
            chkIsActive.Checked = isActive;
        }
        #endregion

        #region Control Events
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            SaveCompnay();
        }
        #endregion

        #region Implement Interface
        public Company Company { get; set; }
        public Dictionary<int, string> RegionList { get; set; }
        #endregion

    }
}