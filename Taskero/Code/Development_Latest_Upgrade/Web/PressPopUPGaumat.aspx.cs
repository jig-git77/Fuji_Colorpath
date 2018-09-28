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
using Dundas.Charting.WebControl;
#endregion

public partial class PressPopUPGaumat : PageBase
{
    #region Page Load and Functions
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Error ==========//
            LblError.Text = "";

            //========== Assign Variables ==========//
            LTRL_JobID.Text = Convert.ToString(Request.QueryString["JobID"]);
            LTRL_DongleID.Text = Convert.ToString(Request.QueryString["DongleID"]);
            if (Request.QueryString["SheetNumber"] != null){ LTRL_SheetNumber.Text = Request.QueryString["SheetNumber"].ToString(); }
            

            //========== Set Keys ==========//
            LTRL_KEY.Text = LTRL_JobID.Text + LTRL_DongleID.Text + "-Press-DSet-ColorChars";
            if (Request.QueryString["SheetNumber"] != null) { LTRL_KEY.Text = LTRL_JobID.Text + LTRL_DongleID.Text + LTRL_SheetNumber.Text + "-Press-Sheet-DSet-ColorChars"; }            

            if (Fuji.Common.Sessions.GetSession(LTRL_KEY.Text) != null )
            {
                DataSet DSet = new DataSet();
                DSet = (DataSet)Fuji.Common.Sessions.GetSession(LTRL_KEY.Text); 

                //========== Variable to Change the table Index ==========//
                int _TableID = 0;

                #region Tables[0] = Gaumat
                FillGaumat(DSet.Tables[_TableID]);
                #endregion
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    private void FillGaumat(DataTable dtGamut)
    {
        try
        {
            #region Plotting the Chart
            dtGamut.DefaultView.RowFilter = "Rank = 8";
            DataView DV = dtGamut.DefaultView;
            if (DV.Count > 0)
            {
                int i = 0;
                CHRT_GAMUT.Series[2].Points.AddXY(Convert.ToDouble(DV[0]["Target_A"]), Convert.ToDouble(DV[i]["Target_B"]));
                CHRT_GAMUT.Series[2].Type = SeriesChartType.FastPoint;
                CHRT_GAMUT.Series[2].Points[0].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Target(a) : " + DV[i]["Target_A"].ToString() + ", Target(b) : " + DV[i]["Target_B"].ToString();
                CHRT_GAMUT.Series[2].Points[0].Href = "#";

                CHRT_GAMUT.Series[3].Points.AddXY(Convert.ToDouble(DV[i]["Actual_A"]), Convert.ToDouble(DV[i]["Actual_B"]));
                CHRT_GAMUT.Series[3].Type = SeriesChartType.FastPoint;
                CHRT_GAMUT.Series[3].Points[0].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Actual(a) : " + DV[i]["Actual_A"].ToString() + ", Actual(b) : " + DV[i]["Actual_B"].ToString();
                CHRT_GAMUT.Series[3].Points[0].Href = "#";
            }
            #endregion

            #region Other Points
            dtGamut.DefaultView.RowFilter = "Rank <> 8";
            DV = dtGamut.DefaultView;
            for (int i = 0; i < DV.Count; i = i + 1)
            {
                CHRT_GAMUT.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["Target_A"]), Convert.ToDouble(DV[i]["Target_B"]));
                CHRT_GAMUT.Series[0].Type = SeriesChartType.Line;
                CHRT_GAMUT.Series[0].Points[i].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Target(a) : " + Convert.ToDouble(DV[i]["Target_A"]).ToString("N2") + ", Target(b) : " + Convert.ToDouble(DV[i]["Target_B"]).ToString("N2");
                CHRT_GAMUT.Series[0].Points[i].Href = "#";

                CHRT_GAMUT.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["Actual_A"]), Convert.ToDouble(DV[i]["Actual_B"]));
                CHRT_GAMUT.Series[1].Type = SeriesChartType.Line;
                CHRT_GAMUT.Series[1].Points[i].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Actual(a) : " + Convert.ToDouble(DV[i]["Actual_A"]).ToString("N2") + ", Actual(b) : " + Convert.ToDouble(DV[i]["Actual_B"]).ToString("N2");
                CHRT_GAMUT.Series[1].Points[i].Href = "#";
            }
            if (DV.Count > 1)
            {
                int i = 0;
                CHRT_GAMUT.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["Target_A"]), Convert.ToDouble(DV[i]["Target_B"]));
                CHRT_GAMUT.Series[0].Type = SeriesChartType.Line;
                CHRT_GAMUT.Series[0].Points[DV.Count].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Target(a) : " + Convert.ToDouble(DV[i]["Target_A"]).ToString("N2") + ", Target(b) : " + Convert.ToDouble(DV[i]["Target_B"]).ToString("N2");
                CHRT_GAMUT.Series[0].Points[DV.Count].Href = "#";

                CHRT_GAMUT.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["Actual_A"]), Convert.ToDouble(DV[i]["Actual_B"]));
                CHRT_GAMUT.Series[1].Type = SeriesChartType.Line;
                CHRT_GAMUT.Series[1].Points[DV.Count].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Actual(a) : " + Convert.ToDouble(DV[i]["Actual_A"]).ToString("N2") + ", Actual(b) : " + Convert.ToDouble(DV[i]["Actual_B"]).ToString("N2");
                CHRT_GAMUT.Series[1].Points[DV.Count].Href = "#";
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
