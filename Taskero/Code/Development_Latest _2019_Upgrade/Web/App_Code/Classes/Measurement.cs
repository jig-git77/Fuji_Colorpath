using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel.Web;
using System.Net;
using System.Collections;
 
namespace MeasurementPlugInSvc
{

    /* using System.Linq http://stackoverflow.com/questions/3907299/c-if-statements-matching-multiple-values */
    public static class Ext { public static bool In<T>(this T t, params T[] values) { return values.Contains(t); } }


    [DataContract]
    public class Measurement
    {
        /**********************************
         ** Any nonprimitive data to be sent or received via WCF methods must be marked with the DataContract attribute
         * *******************************/


        /***database id of the measurement ***/
        private int _MeasurementID = 0;
        public int MeasurementID
        {
            get { return _MeasurementID; }
            set { _MeasurementID = value; }
        }

        //Measurement Name 
        private string _MeasurementName = null;
        [DataMember(Order = 1)]
        public string MeasurementName
        {
            get { return _MeasurementName; }
            set { _MeasurementName = value; }
        }

        //Measurement DateTime  ToDO: Seriazliation issues with JavaScript DateTime and .NET (Fixed...)
        private DateTime _MeasurementDate;
        [DataMember(Order = 2)]
        public DateTime MeasurementDate
        {
            get { return _MeasurementDate; }
            set { _MeasurementDate = value; }
        }


        private int _PressRunID = 0;
         [DataMember(Order = 3)]
        public int PressRunID
        {
            get { return _PressRunID; }
            set { _PressRunID = value; }
        }


         private int _ChartTypeID = 0;
         [DataMember(Order = 4)]
         public int ChartTypeID
         {
             get { return _ChartTypeID; }
             set { _ChartTypeID = value; }
         }


         private Int16 _ProjectTypeID = 0;
         [DataMember(Order = 5)]
         public Int16 ProjectTypeID
         {
             get { return _ProjectTypeID; }
             set { _ProjectTypeID = value; }
         }
        


        /// <summary>
        /// Measured Patches as a Generic Collection of MeasuredPatch
        /// </summary>
        /// <remarks></remarks>
        private IList<MeasuredPatch> _MeasuredPatches;
        [DataMember(Order = 6)]
        public IList<MeasuredPatch> MeasuredPatches
        {
            get { return _MeasuredPatches; }
            set { _MeasuredPatches = value; }
        }
            
        public void Save()
        {

            try
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "CPSync.uspInsertMeasurement";

                Cmd.Parameters.AddWithValue("@PressRunID", this._PressRunID);
                Cmd.Parameters.AddWithValue("@UploadDateTime", this._MeasurementDate);
                Cmd.Parameters.AddWithValue("@MeasurementTypeID", this._ChartTypeID);	
                Cmd.Parameters.AddWithValue("@FileName", this._MeasurementName);
                Cmd.Parameters.Add(new SqlParameter("@MeasurementID", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));


                (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
                this._MeasurementID = Convert.ToInt32(Cmd.Parameters["@MeasurementID"].Value.ToString());

                foreach (MeasuredPatch objPatch in MeasuredPatches)
                {
                    objPatch.MeasurementID = this._MeasurementID;
                    objPatch.Save();
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



        public void SaveDotGain()
        {

            SqlCommand objCmd = new SqlCommand();
            try
            {

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "CPSync.uspInsertColorCharDotGain";

                objCmd.Parameters.AddWithValue("@MeasurementID", this._MeasurementID);

                
                (new Fuji.DataAccess.Database()).ExecuteNonQuery(objCmd);


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


        public void SaveISOGrayab()
        {

            SqlCommand objCmd = new SqlCommand();
            try
            {

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "CPSync.uspInsertColorCharISOGrayab";

                objCmd.Parameters.AddWithValue("@MeasurementID", this._MeasurementID);


                (new Fuji.DataAccess.Database()).ExecuteNonQuery(objCmd);

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


        public void SaveΔF()
        {

            SqlCommand objCmd = new SqlCommand();
            try
            {

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "CPSync.uspInsertColorCharΔF";

                objCmd.Parameters.AddWithValue("@MeasurementID", this._MeasurementID);

                (new Fuji.DataAccess.Database()).ExecuteNonQuery(objCmd);

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


        public void CalculateG7NPDC()
        {

            ArrayList PCSData = new ArrayList();
            ArrayList DCSData = new ArrayList();
            ArrayList GrayBalance = new ArrayList();
        
            double[] arrPCS = new double[] { };
            double[] arrDCS = new double[] { };
            double[] arrGrayBalance = null;
            

            try
            {
                foreach (MeasuredPatch objPatch in this._MeasuredPatches)
                {

                    DCSData.Add(Convert.ToDouble(objPatch.CMYK_C));
                    DCSData.Add(Convert.ToDouble(objPatch.CMYK_M));
                    DCSData.Add(Convert.ToDouble(objPatch.CMYK_Y));
                    DCSData.Add(Convert.ToDouble(objPatch.CMYK_K));

                    PCSData.Add(Convert.ToDouble(objPatch.XYZ.X));
                    PCSData.Add(Convert.ToDouble(objPatch.XYZ.Y));
                    PCSData.Add(Convert.ToDouble(objPatch.XYZ.Z));

                    if ((objPatch.PatchTypeID).In(8,9,10,28,29,30))
                    {
                         GrayBalance.Add(Convert.ToDouble(objPatch.CMYK_C));
                    }


                }


                GrayBalance.Sort();  //Chirag? Why are you sorting this array.
                arrPCS = (double[])PCSData.ToArray(typeof(double));
                arrDCS = (double[])DCSData.ToArray(typeof(double));
                arrGrayBalance = (double[])GrayBalance.ToArray(typeof(double));

                if (arrPCS.Length <= 0)
                {
                    throw new Exception("PCS data not loaded");
                }

                
                CPSyncClient objSync = new CPSyncClient();
                Profile objProfile = new Profile();
                Curve objCurve = new Curve();
                objCurve.arrDCS = arrDCS;
                objCurve.arrPCS = arrPCS;
                objCurve.GrayBalance = arrGrayBalance;


                objProfile = objSync.GetG7NPDC(objCurve);

                int n = arrGrayBalance.Length;


                //iterate thorugh the array 
                for (int j = 0; j < n; j++)
                {

                    double GrayNPD = objProfile.Cyan[j];
                    double BlackNPD = objProfile.Key[j];
                    double Gray_L = CPSync.ColorMath.VisualDensToGrayL(objProfile.Cyan[j]);

                    //SAVE G7 Data
                    SaveG7NPDC(this._MeasurementID, Convert.ToDecimal(arrGrayBalance[j]), GrayNPD, BlackNPD, Gray_L);

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
        /// Save G7 NPDC Data
        /// </summary>
        private void SaveG7NPDC(int MeasurementID, Decimal CMYK_C, Double GrayNPD, Double BlackNPD, Double Gray_L)
        {
            SqlCommand objCmd = new SqlCommand();
            try
            {

                objCmd.CommandText = "CPSync.uspInsertG7NPDC";
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.AddWithValue("@MeasurementID", MeasurementID);
                objCmd.Parameters.AddWithValue("@GrayNPD", Convert.ToDecimal(GrayNPD));
                objCmd.Parameters.AddWithValue("@BlackNPD", Convert.ToDecimal(BlackNPD));
                objCmd.Parameters.AddWithValue("@TargetGray_L", Convert.ToDecimal(Gray_L));
                objCmd.Parameters.AddWithValue("@CMYK_C", CMYK_C);

                (new Fuji.DataAccess.Database()).ExecuteNonQuery(objCmd);

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
        
    }



   
}