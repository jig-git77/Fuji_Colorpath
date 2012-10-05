using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ActivityValidationRules : BaseControl, IViewActivityValidationRule
    {
        private PresenterActivityValidationRule _presenterActivityValidationRule;

        #region Private Variables - Globalization
        private const string _recRuleUpdateSuccessMsg = "ruleUpdateSuccessMsg";
        private const string _recRuleSelectionRequiredMsg = "ruleSelectionRequiredMsg";
        #endregion
        public ActivityValidationRules()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #region Events
        protected override void OnInit(EventArgs e)
        {
            _presenterActivityValidationRule = new PresenterActivityValidationRule(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillActivityTypeList();
            }
        }
        #endregion

        #region Methods
        private void FillActivityTypeList()
        {
            ddlActivityList.DataValueField = "Key";
            ddlActivityList.DataTextField = "Value";
            //Dictionary<int, string> activityTypeList = Common.Utilities.GetEnumDescriptionAttributeDropdown<LogActivityType>();
            //ddlActivityList.DataSource = activityTypeList.Where(c => c.Key != 0).ToDictionary<int,string>(c=>c.Key);
            ddlActivityList.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<LogActivityType>();
            ddlActivityList.DataBind();
            ddlActivityList.Items.Remove(ddlActivityList.Items.FindByValue("0"));
            ddlActivityList.SelectedIndex = 0;

            FillActivityList((LogActivityType)Convert.ToInt32(ddlActivityList.SelectedItem.Value));
        }

        private void FillActivityList(LogActivityType activityType)
        {
            ActivityType = activityType;
            _presenterActivityValidationRule.GetActivityValidationRules();
            rgActivityList.Rebind();
        }
        #endregion

        #region Control Events
        protected void ddlActivityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillActivityList((LogActivityType)Convert.ToInt32(ddlActivityList.SelectedItem.Value));
        }

        protected void rgActivityList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!IsErrorOccured)
            {
                rgActivityList.DataSource = ActivityValidationRule;
            }
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            ActivityValidationRule = new List<ActivityValidation>();
            ActivityType = (LogActivityType)Convert.ToInt32(ddlActivityList.SelectedItem.Value);
            foreach (GridDataItem item in rgActivityList.MasterTableView.Items)
            {
                if (item != null)
                {
                    CheckBox chkRule = item.FindControl("chkRule") as CheckBox;
                    HiddenField hdnValidationRuleId = item.FindControl("hdnValidationRuleId") as HiddenField;
                    if (hdnValidationRuleId != null && chkRule != null && chkRule.Checked)
                    {
                        ActivityValidationRule.Add(new ActivityValidation()
                        {                         
                            ValidationRule = new ValidationRule()
                            {
                                ValidationRuleId = Convert.ToInt32(hdnValidationRuleId.Value)
                            }
                        });
                    }
                }
            }
          

            if (ActivityValidationRule != null)
            {
                _presenterActivityValidationRule.UpdateActivityValidationRules();
                fujiStatusbar.DisplayMessage(GetResourceString(_recRuleUpdateSuccessMsg), false);
                FillActivityList((LogActivityType)Convert.ToInt32(ddlActivityList.SelectedItem.Value));
            }
        }
        #endregion

        #region IViewActivityValidationRule Members
        public List<ActivityValidation> ActivityValidationRule
        {
            get;
            set;
        }

        public LogActivityType ActivityType
        {
            get;
            set;
        }
        #endregion
    }
}