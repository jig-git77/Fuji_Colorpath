using System.Runtime.Serialization;
using Service.Common.DataContracts;
using System;

namespace Service.Common.MessageContracts
{
    [DataContract]
    public class BaseResponse
    {
        #region Private Members
        public DateTime InitialDateTime { get; set; }
        #endregion

        #region Contructor
        public BaseResponse()
        {
            this.Message = new ResponseMessage();
            InitialDateTime = DateTime.Now;
        }
        #endregion

        #region Public Properties

        [DataMember]
        public ResponseMessage Message { get; set; }

        [DataMember]
        public long Count { get; set; }

        [DataMember]
        public double RequestTotalTime { get; set; }
        
        #endregion

        #region Public Methods
        public void CalculateAndLogRequestTime(string methodName)
        {
            RequestTotalTime = InitialDateTime.GetCurrentTimeDifference();
        }
        #endregion
    }

    public static class DateTimeExtension
    {
        public static double GetCurrentTimeDifference(this DateTime baseDateTime)
        {
            return DateTime.Now.Subtract(baseDateTime).TotalMilliseconds;
        }
    }
}