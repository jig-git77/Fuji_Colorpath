using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace FujiFilmUSA.Sync.Service.Controllers
{
    public class PluginController : BaseController, IViewPluginDataService, IViewLogin
    {
        #region Variable Declarations
        private PresenterLogin _presenterLogin;
        #endregion

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            _presenterLogin = new PresenterLogin(this);
            base.Initialize(controllerContext);
        }

        [HttpGet]
        public IHttpActionResult GetReferenceChart(string projectId, string referenceChartDisplayType, string IsSpotProjectType)
        {
            string tocken = "xqHhpNrOm7dIGO74N+XTc7kC4ZGpfeFthvpB7H8NDSQ=";

            IHttpActionResult actionResult = null;            
            try
            {
                this.EncyptedTocken = tocken;
                if (_presenterLogin.ValidateUser())
                {
                    this.ReferenceChartDisType = (EReferenceChartDisType)Enum.Parse(typeof(EReferenceChartDisType), referenceChartDisplayType);
                    var presenter = new PresenterPluginDataService(this);
                    if (Convert.ToBoolean(IsSpotProjectType) == false)
                    {
                        this.ProjectId = Convert.ToInt32(projectId);
                        presenter.GetPluginReferenceChartData();
                    }
                    else
                    {
                        this.SpotProjectId = Convert.ToInt32(projectId);
                        presenter.GetPluginSpotReferenceChartData();
                    }
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, this.ReferenceChartPlugin));

                }
                else
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = "User is invalid.";
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized, errprResponse));//returns the 500 status code
                }                
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
        public IHttpActionResult SaveMeasurement(Measurement measurement)
        {
            IHttpActionResult actionResult = null;
            try
            {
                this.Measurement = measurement;
                var presenter = new PresenterPluginDataService(this);
                presenter.SaveMeasurement();
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, true));
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
        public IHttpActionResult SaveVMeasurement(Measurement measurement)
        {
            IHttpActionResult actionResult = null;
            try
            {
                this.Measurement = measurement;
                var presenter = new PresenterPluginDataService(this);
                presenter.SaveVMeasurement();
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, true));
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
        public IHttpActionResult Save1DMeasurement(Measurement measurement)
        {
            IHttpActionResult actionResult = null;
            try
            {
                this.Measurement = measurement;
                var presenter = new PresenterPluginDataService(this);
                presenter.Save1DMeasurement();

                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, true));
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

        [HttpGet]
        public IHttpActionResult GetMeasurementsByOptimizeId(string optimizeRunId)
        {
            IHttpActionResult actionResult = null;
            try
            {
                this.OptimizeRunId = Convert.ToInt32(optimizeRunId);
                var presenter = new PresenterPluginDataService(this);
                presenter.GetMeasurementsOnRunId();
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, Measurements));
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


        [HttpDelete]
        public IHttpActionResult DeleteMeasurement(string measurementId)
        {
            IHttpActionResult actionResult = null;
            try
            {
                MeasurementId = Convert.ToInt32(measurementId);
                var presenter = new PresenterPluginDataService(this);
                presenter.DeleteMeasurement();
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, true));
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
        

        #region IViewPluginDataService Members
        public Measurement Measurement { get; set; }
        public int ProjectId { get; set; }
        public ReferenceChartData ReferenceChartData { get; set; }
        public int OptimizeRunId { get; set; }
        public List<Measurement> Measurements { get; set; }
        public int MeasurementId { get; set; }
        public EReferenceChartDisType ReferenceChartDisType { get; set; }
        public int SpotProjectId { get; set; }
        public ReferenceChartPlugin ReferenceChartPlugin { get; set; }
        #endregion

        #region IViewTaskeroUser Members
        public string UserName { get; set; }
        public string Password { get; set; }
        public User CurrentUser { get; set; }

        public int UserId { get; set; }
        public string OSName { get; set; }
        public string BrowserName { get; set; }
        public string EntryDate { get; set; }

        public string EncyptedTocken { get; set; }
        #endregion
    }
}
