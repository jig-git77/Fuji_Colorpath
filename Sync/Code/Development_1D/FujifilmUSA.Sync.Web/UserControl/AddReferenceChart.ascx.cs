using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    //Temporary class to store import data to viewstate by serializing them...
    [Serializable]
    public class ReferenceChartImportData
    {
        public short SampleId { get; set; }
        public string SampleName { get; set; }
        public decimal CmykC { get; set; }
        public decimal CmykM { get; set; }
        public decimal CmykY { get; set; }
        public decimal CmykK { get; set; }
        public string HTMLColor { get; set; }
        public decimal Channel_5 { get; set; }
        public decimal Channel_6 { get; set; }
        public decimal Channel_7 { get; set; }
        public decimal Channel_8 { get; set; }
        public decimal Channel_9 { get; set; }
    }

    public partial class AddReferenceChart : BaseControl, IViewAddReferenceChart
    {
        #region Variable Declarations
        private PresenterAddReferenceChart _presenterAddReferenceChart;

        private const string vsChartData = "_vsChartData";
        private const string _recEditRefChart = "editRefChart";
        private const string _recSelectProjectType = "selectProjectType";
        private const string _recSelectInstrumentType = "selectInstrumentType";

        #endregion
        public AddReferenceChart()
        {
            Load += (sender, e) => ExceptionHandling(Page_Load, sender, e);
        }
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterAddReferenceChart = new PresenterAddReferenceChart(this);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            trChannel.Visible = false;
            trTotalNoColor.Visible = false;

            if (!IsPostBack)
            {
                BindInstrumentTypeList();
                BindMultiColorChannel();
                BindDeviceFamilyList();
                if (Request.QueryString["RefChartId"] != null)
                {
                    var RefChartId = Request.QueryString["RefChartId"];
                    var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(RefChartId, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                    int _RefChartId;
                    if (Int32.TryParse(id, out _RefChartId))
                    {
                        lblHeader.Text = GetResourceString(_recEditRefChart);//"Edit Reference Chart";
                        LoadRefChart(_RefChartId);
                    }
                }
            }
        }

        #endregion

        #region Control Events
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cmbInstrumentType.SelectedValue) == (int)EInstrumentType.i1IO || Convert.ToInt16(cmbInstrumentType.SelectedValue) == (int)EInstrumentType.i1IOPro2 || Convert.ToInt16(cmbInstrumentType.SelectedValue) == (int)EInstrumentType.i1Sis)
            {
                if (string.IsNullOrEmpty(txtChartSetting.Text))
                {
                    fujiStatusbar.DisplayMessage("Please add ChartSetting XML.", false);
                    return;
                }
                else if (!string.IsNullOrEmpty(txtChartSetting.Text) && !IsChartSettingXMLValid(txtChartSetting.Text))
                {
                    fujiStatusbar.DisplayMessage("Invalid ChartSetting XML.", false);
                    return;
                }
            }
            if (chkIsSupportMulticolor.Checked)
            {
                trTotalNoColor.Visible = true;
                if (drpChannel5.SelectedValue == "0")
                {
                    fujiStatusbar.DisplayMessage("Please select channel5", false);
                    return;

                }
                if (Convert.ToInt16(drpTotalNoColor.SelectedValue) >= 0)
                {
                    trChannel.Visible = true;
                }
                if (Convert.ToInt16(drpTotalNoColor.SelectedValue) == 5)
                {
                    if (drpChannel5.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel5", false);
                        return;

                    }

                }
                else if (Convert.ToInt16(drpTotalNoColor.SelectedValue) == 6)
                {
                    if (drpChannel5.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel5", false);
                        return;

                    }
                    if (drpChannel6.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel6", false);
                        return;
                    }

                }
                else if (Convert.ToInt16(drpTotalNoColor.SelectedValue) == 7)
                {
                    if (drpChannel5.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel5", false);
                        return;

                    }
                    if (drpChannel6.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel6", false);
                        return;
                    }
                    if (drpChannel7.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel7", false);
                        return;
                    }

                }
                else if (Convert.ToInt16(drpTotalNoColor.SelectedValue) == 8)
                {
                    if (drpChannel5.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel5", false);
                        return;

                    }
                    if (drpChannel6.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel6", false);
                        return;
                    }
                    if (drpChannel7.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel7", false);
                        return;
                    }
                    if (drpChannel8.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel8", false);
                        return;
                    }

                }
                else if (Convert.ToInt16(drpTotalNoColor.SelectedValue) == 9)
                {
                    if (drpChannel5.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel5", false);
                        return;

                    }
                    if (drpChannel6.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel6", false);
                        return;
                    }
                    if (drpChannel7.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel7", false);
                        return;
                    }
                    if (drpChannel8.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel8", false);
                        return;
                    }
                    if (drpChannel9.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel9", false);
                        return;

                    }

                }
            }

            ReferenceChartType = new ReferenceChartType();
            ReferenceChartType.ReferenceChartTypeId = (short)Convert.ToInt32(hdnRefChartID.Value);
            ReferenceChartType.ChartName = txtChartName.Text.Trim();
            ReferenceChartType.NumOfRows = Convert.ToInt32(txtNumOfRows.Text);
            ReferenceChartType.NumOfCols = Convert.ToInt32(txtNumOfCols.Text);
            ReferenceChartType.InstrumentTypeId = Convert.ToInt16(cmbInstrumentType.SelectedValue);
            if (drpDeviceFamilyId.SelectedIndex > 0)
                ReferenceChartType.DeviceFamilyId = Convert.ToInt16(drpDeviceFamilyId.SelectedValue);
            ReferenceChartType.IsSupportCloudFlow = chkIsSupportCloudFlow.Checked;
            ReferenceChartType.IsUsed = chkIsUsed.Checked;
            ReferenceChartType.ChartSettingXML = string.IsNullOrEmpty(txtChartSetting.Text) ? null : txtChartSetting.Text;
            if (chklist_ProjectType.Items[0].Selected == true)
            {
                ReferenceChartType.IsSync1D_ISO = short.Parse(cmbSync1D_ISO.SelectedValue);
            }

            if (chklist_ProjectType.Items[1].Selected == true)
            {
                ReferenceChartType.IsSync1D_G7 = short.Parse(cmbSync1D_G7.SelectedValue);
            }

            if (chklist_ProjectType.Items[2].Selected == true)
            {
                ReferenceChartType.IsSync4D = short.Parse(cmbSync4D.SelectedValue);
            }

            if (chklist_Validation.Items[0].Selected == true)
            {
                ReferenceChartType.Validation_1D = short.Parse(cmbValidation_1D.SelectedValue);
            }

            if (chklist_Validation.Items[1].Selected == true)
            {
                ReferenceChartType.Validation_4D = short.Parse(cmbValidation_4D.SelectedValue);
            }

            if (chklist_ProjectType.Items[3].Selected == true)
            {
                ReferenceChartType.IsSync4D_Plus = short.Parse(cmbSync4DPlus.SelectedValue);
            }

            if (chklist_Validation.Items[2].Selected == true)
            {
                ReferenceChartType.Validation_4DPlus = short.Parse(cmbValidation4DPlus.SelectedValue);
            }

            if (chklist_ProjectType.Items[4].Selected == true)
            {
                ReferenceChartType.IsDLPMultiColor = short.Parse(cmbSync4DMultiColor.SelectedValue);
            }
            if (chklist_ProjectType.Items[5].Selected == true)
            {
                ReferenceChartType.IsSync1DMultiColor_ISO = short.Parse(cmbSync1DMultiColor_ISO.SelectedValue);
            }
            if (chklist_ProjectType.Items[6].Selected == true)
            {
                ReferenceChartType.IsSync1DMultiColor_G7 = short.Parse(cmbSync1DMultiColor_G7.SelectedValue);
            }

            if (chklist_Validation.Items[3].Selected == true)
            {
                ReferenceChartType.Validation_4D_Mutli_Color = short.Parse(cmbValidation_4D_Multi_Color.SelectedValue);
            }
            if (chklist_Validation.Items[4].Selected == true)
            {
                ReferenceChartType.Validation_Sync1D_Multi_Color = short.Parse(cmbValidation_Sync1D_Multi_Color.SelectedValue);
            }

            ReferenceChartType.IsSupportMulticolor = chkIsSupportMulticolor.Checked;
            if (chkIsSupportMulticolor.Checked)
            {
                ReferenceChartType.TotalNoColor = Convert.ToInt16(drpTotalNoColor.SelectedValue);
                ReferenceChartType.Channel_5 = Convert.ToInt16(drpChannel5.SelectedValue);
                ReferenceChartType.Channel_6 = Convert.ToInt16(drpChannel6.SelectedValue);
                ReferenceChartType.Channel_7 = Convert.ToInt16(drpChannel7.SelectedValue);
                ReferenceChartType.Channel_8 = Convert.ToInt16(drpChannel8.SelectedValue);
                ReferenceChartType.Channel_9 = Convert.ToInt16(drpChannel9.SelectedValue);
            }
            else
            {
                ReferenceChartType.TotalNoColor = 0;
                ReferenceChartType.Channel_5 = null;
                ReferenceChartType.Channel_6 = null;
                ReferenceChartType.Channel_7 = null;
                ReferenceChartType.Channel_8 = null;
                ReferenceChartType.Channel_9 = null;
            }

            if ((fucUploadChartPreview.UploadedFiles.Count > 0 || Convert.ToInt32(Hidden_ChartPreview.Value) > 0) && (fucUploadOriginalChart.UploadedFiles.Count > 0 || Convert.ToInt32(Hidden_OriginalChart.Value) > 0))
            {
                if (fucUploadOriginalChart.UploadedFiles.Count > 0)
                {
                    var memoryStream = new MemoryStream();
                    fucUploadOriginalChart.UploadedFiles[0].InputStream.CopyTo(memoryStream);
                    ReferenceChartType.OriginalChart = memoryStream.ToArray();
                }
                if (fucUploadChartPreview.UploadedFiles.Count > 0)
                {
                    var memoryStream = new MemoryStream();
                    fucUploadChartPreview.UploadedFiles[0].InputStream.CopyTo(memoryStream);
                    ReferenceChartType.ChartPreview = memoryStream.ToArray();
                }

                if (hdnRefChartID.Value == "0")
                {
                    if (ViewState[vsChartData] != null)
                    {
                        ReferenceChartType.ReferenceCharts = ((List<ReferenceChartImportData>)ViewState[vsChartData]).
                                                                 Select(x => new ReferenceChart()
                                                                 {
                                                                     SampleId = x.SampleId,
                                                                     SampleName = x.SampleName,
                                                                     CmykC = x.CmykC,
                                                                     CmykM = x.CmykM,
                                                                     CmykY = x.CmykY,
                                                                     CmykK = x.CmykK,
                                                                     HtmlColor = x.HTMLColor,
                                                                     Channel_5 = x.Channel_5,
                                                                     Channel_6 = x.Channel_6,
                                                                     Channel_7 = x.Channel_7,
                                                                     Channel_8 = x.Channel_8,
                                                                     Channel_9 = x.Channel_9
                                                                 }).ToList();


                        _presenterAddReferenceChart.SaveReferenceChart();
                        if (IsErrorOccured)
                        {
                            return;
                        }

                        Response.Redirect("ReferenceChartManagement.aspx");
                    }
                }
                else
                {
                    _presenterAddReferenceChart.UpdateReferenceChart();
                    if (IsErrorOccured)
                    {
                        return;
                    }

                    Response.Redirect("ReferenceChartManagement.aspx");
                }
            }
            else
            {
                if (hdnRefChartID.Value == "0")   ///-----------Missing message for Add Chart
                {
                    fujiStatusbar.DisplayMessage(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_MissingUploadFile"), false);
                }
                else                             ///-----------Missing message for Edit Chart
                {
                    fujiStatusbar.DisplayMessage(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_MissingEditFile"), false);
                }
            }
        }

        protected void lbtnUploadChartData_Click(object sender, EventArgs e)
        {
            if (fucUploadChartData.UploadedFiles.Count > 0)
            {
                try
                {
                    var sr = new StreamReader(fucUploadChartData.UploadedFiles[0].InputStream);
                    var rows = 0;
                    var refChartImportData = new List<ReferenceChartImportData>();
                    while (!sr.EndOfStream)
                    {
                        rows++;
                        if (rows == 1)
                        {
                            #region Check Column Names
                            var columns = sr.ReadLine().Split(',');
                            if (chkIsSupportMulticolor.Checked)
                            {
                                trChannel.Visible = true;
                                if (Convert.ToInt16(drpTotalNoColor.SelectedValue) >= 0)
                                {
                                    trTotalNoColor.Visible = true;
                                }
                                int fixedColumn = 7;
                                if (drpTotalNoColor.SelectedValue == "5")
                                {
                                    fixedColumn = fixedColumn + 1;
                                }
                                else if (drpTotalNoColor.SelectedValue == "6")
                                {
                                    fixedColumn = fixedColumn + 2;
                                }
                                else if (drpTotalNoColor.SelectedValue == "7")
                                {
                                    fixedColumn = fixedColumn + 3;
                                }
                                else if (drpTotalNoColor.SelectedValue == "8")
                                {
                                    fixedColumn = fixedColumn + 4;
                                }
                                else if (drpTotalNoColor.SelectedValue == "9")
                                {
                                    fixedColumn = fixedColumn + 5;
                                }

                                if (columns.Length != fixedColumn)
                                {
                                    throw new Exception(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnCountNotMatch"));
                                }
                            }
                            else
                            {
                                if (columns.Length != 7)
                                {
                                    throw new Exception(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnCountNotMatch"));
                                }
                            }
                            if (chkIsSupportMulticolor.Checked)
                            {
                                trChannel.Visible = true;
                                if (Convert.ToInt16(drpTotalNoColor.SelectedValue) >= 0)
                                {
                                    trTotalNoColor.Visible = true;
                                }
                                if (drpTotalNoColor.SelectedValue == "5")
                                {
                                    columns.ToList().ForEach(x => x = x.Trim());
                                    if (!columns[0].Equals("SampleID", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[1].Equals("SAMPLE_NAME", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[2].Equals("CMYK_C", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[3].Equals("CMYK_M", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[4].Equals("CMYK_Y", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[5].Equals("CMYK_K", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[6].Equals("HTML_RGB", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[7].Equals("CHANNEL_5", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        throw new Exception(FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnNamesNotMatch"));
                                    }
                                }
                                else if (drpTotalNoColor.SelectedValue == "6")
                                {
                                    columns.ToList().ForEach(x => x = x.Trim());
                                    if (!columns[0].Equals("SampleID", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[1].Equals("SAMPLE_NAME", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[2].Equals("CMYK_C", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[3].Equals("CMYK_M", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[4].Equals("CMYK_Y", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[5].Equals("CMYK_K", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[6].Equals("HTML_RGB", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[7].Equals("CHANNEL_5", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[8].Equals("CHANNEL_6", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        throw new Exception(FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnNamesNotMatch"));
                                    }
                                }
                                else if (drpTotalNoColor.SelectedValue == "7")
                                {
                                    columns.ToList().ForEach(x => x = x.Trim());
                                    if (!columns[0].Equals("SampleID", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[1].Equals("SAMPLE_NAME", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[2].Equals("CMYK_C", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[3].Equals("CMYK_M", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[4].Equals("CMYK_Y", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[5].Equals("CMYK_K", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[6].Equals("HTML_RGB", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[7].Equals("CHANNEL_5", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[8].Equals("CHANNEL_6", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[9].Equals("CHANNEL_7", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        throw new Exception(FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnNamesNotMatch"));
                                    }
                                }
                                else if (drpTotalNoColor.SelectedValue == "8")
                                {
                                    columns.ToList().ForEach(x => x = x.Trim());
                                    if (!columns[0].Equals("SampleID", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[1].Equals("SAMPLE_NAME", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[2].Equals("CMYK_C", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[3].Equals("CMYK_M", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[4].Equals("CMYK_Y", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[5].Equals("CMYK_K", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[6].Equals("HTML_RGB", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[7].Equals("CHANNEL_5", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[8].Equals("CHANNEL_6", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[9].Equals("CHANNEL_7", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[10].Equals("CHANNEL_8", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        throw new Exception(FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnNamesNotMatch"));
                                    }
                                }
                                else if (drpTotalNoColor.SelectedValue == "9")
                                {
                                    columns.ToList().ForEach(x => x = x.Trim());
                                    if (!columns[0].Equals("SampleID", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[1].Equals("SAMPLE_NAME", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[2].Equals("CMYK_C", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[3].Equals("CMYK_M", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[4].Equals("CMYK_Y", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[5].Equals("CMYK_K", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[6].Equals("HTML_RGB", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[7].Equals("CHANNEL_5", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[8].Equals("CHANNEL_6", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[9].Equals("CHANNEL_7", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[10].Equals("CHANNEL_8", StringComparison.CurrentCultureIgnoreCase) ||
                                        !columns[11].Equals("CHANNEL_9", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        throw new Exception(FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnNamesNotMatch"));
                                    }
                                }

                            }

                            else
                            {
                                columns.ToList().ForEach(x => x = x.Trim());
                                if (!columns[0].Equals("SampleID", StringComparison.CurrentCultureIgnoreCase) ||
                                    !columns[1].Equals("SAMPLE_NAME", StringComparison.CurrentCultureIgnoreCase) ||
                                    !columns[2].Equals("CMYK_C", StringComparison.CurrentCultureIgnoreCase) ||
                                    !columns[3].Equals("CMYK_M", StringComparison.CurrentCultureIgnoreCase) ||
                                    !columns[4].Equals("CMYK_Y", StringComparison.CurrentCultureIgnoreCase) ||
                                    !columns[5].Equals("CMYK_K", StringComparison.CurrentCultureIgnoreCase) ||
                                    !columns[6].Equals("HTML_RGB", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    throw new Exception(FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnNamesNotMatch"));
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            var dataRow = sr.ReadLine().Split(',');
                            //Comment while implement MultiColor 
                            //if (cmbHowManyColors.SelectedIndex == 0)
                            //{
                            //    if (dataRow.Length != 7)
                            //    {
                            //        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnCountNotMatchOnRow"), rows));
                            //    }
                            //}
                            //else
                            //{
                            //    if (dataRow.Length != 10)
                            //    {
                            //        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnCountNotMatchOnRow"), rows));
                            //    }
                            //}
                            dataRow.ToList().ForEach(x => x = x.Trim());
                            var chartDataRow = new ReferenceChartImportData();
                            #region Converts row to ReferenceChart Object
                            short sampleId;
                            decimal cmyk;
                            //SampleID
                            if (short.TryParse(dataRow[0], out sampleId))
                            {
                                chartDataRow.SampleId = sampleId;
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //SampleName
                            if (!string.IsNullOrEmpty(dataRow[1]))
                            {
                                chartDataRow.SampleName = dataRow[1];
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnEmptyValueOnRow"), rows));
                            }

                            //CMYK_C
                            if (decimal.TryParse(dataRow[2], out cmyk))
                            {
                                chartDataRow.CmykC = cmyk;
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //CMYK_M
                            if (decimal.TryParse(dataRow[3], out cmyk))
                            {
                                chartDataRow.CmykM = cmyk;
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //CMYK_Y
                            if (decimal.TryParse(dataRow[4], out cmyk))
                            {
                                chartDataRow.CmykY = cmyk;
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //CMYK_K
                            if (decimal.TryParse(dataRow[5], out cmyk))
                            {
                                chartDataRow.CmykK = cmyk;
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //HTMLColor
                            if (!string.IsNullOrEmpty(dataRow[6]))
                            {
                                chartDataRow.HTMLColor = dataRow[6];
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnEmptyValueOnRow"), rows));
                            }
                            //PatchTypeId
                            //TODO: Need to calculate patch type Id
                            //Comment while implement MultiColor 
                            //if (cmbHowManyColors.SelectedIndex > 0)
                            //{
                            //    //Color5
                            //    if (decimal.TryParse(dataRow[7], out cmyk))
                            //    {
                            //        chartDataRow.Color5 = cmyk;
                            //    }
                            //    else
                            //    {
                            //        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            //    }

                            //    //Color6
                            //    if (decimal.TryParse(dataRow[8], out cmyk))
                            //    {
                            //        chartDataRow.Color6 = cmyk;
                            //    }
                            //    else
                            //    {
                            //        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            //    }

                            //    //Color7
                            //    if (decimal.TryParse(dataRow[9], out cmyk))
                            //    {
                            //        chartDataRow.Color7 = cmyk;
                            //    }
                            //    else
                            //    {
                            //        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            //    }

                            //}

                            if (chkIsSupportMulticolor.Checked)
                            {
                                if (drpTotalNoColor.SelectedValue == "5")
                                {
                                    //Color5
                                    if (decimal.TryParse(dataRow[7], out cmyk))
                                    {
                                        chartDataRow.Channel_5 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }


                                }
                                else if (drpTotalNoColor.SelectedValue == "6")
                                {
                                    //Color5
                                    if (decimal.TryParse(dataRow[7], out cmyk))
                                    {
                                        chartDataRow.Channel_5 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }
                                    //Color6
                                    if (decimal.TryParse(dataRow[8], out cmyk))
                                    {
                                        chartDataRow.Channel_6 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }


                                }
                                else if (drpTotalNoColor.SelectedValue == "7")
                                {
                                    //Color5
                                    if (decimal.TryParse(dataRow[7], out cmyk))
                                    {
                                        chartDataRow.Channel_5 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }
                                    //Color6
                                    if (decimal.TryParse(dataRow[8], out cmyk))
                                    {
                                        chartDataRow.Channel_6 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }
                                    //Color7
                                    if (decimal.TryParse(dataRow[9], out cmyk))
                                    {
                                        chartDataRow.Channel_7 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }

                                }
                                else if (drpTotalNoColor.SelectedValue == "8")
                                {
                                    //Color5
                                    if (decimal.TryParse(dataRow[7], out cmyk))
                                    {
                                        chartDataRow.Channel_5 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }
                                    //Color6
                                    if (decimal.TryParse(dataRow[8], out cmyk))
                                    {
                                        chartDataRow.Channel_6 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }
                                    //Color7
                                    if (decimal.TryParse(dataRow[9], out cmyk))
                                    {
                                        chartDataRow.Channel_7 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }
                                    //Color8
                                    if (decimal.TryParse(dataRow[10], out cmyk))
                                    {
                                        chartDataRow.Channel_8 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }

                                }
                                else if (drpTotalNoColor.SelectedValue == "9")
                                {
                                    //Color5
                                    if (decimal.TryParse(dataRow[7], out cmyk))
                                    {
                                        chartDataRow.Channel_5 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }
                                    //Color6
                                    if (decimal.TryParse(dataRow[8], out cmyk))
                                    {
                                        chartDataRow.Channel_6 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }
                                    //Color7
                                    if (decimal.TryParse(dataRow[9], out cmyk))
                                    {
                                        chartDataRow.Channel_7 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }
                                    //Color8
                                    if (decimal.TryParse(dataRow[10], out cmyk))
                                    {
                                        chartDataRow.Channel_8 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }
                                    //Color9
                                    if (decimal.TryParse(dataRow[11], out cmyk))
                                    {
                                        chartDataRow.Channel_9 = cmyk;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                                    }
                                }

                            }

                            #endregion
                            refChartImportData.Add(chartDataRow);
                        }
                    }
                    if (refChartImportData.Count > 0)
                    {
                        ViewState[vsChartData] = refChartImportData;
                        rgChartData.Rebind();
                    }
                }
                catch (Exception ex)
                {
                    fujiStatusbar.DisplayMessage(ex.Message, false);
                }
            }
        }

        protected void rgChartData_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (chkIsSupportMulticolor.Checked)
            {

                trTotalNoColor.Visible = true;
                if (Convert.ToInt16(drpTotalNoColor.SelectedValue) > 0)
                {
                    trChannel.Visible = true;
                    SetMultiColorChannel();
                }
            }
            if (ViewState[vsChartData] != null)
            {

                rgChartData.DataSource = ViewState[vsChartData];
                if (chkIsSupportMulticolor.Checked)
                {
                    if (drpTotalNoColor.SelectedValue == "5")
                    {
                        rgChartData.Columns[0].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[1].HeaderStyle.Width = Unit.Percentage(15);
                        rgChartData.Columns[2].HeaderStyle.Width = Unit.Percentage(15);
                        rgChartData.Columns[3].HeaderStyle.Width = Unit.Percentage(15);
                        rgChartData.Columns[4].HeaderStyle.Width = Unit.Percentage(15);
                        rgChartData.Columns[5].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[6].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[7].HeaderStyle.Width = Unit.Percentage(10);

                        rgChartData.Columns[7].Visible = true;
                        rgChartData.Columns[8].Visible = false;
                        rgChartData.Columns[9].Visible = false;
                        rgChartData.Columns[10].Visible = false;
                        rgChartData.Columns[11].Visible = false;
                    }
                    else if (drpTotalNoColor.SelectedValue == "6")
                    {
                        rgChartData.Columns[0].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[1].HeaderStyle.Width = Unit.Percentage(15);
                        rgChartData.Columns[2].HeaderStyle.Width = Unit.Percentage(15);
                        rgChartData.Columns[3].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[4].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[5].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[6].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[7].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[8].HeaderStyle.Width = Unit.Percentage(10);

                        rgChartData.Columns[7].Visible = true;
                        rgChartData.Columns[8].Visible = true;
                        rgChartData.Columns[9].Visible = false;
                        rgChartData.Columns[10].Visible = false;
                        rgChartData.Columns[11].Visible = false;
                    }
                    else if (drpTotalNoColor.SelectedValue == "7")
                    {
                        rgChartData.Columns[0].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[1].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[2].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[3].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[4].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[5].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[6].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[7].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[8].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[9].HeaderStyle.Width = Unit.Percentage(10);

                        rgChartData.Columns[7].Visible = true;
                        rgChartData.Columns[8].Visible = true;
                        rgChartData.Columns[9].Visible = true;
                        rgChartData.Columns[10].Visible = false;
                        rgChartData.Columns[11].Visible = false;
                    }
                    else if (drpTotalNoColor.SelectedValue == "8")
                    {
                        rgChartData.Columns[0].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[1].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[2].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[3].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[4].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[5].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[6].HeaderStyle.Width = Unit.Percentage(8);
                        rgChartData.Columns[7].HeaderStyle.Width = Unit.Percentage(8);
                        rgChartData.Columns[8].HeaderStyle.Width = Unit.Percentage(8);
                        rgChartData.Columns[9].HeaderStyle.Width = Unit.Percentage(8);
                        rgChartData.Columns[10].HeaderStyle.Width = Unit.Percentage(8);

                        rgChartData.Columns[7].Visible = true;
                        rgChartData.Columns[8].Visible = true;
                        rgChartData.Columns[9].Visible = true;
                        rgChartData.Columns[10].Visible = true;
                        rgChartData.Columns[11].Visible = false;
                    }
                    else if (drpTotalNoColor.SelectedValue == "9")
                    {
                        rgChartData.Columns[0].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[1].HeaderStyle.Width = Unit.Percentage(10);
                        rgChartData.Columns[2].HeaderStyle.Width = Unit.Percentage(8);
                        rgChartData.Columns[3].HeaderStyle.Width = Unit.Percentage(8);
                        rgChartData.Columns[4].HeaderStyle.Width = Unit.Percentage(8);
                        rgChartData.Columns[5].HeaderStyle.Width = Unit.Percentage(8);
                        rgChartData.Columns[6].HeaderStyle.Width = Unit.Percentage(8);
                        rgChartData.Columns[7].HeaderStyle.Width = Unit.Percentage(8);
                        rgChartData.Columns[8].HeaderStyle.Width = Unit.Percentage(8);
                        rgChartData.Columns[9].HeaderStyle.Width = Unit.Percentage(8);
                        rgChartData.Columns[10].HeaderStyle.Width = Unit.Percentage(8);
                        rgChartData.Columns[11].HeaderStyle.Width = Unit.Percentage(8);

                        rgChartData.Columns[7].Visible = true;
                        rgChartData.Columns[8].Visible = true;
                        rgChartData.Columns[9].Visible = true;
                        rgChartData.Columns[10].Visible = true;
                        rgChartData.Columns[11].Visible = true;
                    }
                }
                else
                {
                    rgChartData.Columns[0].HeaderStyle.Width = Unit.Percentage(10);
                    rgChartData.Columns[1].HeaderStyle.Width = Unit.Percentage(15);
                    rgChartData.Columns[2].HeaderStyle.Width = Unit.Percentage(15);
                    rgChartData.Columns[3].HeaderStyle.Width = Unit.Percentage(15);
                    rgChartData.Columns[4].HeaderStyle.Width = Unit.Percentage(15);
                    rgChartData.Columns[5].HeaderStyle.Width = Unit.Percentage(15);
                    rgChartData.Columns[6].HeaderStyle.Width = Unit.Percentage(15);
                }
            }
        }

        protected void cmbInstrumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInstrumentType.SelectedValue == ((int)EInstrumentType.i1IO).ToString() || cmbInstrumentType.SelectedValue == ((int)EInstrumentType.i1Sis).ToString())
            {
                trChartSetting.Visible = true;
            }
            else
            {
                trChartSetting.Visible = false;
            }
        }

        protected void chkIsSupportMulticolor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsSupportMulticolor.Checked)
            {

                trTotalNoColor.Visible = true;

            }
            else
            {
                locUploadChartData.Text = "Upload Chart Data(SAMPLEID, SAMPLE_NAME, CMYK_C, CMYK_M, CMYK_Y, CMYK_K, HTML_RGB)";
                trTotalNoColor.Visible = false;

            }

        }
        protected void drpTotalNoColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FixLabel = "Upload Chart Data(SAMPLEID, SAMPLE_NAME, CMYK_C, CMYK_M, CMYK_Y, CMYK_K, HTML_RGB)";
            if (drpTotalNoColor.SelectedIndex > 0)
            {

                trTotalNoColor.Visible = true;
                trChannel.Visible = true;
                SetMultiColorChannel();
                if (drpTotalNoColor.SelectedValue == "5")
                {
                    locUploadChartData.Text = FixLabel.Replace(")", "") + ",CHANNEL_5)";
                }
                else if (drpTotalNoColor.SelectedValue == "6")
                {
                    locUploadChartData.Text = FixLabel.Replace(")", "") + ",CHANNEL_5,CHANNEL_6)";
                }
                else if (drpTotalNoColor.SelectedValue == "7")
                {
                    locUploadChartData.Text = FixLabel.Replace(")", "") + ",CHANNEL_5,CHANNEL_6,CHANNEL_7)";
                }
                else if (drpTotalNoColor.SelectedValue == "8")
                {
                    locUploadChartData.Text = FixLabel.Replace(")", "") + ",CHANNEL_5,CHANNEL_6,CHANNEL_7,CHANNEL_8)";
                }
                else if (drpTotalNoColor.SelectedValue == "9")
                {
                    locUploadChartData.Text = FixLabel.Replace(")", "") + ",CHANNEL_5,CHANNEL_6,CHANNEL_7,CHANNEL_9)";
                }

            }
            else
            {
                locUploadChartData.Text = FixLabel;
                trChannel.Visible = false;
            }

        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ReferenceChartManagement.aspx");
        }

        #endregion

        #region Methods     
        private void SetMultiColorChannel()
        {
            if (drpTotalNoColor.SelectedValue == "5")
            {
                trChannel5.Visible = true;
                trChannel6.Visible = false;
                trChannel7.Visible = false;
                trChannel8.Visible = false;
                trChannel9.Visible = false;
                drpChannel6.SelectedValue = "0";
                drpChannel7.SelectedValue = "0";
                drpChannel8.SelectedValue = "0";
                drpChannel9.SelectedValue = "0";

            }
            else if (drpTotalNoColor.SelectedValue == "6")
            {
                trChannel5.Visible = true;
                trChannel6.Visible = true;
                trChannel7.Visible = false;
                trChannel8.Visible = false;
                trChannel9.Visible = false;

                drpChannel7.SelectedValue = "0";
                drpChannel8.SelectedValue = "0";
                drpChannel9.SelectedValue = "0";
            }
            else if (drpTotalNoColor.SelectedValue == "7")
            {
                trChannel5.Visible = true;
                trChannel6.Visible = true;
                trChannel7.Visible = true;
                trChannel8.Visible = false;
                trChannel9.Visible = false;


                drpChannel8.SelectedValue = "0";
                drpChannel9.SelectedValue = "0";
            }
            else if (drpTotalNoColor.SelectedValue == "8")
            {
                trChannel5.Visible = true;
                trChannel6.Visible = true;
                trChannel7.Visible = true;
                trChannel8.Visible = true;
                trChannel9.Visible = false;

                drpChannel9.SelectedValue = "0";
            }
            else if (drpTotalNoColor.SelectedValue == "9")
            {
                trChannel5.Visible = true;
                trChannel6.Visible = true;
                trChannel7.Visible = true;
                trChannel8.Visible = true;
                trChannel9.Visible = true;
            }
        }
        private void BindInstrumentTypeList()
        {
            _presenterAddReferenceChart.GetInstrumentTypeList();
            if (!IsErrorOccured)
            {
                cmbInstrumentType.DataSource = InstrumentTypeList;
                cmbInstrumentType.DataBind();
                cmbInstrumentType.Items.Insert(0, new ListItem(GetResourceString(_recSelectInstrumentType), ""));//"-- Select Instrument Type --"
            }
        }

        /// <summary>
        /// BindDeviceFamilyList
        /// </summary>
        private void BindDeviceFamilyList()
        {
            _presenterAddReferenceChart.GetDeviceFamilyList();
            if (!IsErrorOccured)
            {
                drpDeviceFamilyId.DataTextField = "Value";
                drpDeviceFamilyId.DataValueField = "Key";
                drpDeviceFamilyId.DataSource = DeviceFamilyList;
                drpDeviceFamilyId.DataBind();
                drpDeviceFamilyId.Items.Insert(0, "--Select Device Family--"); //"--Select Device Family--
            }
        }

        private void LoadRefChart(int RefChartId)
        {
            ReferenceChartType = new ReferenceChartType { ReferenceChartTypeId = (short)RefChartId };
            _presenterAddReferenceChart.GetReferenceChartDetails();
            if (!IsErrorOccured)
            {
                Tr_UploadData.Visible = false;
                hdnRefChartID.Value = Convert.ToString(RefChartId);
                txtChartName.Text = ReferenceChartType.ChartName.ToString();
                txtNumOfRows.Text = ReferenceChartType.NumOfRows.ToString();
                txtNumOfCols.Text = ReferenceChartType.NumOfCols.ToString();
                cmbInstrumentType.SelectedValue = Convert.ToString(ReferenceChartType.InstrumentType.InstrumentTypeId);
                drpDeviceFamilyId.SelectedValue = Convert.ToString(ReferenceChartType.DeviceFamilyId);
                chkIsSupportCloudFlow.Checked = (ReferenceChartType.IsSupportCloudFlow == true ? true : false);
                
                if (ReferenceChartType.InstrumentType.InstrumentTypeId == (int)EInstrumentType.i1IO || ReferenceChartType.InstrumentType.InstrumentTypeId == (int)EInstrumentType.i1IOPro2 || ReferenceChartType.InstrumentType.InstrumentTypeId == (int)EInstrumentType.i1Sis)
                {
                    trChartSetting.Visible = true;
                    txtChartSetting.Text = ReferenceChartType.ChartSettingXML;
                }
                chkIsUsed.Checked = (ReferenceChartType.IsUsed == true ? true : false);

                if (ReferenceChartType.IsSync1D_ISO >= 1)
                {
                    chklist_ProjectType.Items[0].Selected = true;
                    cmbSync1D_ISO.SelectedValue = ReferenceChartType.IsSync1D_ISO.ToString();
                }
                if (ReferenceChartType.IsSync1D_G7 >= 1)
                {
                    chklist_ProjectType.Items[1].Selected = true;
                    cmbSync1D_G7.SelectedValue = ReferenceChartType.IsSync1D_G7.ToString();
                }
                if (ReferenceChartType.IsSync4D >= 1)
                {
                    chklist_ProjectType.Items[2].Selected = true;
                    cmbSync4D.SelectedValue = ReferenceChartType.IsSync4D.ToString();
                }
                if (ReferenceChartType.Validation_1D >= 1)
                {
                    chklist_Validation.Items[0].Selected = true;
                    cmbValidation_1D.SelectedValue = ReferenceChartType.Validation_1D.ToString();
                }
                if (ReferenceChartType.Validation_4D >= 1)
                {
                    chklist_Validation.Items[1].Selected = true;
                    cmbValidation_4D.SelectedValue = ReferenceChartType.Validation_4D.ToString();
                }

                if (ReferenceChartType.IsSync4D_Plus >= 1)
                {
                    chklist_ProjectType.Items[3].Selected = true;
                    cmbSync4DPlus.SelectedValue = ReferenceChartType.IsSync4D_Plus.ToString();
                }
                if (ReferenceChartType.Validation_4DPlus >= 1)
                {
                    chklist_Validation.Items[2].Selected = true;
                    cmbValidation4DPlus.SelectedValue = ReferenceChartType.Validation_4DPlus.ToString();
                }

                if (ReferenceChartType.IsDLPMultiColor >= 1)
                {
                    chklist_ProjectType.Items[4].Selected = true;
                    cmbSync4DMultiColor.SelectedValue = ReferenceChartType.IsDLPMultiColor.ToString();
                }
                if (ReferenceChartType.Validation_4D_Mutli_Color >= 1)
                {
                    chklist_Validation.Items[3].Selected = true;
                    cmbValidation_4D_Multi_Color.SelectedValue = ReferenceChartType.Validation_4D_Mutli_Color.ToString();
                }

                if (ReferenceChartType.IsSync1DMultiColor_ISO >= 1)
                {
                    chklist_ProjectType.Items[5].Selected = true;
                    cmbSync1DMultiColor_ISO.SelectedValue = ReferenceChartType.IsSync1DMultiColor_ISO.ToString();
                }
                if (ReferenceChartType.IsSync1DMultiColor_G7 >= 1)
                {
                    chklist_ProjectType.Items[6].Selected = true;
                    cmbSync1DMultiColor_G7.SelectedValue = ReferenceChartType.IsSync1DMultiColor_G7.ToString();
                }
                if (ReferenceChartType.Validation_Sync1D_Multi_Color >= 1)
                {
                    chklist_Validation.Items[4].Selected = true;
                    cmbValidation_Sync1D_Multi_Color.SelectedValue = ReferenceChartType.Validation_Sync1D_Multi_Color.ToString();
                }

                chkIsSupportMulticolor.Checked = ReferenceChartType.IsSupportMulticolor ?? false;
                chkIsSupportMulticolor.Enabled = false;
                if (ReferenceChartType.IsSupportMulticolor == true)
                {

                    trChannel.Visible = true;
                    trTotalNoColor.Visible = true;
                    drpTotalNoColor.SelectedValue = ReferenceChartType.TotalNoColor.ToString();
                    drpChannel5.SelectedValue = ReferenceChartType.Channel_5.ToString();
                    drpChannel6.SelectedValue = ReferenceChartType.Channel_6.ToString();
                    drpChannel7.SelectedValue = ReferenceChartType.Channel_7.ToString();
                    drpChannel8.SelectedValue = ReferenceChartType.Channel_8.ToString();
                    drpChannel9.SelectedValue = ReferenceChartType.Channel_9.ToString();

                    drpTotalNoColor.Enabled = false;
                    drpChannel5.Enabled = false;
                    drpChannel6.Enabled = false;
                    drpChannel7.Enabled = false;
                    drpChannel8.Enabled = false;
                    drpChannel9.Enabled = false;
                    SetMultiColorChannel();
                }
                else
                {
                    trChannel.Visible = false;
                    trTotalNoColor.Visible = false;
                }
                if (ReferenceChartType.ChartPreview != null)
                {
                    Hidden_ChartPreview.Value = ReferenceChartType.ChartPreview.Length.ToString();
                }
                if (ReferenceChartType.OriginalChart != null)
                {
                    Hidden_OriginalChart.Value = ReferenceChartType.OriginalChart.Length.ToString();
                }

            }
        }

        private bool IsChartSettingXMLValid(string xmlString)
        {
            try
            {
                XDocument doc = XDocument.Parse(xmlString);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        private void BindMultiColorChannel()
        {
            DataSet dataSet = _presenterAddReferenceChart.GetMultiColorChannelList();
            if (dataSet.Tables.Count > 0)
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    drpChannel5.DataSource = dataSet.Tables[0];
                    drpChannel5.DataValueField = "MultiColorChannelID";
                    drpChannel5.DataTextField = "MultiColorChannelName";
                    drpChannel5.DataBind();
                    drpChannel5.Items.Insert(0, new ListItem("---SELECT ANY----", "0"));

                    drpChannel6.DataSource = dataSet.Tables[0];
                    drpChannel6.DataValueField = "MultiColorChannelID";
                    drpChannel6.DataTextField = "MultiColorChannelName";
                    drpChannel6.DataBind();
                    drpChannel6.Items.Insert(0, new ListItem("---SELECT ANY----", "0"));

                    drpChannel7.DataSource = dataSet.Tables[0];
                    drpChannel7.DataValueField = "MultiColorChannelID";
                    drpChannel7.DataTextField = "MultiColorChannelName";
                    drpChannel7.DataBind();
                    drpChannel7.Items.Insert(0, new ListItem("---SELECT ANY----", "0"));

                    drpChannel8.DataSource = dataSet.Tables[0];
                    drpChannel8.DataValueField = "MultiColorChannelID";
                    drpChannel8.DataTextField = "MultiColorChannelName";
                    drpChannel8.DataBind();
                    drpChannel8.Items.Insert(0, new ListItem("---SELECT ANY----", "0"));

                    drpChannel9.DataSource = dataSet.Tables[0];
                    drpChannel9.DataValueField = "MultiColorChannelID";
                    drpChannel9.DataTextField = "MultiColorChannelName";
                    drpChannel9.DataBind();
                    drpChannel9.Items.Insert(0, new ListItem("---SELECT ANY----", "0"));

                }
            }

        }
        #endregion

        #region IViewAddReferenceChart Members

        public Dictionary<int, string> ProjectTypeList { get; set; }
        public Dictionary<int, string> InstrumentTypeList { get; set; }
        public ReferenceChartType ReferenceChartType { get; set; }
        public Dictionary<int, string> DeviceFamilyList { get; set; }

      

        #endregion
        //Comment while implement MultiColor 
        //protected void cmbHowManyColors_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //Comment while implement MultiColor 
        //    //if (cmbHowManyColors.SelectedIndex == 0)
        //    //{
        //    //    Tr_NameOfColors.Visible = false;
        //    //}
        //    //else
        //    //{
        //    //    Tr_NameOfColors.Visible = true;
        //    //}
        //}
    }
}