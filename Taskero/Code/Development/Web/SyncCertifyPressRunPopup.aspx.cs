using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fuji.Business;
using System.Data;
using Fuji.Common;

public partial class SyncCertifyPressRunPopup : PageBase
{
    #region Page Event
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LblError.Text = "";
            if (IsPostBack == false)
            {
                BindSyncMeasurementData();
                if (Fuji.Common.Sessions.GetSession("SelectedPressRunClose").ToString() == Convert.ToInt16(Enums.CPSyncPressRunStatus.Certify).ToString())
                    BtnCertifyPressRun.Visible = false;                
                else
                    BtnCertifyPressRun.Visible = true;
                
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void BtnCertifyPressRun_Click(object sender, EventArgs e)
    {
        try
        {
            //============ Create object of CPSyncPressRunData =============//
            CPSyncPressRunData objCPSyncPressRun = new CPSyncPressRunData();
            objCPSyncPressRun.PressRunID = Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressRunID"));
            objCPSyncPressRun.Status = 3; // 3 For Certify Press Run.
            objCPSyncPressRun.EndDateTime = Convert.ToDateTime(HDClientDatatime.Value.ToString());
            objCPSyncPressRun.uspUpdatePressRunStatus(); // Update status=2 for close press run.           

             #region Close this popup and refresh parent press grid
            string script = "<script>";
            //script += "var modalPopupBehavior = parent.$find('programmaticModalPopupBehavior');    ";
            //script += "modalPopupBehavior.hide();";
            script += "parent.CallPressRunBindGrid();";
            script += "</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "windowclose", script);
            #endregion

            
        }
        catch (Exception Exc)
        { 
            LblError.Text = HandleException(Exc); 
        }
    }
    

    protected void BtnBackToAnalyzeMeasurement_Click(object sender, EventArgs e)
    {
        try
        {
            #region Go To Analyze Measurement Page
            string script = "<script>";
            //script += "var modalPopupBehavior = parent.$find('programmaticModalPopupBehavior');    ";
            //script += "modalPopupBehavior.hide();";
            script += "parent.CallAnalyzetBind();";
            script += "</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "AnalyzetBind", script);
            #endregion
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }

    #endregion

    #region Methods
    /// <summary>
    /// Used to bind the Sync Press Grid
    /// </summary>
    private void BindSyncMeasurementData()
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPSyncMeasurementData ObjSyncMeasurement = new CPSyncMeasurementData();

            //==========  Get Data Table and Bind ==========//
            DataSet DT_SyncMeasurement = new DataSet();

            ObjSyncMeasurement.PressRunID = Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressRunID"));
            DT_SyncMeasurement = ObjSyncMeasurement.uspGetMeasurementByPressRunID();

            //========== Assign the session data to Grid ===========//
            if (DT_SyncMeasurement != null && DT_SyncMeasurement.Tables[0].Rows.Count > 0)
            { RG_SyncMeasurement.DataSource = DT_SyncMeasurement; RG_SyncMeasurement.DataBind(); }
            else
            { RG_SyncMeasurement.DataSource = null; RG_SyncMeasurement.DataBind(); }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    /// <summary>
    /// This function use in html side for reomve some charecter in datename.
    /// </summary>
    /// <param name="FileName"></param>
    /// <returns></returns>
    public string OriginalFileNameReturn(string FileName)
    {
        try
        {
            if (FileName.Contains("_"))
                return FileName.Substring(0, FileName.LastIndexOf('_')) + FileName.Substring(FileName.LastIndexOf('.'));
            else
                return FileName;
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    /// <summary>
    /// This funtion is use in html side for dispaly isused image in grid.
    /// </summary>
    /// <param name="Status"></param>
    /// <returns></returns>
    public string UsedImageName(string Status)
    {
        try
        {
            if (Convert.ToBoolean(Status))
                return "Images/right-icon.png";

            return "Images/dot.png";
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    #endregion
}
