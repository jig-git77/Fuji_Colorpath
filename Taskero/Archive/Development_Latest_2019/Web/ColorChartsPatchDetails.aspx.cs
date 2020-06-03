#region System
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
#endregion

#region Others
using Fuji.Business;
using System.Drawing;
using Formulas;
using Dundas.Charting.WebControl;
#endregion


public partial class ColorChartsPatchDetails : PageBase
{

    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Error ==========//
            LblError.Text = "";

            //========== Assign Css ==========//            
            LtrlCSS.Text = @"<link href='CSS/" + Fuji.Common.Sessions.Css + "' rel='stylesheet' type='text/css' />";


            //========== Check And Set Labels ==========//
            if (IsPostBack == false)
            {
                //========== Set Labels ==========//        
                FillLabels();
                LTRL_KEY.Text = LTRL_CPMeasurementGUID.Text + "-Color-DT-DTLChartPatchDetailsWindow";
                //========== Clear Session ==========//                
                Fuji.Common.Sessions.SetSessions(LTRL_KEY.Text, null);

                //========== Fills Both Chart and DataList ==========//
                FillPatchDetails();

            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    #endregion

    #region Methods and Functiosn
    private void FillLabels()
    {
        try
        {
            LTRL_CPDeviceID.Text = Convert.ToString(Request.QueryString["CPDeviceID"]);
            LTRL_CPDevicePrintTargetID.Text = Convert.ToString(Request.QueryString["CPDevicePrintTargetID"]);
            LTRL_CPMeasurementGUID.Text = Convert.ToString(Request.QueryString["CPMeasurementGUID"]);
            LTRL_CPPatchDataLabID.Text = Convert.ToString(Request.QueryString["CPPatchDataLabID"]); 
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillPatchDetails()
    {
        try
        {
            if (LTRL_CPPatchDataLabID.Text != "")
            {
                //========== Get Data Set ==========//            
                if (Fuji.Common.Sessions.GetSession(LTRL_KEY.Text) == null)
                {
                    CPPatchDataLabData objCPPatchDataLab = new CPPatchDataLabData();
                    objCPPatchDataLab.CPPatchDataLabID = Convert.ToInt32(LTRL_CPPatchDataLabID.Text);
                    Fuji.Common.Sessions.SetSessions(LTRL_KEY.Text, objCPPatchDataLab.uspGetDTLChartsPatchDetailsOnCPPatchDataLabID());
                }
                //========== Get Data From Session ==========//        
                DataTable DT = (DataTable)Fuji.Common.Sessions.GetSession(LTRL_KEY.Text);

                if (DT.Rows.Count == 0)
                {
                    //========== Show/Hide ==========//
                    TR_PatchDetails.Visible = false;
                }
                else
                {
                    //========== Show/Hide ==========//
                    TR_PatchDetails.Visible = true;

                    //========== Assign Title ==========//
                    LTRL_PatchDetails.Text = DT.Rows[0]["Sample_Name"].ToString() + "( C : " + DT.Rows[0]["CMYK_C"].ToString() + ", M : " + DT.Rows[0]["CMYK_M"].ToString() + ", Y : " + DT.Rows[0]["CMYK_Y"].ToString() + ", K : " + DT.Rows[0]["CMYK_K"].ToString() + " , Delta E : " + Convert.ToString(Request.QueryString["DeltaE"]) + " )";

                    //========== Assign Values ==========//
                    TD_LAB1_COLOR.BgColor = Formulas.ColorCalcuations.LabtoRGB(Convert.ToInt32(DT.Rows[0]["LAB_L1"]), Convert.ToInt32(DT.Rows[0]["LAB_A1"]), Convert.ToInt32(DT.Rows[0]["LAB_B1"]));
                    TD_LAB2_COLOR.BgColor = Formulas.ColorCalcuations.LabtoRGB(Convert.ToInt32(DT.Rows[0]["LAB_L2"]), Convert.ToInt32(DT.Rows[0]["LAB_A2"]), Convert.ToInt32(DT.Rows[0]["LAB_B2"]));

                    LTRL_TARGET_L1.Text = DT.Rows[0]["LAB_L1"].ToString();
                    LTRL_TARGET_A1.Text = DT.Rows[0]["LAB_A1"].ToString();
                    LTRL_TARGET_B1.Text = DT.Rows[0]["LAB_B1"].ToString();

                    LTRL_ACTUAL_L2.Text = DT.Rows[0]["LAB_L2"].ToString();
                    LTRL_ACTUAL_A2.Text = DT.Rows[0]["LAB_A2"].ToString();
                    LTRL_ACTUAL_B2.Text = DT.Rows[0]["LAB_B2"].ToString();

                    LTRL_Delta_DL.Text = DT.Rows[0]["DL"].ToString();
                    LTRL_Delta_DA.Text = DT.Rows[0]["DA"].ToString();
                    LTRL_Delta_DB.Text = DT.Rows[0]["DB"].ToString();
                    LTRL_Delta_DH.Text = DT.Rows[0]["DH"].ToString();
                    LTRL_Delta_DC.Text = DT.Rows[0]["DC"].ToString();

                    //========== Plot Chart form CMYK ==========//                
                    CHRT_PlotCMYK.Series["Series1"].Points.AddXY(Convert.ToDouble(LTRL_TARGET_A1.Text), Convert.ToDouble(LTRL_TARGET_B1.Text));
                    CHRT_PlotCMYK.Series["Series1"].Type = SeriesChartType.Point;
                    CHRT_PlotCMYK.Series["Series1"].ToolTip = "A=" + LTRL_TARGET_A1.Text + " | B=" + LTRL_TARGET_B1.Text;
                    CHRT_PlotCMYK.Series["Series1"].ShowInLegend = false;

                    CHRT_PlotCMYK.Series["Series2"].Points.AddXY(Convert.ToDouble(LTRL_ACTUAL_A2.Text), Convert.ToDouble(LTRL_ACTUAL_B2.Text));
                    CHRT_PlotCMYK.Series["Series2"].Type = SeriesChartType.Point;
                    CHRT_PlotCMYK.Series["Series2"].ToolTip = "A=" + LTRL_ACTUAL_A2.Text + " | B=" + LTRL_ACTUAL_B2.Text;
                    CHRT_PlotCMYK.Series["Series2"].ShowInLegend = false;

                    //========== Plot Chart form L ==========//
                    CHRT_PlotL.Series["Series1"].Type = SeriesChartType.Point;
                    CHRT_PlotL.Series["Series1"].Points.AddXY(1, 0);
                    CHRT_PlotL.Series["Series1"].ShowInLegend = false;

                    CHRT_PlotL.Series["Series2"].Type = SeriesChartType.Point;
                    CHRT_PlotL.Series["Series2"].Points.AddXY(1, 100);
                    CHRT_PlotL.Series["Series2"].ShowInLegend = false;

                    CHRT_PlotL.Series["Series3"].Type = SeriesChartType.Point;
                    CHRT_PlotL.Series["Series3"].Points.AddXY(1, Convert.ToDouble(LTRL_TARGET_L1.Text));
                    CHRT_PlotL.Series["Series3"].ToolTip = "Target : " + LTRL_TARGET_L1.Text;
                    CHRT_PlotL.Series["Series3"].ShowInLegend = false;

                    CHRT_PlotL.Series["Series4"].Type = SeriesChartType.Point;
                    CHRT_PlotL.Series["Series4"].Points.AddXY(1, Convert.ToDouble(LTRL_ACTUAL_L2.Text));
                    CHRT_PlotL.Series["Series4"].ToolTip = "Actual : " + LTRL_ACTUAL_L2.Text;
                    CHRT_PlotL.Series["Series4"].ShowInLegend = false;
                }
            }
            else
            {
                //========== Show/Hide ==========//
                TR_PatchDetails.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
}
