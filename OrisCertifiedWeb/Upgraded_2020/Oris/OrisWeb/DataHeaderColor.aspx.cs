using Oris.Business;
using Oris.Common;
using OrisWeb.Models;
using System;
using System.Data;

namespace OrisWeb
{
    public partial class DataHeaderColor : PageBase
    {
        #region Page Load And Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Clear Error =========//
                LblError.Text = "";

                //========== Assign Css ==========//            
                LtrlCSS.Text = @"<link href='CSS/" + Sessions.Css + "' rel='stylesheet' type='text/css' />";

                //========== Set Default Grid Settings =========//
                CommonFunctions.SetDataGridDefaultSettings(DG1);
                DG1.AllowPaging = false;
                DG1.ShowHeader = false;
                DG1.Style.Add("width", "100%");

                //========== Set Default Grid Settings =========//
                CommonFunctions.SetDataGridDefaultSettings(DG2);
                DG2.AllowPaging = false;
                DG2.AllowSorting = false;
                DG2.AutoGenerateColumns = true;
                DG2.Style.Add("width", "100%");

                if (!IsPostBack)
                {
                    //========== Get Guid =========//
                    if (Request.QueryString["CPMeasurementGUID"] != null)
                    { LTRL_CPMeasurementGUID.Text = Request.QueryString["CPMeasurementGUID"].ToString(); }

                    //========== Get Data Table and Bind Grid =========//
                    FillGrid();


                    if (DG2.Items.Count == 0) { CommonFunctions.PageRedirect("DataHeaderPlate", "CPMeasurementGUID=" + LTRL_CPMeasurementGUID.Text); }
                }
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        #endregion

        #region Methods and Functions
        private void FillGrid()
        {
            try
            {
                //========== Get Data ==========//
                CPPatchDataLabData objCPPatchDataLab = new CPPatchDataLabData();
                objCPPatchDataLab.MeasurementGUID = LTRL_CPMeasurementGUID.Text;
                DataSet DSet = objCPPatchDataLab.uspGetColorCPVMeasurement();

                if (DSet.Tables[0].Rows.Count > 0)
                {

                    #region Fill DG 1
                    //========== Bind a datatable for DG1 ==========//
                    DataTable DT1 = new DataTable();
                    DT1.Columns.Add("ID");
                    DT1.Columns.Add("FieldName");
                    DT1.Columns.Add("Value");

                    for (int i = 0; i < DSet.Tables[0].Columns.Count; i = i + 1)
                    {
                        //========== Create DataRow ==========//
                        DataRow Dr1 = DT1.NewRow();
                        Dr1["ID"] = Convert.ToString(i + 1);
                        Dr1["FieldName"] = DSet.Tables[0].Columns[i].ColumnName;
                        Dr1["Value"] = DSet.Tables[0].Rows[0][i].ToString();
                        DT1.Rows.Add(Dr1);
                    }

                    DG1.DataSource = DT1;
                    DG1.DataBind();
                    #endregion

                    #region Fill DG 2
                    DG2.DataSource = DSet.Tables[1];
                    DG2.DataBind();
                    #endregion
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

    }
}