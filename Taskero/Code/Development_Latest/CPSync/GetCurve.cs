using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Fuji.DataAccess;

namespace CPSync
{
    public class GetCurve
    {

        //Constructor
        private GetCurve()
        {

        }

        public GetCurve(string MeasurementIDs, int PressRunID)
        {
            this._PressRunID = PressRunID;
            this._MeasurementID = MeasurementIDs;
        }

        private int _ProjectID = 0;
        public int ProjectID
        {
            get { return _ProjectID; }
            set { _ProjectID = value; }
        }

        private int _PressRunID = 0;
        public int PressRunID
        {
            get { return _PressRunID; }
            set { _PressRunID = value; }
        }

        //if this is a reiteration based on the existing press 
        // run pass the press run ID
        private int _MasterPressRunID = 0;
        public int MasterPressRunID
        {
            get { return _MasterPressRunID; }
            set { _MasterPressRunID = value; }
        }


        //measurement ids as comma separated values
        private string _MeasurementID;
        public string MeasurementID
        {
            get { return _MeasurementID; }
            set { _MeasurementID = value; }
        }

        //Use stored DLP For Iteration
        private bool _UseDLP = true;

        private ArrayList PCSData = new ArrayList();
        private ArrayList DCSData = new ArrayList();
        private ArrayList DensityData = new ArrayList();


        //array of doubles to hold PCS and DSC data
        private double[] arrPCS = new double[] { };
        private double[] arrDCS = new double[] { };
        private double[] arrDensity = new double[] { };

        // quarter tone target TVI 
        private string _LinearizationTVI25 = null;
        public string LinearizationTVI25
        {
            get { return _LinearizationTVI25; }
            set { _LinearizationTVI25 = value; }
        }

        // mid tone target TVI 
        private string _LinearizationTVI50 = null;
        public string LinearizationTVI50
        {
            get { return _LinearizationTVI50; }
            set { _LinearizationTVI50 = value; }
        }


        // three quarter tone target TVI 
        private string _LinearizationTVI75 = null;
        public string LinearizationTVI75
        {
            get { return _LinearizationTVI75; }
            set { _LinearizationTVI75 = value; }
        }


        /// <summary>
        /// Save Curve based on the given PCS and DSC data
        /// </summary>
        public string CreateCurve()
        {


            if (this._MeasurementID == String.Empty)
            {
                return "Create Curve Error - No Measurement IDs Passed";
            }
            if (this._PressRunID == 0)
            {
                return "Create Curve Error - No PressRun ID Passed";
            }


            try
            {

                Load_PCS_DCS_Data();

                if (arrPCS.Length <= 0)
                {
                    return "Create Curve Error - No PCS data loaded";
                }

                this._MasterPressRunID = GetMasterPressRun();


                CPSyncClient objSync = new CPSyncClient();
                Profile objProfile = new Profile();
                Curve objCurve = new Curve();
                objCurve.arrDCS = this.arrDCS;
                objCurve.arrPCS = this.arrPCS;
                objCurve.arrDensity = this.arrDensity;


                if (MasterPressRunID != 0 && this._UseDLP == true)
                {
                    byte[] profData = null;
                    profData = LoadProfileFromDB();

                    objCurve.UseDLP = this._UseDLP;
                    objCurve.ICCProfile = profData;
                }

                objProfile = objSync.CreateCurve(objCurve);

                int n = 101;
                double[] c = new double[n];
                double[] m = new double[n];
                double[] y = new double[n];
                double[] k = new double[n];


                for (int i = 0; i < n; i++)
                {

                    c[i] = objProfile.Cyan[i];
                    m[i] = objProfile.Magenta[i];
                    y[i] = objProfile.Yellow[i];
                    k[i] = objProfile.Key[i];

                }

                //loop through and save curve data
                for (int i = 0; i < n; i++)
                {
                    SaveCurveData(i, c[i], m[i], y[i], k[i]);
                }

                SaveProfile(objProfile.ICCProfile);

                return "OK";


            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


        }



        /// <summary>
        /// Save Curve based on the given PCS and DSC data
        /// </summary>
        public string CreateISOCurve()
        {
            if (this._MeasurementID == String.Empty)
            {
                return "Create Curve Error - No Measurement IDs Passed";
            }
            if (this._PressRunID == 0)
            {
                return "Create Curve Error - No PressRun ID Passed";
            }



            try
            {

                Load_PCS_DCS_Data();


                if (arrDensity.Length <= 0)
                {
                    return "Create Curve Error - No Density data loaded";
                }

                this._MasterPressRunID = GetMasterPressRun();

                Load_Linearization_TVI();

                CPSyncClient objSync = new CPSyncClient();
                Profile objProfile = new Profile();
                Curve objCurve = new Curve();
                objCurve.arrDCS = this.arrDCS;
                objCurve.arrPCS = this.arrPCS;
                objCurve.arrDensity = this.arrDensity;
                objCurve.LinearizationTVI25 = this._LinearizationTVI25;
                objCurve.LinearizationTVI50 = this._LinearizationTVI50;
                objCurve.LinearizationTVI75 = this._LinearizationTVI75;

                if (MasterPressRunID != 0 && this._UseDLP == true)
                {

                    byte[] profData = null;
                    profData = LoadProfileFromDB();

                    objCurve.UseDLP = this._UseDLP;
                    objCurve.ICCProfile = profData;
                }

                objProfile = objSync.CreateISOCurve(objCurve);

                int n = 101;
                double[] c = new double[n];
                double[] m = new double[n];
                double[] y = new double[n];
                double[] k = new double[n];


                for (int i = 0; i < n; i++)
                {

                    c[i] = objProfile.Cyan[i];
                    m[i] = objProfile.Magenta[i];
                    y[i] = objProfile.Yellow[i];
                    k[i] = objProfile.Key[i];

                }

                //loop through and save curve data
                for (int i = 0; i < n; i++)
                {
                    SaveCurveData(i, c[i], m[i], y[i], k[i]);
                }

                SaveProfile(objProfile.ICCProfile);
                return "OK";


            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {

            }

        }


        /// <summary>
        /// Delete Existing Curve Data
        /// </summary>
        public void DeleteCurve()
        {

            SqlCommand objCmd = new SqlCommand();
            try
            {
                objCmd.CommandText = "CPSync.uspDeleteCurveData";
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.AddWithValue("@PressRunID", this._PressRunID);

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

                objCmd.Parameters.AddWithValue("@MeasurementID", this._MeasurementID);

                SqlDataReader objDR = null;
                objDR = (new Fuji.DataAccess.Database()).GetDataReader(objCmd);

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

                arrPCS = (double[])PCSData.ToArray(typeof(double));
                arrDCS = (double[])DCSData.ToArray(typeof(double));
                arrDensity = (double[])DensityData.ToArray(typeof(double));
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

                objCmd.Parameters.AddWithValue("@ProjectID", this._ProjectID);

                SqlDataReader objDR = null;
                objDR = (new Fuji.DataAccess.Database()).GetDataReader(objCmd);

                if ((objDR != null) && objDR.HasRows == true)
                {

                    while (objDR.Read())
                    {


                        switch (Convert.ToInt16(objDR["TVI"]))
                        {
                            case 25:
                                this._LinearizationTVI25 = objDR["TVITarget"].ToString();
                                break;
                            case 50:
                                this._LinearizationTVI50 = objDR["TVITarget"].ToString();
                                break;
                            case 75:
                                this._LinearizationTVI75 = objDR["TVITarget"].ToString();
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
        /// Save Curve Data
        /// </summary>
        private void SaveCurveData(Int32 ReferenceNo, Double CMYK_C, Double CMYK_M, Double CMYK_Y, Double CMYK_K)
        {
            SqlCommand objCmd = new SqlCommand();
            try
            {

                objCmd.CommandText = "CPSync.uspInsertCurveData";
                objCmd.CommandType = CommandType.StoredProcedure;


                objCmd.Parameters.AddWithValue("@PressRunID", this._PressRunID);
                objCmd.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);
                objCmd.Parameters.AddWithValue("@CMYK_C", Convert.ToDecimal(CMYK_C));
                objCmd.Parameters.AddWithValue("@CMYK_M", Convert.ToDecimal(CMYK_M));
                objCmd.Parameters.AddWithValue("@CMYK_Y", Convert.ToDecimal(CMYK_Y));
                objCmd.Parameters.AddWithValue("@CMYK_K", Convert.ToDecimal(CMYK_K));

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

        ////Convert in memory profile (unmanged C++ memory buffer) to a managed (.net) byte array
        //private byte[] GetDLP(ref TYPE_p_void prof)
        //{
        //    IntPtr pAddr = IntPtr.Zero;
        //    try
        //    {
        //        int err = CrossXColor.CLNoErr;

        //        // write profile in memory and then save in the database
        //        int profSize = 0;
        //        err = CrossXColor.CLWriteProfileGetSize(prof, ref profSize);
        //        // now alloc unmanaged buffer and write profile to it
        //        pAddr = Marshal.AllocHGlobal(profSize);
        //        err = CrossXColor.CLWriteProfileToMemory(prof, pAddr, profSize);
        //        if (err != 0)
        //        {
        //            throw new Exception(Enum.GetName(typeof(CrossXColor.CLErrorType), err));
        //        }


        //        // alloc a managed array and copy profile data to it 
        //        byte[] profData = new byte[profSize];
        //        Marshal.Copy(pAddr, profData, 0, profSize);


        //        //return in memory profile as byte array
        //        return profData;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //    finally
        //    {
        //        if (pAddr != IntPtr.Zero)
        //        {
        //            // free the unmanaged buffer 
        //            Marshal.FreeHGlobal(pAddr);
        //        }
        //    }

        //}



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

                objCmd.Parameters.AddWithValue("@PressRunID", this._MasterPressRunID);

                SqlDataReader objDR = null;
                objDR = (new Fuji.DataAccess.Database()).GetDataReader(objCmd);

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



        ///// <summary>
        ///// Copy profile from byte array (Managed .NET) to unmanaged memory buffer (C++) 
        ///// </summary>
        ///// <param name="profile"></param>
        ///// <returns></returns>
        //private void CopyProfileInMemory(byte[] profile, ref TYPE_p_void srcProf)
        //{
        //    IntPtr pAddr = IntPtr.Zero;
        //    Int32 profSize = profile.Length;

        //    try
        //    {
        //        int err = CrossXColor.CLNoErr;

        //        pAddr = Marshal.AllocHGlobal(profSize);
        //        Marshal.Copy(profile, 0, pAddr, profSize);

        //        err = CrossXColor.CLReadProfileFromMemory(srcProf, pAddr, profSize);
        //        if (err != 0)
        //        {
        //            throw new Exception(Enum.GetName(typeof(CrossXColor.CLErrorType), err));
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //    finally
        //    {

        //        if (pAddr != IntPtr.Zero)
        //        {
        //            // free the unmanaged buffer 
        //            Marshal.FreeHGlobal(pAddr);
        //        }
        //    }

        //}


        /// <summary>
        /// Save Profile in the Database as a binary file 
        /// </summary>
        /// <param name="profData"></param>
        private void SaveProfile(byte[] profData)
        {

            SqlCommand objCmd = new SqlCommand();
            try
            {
                objCmd.CommandText = "CPSync.uspUpdateICCProfile";
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.AddWithValue("@PressRunID", this._PressRunID);
                objCmd.Parameters.AddWithValue("@ICCProfile", profData);

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


        private int GetMasterPressRun()
        {
            int MastPressRunID = 0;
            SqlCommand objCmd = new SqlCommand();
            try
            {
                objCmd.CommandText = "CPSync.uspGetMasterPressRun";
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.AddWithValue("@PressRunID", this._PressRunID);

                SqlDataReader objDR = null;
                objDR = (new Fuji.DataAccess.Database()).GetDataReader(objCmd);

                if ((objDR != null) && objDR.HasRows == true)
                {
                    while (objDR.Read())
                    {
                        if (objDR["MasterPressRunID"] != DBNull.Value)
                        {
                            MastPressRunID = (int)objDR["MasterPressRunID"];
                            this._UseDLP = (bool)objDR["UseDLP"];
                        }

                    }

                }
                return MastPressRunID;

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
        /// Save Neutral Print Desnity
        /// </summary>
        /// <param name="objMeasurement"></param>
        public void GetG7NPDC(Measurement objMeasurement)
        {

            try
            {

                //iterate through the collection and load PDC and DSC Data
                foreach (MeasuredPatch objPatch in objMeasurement.MeasuredPatches)
                {

                    DCSData.Add(Convert.ToDouble(objPatch.CMYK_C));
                    DCSData.Add(Convert.ToDouble(objPatch.CMYK_M));
                    DCSData.Add(Convert.ToDouble(objPatch.CMYK_Y));
                    DCSData.Add(Convert.ToDouble(objPatch.CMYK_K));

                    PCSData.Add(Convert.ToDouble(objPatch.XYZ.X));
                    PCSData.Add(Convert.ToDouble(objPatch.XYZ.Y));
                    PCSData.Add(Convert.ToDouble(objPatch.XYZ.Z));
                }
                arrPCS = (double[])PCSData.ToArray(typeof(double));
                arrDCS = (double[])DCSData.ToArray(typeof(double));



                if (arrPCS.Length <= 0)
                {
                    throw new Exception("PCS data not loaded");
                }

                double[] GrayBalance = null;

                if (objMeasurement.MeasurementTypeID == 1)
                {
                    GrayBalance = new double[25] { 0, 1.96, 3.92, 5.88, 7.84, 10.2, 14.9, 20, 25.1, 30.2, 34.9, 40, 45.1, 49.8, 54.9, 60, 65.1, 69.8, 74.9, 80, 85.1, 89.8, 94.9, 98.04, 100 };

                }
                else if (objMeasurement.MeasurementTypeID == 2)
                {
                    GrayBalance = new double[16] { 0, 5, 10, 15, 20, 25, 30, 40, 50, 60, 70, 75, 80, 90, 95, 100 };

                }
                else if (objMeasurement.MeasurementTypeID == 3)
                {
                    GrayBalance = new double[5] { 0, 25, 50, 75, 100 };
                }
                else if (objMeasurement.MeasurementTypeID == 4)
                {
                    GrayBalance = new double[5] { 0, 23, 45, 73, 100 };
                }
                else if (objMeasurement.MeasurementTypeID == 9)
                {
                    GrayBalance = new double[25] { 0, 1.96, 3.92, 5.88, 7.84, 10.2, 14.9, 20, 25.1, 30.2, 34.9, 40, 45.1, 49.8, 54.9, 60, 65.1, 69.8, 74.9, 80, 85.1, 89.8, 94.9, 98.04, 100 };

                }

                CPSyncClient objSync = new CPSyncClient();
                Profile objProfile = new Profile();
                Curve objCurve = new Curve();
                objCurve.arrDCS = this.arrDCS;
                objCurve.arrPCS = this.arrPCS;
                objCurve.GrayBalance = GrayBalance;

                objProfile = objSync.GetG7NPDC(objCurve);

                int n = GrayBalance.Length;


                //iterate thorugh the array 
                for (int j = 0; j < n; j++)
                {

                    double GrayNPD = objProfile.Cyan[j];
                    double BlackNPD = objProfile.Key[j];
                    double Gray_L = ColorMath.VisualDensToGrayL(objProfile.Cyan[j]);

                    //SAVE G7 Data
                    SaveG7NPDC(objMeasurement.MeasurementID, Convert.ToDecimal(GrayBalance[j]), GrayNPD, BlackNPD, Gray_L);

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {

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
