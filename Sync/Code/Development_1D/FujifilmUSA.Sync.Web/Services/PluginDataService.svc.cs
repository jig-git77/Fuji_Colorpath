using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Entity;
using System.Data;
using Newtonsoft.Json;
using FujifilmUSA.Sync.Common;
using System.Security.Authentication;

namespace FujifilmUSA.Sync.Web.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PluginDataService : IViewPluginDataService, IViewLogin
    {
        [WebInvoke(Method = "GET", UriTemplate = "Ping")]
        [OperationContract]
        public bool Ping()
        {
            CheckUserValidation();
            return true;
        }

        [WebInvoke(Method = "POST", UriTemplate = "SaveMeasurement")]
        [OperationContract]
        public bool SaveMeasurement(Entity.Measurement measurement)
        {
            CheckUserValidation();
            this.Measurement = measurement;
            var presenter = new PresenterPluginDataService(this);
            UserID = SessionManager.Instance.CurrentUser.UserId;
            return presenter.SaveMeasurement();
        }

        [WebInvoke(Method = "POST", UriTemplate = "SaveVMeasurement")]
        [OperationContract]
        public bool SaveVMeasurement(Entity.Measurement measurement)
        {
            CheckUserValidation();
            this.Measurement = measurement;
            var presenter = new PresenterPluginDataService(this);
            UserID = SessionManager.Instance.CurrentUser.UserId;
            return presenter.SaveVMeasurement();
        }

        [WebInvoke(Method = "POST", UriTemplate = "Save1DMeasurement")]
        [OperationContract]
        public bool Save1DMeasurement(Entity.Measurement measurement)
        {
            CheckUserValidation();
            this.Measurement = measurement;
            var presenter = new PresenterPluginDataService(this);
            UserID = SessionManager.Instance.CurrentUser.UserId;
            return presenter.Save1DMeasurement();
        }

        [WebGet(UriTemplate = "GetReferenceChart/{projectId}/{referencechartdisplaytype}/{IsSpotProjectType}")]
        [OperationContract]
        public ReferenceChartData GetReferenceChart(string projectId, string referenceChartDisplayType, string IsSpotProjectType)
        {
            this.ReferenceChartDisType = (EReferenceChartDisType)Enum.Parse(typeof(EReferenceChartDisType), referenceChartDisplayType);
            var presenter = new PresenterPluginDataService(this);
            if (Convert.ToBoolean(IsSpotProjectType) == false)
            {
                this.ProjectId = Convert.ToInt32(projectId);
                presenter.GetReferenceChartData();
            }
            else
            {
                this.SpotProjectId = Convert.ToInt32(projectId);
                presenter.GetSpotReferenceChartData();
            }
            return ReferenceChartData;
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetMeasurementsByOptimizeId/{optimizeRunId}")]
        [OperationContract]
        public List<Entity.Measurement> GetMeasurementsByOptimizeId(string optimizeRunId)
        {
            this.OptimizeRunId = Convert.ToInt32(optimizeRunId);
            var presenter = new PresenterPluginDataService(this);
            presenter.GetMeasurementsOnRunId();
            if (Measurements.Count == 0) //------If someone delete all measurements, and still able to go to Analyze page, then set this to false
            {
                FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentOptimizeRun.HasMeasurements = false;
            }
            return Measurements;
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetVMeasurementsByProjectId/{projectId}")]
        [OperationContract]
        public List<Entity.Measurement> GetVMeasurementsByProjectId(string projectId)
        {
            this.ProjectId = Convert.ToInt32(projectId);
            var presenter = new PresenterPluginDataService(this);
            presenter.GetVMeasurementsOnProjectId();
            if (Measurements.Count == 0) //------If someone delete all measurements, and still able to go to Analyze page, then set this to false
            {
                FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentOptimizeRun.HasMeasurements = false;
            }
            return Measurements;
        }

        [WebInvoke(Method = "GET", UriTemplate = "Get1DMeasurementsByOptimizeId/{optimizeRunId}")]
        [OperationContract]
        public List<Entity.Measurement> Get1DMeasurementsByOptimizeId(string optimizeRunId)
        {
            this.OptimizeRunId = Convert.ToInt32(optimizeRunId);
            var presenter = new PresenterPluginDataService(this);
            presenter.Get1DMeasurementsOnRunId();
            if (Measurements.Count == 0) //------If someone delete all measurements, and still able to go to Analyze page, then set this to false
            {
                FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentOptimizeRun.HasMeasurements = false;
            }
            return Measurements;
        }

        [WebInvoke(Method = "POST", UriTemplate = "DeleteMeasurement/{measurementId}")]
        [OperationContract]
        public bool DeleteMeasurement(string measurementId)
        {
            CheckUserValidation();
            MeasurementId = Convert.ToInt32(measurementId);
            var presenter = new PresenterPluginDataService(this);
            return presenter.DeleteMeasurement();
        }

        [WebInvoke(Method = "POST", UriTemplate = "DeleteVMeasurement/{measurementId}")]
        [OperationContract]
        public bool DeleteVMeasurement(string measurementId)
        {
            CheckUserValidation();
            MeasurementId = Convert.ToInt32(measurementId);
            var presenter = new PresenterPluginDataService(this);
            return presenter.DeleteVMeasurement();
        }

        [WebInvoke(Method = "POST", UriTemplate = "Delete1DMeasurement/{measurementId}")]
        [OperationContract]
        public bool Delete1DMeasurement(string measurementId)
        {
            CheckUserValidation();
            MeasurementId = Convert.ToInt32(measurementId);
            var presenter = new PresenterPluginDataService(this);
            return presenter.Delete1DMeasurement();
        }

        [WebInvoke(Method = "POST", UriTemplate = "KeepUserAlive/{username}")]
        [OperationContract]
        public bool KeepUserAlive(string username)
        {

            List<User> users = FujifilmUSA.Sync.Common.CacheManager.Instance.ActiveUsers;
            User user = users.FirstOrDefault(c => c.LoginId == username);
            if (user != null)
                user.LastPingTime = DateTime.Now;
            FujifilmUSA.Sync.Common.CacheManager.Instance.ActiveUsers = users;
            return true;
        }

        [WebInvoke(Method = "POST", UriTemplate = "KeepUserSessionAlive/")]
        [OperationContract]
        public bool KeepUserSessionAlive()
        {
            return true;
        }

        [WebInvoke(Method = "POST", UriTemplate = "SaveSpotMeasurement")]
        [OperationContract]
        public bool SaveSpotMeasurement(Entity.Measurement measurement)
        {
            CheckUserValidation();
            this.Measurement = measurement;
            var presenter = new PresenterPluginDataService(this);
            UserID = SessionManager.Instance.CurrentUser.UserId;
            return presenter.SaveSpotMeasurement();

            /*Temp Code Start*/
            //GenerateJSONFile(measurement, "Spot");
            /*Temp Code End*/
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetSpotMeasurementsByOptimizeId/{optimizeRunId}")]
        [OperationContract]
        public List<Entity.Measurement> GetSpotMeasurementsByOptimizeId(string optimizeRunId)
        {
            this.OptimizeRunId = Convert.ToInt32(optimizeRunId);
            var presenter = new PresenterPluginDataService(this);
            presenter.GetSpotMeasurementsOnRunId();
            if (Measurements.Count == 0) //------If someone delete all measurements, and still able to go to Analyze page, then set this to false
            {
                FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentOptimizeRun.HasMeasurements = false;
            }
            return Measurements;
        }

        [WebInvoke(Method = "POST", UriTemplate = "DeleteSpotMeasurement/{measurementId}")]
        [OperationContract]
        public bool DeleteSpotMeasurement(string measurementId)
        {
            CheckUserValidation();
            MeasurementId = Convert.ToInt32(measurementId);
            var presenter = new PresenterPluginDataService(this);
            return presenter.DeleteSpotMeasurement();
        }


        public void CheckUserValidation()
        {
            if (!IsValidateUser())
                throw new WebFaultException(System.Net.HttpStatusCode.Unauthorized);            
        }

        //public void CheckUserValidation()
        //{
        //    int result = IsValidateUser();
        //    if (result == -1)
        //        throw new WebFaultException(System.Net.HttpStatusCode.Unauthorized);
        //    else if (result == -2)
        //        throw new WebFaultException(System.Net.HttpStatusCode.BadRequest);
        //}

        //public int IsValidateUser()
        //{
        //    //Getting the user token from client request
        //    var _presenterlogin = new PresenterLogin(this);
        //    if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Headers["Auth-Key"]))
        //    {
        //        this.EncyptedTocken = Convert.ToString(System.Web.HttpContext.Current.Request.Headers["Auth-Key"]);
        //        //return _presenterlogin.ValidateToken();

        //    }                       
        //    return -3;
        //}

        public bool IsValidateUser()
        {
            //Getting the user token from client request
            var _presenterlogin = new PresenterLogin(this);
            var authRequest = System.Web.HttpContext.Current.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authRequest))
            {
                var auth = authRequest.Split(' ');
                if (!string.IsNullOrEmpty(auth[1]) && auth[0] == "Basic")
                {
                    this.EncyptedTocken = Convert.ToString(auth[1]);
                    return _presenterlogin.ValidateUser();
                }
            }

            return false;
        }

        public void GenerateJSONFile(Entity.Measurement measurement, string type)
        {
            Entity.Measurement measurementDummy = new Entity.Measurement();
            measurementDummy = measurement;
            measurementDummy.MeasurementAppPatches = new List<MeasurementPatchApp>();
            foreach (var patch in measurement.MeasurementPatches)
            {
                measurementDummy.MeasurementAppPatches.Add(new MeasurementPatchApp()
                {
                    MeasurementPatchId = patch.MeasurementPatchId,
                    MeasurementId = patch.MeasurementId,
                    SampleName = patch.SampleName,
                    Ciexyz = new CieXYZ()
                    {
                        X = Convert.ToString(patch.CiexyzX),
                        Y = Convert.ToString(patch.CiexyzY),
                        Z = Convert.ToString(patch.CiexyzZ),
                    },
                    Cielab = new CieLAB()
                    {
                        L = Convert.ToString(patch.CielabL),
                        A = Convert.ToString(patch.CielabA),
                        B = Convert.ToString(patch.CielabB),
                    },
                    Δe = Convert.ToString(patch.Δe),
                    Δe94 = Convert.ToString(patch.Δe94),
                    Δe2000 = Convert.ToString(patch.Δe2000),
                    HtmlColor = patch.HtmlColor,
                    Density = new DensityApp()
                    {
                        Red = Convert.ToString(patch.Density.Red),
                        Green = Convert.ToString(patch.Density.Green),
                        Blue = Convert.ToString(patch.Density.Blue),
                        Visual = Convert.ToString(patch.Density.Visual)
                    },
                    Spectral = new SpectralApp()
                    {
                        nm380 = Convert.ToString(patch.Spectral.nm380),
                        nm390 = Convert.ToString(patch.Spectral.nm390),
                        nm400 = Convert.ToString(patch.Spectral.nm400),
                        nm410 = Convert.ToString(patch.Spectral.nm410),
                        nm420 = Convert.ToString(patch.Spectral.nm420),
                        nm430 = Convert.ToString(patch.Spectral.nm430),
                        nm440 = Convert.ToString(patch.Spectral.nm440),
                        nm450 = Convert.ToString(patch.Spectral.nm450),
                        nm460 = Convert.ToString(patch.Spectral.nm460),
                        nm470 = Convert.ToString(patch.Spectral.nm470),
                        nm480 = Convert.ToString(patch.Spectral.nm480),
                        nm490 = Convert.ToString(patch.Spectral.nm490),
                        nm500 = Convert.ToString(patch.Spectral.nm500),
                        nm510 = Convert.ToString(patch.Spectral.nm510),
                        nm520 = Convert.ToString(patch.Spectral.nm520),
                        nm530 = Convert.ToString(patch.Spectral.nm530),
                        nm540 = Convert.ToString(patch.Spectral.nm540),
                        nm550 = Convert.ToString(patch.Spectral.nm550),
                        nm560 = Convert.ToString(patch.Spectral.nm560),
                        nm570 = Convert.ToString(patch.Spectral.nm570),
                        nm580 = Convert.ToString(patch.Spectral.nm580),
                        nm590 = Convert.ToString(patch.Spectral.nm590),
                        nm600 = Convert.ToString(patch.Spectral.nm600),
                        nm610 = Convert.ToString(patch.Spectral.nm610),
                        nm620 = Convert.ToString(patch.Spectral.nm620),
                        nm630 = Convert.ToString(patch.Spectral.nm630),
                        nm640 = Convert.ToString(patch.Spectral.nm640),
                        nm650 = Convert.ToString(patch.Spectral.nm650),
                        nm660 = Convert.ToString(patch.Spectral.nm660),
                        nm670 = Convert.ToString(patch.Spectral.nm670),
                        nm680 = Convert.ToString(patch.Spectral.nm680),
                        nm690 = Convert.ToString(patch.Spectral.nm690),
                        nm700 = Convert.ToString(patch.Spectral.nm700),
                        nm710 = Convert.ToString(patch.Spectral.nm710),
                        nm720 = Convert.ToString(patch.Spectral.nm720),
                        nm730 = Convert.ToString(patch.Spectral.nm730),
                    },
                    SpotId = patch.SpotId,
                    SpotName = patch.SpotName
                });
            }
            measurementDummy.MeasurementPatches = null;
            string jsonString = JsonConvert.SerializeObject(measurementDummy, Formatting.Indented);
            ErrorLog.WriteJsonFile(jsonString, measurement.ProjectId.ToString() + "_" + type, measurement.OptimizeRunId.ToString());
        }

        #region IViewPluginDataService Members
        public Entity.Measurement Measurement { get; set; }
        public int ProjectId { get; set; }
        public ReferenceChartData ReferenceChartData { get; set; }
        public int OptimizeRunId { get; set; }
        public List<Entity.Measurement> Measurements { get; set; }
        public int MeasurementId { get; set; }
        public EReferenceChartDisType ReferenceChartDisType { get; set; }
        public int SpotProjectId { get; set; }
        public ReferenceChartPlugin ReferenceChartPlugin { get; set; }
        public string ProjectType { get; set; }
        public DataTable dtMeasurementValidation { get; set; }
        public int UserID { get; set; }
        public string MeaDeviceSerialNo { get; set; }
        public string RequestMeasurment { get; set; }
        public string MToolEntryDate { get; set; }
        public int OSPlatformId { get; set; }
        public MToolVersion MTVersion { get; set; }
        #endregion

        #region IViewBase Members

        public int CurrentUserId { get; set; }

        public void ErrorOccured(object sender, ErrorArgs e)
        {
            //Do nothing in the service...
        }

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
        public ReferenceChartApp UserReferenceChartApp { get; set; }
        public string DeviceSerialNo { get; set; }
        public short InstrumentTypeID { get; set; }

        public RevoriaUser RevoriaUser { get; set; }
        #endregion
    }
}
