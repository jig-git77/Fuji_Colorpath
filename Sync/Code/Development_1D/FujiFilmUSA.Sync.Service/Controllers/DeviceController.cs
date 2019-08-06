using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujiFilmUSA.Sync.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace FujiFilmUSA.Sync.Service.Controllers
{
    public class DeviceController : BaseController, IViewDevice
    {
        #region "Declaration"
        private PresenterDevice _presenterdevice;
        #endregion
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            _presenterdevice = new PresenterDevice(this);
            base.Initialize(controllerContext);
        }

        [CustomAuthentication]
        public IHttpActionResult GetProjectLicenseType(int UserID, int DeviceID)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<List<LicenseType>> httpResponse = new BaseResponse<List<LicenseType>>();
            try
            {
                DeviceId = DeviceID;
                CurrentUserId = UserID;
                _presenterdevice.DeviceListByDeviceId();

                var licenseTypeList = new List<LicenseType>();
                if (SelectedDevice != null && SelectedDevice.LicenseTypeProject != null && SelectedDevice.LicenseTypeProject.Count > 0)
                {
                    foreach (KeyValuePair<ELicenseType, int> item in SelectedDevice.LicenseTypeProject)
                        licenseTypeList.Add(new LicenseType() { LicenseTypeId = Convert.ToInt16(item.Key), Type = Utilities.GetEnumDescriptionAttributeValue(item.Key) });                        
                }
                httpResponse.Data = licenseTypeList;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }
            return actionResult;
        }

        #region IViewDevice Members
        public int TaskeroUserId { get; set; }
        public int RoleId { get; set; }
        public int CompanyId
        {
            get
            {
                return SessionManager.Instance.SelectedCompany;
            }
            set
            {
                SessionManager.Instance.SelectedCompany = value;
            }
        }

        public int BranchId
        {
            get
            {
                return SessionManager.Instance.SelectedBranch;
            }
            set
            {
                SessionManager.Instance.SelectedBranch = value;
            }
        }

        public MeasurementDevice MeasurementDevice { get; set; }
        public List<MeasurementDevice> MeasurementDevices { get; set; }
        public Dictionary<int, string> BranchList { get; set; }
        public Dictionary<int, string> CompanyList { get; set; }
        public Dictionary<int, string> LicenseList { get; set; }
        public Dictionary<int, string> InstrumentTypeList { get; set; }
        public List<Device> Devices { get; set; }
        public int DeviceId { get; set; }
        public Device SelectedDevice { get; set; }
        #endregion
    }


}
