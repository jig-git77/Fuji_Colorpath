using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProfilerOptionPresetsManagement : BaseControl, IViewDeviceProfilerSettings
    {
        private PresenterDeviceProfilerSettings _presenterDeviceProfilerSettings;

        public string ProfilerOptionPresetsManagement_DeleteSourceProfile_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("ProfilerOptionPresetsManagement_DeleteSourceProfile_Confirmation");
            }
        }

        #region Constructor
        public ProfilerOptionPresetsManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterDeviceProfilerSettings = new PresenterDeviceProfilerSettings(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            rgProfilerOptionPresetMangement.PageSize = Common.Utilities.PageSize;
            if (!Page.IsPostBack)
            {
                rgProfilerOptionPresetMangement.ItemCommand += rgProfilerOptionPresetMangement_ItemCommand;
                BindProfilerOptionPresetsList();
            }
        }

        protected void likAdd_Click(object sender, EventArgs e)
        {
            //SessionManager.Instance.CurrentMeasurementDevice = null;
            Response.RedirectPermanent("AddDeviceProfilerSettings.aspx");
        }
        public void BindProfilerOptionPresetsList()
        {
            _presenterDeviceProfilerSettings.GetProfilerOptionPresetsList();
            if (!IsErrorOccured)
            {
                rgProfilerOptionPresetMangement.DataSource = ProfilerOptionPresetsResponse;
                rgProfilerOptionPresetMangement.DataBind();
            }
        }
        #endregion
        #region Control Events
        protected void rgProfilerOptionPresetMangement_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                // hdfIsDelete = Convert.ToBoolean(item.FindControl("hdfIsDelete"));
                HiddenField hdfIsDelete = (HiddenField)item.FindControl("hdfIsDelete");
                int hdfIsDeleteId = Convert.ToInt32(hdfIsDelete.Value);
                //int currentRefID = Convert.ToInt32(hdfRefID.Value);
                //string selectedReferenceIDs = hdfSelectedReference.Value;
                //if (selectedReferenceIDs != "")
                //{
                //    var arrSelected = selectedReferenceIDs.Split(',');
                //    if (arrSelected.Contains(currentRefID.ToString()))
                //    {
                //        CheckBox chk = item.FindControl("hdfReferenceChartTypeID");
                //        chk.Checked = true;
                //    }
                //}
                if (hdfIsDeleteId == 0)
                {
                    ImageButton column = (ImageButton)item.FindControl("imgbtnDelete");
                    column.Attributes.Add("class", "gray-out");

                }
            }
        }
        protected void rgProfilerOptionPresetMangement_ItemCommand(object sender, GridCommandEventArgs e)
        {
            PresetsResponse = new Entity.ProfilerOptionPresetsResponse();
            switch (e.CommandName)
            {
                case "ClearFilter":
                    foreach (GridColumn column in rgProfilerOptionPresetMangement.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgProfilerOptionPresetMangement.MasterTableView.FilterExpression = string.Empty;
                    rgProfilerOptionPresetMangement.MasterTableView.Rebind();
                    break;
                case "Delete":
                    PresetsResponse.DeviceFamilyProfilerOptionID = (short)Convert.ToInt32(e.CommandArgument);
                    _presenterDeviceProfilerSettings.DeleteProfilerOption();
                    break;
                case "Edit":
                    PresetsResponse.DeviceFamilyProfilerOptionID = (short)Convert.ToInt32(e.CommandArgument);
                    string parms = Service.Common.Encryption.CryptHelper.EncryptString(PresetsResponse.DeviceFamilyProfilerOptionID.ToString(), FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey);
                    Response.Redirect("AddDeviceProfilerSettings.aspx?DeviceFamilyProfilerOptionID=" + ContentEncoder.Encode(parms));
                    break;
            }
        }
        protected void rgProfilerOptionPresetMangement_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _presenterDeviceProfilerSettings.GetProfilerOptionPresetsList();
            if (!IsErrorOccured)
            {
                rgProfilerOptionPresetMangement.DataSource = ProfilerOptionPresetsResponse;
            }
        }


        #region IViewDeviceProfilerSettings Members
        public ProfilerOptionPreset ProfilerOptionPreset { get; set; }
        public Dictionary<int, string> DeviceFamilyList { get; set; }
        public Dictionary<int, string> ProjectTypeList { get; set; }  // Public access modifier
        public DeviceFamilyProfilerOption DeviceFamilyProfilerOption { get; set; }
        public int DeviceFamilyProfilerOptionID { get; set; }
        public ProfilerOptionPresetsResponse PresetsResponse { get; set; }
        public List<ProfilerOptionPresetsResponse> ProfilerOptionPresetsResponse { get; set; }
        #endregion

        #endregion
    }

}