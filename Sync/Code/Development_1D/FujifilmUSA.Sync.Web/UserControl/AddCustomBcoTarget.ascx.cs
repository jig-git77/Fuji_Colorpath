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

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddCustomBcoTarget : BaseControl, IViewAddSpotTarget
    {
        #region Private Variables - Globalization
        private const string _recSelectCompany = "selectCompanyText";
        private const string _recEditSpotTarget = "editSpotTarget";
        private const string vsChartData = "_vsChartData";
        #endregion

        #region Declaration
        private PresenterSpotTargetManagement _presenterSpotTarget;
        #endregion

        public AddCustomBcoTarget()
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
                            Regex regexItem = new Regex("^[a-z0-9_].*?$");

                            #region Converts row to ReferenceChart Object
                            decimal lab;
                            //SpotID
                            chartDataRow.SpotID = (short)(rows);



                            //SpotName
                            if (!string.IsNullOrEmpty(dataRow[0]))
                            {
                                chartDataRow.SpotName = dataRow[0];
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnEmptyValueOnRow"), rows));
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
                        //if (refChartImportData.Count != refChartImportData.GroupBy(x => x.SpotName).Count())
                        //{
                        //    throw new Exception("Color ID must be unique");
                        //}

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
            SpotTarget.Company.CompanyId = cmbCompany.SelectedValue == "" ? -1 : Convert.ToInt32(cmbCompany.SelectedValue);
            SpotTarget.SpotTargetName = txtSpotTargetName.Text;
            SpotTarget.Description = txtDescription.Text;
            SpotTarget.IsUsed = chkIsUsed.Checked;

            SpotTarget.spotTargetSetting = new SpotProjectChartSetting();
            SpotTarget.spotTargetSetting.ChartName = txtChartName.Text;
            SpotTarget.spotTargetSetting.NumOfRows = Convert.ToInt32(txtNumOfRows.Text);
            SpotTarget.spotTargetSetting.NumOfCols = Convert.ToInt32(txtNumOfCols.Text);
            //SpotTarget.spotTargetSetting.ChartSizeX = Convert.ToDecimal(txtChartSizeX.Text);
            //SpotTarget.spotTargetSetting.ChartSizeY = Convert.ToDecimal(txtChartSizeY.Text);
            //SpotTarget.spotTargetSetting.PatchSizeX = Convert.ToDecimal(txtPatchSizeX.Text);
            //SpotTarget.spotTargetSetting.PatchSizeY = Convert.ToDecimal(txtPatchSizeY.Text);
            SpotTarget.spotTargetSetting.FirstSample = "TopLeft";
            SpotTarget.spotTargetSetting.ReadDirection = "LeftToRight";
            SpotTarget.spotTargetSetting.ReadMode = "Strip";
            SpotTarget.spotTargetSetting.Pages = Convert.ToInt32(txtPages.Text);
            SpotTarget.spotTargetSetting.RowsPerPage = Convert.ToInt32(txtRowsPerPage.Text);
            SpotTarget.spotTargetSetting.IsCustom = true;
            //SpotTarget.spotTargetSetting.ChartBorder = Convert.ToInt32(txtChartBorder.Text);
            //SpotTarget.spotTargetSetting.Vgap = Convert.ToInt32(txtVgap.Text);
            //SpotTarget.spotTargetSetting.Hgap = Convert.ToInt32(txtHgap.Text);
            //SpotTarget.spotTargetSetting.GapLeval = Convert.ToInt32(txtGapLevel.Text);
            //SpotTarget.spotTargetSetting.PreviousL = Convert.ToInt32(txtPreviousL.Text);
            //SpotTarget.spotTargetSetting.ChartRes = Convert.ToInt32(txtChartRes.Text);


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
        #endregion
    }
}