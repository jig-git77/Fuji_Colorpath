using FujifilmUSA.Sync.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.App.Util
{
    public static class WebCommon
    {
        public static string BaseUri = ConfigurationManager.AppSettings["PluginServicePath"].ToString();

        public static ReferenceChartPlugin GetReferenceChart(string projectId, string referenceChartDisplayType, string IsSpotProjectType)
        {
            ReferenceChartPlugin chartData = new ReferenceChartPlugin();
            try
            {
                string methodUrl = "api/Plugin/GetReferenceChart?projectId=" + projectId + "&referenceChartDisplayType=" + referenceChartDisplayType + "&IsSpotProjectType=" + IsSpotProjectType;
                using (HttpClient httpClient = new HttpClient())
                {
                    var result = httpClient.GetAsync(BaseUri + methodUrl).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var resultString = result.Content.ReadAsStringAsync().Result;
                        chartData = JsonConvert.DeserializeObject<ReferenceChartPlugin>(resultString);
                    }
                    else
                    {
                        ShowMessage(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return chartData;
        }

        public static void ShowMessage(HttpResponseMessage message)
        {
            string result = message.Content.ReadAsStringAsync().Result;
            BaseErrorResponse errorResponse = JsonConvert.DeserializeObject<BaseErrorResponse>(result);
            if (errorResponse.Error.Code < 0)
            {
                
            }
        }
        public static bool SaveMeasurement(Measurement measurement, string projectType)
        {
            bool isResult = false;
            try
            {

                string methodUrl = "api/Plugin/SaveMeasurement/";
                if (projectType == "1D")
                    methodUrl = "api/Plugin/Save1DMeasurement/";
                else if (projectType == "Validation")
                    methodUrl = "api/Plugin/SaveVMeasurement/";
                else if (projectType == "Spot Library")
                    methodUrl = "api/Plugin/SaveSpotMeasurement/";

                using (HttpClient httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(measurement), Encoding.UTF8, "application/json");
                    var result = httpClient.PostAsync(BaseUri + methodUrl, content).Result;
                    if (result.IsSuccessStatusCode)
                        isResult = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return isResult;
        }

        //public static bool SaveMeasurement(Measurement measurement)
        //{
        //    bool isResult = false;
        //    try
        //    {
        //        string methodUrl = "PluginData.svc/SaveMeasurement/";
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            StringContent content = new StringContent(JsonConvert.SerializeObject(measurement), null, "application/json");
        //            var result = httpClient.PostAsync("http://localhost/FujiFilmUSA.Sync.PluginService/" + methodUrl, content).Result;
        //            if (result.IsSuccessStatusCode)
        //            {
        //                isResult = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return isResult;
        //}

        public static List<Measurement> GetMeasurementByOptimizationRun(string OptimizationRunId, string projectType)
        {
            List<Measurement> measurements = new List<Measurement>();
            try
            {
                string methodUrl = "api/Plugin/GetMeasurementsByOptimizeId?optimizeRunId=" + OptimizationRunId;
                if (projectType == "1D")
                    methodUrl = "api/Plugin/Get1DMeasurementsByOptimizeId?optimizeRunId=" + OptimizationRunId;
                else if (projectType == "Spot Library")
                    methodUrl = "api/Plugin/GetSpotMeasurementsByOptimizeId?optimizeRunId=" + OptimizationRunId;

                using (HttpClient httpClient = new HttpClient())
                {
                    var result = httpClient.GetAsync(BaseUri + methodUrl).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var resultString = result.Content.ReadAsStringAsync().Result;
                        measurements = JsonConvert.DeserializeObject<List<Measurement>>(resultString);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return measurements;
        }

        public static List<Measurement> GetValidationMeasurement(string ProjectId)
        {
            List<Measurement> measurements = new List<Measurement>();
            try
            {
                string methodUrl = "api/Plugin/GetVMeasurementsByProjectId?projectId=" + ProjectId;
                using (HttpClient httpClient = new HttpClient())
                {
                    var result = httpClient.GetAsync(BaseUri + methodUrl).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var resultString = result.Content.ReadAsStringAsync().Result;
                        measurements = JsonConvert.DeserializeObject<List<Measurement>>(resultString);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return measurements;
        }

        public static bool DeleteMeasurement(string measurementID, string projectType)
        {
            bool isResult = false;
            try
            {
                string methodUrl = "api/Plugin/DeleteMeasurement?measurementId=" + measurementID;

                if (projectType == "1D")
                    methodUrl = "api/Plugin/Delete1DMeasurement?measurementId=" + measurementID;
                else if (projectType == "Validation")
                    methodUrl = "api/Plugin/DeleteVMeasurement?measurementId=" + measurementID;
                else if (projectType == "Spot Library")
                    methodUrl = "api/Plugin/DeleteSpotMeasurement?measurementId=" + measurementID;

                using (HttpClient httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(measurementID), Encoding.UTF8, "application/json");
                    var result = httpClient.DeleteAsync(BaseUri + methodUrl).Result;
                    if (result.IsSuccessStatusCode)
                        isResult = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return isResult;
        }
    }
}
