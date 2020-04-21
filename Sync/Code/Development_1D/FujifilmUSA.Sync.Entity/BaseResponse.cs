using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            if (Error == null)
            {
                Error = new Error();
            }
        }

        [JsonIgnore]
        public ResponseStatus status { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonIgnore]
        private Error _error { get; set; }

        [JsonProperty("error")]
        //[JsonIgnore]
        public Error Error
        {
            get { return status == ResponseStatus.Success ? null : _error; }
            set { _error = value; }
        }

    }

    public class FileResponse
    {
        //public bool isSuccess { get; set; }
        //public string Message { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }
        [JsonProperty("filePath")]
        public string FilePath { get; set; }
    }
    public class Error
    {
        [JsonIgnore]
        private string _userMessage = string.Empty;

        [JsonProperty("userMessage")]
        public string UserMessage { get { return _userMessage; } set { _userMessage = value; } }

        [JsonIgnore]
        private string _internalMessage = string.Empty;

        [JsonProperty("internalMessage")]
        public string InternalMessage { get { return _internalMessage; } set { _internalMessage = value; } }

        [JsonIgnore]
        private int _code = 0;

        [JsonProperty("code")]
        public int Code { get { return _code; } set { _code = value; } }


        [JsonIgnore]
        public Exception exception { get; set; }
    }
    [Serializable]
    public enum ResponseStatus
    {
        NoResult = 0,

        [Description("Request Success.")]
        Success = 1,

        [Description("Validation Error.")]
        ValidationError = -1,

        [Description("Internal Server Error")]
        InternalServer = -2,
        
        [Description("{0} can not be empty.")]
        Business_EmptyData = -3,

        [Description("Invalid {0}.")]
        Business_InvalidData = -4,

        Business_OperationError = -5,

        [Description("Request can not be null.")]
        Business_NullRequest = -6,

        Business_CustomError = -7,

        ForgotPassword_Success = 2,

        [Description("Access Denied.")]
        AccessDenied = -8,

        [Description("Invalid API Key.")]
        InvalidAPIKey = -9,


    }

    public class BaseErrorResponse
    {
        public BaseErrorResponse()
        {
            if (Error == null)
            {
                Error = new Error();
            }
        }

        [JsonIgnore]
        public ResponseStatus status { get; set; }

        [JsonIgnore]
        private Error _error { get; set; }

        [JsonProperty("error")]
        //[JsonIgnore]
        public Error Error
        {
            get { return status == ResponseStatus.Success ? null : _error; }
            set { _error = value; }
        }
    }
}
