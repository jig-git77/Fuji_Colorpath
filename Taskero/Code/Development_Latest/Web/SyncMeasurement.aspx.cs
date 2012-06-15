using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fuji.Business;
using System.Data;
using Telerik.WebControls;


public partial class SyncMeasurement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {

                if (Fuji.Common.Sessions.GetSession("SelectedProjectID") != null)
                {
                    this.hdProjectID.Value = Fuji.Common.Sessions.GetSession("SelectedProjectID").ToString();
                    this.hdChartTypeID.Value = Fuji.Common.Sessions.GetSession("SelectedChartTypeID").ToString();
                }
                if (Fuji.Common.Sessions.GetSession("SelectedProjectTypeID") != null)
                {
                    this.hiddenProjectTypeID.Value = Fuji.Common.Sessions.GetSession("SelectedProjectTypeID").ToString();
                    this.hiddenPressRunID.Value = Fuji.Common.Sessions.GetSession("SelectedPressRunID").ToString();
                }
            
                    BindSyncMeasurementData();
            }
        }   
        catch (Exception exc)
        {
            throw exc;
        }
    }


    protected void btnBindGrid_Click(object sender, EventArgs e)
    {
        BindSyncMeasurementData();
    }
    protected void RG_SyncMeasurement_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            //========== Check CommandName for the Edit Event ===========//
            if (e.CommandName == "Delete")
            {
                CPSyncMeasurementData ObjSyncMeasurement = new CPSyncMeasurementData();
                ObjSyncMeasurement.MeasurementID = Convert.ToInt16(e.Item.Cells[2].Text); 
                ObjSyncMeasurement.uspDeleteMeasurement();
                BindSyncMeasurementData();
                
            }
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }

    #region Methods
    /// <summary>
    /// Used to bind the Sync Press Grid
    /// </summary>
    private void BindSyncMeasurementData()
    {
        DataView DV = new DataView();
        try
        {
            //==========  Declaring Objects ==========//
            CPSyncMeasurementData ObjSyncMeasurement = new CPSyncMeasurementData();

            //==========  Get Data Table and Bind ==========//
            DataSet DT_SyncMeasurement = new DataSet();

            ObjSyncMeasurement.PressRunID = Convert.ToInt16(Fuji.Common.Sessions.GetSession("SelectedPressRunID"));
            DT_SyncMeasurement = ObjSyncMeasurement.uspGetMeasurementByPressRunID();

            DV = DT_SyncMeasurement.Tables[0].DefaultView;
            DV.Sort = "UploadDateTime DESC";

            //========== Assign the session data to Grid ===========//
            if (DT_SyncMeasurement != null && DT_SyncMeasurement.Tables[0].Rows.Count > 0)
            { RG_SyncMeasurement.DataSource = DV; RG_SyncMeasurement.DataBind(); }
            else
            { RG_SyncMeasurement.DataSource = null; RG_SyncMeasurement.DataBind(); }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }   
  
    #endregion
}