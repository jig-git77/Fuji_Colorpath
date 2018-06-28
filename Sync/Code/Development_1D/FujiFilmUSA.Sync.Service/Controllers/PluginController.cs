using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FujiFilmUSA.Sync.Service.Controllers
{
    public class PluginController : BaseController, IViewPluginDataService
    {
        [HttpGet]
        public IHttpActionResult GetReferenceChart(string projectId, string referenceChartDisplayType, string IsSpotProjectType)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<ReferenceChartData> httpResponse = new BaseResponse<ReferenceChartData>();
            try
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
                httpResponse.Data = this.ReferenceChartData;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, ReferenceChartData));
            }
            catch (Exception ex)
            {

                throw;
            }
            return actionResult;
        }

        [HttpPost]
        public IHttpActionResult SaveMeasurement(Measurement measurement)
        {
            IHttpActionResult actionResult = null;
            bool result = false;
            try
            {
                this.Measurement = measurement;
                var presenter = new PresenterPluginDataService(this);
                presenter.SaveMeasurement();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;                
            }
            actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, result));
            return actionResult;
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
        #endregion
    }
}
