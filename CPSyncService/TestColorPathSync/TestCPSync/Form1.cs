using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace TestCPSync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SyncSvc.Curve objSyncIn = new SyncSvc.Curve();
        SyncSvc.Profile objSyncOut = new SyncSvc.Profile();


        private double[] _GrayBalance = new double[] { };
        public double[] GrayBalance
        {
            get { return this._GrayBalance; }
            set { this._GrayBalance = value; }
        }


        //measurement ids as comma separated values
        private int _MeasurementTypeID =1;
        public int MeasurementTypeID
        {
            get { return _MeasurementTypeID; }
            set { _MeasurementTypeID = value; }
        }

        private ArrayList PCSData = new ArrayList();
        private ArrayList DCSData = new ArrayList();
        private ArrayList DensityData = new ArrayList();

        // quarter tone target TVI 
        private string _LinearizationTVI25 = null;
        // mid tone target TVI 
        private string _LinearizationTVI50 = null;
        // three quarter tone target TVI 
        private string _LinearizationTVI75 = null;

        private static double[] LinRefColorsCMYK24Patch = {
         0.00,0.00,0.00,0.00,
25.00,0.00,0.00,0.00,
50.00,0.00,0.00,0.00,
75.00,0.00,0.00,0.00,
100.00,0.00,0.00,0.00,
0.00,25.00,0.00,0.00,
0.00,50.00,0.00,0.00,
0.00,75.00,0.00,0.00,
0.00,100.00,0.00,0.00,
0.00,0.00,25.00,0.00,
0.00,0.00,50.00,0.00,
0.00,0.00,75.00,0.00,
0.00,0.00,100.00,0.00,
0.00,0.00,0.00,25.00,
0.00,0.00,0.00,50.00,
0.00,0.00,0.00,75.00,
0.00,0.00,0.00,100.00,
23.00,16.00,16.00,0.00,
45.00,35.00,34.00,0.00,
73.00,63.00,62.00,0.00,
100.00,100.00,0.00,0.00,
0.00,100.00,100.00,0.00,
100.00,0.00,100.00,0.00,
100.00,100.00,100.00,0.00

        };

        private static double[] LinRefDensity = {
           
      
           0.06652,0.05752,0.03947,0.06231,0.2983,0.11781,0.06145,0.20403,0.61304,0.19068,0.0869,
  0.36953,1.0409,0.26118,0.10937,0.53629,1.8378,0.40081,0.15564,0.77092,0.09191,0.24532,0.08767,
  0.17264,0.11843,0.47486,0.1424,0.28625,0.12465,0.80234,0.23037,0.39285,0.20466,1.69079,0.54685,
  0.59345,0.06894,0.07191,0.27356,0.07198,0.07119,0.08721,0.5866,0.08091,0.07504,0.10471,1.00486,
  0.09045,0.0784,0.16246,1.66174,0.11015,0.35247,0.34619,0.3428,0.34702,0.72618,0.7277,0.74045,
  0.71802,1.28331,1.29437,1.30982,1.25878,2.47631,2.46385,2.5602,2.46624,0.27426,0.21812,0.18697,
  0.24258,0.52649,0.44027,0.40369,0.47117,1.15334,0.95199,0.88466,1.00218,2.15794,1.85206,0.64225,
  1.94355,0.1241,1.33484,1.91473,0.43686,1.3655,0.36424,1.42073,0.66193,1.75117,1.74122,1.68052,
  1.71568
        };


        private void button1_Click(object sender, EventArgs e)
        {
          
            SyncSvc.CPSyncClient objSync = new SyncSvc.CPSyncClient();
          
            //initialize parameters here 
           
            //Load_Linearization_TVI();
            //objSyncIn.UseDLP = true;
            //objSyncIn.ICCProfile = LoadProfileFromDB();

            objSyncIn.LinearizationTVI25 = "9.6 9.6 9.6 12.4";
            objSyncIn.LinearizationTVI50 = "14.4 14.4 14.4 17";
            objSyncIn.LinearizationTVI75 = "12 12 12 13.1";    

            DeleteCurve();
            //Load_PCS_DCS_Data();
            objSyncIn.arrDCS =   LinRefColorsCMYK24Patch;
            objSyncIn.arrDensity =  LinRefDensity;

            //objSyncOut = objSync.CreateCurve(objSyncIn);
            objSyncOut = objSync.CreateISOCurve(objSyncIn);
            
        }

        /// <summary>
        /// Load PCS (Profile Connection Space (XYZ or Lab data) and CMYK (DCS) data
        /// </summary>
        private void Load_PCS_DCS_Data()
        {

            SqlCommand objCmd = new SqlCommand();
            try
            {
                objCmd.CommandText = "CPSync.uspGetPCSData";
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.AddWithValue("@MeasurementID", "926");

                SqlDataReader objDR = null;
                objDR = (new DataAccess()).GetDataReader(objCmd);

                if ((objDR != null) && objDR.HasRows == true)
                {

                    while (objDR.Read())
                    {

                        DCSData.Add(Convert.ToDouble(objDR["CMYK_C"]));
                        DCSData.Add(Convert.ToDouble(objDR["CMYK_M"]));
                        DCSData.Add(Convert.ToDouble(objDR["CMYK_Y"]));
                        DCSData.Add(Convert.ToDouble(objDR["CMYK_K"]));

                        PCSData.Add(Convert.ToDouble(objDR["CIEXYZ_X"]));
                        PCSData.Add(Convert.ToDouble(objDR["CIEXYZ_Y"]));
                        PCSData.Add(Convert.ToDouble(objDR["CIEXYZ_Z"]));

                        DensityData.Add(Convert.ToDouble(objDR["Density_C"]));
                        DensityData.Add(Convert.ToDouble(objDR["Density_M"]));
                        DensityData.Add(Convert.ToDouble(objDR["Density_Y"]));
                        DensityData.Add(Convert.ToDouble(objDR["Density_V"]));

                    }

                }

                objSyncIn.arrPCS = (double[])PCSData.ToArray(typeof(double));
                objSyncIn.arrDCS = (double[])DCSData.ToArray(typeof(double));
                objSyncIn.arrDensity = (double[])DensityData.ToArray(typeof(double));
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Load Linearization TVI 
        /// </summary>
        private void Load_Linearization_TVI()
        {

            SqlCommand objCmd = new SqlCommand();
            try
            {
                objCmd.CommandText = "CPSync.uspGetLinearizationTVI";
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.AddWithValue("@ProjectID", 89);

                SqlDataReader objDR = null;
                objDR = (new DataAccess()).GetDataReader(objCmd);

                if ((objDR != null) && objDR.HasRows == true)
                {

                    while (objDR.Read())
                    {


                        switch (Convert.ToInt16(objDR["TVI"]))
                        {
                            case 25:
                                this._LinearizationTVI25 = objDR["TVITarget"].ToString();
                                objSyncIn.LinearizationTVI25 = this._LinearizationTVI25;
                                break;
                            case 50:
                                this._LinearizationTVI50 = objDR["TVITarget"].ToString();
                                 objSyncIn.LinearizationTVI50 =  this._LinearizationTVI50;
                                break;
                            case 75:
                                this._LinearizationTVI75 = objDR["TVITarget"].ToString();
                                objSyncIn.LinearizationTVI75 = this._LinearizationTVI75;
                                break;
                        }
                    }

                }


            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }



        /// <summary>
        /// Delete Existing Curve Data
        /// </summary>
        private void DeleteCurve()
        {

            SqlCommand objCmd = new SqlCommand();
            try
            {
                objCmd.CommandText = "CPSync.uspDeleteCurveData";
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.AddWithValue("@PressRunID", 203);

                (new DataAccess()).ExecuteNonQuery(objCmd);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }




        /// <summary>
        /// Load L-DLP profile from the database and and convert it to a byte array
        /// </summary>
        /// <returns></returns>
        private byte[] LoadProfileFromDB()
        {
            SqlCommand objCmd = new SqlCommand();
            byte[] profData = null;
            try
            {

                objCmd.CommandText = "CPSync.uspGetICCProfile";
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.AddWithValue("@PressRunID", 190);

                SqlDataReader objDR = null;
                objDR = (new DataAccess()).GetDataReader(objCmd);

                if ((objDR != null) && objDR.HasRows == true)
                {
                    while (objDR.Read())
                    {
                        profData = (byte[])objDR["ICCProfile"];

                    }
                }


                return profData;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception)
            {
                throw new Exception("Can not load profile from the database");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Load_PCS_DCS_Data();

            if (this._MeasurementTypeID == 1)
            {
                objSyncIn.GrayBalance = new double[25] { 0, 1.96, 3.92, 5.88, 7.84, 10.2, 14.9, 20, 25.1, 30.2, 34.9, 40, 45.1, 49.8, 54.9, 60, 65.1, 69.8, 74.9, 80, 85.1, 89.8, 94.9, 98.04, 100 };

            }
            else if (this._MeasurementTypeID == 2)
            {
                objSyncIn.GrayBalance = new double[16] { 0, 5, 10, 15, 20, 25, 30, 40, 50, 60, 70, 75, 80, 90, 95, 100 };

            }
            else if (this._MeasurementTypeID == 3)
            {
                objSyncIn.GrayBalance = new double[5] { 0, 25, 50, 75, 100 };
            }
            else if (this._MeasurementTypeID == 4)
            {
                objSyncIn.GrayBalance = new double[5] { 0, 23, 45, 73, 100 };
            }

            SyncSvc.CPSyncClient objSync = new SyncSvc.CPSyncClient();

            objSyncOut = objSync.GetG7NPDC(objSyncIn);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }


    
    }
}
