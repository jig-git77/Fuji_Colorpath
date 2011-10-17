using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Dundas.Charting.WebControl;
using System.Drawing;

public partial class PlateCharts : PageBase
{
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Error ==========//
            LblError.Text = "";

            //========== Default Settings ==========//
            if (IsPostBack == false)
            {
                //========== Fill From Request Querystring ==========//
                FillLabels();
                LTRL_KEY.Text = LTRL_CPMeasurementGUID.Text + "-Plate-DT-Charts";

                //========== Clear the Sessions ==========//
                Fuji.Common.Sessions.SetSessions(LTRL_KEY.Text, null);


                //========== Fill Charts ==========//
                RefillCharts();

                //========== Plot Legend At Bottom ==========//            
                CHRT_Deviation.Legends[0].Docking = LegendDocking.Bottom;
                CHRT_Deviation.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

            }

        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }

    protected void CHK_Deviation_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //========== Fills All Charts ==========//
            RefillCharts();
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }

    #endregion

    #region Methods and Functions
    private void FillLabels()
    {
        try
        {
            LTRL_CPDeviceID.Text = Convert.ToString(Request.QueryString["CPDeviceID"]);
            LTRL_CPDevicePrintTargetID.Text = Convert.ToString(Request.QueryString["CPDevicePrintTargetID"]);
            LTRL_CPMeasurementGUID.Text = Convert.ToString(Request.QueryString["CPMeasurementGUID"]);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Fill All Charts
    private void RefillCharts()
    {
        try
        {
            FillChartDeviation();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillChartDeviation()
    {
        try
        {
            //========== Get The Delta E Data ==========//        
            if (Fuji.Common.Sessions.GetSession(LTRL_KEY.Text) == null)
            {
                Fuji.Business.CPPatchDataPlateData objPatchDataPlate = new Fuji.Business.CPPatchDataPlateData();
                objPatchDataPlate.MeasurementGUID = LTRL_CPMeasurementGUID.Text.Trim();
                Fuji.Common.Sessions.SetSessions(LTRL_KEY.Text, objPatchDataPlate.uspGetDeviationByGUID());
            }
            DataTable Dt = (DataTable)Fuji.Common.Sessions.GetSession(LTRL_KEY.Text);

            #region Deviation of 25,50,75,95
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i = i + 1) // Loop For : Deviation 25, Deviation 50, Deviation 75, Deviation 95
                {

                    #region Deviation : Gets the Series for Deviation
                    Series objSeries = (Series)CHRT_Deviation.Series["Series1"];
                    objSeries.Type = SeriesChartType.Column;

                    objSeries.XValueType = ChartValueTypes.String;
                    objSeries.YValueType = ChartValueTypes.Double;
                    #endregion

                    //====  Set Value in Graph  ====//

                    objSeries.Points.AddXY(Dt.Rows[i]["AREA_TARGET"].ToString(), Dt.Rows[i]["DEVIATION"].ToString());

                    if (i == 0) { objSeries.Points[i].Color = Color.FromArgb(140, 140, 140); }
                    if (i == 1) { objSeries.Points[i].Color = Color.FromArgb(120, 120, 120); }
                    if (i == 2) { objSeries.Points[i].Color = Color.FromArgb(100, 100, 100); }
                    if (i == 3) { objSeries.Points[i].Color = Color.FromArgb(80, 80, 80); }
                    if (i == 4) { objSeries.Points[i].Color = Color.FromArgb(75, 75, 75); }
                    if (i == 5) { objSeries.Points[i].Color = Color.FromArgb(70, 70, 70); }
                    if (i == 6) { objSeries.Points[i].Color = Color.FromArgb(65, 65, 65); }


                    objSeries.Points[i].ToolTip = " Dot Area " + Dt.Rows[i]["AREA_TARGET"].ToString() + "; Deviation:" + Dt.Rows[i]["DEVIATION"].ToString();
                    objSeries.Points[i].Href = "#";

                    //====  Add Tooltip  ====//
                }
            }
            else
            {
                AddBlankRow(CHRT_Deviation);
            }
            #endregion
        }
        catch (Exception Ex)
        {
            throw Ex;
        }

    }
    #endregion

    #region Add Blank Entry
    private void AddBlankRow(Chart objChart)
    {
        try
        {
            Series objSeries0 = (Series)objChart.Series["Series0"];
            objSeries0.Type = SeriesChartType.Spline;
            objSeries0.XValueType = ChartValueTypes.DateTime;
            objSeries0.YValueType = ChartValueTypes.Double;

            objSeries0.Points.AddXY(System.DateTime.Now.ToString(), "1");
            objSeries0.Points.AddXY(System.DateTime.Now.AddDays(1).ToString(), "1");
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
}
