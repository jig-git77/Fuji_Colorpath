using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;

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
    }

    public partial class AddReferenceChart : BaseControl, IViewAddReferenceChart
    {
        #region Variable Declarations
        private PresenterAddReferenceChart _presenterAddReferenceChart;
        private const string vsChartData = "_vsChartData";
        #endregion

        #region Page Events
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterAddReferenceChart = new PresenterAddReferenceChart(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (!IsPostBack)
            {
                BindProjectTypeList();
                BindInstrumentTypeList();

                if (Request.QueryString["RefChartId"] != null)
                {
                    lblHeader.Text = "Edit Reference Chart";
                    LoadRefChart(Convert.ToInt32(Request.QueryString["RefChartId"]));
                }
            }
        }

        #endregion

        #region Control Events
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            ReferenceChartType = new ReferenceChartType();
            ReferenceChartType.ReferenceChartTypeId = (short)Convert.ToInt32(hdnRefChartID.Value);
            ReferenceChartType.ChartName = txtChartName.Text.Trim();
            ReferenceChartType.NumOfRows = Convert.ToInt32(txtNumOfRows.Text);
            ReferenceChartType.NumOfCols = Convert.ToInt32(txtNumOfCols.Text);
            ReferenceChartType.InstrumentTypeId = Convert.ToInt16(cmbInstrumentType.SelectedValue);
            ReferenceChartType.ProjectTypeId = Convert.ToInt16(cmbProjectType.SelectedValue);
            ReferenceChartType.IsUsed = chkIsUsed.Checked;

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
                                                                     CmykK = x.CmykK
                                                                 }).ToList();


                        _presenterAddReferenceChart.SaveReferenceChart();
                        Response.Redirect("ReferenceChartManagement.aspx");
                    }
                }
                else
                {
                    _presenterAddReferenceChart.UpdateReferenceChart();
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
                            if (columns.Length != 6)
                            {
                                throw new Exception(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnCountNotMatch"));
                            }
                            columns.ToList().ForEach(x => x = x.Trim());
                            if (!columns[0].Equals("SampleID", StringComparison.CurrentCultureIgnoreCase) ||
                                !columns[1].Equals("SAMPLE_NAME", StringComparison.CurrentCultureIgnoreCase) ||
                                !columns[2].Equals("CMYK_C", StringComparison.CurrentCultureIgnoreCase) ||
                                !columns[3].Equals("CMYK_M", StringComparison.CurrentCultureIgnoreCase) ||
                                !columns[4].Equals("CMYK_Y", StringComparison.CurrentCultureIgnoreCase) ||
                                !columns[5].Equals("CMYK_K", StringComparison.CurrentCultureIgnoreCase))
                            {
                                throw new Exception(FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnNamesNotMatch"));
                            }
                            #endregion
                        }
                        else
                        {
                            var dataRow = sr.ReadLine().Split(',');
                            if (dataRow.Length != 6)
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnCountNotMatchOnRow"), rows));
                            }
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

                            //PatchTypeId
                            //TODO: Need to calculate patch type Id

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
            if (ViewState[vsChartData] != null)
            {
                rgChartData.DataSource = ViewState[vsChartData];
            }
        }
        #endregion

        #region Methods
        private void BindProjectTypeList()
        {
            _presenterAddReferenceChart.GetProjectTypeList();
            if (!IsErrorOccured)
            {
                cmbProjectType.DataSource = ProjectTypeList;
                cmbProjectType.DataBind();
                cmbProjectType.Items.Insert(0, new ListItem("-- Select Project Type --", ""));
            }
        }

        private void BindInstrumentTypeList()
        {
            _presenterAddReferenceChart.GetInstrumentTypeList();
            if (!IsErrorOccured)
            {
                cmbInstrumentType.DataSource = InstrumentTypeList;
                cmbInstrumentType.DataBind();
                cmbInstrumentType.Items.Insert(0, new ListItem("-- Select Instrument Type --", ""));
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
                cmbProjectType.SelectedValue = Convert.ToString(ReferenceChartType.ProjectType.ProjectTypeId);
                chkIsUsed.Checked = (ReferenceChartType.IsUsed == true ? true : false);

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

        #endregion

        #region IViewAddReferenceChart Members

        public Dictionary<int, string> ProjectTypeList { get; set; }

        public Dictionary<int, string> InstrumentTypeList { get; set; }

        public ReferenceChartType ReferenceChartType { get; set; }

        #endregion
    }
}