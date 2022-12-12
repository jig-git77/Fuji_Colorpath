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
using FujifilmUSA.Sync.Common;
using System.Text;
using System.Data;
using System.Reflection;
using System.Web.Services.Description;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using System.Runtime.Remoting.Lifetime;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddCustomBcoTarget : BaseControl, IViewAddSpotTarget, IViewDevice
    {
        #region Variables - Globalization
        private const string _recSelectCompany = "selectCompanyText";
        private const string _recEditSpotTarget = "editSpotTarget";
        private const string vsChartData = "_vsChartData";
        public string SpotTargetManagement_DeleteSpot_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("SpotTargetManagement_DeleteSpot_Confirmation");
            }
        }

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
                    LoadSpotTarget(_SpotTargetId);
                }
            }
            lblHeader.Text = GetResourceString(_recEditSpotTarget);//"Edit Spot Target";
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
                                throw new Exception(string.Format(GetResourceString("spacesarenotallowedincolornames"), rows));
                            }
                            else if (regexItem.IsMatch(dataRow[0]) == false)
                            {
                                throw new Exception(string.Format(GetResourceString("colorNamemustbeallowalphanumeric"), rows));
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
                                { throw new Exception(string.Format(GetResourceString("valueofTargetLMustBeBetween0to100LineNo"), rows)); }
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
                                { throw new Exception(string.Format(GetResourceString("valueofTargetAMustBeBetween128to128Line"), rows)); }
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
                                { throw new Exception(string.Format(GetResourceString("valueofTargetBMustBeBetween128to128Line"), rows)); }
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
                            throw new Exception(GetResourceString("colorIDmustbeunique"));
                        }

                        ViewState[vsChartData] = refChartImportData;
                        rgChartData.Rebind();
                    }
                    else
                    {
                        throw new Exception(GetResourceString("pleaseselectvalidColorLibraryCsvFile"));
                    }
                }
                else
                {
                    throw new Exception(GetResourceString("pleaseselectColorLibraryCsvFile"));
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
                    throw new Exception(GetResourceString("pleaseselectColorLibraryCsvFile"));
                }
                else
                {
                    SpotTarget = new SpotTarget();
                    SpotTarget.Company = new Company();
                    SpotTarget.Company.CompanyId = cmbCompany.SelectedValue == "" ? -1 : Convert.ToInt32(cmbCompany.SelectedValue);
                    SpotTarget.SpotTargetName = txtSpotTargetName.Text;
                    SpotTarget.Description = txtDescription.Text;
                    SpotTarget.IsUsed = true;
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
                        if (hdnSpotTargetID.Value == "0")
                        {
                            _presenterSpotTarget.SaveSpotTarget();
                            if (IsErrorOccured) return;
                            Response.Redirect("CustomBcoManagement.aspx");
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
            }
            catch (Exception ex)
            {
                fujiStatusbar.DisplayMessage(ex.Message, false);
            }
        }

        protected void rgSpotTarget_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    if (e.CommandArgument != null && ViewState[vsChartData] != null)
                    {
                        int SpotID = Convert.ToInt32(e.CommandArgument);
                        List<SpotTargetLAB> refChartImportData = (List<SpotTargetLAB>)ViewState[vsChartData];
                        refChartImportData.RemoveAt(SpotID - 1);

                        for (short i = 0; i < refChartImportData.Count; i++)
                        {
                            refChartImportData[i].SpotID = (short)(i + 1);
                        }

                        ViewState[vsChartData] = refChartImportData;
                    }
                    break;
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

                if (SpotTarget.spotTargetLab.Count > 0)
                {
                    ViewState[vsChartData] = SpotTarget.spotTargetLab;
                }
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

        protected void lbtnCSVSpotTargetLAB_Click(object sender, EventArgs e)
        {
            try
            {
               if (hdnSpotTargetID.Value == "0" && rgChartData.Items.Count == 0)
                {
                    throw new Exception(GetResourceString("pleasesubmitselectedColorLibraryCsvFile"));
                }
                else if (rgChartData.Items.Count == 0)
                {
                    throw new Exception(GetResourceString("Norecordfound"));
                }
                else
                {
                    var spotTargetLab = (IList<SpotTargetLAB>)ViewState[vsChartData];
                    string exportedFileName = "SpotTargetLab.csv";
                    exportedFileName = exportedFileName.Replace(":", "_").Replace("/", "_").Replace(" ", "_");
                    var csvFile = new ExportFile<SpotTargetLAB>(spotTargetLab, exportedFileName);
                    var filePath = csvFile.ExportCSV();
                    if (File.Exists(filePath))
                    {
                        var path = "UploadFiles/CsvFile/" + exportedFileName;
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert",
                            String.Format("DownloadCsv('{0}');", path), true);
                                       }

                }
            }
            catch (Exception ex)
            {
                fujiStatusbar.DisplayMessage(ex.Message, false);
            }
        }

        public void ExportTableDateAsCSV(DataTable dtSpotTargetLab)
        {
            if (dtSpotTargetLab != null && dtSpotTargetLab.Rows.Count > 0)
            {
                string exportedFileInfo = "SpotTargetLab_" + DateTime.Now.ToUniversalTime() + ".csv";
                exportedFileInfo = exportedFileInfo.Replace(":", "_").Replace("/", "_").Replace(" ", "_");
                StringBuilder sb = new StringBuilder();
                string[] columnName = dtSpotTargetLab.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
                var header = string.Join(",", columnName);
                sb.AppendLine(header);

                List<string> values = dtSpotTargetLab.AsEnumerable().Select(r => string.Join(",", r.ItemArray)).ToList();
                var rows = string.Join(Environment.NewLine, values);
                sb.AppendLine(rows);

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.Response.AddHeader("content-length", sb.Length.ToString());
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + exportedFileInfo);
                System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                System.Web.HttpContext.Current.Response.Write(sb.ToString());
                System.Web.HttpContext.Current.Response.End();


                #region Commented
                //System.Web.HttpContext.Current.Response.Buffer = true;

                //System.Web.HttpContext.Current.Response.Charset = "";

                //System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                //System.Web.HttpContext.Current.Response.ContentType = ReturnExtension(".csv");

                //System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=");

                //System.Web.HttpContext.Current.Response.BinaryWrite(bytes);

                //System.Web.HttpContext.Current.Response.Flush();

                //System.Web.HttpContext.Current.Response.End(); 
                #endregion

            }
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}