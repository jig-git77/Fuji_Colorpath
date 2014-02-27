using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ColorPathSync.MessageContracts;
using System.Runtime.InteropServices;

namespace ColorPathSync.Business
{
    public partial class ProfilerBusiness
    {
        public static CreateG7AlignmentCurveResponse CreateG7AlignmentCurve(CreateG7AlignmentCurveRequest request)
        {
            CreateG7AlignmentCurveResponse response = new CreateG7AlignmentCurveResponse();
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
                CrossXColor.CLInitVectorDataSet(DCSDataSet, CrossXColor.icSigCmykData, request.DCSData, request.DCSData.Length);
                switch (request.PCSDataType)
                {
                    case DataContracts.PCSDataType.XYZ:                        
                        CrossXColor.CLInitVectorDataSet(PCSDataSet, CrossXColor.icSigXYZData, request.PCSData, request.PCSData.Length);
                        break;                
                    case DataContracts.PCSDataType.Spectral:
                        CrossXColor.CLInitVectorDataSet(PCSDataSet, CrossXColor.clSpectral, request.PCSData, request.PCSData.Length);
                        break;
                }
                
                if (request.PCSData.Length <= 0)
                {
                    response.Message = "Create Curve Error - No PCS data loaded";
                    return response;
                }

                //--------Set other parameters-------//
                request.ProfileOptions.ForEach((param) => err = CrossXColor.CLSetProfilerOption(profiler, param.Name, param.Value));
                err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujifilm Graphic Systems USA");
                err = CrossXColor.CLSetProfilerOption(profiler, "ProfileType", "LinearizationDeviceLink");
                err = CrossXColor.CLSetProfilerOption(profiler, "LinearizationType", "G7");
                err = CrossXColor.CLSetProfilerOption(profiler, "SaveProfilerData", "1");
                err = CrossXColor.CLSetProfilerOption(profiler, "SaveProfilerInfo", "1");

                CrossXColor.CLSetProfilerData(profiler, request.DCSData.Length / 4, DCSDataSet, PCSDataSet);

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
                    response.Cyan[i] = c[i];
                    response.Magenta[i] = m[i];
                    response.Yellow[i] = y[i];
                    response.Key[i] = k[i];
                }
                response.DeviceLinkProfile.ProfileData = profData;

                //----Set Specific parameters for optimize run 1 to n.....(Like set source profile, not for initial 0 run)
                if (request.DeviceLinkProfile != null || request.UseDLP == true)
                {
                    //Declare a byte array to load profile from the database 
                    byte[] profData1 = null;
                    profData1 = request.DeviceLinkProfile.ProfileData;

                    //copy byte array into unmangaed buffer (in memory profile)
                    CopyProfileInMemory(profData1, ref srcProf);

                    //Set the source profile 
                    CrossXColor.CLSetOptionProfile(profiler, "SrcProfile", srcProf);
                }

                return response;
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
}