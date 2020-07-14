using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FujiFilmUSA.Sync.PluginService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PluginData" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PluginData.svc or PluginData.svc.cs at the Solution Explorer and start debugging.
    public class PluginData : IPluginData
    {

        public void DoWork()
        {
        }

        public object UpdateCache(Stream messages)
        {
            using (var streamReader = new StreamReader(messages))
            {
                string inputString = streamReader.ReadToEnd();
                // var inputData = JsonConvert.DeserializeObject<List<dynamic>>(inputString);
            }
            return null;
        }

        public void SaveMeasurement(Measurement measurement)
        {
            string jsonString = JsonConvert.SerializeObject(measurement, Formatting.Indented);
            ErrorLog.WriteJsonFile(jsonString, measurement.ProjectId.ToString(), measurement.OptimizeRunId.ToString());
            this.Measurement = measurement;
            var presenter = new PresenterPluginDataService(this);
            presenter.SaveMeasurement();
        }

        public void SaveVMeasurement(Measurement measurement)
        {
            string jsonString = JsonConvert.SerializeObject(measurement, Formatting.Indented);
            ErrorLog.WriteJsonFile(jsonString, measurement.ProjectId.ToString(), measurement.OptimizeRunId.ToString());
            string str = JsonConvert.SerializeObject(measurement, Formatting.Indented);
            this.Measurement = measurement;
            var presenter = new PresenterPluginDataService(this);
            presenter.SaveVMeasurement();
        }

        public void Save1DMeasurement(Measurement measurement)
        {
            string jsonString = JsonConvert.SerializeObject(measurement, Formatting.Indented);
            ErrorLog.WriteJsonFile(jsonString, measurement.ProjectId.ToString(), measurement.OptimizeRunId.ToString());
            string str = JsonConvert.SerializeObject(measurement, Formatting.Indented);
            this.Measurement = measurement;
            var presenter = new PresenterPluginDataService(this);
            presenter.Save1DMeasurement();
        }

        public ReferenceChartData GetReferenceChart(string projectId, string referenceChartDisplayType, string IsSpotProjectType)
        {
            ErrorLog.WriteErrorLog("Error");
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

        public List<Measurement> GetMeasurementsByOptimizeId(string optimizeRunId)
        {
            this.OptimizeRunId = Convert.ToInt32(optimizeRunId);
            var presenter = new PresenterPluginDataService(this);
            presenter.GetMeasurementsOnRunId();
            //if (Measurements.Count == 0) //------If someone delete all measurements, and still able to go to Analyze page, then set this to false
            //{
            //    FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentOptimizeRun.HasMeasurements = false;
            //}
            return Measurements;
        }

        public List<Measurement> GetVMeasurementsByProjectId(string projectId)
        {
            this.ProjectId = Convert.ToInt32(projectId);
            var presenter = new PresenterPluginDataService(this);
            presenter.GetVMeasurementsOnProjectId();
            //if (Measurements.Count == 0) //------If someone delete all measurements, and still able to go to Analyze page, then set this to false
            //{
            //    FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentOptimizeRun.HasMeasurements = false;
            //}
            return Measurements;
        }

        public List<Measurement> Get1DMeasurementsByOptimizeId(string optimizeRunId)
        {
            this.OptimizeRunId = Convert.ToInt32(optimizeRunId);
            var presenter = new PresenterPluginDataService(this);
            presenter.Get1DMeasurementsOnRunId();
            //if (Measurements.Count == 0) //------If someone delete all measurements, and still able to go to Analyze page, then set this to false
            //{
            //    FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentOptimizeRun.HasMeasurements = false;
            //}
            return Measurements;
        }

        public bool DeleteMeasurement(string measurementId)
        {
            MeasurementId = Convert.ToInt32(measurementId);
            var presenter = new PresenterPluginDataService(this);
            return presenter.DeleteMeasurement();
        }

        public bool DeleteVMeasurement(string measurementId)
        {
            MeasurementId = Convert.ToInt32(measurementId);
            var presenter = new PresenterPluginDataService(this);
            return presenter.DeleteVMeasurement();
        }

        public bool Delete1DMeasurement(string measurementId)
        {
            MeasurementId = Convert.ToInt32(measurementId);
            var presenter = new PresenterPluginDataService(this);
            return presenter.Delete1DMeasurement();
        }

        public bool KeepUserAlive(string username)
        {

            //List<User> users = FujifilmUSA.Sync.Common.CacheManager.Instance.ActiveUsers;
            //User user = users.FirstOrDefault(c => c.LoginId == username);
            //if (user != null)
            //    user.LastPingTime = DateTime.Now;
            //FujifilmUSA.Sync.Common.CacheManager.Instance.ActiveUsers = users;
            return true;
        }

        public bool KeepUserSessionAlive()
        {
            return true;
        }

        public void SaveSpotMeasurement(Measurement measurement)
        {
            this.Measurement = measurement;
            var presenter = new PresenterPluginDataService(this);
            presenter.SaveSpotMeasurement();
        }

        public List<Measurement> GetSpotMeasurementsByOptimizeId(string optimizeRunId)
        {
            this.OptimizeRunId = Convert.ToInt32(optimizeRunId);
            var presenter = new PresenterPluginDataService(this);
            presenter.GetSpotMeasurementsOnRunId();
            //if (Measurements.Count == 0) //------If someone delete all measurements, and still able to go to Analyze page, then set this to false
            //{
            //    FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentOptimizeRun.HasMeasurements = false;
            //}
            return Measurements;
        }

        public bool DeleteSpotMeasurement(string measurementId)
        {
            MeasurementId = Convert.ToInt32(measurementId);
            var presenter = new PresenterPluginDataService(this);
            return presenter.DeleteSpotMeasurement();
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

    }
}
