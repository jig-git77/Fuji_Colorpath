using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Newtonsoft.Json;

namespace FujiFilmUSA.Sync.Service.Controllers
{
    public class LogInController : BaseController, IViewLogin
    {

        #region Variable Declarations
        private PresenterLogin _presenterlogin;
        #endregion

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            _presenterlogin = new PresenterLogin(this);
            base.Initialize(controllerContext);
        }

        [HttpGet]
        public IHttpActionResult UserLogIn(string UserName, string Password)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {
                if (UserName == null)
                    validationMessage = "Please provide User name.";

                if (Password == null)
                    validationMessage = "Please provide password";


                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {

                    this.UserName = UserName;
                    this.Password = Password;
                    _presenterlogin.UserLoginFromApplication();

                    if (UserReferenceChart.User != null && UserReferenceChart.User.LoginId != null)
                    {
                        if (UserReferenceChart.ReferenceChartData != null)
                        {
                            //var jsonReferenceChart = JsonConvert.SerializeObject(UserReferenceChart);

                            //httpResponse.Data = jsonReferenceChart;
                            actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, UserReferenceChart));
                        }
                        else
                        {
                            BaseErrorResponse errprResponse = new BaseErrorResponse();
                            errprResponse.Error.UserMessage = "Reference chart is not available.";
                            errprResponse.Error.Code = (int)ResponseStatus.Business_InvalidData;
                            actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                        }
                    }
                    else
                    {
                        BaseErrorResponse errprResponse = new BaseErrorResponse();
                        errprResponse.Error.UserMessage = "Invalid Username or Password.";
                        errprResponse.Error.Code = (int)ResponseStatus.Business_InvalidData;
                        actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    }

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

        [HttpGet]
        public IHttpActionResult UserLogInByDevice(string SerialNumber, short? InstrumentType)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {

                if (SerialNumber == null)
                    validationMessage = "Please provide Serial Number.";

                if (InstrumentType == null)
                    validationMessage = "Please provide Instrument Type";

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {

                    this.DeviceSerialNo = SerialNumber;
                    this.InstrumentTypeID = Convert.ToInt16(InstrumentType);
                    _presenterlogin.UserLoginFromApplicationBySerial();

                    if (UserReferenceChartApp != null)
                    {
                        //var jsonReferenceChart = JsonConvert.SerializeObject(UserReferenceChart);

                        //httpResponse.Data = jsonReferenceChart;
                        actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, UserReferenceChartApp));
                    }
                    else
                    {
                        BaseErrorResponse errprResponse = new BaseErrorResponse();
                        errprResponse.Error.UserMessage = "Reference chart is not available.";
                        errprResponse.Error.Code = (int)ResponseStatus.Business_InvalidData;
                        actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                    }
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
        public ReferenceChartApp UserReferenceChartApp { get; set; }
        public string DeviceSerialNo { get; set; }
        public short InstrumentTypeID { get; set; }
        #endregion
    }
}
