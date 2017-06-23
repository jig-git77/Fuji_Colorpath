using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ApplicationConfiguration : BaseControl, IViewApplicationConfiguration
    {
        private PresenterApplicationConfiguration _presenterApplicationConfiguration;

        #region Private Variables - Globalization
        private const string _recConfigValueSaveSuccess = "configValueSaveSuccess";

        #endregion

        public ApplicationConfiguration()
        {            
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #region Events
        protected override void OnInit(EventArgs e)
        {
            _presenterApplicationConfiguration = new PresenterApplicationConfiguration(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                FillData();
            }         
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            List<Entity.ApplicationConfiguration> updatedApplicationConfiguration = new List<Entity.ApplicationConfiguration>();
            foreach (GridDataItem item in rgApplicationConfiguration.MasterTableView.Items)
            {
                int id = 0;
                HiddenField hdnConfigurationId = item.FindControl("hdnConfigurationId") as HiddenField;
                if (hdnConfigurationId != null)
                    id = string.IsNullOrEmpty(hdnConfigurationId.Value.Trim()) ? 0 : Convert.ToInt32(hdnConfigurationId.Value);

                string configKey = string.Empty;
                Label lblKey = item.FindControl("lblKey") as Label;
                if (lblKey != null)
                    configKey = lblKey.Text.Trim();

                string configValue = string.Empty;
                TextBox txtValue = item.FindControl("txtValue") as TextBox;
                if (txtValue != null)
                    configValue = txtValue.Text.Trim();

                if (id != 0 && !string.IsNullOrEmpty(configValue))
                {
                    updatedApplicationConfiguration.Add(new Entity.ApplicationConfiguration()
                        {
                            ApplicationConfigurationId = id,
                            ConfigurationKey = configKey,
                            ConfigurationValue = configValue
                        });
                }
            }

            if (updatedApplicationConfiguration.Count > 0)
            {
                ApplicationConfigurationList = updatedApplicationConfiguration;
                _presenterApplicationConfiguration.UpdateApplicationConfiguration();

                if (!IsErrorOccured)
                {
                    //Display success message
                    fujiStatusbar.DisplayMessage(GetResourceString(_recConfigValueSaveSuccess), true);
                    //Refresh grid
                    FillData();
                }                
            }
        }

        protected void rgApplicationConfiguration_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!IsErrorOccured)
            {
                rgApplicationConfiguration.DataSource = this.ApplicationConfigurationList;
            }
        }
        #endregion

        #region Methods
        private void FillData()
        {
            _presenterApplicationConfiguration.GetApplicationConfiguration();
            rgApplicationConfiguration.Rebind();
        }
        #endregion

        #region IViewApplicationConfiguration Implementation
        public bool IsSingleUserLoginEnabled { get; set; }

        public int KeepUserAliveInSeconds { get; set; }

        public List<Entity.ApplicationConfiguration> ApplicationConfigurationList { get; set; }
        #endregion
    }
}