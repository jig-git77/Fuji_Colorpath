using FujifilmUSA.Sync.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.App.Util
{
    public static class WebCommon
    {
        public static string BaseUri = "http://localhost/FujifilmUSA.Sync.PluginService/";
        public static ReferenceChartData GetReferenceChart(string projectId, string referenceChartDisplayType, string IsSpotProjectType)
        {
            ReferenceChartData chartData = new ReferenceChartData();
            try
            {
                string methodUrl = "PluginData.svc/GetReferenceChart/" + projectId + "/" + referenceChartDisplayType + "/" + IsSpotProjectType;
                using (HttpClient httpClient = new HttpClient())
                {
                    var result = httpClient.GetAsync(BaseUri + methodUrl).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var resultString = result.Content.ReadAsStringAsync().Result;
                        chartData = JsonConvert.DeserializeObject<ReferenceChartData>(resultString);                                               
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return chartData;
        }

        public static bool SaveMeasurement(Measurement measurement)
        {
            bool isResult= false;
            try
            {
                string methodUrl = "PluginData.svc/SaveMeasurement/";
                using (HttpClient httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(measurement), null, "application/json");
                    var result = httpClient.PostAsync(BaseUri + methodUrl, content).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        isResult = true;
                    }
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
