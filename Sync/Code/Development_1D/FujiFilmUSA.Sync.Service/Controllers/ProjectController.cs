using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujiFilmUSA.Sync.Service.Models;
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
            base.Initialize(controllerContext);
        }

        public IHttpActionResult GetProjectDetailsByDeviceID(int deviceID)
        {
            IHttpActionResult actionResult = null;
            //BaseResponse<List<Project>> httpResponse = new BaseResponse<List<Project>>();
            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {
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
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }
            return actionResult;
        }

        public IHttpActionResult GetSpotProjectDetailsByProjectID(int projectID)
        {
            IHttpActionResult actionResult = null;
            //BaseResponse<List<SpotProjectLibrary>> httpResponse = new BaseResponse<List<SpotProjectLibrary>>();
            BaseResponse<string> httpResponse = new BaseResponse<string>();
            try
            {
                ProjectId = projectID;
                _presenterProjectList.GetSpotLibraryListByProjectId();
                var json = JsonConvert.SerializeObject(SpotProjectLibraries);
                httpResponse.Data = json;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.Message = ex.Message;
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
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }
            return actionResult;
        }

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
                errprResponse.Error.Message = ex.Message;
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
        public List<FujifilmUSA.Sync.Entity.CPSpot.SpotProjectLibrary> SpotProjectLibraries { get; set; }
        public int SpotProjectId { get; set; }

        #endregion
    }
}
