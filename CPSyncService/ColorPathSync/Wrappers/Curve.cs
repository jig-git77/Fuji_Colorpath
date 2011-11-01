using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;


[DataContract]
public class Curve
    {

        //Constructor
        public Curve()
        {

        }

        private double[] _GrayBalance = new double[] { };

        [DataMember()]
        public double[] GrayBalance
        {
            get { return this._GrayBalance; }
            set { this._GrayBalance = value; }
        }
        
             
        //Use stored DLP For Iteration
        private bool _UseDLP = true;

        [DataMember()]
        public bool UseDLP
        {
            get { return _UseDLP; }
            set { _UseDLP = value; }
        }
    

        private ArrayList PCSData = new ArrayList();
        private ArrayList DCSData = new ArrayList();
        private ArrayList DensityData = new ArrayList();
       

        //array of doubles to hold PCS and DSC data
        private double[] _arrPCS = new double[] { };
        private double[] _arrDCS = new double[] { };
        private double[] _arrDensity = new double[] { };


        [DataMember()]
        public double[] arrPCS
        {
            get { return this._arrPCS; }
            set { this._arrPCS = value; }
        }


        [DataMember()]
        public double[] arrDCS
        {
            get { return this._arrDCS; }
            set { this._arrDCS = value; }
        }


        [DataMember()]
        public double[] arrDensity
        {
            get { return this._arrDensity; }
            set { this._arrDensity = value; }
        }

        // quarter tone target TVI 
        private string _LinearizationTVI25 = null;


        [DataMember()]    
        public string LinearizationTVI25
        {
            get { return _LinearizationTVI25; }
            set { _LinearizationTVI25 = value; }
        }

        // mid tone target TVI 
        private string _LinearizationTVI50 = null;


        [DataMember()]
        public string LinearizationTVI50
        {
            get { return _LinearizationTVI50; }
            set { _LinearizationTVI50 = value; }
        }


        // three quarter tone target TVI 
        private string _LinearizationTVI75 = null;


        [DataMember()]
        public string LinearizationTVI75
        {
            get { return _LinearizationTVI75; }
            set { _LinearizationTVI75 = value; }
        }

        //if this is a reiteration based on the existing press run
        // pass the ICC Profile
        
        private byte[] _ICCProfile;


        [DataMember()]
        public byte[] ICCProfile
        {
            set { _ICCProfile = value; }
            get { return _ICCProfile; }
        }


        /// <summary>
        /// Save Curve based on the given PCS and DSC data
        /// </summary>
        public Profile CreateCurve()
        {

            Profile objProfile = new Profile();
            

            int err = CrossXColor.CLNoErr;
            TYPE_p_void profiler = CrossXColor.CLNewCLProfilerRef(); // this object referece to CroxColor Profiler object
            TYPE_p_void prof = CrossXColor.CLNewICCProfileRef();  // this object  references an ICC profile to access its data.
            TYPE_p_void srcProf = CrossXColor.CLNewICCProfileRef();
            TYPE_p_void trafo = null;
            int[] renderingIntents = new int[] { CrossXColor.icPerceptual };

            try
            {
              
                CLVectorDataSet DCSDataSet = new CLVectorDataSet();
                CLVectorDataSet PCSDataSet = new CLVectorDataSet();

                CrossXColor.CLInitVectorDataSet(DCSDataSet, CrossXColor.icSigCmykData, arrDCS, arrDCS.Length);
                CrossXColor.CLInitVectorDataSet(PCSDataSet, CrossXColor.icSigXYZData, arrPCS, arrPCS.Length);

                if (arrPCS.Length <= 0)
                {
                    objProfile.ErrorMsg = "Create Curve Error - No PCS data loaded";
                    return objProfile;
                }

               
                if (this._ICCProfile == null || this._UseDLP == false)
                {

                    err = CrossXColor.CLSetProfilerOption(profiler, "ProfileType", "LinearizationDeviceLink");
                    err = CrossXColor.CLSetProfilerOption(profiler, "LinearizationType", "G7");
                    err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                    err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujifilm Graphic Systems USA");

                    CrossXColor.CLSetProfilerData(profiler, arrDCS.Length / 4, DCSDataSet, PCSDataSet);

                    // calc profile in memory
                    err = CrossXColor.CLCalcProfile(profiler, prof);
                    if (err != 0)
                    {
                        throw new Exception(Enum.GetName(typeof(CrossXColor.CLErrorType), err));
                    }

                    // Use memory profile for calculation
                    // pulls the profile into a "trafo" or transformation
                    TYPE_p_void[] profiles = new TYPE_p_void[1] { prof };
                    trafo = CrossXColor.CLConcatICCProfileRefs(1, profiles, renderingIntents);
                    if (trafo == null)
                    {
                        throw new Exception("CLConcatICCProfileRefs Error");
                    }

                    int n = 101;
                    double[] c = new double[n];
                    double[] m = new double[n];
                    double[] y = new double[n];
                    double[] k = new double[n];
                    double[] input = new double[4];
                    double[] output = new double[4];
                    for (int i = 0; i < n; i++)
                    {
                        input[0] = input[1] = input[2] = input[3] = (double)i;

                        // Extract the desired datapoints from the Trafo, with the desired "IN" values this returns desired "OUT"
                        err = CrossXColor.CLDoTrafo(trafo, input, output);
                        if (err != 0)
                        {
                            throw new Exception(Enum.GetName(typeof(CrossXColor.CLErrorType), err));
                        }

                        c[i] = output[0];
                        m[i] = output[1];
                        y[i] = output[2];
                        k[i] = output[3];
                    }

                   byte[] profData = GetDLP(ref prof);
                   
                   for (int i = 0; i < n; i++)
                   {
                        objProfile.Cyan[i] = c[i];
                        objProfile.Magenta[i] = m[i];
                        objProfile.Yellow[i] = y[i];
                        objProfile.Key[i] = k[i];
                   } 
                   objProfile.ICCProfile = profData;
                   return objProfile;

                }
                else
                {

                    err = CrossXColor.CLSetProfilerOption(profiler, "ProfileType", "LinearizationDeviceLink");
                    err = CrossXColor.CLSetProfilerOption(profiler, "LinearizationType", "G7");
                    err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                    err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujifilm Graphic Systems USA");


                    //Declare a byte array to load profile from the database 
                    byte[] profData = null;
                    profData = this._ICCProfile;

                    //copy byte array into unmangaed buffer (in memory profile)
                    CopyProfileInMemory(profData, ref srcProf);

                    //Set the source profile 
                    CrossXColor.CLSetOptionProfile(profiler, "SrcProfile", srcProf);

                    CrossXColor.CLSetProfilerData(profiler, arrDCS.Length / 4, DCSDataSet, PCSDataSet);

                    // calc profile in memory
                    err = CrossXColor.CLCalcProfile(profiler, prof);
                    if (err != 0)
                    {
                        throw new Exception(Enum.GetName(typeof(CrossXColor.CLErrorType), err));
                    }

                    // Use memory profile for calculation
                    // pulls the profile into a "trafo" or transformation
                    TYPE_p_void[] profiles = new TYPE_p_void[1] { prof };
                    trafo = CrossXColor.CLConcatICCProfileRefs(1, profiles, renderingIntents);
                    if (trafo == null)
                    {
                        throw new Exception("CLConcatICCProfileRefs Error");
                    }

                    int n = 101;
                    double[] c = new double[n];
                    double[] m = new double[n];
                    double[] y = new double[n];
                    double[] k = new double[n];
                    double[] input = new double[4];
                    double[] output = new double[4];
                    for (int i = 0; i < n; i++)
                    {
                        input[0] = input[1] = input[2] = input[3] = (double)i;

                        // Extract the desired datapoints from the Trafo, with the desired "IN" values this returns desired "OUT"
                        err = CrossXColor.CLDoTrafo(trafo, input, output);
                        if (err != 0)
                        {
                            throw new Exception(Enum.GetName(typeof(CrossXColor.CLErrorType), err));
                        }

                        c[i] = output[0];
                        m[i] = output[1];
                        y[i] = output[2];
                        k[i] = output[3];
                    }
   

                    byte[] profData1 = GetDLP(ref prof);

                    for (int i = 0; i < n; i++)
                    {
                        objProfile.Cyan[i] = c[i];
                        objProfile.Magenta[i] = m[i];
                        objProfile.Yellow[i] = y[i];
                        objProfile.Key[i] = k[i];
                    }
                    objProfile.ICCProfile = profData1;
                    return objProfile;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                //clear the unmanaged memory

                CrossXColor.CLFreeCLProfilerRef(profiler);
                CrossXColor.CLFreeICCProfileRef(prof);
                CrossXColor.CLFreeCLTrafoRef(trafo);
                CrossXColor.CLFreeICCProfileRef(srcProf);
            }

        }


   
      

        //Convert in memory profile (unmanged C++ memory buffer) to a managed (.net) byte array
        internal static byte[] GetDLP(ref TYPE_p_void prof)
        {
            IntPtr pAddr = IntPtr.Zero;
            try
            {
                int err = CrossXColor.CLNoErr;

                // write profile in memory and then save in the database
                int profSize = 0;
                err = CrossXColor.CLWriteProfileGetSize(prof, ref profSize);
                // now alloc unmanaged buffer and write profile to it
                pAddr = Marshal.AllocHGlobal(profSize);
                err = CrossXColor.CLWriteProfileToMemory(prof, pAddr, profSize);
                if (err != 0)
                {
                    throw new Exception(Enum.GetName(typeof(CrossXColor.CLErrorType), err));
                }


                // alloc a managed array and copy profile data to it 
                byte[] profData = new byte[profSize];
                Marshal.Copy(pAddr, profData, 0, profSize);


                //return in memory profile as byte array
                return profData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (pAddr != IntPtr.Zero)
                {
                    // free the unmanaged buffer 
                    Marshal.FreeHGlobal(pAddr);
                }
            }

        }


        /// <summary>
        /// Copy profile from byte array (Managed .NET) to unmanaged memory buffer (C++) 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        private void CopyProfileInMemory(byte[] profile, ref TYPE_p_void srcProf)
        {
            IntPtr pAddr = IntPtr.Zero;
            Int32 profSize = profile.Length;

            try
            {
                int err = CrossXColor.CLNoErr;

                pAddr = Marshal.AllocHGlobal(profSize);
                Marshal.Copy(profile, 0, pAddr, profSize);

                err = CrossXColor.CLReadProfileFromMemory(srcProf, pAddr, profSize);
                if (err != 0)
                {
                    throw new Exception(Enum.GetName(typeof(CrossXColor.CLErrorType), err));
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {

                if (pAddr != IntPtr.Zero)
                {
                    // free the unmanaged buffer 
                    Marshal.FreeHGlobal(pAddr);
                }
            }

        }

        /// <summary>
        /// Save Neutral Print Desnity
        /// </summary>
        /// <param name="objMeasurement"></param>
        public Profile GetG7NPDC()
        {

            Profile objProfile = new Profile();

            int err = CrossXColor.CLNoErr;
            TYPE_p_void profiler = CrossXColor.CLNewCLProfilerRef();            // this object referece to CroxColor Profiler object
            TYPE_p_void prof = CrossXColor.CLNewICCProfileRef();                // this object  references an ICC profile to access its data.
            TYPE_p_void srcProf = CrossXColor.CLNewICCProfileRef();

            TYPE_p_void trafo = null;
            int[] renderingIntents = new int[] { CrossXColor.icPerceptual };


            try
            {

               

                CLVectorDataSet DCSDataSet = new CLVectorDataSet();
                CLVectorDataSet PCSDataSet = new CLVectorDataSet();

                CrossXColor.CLInitVectorDataSet(DCSDataSet, CrossXColor.icSigCmykData, this._arrDCS, this._arrDCS.Length);
                CrossXColor.CLInitVectorDataSet(PCSDataSet, CrossXColor.icSigXYZData, this._arrPCS, this._arrPCS.Length);

                if (arrPCS.Length <= 0)
                {
                    objProfile.ErrorMsg = "Get G7NPDC  Error - No Density data loaded";
                    return objProfile;
                }

                err = CrossXColor.CLSetProfilerOption(profiler, "ProfileType", "LinearizationDeviceLink");
                err = CrossXColor.CLSetProfilerOption(profiler, "LinearizationType", "G7-NPDC-Target");
                err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujifilm Graphic Systems USA");

                CrossXColor.CLSetProfilerData(profiler, arrDCS.Length / 4, DCSDataSet, PCSDataSet);

                // calc profile in memory
                err = CrossXColor.CLCalcProfile(profiler, prof);
                if (err != 0)
                {
                    throw new Exception(Enum.GetName(typeof(CrossXColor.CLErrorType), err));
                }

                // Use memory profile for calculation
                // pulls the profile into a "trafo" or transformation
                TYPE_p_void[] profiles = new TYPE_p_void[1] { prof };
                trafo = CrossXColor.CLConcatICCProfileRefs(1, profiles, renderingIntents);
                if (trafo == null)
                {
                    throw new Exception("CLConcatICCProfileRefs Error");
                }


                int n = this._GrayBalance.Length;
                double[] c = new double[n];
                double[] m = new double[n];
                double[] y = new double[n];
                double[] k = new double[n];
                double[] input = new double[4];
                double[] output = new double[4];


                for (int i = 0; i < n; i++)
                {
                    input[0] = Convert.ToDouble(GrayBalance[i]);
                    input[1] = 0;
                    input[2] = 0;
                    input[3] = Convert.ToDouble(GrayBalance[i]);


                    // Extract the desired datapoints from the Trafo, with the desired "IN" values this returns desired "OUT"
                    err = CrossXColor.CLDoTrafo(trafo, input, output);
                    if (err != 0)
                    {
                        throw new Exception(Enum.GetName(typeof(CrossXColor.CLErrorType), err));
                    }


                    //I added the linearization type "G7-NPDC-Target". It calculates the target curves (CMYK->Neutral density) based on the measurements. 
                    //The 'problem' is, that the ICC does not define density types. Therefore I coded the target density as CMYK with factor 10.0. Example: To calculate the 50% target densities Input = "50 50 50 50" => Output = " 5.47 5.47 5.47 4.98" means CMY density = 0.547 and black density = 0.498. 
                    c[i] = output[0] / 10.0;
                    m[i] = output[1];
                    y[i] = output[2];
                    k[i] = output[3] / 10.0;

                }

                for (int i = 0; i < n; i++)
                {
                    objProfile.Cyan[i] = c[i];
                    objProfile.Magenta[i] = m[i];
                    objProfile.Yellow[i] = y[i];
                    objProfile.Key[i] = k[i];
                }

                return objProfile;

               


            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                //clear the unmanaged memory

                CrossXColor.CLFreeCLProfilerRef(profiler);
                CrossXColor.CLFreeICCProfileRef(prof);
                CrossXColor.CLFreeCLTrafoRef(trafo);
                CrossXColor.CLFreeICCProfileRef(srcProf);
            }

        }



 
    }

