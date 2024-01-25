using System;
using System.Data;
using System.Web.UI.WebControls;
using Taskero.Common;
using Taskero.Business;
using TaskeroWeb.Models;


namespace TaskeroWeb
{
    public partial class ReportPressMakeReady : PageBase
    {
        #region Page Load Event And Other Control's Event
        int MakeReadyReportID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            LblError.Text = "";

            //========== Default Settings ==========//
            if (IsPostBack == false)
            {
                FillCompanyDropDown(); // Fill Company Drop Down 

                //======= Set Default Date ========//
                TXT_ToDate.Text = DateTime.Now.ToShortDateString();
                TXT_FromDate.Text = DateTime.Now.AddDays(-30).ToShortDateString();
                //================================//

            }
        }


        protected void DDL_Company_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDL_Company.SelectedIndex != 0)
                {
                    CHK_Press.Items.Clear();
                    if (CHK_Press.Items.Count == 0)
                    { PNL_PressChkList.Height = 0; PNL_PressChkList.Visible = false; }
                    else if (CHK_Press.Items.Count >= 5)
                    { PNL_PressChkList.Height = 120; PNL_PressChkList.Visible = true; }
                    else if (CHK_Press.Items.Count <= 2)
                    { PNL_PressChkList.Height = 50; PNL_PressChkList.Visible = true; }
                    else
                    { PNL_PressChkList.Height = 75; PNL_PressChkList.Visible = true; }

                    FillBranch(int.Parse(DDL_Company.SelectedValue));


                }
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }


        private void FillBranch(int CompanyID)
        {
            try
            {
                CHK_Branch.Items.Clear();

                ReportPressMakeReadyData ObjMakeReady = new ReportPressMakeReadyData();
                DataTable dtBranch = new DataTable();

                //==========  Get Data Set and Fill ==========//
                ObjMakeReady.TaskeroUserID = Sessions.UserID;
                ObjMakeReady.CompanyID = CompanyID;
                ObjMakeReady.RoleID = Sessions.RoleID;
                dtBranch = ObjMakeReady.GetBranchFromUserID();

                for (int j = 0; j < dtBranch.Rows.Count; j++)
                {
                    CHK_Branch.Items.Add(new ListItem(dtBranch.Rows[j]["BranchName"].ToString(), dtBranch.Rows[j]["BranchID"].ToString()));
                }


                if (CHK_Branch.Items.Count == 0)
                { PNL_Branch.Height = 0; PNL_Branch.Visible = false; }
                else if (CHK_Branch.Items.Count >= 5)
                { PNL_Branch.Height = 120; PNL_Branch.Visible = true; }
                else if (CHK_Branch.Items.Count <= 2)
                { PNL_Branch.Height = 50; PNL_Branch.Visible = true; }
                else
                { PNL_Branch.Height = 75; PNL_Branch.Visible = true; }

            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }


        protected void CHK_Branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillPressChkList();
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }


        protected void BTN_CreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Validations ==========//
                if (DDL_Company.SelectedIndex == -1)
                { LblError.Text = "Please select a company!!!"; return; }
                if (Convert.ToDateTime(TXT_FromDate.Text.Trim()) > Convert.ToDateTime(TXT_ToDate.Text.Trim()))
                { LblError.Text = "To date must be greter then From date!!!"; return; }
                //=====================================//


                //iterate through branch checkbox list and find branchid as comma seprated values
                string BranchID = "";
                for (int i = 0; i < CHK_Branch.Items.Count; i++)
                {
                    if (CHK_Branch.Items[i].Selected == true)
                    {
                        if (BranchID == "")
                            BranchID = BranchID + CHK_Branch.Items[i].Value;
                        else
                            BranchID = BranchID + "," + CHK_Branch.Items[i].Value;
                    }
                }



                //iterate through press checkbox list and find presses that are selected as comma separated values
                string CPDeviceID = "";
                string strPressName = string.Empty;
                for (int i = 0; i < CHK_Press.Items.Count; i++)
                {
                    if (CHK_Press.Items[i].Selected == true)
                    {
                        if (CPDeviceID == "")
                            CPDeviceID = CPDeviceID + CHK_Press.Items[i].Value;
                        else
                            CPDeviceID = CPDeviceID + "," + CHK_Press.Items[i].Value;

                        strPressName += "<tr class='ITEMSTYLE'>";
                        strPressName += "<td width='5%' align='left'>&nbsp;</td>";
                        strPressName += "<td width='95%'>" + CHK_Press.Items[i].Text.Trim() + "</td>";
                        strPressName += "</tr>";

                    }
                }


                //========== Validations ==========//            
                if (BranchID.Trim() == "")
                { LblError.Text = "Please select a branch!!!"; return; }
                if (CPDeviceID.Trim() == "")
                { LblError.Text = "Please select a press!!!"; return; }
                //=====================================//

                //==========  Declaring Objects ==========//
                ReportPressMakeReadyData ObjMakeReady = new ReportPressMakeReadyData();
                DataSet DsMakeReadySummary = new DataSet();

                //==========  Get Data Set and Fill ==========//
                ObjMakeReady.CompanyID = int.Parse(DDL_Company.SelectedValue.ToString());
                ObjMakeReady.BranchID = BranchID;
                ObjMakeReady.CPDeviceID = CPDeviceID;
                ObjMakeReady.JobName = txtJobName.Text.Trim();
                ObjMakeReady.StartDate = Convert.ToDateTime(TXT_FromDate.Text.Trim());
                ObjMakeReady.EndDate = Convert.ToDateTime(TXT_ToDate.Text.Trim());

                //==== Set From Time ====/
                int StartHours = int.Parse(DDL_FromHours.SelectedValue.ToString()) + int.Parse(RBTN_FromAMPM.SelectedValue.ToString());
                DateTime DTStartTime = Convert.ToDateTime("01/01/1900 " + StartHours.ToString() + ":" + DDL_FromMins.SelectedValue.ToString());
                ObjMakeReady.StartTime = DTStartTime;

                //==== Set To Time ====/
                int EndHours = int.Parse(DDL_ToHours.SelectedValue.ToString()) + int.Parse(RBTN_ToAMPM.SelectedValue.ToString());
                DateTime DTEndTime = Convert.ToDateTime("01/01/1900 " + EndHours.ToString() + ":" + DDL_ToMins.SelectedValue.ToString());
                ObjMakeReady.EndTime = DTEndTime;



                MakeReadyReportID = ObjMakeReady.usp_MakeReadySearchResult();

                if (MakeReadyReportID == 0)
                { LblError.Text = "There is no data for this Search Criteria!!!"; return; }

                //check resultset returned MakeReadyReportID contains multiple branches
                DataTable dtBranchList = new DataTable();

                ReportPressMakeReadyData ObjMRData = new ReportPressMakeReadyData();
                ObjMRData.MakeReadyReportID = MakeReadyReportID;
                dtBranchList = ObjMRData.usp_MakeReadyGetBranchIDs();


                if (dtBranchList != null && dtBranchList.Rows.Count > 0)
                {
                    //=========== Set Value of  Report Parameters in Report Templete ======================================//
                    LTRL_DateRange.Text = TXT_FromDate.Text.Trim() + " - " + TXT_ToDate.Text.Trim();
                    LTRL_TimeRange.Text = "Start " + DDL_FromHours.SelectedValue.ToString() + " : " + DDL_FromMins.SelectedValue.ToString() + " " + RBTN_FromAMPM.SelectedItem.Text.Trim() + " End " + DDL_ToHours.SelectedValue.ToString() + " : " + DDL_ToMins.SelectedValue.ToString() + " " + RBTN_ToAMPM.SelectedItem.Text.Trim();
                    LTRL_CompanyName.Text = DDL_Company.SelectedItem.Text.Trim();
                    LTRL_JobRunName.Text = txtJobName.Text.Trim();
                    //LTRL_PressName.Text = CHK_Press.SelectedItem.Text.Trim() + "...";
                    LTRL_PressName.Text = strPressName;
                    //=====================================================================================================//

                    //=== Set Blank To Detail Lable =======//
                    LBL_CompanyDetail.Text = string.Empty;
                    LBL_BranchDetail.Text = string.Empty;
                    //=====================================//

                    if (dtBranchList.Rows.Count > 1)
                    {

                        //display both branch and company summary                
                        TRCompanySummary.Visible = true; // Visible true Company Summary Detail
                        FillCompanyDetail();    // Fill Company Summary Detail
                        FillBranchDetail(dtBranchList); // Fill Branch Summary Detail
                    }
                    else
                    {
                        TRCompanySummary.Visible = false; // Visible false Company Summary Detail
                        FillBranchDetail(dtBranchList); // Fill Branch Summary Detail
                    }
                }



                //===== Show Report in model popup =====//
                programmaticModalPopup.Show();

            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }

        protected void BTN_CreatePDF_Click(object sender, EventArgs e)
        {
            try
            {
                //==========  Declaring Dynamic Table For Creating PDF And Pass To MakePDF ==========//
                DataSet Ds = new DataSet();
                DataTable myDt = new DataTable();
                myDt.TableName = "MakeReadyDetail";
                DataColumn dcDateRange = new DataColumn("DateRange");
                myDt.Columns.Add(dcDateRange);
                DataColumn dcTimeRange = new DataColumn("TimeRange");
                myDt.Columns.Add(dcTimeRange);
                DataColumn dcCompanyName = new DataColumn("CompanyName");
                myDt.Columns.Add(dcCompanyName);
                DataColumn dcBranchName = new DataColumn("BranchName");
                myDt.Columns.Add(dcBranchName);
                DataColumn dcJobRunName = new DataColumn("JobRunName");
                myDt.Columns.Add(dcJobRunName);
                DataColumn dcPressName = new DataColumn("PressName");
                myDt.Columns.Add(dcPressName);
                DataColumn dcCompanySummary = new DataColumn("CompanySummary");
                myDt.Columns.Add(dcCompanySummary);
                DataColumn dcBranchSummary = new DataColumn("BranchSummary");
                myDt.Columns.Add(dcBranchSummary);
                //=======================================================================================//

                //================= Insert Row in to Dynamic Table ======================================//
                DataRow dr = myDt.NewRow();
                dr["DateRange"] = LTRL_DateRange.Text.Trim();
                dr["TimeRange"] = LTRL_TimeRange.Text.Trim();
                dr["CompanyName"] = LTRL_CompanyName.Text.Trim();
                dr["BranchName"] = LTRL_BranchName.Text.Trim();
                dr["JobRunName"] = LTRL_JobRunName.Text.Trim();
                dr["PressName"] = LTRL_PressName.Text.Trim();
                dr["CompanySummary"] = LBL_CompanyDetail.Text.Trim();
                dr["BranchSummary"] = LBL_BranchDetail.Text.Trim();

                myDt.Rows.Add(dr);
                Ds.Tables.Add(myDt);

                //=======================================================================================//

                //=== Call Create PDF Function =================//
                CreateReportPDF(Ds);
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }

        }
        #endregion


        #region Fill Report Detail
        private void CreateReportPDF(DataSet Ds)
        {
            try
            {
                // === Set all parameters to generate pdf file === //
                ReportPDF objReportPDF = new ReportPDF();
                objReportPDF.ReportType = (int)Enums.enumReportType.ReportPressMakeReady;

                objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
                objReportPDF.PDF_NAME = Sessions.UserID + "_PressMakeReadyReport.PDF";
                objReportPDF.DS_Common = Ds;
                objReportPDF.MakePDF();

                // === After generat and save PDF file, Download it at a time === //
                CommonFunctions.DownlaodFile(objReportPDF.PDFPath, objReportPDF.PDF_NAME, objReportPDF.PDF_NAME);
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }

        private void FillCompanyDetail()
        {
            try
            {
                string strCompanyDetail;
                //==========  Declaring Objects ==========//
                DataSet DsMakeReadyCompanySummary = new DataSet();
                ReportPressMakeReadyData ObjMRData = new ReportPressMakeReadyData();
                ObjMRData.MakeReadyReportID = MakeReadyReportID;
                DsMakeReadyCompanySummary = ObjMRData.usp_MakeReadyCompanySummary();
                //===========================================//


                if (DsMakeReadyCompanySummary != null && DsMakeReadyCompanySummary.Tables.Count > 0)
                {

                    //================= Fill company detail form Dataset table=================//
                    strCompanyDetail = "<tr class='ITEMSTYLE'>";
                    strCompanyDetail += "<td style='width:15%'>" + DDL_Company.SelectedItem.Text.Trim() + "</td>";
                    if (DsMakeReadyCompanySummary.Tables[0].Rows[0]["NumOfJobs"] != null && DsMakeReadyCompanySummary.Tables[0].Rows[0]["NumOfJobs"].ToString().Trim() != "")
                        strCompanyDetail += "<td align='center'>" + DsMakeReadyCompanySummary.Tables[0].Rows[0]["NumOfJobs"].ToString() + "</td>";
                    else
                        strCompanyDetail += "<td align='center'>----</td>";

                    if (DsMakeReadyCompanySummary.Tables[0].Rows[0]["AvgMRSheetPerJob"] != null && DsMakeReadyCompanySummary.Tables[0].Rows[0]["AvgMRSheetPerJob"].ToString().Trim() != "")
                        strCompanyDetail += "<td align='center'>" + DsMakeReadyCompanySummary.Tables[0].Rows[0]["AvgMRSheetPerJob"].ToString() + "</td>";
                    else
                        strCompanyDetail += "<td align='center'>----</td>";

                    if (DsMakeReadyCompanySummary.Tables[0].Rows[0]["TotalMRSheets"] != null && DsMakeReadyCompanySummary.Tables[0].Rows[0]["TotalMRSheets"].ToString().Trim() != "")
                        strCompanyDetail += "<td align='center'>" + DsMakeReadyCompanySummary.Tables[0].Rows[0]["TotalMRSheets"].ToString() + "</td>";
                    else
                        strCompanyDetail += "<td align='center'>----</td>";

                    if (DsMakeReadyCompanySummary.Tables[0].Rows[0]["AvgMRMinues"] != null && DsMakeReadyCompanySummary.Tables[0].Rows[0]["AvgMRMinues"].ToString().Trim() != "")
                        strCompanyDetail += "<td align='center'>" + DsMakeReadyCompanySummary.Tables[0].Rows[0]["AvgMRMinues"].ToString() + "</td>";
                    else
                        strCompanyDetail += "<td align='center'>----</td>";

                    if (DsMakeReadyCompanySummary.Tables[0].Rows[0]["TotalMRMinues"] != null && DsMakeReadyCompanySummary.Tables[0].Rows[0]["TotalMRMinues"].ToString().Trim() != "")
                        strCompanyDetail += "<td align='center'>" + DsMakeReadyCompanySummary.Tables[0].Rows[0]["TotalMRMinues"].ToString() + "</td>";
                    else
                        strCompanyDetail += "<td align='center'>----</td>";

                    if (DsMakeReadyCompanySummary.Tables[0].Rows[0]["AvgError"] != null && DsMakeReadyCompanySummary.Tables[0].Rows[0]["AvgError"].ToString().Trim() != "")
                        strCompanyDetail += "<td align='center'>" + DsMakeReadyCompanySummary.Tables[0].Rows[0]["AvgError"].ToString() + "</td>";
                    else
                        strCompanyDetail += "<td align='center'>----</td>";

                    if (DsMakeReadyCompanySummary.Tables[0].Rows[0]["AvgDeltaE"] != null && DsMakeReadyCompanySummary.Tables[0].Rows[0]["AvgDeltaE"].ToString().Trim() != "")
                        strCompanyDetail += "<td align='center'>" + DsMakeReadyCompanySummary.Tables[0].Rows[0]["AvgDeltaE"].ToString() + "</td>";
                    else
                        strCompanyDetail += "<td align='center'>----</td>";

                    strCompanyDetail += "</tr>";
                    LBL_CompanyDetail.Text = strCompanyDetail;
                    //=====================================================================================================================//
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillBranchDetail(DataTable dtBranch)
        {
            try
            {
                string strBranchDetail = ""; ;
                if (dtBranch != null && dtBranch.Rows.Count > 0)
                {
                    for (int i = 0; i < dtBranch.Rows.Count; i++) // For loop for multipal Branchs.
                    {
                        //==========  Declaring Objects ==========//
                        DataSet DsMakeReadyBranchSummary = new DataSet();
                        ReportPressMakeReadyData ObjMRData = new ReportPressMakeReadyData();
                        ObjMRData.MakeReadyReportID = MakeReadyReportID;
                        ObjMRData.BranchID = dtBranch.Rows[i]["BranchID"].ToString();
                        DsMakeReadyBranchSummary = ObjMRData.usp_MakeReadyBranchSummary();
                        //============================================//
                        if (DsMakeReadyBranchSummary != null && DsMakeReadyBranchSummary.Tables.Count > 0)
                        {
                            //================= Fill company detail form Dataset table=================//
                            strBranchDetail += "<tr class='ITEMSTYLE'>";

                            //=================== Set Value of  Report Parameters in Report Templete ==============================//
                            if (LTRL_BranchName.Text.Trim() == "")
                                LTRL_BranchName.Text = dtBranch.Rows[i]["BranchName"].ToString();
                            else
                            {
                                if (LTRL_BranchName.Text.Contains(dtBranch.Rows[i]["BranchName"].ToString()) == false)
                                {
                                    LTRL_BranchName.Text = LTRL_BranchName.Text.Trim() + ", " + dtBranch.Rows[i]["BranchName"].ToString();
                                }
                            }
                            //=====================================================================================================//

                            strBranchDetail += "<td style='width:15%'>" + dtBranch.Rows[i]["BranchName"].ToString() + "</td>";
                            if (DsMakeReadyBranchSummary.Tables[0].Rows[0]["NumOfJobs"] != null && DsMakeReadyBranchSummary.Tables[0].Rows[0]["NumOfJobs"].ToString().Trim() != "")
                                strBranchDetail += "<td align='center'>" + DsMakeReadyBranchSummary.Tables[0].Rows[0]["NumOfJobs"].ToString() + "</td>";
                            else
                                strBranchDetail += "<td align='center'>----</td>";

                            if (DsMakeReadyBranchSummary.Tables[0].Rows[0]["AvgMRSheetPerJob"] != null && DsMakeReadyBranchSummary.Tables[0].Rows[0]["AvgMRSheetPerJob"].ToString().Trim() != "")
                                strBranchDetail += "<td align='center'>" + DsMakeReadyBranchSummary.Tables[0].Rows[0]["AvgMRSheetPerJob"].ToString() + "</td>";
                            else
                                strBranchDetail += "<td align='center'>----</td>";

                            if (DsMakeReadyBranchSummary.Tables[0].Rows[0]["TotalMRSheets"] != null && DsMakeReadyBranchSummary.Tables[0].Rows[0]["TotalMRSheets"].ToString().Trim() != "")
                                strBranchDetail += "<td align='center'>" + DsMakeReadyBranchSummary.Tables[0].Rows[0]["TotalMRSheets"].ToString() + "</td>";
                            else
                                strBranchDetail += "<td align='center'>----</td>";

                            if (DsMakeReadyBranchSummary.Tables[0].Rows[0]["AvgMRMinues"] != null && DsMakeReadyBranchSummary.Tables[0].Rows[0]["AvgMRMinues"].ToString().Trim() != "")
                                strBranchDetail += "<td align='center'>" + DsMakeReadyBranchSummary.Tables[0].Rows[0]["AvgMRMinues"].ToString() + "</td>";
                            else
                                strBranchDetail += "<td align='center'>----</td>";

                            if (DsMakeReadyBranchSummary.Tables[0].Rows[0]["TotalMRMinues"] != null && DsMakeReadyBranchSummary.Tables[0].Rows[0]["TotalMRMinues"].ToString().Trim() != "")
                                strBranchDetail += "<td align='center'>" + DsMakeReadyBranchSummary.Tables[0].Rows[0]["TotalMRMinues"].ToString() + "</td>";
                            else
                                strBranchDetail += "<td align='center'>----</td>";

                            if (DsMakeReadyBranchSummary.Tables[0].Rows[0]["AvgError"] != null && DsMakeReadyBranchSummary.Tables[0].Rows[0]["AvgError"].ToString().Trim() != "")
                                strBranchDetail += "<td align='center'>" + DsMakeReadyBranchSummary.Tables[0].Rows[0]["AvgError"].ToString() + "</td>";
                            else
                                strBranchDetail += "<td align='center'>----</td>";

                            if (DsMakeReadyBranchSummary.Tables[0].Rows[0]["AvgDeltaE"] != null && DsMakeReadyBranchSummary.Tables[0].Rows[0]["AvgDeltaE"].ToString().Trim() != "")
                                strBranchDetail += "<td align='center'>" + DsMakeReadyBranchSummary.Tables[0].Rows[0]["AvgDeltaE"].ToString() + "</td>";
                            else
                                strBranchDetail += "<td align='center'>----</td>";

                            strBranchDetail += "</tr>";
                            //=====================================================================================================================//
                        }
                    }

                }
                LBL_BranchDetail.Text = strBranchDetail;


            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Validations
        protected void TXT_DateText(object sender, EventArgs e)
        {
            //======= Check entered Date is valid or not ========= //
            if (Validations.IsDateTime(((TextBox)sender).Text) == false)
            { LblError.Text = "Please enter a valid date!"; ((TextBox)sender).Text = ""; return; }
            //===================================================//
        }
        #endregion

        #region Search Criteria Fill Method    

        //populate company dropdown menu
        //if user is taskero admin then only show option to select other companies
        //else preselect company based on the user's company  
        private void FillCompanyDropDown()
        {
            try
            {
                //==========  Declaring Objects ==========//
                ReportPressMakeReadyData ObjMakeReady = new ReportPressMakeReadyData();
                DataSet DsComapny = new DataSet();

                //==========  Get Data Set and Fill ==========//
                ObjMakeReady.TaskeroUserID = Sessions.UserID;
                ObjMakeReady.RoleID = Sessions.RoleID;
                DsComapny = ObjMakeReady.GetComapnyFromUserID();

                //==========  Fill Company DDL ==========//
                DDL_Company.DataSource = DsComapny.Tables[0];
                DDL_Company.DataTextField = "CompanyName";
                DDL_Company.DataValueField = "CompanyID";
                DDL_Company.DataBind();
                if (DsComapny.Tables[0].Rows.Count > 1)
                {  //if user (i.e. taskeroadmin can can access more than one company show dropdown with select
                    DDL_Company.Items.Insert(0, new ListItem("-- Select Company --", "-1"));
                }
                else if (DsComapny.Tables[0].Rows.Count != 0) //preselect the company but check if no company found
                {
                    DDL_Company.SelectedValue = DsComapny.Tables[0].Rows[0]["CompanyID"].ToString();
                    FillBranch(int.Parse(DDL_Company.SelectedValue));
                }
                else // No Comapny found
                {
                    DDL_Company.Items.Insert(0, new ListItem("-- Select Company --", "-1"));
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //populate press checkbox list based on the branch selection
        private void FillPressChkList()
        {
            try
            {
                CHK_Press.Items.Clear();
                string BranchIDs = "";
                for (int i = 0; i < CHK_Branch.Items.Count; i++)
                {
                    if (CHK_Branch.Items[i].Selected == true)
                    {
                        if (BranchIDs == "")
                            BranchIDs = BranchIDs + CHK_Branch.Items[i].Value;
                        else
                            BranchIDs = BranchIDs + "," + CHK_Branch.Items[i].Value;
                    }
                }

                //==========  Declaring Objects ==========//
                ReportPressMakeReadyData ObjMakeReady = new ReportPressMakeReadyData();
                DataTable dtPress = new DataTable();

                //==========  Get Data Set and Fill ==========//            
                ObjMakeReady.BranchID = BranchIDs;
                dtPress = ObjMakeReady.GetPressDevicesFromBranch();



                CHK_Press.DataTextField = "PressName";
                CHK_Press.DataValueField = "CPDeviceID";
                CHK_Press.DataSource = dtPress;
                CHK_Press.DataBind();


                if (CHK_Press.Items.Count == 0)
                { PNL_PressChkList.Height = 0; PNL_PressChkList.Visible = false; }
                else if (CHK_Press.Items.Count >= 5)
                { PNL_PressChkList.Height = 120; PNL_PressChkList.Visible = true; }
                else if (CHK_Press.Items.Count <= 2)
                { PNL_PressChkList.Height = 48; PNL_PressChkList.Visible = true; }
                else
                { PNL_PressChkList.Height = 75; PNL_PressChkList.Visible = true; }


            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion


    }

}