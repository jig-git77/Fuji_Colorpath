using Dundas.Charting.WebControl;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;


namespace TaskeroWeb
{
    public partial class ReportTargetMeasurement : PageBase
    {
        private const int ConstDefaultPageSizeForDataList = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //=== Clear Message ===//
                LblError.Text = "";

                //=== Check User Login ===//
                Sessions.CheckUserLogin();

                //=== Default Settings ===//
                if (!IsPostBack)
                {
                    // === when button is clicked to create report at that time chk wheter job is selected or not === //
                    btnCreateReport.Attributes.Add("onclick", "javascript:return chkSelection(" + hidSelectedItem.ClientID + ")");

                    // === Set global values here === //
                    ViewState["DataListPageIndex"] = 0;
                    ViewState["TotalRecords"] = 0;

                    // === Fill job list === //
                    FillJobList();
                }
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        private void FillJobList()
        {
            try
            {
                ReportTargetMeasurementData objTargetMeasurement = new ReportTargetMeasurementData();

                //====  Set Parameters for Command ====//
                objTargetMeasurement.PromoID = txtPromoIDNumber.Text.Trim();
                objTargetMeasurement.ItemNumber = txtItemNumber.Text.Trim();
                objTargetMeasurement.Brand = txtBrand.Text.Trim();
                objTargetMeasurement.OrderCode = txtOrderCode.Text.Trim();

                //====  Get DataTable ====//
                DataTable dt = objTargetMeasurement.GetSearchJobs();

                // === Set Page Index, StartPosition & EndPosition === //
                int CurrentPageIndex = (int)(ViewState["DataListPageIndex"]);

                int intStartPosition = (CurrentPageIndex * ConstDefaultPageSizeForDataList);
                int intEndPosition = (CurrentPageIndex * ConstDefaultPageSizeForDataList) + ConstDefaultPageSizeForDataList;
                if (intEndPosition > dt.Rows.Count)
                    intEndPosition = dt.Rows.Count;

                // === Set Total records count for paging and hide show next previous buttons === //
                ViewState["TotalRecords"] = Convert.ToString(dt.Rows.Count) == "" ? 0 : dt.Rows.Count;

                DataTable DT_Temp = dt.Clone();
                if (dt.Rows.Count > 0)
                {
                    // === Fill dataset according to navigated page === //
                    for (int i = intStartPosition; i < intEndPosition; i++)
                    {
                        DT_Temp.ImportRow(dt.Rows[i]);
                    }

                    dlJobs.DataSource = DT_Temp;
                    dlJobs.DataBind();
                    dlJobs.Visible = true;

                    lblNoSearchFound.Visible = false;
                    // === Display displayed number of records out of total records === //
                    lblPage.Text = (intStartPosition + 1).ToString() + " To " + intEndPosition.ToString() + " / " + ViewState["TotalRecords"].ToString();
                }
                else
                {
                    dlJobs.DataSource = null;
                    dlJobs.DataBind();
                    lblNoSearchFound.Visible = true;
                    dlJobs.Visible = false;
                    /// === If no record found then display appropriate message and Paging label must not display anything === //
                    lblNoSearchFound.Text = "No Result Found.";
                    lblPage.Text = "";
                }
                // === Manage PageNavigation Buttons visibility === //
                SetNavigationButton(intStartPosition, intEndPosition, dt.Rows.Count, BTN_ListPrev, BTN_ListNext);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void SetNavigationButton(int intStartPosition, int intEndPosition, int intTotal, Button Pre, Button Next)
        {
            try
            {
                // === Set Next Previouse Buttons Visible trye/false based on data available === //
                Pre.Visible = ((intTotal == 0) ? false : (intStartPosition == 0 ? false : true));
                Next.Visible = (intEndPosition >= intTotal ? false : true);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        protected void dlJobs_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                // ==== Set Job records client click event to make it selected === //
                System.Web.UI.HtmlControls.HtmlTableRow row = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trTable");
                row.Attributes.Add("onclick", "javascript:SelectedItem('" + e.Item.ItemIndex.ToString() + "')");

                // === Set Colored Stared before each record === //
                System.Web.UI.WebControls.Label lblStatus = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblStatus");
                System.Web.UI.WebControls.Image imgStar = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgStar");
                int intStatus = Convert.ToInt16(lblStatus.Text);
                if (intStatus == (int)Enums.enumStatus.Failed)
                {
                    //========  Set Status Image Path And Title  ========//
                    imgStar.ImageUrl = "Images/RedStar.gif"; //========  Failed Then Red Star  ========//
                    imgStar.ToolTip = Enums.enumStatus.Failed.ToString();
                }
                else if (intStatus == (int)Enums.enumStatus.Caution)
                {
                    //========  Set Status Image Path And Title  ========//
                    imgStar.ImageUrl = "Images/YellowStar.gif";//========  Caution Then Yellow Star  ========//
                    imgStar.ToolTip = Enums.enumStatus.Caution.ToString();
                }
                else if (intStatus == (int)Enums.enumStatus.Passed)
                {
                    //========  Set Status Image Path And Title  ========//
                    imgStar.ImageUrl = "Images/GreenStar.gif";//========  Passed Then Green Star  ========//
                    imgStar.ToolTip = Enums.enumStatus.Passed.ToString();
                }

                // === Check if hiddenField got value then make that record selected === //
                if (hidSelectedItem.Value.Trim() != "" && e.Item.ItemIndex == Convert.ToInt16(hidSelectedItem.Value))
                {
                    e.Item.BackColor = System.Drawing.ColorTranslator.FromHtml("#00ccff");
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        protected void BTN_SearchJob_Click(object sender, EventArgs e)
        {
            try
            {
                // === When Search is apply, assign all global variables to default value === //
                ViewState["DataListPageIndex"] = 0;
                ViewState["TotalRecords"] = 0;
                hidSelectedItem.Value = "";
                FillJobList();
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        protected void BTN_ListPrev_Click(object sender, EventArgs e)
        {
            try
            {
                int intPrev = (int)(ViewState["DataListPageIndex"] == null ? 0 : ViewState["DataListPageIndex"]);
                intPrev = intPrev - 1;
                int intTotalPage = (((int)ViewState["TotalRecords"]) / ConstDefaultPageSizeForDataList);
                if ((((int)ViewState["TotalRecords"]) % ConstDefaultPageSizeForDataList) > 0)
                    intTotalPage = intTotalPage + 1;
                if (intPrev <= 0)
                    intPrev = 0;

                ViewState["DataListPageIndex"] = intPrev;

                // ==== If page is changed then job selection must be removed === //
                hidSelectedItem.Value = "";
                FillJobList();
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        protected void BTN_ListNext_Click(object sender, EventArgs e)
        {
            try
            {
                int intNext = (int)(ViewState["DataListPageIndex"] == null ? 0 : ViewState["DataListPageIndex"]);
                intNext = intNext + 1;

                int intTotalPage = (((int)ViewState["TotalRecords"]) / ConstDefaultPageSizeForDataList);

                if ((((int)ViewState["TotalRecords"]) % ConstDefaultPageSizeForDataList) > 0)
                    intTotalPage = intTotalPage + 1;
                if (intNext >= intTotalPage)
                    intNext = intTotalPage - 1;

                ViewState["DataListPageIndex"] = intNext;

                // ==== If page is changed then job selection must be removed === //
                hidSelectedItem.Value = "";
                FillJobList();
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }

        protected void btnSelectJob_Click(object sender, EventArgs e)
        {
            try
            {
                // === To make individual record selected === //
                if (hidSelectedItem.Value.Trim() != "")
                {
                    dlJobs.Items[Convert.ToInt32(hidSelectedItem.Value)].BackColor = System.Drawing.ColorTranslator.FromHtml("#00ccff");
                }
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }

        protected void btnCreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateReport();
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }

        #region " === Fill/Save Charts Image & Create Report === "
        private void CreateReport()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                // === Find selected Index of List of Job === //
                int intIndex = hidSelectedItem.Value.Trim() != "" ? (Convert.ToInt32(hidSelectedItem.Value)) : -1;
                if (intIndex != -1)
                {
                    // ==== Find Guid of that job from the JobList and get Measurement Data according to that GUID === //
                    string strGuid = ((System.Web.UI.WebControls.Label)dlJobs.Items[intIndex].FindControl("lblGuid")).Text.Trim();
                    ReportTargetMeasurementData obj = new ReportTargetMeasurementData();
                    obj.CPMeasurementGUID = strGuid;
                    ds = obj.GetMeasurementData();

                    #region " === Fill First Page Of the report === "
                    if (ds != null)
                    {
                        int intTable;
                        #region " === intTable = 0 JobDetail === "
                        //intTable = 0;
                        #endregion

                        #region " === intTable = 1 Target Vs Actual AB Plot === "
                        intTable = 1;
                        if (ds.Tables[intTable] != null && ds.Tables[intTable].Rows.Count > 0)
                            Bind_TargetVersusActual_CHART(ds.Tables[intTable]);

                        #endregion

                        #region " === intTable = 2 Summary Delta (Upper Part with 7 rows data) === "
                        intTable = 2;
                        if (ds.Tables[intTable] != null && ds.Tables[intTable].Rows.Count > 0)
                            FillSummaryDelta(ds.Tables[intTable]);
                        #endregion

                        #region " === intTable = 3 Delta E 2000 Values (Lower Part with 10 rows data) === "
                        intTable = 3;
                        if (ds.Tables[intTable] != null && ds.Tables[intTable].Rows.Count > 0)
                            FillDeltaE2000(ds.Tables[intTable]);
                        #endregion
                    }
                    #endregion

                    #region " === Fill Second Page(Measurement List Report) Of the report === "
                    dt = obj.GetPatchData();
                    #endregion

                    // === To make record selected in grid === //
                    btnSelectJob_Click(null, null);

                    ds.Tables.Add(dt);
                    // ==== Create PDF for first Page of the reposrt === //
                    CreateReportPDF(ds);
                }
                else
                {
                    // === If No record is selected then no data display and give message === //
                    LblError.Text = "Select record.";
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void CreateReportPDF(DataSet Ds)
        {
            try
            {
                // === Set all parameters to generate pdf file === //
                ReportPDF objReportPDF = new ReportPDF();
                objReportPDF.ReportType = (int)Enums.enumReportType.ReportTargetMeasurement;
                objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
                objReportPDF.PDF_NAME = Sessions.UserID + "_ReportTargetMeasurement.PDF";
                objReportPDF.DS_Common = Ds;
                objReportPDF.MakePDF();

                // === After generat and save PDF file, Download it at a time === //
                CommonFunctions.DownlaodFile(objReportPDF.PDFPath, objReportPDF.PDF_NAME, objReportPDF.PDF_NAME);
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }

        private void Bind_TargetVersusActual_CHART(DataTable dtGamut)
        {
            try
            {
                #region Plotting the Chart
                dtGamut.DefaultView.RowFilter = "(CMYK_C = 0 AND CMYK_M = 0  AND CMYK_Y = 0  AND CMYK_K = 0)";
                DataView DV = dtGamut.DefaultView;
                if (DV.Count > 0)
                {
                    int i = 0;
                    CHART_TargetVersusActualAB.Series[2].Points.AddXY(Convert.ToDouble(DV[0]["LAB_A1"]), Convert.ToDouble(DV[i]["LAB_B1"]));
                    CHART_TargetVersusActualAB.Series[2].Type = SeriesChartType.FastPoint;
                    CHART_TargetVersusActualAB.Series[2].Points[0].ToolTip = "Color : " + DV[i]["ColorType"].ToString() + ", Target(a) : " + DV[i]["LAB_A1"].ToString() + ", Target(b) : " + DV[i]["LAB_B1"].ToString();
                    CHART_TargetVersusActualAB.Series[2].Points[0].Href = "#";


                    CHART_TargetVersusActualAB.Series[3].Points.AddXY(Convert.ToDouble(DV[i]["LAB_A2"]), Convert.ToDouble(DV[i]["LAB_B2"]));
                    CHART_TargetVersusActualAB.Series[3].Type = SeriesChartType.FastPoint;
                    CHART_TargetVersusActualAB.Series[3].Points[0].ToolTip = "Color : " + DV[i]["ColorType"].ToString() + ", Actual(a) : " + DV[i]["LAB_A2"].ToString() + ", Actual(b) : " + DV[i]["LAB_B2"].ToString();
                    CHART_TargetVersusActualAB.Series[3].Points[0].Href = "#";
                }
                #endregion

                #region Other Points
                dtGamut.DefaultView.RowFilter = "(ColorType <> 'WHITE') AND (LAB_A1 <> 0 AND LAB_B1 <> 0 AND LAB_A2 <> 0 AND LAB_B2 <> 0)";
                DV = dtGamut.DefaultView;
                for (int i = 0; i < DV.Count; i = i + 1)
                {
                    CHART_TargetVersusActualAB.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["LAB_A1"]), Convert.ToDouble(DV[i]["LAB_B1"]));
                    CHART_TargetVersusActualAB.Series[0].Type = SeriesChartType.Line;
                    CHART_TargetVersusActualAB.Series[0].Points[i].ToolTip = "Color : " + DV[i]["ColorType"].ToString() + ", Target(a) : " + Convert.ToDouble(DV[i]["LAB_A1"]).ToString("N2") + ", Target(b) : " + Convert.ToDouble(DV[i]["LAB_B1"]).ToString("N2");
                    CHART_TargetVersusActualAB.Series[0].Points[i].Href = "#";
                    CHART_TargetVersusActualAB.Series[0].Points[i].BorderWidth = 2;

                    CHART_TargetVersusActualAB.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["LAB_A2"]), Convert.ToDouble(DV[i]["LAB_B2"]));
                    CHART_TargetVersusActualAB.Series[1].Type = SeriesChartType.Line;
                    CHART_TargetVersusActualAB.Series[1].Points[i].ToolTip = "Color : " + DV[i]["ColorType"].ToString() + ", Actual(a) : " + Convert.ToDouble(DV[i]["LAB_A2"]).ToString("N2") + ", Actual(b) : " + Convert.ToDouble(DV[i]["LAB_B2"]).ToString("N2");
                    CHART_TargetVersusActualAB.Series[1].Points[i].Href = "#";
                    CHART_TargetVersusActualAB.Series[1].Points[i].BorderWidth = 2;
                }
                if (DV.Count > 1)
                {
                    int i = 0;
                    CHART_TargetVersusActualAB.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["LAB_A1"]), Convert.ToDouble(DV[i]["LAB_B1"]));
                    CHART_TargetVersusActualAB.Series[0].Type = SeriesChartType.Line;
                    CHART_TargetVersusActualAB.Series[0].Points[DV.Count].ToolTip = "Color : " + DV[i]["ColorType"].ToString() + ", Target(a) : " + Convert.ToDouble(DV[i]["LAB_A1"]).ToString("N2") + ", Target(b) : " + Convert.ToDouble(DV[i]["LAB_B1"]).ToString("N2");
                    CHART_TargetVersusActualAB.Series[0].Points[DV.Count].Href = "#";
                    CHART_TargetVersusActualAB.Series[0].Points[DV.Count].BorderWidth = 2;

                    CHART_TargetVersusActualAB.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["LAB_A2"]), Convert.ToDouble(DV[i]["LAB_B2"]));
                    CHART_TargetVersusActualAB.Series[1].Type = SeriesChartType.Line;
                    CHART_TargetVersusActualAB.Series[1].Points[DV.Count].ToolTip = "Color : " + DV[i]["ColorType"].ToString() + ", Actual(a) : " + Convert.ToDouble(DV[i]["LAB_A2"]).ToString("N2") + ", Actual(b) : " + Convert.ToDouble(DV[i]["LAB_B2"]).ToString("N2");
                    CHART_TargetVersusActualAB.Series[1].Points[DV.Count].Href = "#";
                    CHART_TargetVersusActualAB.Series[1].Points[DV.Count].BorderWidth = 2;
                }

                string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(CHART_TargetVersusActualAB.ImageUrl) + "." + CHART_TargetVersusActualAB.ImageType;
                CHART_TargetVersusActualAB.Save(Server.MapPath(FileName));
                CHART_TargetVersusActualAB.ImageType = ChartImageType.Flash;

                #endregion
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }

        }

        private void FillSummaryDelta(DataTable dt)
        {
            try
            {
                // === Parameters   Chart, DataTable, StartIndex, EndIndex === //
                BindChartSummaryDelta(ChartSummaryDelta, dt, 0, 7);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void BindChartSummaryDelta(Chart CHRT, DataTable DT, int _StartIndex, int _EndIndex)
        {
            try
            {
                //lblChartLegend.Text = "<img src='Images/Chart-Red-Image.png' alt='red img' id='imgError' />&nbsp;Error&nbsp;&nbsp;<img src='Images/Chart-Yellow-Image.png' alt='Yellow img' id='img2' />&nbsp;Caution&nbsp;&nbsp;";
                CHRT.BorderColor = Color.White;
                CHRT.BorderWidth = 0;
                CHRT.BackColor = Color.White;

                //========== Clear Points and Show Chart ==========//
                CHRT.Visible = true;
                CHRT.Series[0].Points.Clear();// For Threshold value display
                CHRT.Series[1].Points.Clear();// For Actual value display
                CHRT.Series[2].Points.Clear();// For Marker Image of Threshold value display


                #region //========== Declarations ==========//
                int PointIndex = 0;

                // === Set Series0 default settings === //
                Series objSeries0 = (Series)CHRT.Series[0];
                objSeries0.XValueType = ChartValueTypes.Double;
                objSeries0.YValueType = ChartValueTypes.String;
                objSeries0.ShowInLegend = false;
                objSeries0.Type = SeriesChartType.Bar;

                // === Set Series1 default settings === //
                Series objSeries1 = (Series)CHRT.Series[1];
                objSeries1.XValueType = ChartValueTypes.Double;
                objSeries1.YValueType = ChartValueTypes.String;
                objSeries1.ShowInLegend = false;
                objSeries1.Type = SeriesChartType.Bar;

                Series objSeries2 = (Series)CHRT.Series[2];
                objSeries2.XValueType = ChartValueTypes.Int;
                objSeries2.YValueType = ChartValueTypes.String;
                objSeries2.ShowInLegend = false;
                objSeries2.Type = SeriesChartType.Bar;
                #endregion

                #region //========== Set Chart Area Properties ==========//
                CHRT.ChartAreas["ChartArea1"].Area3DStyle.RightAngleAxes = true;
                CHRT.ChartAreas["ChartArea1"].Area3DStyle.Clustered = true;
                CHRT.ChartAreas["ChartArea1"].Area3DStyle.XAngle = 0;
                CHRT.ChartAreas["ChartArea1"].Area3DStyle.YAngle = 0;
                CHRT.ChartAreas["ChartArea1"].BackColor = Color.White;

                // === To Display Chart area properly in report, Set its value here === //
                CHRT.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;
                CHRT.ChartAreas["ChartArea1"].Position.Auto = true;
                CHRT.ChartAreas["ChartArea1"].Position.X = float.Parse("0");
                CHRT.ChartAreas["ChartArea1"].Position.Y = float.Parse("5");
                CHRT.ChartAreas["ChartArea1"].Position.Width = float.Parse("99");
                CHRT.ChartAreas["ChartArea1"].Position.Height = float.Parse("91");

                CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
                CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.X = float.Parse("0");
                CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Y = float.Parse("3");
                CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Width = float.Parse("98");
                CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Height = float.Parse("91");

                CHRT.ChartAreas["ChartArea1"].ShadowOffset = 0;
                CHRT.BorderSkin.SkinStyle = BorderSkinStyle.None;
                #endregion

                #region " === Fill Series === "

                Double Y1; // Value for series0(Threshold)
                Double Y2;// Value for Series1(Actual)
                DataRow Row = DT.Rows[0];

                #region " Row-1 === AverageDE === "
                Y1 = (Convert.ToString(Row["AverageDE_TOL"]) == "" ? 0 : Convert.ToDouble(Row["AverageDE_TOL"]));
                Y2 = (Convert.ToString(Row["AverageDE_ACT"]) == "" ? 0 : Convert.ToDouble(Row["AverageDE_ACT"]));

                // === If Actual value is zero then Threshold value must not show, and same for all points === //
                Y1 = (Y2 == 0) ? Y2 : Y1;

                objSeries0.Points.AddXY((Double)0, Y1);
                objSeries0.Points[PointIndex]["PointWidth"] = "0.8";
                objSeries1.Points.AddXY((Double)0, Y2);
                objSeries1.Points[PointIndex]["PointWidth"] = "0.4";
                objSeries2.Points.AddXY((Double)0, Y1);
                if (Y1 == 0)// === If no threshold value then marker should not show === //
                    objSeries0.Points[PointIndex].MarkerImage = "";

                // === If Actual value is more then threshold value then, to show marker of the threshold , make Actual series transparent === //
                //if (Y2 > Y1)
                //  objSeries1.Points[PointIndex].Color = Color.Transparent;
                #endregion

                #region " Row-2 === MaximumDE === "
                PointIndex++;
                Y1 = (Convert.ToString(Row["MaximumDE_TOL"]) == "" ? 0 : Convert.ToDouble(Row["MaximumDE_TOL"]));
                Y2 = (Convert.ToString(Row["MaximumDE_ACT"]) == "" ? 0 : Convert.ToDouble(Row["MaximumDE_ACT"]));
                Y1 = (Y2 == 0) ? Y2 : Y1;
                objSeries0.Points.AddXY((Double)0, Y1);
                objSeries0.Points[PointIndex]["PointWidth"] = "0.8";
                objSeries1.Points.AddXY((Double)0, Y2);
                objSeries1.Points[PointIndex]["PointWidth"] = "0.4";
                objSeries2.Points.AddXY((Double)0, Y1);
                if (Y1 == 0)
                    objSeries0.Points[PointIndex].MarkerImage = "";
                //if (Y2 > Y1)
                //  objSeries1.Points[PointIndex].Color = Color.Transparent;
                #endregion

                #region " Row-3 === PrimaryDE === "
                PointIndex++;
                Y1 = (Convert.ToString(Row["PrimaryDE_TOL"]) == "" ? 0 : Convert.ToDouble(Row["PrimaryDE_TOL"]));
                Y2 = (Convert.ToString(Row["PrimaryDE_ACT"]) == "" ? 0 : Convert.ToDouble(Row["PrimaryDE_ACT"]));
                Y1 = (Y2 == 0) ? Y2 : Y1;

                objSeries0.Points.AddXY((Double)0, Y1);
                objSeries0.Points[PointIndex]["PointWidth"] = "0.8";
                objSeries1.Points.AddXY((Double)0, Y2);
                objSeries1.Points[PointIndex]["PointWidth"] = "0.4";
                objSeries2.Points.AddXY((Double)0, Y1);
                if (Y1 == 0)
                    objSeries0.Points[PointIndex].MarkerImage = "";
                //if (Y2 > Y1)
                //  objSeries1.Points[PointIndex].Color = Color.Transparent;
                #endregion

                #region " Row-4 === PrimaryDH === "
                PointIndex++;
                Y1 = (Convert.ToString(Row["PrimaryDH_TOL"]) == "" ? 0 : Convert.ToDouble(Row["PrimaryDH_TOL"]));
                Y2 = (Convert.ToString(Row["PrimaryDH_ACT"]) == "" ? 0 : Convert.ToDouble(Row["PrimaryDH_ACT"]));
                Y1 = (Y2 == 0) ? Y2 : Y1;
                objSeries0.Points.AddXY((Double)0, Y1);
                objSeries0.Points[PointIndex]["PointWidth"] = "0.8";
                objSeries1.Points.AddXY((Double)0, Y2);
                objSeries1.Points[PointIndex]["PointWidth"] = "0.4";
                objSeries2.Points.AddXY((Double)0, Y1);
                if (Y1 == 0)
                    objSeries0.Points[PointIndex].MarkerImage = "";
                //if (Y2 > Y1)
                //  objSeries1.Points[PointIndex].Color = Color.Transparent;
                #endregion

                #region " Row-5 === GrayAverageDH === "
                PointIndex++;
                Y1 = (Convert.ToString(Row["GrayAverageDH_TOL"]) == "" ? 0 : Convert.ToDouble(Row["GrayAverageDH_TOL"]));
                Y2 = (Convert.ToString(Row["GrayAverageDH_ACT"]) == "" ? 0 : Convert.ToDouble(Row["GrayAverageDH_ACT"]));
                Y1 = (Y2 == 0) ? Y2 : Y1;
                objSeries0.Points.AddXY((Double)0, Y1);
                objSeries0.Points[PointIndex]["PointWidth"] = "0.8";
                objSeries1.Points.AddXY((Double)0, Y2);
                objSeries1.Points[PointIndex]["PointWidth"] = "0.4";
                objSeries2.Points.AddXY((Double)0, Y1);
                if (Y1 == 0)
                    objSeries0.Points[PointIndex].MarkerImage = "";
                //if (Y2 > Y1)
                //  objSeries1.Points[PointIndex].Color = Color.Transparent;
                #endregion

                #region " Row-6 & 7 === Quartile & Delta E Outer === "
                // === If all records are availabel to display chart then go ahead, for now we have up to 5 records and so cant display 6th and 7th record === //
                if (DT.Columns.Count > 10)
                {
                    #region " Row-6 === 90% Quartile === "
                    PointIndex++;
                    Y1 = (Convert.ToString(Row["Quartile_TOL"]) == "" ? 0 : Convert.ToDouble(Row["Quartile_TOL"]));
                    Y2 = (Convert.ToString(Row["Quartile_ACT"]) == "" ? 0 : Convert.ToDouble(Row["Quartile_ACT"]));
                    Y1 = (Y2 == 0) ? Y2 : Y1;
                    objSeries0.Points.AddXY((Double)0, Y1);
                    objSeries0.Points[PointIndex]["PointWidth"] = "0.8";
                    objSeries1.Points.AddXY((Double)0, Y2);
                    objSeries1.Points[PointIndex]["PointWidth"] = "0.4";
                    objSeries2.Points.AddXY((Double)0, Y1);
                    if (Y1 == 0)
                        objSeries0.Points[PointIndex].MarkerImage = "";
                    //if (Y2 > Y1)
                    //  objSeries1.Points[PointIndex].Color = Color.Transparent;
                    #endregion

                    #region " Row-7 === Delta E Outer === "
                    PointIndex++;
                    Y1 = (Convert.ToString(Row["Outer_TOL"]) == "" ? 0 : Convert.ToDouble(Row["Outer_TOL"]));
                    Y2 = (Convert.ToString(Row["Outer_ACT"]) == "" ? 0 : Convert.ToDouble(Row["Outer_ACT"]));
                    Y1 = (Y2 == 0) ? Y2 : Y1;
                    objSeries0.Points.AddXY((Double)0, Y1);
                    objSeries0.Points[PointIndex]["PointWidth"] = "0.8";
                    objSeries1.Points.AddXY((Double)0, Y2);
                    objSeries1.Points[PointIndex]["PointWidth"] = "0.4";
                    objSeries2.Points.AddXY((Double)0, Y1);
                    if (Y1 == 0)
                        objSeries0.Points[PointIndex].MarkerImage = "";
                    //if (Y2 > Y1)
                    //  objSeries1.Points[PointIndex].Color = Color.Transparent;
                    #endregion
                }
                else
                {
                    #region " Row-6 === 90% Quartile === "
                    PointIndex++;
                    Y1 = 0;
                    Y2 = 0;
                    Y1 = (Y2 == 0) ? Y2 : Y1;
                    objSeries0.Points.AddXY((Double)0, Y1);
                    objSeries0.Points[PointIndex]["PointWidth"] = "0.8";
                    if (Y1 == 0)
                        objSeries0.Points[PointIndex].MarkerImage = "";
                    objSeries1.Points.AddXY((Double)0, Y2);
                    objSeries1.Points[PointIndex]["PointWidth"] = "0.4";
                    objSeries2.Points.AddXY((Double)0, Y1);
                    #endregion

                    #region " Row-7 === Delta E Outer === "
                    PointIndex++;
                    Y1 = (Y2 == 0) ? Y2 : Y1;
                    objSeries0.Points.AddXY((Double)0, Y1);
                    objSeries0.Points[PointIndex]["PointWidth"] = "0.8";
                    if (Y1 == 0)
                        objSeries0.Points[PointIndex].MarkerImage = "";
                    objSeries1.Points.AddXY((Double)0, Y2);
                    objSeries1.Points[PointIndex]["PointWidth"] = "0.4";
                    objSeries2.Points.AddXY((Double)0, Y1);
                    #endregion
                }
                #endregion

                string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(CHRT.ImageUrl) + "." + CHRT.ImageType;
                CHRT.Save(Server.MapPath(FileName));

                //========== Increase Point Index ==========//
                PointIndex = PointIndex + 1;
                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillDeltaE2000(DataTable dt)
        {
            try
            {
                // === Parameters Chart, DataTable, StartIndex, EndIndex === //
                BindBarChart(ChartDeltaE2000, dt, 0, 10);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void BindBarChart(Chart CHRT, DataTable DT, int _StartIndex, int _EndIndex)
        {
            try
            {
                CHRT.BorderColor = Color.White;
                CHRT.BorderWidth = 0;
                CHRT.BackColor = Color.White;

                //========== Clear Points and Show Chart ==========//
                CHRT.Visible = true;
                CHRT.Series[0].Points.Clear();
                CHRT.Series[1].Points.Clear();
                CHRT.Series[2].Points.Clear();

                #region //========== Declarations ==========//
                int PointIndex = 0;
                // === Series0 to display Thresold value === //
                Series objSeries0 = (Series)CHRT.Series[0];
                objSeries0.XValueType = ChartValueTypes.Int;
                objSeries0.YValueType = ChartValueTypes.String;
                objSeries0.ShowInLegend = false;
                objSeries0.Type = SeriesChartType.Bar;
                //objSeries0.Color = Color.Gray;
                objSeries0.BackGradientType = GradientType.DiagonalRight;

                // === Series1 to display Actual value === //
                Series objSeries1 = (Series)CHRT.Series[1];
                objSeries1.XValueType = ChartValueTypes.Int;
                objSeries1.YValueType = ChartValueTypes.String;
                objSeries1.ShowInLegend = false;
                objSeries1.Type = SeriesChartType.Bar;

                // === Series2 to display marker on Thresold value === //
                Series objSeries2 = (Series)CHRT.Series[2];
                objSeries2.XValueType = ChartValueTypes.Int;
                objSeries2.YValueType = ChartValueTypes.String;
                objSeries2.ShowInLegend = false;
                objSeries2.Type = SeriesChartType.Bar;

                // === Make Chart area look like 2D even it is 3D === //
                CHRT.ChartAreas["ChartArea1"].Area3DStyle.RightAngleAxes = true;
                CHRT.ChartAreas["ChartArea1"].Area3DStyle.Clustered = true;
                CHRT.ChartAreas["ChartArea1"].Area3DStyle.XAngle = 0;
                CHRT.ChartAreas["ChartArea1"].Area3DStyle.YAngle = 0;
                CHRT.ChartAreas["ChartArea1"].BackColor = Color.White;
                #endregion

                #region //========== ChartArea Settings ==========//
                CHRT.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
                CHRT.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;

                CHRT.ChartAreas["ChartArea1"].Position.Auto = true;
                CHRT.ChartAreas["ChartArea1"].Position.X = float.Parse("0");
                CHRT.ChartAreas["ChartArea1"].Position.Y = float.Parse("5");
                CHRT.ChartAreas["ChartArea1"].Position.Width = float.Parse("99");
                CHRT.ChartAreas["ChartArea1"].Position.Height = float.Parse("91");

                CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
                CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.X = float.Parse("0");
                CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Y = float.Parse("2");
                CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Width = float.Parse("98");
                CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Height = float.Parse("91");

                CHRT.ChartAreas["ChartArea1"].ShadowOffset = 0;
                CHRT.BorderSkin.SkinStyle = BorderSkinStyle.None;
                CHRT.ChartAreas["ChartArea1"].BorderWidth = 0;
                CHRT.BorderSkin.FrameBorderWidth = 0;



                #endregion

                #region " === Fill Series === "
                //========== Loop and add values ==========//

                for (int i = _StartIndex; i < _EndIndex; i = i + 1)
                {

                    Double Y1 = (Convert.ToString(DT.Rows[i]["TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[i]["TOL"].ToString()));
                    Double Y2 = (Convert.ToString(DT.Rows[i]["DE"]) == "" ? 0 : Convert.ToDouble(DT.Rows[i]["DE"].ToString()));

                    // === When Actual value is not available then no series will display and no marker will display
                    if (Y2 == 0)
                    {
                        Y1 = Y2;
                        objSeries0.Points[PointIndex].MarkerImage = "";
                        objSeries2.Points[PointIndex].MarkerImage = "";
                    }

                    // === Get HTML color Name from LAB(s) data === //
                    string strColorName = Convert.ToString(DT.Rows[i]["PatchType"]);
                    string strHTMLColor = Formulas.ColorCalcuations.LabtoRGB(Convert.ToInt32(DT.Rows[i]["LAB_L1"]), Convert.ToInt32(DT.Rows[i]["LAB_A1"]), Convert.ToInt32(DT.Rows[i]["LAB_B1"])); ;

                    Series SeriesActual = objSeries1;
                    objSeries0.Points.AddXY((Double)0, Y1);
                    objSeries0.Points[PointIndex]["PointWidth"] = "0.8";
                    objSeries1.Points.AddXY((Double)0, Y2);
                    objSeries2.Points.AddXY((Double)0, Y1);

                    objSeries0.Points[PointIndex].BackGradientType = GradientType.DiagonalRight;

                    // === There are total 10 records fixed in this table === //
                    // === Gether Color in Color Object to assing in series === //
                    Color objActualColor = new Color();
                    switch (strColorName)
                    {
                        case "Cyan":
                            objActualColor = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
                            break;
                        case "Magenta":
                            objActualColor = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
                            break;
                        case "Yellow":
                            objActualColor = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
                            break;
                        case "Red":
                            objActualColor = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
                            break;
                        case "Green":
                            objActualColor = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
                            break;
                        case "Blue":
                            objActualColor = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
                            break;
                        case "Paper":
                            objActualColor = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
                            break;
                        case "Gray 25":
                            objActualColor = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
                            break;
                        case "Gray 50":
                            objActualColor = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
                            break;
                        case "Gray 75":
                            objActualColor = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
                            break;
                    }

                    //SeriesActual.Points[PointIndex].BorderColor = objActualColor;
                    //if (Y2 > Y1)
                    //objActualColor = Color.Transparent;

                    SeriesActual.Points[PointIndex].Color = objActualColor;
                    SeriesActual.Points[PointIndex]["PointWidth"] = "0.8";
                    SeriesActual.Points[PointIndex].Href = "#";

                    //========== Increase Point Index ==========//
                    PointIndex = PointIndex + 1;
                }

                // === Save Chart Image after generating image with proper data === //
                string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(CHRT.ImageUrl) + "." + CHRT.ImageType;
                CHRT.Save(Server.MapPath(FileName));
                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion



    }
}