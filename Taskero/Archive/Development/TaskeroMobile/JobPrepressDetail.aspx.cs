#region System
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI;
#endregion

#region Others
using System.Data.SqlClient;
using Fuji.Common;
using Fuji.Business;
using Dundas.Charting.WebControl;
using System.Drawing;
#endregion


public partial class JobPrepressDetail : PageBase
{
    #region Page Load and Events
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param> 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["GUID"] != null && Convert.ToString(Request.QueryString["GUID"]) != "")
                FillData(Convert.ToString(Request.QueryString["GUID"]));
        }
    }
    protected void ChkPrintTarget_CheckedChanged(object sender, EventArgs e)
    {
        if (Request.QueryString["GUID"] != null && Convert.ToString(Request.QueryString["GUID"]) != "")
            FillDeviceTrend(Convert.ToString(Request.QueryString["GUID"]), ChkPrintTarget.Checked);

    }
    #endregion

    #region Methods
    private void FillData(string GUID)
    {
        DataTable dtJobReportPrepress = new DataTable();        
        dtJobReportPrepress = ((DataTable)Fuji.Common.Sessions.GetSession("dtJobReportPrepressMob")).Copy();

        if (dtJobReportPrepress != null && dtJobReportPrepress.Rows.Count > 0)
        {
            dtJobReportPrepress.DefaultView.RowFilter = "GUID ='" + GUID + "'";
            if (dtJobReportPrepress.DefaultView.Count > 0)
            {
                if (dtJobReportPrepress.DefaultView[0]["Status"].ToString() == Fuji.Common.Enums.enumStatus.Failed.ToString())
                    imgStatus.Src = "images/status_failed.png"; //========  Failed Then Red Star  ========//            
                else if (dtJobReportPrepress.DefaultView[0]["Status"].ToString() == Fuji.Common.Enums.enumStatus.Caution.ToString())
                    imgStatus.Src = "images/status_caution.png"; //========  Caution Then Yellow Star  ========//                
                else if (dtJobReportPrepress.DefaultView[0]["Status"].ToString() == Fuji.Common.Enums.enumStatus.Passed.ToString())
                    imgStatus.Src = "images/status_passed.png"; //========  Pass Then Green Star  ========//

                LblJobNo.Text = Convert.ToString(dtJobReportPrepress.DefaultView[0]["Job"]);
                LblPrintTarget.Text = Convert.ToString(dtJobReportPrepress.DefaultView[0]["PrintTarget"]);
                LblDevice.Text = Convert.ToString(dtJobReportPrepress.DefaultView[0]["CPDeviceDesc"]);
                LblWorkStation.Text = Convert.ToString(dtJobReportPrepress.DefaultView[0]["DongleSerial"]);
                LblBranchName.Text = Convert.ToString(dtJobReportPrepress.DefaultView[0]["BranchName"]);
                LblDeviceType.Text = Convert.ToString(dtJobReportPrepress.DefaultView[0]["CPDeviceTypeDesc"]);
                LblDate.Text = Convert.ToString(dtJobReportPrepress.DefaultView[0]["DateTime"]);

                FillJobMatrics(GUID);
                FillDeviceTrend(GUID,ChkPrintTarget.Checked);
            }
        }
           

    }
    private void FillJobMatrics(string GUID)
    {
        double Tol = 0;
        double Act = 0;
        DataTable dtMobiJobDeviceData = new DataTable();
        CPVMobiJobDeviceData ObjCPVMobiJobDeviceData = new CPVMobiJobDeviceData();
        ObjCPVMobiJobDeviceData.CPMeasurementGUID = GUID;
        dtMobiJobDeviceData = ObjCPVMobiJobDeviceData.uspGetMobiJobMetrics();
        
        if (dtMobiJobDeviceData != null && dtMobiJobDeviceData.Rows.Count > 0)
        {

            #region de Paper
            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["Paper_Tol"].ToString()))
            {
                lbldePaperTol.Text = dtMobiJobDeviceData.Rows[0]["Paper_Tol"].ToString();
                Tol = Convert.ToDouble(dtMobiJobDeviceData.Rows[0]["Paper_Tol"].ToString());
            }
            else
            {
                lbldePaperTol.Text = string.Empty;
                Tol = 0;
            }

            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["Paper_ACT"].ToString()))
            {
                lbldePaperAct.Text = dtMobiJobDeviceData.Rows[0]["Paper_ACT"].ToString();
                Act = Convert.ToDouble(dtMobiJobDeviceData.Rows[0]["Paper_ACT"].ToString());
            }
            else
            {
                lbldePaperAct.Text = string.Empty;
                Act = 0;
            }

            if (string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["Paper_Tol"].ToString()) && string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["Paper_ACT"].ToString()))
                imgdePaper.Visible = false;
            else
                imgdePaper.Src = CheckPassOrFail(Tol,Act);

            #endregion

            #region dE Ave
            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["AvgDE_TOL"].ToString()))
            {
                lbldEAveTol.Text = dtMobiJobDeviceData.Rows[0]["AvgDE_TOL"].ToString();
                Tol = Convert.ToDouble(dtMobiJobDeviceData.Rows[0]["AvgDE_TOL"].ToString());
            }
            else
            {
                lbldEAveTol.Text = string.Empty;
                Tol = 0;
            }

            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["AvgDE_ACT"].ToString()))
            {
                lbldEAveAct.Text = dtMobiJobDeviceData.Rows[0]["AvgDE_ACT"].ToString();
                Act = Convert.ToDouble(dtMobiJobDeviceData.Rows[0]["AvgDE_ACT"].ToString());
            }
            else
            {
                lbldEAveAct.Text = string.Empty;
                Act = 0;
            }

            if (string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["AvgDE_TOL"].ToString()) && string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["AvgDE_ACT"].ToString()))
                imgdEAve.Visible = false;
            else
                imgdEAve.Src = CheckPassOrFail(Tol, Act);
            #endregion

            #region dE Max
            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["MaxDE_Tol"].ToString()))
            {
                lbldEMaxTol.Text = dtMobiJobDeviceData.Rows[0]["MaxDE_Tol"].ToString();
                Tol = Convert.ToDouble(dtMobiJobDeviceData.Rows[0]["MaxDE_Tol"].ToString());
            }
            else
            {
                lbldEMaxTol.Text = string.Empty;
                Tol = 0;
            }

            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["MaxDE_ACT"].ToString()))
            {
                lbldEMaxAct.Text = dtMobiJobDeviceData.Rows[0]["MaxDE_ACT"].ToString();
                Act = Convert.ToDouble(dtMobiJobDeviceData.Rows[0]["MaxDE_ACT"].ToString());
            }
            else
            {
                lbldEMaxAct.Text = string.Empty;
                Act = 0;
            }

            if (string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["MaxDE_Tol"].ToString()) && string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["MaxDE_ACT"].ToString()))
                imgdEMax.Visible = false;
            else
                imgdEMax.Src = CheckPassOrFail(Tol, Act);
            #endregion

            #region dE Prim
            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["PatchMaximumdE"].ToString()))
                lbldEMaxAct.Text = lbldEMaxAct.Text + "&nbsp;<b>" + dtMobiJobDeviceData.Rows[0]["PatchMaximumdE"].ToString() + "</b>";

            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["Primary_Tol"].ToString()))
            {
                lbldEPrimTol.Text = dtMobiJobDeviceData.Rows[0]["Primary_Tol"].ToString();
                Tol = Convert.ToDouble(dtMobiJobDeviceData.Rows[0]["Primary_Tol"].ToString());
            }
            else
            {
                lbldEPrimTol.Text = string.Empty;
                Tol = 0;
            }

            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["Primary_ACT"].ToString()))
            {
                lbldEPrimAct.Text = dtMobiJobDeviceData.Rows[0]["Primary_ACT"].ToString();
                Act = Convert.ToDouble(dtMobiJobDeviceData.Rows[0]["Primary_ACT"].ToString());
            }
            else
            {
                lbldEPrimAct.Text = string.Empty;
                Act = 0;
            }

            if (string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["Primary_Tol"].ToString()) && string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["Primary_ACT"].ToString()))
                imgdEPrim.Visible = false;
            else
                imgdEPrim.Src = CheckPassOrFail(Tol, Act);

            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["PrimaryDE_Patch"].ToString()))
                lbldEPrimAct.Text = lbldEPrimAct.Text + "&nbsp;<b>" + dtMobiJobDeviceData.Rows[0]["PrimaryDE_Patch"].ToString() + "</b>";

            #endregion

            #region dH Prim
            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["PrimaryDH_TOL"].ToString()))
            {
                lbldHPrimTol.Text = dtMobiJobDeviceData.Rows[0]["PrimaryDH_TOL"].ToString();
                Tol = Convert.ToDouble(dtMobiJobDeviceData.Rows[0]["PrimaryDH_TOL"].ToString());
            }
            else
            {
                lbldHPrimTol.Text = string.Empty;
                Tol = 0;
            }

            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["PrimaryDH_ACT"].ToString()))
            {
                lbldHPrimAct.Text = dtMobiJobDeviceData.Rows[0]["PrimaryDH_ACT"].ToString();
                Act = Convert.ToDouble(dtMobiJobDeviceData.Rows[0]["PrimaryDH_ACT"].ToString());
            }
            else
            {
                lbldHPrimAct.Text = string.Empty;
                Act = 0;
            }

            if (string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["PrimaryDH_TOL"].ToString()) && string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["PrimaryDH_ACT"].ToString()))
                imgdHPrim.Visible = false;
            else
                imgdHPrim.Src = CheckPassOrFail(Tol, Act);

            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["PrimaryDH_Patch"].ToString()))
                lbldHPrimAct.Text = lbldHPrimAct.Text + "&nbsp;<b>" + dtMobiJobDeviceData.Rows[0]["PrimaryDH_Patch"].ToString() + "</b>";

            #endregion

            #region dH Gray
            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["GrayAvgDH_TOL"].ToString()))
            {
                lbldHGrayTol.Text = dtMobiJobDeviceData.Rows[0]["GrayAvgDH_TOL"].ToString();
                Tol = Convert.ToDouble(dtMobiJobDeviceData.Rows[0]["GrayAvgDH_TOL"].ToString());                
            }
            else
            {
                lbldHGrayTol.Text = string.Empty;
                Tol = 0;
            }

            if (!string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["GrayAvgDH_ACT"].ToString()))
            {
                lbldHGrayAct.Text = dtMobiJobDeviceData.Rows[0]["GrayAvgDH_ACT"].ToString();
                Act = Convert.ToDouble(dtMobiJobDeviceData.Rows[0]["GrayAvgDH_ACT"].ToString());
            }
            else
            {
                lbldHGrayAct.Text = string.Empty;
                Act = 0;
            }

            if (string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["GrayAvgDH_TOL"].ToString()) && string.IsNullOrEmpty(dtMobiJobDeviceData.Rows[0]["GrayAvgDH_ACT"].ToString()))
                imgdHGray.Visible = false;
            else
                imgdHGray.Src = CheckPassOrFail(Tol, Act);
            #endregion


        }
        else
        {
            lbldePaperTol.Text = string.Empty;
            lbldePaperAct.Text = string.Empty;
            lbldEAveTol.Text = string.Empty;
            lbldEAveAct.Text = string.Empty;
            lbldEMaxTol.Text = string.Empty;
            lbldEMaxAct.Text = string.Empty;
            lbldEPrimTol.Text = string.Empty;
            lbldEPrimAct.Text = string.Empty;
            lbldHPrimTol.Text = string.Empty;
            lbldHPrimAct.Text = string.Empty;
            lbldHGrayTol.Text = string.Empty;
            lbldHGrayAct.Text = string.Empty;
        }

    }
    private string CheckPassOrFail(double Tol, double Act)
    {
        if (Tol >= Act)
            return "images/icon_passed.png";
        else
            return "images/icon_failed.png";
    }

    private void FillDeviceTrend(string GUID,bool PrintTarget)
    {
        try
        {
            DataTable dtMobiDeviceTrendData = new DataTable();
            CPVMobiJobDeviceData ObjCPVMobiJobDeviceData = new CPVMobiJobDeviceData();
            ObjCPVMobiJobDeviceData.CPMeasurementGUID = GUID;
            ObjCPVMobiJobDeviceData.PrintTarget = PrintTarget;
            dtMobiDeviceTrendData = ObjCPVMobiJobDeviceData.uspGetMobiDeviceTrend();



            #region Delta E
            //========== Get all the series ==========//
            Series objAvgDE = (Series)CHRT_PrepressTargetData.Series["Series1"]; // AvgDE
            objAvgDE.Type = SeriesChartType.Spline;
            objAvgDE.Color = Color.Cyan;

            Series objMaxDE = (Series)CHRT_PrepressTargetData.Series["Series2"]; // MaxDE
            objMaxDE.Type = SeriesChartType.Spline;
            objMaxDE.Color = Color.Magenta;

            #endregion

            #region Plot Values
            for (int i = 0; i < dtMobiDeviceTrendData.Rows.Count; i = i + 1)
            {
                objAvgDE.Points.AddXY(dtMobiDeviceTrendData.Rows[i]["CreatedDT"], dtMobiDeviceTrendData.Rows[i]["AvgDE"]);
                objAvgDE.Points[i].ToolTip = "GUID : " + GUID;
                objAvgDE.Points[i].MarkerStyle = MarkerStyle.Circle;
                objAvgDE.Points[i].MarkerColor = Color.Red;
                objAvgDE.Points[i].MarkerSize = 8;

                objMaxDE.Points.AddXY(dtMobiDeviceTrendData.Rows[i]["CreatedDT"], dtMobiDeviceTrendData.Rows[i]["MaxDE"]);
                objMaxDE.Points[i].ToolTip = "GUID : " + GUID;
                objMaxDE.Points[i].MarkerStyle = MarkerStyle.Circle;
                objMaxDE.Points[i].MarkerColor = Color.Red;
                objMaxDE.Points[i].MarkerSize = 8;

            }
            #endregion
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

   
}
