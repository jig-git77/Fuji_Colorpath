using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
        private PresenterPluginDataService _presenterPlugin;
        #endregion

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            _presenterLogin = new PresenterLogin(this);
            _presenterPlugin = new PresenterPluginDataService(this);
            base.Initialize(controllerContext);
        }

        [HttpGet]
        public IHttpActionResult GetReferenceChart(string projectId, string referenceChartDisplayType, string IsSpotProjectType)
        {
            //string tocken = "xqHhpNrOm7dIGO74N+XTc7kC4ZGpfeFthvpB7H8NDSQ=";
            string validationMessage = "";
            IHttpActionResult actionResult = null;
            try
            {
                if (projectId == null)
                    validationMessage = "Invalid Project ID.";
                if (referenceChartDisplayType == null)
                    validationMessage = "Invalid Reference Chart Display Type.";
                if (IsSpotProjectType == null)
                    validationMessage = "Invalid Is spot project type.";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    return actionResult;
                }


                //this.EncyptedTocken = tocken;
                //if (_presenterLogin.ValidateUser())
                if (true)
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
            string validationMessage = "";
            IHttpActionResult actionResult = null;
            try
            {

                if (measurement == null)
                    validationMessage = "Invalid measurements.";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    return actionResult;
                }

                this.Measurement = measurement;
                this.ProjectId = measurement.ProjectId;
                this.OptimizeRunId = measurement.OptimizeRunId;
                this.ProjectType = "Sync4D";

                bool isValid = CheckMeasurementValidation(ref actionResult);
                if (!isValid)
                    return actionResult;

                _presenterPlugin.SaveMeasurement();
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, true));

                //var presenter = new PresenterPluginDataService(this);                
                //presenter.GetMeasurementValidationData();

                //string errorMessage = "";
                //if (dtMeasurementValidation.Rows.Count > 0)
                //{
                //    if (!Convert.ToBoolean(dtMeasurementValidation.Rows[0]["ProjectAvailable"]))
                //    {
                //        errorMessage = "Project is not available.";
                //    }
                //    else if (!Convert.ToBoolean(dtMeasurementValidation.Rows[0]["OptimizationRunAvailable"]))
                //    {
                //        errorMessage = "OptimizeRun is not available.";
                //    }
                //    else
                //    {
                //        if(Convert.ToInt16(dtMeasurementValidation.Rows[0]["OptimizationRunAvailable"]) != (int)EOptimizeRunStatus.Open)
                //        {
                //            errorMessage = "OptimizeRun is not in open status.";
                //        }
                //    }
                //    if (!string.IsNullOrEmpty(errorMessage))
                //    {
                //        BaseErrorResponse errprResponse = new BaseErrorResponse();
                //        errprResponse.Error.UserMessage = errorMessage;
                //        errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                //        actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                //        return actionResult;
                //    }

                //    presenter.SaveMeasurement();
                //    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, true));
                //}
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
            string validationMessage = "";           
            try
            {

                if (measurement == null)
                    validationMessage = "Invalid measurements.";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    return actionResult;
                }

                this.Measurement = measurement;
                this.ProjectId = measurement.ProjectId;
                this.OptimizeRunId = measurement.OptimizeRunId;
                this.ProjectType = "Validation";

                bool isValid = CheckMeasurementValidation(ref actionResult);
                if (!isValid)
                    return actionResult;

                this.Measurement = measurement;                
                _presenterPlugin.SaveVMeasurement();
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
            string validationMessage = "";
            try
            {

                if (measurement == null)
                    validationMessage = "Invalid measurements.";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    return actionResult;
                }

                this.Measurement = measurement;
                this.ProjectId = measurement.ProjectId;
                this.OptimizeRunId = measurement.OptimizeRunId;
                this.ProjectType = "Sync1D";

                bool isValid = CheckMeasurementValidation(ref actionResult);
                if (!isValid)
                    return actionResult;

                //var presenter = new PresenterPluginDataService(this);
                _presenterPlugin.Save1DMeasurement();
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
        public IHttpActionResult SaveSpotMeasurement(Measurement measurement)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";
            try
            {

                if (measurement == null)
                    validationMessage = "Invalid spot measurements.";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    return actionResult;
                }

                this.Measurement = measurement;
                var presenter = new PresenterPluginDataService(this);
                presenter.SaveSpotMeasurement();

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
            string validationMessage = "";
            try
            {

                if (optimizeRunId == null)
                    validationMessage = "Invalid optimizeRunId.";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    return actionResult;
                }

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

        [HttpGet]
        public IHttpActionResult Get1DMeasurementsByOptimizeId(string optimizeRunId)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";
            try
            {

                if (optimizeRunId == null)
                    validationMessage = "Invalid optimizeRunId.";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    return actionResult;
                }


                this.OptimizeRunId = Convert.ToInt32(optimizeRunId);
                var presenter = new PresenterPluginDataService(this);
                presenter.Get1DMeasurementsOnRunId();
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

        [HttpGet]
        public IHttpActionResult GetVMeasurementsByProjectId(string projectId)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";
            try
            {

                if (projectId == null)
                    validationMessage = "Invalid projectId.";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    return actionResult;
                }


                this.ProjectId = Convert.ToInt32(projectId);
                var presenter = new PresenterPluginDataService(this);
                presenter.GetVMeasurementsOnProjectId();
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

        [HttpGet]
        public IHttpActionResult GetSpotMeasurementsByOptimizeId(string optimizeRunId)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";
            try
            {

                if (optimizeRunId == null)
                    validationMessage = "Invalid optimizeRunId.";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    return actionResult;
                }

                this.OptimizeRunId = Convert.ToInt32(optimizeRunId);
                var presenter = new PresenterPluginDataService(this);
                presenter.GetSpotMeasurementsOnRunId();
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
            string validationMessage = "";
            try
            {

                if (measurementId == null)
                    validationMessage = "Invalid measurementId.";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    return actionResult;
                }

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

        [HttpDelete]
        public IHttpActionResult DeleteVMeasurement(string measurementId)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";
            try
            {

                if (measurementId == null)
                    validationMessage = "Invalid measurementId.";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    return actionResult;
                }

                MeasurementId = Convert.ToInt32(measurementId);
                _presenterPlugin.DeleteVMeasurement();
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

        [HttpDelete]
        public IHttpActionResult Delete1DMeasurement(string measurementId)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";
            try
            {

                if (measurementId == null)
                    validationMessage = "Invalid measurementId.";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    return actionResult;
                }

                MeasurementId = Convert.ToInt32(measurementId);
                _presenterPlugin.Delete1DMeasurement();
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

        [HttpDelete]
        public IHttpActionResult DeleteSpotMeasurement(string measurementId)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";
            try
            {

                if (measurementId == null)
                    validationMessage = "Invalid measurementId.";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    return actionResult;
                }

                MeasurementId = Convert.ToInt32(measurementId);
                _presenterPlugin.DeleteSpotMeasurement();
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

        public bool CheckMeasurementValidation(ref IHttpActionResult actionResult)
        {
            _presenterPlugin.GetMeasurementValidationData();
            string errorMessage = "";
            if (dtMeasurementValidation.Rows.Count > 0)
            {
                if (!Convert.ToBoolean(dtMeasurementValidation.Rows[0]["ProjectAvailable"]))
                {
                    errorMessage = "Project is not available.";
                }
                else if (this.ProjectType != "Validation" && !Convert.ToBoolean(dtMeasurementValidation.Rows[0]["OptimizationRunAvailable"]))
                {
                    errorMessage = "OptimizeRun is not available.";
                }
                else
                {
                    if (this.ProjectType != "Validation" && Convert.ToInt16(dtMeasurementValidation.Rows[0]["OptimizationRunAvailable"]) != (int)EOptimizeRunStatus.Open)
                    {
                        errorMessage = "OptimizeRun is not in open status.";
                    }
                }
                if (!string.IsNullOrEmpty(errorMessage))
                {

                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = errorMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code                    
                    return false;
                }
                return true;
            }
            return false;
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
        public string ProjectType { get; set; }
        public DataTable dtMeasurementValidation { get; set; }
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
