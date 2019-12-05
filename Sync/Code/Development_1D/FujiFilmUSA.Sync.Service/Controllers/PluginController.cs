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

        #region Contoller Events
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            _presenterLogin = new PresenterLogin(this);
            _presenterPlugin = new PresenterPluginDataService(this);
            base.Initialize(controllerContext);
        }
        #endregion

        #region RESTfull Services
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

                BindMeasurementPatches(measurement);

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

                BindMeasurementPatches(measurement);
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

                BindMeasurementPatches(measurement);
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

                BindMeasurementPatches(measurement);
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

        [HttpGet]
        public IHttpActionResult ValidateApplicationMeasurement(string optimizationRunID,string projectId,string SerialNo)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";
            try
            {

                if (string.IsNullOrEmpty(optimizationRunID))
                    validationMessage = "Invalid optimizeRunId.";
                if (string.IsNullOrEmpty(projectId))
                    validationMessage = "Invalid projectid.";
                if (string.IsNullOrEmpty(SerialNo))
                    validationMessage = "Invalid Serial Number.";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    return actionResult;
                }


                this.OptimizeRunId = Convert.ToInt32(optimizationRunID);
                this.ProjectId = Convert.ToInt32(projectId);
                this.MeaDeviceSerialNo = SerialNo;
                var presenter = new PresenterPluginDataService(this);
                presenter.ValidateApplicationMeasurement();
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, dtMeasurementValidation));
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
        #endregion

        #region Public Methods
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

        public void BindMeasurementPatches(Measurement measurement)
        {

            Measurement.MeasurementPatches = new List<MeasurementPatch>();
            foreach (var patch in Measurement.MeasurementAppPatches)
            {
                Measurement.MeasurementPatches.Add(new MeasurementPatch()
                {
                    MeasurementPatchId = patch.MeasurementPatchId,
                    MeasurementId = patch.MeasurementId,
                    SampleName = patch.SampleName,
                    CiexyzX = string.IsNullOrEmpty(patch.Ciexyz != null ? patch.Ciexyz.X : string.Empty) ? 0 : Convert.ToDecimal(patch.Ciexyz.X),
                    CiexyzY = string.IsNullOrEmpty(patch.Ciexyz != null ? patch.Ciexyz.Y : string.Empty) ? 0 : Convert.ToDecimal(patch.Ciexyz.Y),
                    CiexyzZ = string.IsNullOrEmpty(patch.Ciexyz != null ? patch.Ciexyz.Z : string.Empty) ? 0 : Convert.ToDecimal(patch.Ciexyz.Z),
                    CielabL = string.IsNullOrEmpty(patch.Ciexyz != null ? patch.Cielab.L : string.Empty) ? 0 : Convert.ToDecimal(patch.Cielab.L),
                    CielabA = string.IsNullOrEmpty(patch.Ciexyz != null ? patch.Cielab.A : string.Empty) ? 0 : Convert.ToDecimal(patch.Cielab.A),
                    CielabB = string.IsNullOrEmpty(patch.Ciexyz != null ? patch.Cielab.B : string.Empty) ? 0 : Convert.ToDecimal(patch.Cielab.B),
                    Δe = string.IsNullOrEmpty(patch.Δe) ? 0 : Convert.ToDecimal(patch.Δe),
                    Δe94 = string.IsNullOrEmpty(patch.Δe94) ? 0 : Convert.ToDecimal(patch.Δe94),
                    Δe2000 = string.IsNullOrEmpty(patch.Δe2000) ? 0 : Convert.ToDecimal(patch.Δe2000),
                    HtmlColor = patch.HtmlColor,
                    Density = new Density()
                    {
                        Red = string.IsNullOrEmpty(patch.Density.Red) ? 0 : Convert.ToDouble(patch.Density.Red),
                        Green = string.IsNullOrEmpty(patch.Density.Green) ? 0 : Convert.ToDouble(patch.Density.Green),
                        Blue = string.IsNullOrEmpty(patch.Density.Blue) ? 0 : Convert.ToDouble(patch.Density.Blue),
                        Visual = string.IsNullOrEmpty(patch.Density.Visual) ? 0 : Convert.ToDouble(patch.Density.Visual)
                    },
                    Spectral = new Spectral()
                    {
                        nm380 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm380 : string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm380),
                        nm390 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm390: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm390),
                        nm400 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm400: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm400),
                        nm410 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm410: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm410),
                        nm420 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm420: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm420),
                        nm430 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm430: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm430),
                        nm440 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm440: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm440),
                        nm450 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm450: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm450),
                        nm460 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm460: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm460),
                        nm470 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm470: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm470),
                        nm480 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm480: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm480),
                        nm490 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm490: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm490),
                        nm500 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm500: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm500),
                        nm510 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm510: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm510),
                        nm520 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm520: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm520),
                        nm530 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm530: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm530),
                        nm540 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm540: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm540),
                        nm550 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm550: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm550),
                        nm560 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm560: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm560),
                        nm570 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm570: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm570),
                        nm580 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm580: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm580),
                        nm590 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm590: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm590),
                        nm600 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm600: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm600),
                        nm610 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm610: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm610),
                        nm620 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm620: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm620),
                        nm630 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm630: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm630),
                        nm640 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm640: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm640),
                        nm650 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm650: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm650),
                        nm660 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm660: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm660),
                        nm670 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm670: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm670),
                        nm680 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm680: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm680),
                        nm690 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm690: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm690),
                        nm700 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm700: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm700),
                        nm710 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm710: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm710),
                        nm720 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm720: string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm720),
                        nm730 = string.IsNullOrEmpty(patch.Spectral != null ? patch.Spectral.nm730 : string.Empty) ? 0 : Convert.ToDouble(patch.Spectral.nm730),
                    },
                    SpotId = patch.SpotId,
                    SpotName = patch.SpotName
                });
            }

        }

        #endregion

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
        public ReferenceChartApp UserReferenceChartApp { get; set; }
        public string ProjectType { get; set; }
        public DataTable dtMeasurementValidation { get; set; }
        public int UserID { get; set; }
        public string MeaDeviceSerialNo { get; set; }
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
        public UserReferenceChart UserReferenceChart { get; set; }
        public string DeviceSerialNo { get; set; }
        public short InstrumentTypeID { get; set; }
        #endregion
    }
}
