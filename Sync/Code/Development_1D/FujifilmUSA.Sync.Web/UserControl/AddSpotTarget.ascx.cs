using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSpot;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddSpotTarget : BaseControl, IViewAddSpotTarget
    {

        #region Private Variables - Globalization
        private const string _recSelectCompany = "selectCompanyText";
        private const string _recPlzSelectCompany = "plzSelectCompany";
        private const string vsChartData = "_vsChartData";
        #endregion

        #region Declaration
        private PresenterSpotTargetManagement _presenterSpotTarget;
        #endregion

        public AddSpotTarget()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterSpotTarget = new PresenterSpotTargetManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack) return;
            Bindcompany();
        }
        #endregion

        #region Control Events        
        protected void lbtnUploadChartData_Click(object sender, EventArgs e)
        {
            if (fucUploadChartData.UploadedFiles.Count > 0)
            {
                try
                {
                    var sr = new StreamReader(fucUploadChartData.UploadedFiles[0].InputStream);
                    var rows = 0;
                    var refChartImportData = new List<SpotTargetLAB>();
                    while (!sr.EndOfStream)
                    {
                        rows++;
                        if (rows == 1)
                        {
                            #region Check Column Names
                            var columns = sr.ReadLine().Split(',');
                            if (columns.Length != 5)
                            {
                                throw new Exception(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnCountNotMatch"));
                            }
                            columns.ToList().ForEach(x => x = x.Trim());
                            if (!columns[0].Equals("SpotID", StringComparison.CurrentCultureIgnoreCase) ||
                                !columns[1].Equals("SpotName", StringComparison.CurrentCultureIgnoreCase) ||
                                !columns[2].Equals("Target_L*", StringComparison.CurrentCultureIgnoreCase) ||
                                !columns[3].Equals("Target_a*", StringComparison.CurrentCultureIgnoreCase) ||
                                !columns[4].Equals("Target_b*", StringComparison.CurrentCultureIgnoreCase))
                            {
                                throw new Exception(FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnNamesNotMatch"));
                            }
                            #endregion
                        }
                        else
                        {
                            var dataRow = sr.ReadLine().Split(',');                            
                            dataRow.ToList().ForEach(x => x = x.Trim());
                            var chartDataRow = new SpotTargetLAB();
                            #region Converts row to ReferenceChart Object
                            short sampleId;
                            decimal lab;
                            //SampleID
                            if (short.TryParse(dataRow[0], out sampleId))
                            {
                                chartDataRow.SpotID = sampleId;
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //SampleName
                            if (!string.IsNullOrEmpty(dataRow[1]))
                            {
                                chartDataRow.SpotName = dataRow[1];
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnEmptyValueOnRow"), rows));
                            }

                            //CMYK_C
                            if (decimal.TryParse(dataRow[2], out lab))
                            {
                                chartDataRow.Target_L = lab;
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //CMYK_M
                            if (decimal.TryParse(dataRow[3], out lab))
                            {
                                chartDataRow.Target_a = lab;
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //CMYK_Y
                            if (decimal.TryParse(dataRow[4], out lab))
                            {
                                chartDataRow.Target_b = lab;
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
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
            if (ViewState[vsChartData] != null)
            {
                rgChartData.DataSource = ViewState[vsChartData];
            }
        }
        #endregion

        #region Methods
        private void Bindcompany()
        {
            cmbCompany.Items.Clear();
            Compnaies = new Dictionary<int, string>();
            _presenterSpotTarget.GetCompanies();
            if (!IsErrorOccured)
            {
                cmbCompany.DataTextField = "Value";
                cmbCompany.DataValueField = "Key";
                cmbCompany.DataSource = Compnaies;
                cmbCompany.DataBind();
                cmbCompany.Items.Insert(0, new ListItem(GetResourceString(_recSelectCompany), string.Empty));//--Select Company--
            }
        }
        #endregion

        #region Implement Interface
        public SpotTarget SpotTarget { get; set; }
        public int CompanyId { get; set; }
        public Dictionary<int, string> Compnaies { get; set; }
        #endregion
    }
}