using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity.CPSpot;
using System;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using FujifilmUSA.Sync.GlobalResources;
using FujiFilmUSA.Sync.Service.Models;

namespace FujiFilmUSA.Sync.Service.Controllers
{
    public class ProjectController : BaseController, IViewProjectList
    {
        #region Variable Declarations
        private PresenterProjectList _presenterProjectList;
        private Dictionary<string, string> _resourceValues { get; set; }
        private Dictionary<string, string> _globalResourceValues { get; set; }
        #endregion
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            _presenterProjectList = new PresenterProjectList(this);
            _resourceValues = new Dictionary<string, string>();
            _globalResourceValues = new Dictionary<string, string>();
            _resourceValues.Add("GridDateTimeColumnResource1", "GridDateTimeColumnResource1");
            _resourceValues.Add("GridTemplateColumnResource0", "GridTemplateColumnResource0");
            _resourceValues.Add("GridTemplateColumnResource1", "GridTemplateColumnResource1");
            _resourceValues.Add("GridTemplateColumnResource2", "GridTemplateColumnResource2");
            _resourceValues.Add("GridTemplateColumnResource3", "GridTemplateColumnResource3");
            _resourceValues.Add("GridTemplateColumnResource4", "GridTemplateColumnResource4");
            _resourceValues.Add("GridTemplateColumnResource5", "GridTemplateColumnResource5");
            _resourceValues.Add("GridTemplateColumnResource6", "GridTemplateColumnResource6");
            _resourceValues.Add("ToolTipClickToShort", "ToolTipClickToShort");
            _resourceValues.Add("GridTemplateColumnResourceLibrary", "GridTemplateColumnResourceLibrary");
            


            _resourceValues.Add("ELicenseType_DLP", "ELicenseType_DLP");
            _resourceValues.Add("ELicenseType_Sync1D", "ELicenseType_Sync1D");
            _resourceValues.Add("ELicenseType_Sync4DCertification", "ELicenseType_Sync4DCertification");
            _resourceValues.Add("ELicenseType_InkOptimization", "ELicenseType_InkOptimization");
            _resourceValues.Add("ELicenseType_InkOptimization_Source", "ELicenseType_InkOptimization_Source");
            _resourceValues.Add("ELicenseType_SpotLibrary", "ELicenseType_SpotLibrary");
            _resourceValues.Add("ELicenseType_Validation", "ELicenseType_Validation");
            _resourceValues.Add("ValidationStatus_LastOK", "ValidationStatus_LastOK");
            _resourceValues.Add("ValidationStatus_NotCertified", "ValidationStatus_NotCertified");
            _resourceValues.Add("ValidationStatus_LastFailed", "ValidationStatus_LastFailed");
            _resourceValues.Add("ValidationStatus_Disabled", "ValidationStatus_Disabled");
            _resourceValues.Add("ValidationStatus_None", "ValidationStatus_None");
            _resourceValues.Add("ValidationStatus_NoMeasurement", "ValidationStatus_NoMeasurement");
            _resourceValues.Add("OpenProject", "OpenProject");
            _resourceValues.Add("MsgDeleteProject", "MsgDeleteProject");
            _resourceValues.Add("CreateNewLibrary", "CreateNewLibrary");
            _resourceValues.Add("MsgNoRecords", "MsgNoRecords");
            _resourceValues.Add("ClearFillterResource", "ClearFillterResource");

            _resourceValues.Add("SpotProject_Open", "SpotProject_Open");
            _resourceValues.Add("SpotProject_Certified", "SpotProject_Certified");

            base.Initialize(controllerContext);
        }
        
        //[CustomAuthentication]
        public IHttpActionResult GetProjectDetailsByDeviceID(int deviceID)
        {
            IHttpActionResult actionResult = null;
            //Models.BaseResponse<List<Project>> httpResponse = new Models.BaseResponse<List<Project>>();
            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {
                DetermineCulture();
                Projects = new List<Project>();
                DeviceId = deviceID;
                //Now we are passing LicenseTypeID as None to bring all projects based on DeviceId
                LicenseTypeID = Convert.ToInt32(ELicenseType.None);
                _presenterProjectList.GetProjectListByDeviceId();
                GenerateResources();
                var objGroup = new 
                {
                    Projects = Projects,
                    GlobalResources = _globalResourceValues
                };
                var json = JsonConvert.SerializeObject(objGroup);
                httpResponse.Data = json;
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

        public IHttpActionResult GetSpotProjectDetailsByProjectID(int projectID)
        {
            IHttpActionResult actionResult = null;
            //Models.BaseResponse<List<SpotProjectLibrary>> httpResponse = new Models.BaseResponse<List<SpotProjectLibrary>>();
            BaseResponse<string> httpResponse = new BaseResponse<string>();
            try
            {
                GenerateResources();
                ProjectId = projectID;
                _presenterProjectList.GetSpotLibraryListByProjectId();
                var json = JsonConvert.SerializeObject(SpotProjectList);
                httpResponse.Data = json;
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

        [HttpPost]
        public IHttpActionResult DeleteProject(int projectID)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<bool> httpResponse = new BaseResponse<bool>();
            try
            {
                ProjectId = projectID;
                _presenterProjectList.DeleteProject();
                httpResponse.Data = true;
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

        [CustomAuthentication]
        [HttpPost]
        public IHttpActionResult DeleteSpotProject(int spotProjectID)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<bool> httpResponse = new BaseResponse<bool>();
            try
            {

                SpotProjectId = spotProjectID;
                _presenterProjectList.DeleteSpotProject();
                httpResponse.Data = true;
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


        public void GenerateResources()
        {
            foreach (var item in _resourceValues)
            {
                _globalResourceValues.Add(item.Key, GlobalResourceManager.Instance.GetGlobalResourceString(item.Value));
            }
        }

        #region IViewProjectList Members
        public List<Project> Projects { get; set; }
        public int CompanyId { get; set; }
        public Dictionary<int, string> SourceProfile { get; set; }
        public int ProjectId { get; set; }
        public int DeviceId { get; set; }
        public int LicenseTypeID { get; set; }
        public Project Project { get; set; }
        //public List<FujifilmUSA.Sync.Entity.CPSpot.SpotProjectLibrary> SpotProjectLibraries { get; set; }
        public SpotProjectList SpotProjectList { get; set; }
        public int SpotProjectId { get; set; }

        #endregion
    }
}
