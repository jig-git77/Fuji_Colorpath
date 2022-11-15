using FujifilmUSA.Sync.Entity.CPSpot;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.UI.DataVisualization.Charting;
using Telerik.Web.UI.Skins;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddCustomBcoTarget : BaseControl, IViewAddSpotTarget, IViewDevice
    {
        #region Private Variables - Globalization
        private const string _recSelectCompany = "selectCompanyText";
        private const string _recEditSpotTarget = "editSpotTarget";
        private const string vsChartData = "_vsChartData";
        #endregion

        #region Declaration
        private PresenterSpotTargetManagement _presenterSpotTarget;
        private PresenterDevice _presenterdevice;
        #endregion

        public AddCustomBcoTarget()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterSpotTarget = new PresenterSpotTargetManagement(this);
            _presenterdevice = new PresenterDevice(this);
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
            try
            {
                if (fucUploadChartData.UploadedFiles.Count > 0)
                {
                    StreamReader sr = new StreamReader(fucUploadChartData.UploadedFiles[0].InputStream);
                    List<SpotTargetLAB> refChartImportData = new List<SpotTargetLAB>();
                    string strAllLine = sr.ReadToEnd();
                    string[] lstLines = { };

                    if (!string.IsNullOrEmpty(strAllLine))
                    {
                        lstLines = strAllLine.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    }

                    for (int rows = 0; rows < (uint)lstLines.Count(); rows++)
                    {
                        if (rows == 0)
                        {
                            #region Check Column Names
                            string[] columns = lstLines[rows].Split(',');
                            if (columns.Length != 4)
                            {
                                throw new Exception(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnCountNotMatch"));
                            }
                            columns.ToList().ForEach(x => x = x.Trim());
                            if (!columns[0].Equals("Color_Name", StringComparison.CurrentCultureIgnoreCase) ||
                                !columns[1].Equals("Target_L*", StringComparison.CurrentCultureIgnoreCase) ||
                                !columns[2].Equals("Target_a*", StringComparison.CurrentCultureIgnoreCase) ||
                                !columns[3].Equals("Target_b*", StringComparison.CurrentCultureIgnoreCase))
                            {
                                throw new Exception(FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnNamesNotMatch"));
                            }
                            #endregion
                        }
                        else
                        {
                            string[] dataRow = lstLines[rows].Split(',');
                            dataRow.ToList().ForEach(x => x = x.Trim());
                            SpotTargetLAB chartDataRow = new SpotTargetLAB();
                            Regex regexItem = new Regex("^[a-zA-Z0-9_]*$");

                            #region Converts row to ReferenceChart Object
                            decimal lab;
                            //SpotID
                            chartDataRow.SpotID = (short)(rows);

                            //Color Name
                            if (string.IsNullOrEmpty(dataRow[0]))
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnEmptyValueOnRow"), rows));
                            }

                            else if (dataRow[0].Any(x => Char.IsWhiteSpace(x)))
                            {
                                throw new Exception(string.Format("Spaces are not allowed in color names. Please use _ (underscores) instead, Line No. {0}.", rows));
                            }
                            else if (regexItem.IsMatch(dataRow[0]) == false)
                            {
                                throw new Exception(string.Format("Color Name must be allow alpha numeric and _ (underscores), Line No. {0}.", rows));
                            }
                            else
                            {
                                chartDataRow.SpotName = dataRow[0];
                            }

                            //Target L
                            if (decimal.TryParse(dataRow[1], out lab))
                            {
                                lab = Math.Round(lab, 2);

                                if (lab > 100 || lab < 0)
                                { throw new Exception(string.Format("Value of Target L* Must Be Between 0 to 100 Line No. {0}.", rows)); }
                                else
                                { chartDataRow.Target_L = lab; }
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //Target a
                            if (decimal.TryParse(dataRow[2], out lab))
                            {
                                lab = Math.Round(lab, 2);

                                if (lab > 128 || lab < -128)
                                { throw new Exception(string.Format("Value of Target A* Must Be Between -128 to 128 Line No. {0}.", rows)); }
                                else
                                { chartDataRow.Target_a = lab; }
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //Target b
                            if (decimal.TryParse(dataRow[3], out lab))
                            {
                                lab = Math.Round(lab, 2);

                                if (lab > 128 || lab < -128)
                                { throw new Exception(string.Format("Value of Target B* Must Be Between -128 to 128 Line No. {0}.", rows)); }
                                else
                                { chartDataRow.Target_b = lab; }
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
                        if (refChartImportData.Count != refChartImportData.Select(O => O.SpotName).GroupBy(X => X).Count())
                        {
                            throw new Exception("Color ID must be unique.");
                        }

                        ViewState[vsChartData] = refChartImportData;
                        rgChartData.Rebind();
                    }
                    else
                    {
                        throw new Exception("Please select valid Color Library Csv File.");
                    }
                }
                else
                {
                    throw new Exception("Please select Color Library Csv File.");
                }
            }
            catch (Exception ex)
            {
                fujiStatusbar.DisplayMessage(ex.Message, false);
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
            try
            {
                if (hdnSpotTargetID.Value == "0" && rgChartData.Items.Count == 0)
                {
                    throw new Exception("Please select Color Library Csv File.");
                }
                else
                {
                    SpotTarget = new SpotTarget();
                    SpotTarget.Company = new Company();
                    SpotTarget.Company.CompanyId = cmbCompany.SelectedValue == "" ? -1 : Convert.ToInt32(cmbCompany.SelectedValue);
                    SpotTarget.SpotTargetName = txtSpotTargetName.Text;
                    SpotTarget.Description = txtDescription.Text;
                    SpotTarget.IsUsed = chkIsUsed.Checked;
                    SpotTarget.IsCustom = true;

                    SpotProjectChartSetting spotTargetSetting = new SpotProjectChartSetting();

                    spotTargetSetting.ChartName = SpotTarget.SpotTargetName;
                    spotTargetSetting.NumOfRows = Convert.ToInt32(txtNumOfRows.Text);
                    spotTargetSetting.NumOfCols = Convert.ToInt32(txtNumOfCols.Text);

                    spotTargetSetting.PatchSizeX = 27;
                    spotTargetSetting.PatchSizeY = 27;
                    spotTargetSetting.ChartBorder = 40;
                    spotTargetSetting.Vgap = 2;
                    spotTargetSetting.Hgap = 0;
                    spotTargetSetting.GapLeval = 75;
                    spotTargetSetting.PreviousL = 0;
                    spotTargetSetting.ChartRes = 30;
                    spotTargetSetting.ChartSizeX = (((spotTargetSetting.NumOfCols + 1) * spotTargetSetting.Vgap) + (spotTargetSetting.NumOfCols * spotTargetSetting.PatchSizeX)) / (spotTargetSetting.ChartRes / 10);
                    spotTargetSetting.ChartSizeY = (((spotTargetSetting.NumOfRows - 1) * spotTargetSetting.Hgap) + (spotTargetSetting.NumOfRows * spotTargetSetting.PatchSizeY)) / (spotTargetSetting.ChartRes / 10);

                    spotTargetSetting.FirstSample = "TopLeft";
                    spotTargetSetting.ReadDirection = "LeftToRight";
                    spotTargetSetting.ReadMode = "Strip";
                    spotTargetSetting.Pages = Convert.ToInt32(txtPages.Text);
                    spotTargetSetting.RowsPerPage = Convert.ToInt32(txtRowsPerPage.Text);
                  
                    SpotTarget.spotTargetSetting = spotTargetSetting;

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
                                                                     }).ToList();

                            _presenterSpotTarget.SaveSpotTarget();
                            if (IsErrorOccured) return;
                            Response.Redirect("CustomBcoManagement.aspx");
                        }
                    }
                    else
                    {
                        SpotTarget.SpotTargetID = Convert.ToInt32(hdnSpotTargetID.Value);
                        _presenterSpotTarget.UpdateSpotTarget();
                        if (IsErrorOccured) return;
                        Response.Redirect("CustomBcoManagement.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                fujiStatusbar.DisplayMessage(ex.Message, false);
            }
        }
        #endregion

        #region Methods
        private void Bindcompany()
        {
            cmbCompany.Items.Clear();
            _presenterdevice.CompanyList();
            if (!IsErrorOccured)
            {
                cmbCompany.DataTextField = "Value";
                cmbCompany.DataValueField = "Key";
                cmbCompany.DataSource = CompanyList;
                cmbCompany.DataBind();

                if (CompanyList.Count == 1)
                {
                    cmbCompany.SelectedIndex = 0;
                    trCompanyName.Visible = false;
                }
                else if (CompanyList.Count > 1)
                {
                    cmbCompany.Items.Insert(0, new ListItem(GetResourceString(_recSelectCompany), string.Empty));//--Select Company--
                }
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

                txtNumOfRows.Text = SpotTarget.spotTargetSetting.NumOfRows.ToString();
                txtNumOfCols.Text = SpotTarget.spotTargetSetting.NumOfCols.ToString();

                txtPages.Text = SpotTarget.spotTargetSetting.Pages.ToString();
                txtRowsPerPage.Text = SpotTarget.spotTargetSetting.RowsPerPage.ToString();

                chkIsUsed.Checked = (SpotTarget.IsUsed == true ? true : false);
            }
        }
        #endregion

        #region Implement Interface
        public SpotTarget SpotTarget { get; set; }
        public int CompanyId { get; set; }
        public Dictionary<int, string> Compnaies { get; set; }
        public int TaskeroUserId { get; set; }
        public int RoleId { get; set; }
        public int BranchId { get; set; }
        public MeasurementDevice MeasurementDevice { get; set; }
        public List<MeasurementDevice> MeasurementDevices { get; set; }
        public Dictionary<int, string> BranchList { get; set; }
        public Dictionary<int, string> CompanyList { get; set; }
        public Dictionary<int, string> LicenseList { get; set; }
        public Dictionary<int, string> InstrumentTypeList { get; set; }
        public List<Device> Devices { get; set; }
        public Device SelectedDevice { get; set; }
        public int DeviceId { get; set; }
        #endregion
    }
}