using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Fuji.Business;
using System.IO;

public partial class PlateDetails : PageBase
{
    #region Page Events

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
                LTRL_KeyDetails.Text = LTRL_CPMeasurementGUID.Text + "-Plate-DSet-Details";

                //========== Clear Session ==========//
                Fuji.Common.Sessions.SetSessions(LTRL_KeyDetails.Text, null);

                //========== Show Hide ==========//
                if (Fuji.Common.Sessions.Discard_CPV_Measurement == false)
                { BTN_DeleteJob.Visible = false; }
                else
                { BTN_DeleteJob.Visible = true; }

                //========== Set Variables ==========//                
                FillDataTable();
                FillOutputDeivce();
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

    #region Methods & Functions

    private void FillDataTable()
    {
        try
        {
            if (Fuji.Common.Sessions.GetSession(LTRL_KeyDetails.Text) == null)
            {
                Fuji.Business.CPMeasurementData objMeasurement = new CPMeasurementData();
                objMeasurement.CPMeasurementGUID = LTRL_CPMeasurementGUID.Text.Trim();
                Fuji.Common.Sessions.SetSessions(LTRL_KeyDetails.Text, objMeasurement.uspGetDTLDetailsPlateByGUID());
            }
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
            DataTable DT = (DataTable)Fuji.Common.Sessions.GetSession(LTRL_KeyDetails.Text);

            if (DT.Rows.Count > 0)
            {
                //====  Output Device  ====//
                LTRL_Name.Text = DT.Rows[0]["DeviceDesc"].ToString();
                LTRL_Manufacturer.Text = DT.Rows[0]["Manufacturer"].ToString();
                LTRL_Model.Text = DT.Rows[0]["Model"].ToString();
                LTRL_SN.Text = DT.Rows[0]["OD_SerialNum"].ToString();

                //====  Measurement Info  ====//
                LTRL_Device.Text = DT.Rows[0]["MF_Device"].ToString();
                LTRL_SN_1.Text = DT.Rows[0]["MF_SerialNum"].ToString();
                LTRL_User.Text = DT.Rows[0]["UserName"].ToString();
                LTRL_Customer.Text = DT.Rows[0]["Customer"].ToString();
                LTRL_Information.Text = DT.Rows[0]["InfoText"].ToString();
                LTRL_TargetFile.Text = Path.GetFileName(DT.Rows[0]["TargetFile"].ToString());

                //====  Substrate Info  ====//
                LTRL_Manu.Text = DT.Rows[0]["SUBSTRATE"].ToString();
                LTRL_SKU.Text = DT.Rows[0]["SUBSTRATE_SKU"].ToString();
                LTRL_Batch.Text = DT.Rows[0]["SUBSTRATE_BATCH"].ToString();

                //====  Chemistry Info  ====//
                LTRL_Manu_1.Text = DT.Rows[0]["CHEMISTRY"].ToString();
                LTRL_SKU_1.Text = DT.Rows[0]["CHEMISTRY_SKU"].ToString();
                LTRL_Batch_1.Text = DT.Rows[0]["CHEMISTRY_BATCH"].ToString();
            }

            LTRL_GUID.Text = LTRL_CPMeasurementGUID.Text;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

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

    #endregion

}
