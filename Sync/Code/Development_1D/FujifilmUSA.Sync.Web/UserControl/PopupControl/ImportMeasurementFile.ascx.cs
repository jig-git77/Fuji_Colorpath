using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl.PopupControl
{
    public partial class ImportMeasurementFile : BaseControl, IViewImportMeasurements
    {
        #region Variable Declaration
        private PresenterImportMeasurements _presenterImportMeasurements;
        #endregion

        #region Page Events
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterImportMeasurements = new PresenterImportMeasurements(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region Control Events
        protected void lbtnImport_Click(object sender, EventArgs e)
        {
            try
            {


                int parentProjectMeasurementCount = 0;
                #region Check for current project existing measurement
                DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
                ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                LicenseTypeID = (int)SessionManager.Instance.CurrentProject.LicenseType;
                _presenterImportMeasurements.GetMeasurementIDsbyProjectID();
                #endregion
                if (!IsErrorOccured)
                {
                    parentProjectMeasurementCount = Measurements.Count;
                }
                if (parentProjectMeasurementCount + Measurements.Count > (FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentProject.LicenseType == FujifilmUSA.Sync.Entity.ELicenseType.Sync1D ? Business.ApplicationConfiguration.Instance.MeasurementLimitSync1D : Business.ApplicationConfiguration.Instance.MeasurementsLimit))
                {
                    fujiStatusbar.DisplayMessage("Measurements cannot be imported as total allowed measurement limit exceeded.", false);
                    return;
                }

                if (fuMeasurement.PostedFile != null)
                {
                    if (fuMeasurement.PostedFile.ContentLength > 0)
                    {
                        string inputContent;
                        using (StreamReader inputStreamReader = new StreamReader(fuMeasurement.PostedFile.InputStream))
                        {
                            inputContent = inputStreamReader.ReadToEnd();
                        }

                        Measurement = JsonConvert.DeserializeObject<Entity.Measurement>(inputContent);
                        Measurement.MeasurementName = txtMeasurementName.Text;
                        Measurement.OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                        Measurement.MeasurementDate = DateTime.Now;
                        Measurement.PCSDataType = 1;
                        Measurement.ReferenceChartTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key;
                        BindMeasurementPatches(Measurement);

                        if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
                            _presenterImportMeasurements.ImportSpotMeasurementFile();
                        else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                            _presenterImportMeasurements.Import1DMeasurementFile();
                        else
                            _presenterImportMeasurements.ImportMeasurementFile();


                        if (!IsErrorOccured)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", "<script type=text/javascript> CloseImportWindow(); </script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = GetResourceString("ImportUnsuccessfulMessage");
            }
        }

        public void BindMeasurementPatches(Entity.Measurement measurement)
        {

            Measurement.MeasurementPatches = new List<MeasurementPatch>();
            foreach (var patch in Measurement.MeasurementAppPatches)
            {
                Measurement.MeasurementPatches.Add(new MeasurementPatch()
                {
                    MeasurementPatchId = patch.MeasurementPatchId,
                    MeasurementId = patch.MeasurementId,
                    SampleName = patch.SampleName,
                    CiexyzX = patch.Ciexyz == null ? 0 : string.IsNullOrEmpty(patch.Ciexyz.X) ? 0 : Convert.ToDecimal(patch.Ciexyz.X),
                    CiexyzY = patch.Ciexyz == null ? 0 : string.IsNullOrEmpty(patch.Ciexyz.Y) ? 0 : Convert.ToDecimal(patch.Ciexyz.Y),
                    CiexyzZ = patch.Ciexyz == null ? 0 : string.IsNullOrEmpty(patch.Ciexyz.Z) ? 0 : Convert.ToDecimal(patch.Ciexyz.Z),
                    CielabL = patch.Cielab == null ? 0 : string.IsNullOrEmpty(patch.Cielab.L) ? 0 : Convert.ToDecimal(patch.Cielab.L),
                    CielabA = patch.Cielab == null ? 0 : string.IsNullOrEmpty(patch.Cielab.A) ? 0 : Convert.ToDecimal(patch.Cielab.A),
                    CielabB = patch.Cielab == null ? 0 : string.IsNullOrEmpty(patch.Cielab.B) ? 0 : Convert.ToDecimal(patch.Cielab.B),
                    Δe = string.IsNullOrEmpty(patch.Δe) ? 0 : Convert.ToDecimal(patch.Δe),
                    Δe94 = string.IsNullOrEmpty(patch.Δe94) ? 0 : Convert.ToDecimal(patch.Δe94),
                    Δe2000 = string.IsNullOrEmpty(patch.Δe2000) ? 0 : Convert.ToDecimal(patch.Δe2000),
                    HtmlColor = patch.HtmlColor,
                    Density = patch.Density == null ? null : new Density()
                    {
                        Red = string.IsNullOrEmpty(patch.Density.Red) ? 0 : Convert.ToDouble(patch.Density.Red),
                        Green = string.IsNullOrEmpty(patch.Density.Green) ? 0 : Convert.ToDouble(patch.Density.Green),
                        Blue = string.IsNullOrEmpty(patch.Density.Blue) ? 0 : Convert.ToDouble(patch.Density.Blue),
                        Visual = string.IsNullOrEmpty(patch.Density.Visual) ? 0 : Convert.ToDouble(patch.Density.Visual)
                    },
                    Spectral = patch.Spectral == null ? null : new Spectral()
                    {
                        nm380 = string.IsNullOrEmpty(patch.Spectral.nm380) ? 0 : Convert.ToDouble(patch.Spectral.nm380),
                        nm390 = string.IsNullOrEmpty(patch.Spectral.nm390) ? 0 : Convert.ToDouble(patch.Spectral.nm390),
                        nm400 = string.IsNullOrEmpty(patch.Spectral.nm400) ? 0 : Convert.ToDouble(patch.Spectral.nm400),
                        nm410 = string.IsNullOrEmpty(patch.Spectral.nm410) ? 0 : Convert.ToDouble(patch.Spectral.nm410),
                        nm420 = string.IsNullOrEmpty(patch.Spectral.nm420) ? 0 : Convert.ToDouble(patch.Spectral.nm420),
                        nm430 = string.IsNullOrEmpty(patch.Spectral.nm430) ? 0 : Convert.ToDouble(patch.Spectral.nm430),
                        nm440 = string.IsNullOrEmpty(patch.Spectral.nm440) ? 0 : Convert.ToDouble(patch.Spectral.nm440),
                        nm450 = string.IsNullOrEmpty(patch.Spectral.nm450) ? 0 : Convert.ToDouble(patch.Spectral.nm450),
                        nm460 = string.IsNullOrEmpty(patch.Spectral.nm460) ? 0 : Convert.ToDouble(patch.Spectral.nm460),
                        nm470 = string.IsNullOrEmpty(patch.Spectral.nm470) ? 0 : Convert.ToDouble(patch.Spectral.nm470),
                        nm480 = string.IsNullOrEmpty(patch.Spectral.nm480) ? 0 : Convert.ToDouble(patch.Spectral.nm480),
                        nm490 = string.IsNullOrEmpty(patch.Spectral.nm490) ? 0 : Convert.ToDouble(patch.Spectral.nm490),
                        nm500 = string.IsNullOrEmpty(patch.Spectral.nm500) ? 0 : Convert.ToDouble(patch.Spectral.nm500),
                        nm510 = string.IsNullOrEmpty(patch.Spectral.nm510) ? 0 : Convert.ToDouble(patch.Spectral.nm510),
                        nm520 = string.IsNullOrEmpty(patch.Spectral.nm520) ? 0 : Convert.ToDouble(patch.Spectral.nm520),
                        nm530 = string.IsNullOrEmpty(patch.Spectral.nm530) ? 0 : Convert.ToDouble(patch.Spectral.nm530),
                        nm540 = string.IsNullOrEmpty(patch.Spectral.nm540) ? 0 : Convert.ToDouble(patch.Spectral.nm540),
                        nm550 = string.IsNullOrEmpty(patch.Spectral.nm550) ? 0 : Convert.ToDouble(patch.Spectral.nm550),
                        nm560 = string.IsNullOrEmpty(patch.Spectral.nm560) ? 0 : Convert.ToDouble(patch.Spectral.nm560),
                        nm570 = string.IsNullOrEmpty(patch.Spectral.nm570) ? 0 : Convert.ToDouble(patch.Spectral.nm570),
                        nm580 = string.IsNullOrEmpty(patch.Spectral.nm580) ? 0 : Convert.ToDouble(patch.Spectral.nm580),
                        nm590 = string.IsNullOrEmpty(patch.Spectral.nm590) ? 0 : Convert.ToDouble(patch.Spectral.nm590),
                        nm600 = string.IsNullOrEmpty(patch.Spectral.nm600) ? 0 : Convert.ToDouble(patch.Spectral.nm600),
                        nm610 = string.IsNullOrEmpty(patch.Spectral.nm610) ? 0 : Convert.ToDouble(patch.Spectral.nm610),
                        nm620 = string.IsNullOrEmpty(patch.Spectral.nm620) ? 0 : Convert.ToDouble(patch.Spectral.nm620),
                        nm630 = string.IsNullOrEmpty(patch.Spectral.nm630) ? 0 : Convert.ToDouble(patch.Spectral.nm630),
                        nm640 = string.IsNullOrEmpty(patch.Spectral.nm640) ? 0 : Convert.ToDouble(patch.Spectral.nm640),
                        nm650 = string.IsNullOrEmpty(patch.Spectral.nm650) ? 0 : Convert.ToDouble(patch.Spectral.nm650),
                        nm660 = string.IsNullOrEmpty(patch.Spectral.nm660) ? 0 : Convert.ToDouble(patch.Spectral.nm660),
                        nm670 = string.IsNullOrEmpty(patch.Spectral.nm670) ? 0 : Convert.ToDouble(patch.Spectral.nm670),
                        nm680 = string.IsNullOrEmpty(patch.Spectral.nm680) ? 0 : Convert.ToDouble(patch.Spectral.nm680),
                        nm690 = string.IsNullOrEmpty(patch.Spectral.nm690) ? 0 : Convert.ToDouble(patch.Spectral.nm690),
                        nm700 = string.IsNullOrEmpty(patch.Spectral.nm700) ? 0 : Convert.ToDouble(patch.Spectral.nm700),
                        nm710 = string.IsNullOrEmpty(patch.Spectral.nm710) ? 0 : Convert.ToDouble(patch.Spectral.nm710),
                        nm720 = string.IsNullOrEmpty(patch.Spectral.nm720) ? 0 : Convert.ToDouble(patch.Spectral.nm720),
                        nm730 = string.IsNullOrEmpty(patch.Spectral.nm730) ? 0 : Convert.ToDouble(patch.Spectral.nm730),
                    },
                    SpotId = patch.SpotId,
                    SpotName = patch.SpotName
                });
            }

        }
        #endregion

        #region IViewImportMeasurements Members
        public List<Project> Projects { get; set; }
        public int ProjectId { get; set; }
        public int DeviceId { get; set; }
        public int LicenseTypeID { get; set; }
        public List<int> Measurements { get; set; }
        public int MeasurementID { get; set; }
        public int OptimizeRunID { get; set; }
        public Entity.Measurement Measurement { get; set; }
        #endregion
    }
}