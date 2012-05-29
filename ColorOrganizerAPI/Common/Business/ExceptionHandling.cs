using System.Net;
using System.ServiceModel;
using System.Web.Services.Protocols;
using Service.Common.FaultContracts;
using System;
using Service.Common.MessageContracts;
using System.ServiceModel.Web;
using Service.Common.DataContracts;

namespace Service.Common.Business
{
    public static class ExceptionHandling
    {
        public static string ReplaceNewLineCharacters(this string baseStr)
        {
            return baseStr.Replace(Environment.NewLine, "").Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
        }

        public static void LogMethod(BaseRequest request, Action action, string methodName)
        {
            try
            {
                if (request == null)
                {
                    RaiseException("Request object can not be null", ErrorCode.BadRequest);
                }

                if (request.IsAuthenticationRequired)
                {
                    var serviceGroup = UserBusiness.AuthenticateRequest(request);
                }
                action();
            }
            catch (SoapException ex)
            {
                RaiseException(ex.Message, ex, ErrorCode.UnKnown);
            }
            catch (FaultException<ServiceFault> e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                RaiseException("Error in " + methodName + "." + ex.Message, ErrorCode.UnKnown);
            }
        }

        public static void RestLogMethod(Action action, BaseResponse response)
        {
            try
            {
                action();
            }
            catch (FaultException<ServiceFault> e)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                WebOperationContext.Current.OutgoingResponse.StatusDescription = e.Detail.FaultMessage.ReplaceNewLineCharacters();
                response.Message = new ResponseMessage()
                {
                    Message = e.Detail.FaultMessage,
                    Severity = MessageSeverity.Exception
                };
            }
            catch (Exception e)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                WebOperationContext.Current.OutgoingResponse.StatusDescription = e.Message.ReplaceNewLineCharacters();
                response = new BaseResponse()
                {
                    Message = new ResponseMessage()
                    {
                        Message = e.Message,
                        Severity = MessageSeverity.Exception
                    }
                };
            }
            finally
            {
                ValidateRestResponse(response);
            }
        }

        private static void ValidateRestResponse(BaseResponse response)
        {
            if (!string.IsNullOrEmpty(response.Message.Message))
            {
                response.Message.Message = response.Message.Message.ReplaceNewLineCharacters();
            }
        }

        #region Exception Handling

        /// <summary>
        ///  raises an exception to the client whenever there is a problem
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="errorCode"></param>
        /// <exception cref="ServiceFault"></exception>
        public static void RaiseException(string message, System.Exception innerException, ErrorCode errorCode)
        {
            throw new FaultException<ServiceFault>(new ServiceFault
                                                       {
                                                           FaultMessage = message,
                                                           FaultDetails = innerException.ToString(),
                                                           FaultErrorCode = errorCode,
                                                           FaultSource = innerException.StackTrace,
                                                           FaultType = ServiceFaultType.Unknown
                                                       });
        }

        /// <summary>
        ///  raises an exception to the client whenever there is a problem
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public static void RaiseException(string message, ErrorCode errorCode)
        {
            throw new FaultException<ServiceFault>(new ServiceFault
                                                       {
                                                           FaultMessage = message,
                                                           FaultDetails = message,
                                                           FaultErrorCode = errorCode,
                                                           FaultSource = string.Empty,
                                                           FaultType = ServiceFaultType.Unknown
                                                       },
                                                   new FaultReason(message));
        }

        #endregion
    }
}