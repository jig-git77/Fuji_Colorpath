using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Business;
using System.Configuration;

namespace ProblemSpecs
{
    public partial class _15Minutes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {


            if (IsPostBack == false)
            {
                
                txtLocation.Attributes.Add("onfocus", "clearText(this)");
                txtIsNotLocation.Attributes.Add("onfocus", "clearText(this)");
                txtOntheObject.Attributes.Add("onfocus", "clearText(this)");
                txtNumOfObjects.Attributes.Add("onfocus", "clearText(this)");
                txtHowManyDeviations.Attributes.Add("onfocus", "clearText(this)");
                txtDeviationSize.Attributes.Add("onfocus", "clearText(this)");
                txtIsNotNumOFObjects.Attributes.Add("onfocus", "clearText(this)");
                txtIsNotFirst.Attributes.Add("onfocus", "clearText(this)");

                txtFirst.Attributes.Add("onfocus", "clearText(this)");
                txtSince.Attributes.Add("onfocus", "clearText(this)");
                txtInHistory.Attributes.Add("onfocus", "clearText(this)");
                txtLifeCycleIsNot.Attributes.Add("onfocus", "clearText(this)");

                txtIsNotProblemDesc.Attributes.Add("onfocus", "clearText(this)");
                txtIsNotObject.Attributes.Add("onfocus", "clearText(this)");
                txtTRCNotes.Attributes.Add("onfocus", "clearText(this)");
                txtIsNotProblemDesc.Attributes.Add("onfocus", "clearText(this)");

                //enable TRC notes only for Phone Support; disable if request came from Call Closing
                if (  Request.QueryString["SessionID"] != null && Request.QueryString["SessionID"] == "99912399911")
                {
                   txtTRCNotes.Enabled = false;         

                }
                
            

                LoadData();

            }
            }
            catch (Exception Exc)
            {
                if (ConfigurationManager.AppSettings["SendErrorEmail"].ToString() == "Y")
                { 
                     FGSU_TOOLS.CommonFunctions.SendEmail("FTSS@Fujifilmgs.com","Fujifilmgs Technical Serv","cshah@fujifilm.com",null,Exc.ToString(),"KT 15Minutes Web Error",null,"HIGH","text/plain");
                }
            }


        }


        private void LoadData()
        {

            try
            {

                if (Request.QueryString["OrderID"] == null) return;

                string orderid = Request.QueryString["OrderID"].ToString();

                ProblemSpec objSpec = new ProblemSpec (orderid);

                objSpec.LoadData();
                BindControls(objSpec);           
                               
            }
            catch (Exception Exc)
            {
                throw Exc;
            }

        }



        private void BindControls(ProblemSpec SpectObj)
        {

            try
            {
                txtProblemStatement.Text = SpectObj.ProblemStat;
                lblOrderID.Text = SpectObj.Order_id;
                txtObject.Text = SpectObj.mObject;
                txtIsNotObject.Text = SpectObj.ObjectIsNot;
                txtProblemDesc.Text = SpectObj.Deviation;
                txtIsNotProblemDesc.Text = SpectObj.DeviationIsNot;
                txtLocation.Text = SpectObj.Location;
                txtIsNotLocation.Text = SpectObj.LocationIsNot;
                txtOntheObject.Text = SpectObj.OntheObject;
                txtFirst.Text = SpectObj.WhenFirst;
                txtIsNotFirst.Text = SpectObj.WhenFirstNot;
                txtSince.Text = SpectObj.SinceWhen;
                txtInHistory.Text = SpectObj.LifeCycle;
                txtLifeCycleIsNot.Text = SpectObj.LifeCycleIsNot;
                txtNumOfObjects.Text = SpectObj.NumOfObject;
                txtIsNotNumOFObjects.Text = SpectObj.NumOfObjectsIsNot;
                txtDeviationSize.Text = SpectObj.SizeOfDeviation;
                txtHowManyDeviations.Text = SpectObj.NumOfDeviation;

                
                if (SpectObj.Trend == 0)
                {
                    ddlTrend.SelectedIndex = 0;
                } 
                else if (SpectObj.Trend == 1)
                {
                    ddlTrend.SelectedIndex = 1;
                }
                else if (SpectObj.Trend ==2)
                {
                    ddlTrend.SelectedIndex =2;
                }
                else if (SpectObj.Trend == 3)
                {
                    ddlTrend.SelectedIndex = 3;
                }
            
  
                txtTRCNotes.Text = SpectObj.TRCNotes;


           

            }
            catch (Exception Exc)
            {
                throw Exc;
            }

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //check entry for service call alreay exist           
                SaveData();
            }
            catch (Exception Exc)
            {
                throw Exc;
            }

        }


        private void SaveData()
        {

            ProblemSpec objSpec = new ProblemSpec (Request.QueryString["OrderID"].ToString());
            try
            {
               
                if (txtObject.Text == String.Empty) lblErrorMessage.Text = "An Error Occured: you must fill out OBJECT  field.";

                //update
                if (txtProblemDesc.Text != "<Need more data>" && txtProblemDesc.Text != "") objSpec.mObject = txtObject.Text;
                if (txtProblemDesc.Text != "<Need more data>" && txtProblemDesc.Text != "" ) objSpec.Deviation = txtProblemDesc.Text;
                if (txtProblemStatement.Text != "<Need more data>" && txtProblemStatement.Text != "" ) objSpec.ProblemStat = txtProblemStatement.Text;
                if (txtIsNotObject.Text != "<Need more data>" && txtIsNotObject.Text != "") objSpec.ObjectIsNot = txtIsNotObject.Text;
                if (txtIsNotProblemDesc.Text != "<Need more data>" && txtIsNotProblemDesc.Text != "" ) objSpec.DeviationIsNot = txtIsNotProblemDesc.Text;
                


                //where
                if (txtLocation.Text != "<Need more data>" && txtLocation.Text != "") objSpec.Location = txtLocation.Text;
                if (txtIsNotLocation.Text != "<Need more data>" && txtIsNotLocation.Text != "") objSpec.LocationIsNot = txtIsNotLocation.Text;
                if (txtOntheObject.Text != "<Need more data>" && txtOntheObject.Text != "" ) objSpec.OntheObject = txtOntheObject.Text;


                //when
                if (txtFirst.Text != "<Need more data>" && txtFirst.Text != "" ) objSpec.WhenFirst = txtFirst.Text;
                if (txtIsNotFirst.Text != "<Need more data>" && txtIsNotFirst.Text != "") objSpec.WhenFirstNot = txtIsNotFirst.Text;
                if (txtSince.Text != "<Need more data>" && txtSince.Text != "") objSpec.SinceWhen = txtSince.Text;
                if (txtInHistory.Text != "<Need more data>" && txtInHistory.Text != "") objSpec.LifeCycle = txtInHistory.Text;
                if (txtLifeCycleIsNot.Text != "<Need more data>" && txtLifeCycleIsNot.Text != "") objSpec.LifeCycleIsNot = txtLifeCycleIsNot.Text;

                //extent
                if (txtNumOfObjects.Text != "<Need more data>" && txtNumOfObjects.Text != "") objSpec.NumOfObject = txtNumOfObjects.Text;
                if (txtIsNotNumOFObjects.Text != "<Need more data>" && txtIsNotNumOFObjects.Text != "") objSpec.NumOfObjectsIsNot = txtIsNotNumOFObjects.Text;
                if (txtDeviationSize.Text != "<Need more data>" && txtDeviationSize.Text != "") objSpec.SizeOfDeviation = txtDeviationSize.Text;
                if (txtHowManyDeviations.Text != "<Need more data>" && txtHowManyDeviations.Text != "") objSpec.NumOfDeviation = txtHowManyDeviations.Text;
                if (ddlTrend.SelectedIndex != 0) objSpec.Trend = Convert.ToInt16(ddlTrend.SelectedValue);

                //TRC Notes
                if (txtTRCNotes.Text != "<Need more data>" && txtTRCNotes.Text != "" ) objSpec.TRCNotes = txtTRCNotes.Text;

                objSpec.SaveData();

            }
            catch (Exception Exc)
            {
                throw Exc;
            }

        }
    }
}
