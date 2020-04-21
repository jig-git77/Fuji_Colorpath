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
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddSpotTarget : BaseControl, IViewAddSpotTarget
    {

        #region Private Variables - Globalization
        private const string _recSelectCompany = "selectCompanyText";
        private const string _recPlzSelectCompany = "plzSelectCompany";
        private const string _recEditSpotTarget = "editSpotTarget";                
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
            if (Request.QueryString["SpotTargetId"] != null)
            {
                var SpotTargetId = Request.QueryString["SpotTargetId"];
                var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(SpotTargetId, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                int _SpotTargetId;
                if (Int32.TryParse(id, out _SpotTargetId))
                {
                    lblHeader.Text = GetResourceString(_recEditSpotTarget);//"Edit Spot Target";
                    LoadSpotTarget(_SpotTargetId);
                }
            }
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
                            //SpotID
                            if (short.TryParse(dataRow[0], out sampleId))
                            {
                                chartDataRow.SpotID = sampleId;
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //SpotName
                            if (!string.IsNullOrEmpty(dataRow[1]))
                            {
                                chartDataRow.SpotName = dataRow[1];
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnEmptyValueOnRow"), rows));
                            }

                            //Target L
                            if (decimal.TryParse(dataRow[2], out lab))
                            {
                                chartDataRow.Target_L = lab;
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //Target a
                            if (decimal.TryParse(dataRow[3], out lab))
                            {
                                chartDataRow.Target_a = lab;
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //Target b
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
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            SpotTarget = new SpotTarget();
            SpotTarget.Company = new Company();            
            SpotTarget.Company.CompanyId = cmbCompany.SelectedValue==""?-1:Convert.ToInt32(cmbCompany.SelectedValue);
            SpotTarget.SpotTargetName = txtSpotTargetName.Text;
            SpotTarget.Description = txtDescription.Text;
            SpotTarget.IsUsed = chkIsUsed.Checked;

            SpotTarget.spotTargetSetting = new SpotProjectChartSetting();
            SpotTarget.spotTargetSetting.ChartName = txtChartName.Text;
            SpotTarget.spotTargetSetting.NumOfRows = Convert.ToInt32(txtNumOfRows.Text);
            SpotTarget.spotTargetSetting.NumOfCols = Convert.ToInt32(txtNumOfCols.Text);
            SpotTarget.spotTargetSetting.ChartSizeX= Convert.ToDecimal(txtChartSizeX.Text);
            SpotTarget.spotTargetSetting.ChartSizeY = Convert.ToDecimal(txtChartSizeY.Text);
            SpotTarget.spotTargetSetting.PatchSizeX = Convert.ToDecimal(txtPatchSizeX.Text);
            SpotTarget.spotTargetSetting.PatchSizeY = Convert.ToDecimal(txtPatchSizeY.Text);
            SpotTarget.spotTargetSetting.FirstSample = "TopLeft";
            SpotTarget.spotTargetSetting.ReadDirection = "LeftToRight";
            SpotTarget.spotTargetSetting.ReadMode = "Strip";
            SpotTarget.spotTargetSetting.Pages = Convert.ToInt32(txtPages.Text);
            SpotTarget.spotTargetSetting.RowsPerPage = Convert.ToInt32(txtRowsPerPage.Text);
            SpotTarget.spotTargetSetting.ChartBorder= Convert.ToInt32(txtChartBorder.Text);
            SpotTarget.spotTargetSetting.Vgap = Convert.ToInt32(txtVgap.Text);
            SpotTarget.spotTargetSetting.Hgap= Convert.ToInt32(txtHgap.Text);
            SpotTarget.spotTargetSetting.GapLeval= Convert.ToInt32(txtGapLevel.Text);
            SpotTarget.spotTargetSetting.PreviousL = Convert.ToInt32(txtPreviousL.Text);
            SpotTarget.spotTargetSetting.ChartRes = Convert.ToInt32(txtChartRes.Text);
            

            if (hdnSpotTargetID.Value == "0")
            {
                if (ViewState[vsChartData] != null)
                {
                    SpotTarget.spotTargetLab = ((List<SpotTargetLAB>)ViewState[vsChartData]).
                                                             Select(x => new SpotTargetLAB()
                                                             {
                                                                 SpotID = x.SpotID,
                                                                 SpotName = x.SpotName,
                                                                 Target_L = x.Target_L,
                                                                 Target_a = x.Target_a,
                                                                 Target_b = x.Target_b
                                                                 //HTMLColor = x.HTMLColor
                                                             }).ToList();

                    _presenterSpotTarget.SaveSpotTarget();
                    if (IsErrorOccured) return;
                    Response.Redirect("SpotTargetManagement.aspx");
                }
            }
            else
            {
                SpotTarget.SpotTargetID = Convert.ToInt32(hdnSpotTargetID.Value);
                _presenterSpotTarget.UpdateSpotTarget();
                if (IsErrorOccured) return;
                Response.Redirect("SpotTargetManagement.aspx");
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
        private void LoadSpotTarget(int SpotTargetId)
        {
            SpotTarget = new SpotTarget { SpotTargetID = SpotTargetId };
            _presenterSpotTarget.GetSpotTarget();
            if (!IsErrorOccured)
            {
                Tr_UploadData.Visible = false;
                hdnSpotTargetID.Value = Convert.ToString(SpotTargetId);
                cmbCompany.SelectedValue = Convert.ToString(SpotTarget.Company.CompanyId);
                txtSpotTargetName.Text = SpotTarget.SpotTargetName.ToString();
                txtDescription.Text = SpotTarget.Description.ToString();

                txtChartName.Text = SpotTarget.spotTargetSetting.ChartName.ToString();
                txtNumOfRows.Text = SpotTarget.spotTargetSetting.NumOfRows.ToString();
                txtNumOfCols.Text = SpotTarget.spotTargetSetting.NumOfCols.ToString();
                txtChartSizeX.Text = SpotTarget.spotTargetSetting.ChartSizeX.ToString();
                txtChartSizeY.Text = SpotTarget.spotTargetSetting.ChartSizeY.ToString();
                txtPatchSizeX.Text = SpotTarget.spotTargetSetting.PatchSizeX.ToString();
                txtPatchSizeY.Text = SpotTarget.spotTargetSetting.PatchSizeY.ToString();
                txtPages.Text = SpotTarget.spotTargetSetting.Pages.ToString();
                txtRowsPerPage.Text = SpotTarget.spotTargetSetting.RowsPerPage.ToString();

                txtChartBorder.Text = SpotTarget.spotTargetSetting.ChartBorder.ToString();
                txtVgap.Text = SpotTarget.spotTargetSetting.Vgap.ToString();
                txtHgap.Text = SpotTarget.spotTargetSetting.Hgap.ToString();
                txtGapLevel.Text = SpotTarget.spotTargetSetting.GapLeval.ToString();
                txtPreviousL.Text = SpotTarget.spotTargetSetting.PreviousL.ToString();
                txtChartRes.Text = SpotTarget.spotTargetSetting.ChartRes.ToString();
                chkIsUsed.Checked = (SpotTarget.IsUsed == true ? true : false);
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