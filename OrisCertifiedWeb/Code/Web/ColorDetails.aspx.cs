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
using System.Data.SqlClient;
using Fuji.Business;
using System.Drawing;
using Dundas.Charting.WebControl;
using Dundas.Gauges.WebControl;
#endregion

public partial class ColorDetails : PageBase
{
    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Error ==========//
            LblError.Text = "";

            //========== Check And Set Labels ==========//
            if (IsPostBack == false)
            {
                //========== Fill Labels ==========//
                FillLabels();

                //========== Set Key ==========//
                LTRL_KeyDetails.Text = LTRL_CPMeasurementGUID.Text + "-Color-DSet-Details";

                //========== Clear Session ==========//
                Fuji.Common.Sessions.SetSessions(LTRL_KeyDetails.Text, null);

                //========== Show Hide ==========//
                if (Fuji.Common.Sessions.Discard_CPV_Measurement == false)
                { BTN_DeleteJob.Visible = false; }
                else
                { BTN_DeleteJob.Visible = true; }

                //========== Set Variables ==========//                
                FillDataSet();
                FillOutputDeivce();
                FillInkLevels();
                FillJobPreview();
                FillTempRH();
                FillDelta();
            }

            //========== Assign Attribute to the button ==========//
            //BTN_DeleteJob.Attributes.Add("onclick", "javascript:if( confirm('This will remove the entry from the list.\nAre you sure you sure you want to proceed?') == true ){return true;}else{return false;}");
            BTN_Header.Attributes.Add("OnClick", "javascript:Pageopen('" + LTRL_CPMeasurementGUID.Text + "');return false;");
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void BTN_DeleteJob_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Delete the Job ==========//
            CPMeasurementData objCPMeasurement = new CPMeasurementData();
            objCPMeasurement.CPMeasurementGUID = LTRL_CPMeasurementGUID.Text;
            objCPMeasurement.uspUpdateIsUsedFlagOnGUID();

            //========== Message ==========//
            //LblError.Text = "CPV Measurement Discarded successfully.";
            Fuji.Common.CommonFunctions.PageRedirect("MeasurementNotFound", "");

        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }


    /* 
       Added by cshah@fujifilmgs.com 04/17/2009
       download measurementfile if it were saved in the db        
    */
    protected void BTN_DownloadFile_Click(object sender, EventArgs e)
    {
        try
        {
            CPMeasurementData objCPMeasurement = new CPMeasurementData();
            objCPMeasurement.CPMeasurementGUID = LTRL_CPMeasurementGUID.Text;
            DataTable DT = objCPMeasurement.uspGetMeasurementFile();

            if (DT.Rows.Count > 0)
            {
               
                Response.AddHeader("Content-disposition", "attachment; filename=" + LTRL_CPMeasurementGUID.Text + ".txt");
                Response.ContentType = "application/octet-stream";

                Response.Write(DT.Rows[0]["MeasurementFile"].ToString());
                Response.End();  //By Chirag why not to use HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Fuji.Common.CommonFunctions.PageRedirect("MeasurementNotFound", "");
            }

        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }

    #endregion

    #region Methods and Functions
    private void FillLabels()
    {
        try
        {
            LTRL_CPDeviceID.Text = Convert.ToString(Request.QueryString["CPDeviceID"]);
            LTRL_CPMeasurementGUID.Text = Convert.ToString(Request.QueryString["CPMeasurementGUID"]);
            LTRL_CPDevicePrintTargetID.Text = Convert.ToString(Request.QueryString["CPDevicePrintTargetID"]);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillDataSet()
    {
        try
        {
            //========== Instructions ==========//
            //Table[0] = CPMeasurement
            //Table[1] = CPMeasurement_Printer
            //Table[2] = CPDevicePrintTarget
            //Table[3] = CPMeasurement_DP
            //Table[4] = CPDevice
            //Table[5] = CPDevicePrintTarget

            //========== Get Data ==========//
            if (Fuji.Common.Sessions.GetSession(LTRL_KeyDetails.Text) == null)
            {
                //========== Declarations and Retrieval ==========//
                CPMeasurementData objCPMeasurement = new CPMeasurementData();
                objCPMeasurement.CPMeasurementGUID = LTRL_CPMeasurementGUID.Text;
                objCPMeasurement.CPDeviceID = Convert.ToInt32(LTRL_CPDeviceID.Text);
                Fuji.Common.Sessions.SetSessions(LTRL_KeyDetails.Text, objCPMeasurement.uspGetDTLTrendDetails(Convert.ToInt32(LTRL_CPDevicePrintTargetID.Text)));
            }
            //DataSet DSet = (DataSet)Fuji.Common.Sessions.SetGet(LTRL_KeyDetails.Text);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillOutputDeivce()
    {
        try
        {
            //========== Get The Data Set ==========//
            DataSet DSet = (DataSet)Fuji.Common.Sessions.GetSession(LTRL_KeyDetails.Text);

            #region Tables[0]
            if (DSet.Tables[0].Rows.Count > 0)
            {
                LTRL_User.Text = DSet.Tables[0].Rows[0]["User"].ToString();
                LTRL_Customer.Text = DSet.Tables[0].Rows[0]["Customer"].ToString();
                LTRL_Information.Text = DSet.Tables[0].Rows[0]["Info_Text"].ToString();
            }
            #endregion

            #region Tables[1]
            if (DSet.Tables[1].Rows.Count > 0)
            {
                LTRL_PrinterUser.Text = DSet.Tables[1].Rows[0]["Printer_User"].ToString();
                LTRL_PrinterHost.Text = DSet.Tables[1].Rows[0]["Printer_Host"].ToString();
                LTRL_OutputTime.Text = DSet.Tables[1].Rows[0]["Printer_Time"].ToString();
                LTRL_PrinterPaper.Text = DSet.Tables[1].Rows[0]["Printer_Paper"].ToString();
                LTRL_PrinterCCTable.Text = DSet.Tables[1].Rows[0]["Printer_CC_Table"].ToString();
                LTRL_PrinterTargetICC.Text = DSet.Tables[1].Rows[0]["Printer_ICC_Target"].ToString();
            }
            #endregion

            #region Tables[3]
            if (DSet.Tables[3].Rows.Count > 0)
            {
                LTRL_Inks.Text = DSet.Tables[3].Rows[0]["Ink"].ToString();
                LTRL_Substrate.Text = DSet.Tables[3].Rows[0]["Substrate"].ToString();
            }
            #endregion

            #region Tables[4]
            if (DSet.Tables[4].Rows.Count > 0)
            {
                LTRL_OutPutDevice.Text = DSet.Tables[4].Rows[0]["CPDeviceDesc"].ToString();
            }
            #endregion

            #region Tables[5]
            if (DSet.Tables[5].Rows.Count > 0)
            {
                LTRL_PrintTarget.Text = DSet.Tables[5].Rows[0]["PrintTarget"].ToString();
                if (LTRL_OutPutDevice.Text == "") { LTRL_OutPutDevice.Text = DSet.Tables[5].Rows[0]["PrintTarget"].ToString(); }
            }
            #endregion

            #region Tables[6]
            if (DSet.Tables[6].Rows.Count > 0)
            {
                LTRL_Spectro.Text = DSet.Tables[6].Rows[0]["Spectro"].ToString();
            }
            #endregion
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillInkLevels()
    {
        try
        {
            //========== Gets the Link ==========//
            CPMeasurement_PrinterData objCPMeasurementPrinter = new CPMeasurement_PrinterData();
            objCPMeasurementPrinter.CPMeasurementGUID = LTRL_CPMeasurementGUID.Text;//"6ACC1562-4E02-45FF-B480-9E01FB97FCCF";//
            DataTable DT = objCPMeasurementPrinter.uspGetDTLTrendInk();

            //========== Bind the chart ==========//
            if (DT.Rows.Count > 0)
            {
                //========== Get the series ==========//
                Series objSeries = (Series)CHRT_InkLevel.Series["Series1"];
                objSeries.ShowInLegend = false;
                Boolean Status = false;
                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    //========== Get the key and value ==========//
                    string _Key = DT.Rows[i]["InkName"].ToString(); if (_Key == "") { _Key = "-"; }
                    string _Value = DT.Rows[i]["InkLevel"].ToString();
                    _Value = Convert.ToString(Convert.ToDouble(_Value) * 100);

                    if (Convert.ToDecimal(DT.Rows[0]["InkLevel"]) > 0
                        & Status != true)
                    { Status = true; }

                    objSeries.Points.AddXY(_Key, _Value);
                    objSeries.Points[i].ToolTip = "Name : " + _Key + ", Value : " + _Value;
                    objSeries.Points[i].Href = "#";

                    //==========  Get The colors and Print ==========//
                    string _Color = Fuji.Common.CommonFunctions.ConvertNameToHTMLColor(_Key);
                    objSeries.Points[i].Color = ColorTranslator.FromHtml(_Color);

                }
                if (Status == true)
                    DivInkLevelCHRT.Visible = true;
                else
                    DivInkLevelCHRT.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void FillJobPreview()
    {
        try
        {
            CPMeasurementData objCPMeasurement = new CPMeasurementData();
            objCPMeasurement.CPMeasurementGUID = LTRL_CPMeasurementGUID.Text;
            DataTable DT = objCPMeasurement.uspGetCPJobPreview();

            

            //========== Fill Label Values ==========//
            if (DT.Rows.Count > 0)
            {
                Image1.ImageUrl = "DisplayJobPreview.aspx?CPMeasurementGUID=" + LTRL_CPMeasurementGUID.Text;

                DivJobPreview.Visible = true;
               
            }
           
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void FillTempRH()
    {
        try
        {
            //========== Gets the information on DeviceID ==========//
            CPMeasurementData objCPMeasurement = new CPMeasurementData();
            objCPMeasurement.CPMeasurementGUID = LTRL_CPMeasurementGUID.Text;
            DataTable DT = objCPMeasurement.uspGetTempRHData();

            //========== Fill Label Values ==========//
            if (DT.Rows.Count > 0)
            {
                LTRL_Temp_ACT.Text = DT.Rows[0]["TempF"].ToString();
                LTRL_Temp_TRG.Text = DT.Rows[0]["TargetTemp"].ToString();

                LTRL_RH_ACT.Text = DT.Rows[0]["RH"].ToString();
                LTRL_RH_TRG.Text = DT.Rows[0]["TargetRH"].ToString();
                DivTempCHART.Visible = true;
            }
            else
                DivTempCHART.Visible = false;

            CHRT_Temp.LinearGauges[0].Pointers[0].Value = Convert.ToDouble(LTRL_Temp_ACT.Text);
            CHRT_Temp.LinearGauges[0].Pointers[0].ToolTip = "Actual Temp : " + LTRL_Temp_ACT.Text + "&#176;F, Target Temp : " + LTRL_Temp_TRG.Text + "&#176;F";
            CHRT_Temp.LinearGauges[0].Pointers[0].Href = "#";

            CHRT_RH.LinearGauges[0].Pointers[0].Value = Convert.ToDouble(LTRL_RH_ACT.Text);
            CHRT_RH.LinearGauges[0].Pointers[0].ToolTip = "Actual RH : " + LTRL_RH_ACT.Text + "%, Target RH : " + LTRL_RH_TRG.Text + "%";
            CHRT_RH.LinearGauges[0].Pointers[0].Href = "#";
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillDelta()
    {
        try
        {
            //============ Get all data and show =========//
            CPPatchDataLabData objCPPatchDataLab = new CPPatchDataLabData();
            objCPPatchDataLab.MeasurementGUID = LTRL_CPMeasurementGUID.Text;
            DataSet DSet = objCPPatchDataLab.uspGetDTLDetailsOnGUID();


            //============ Cyan =========//
            if (DSet.Tables[0].Rows.Count > 0)
            {
                LTRL_Thrsh_CyanDe.Text = DSet.Tables[0].Rows[0]["ThrshDe"].ToString();

                LTRL_Actual_CyanDe.Text = DSet.Tables[0].Rows[0]["AveDe"].ToString();
                LTRL_Std_CyanDe.Text = DSet.Tables[0].Rows[0]["StdDe"].ToString();
                LTRL_Actual_CyanDH.Text = DSet.Tables[0].Rows[0]["AveDH"].ToString();
                LTRL_Std_CyanDH.Text = DSet.Tables[0].Rows[0]["StdDH"].ToString();
            }

            //============ Magenta =========//
            if (DSet.Tables[1].Rows.Count > 0)
            {
                LTRL_Thrsh_MagDe.Text = DSet.Tables[1].Rows[0]["ThrshDe"].ToString();

                LTRL_Actual_MagDe.Text = DSet.Tables[1].Rows[0]["AveDe"].ToString();
                LTRL_Std_MagDe.Text = DSet.Tables[1].Rows[0]["StdDe"].ToString();
                LTRL_Actual_MagDH.Text = DSet.Tables[1].Rows[0]["AveDH"].ToString();
                LTRL_Std_MagDH.Text = DSet.Tables[1].Rows[0]["StdDH"].ToString();
            }

            //============ Yellow =========//
            if (DSet.Tables[2].Rows.Count > 0)
            {
                LTRL_Thrsh_YellDe.Text = DSet.Tables[2].Rows[0]["ThrshDe"].ToString();

                LTRL_Actual_YellDe.Text = DSet.Tables[2].Rows[0]["AveDe"].ToString();
                LTRL_Std_YellDe.Text = DSet.Tables[2].Rows[0]["StdDe"].ToString();
                LTRL_Actual_YellDH.Text = DSet.Tables[2].Rows[0]["AveDH"].ToString();
                LTRL_Std_YellDH.Text = DSet.Tables[2].Rows[0]["StdDH"].ToString();
            }

            //============ Black =========//
            if (DSet.Tables[3].Rows.Count > 0)
            {
                LTRL_Thrsh_BlackDe.Text = DSet.Tables[3].Rows[0]["ThrshDe"].ToString();

                LTRL_Actual_BlackDe.Text = DSet.Tables[3].Rows[0]["AveDe"].ToString();
                LTRL_Std_BlackDe.Text = DSet.Tables[3].Rows[0]["StdDe"].ToString();
                LTRL_Actual_BlackDH.Text = DSet.Tables[3].Rows[0]["AveDH"].ToString();
                LTRL_Std_BlackDH.Text = DSet.Tables[3].Rows[0]["StdDH"].ToString();
            }

            //============ Paper White =========//
            if (DSet.Tables[4].Rows.Count > 0)
            {
                LTRL_Thrsh_PaperDe.Text = DSet.Tables[4].Rows[0]["ThrshDe"].ToString();

                LTRL_Actual_PaperDe.Text = DSet.Tables[4].Rows[0]["AveDe"].ToString();
                LTRL_Std_PaperDe.Text = DSet.Tables[4].Rows[0]["StdDe"].ToString();
            }

            //============ De Gray =========//
            if (DSet.Tables[5].Rows.Count > 0)
            {
                LTRL_Actual_GrayAveDH.Text = DSet.Tables[5].Rows[0]["AveDH"].ToString();
                LTRL_Std_GrayAveDH.Text = DSet.Tables[5].Rows[0]["StdDH"].ToString();
            }

            //============ Ave De & Max De =========//
            if (DSet.Tables[6].Rows.Count > 0)
            {
                LTRL_Actual_AveDe.Text = DSet.Tables[6].Rows[0]["Average"].ToString();
                LTRL_Actual_MaxDe.Text = DSet.Tables[6].Rows[0]["PatchDeValue"].ToString();
            }

            //============ Show values for Tol =========//
            if (DSet.Tables[7].Rows.Count > 0)
            {
                LTRL_Thrsh_AveDe.Text = DSet.Tables[7].Rows[0]["Average_Limit"].ToString();
                LTRL_Thrsh_MaxDe.Text = DSet.Tables[7].Rows[0]["Maximum_Limit"].ToString();
                LTRL_Thrsh_GrayAveDH.Text = DSet.Tables[7].Rows[0]["GRAY_AVERAGE_LIMIT_DH"].ToString();

                LTRL_Thrsh_CyanDH.Text = LTRL_Thrsh_MagDH.Text = LTRL_Thrsh_YellDH.Text = LTRL_Thrsh_BlackDH.Text = DSet.Tables[7].Rows[0]["PRIMARY_LIMIT_DH"].ToString();
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

}
