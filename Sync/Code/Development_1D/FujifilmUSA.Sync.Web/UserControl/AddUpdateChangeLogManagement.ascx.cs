using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddUpdateChangeLogManagement : BaseControl, IViewUpdateChangeLog
    {
        #region Declartion
        private PresenterUpdateChangeLog _presenterUpdateChangeLogManagement;
        private int UpdateListID
        {
            get
            {
                return (ViewState["UpdateListID"] != null && !string.IsNullOrEmpty(ViewState["UpdateListID"].ToString())) ? Convert.ToInt16(ViewState["UpdateListID"]) : Convert.ToInt16(0);
            }
            set { ViewState["UpdateListID"] = value.ToString(); }
        }
        public AddUpdateChangeLogManagement()
        {
            Load += (sender, e) => ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        #region Event
        protected override void OnInit(EventArgs e)
        {
            _presenterUpdateChangeLogManagement = new PresenterUpdateChangeLog(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack)
            {
                return;
            }

            //lblHeader.Text = "Add Update Change Log";

            if (Request.QueryString["UpdateListID"] != null)
            {
                var UpdateListIDtemp = Request.QueryString["UpdateListID"];
                var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(UpdateListIDtemp, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                int _UpdateListID;
                if (Int32.TryParse(id, out _UpdateListID))
                {
                    lblHeader.Text = "Edit Update Change Log";
                    SetUpdateChangeLog(_UpdateListID);
                }
            }
            BindResourceValidationMessages();
        }
        private void BindResourceValidationMessages()
        {
            rfvValHeaderLine.ValidationMsg = GetResourceString("rfvValHeaderLine");
            rfvVSubHeaderLine.ValidationMsg = GetResourceString("rfvVSubHeaderLine");
            rfvActivateContainer.ValidationMsg = GetResourceString("rfvActivateContainer");
            rfvExpiraDateContainer.ValidationMsg = GetResourceString("rfvExpiraDateContainer");
            rfvMinReqVersionContainer.ValidationMsg = GetResourceString("rfvMinReqVersionContainer");
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SaveUpdateChangeLog();
        }
        #endregion

        #region Method
        private void SaveUpdateChangeLog()
        {
            objUpdateChangeLog = new Entity.UpdateChangeLog()
            {

                HeaderLine = txtHeaderLine.Text,
                SubHeaderLine = txtSubHeaderLine.Text,
                Description = txtDescription.Text,
                EntryDate = txtActivationDateNew.SelectedDate.Value.Date,
                ExpirationDate = txtExpirationDateNew.SelectedDate.Value.Date,
                IsActive = chkIsActive.Checked,
                IsAlert = chkIsAlert.Checked
            };
            if (UpdateListID == 0) // Add New Record UpdateChangeLogManagement
            {
                _presenterUpdateChangeLogManagement.InsertUpdateChangeLogs();
                if (IsErrorOccured)
                {
                    return;
                }
            }
            else // Edit New UpdateChangeLogManagement
            {
                objUpdateChangeLog.UpdateListID = UpdateListID;
                _presenterUpdateChangeLogManagement.InsertUpdateChangeLogs();
                if (IsErrorOccured)
                {
                    return;
                }
            }
            Response.Redirect("UpdateChangeLogManagement.aspx");
        }
        private void SetUpdateChangeLog(int updateListID)
        {
            objUpdateChangeLog = new Entity.UpdateChangeLog { UpdateListID = updateListID };
            _presenterUpdateChangeLogManagement.GetAllUpdateChangeLogs();
            if (!IsErrorOccured)
            {
                UpdateListID = updateListID;
                txtHeaderLine.Text = Convert.ToString(resultData.Tables[0].Rows[0]["HeaderLine"]);
                txtSubHeaderLine.Text = Convert.ToString(resultData.Tables[0].Rows[0]["SubHeaderLine"]);
                txtDescription.Text = Convert.ToString(resultData.Tables[0].Rows[0]["Description"]);
                txtActivationDateNew.SelectedDate = Convert.ToDateTime(Convert.ToString(resultData.Tables[0].Rows[0]["EntryDate"]));
                txtExpirationDateNew.SelectedDate = Convert.ToDateTime(Convert.ToString(resultData.Tables[0].Rows[0]["ExpirationDate"]));
                chkIsActive.Checked = Convert.ToBoolean(resultData.Tables[0].Rows[0]["IsActive"]);
                chkIsAlert.Checked = Convert.ToBoolean(resultData.Tables[0].Rows[0]["IsAlert"]);
            }
        }
        #endregion

        #region Implement Interface IViewUpdateChangeLog
        public List<Entity.UpdateChangeLog> UpdateChangeLogList { get; set; }
        public DataSet resultData { get; set; }
        public Entity.UpdateChangeLog objUpdateChangeLog { get; set; }
        #endregion
    }
}