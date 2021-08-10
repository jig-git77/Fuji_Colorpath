using CPProfilerService.Models.Entity;
using CPProfilerService.Models.Helper;
using CPProfilerService.Models.Request;
using CPProfilerService.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static CPProfilerService.Models.Entity.Enums;

namespace CPProfilerService.Business
{
    public class ProfilerApiBusiness
    {
        public static CreateDestinationAndDeviceLinkProfileResponse CreateDestinationAndDeviceLinkProfile(CreateDestinationAndDeviceLinkProfileRequest request)
        {
            var response = new CreateDestinationAndDeviceLinkProfileResponse()
            {
                DestinationProfile = new IccProfile(),
                DeviceLinkProfile = new IccProfile()
            };
            if (request.DcsData == null || request.DcsData.Length == 0)
            {
                response.Message = "DcsData not loaded";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            if (request.PcsData == null || request.PcsData.Length == 0)
            {
                response.Message = "PcsData not loaded";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            if (request.SourceProfile == null || request.SourceProfile.ProfileData == null)
            {
                response.Message = "Please provide source profile";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            TYPE_p_void profiler = CrossXColor.CLNewCLProfilerRef(); // this object referece to CroxColor Profiler object
            TYPE_p_void prof = CrossXColor.CLNewICCProfileRef();  // this object  references an ICC profile to access its data.
            TYPE_p_void destProf = CrossXColor.CLNewICCProfileRef();
            TYPE_p_void srcProf = CrossXColor.CLNewICCProfileRef();

            try
            {
                #region Calculate destination profile
                var dcsDataSet = new CLVectorDataSet();
                var pcsDataSet = new CLVectorDataSet();
                CrossXColor.CLInitVectorDataSet(dcsDataSet, CrossXColor.icSigCmykData, request.DcsData, request.DcsData.Length);

                switch (request.PCSDataType)
                {
                    case PCSDataType.XYZ:
                        CrossXColor.CLInitVectorDataSet(pcsDataSet, CrossXColor.icSigXYZData, request.PcsData, request.PcsData.Length);
                        break;
                    case PCSDataType.Spectral:
                        CrossXColor.CLInitVectorDataSet(pcsDataSet, CrossXColor.clRemission, request.PcsData, request.PcsData.Length);
                        break;
                }

                if (request.PcsData.Length <= 0)
                {
                    response.Message = "Error - No PCS data loaded";
                    response.Severity = MessageSeverity.Error;
                    return response;
                }

                var err = CrossXColor.CLNoErr;
                request.DestinationProfileOptions.ForEach((param) => err = CrossXColor.CLSetProfilerOption(profiler, param.Name, param.Value));
                err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujifilm Graphic Systems USA");
                err = CrossXColor.CLSetProfilerOption(profiler, "SaveProfilerData", "1");

                CrossXColor.CLSetProfilerData(profiler, request.DcsData.Length / 4, dcsDataSet, pcsDataSet);

                // calc profile in memory
                err = CrossXColor.CLCalcProfile(profiler, destProf);
                if (err != 0)
                {
                    response.Message = Enum.GetName(typeof(CrossXColor.CLErrorType), err);
                    response.Severity = MessageSeverity.Error;
                    return response;
                }
                response.DestinationProfile.ProfileData = Curve.GetDLP(ref destProf);
                #endregion

                #region Calculate Device link profile
                //clear profiler memory...
                CrossXColor.CLFreeCLProfilerRef(profiler);
                profiler = CrossXColor.CLNewCLProfilerRef();
                request.ProfileOptions.ForEach((param) => err = CrossXColor.CLSetProfilerOption(profiler, param.Name, param.Value));
                err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujifilm Graphic Systems USA");

                Curve.CopyProfileInMemory(request.SourceProfile.ProfileData, ref srcProf);

                err = CrossXColor.CLSetOptionProfile(profiler, "SrcProfile", srcProf);
                err = CrossXColor.CLSetOptionProfile(profiler, "DstProfile", destProf);

                // calc profile in memory
                err = CrossXColor.CLCalcProfile(profiler, prof);
                if (err != 0)
                {
                    response.Message = Enum.GetName(typeof(CrossXColor.CLErrorType), err);
                    response.Severity = MessageSeverity.Error;
                    return response;
                }
                response.DeviceLinkProfile.ProfileData = Curve.GetDLP(ref prof);
                #endregion
            }
            catch (Exception ex)
            {
                response.Message = String.Format("Error:{0}, Additional Info:{1}", ex.Message, ex.ToString());
                response.Severity = MessageSeverity.Error;
            }
            finally
            {
                //clear the unmanaged memory
                CrossXColor.CLFreeCLProfilerRef(profiler);
                CrossXColor.CLFreeICCProfileRef(prof);
                CrossXColor.CLFreeICCProfileRef(srcProf);
                CrossXColor.CLFreeICCProfileRef(destProf);
            }
            return response;
        }
        public static OptimizeDeviceLinkProfileResponse OptimizeDeviceLinkProfile(OptimizeDeviceLinkProfileRequest request)
        {
            var response = new OptimizeDeviceLinkProfileResponse()
            {
                OptimizedProfile = new IccProfile()
            };

            if (request.DCSData == null || request.DCSData.Length == 0)
            {
                response.Message = "DCSData not loaded";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            if (request.PCSData == null || request.PCSData.Length == 0)
            {
                response.Message = "PCSData not loaded";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            if (request.SourceProfile == null || request.SourceProfile.ProfileData == null)
            {
                response.Message = "Please provide source profile";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            if (request.DestinationProfile == null || request.DestinationProfile.ProfileData == null)
            {
                response.Message = "Please provide destination profile";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            if (request.DeviceLinkProfile == null || request.DeviceLinkProfile.ProfileData == null)
            {
                response.Message = "Please provide device link profile";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            TYPE_p_void profiler = CrossXColor.CLNewCLProfilerRef(); // this object referece to CroxColor Profiler object
            TYPE_p_void prof = CrossXColor.CLNewICCProfileRef();  // this object  references an ICC profile to access its data.
            TYPE_p_void srcProf = CrossXColor.CLNewICCProfileRef();
            TYPE_p_void destProf = CrossXColor.CLNewICCProfileRef();
            TYPE_p_void dlpProf = CrossXColor.CLNewICCProfileRef();

            try
            {
                var dcsDataSet = new CLVectorDataSet();
                var pcsDataSet = new CLVectorDataSet();
                CrossXColor.CLInitVectorDataSet(dcsDataSet, CrossXColor.icSigCmykData, request.DCSData, request.DCSData.Length);

                switch (request.PCSDataType)
                {
                    case PCSDataType.XYZ:
                        CrossXColor.CLInitVectorDataSet(pcsDataSet, CrossXColor.icSigXYZData, request.PCSData, request.PCSData.Length);
                        break;
                    case PCSDataType.Spectral:
                        CrossXColor.CLInitVectorDataSet(pcsDataSet, CrossXColor.clRemission, request.PCSData, request.PCSData.Length);
                        break;
                }

                if (request.PCSData.Length <= 0)
                {
                    response.Message = "Error - No PCS data loaded";
                    response.Severity = MessageSeverity.Error;
                    return response;
                }

                var err = CrossXColor.CLNoErr;
                request.ProfileOptions.ForEach((param) => err = CrossXColor.CLSetProfilerOption(profiler, param.Name, param.Value));
                err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujifilm Graphic Systems USA");

                CrossXColor.CLSetProfilerData(profiler, request.DCSData.Length / 4, dcsDataSet, pcsDataSet);

                Curve.CopyProfileInMemory(request.SourceProfile.ProfileData, ref srcProf);
                Curve.CopyProfileInMemory(request.DestinationProfile.ProfileData, ref destProf);
                Curve.CopyProfileInMemory(request.DeviceLinkProfile.ProfileData, ref dlpProf);

                err = CrossXColor.CLSetOptionProfile(profiler, "SrcProfile", srcProf);
                //err = CrossXColor.CLSetProfilerOption(profiler, "SrcProfile", "C:\\Data\\Projects\\Fujifilm\\CPSyncService\\ColorPathSyncTest\\DataFiles\\gracol2006_320_gcrV4Z2.icc");
                err = CrossXColor.CLSetOptionProfile(profiler, "DstProfile", destProf);
                err = CrossXColor.CLSetOptionProfile(profiler, "Src2DstProfile", dlpProf);

                // calc profile in memory
                err = CrossXColor.CLCalcProfile(profiler, prof);
                if (err != 0)
                {
                    response.Message = Enum.GetName(typeof(CrossXColor.CLErrorType), err);
                    response.Severity = MessageSeverity.Error;
                    return response;
                }
                response.OptimizedProfile.ProfileData = Curve.GetDLP(ref prof);
            }
            catch (Exception ex)
            {
                response.Message = String.Format("Error:{0}, Additional Info:{1}", ex.Message, ex.ToString());
                response.Severity = MessageSeverity.Error;
            }
            finally
            {
                //clear the unmanaged memory
                CrossXColor.CLFreeCLProfilerRef(profiler);
                CrossXColor.CLFreeICCProfileRef(prof);
                CrossXColor.CLFreeICCProfileRef(srcProf);
                CrossXColor.CLFreeICCProfileRef(destProf);
                CrossXColor.CLFreeICCProfileRef(dlpProf);
            }
            return response;
        }
        public static CreateSourceToSourceDeviceLinkProfileResponse CreateSourceToSourceDeviceLinkProfile(CreateSourceToSourceDeviceLinkProfileRequest request)
        {
            //----Use this fuction for Source to Source DLP creation-----//
            var response = new CreateSourceToSourceDeviceLinkProfileResponse()
            {
                DeviceLinkProfile = new IccProfile()
            };
            if (request.SourceProfile == null || request.SourceProfile.ProfileData == null)
            {
                response.Message = "Please provide source profile";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            TYPE_p_void profiler = CrossXColor.CLNewCLProfilerRef(); // this object referece to CroxColor Profiler object
            TYPE_p_void prof = CrossXColor.CLNewICCProfileRef();  // this object  references an ICC profile to access its data.
            TYPE_p_void srcProf = CrossXColor.CLNewICCProfileRef();

            try
            {
                var err = CrossXColor.CLNoErr;
                request.ProfileOptions.ForEach((param) => err = CrossXColor.CLSetProfilerOption(profiler, param.Name, param.Value));
                err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujifilm Graphic Systems USA");

                Curve.CopyProfileInMemory(request.SourceProfile.ProfileData, ref srcProf);

                err = CrossXColor.CLSetOptionProfile(profiler, "SrcProfile", srcProf);
                err = CrossXColor.CLSetOptionProfile(profiler, "DstProfile", srcProf);

                // calc profile in memory
                err = CrossXColor.CLCalcProfile(profiler, prof);
                if (err != 0)
                {
                    response.Message = Enum.GetName(typeof(CrossXColor.CLErrorType), err);
                    response.Severity = MessageSeverity.Error;
                    return response;
                }
                response.DeviceLinkProfile.ProfileData = Curve.GetDLP(ref prof);
            }
            catch (Exception ex)
            {
                response.Message = String.Format("Error:{0}, Additional Info:{1}", ex.Message, ex.ToString());
                response.Severity = MessageSeverity.Error;
            }
            finally
            {
                //clear the unmanaged memory
                CrossXColor.CLFreeCLProfilerRef(profiler);
                CrossXColor.CLFreeICCProfileRef(prof);
                CrossXColor.CLFreeICCProfileRef(srcProf);
            }
            return response;
        }
        public static CreateG7AlignmentCurveResponse CreateG7AlignmentCurve(CreateG7AlignmentCurveRequest request)
        {
            CreateG7AlignmentCurveResponse response = new CreateG7AlignmentCurveResponse();
            int err = CrossXColor.CLNoErr;
            TYPE_p_void profiler = CrossXColor.CLNewCLProfilerRef(); // this object referece to CroxColor Profiler object
            TYPE_p_void prof = CrossXColor.CLNewICCProfileRef();  // this object  references an ICC profile to access its data.
            TYPE_p_void srcProf = CrossXColor.CLNewICCProfileRef();
            TYPE_p_void trafo = null;
            int[] renderingIntents = new int[] { CrossXColor.icPerceptual };
            int outvar = 0;

            try
            {
                CLVectorDataSet DCSDataSet = new CLVectorDataSet();
                CLVectorDataSet PCSDataSet = new CLVectorDataSet();
                CrossXColor.CLInitVectorDataSet(DCSDataSet, CrossXColor.icSigCmykData, request.DCSData, request.DCSData.Length);
                switch (request.PCSDataType)
                {
                    case PCSDataType.XYZ:
                        CrossXColor.CLInitVectorDataSet(PCSDataSet, CrossXColor.icSigXYZData, request.PCSData, request.PCSData.Length);
                        break;
                    case PCSDataType.Spectral:
                        CrossXColor.CLInitVectorDataSet(PCSDataSet, CrossXColor.clRemission, request.PCSData, request.PCSData.Length);
                        break;
                }

                if (request.PCSData.Length <= 0)
                {
                    response.Message = "Create Curve Error - No PCS data loaded";
                    return response;
                }

                //--------Set other parameters-------//
                request.ProfileOptions.ForEach((param) => err = CrossXColor.CLSetProfilerOption(profiler, param.Name, param.Value));

                // request.ProfileOptions.ForEach((param) => outvar = (param.Name.Contains("LinearizationType")) ? outvar = outvar + 1 : outvar = outvar + 0);

                int index = request.ProfileOptions.FindIndex(item => item.Name == "LinearizationType");

                err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujifilm Graphic Systems USA");
                err = CrossXColor.CLSetProfilerOption(profiler, "ProfileType", "LinearizationDeviceLink");

                if (index < 0)
                {
                    err = CrossXColor.CLSetProfilerOption(profiler, "LinearizationType", "G7");
                }

                err = CrossXColor.CLSetProfilerOption(profiler, "SaveProfilerData", "1");
                err = CrossXColor.CLSetProfilerOption(profiler, "SaveProfilerInfo", "1");

                CrossXColor.CLSetProfilerData(profiler, request.DCSData.Length / 4, DCSDataSet, PCSDataSet);

                //----Set Specific parameters for optimize run 1 to n.....(Like set source profile, not for initial 0 run)
                if (request.DeviceLinkProfile != null && request.UseDLP == true)
                {
                    //Declare a byte array to load profile from the database 
                    byte[] profData1 = null;
                    profData1 = request.DeviceLinkProfile.ProfileData;

                    //copy byte array into unmangaed buffer (in memory profile)
                    Common.CopyProfileInMemory(profData1, ref srcProf);

                    //Set the source profile 
                    CrossXColor.CLSetOptionProfile(profiler, "SrcProfile", srcProf);
                }

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

                byte[] profData =  Common.GetDLP(ref prof);

                for (int i = 0; i < n; i++)
                {
                    response.Cyan[i] = c[i];
                    response.Magenta[i] = m[i];
                    response.Yellow[i] = y[i];
                    response.Key[i] = k[i];
                }
                response.DeviceLinkProfile.ProfileData = profData;

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
        public static CreateISOAlignmentCurveResponse CreateISOAlignmentCurve(CreateISOAlignmentCurveRequest request)
        {
            CreateISOAlignmentCurveResponse response = new CreateISOAlignmentCurveResponse();
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
                    case PCSDataType.Density:
                        CrossXColor.CLInitVectorDataSet(PCSDataSet, CrossXColor.clDensityCMYK, request.PCSData, request.PCSData.Length);
                        break;
                    case PCSDataType.Spectral:
                        CrossXColor.CLInitVectorDataSet(PCSDataSet, CrossXColor.clRemission, request.PCSData, request.PCSData.Length);
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
                err = CrossXColor.CLSetProfilerOption(profiler, "LinearizationType", "Density");
                err = CrossXColor.CLSetProfilerOption(profiler, "SaveProfilerData", "1");
                err = CrossXColor.CLSetProfilerOption(profiler, "SaveProfilerInfo", "1");

                CrossXColor.CLSetProfilerData(profiler, request.DCSData.Length / 4, DCSDataSet, PCSDataSet);

                //Profiler's Default value will be "Staus-E"
                switch (request.DensityType)
                {
                    case DensityType.Status_E:
                        err = CrossXColor.CLCrossXColorSetOption("DensityMode", "Status-E");
                        break;
                    case DensityType.Status_T:
                        err = CrossXColor.CLCrossXColorSetOption("DensityMode", "Status-T");
                        break;

                }

                //----Set Specific parameters for optimize run 1 to n.....(Like set source profile, not for initial 0 run)
                if (request.DeviceLinkProfile != null && request.UseDLP == true)
                {
                    //Declare a byte array to load profile from the database 
                    byte[] profData1 = null;
                    profData1 = request.DeviceLinkProfile.ProfileData;

                    //copy byte array into unmangaed buffer (in memory profile)
                    Common.CopyProfileInMemory(profData1, ref srcProf);

                    //Set the source profile 
                    CrossXColor.CLSetOptionProfile(profiler, "SrcProfile", srcProf);
                }

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

                byte[] profData = Common.GetDLP(ref prof);

                for (int i = 0; i < n; i++)
                {
                    response.Cyan[i] = c[i];
                    response.Magenta[i] = m[i];
                    response.Yellow[i] = y[i];
                    response.Key[i] = k[i];
                }
                response.DeviceLinkProfile.ProfileData = profData;

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
            //return new CreateISOAlignmentCurveResponse();
        }
        public static CreateSourceProfileResponse CreateSourceProfile(CreateSourceProfileRequest request)
        {
            var response = new CreateSourceProfileResponse()
            {
                Profile = new IccProfile()
            };
            if (request.DCSData == null || request.DCSData.Length == 0)
            {
                response.Message = "DCSData not loaded";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            if (request.PCSData == null || request.PCSData.Length == 0)
            {
                response.Message = "PCSData not loaded";
                response.Severity = MessageSeverity.Error;
                return response;
            }


            TYPE_p_void profiler = CrossXColor.CLNewCLProfilerRef(); // this object referece to CroxColor Profiler object
            TYPE_p_void prof = CrossXColor.CLNewICCProfileRef();  // this object  references an ICC profile to access its data.
            TYPE_p_void srcProf = CrossXColor.CLNewICCProfileRef();

            try
            {
                var dcsDataSet = new CLVectorDataSet();
                var pcsDataSet = new CLVectorDataSet();
                CrossXColor.CLInitVectorDataSet(dcsDataSet, CrossXColor.icSigCmykData, request.DCSData, request.DCSData.Length);

                switch (request.PCSDataType)
                {
                    case PCSDataType.XYZ:
                        CrossXColor.CLInitVectorDataSet(pcsDataSet, CrossXColor.icSigXYZData, request.PCSData, request.PCSData.Length);
                        break;
                    case PCSDataType.Spectral:
                        CrossXColor.CLInitVectorDataSet(pcsDataSet, CrossXColor.clRemission, request.PCSData, request.PCSData.Length);
                        break;
                }

                if (request.PCSData.Length <= 0)
                {
                    response.Message = "Error - No PCS data loaded";
                    response.Severity = MessageSeverity.Error;
                    return response;
                }

                var err = CrossXColor.CLNoErr;
                request.ProfileOptions.ForEach((param) => err = CrossXColor.CLSetProfilerOption(profiler, param.Name, param.Value));
                err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujifilm Graphic Systems USA");
                CrossXColor.CLSetProfilerData(profiler, request.DCSData.Length / 4, dcsDataSet, pcsDataSet);

                if (request.SourceProfile != null || request.SourceProfile.ProfileData != null)
                {
                    Curve.CopyProfileInMemory(request.SourceProfile.ProfileData, ref srcProf);
                    err = CrossXColor.CLSetOptionProfile(profiler, "RefProfile", srcProf);
                }

                // calc profile in memory
                err = CrossXColor.CLCalcProfile(profiler, prof);
                if (err != 0)
                {
                    response.Message = Enum.GetName(typeof(CrossXColor.CLErrorType), err);
                    response.Severity = MessageSeverity.Error;
                    return response;
                }
                response.Profile.ProfileData = Curve.GetDLP(ref prof);
            }
            catch (Exception ex)
            {
                response.Message = String.Format("Error:{0}, Additional Info:{1}", ex.Message, ex.ToString());
                response.Severity = MessageSeverity.Error;
            }
            finally
            {
                //clear the unmanaged memory
                CrossXColor.CLFreeCLProfilerRef(profiler);
                CrossXColor.CLFreeICCProfileRef(prof);
                CrossXColor.CLFreeICCProfileRef(srcProf);
            }
            return response;
        }
        public static ExtractProfileValuesResponse ExtractProfileValues(ExtractProfileValuesRequest request)
        {
            var response = new ExtractProfileValuesResponse();

            if (request.ExtractType == ProfileExtractType.None)
            {
                response.Message = "Please specify extract type";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            if (request.Profile == null || request.Profile.ProfileData == null)
            {
                response.Message = "Please provide profile data";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            TYPE_p_void profiler = CrossXColor.CLNewCLProfilerRef(); // this object referece to CroxColor Profiler object
            TYPE_p_void prof = CrossXColor.CLNewICCProfileRef();  // this object  references an ICC profile to access its data.
            TYPE_p_void srcProf = CrossXColor.CLNewICCProfileRef();
            TYPE_p_void trafo = null;
            var renderingIntents = new int[] { CrossXColor.icAbsoluteColorimetric };

            try
            {
                var err = CrossXColor.CLNoErr;
                Curve.CopyProfileInMemory(request.Profile.ProfileData, ref srcProf);
                var profiles = new TYPE_p_void[1] { srcProf };
                trafo = CrossXColor.CLConcatICCProfileRefs(1, profiles, renderingIntents);
                if (trafo == null)
                {
                    response.Message = "CLConcatICCProfileRefs Error";
                    response.Severity = MessageSeverity.Error;
                    return response;
                }

                if (request.ExtractType == ProfileExtractType.Xyz)
                {
                    var input = new double[4];
                    var output = new double[4];
                    var x = new List<double>();
                    var y = new List<double>();
                    var z = new List<double>();
                    for (int i = 0; i < request.C_X.Length; i++)
                    {
                        input[0] = request.C_X[i];
                        input[1] = request.M_Y[i];
                        input[2] = request.Y_Z[i];
                        input[3] = request.K[i];

                        // Extract the desired datapoints from the Trafo, with the desired "IN" values this returns desired "OUT"
                        err = CrossXColor.CLDoTrafo(trafo, input, output);
                        if (err != 0)
                        {
                            response.Message = Enum.GetName(typeof(CrossXColor.CLErrorType), err);
                            response.Severity = MessageSeverity.Error;
                            return response;
                        }
                        x.Add(output[0]);
                        y.Add(output[1]);
                        z.Add(output[2]);
                    }
                    response.C_X = x.ToArray();
                    response.M_Y = y.ToArray();
                    response.Y_Z = z.ToArray();
                }
                else
                {
                    var input = new double[3];
                    var output = new double[4];
                    var c = new List<double>();
                    var m = new List<double>();
                    var y = new List<double>();
                    var k = new List<double>();
                    for (int i = 0; i < request.C_X.Length; i++)
                    {
                        input[0] = request.C_X[i];
                        input[1] = request.M_Y[i];
                        input[2] = request.Y_Z[i];
                        // Extract the desired datapoints from the Trafo, with the desired "IN" values this returns desired "OUT"
                        err = CrossXColor.CLDoTrafo(trafo, input, output);
                        if (err != 0)
                        {
                            response.Message = Enum.GetName(typeof(CrossXColor.CLErrorType), err);
                            response.Severity = MessageSeverity.Error;
                            return response;
                        }
                        c.Add(output[0]);
                        m.Add(output[1]);
                        y.Add(output[2]);
                        k.Add(output[3]);
                    }
                    response.C_X = c.ToArray();
                    response.M_Y = m.ToArray();
                    response.Y_Z = y.ToArray();
                    response.K = k.ToArray();
                }
            }
            catch (Exception ex)
            {
                response.Message = String.Format("Error:{0}, Additional Info:{1}", ex.Message, ex.ToString());
                response.Severity = MessageSeverity.Error;
            }
            finally
            {
                //clear the unmanaged memory
                CrossXColor.CLFreeCLProfilerRef(profiler);
                CrossXColor.CLFreeICCProfileRef(prof);
                CrossXColor.CLFreeCLTrafoRef(trafo);
                CrossXColor.CLFreeICCProfileRef(srcProf);
            }
            return response;
        }
        public static CreateSpotLibraryResponse CreateSpotLibraryValues(CreateSpotLibraryRequest request)
        {
            try
            {
                var response = new CreateSpotLibraryResponse();
                var ms = new MemoryStream();
                //Once the byte array is revcieved from the service it then needs to converted to astring
                //string str1 = System.Text.Encoding.UTF8.GetString(Pantonebytes, 0, Pantonebytes.Length);
                string strPCSData = System.Text.Encoding.UTF8.GetString(request.TargetProfile, 0, request.TargetProfile.Length);

                //if we wanted to save to disk
                int err = CrossXColor.CLNoErr;

                TYPE_p_void profiler = CrossXColor.CLNewCLProfilerRef();

                err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujiflm Graphic Systems USA");
                err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                err = CrossXColor.CLSetProfilerOption(profiler, "ProfileType", "NamedColors");

                //dont set DCS data this confused the profiler....
                // err = CrossXColor.CLSetProfilerOption(profiler, "DCSData", str1);
                err = CrossXColor.CLSetProfilerOption(profiler, "PCSData", strPCSData);
                request.ProfileOptions.ForEach((param) => err = CrossXColor.CLSetProfilerOption(profiler, param.Name, param.Value));
                //end of params


                // Define the Destination Profile
                //Here it read from disk
                //In the service it will be from memory as a byte array then converted to a profile

                TYPE_p_void destProf = CrossXColor.CLNewICCProfileRef();
                TYPE_p_p_void destSpec = CrossXColor.CLNewFileSpecRef();

                Curve.CopyProfileInMemory(request.DestinationProfile.ProfileData, ref destProf);
                err = CrossXColor.CLSetOptionProfile(profiler, "RefProfile", destProf);

                //Now we have the target file, the params and the destination profile
                // calc the Nmaed Color Profile in memory
                System.Console.WriteLine("Calculating NamedColorProfile in Memory");
                TYPE_p_void prof = CrossXColor.CLNewICCProfileRef();
                err = CrossXColor.CLCalcProfile(profiler, prof);
                System.Console.WriteLine("CLCalcProfile-Err = {0:d}", err);

                // //            // save profile to disc
                //Here it just an example
                //and a hacky way to open in the code to read the profile
                //The named color profile is currently in unmanaged memory, to use in C#
                //needs to copied to managed memeory to read in C#

                // TYPE_p_p_void NCPSpec = CrossXColor.CLNewFileSpecRef();
                // CrossXColor.CLName2FileSpec(NCPSpec, "NCP2patch.icc");
                // err = CrossXColor.CLWriteProfile(prof, NCPSpec, 0);

                response.namedColorProfile = Curve.GetDLP(ref prof);
                System.Console.WriteLine("CLWriteProfile-Error = {0:d}", err);

                //now we create the collection for reading the named color profile into
                List<Patch> listPatch = new List<Patch>();

                //just pass the byte array of the named color profile
                listPatch = ICCHelper.readICC(response.namedColorProfile);

                //Now we setup the transformation and the CMM where we can pass the 
                //CMYK through the Destination profile and get Predicted LAB values returned
                //here we still using destProf we are already have in unmanaged memory
                TYPE_p_void trafo = null;
                var renderingIntents = new int[] { CrossXColor.icAbsoluteColorimetric };

                var profiles = new TYPE_p_void[1] { destProf };
                trafo = CrossXColor.CLConcatICCProfileRefs(1, profiles, renderingIntents);
                if (trafo == null)
                {
                    //
                }

                var input = new double[4];
                var output = new double[3];
                //Now we iteraye through the listpatch pass CMYK value through CMM as IN
                //Get predicted LAB as OUT
                foreach (var patch in listPatch)
                {

                    input[0] = Convert.ToDouble(patch.Cmyk_C);
                    input[1] = Convert.ToDouble(patch.Cmyk_M);
                    input[2] = Convert.ToDouble(patch.Cmyk_Y);
                    input[3] = Convert.ToDouble(patch.Cmyk_K);

                    err = CrossXColor.CLDoTrafo(trafo, input, output);

                    patch.p_Cie_L = Math.Round(output[0], 4);
                    patch.p_Cie_a = Math.Round(output[1], 4);
                    patch.p_Cie_b = Math.Round(output[2], 4);
                }

                response.spotPatchDetail = listPatch;
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static OptimizeSpotLibraryResponse OptimizeSpotLibraryValues(OptimizeSpotLibraryRequest request)
        {
            try
            {
                var response = new OptimizeSpotLibraryResponse();
                var ms = new MemoryStream();

                //FOR OPTIMIZATION We are now sending measurement data in the same form as the target data
                //SAME Steps are used to convert this to a CGATS file
                //Once the byte array is revcieved from the service it then needs to converted to astring
                //string str1 = System.Text.Encoding.UTF8.GetString(Pantonebytes, 0, Pantonebytes.Length);
                string strPCSData = System.Text.Encoding.UTF8.GetString(request.TargetProfile, 0, request.TargetProfile.Length);

                //if we wanted to save to disk
                int err = CrossXColor.CLNoErr;

                TYPE_p_void profiler = CrossXColor.CLNewCLProfilerRef();

                err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujiflm Graphic Systems USA");
                err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                err = CrossXColor.CLSetProfilerOption(profiler, "ProfileType", "OptimizeColors");


                //dont set DCS data this confused the profiler....
                // err = CrossXColor.CLSetProfilerOption(profiler, "DCSData", str1);
                err = CrossXColor.CLSetProfilerOption(profiler, "PCSData", strPCSData);
                request.ProfileOptions.ForEach((param) => err = CrossXColor.CLSetProfilerOption(profiler, param.Name, param.Value));
                //end of params


                // Define the Destination Profile
                //Here it read from disk
                //In the service it will be from memory as a byte array then converted to a profile

                TYPE_p_void destProf = CrossXColor.CLNewICCProfileRef();
                //TYPE_p_p_void destSpec = CrossXColor.CLNewFileSpecRef();

                //Set the destination profile, please note the difference in the call from RefProfile to DstProfile in OPtimization
                Curve.CopyProfileInMemory(request.DestinationProfile.ProfileData, ref destProf);
                err = CrossXColor.CLSetOptionProfile(profiler, "DstProfile", destProf);

                TYPE_p_void srcProf = CrossXColor.CLNewICCProfileRef();
                //TYPE_p_p_void srcSpec = CrossXColor.CLNewFileSpecRef();

                //Set the Source Profile, the Source profile is the previous run created NamedColorprofile
                Curve.CopyProfileInMemory(request.previousNamedColorProfile.ProfileData, ref srcProf);
                err = CrossXColor.CLSetOptionProfile(profiler, "SrcProfile", srcProf);


                //Now we have the target file, the params and the destination profile
                // calc the Nmaed Color Profile in memory
                // System.Console.WriteLine("Calculating OPtimized NamedColorProfile in Memory");
                TYPE_p_void prof = CrossXColor.CLNewICCProfileRef();
                err = CrossXColor.CLCalcProfile(profiler, prof);
                //System.Console.WriteLine("CLCalcProfile-Err = {0:d}", err);

                // //            // save profile to disc
                //Here it just an example
                //and a hacky way to open in the code to read the profile
                //The named color profile is currently in unmanaged memory, to use in C#
                //needs to copied to managed memeory to read in C#

                // TYPE_p_p_void NCPSpec = CrossXColor.CLNewFileSpecRef();
                // CrossXColor.CLName2FileSpec(NCPSpec, "NCP2patch.icc");
                // err = CrossXColor.CLWriteProfile(prof, NCPSpec, 0);

                response.namedColorProfile = Curve.GetDLP(ref prof);
                //System.Console.WriteLine("CLWriteProfile-Error = {0:d}", err);

                //now we create the collection for reading the named color profile into
                List<Patch> listPatch = new List<Patch>();



                //just pass the byte array of the named color profile
                listPatch = ICCHelper.readICC(response.namedColorProfile);

                //With OPtimization there is NO CMM we just pass back the values from the Named Color Profile
                //

                response.spotPatchDetail = listPatch;

                CrossXColor.CLFreeCLProfilerRef(profiler);
                CrossXColor.CLFreeICCProfileRef(prof);
                CrossXColor.CLFreeICCProfileRef(srcProf);
                CrossXColor.CLFreeICCProfileRef(destProf);


                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static CreateDestinationAndDeviceLinkProfileResponse CreateDestinationAndDeviceLinkMultiColorProfile(CreateDestinationAndDeviceLinkProfileMultiColorRequest request)
        {
            var response = new CreateDestinationAndDeviceLinkProfileResponse()
            {
                DestinationProfile = new IccProfile(),
                DeviceLinkProfile = new IccProfile()
            };
            if (request.DcsData == null || request.DcsData.Length == 0)
            {
                response.Message = "DcsData not loaded";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            if (request.PcsData == null || request.PcsData.Length == 0)
            {
                response.Message = "PcsData not loaded";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            if (request.SourceProfile == null || request.SourceProfile.ProfileData == null)
            {
                response.Message = "Please provide source profile";
                response.Severity = MessageSeverity.Error;
                return response;
            }

            TYPE_p_void profiler = CrossXColor.CLNewCLProfilerRef(); // this object referece to CroxColor Profiler object
            TYPE_p_void prof = CrossXColor.CLNewICCProfileRef();  // this object  references an ICC profile to access its data.
            TYPE_p_void destProf = CrossXColor.CLNewICCProfileRef();
            TYPE_p_void srcProf = CrossXColor.CLNewICCProfileRef();

            try
            {
                #region Calculate destination profile
                var dcsDataSet = new CLVectorDataSet();
                var pcsDataSet = new CLVectorDataSet();
                if (request.TotalNoColors == 5)
                {
                    CrossXColor.CLInitVectorDataSet(dcsDataSet, CrossXColor.icSig5colorData, request.DcsData, request.DcsData.Length);
                }
                else if (request.TotalNoColors == 6)
                {
                    CrossXColor.CLInitVectorDataSet(dcsDataSet, CrossXColor.icSig6colorData, request.DcsData, request.DcsData.Length);
                }
                else if (request.TotalNoColors == 7)
                {
                    CrossXColor.CLInitVectorDataSet(dcsDataSet, CrossXColor.icSig7colorData, request.DcsData, request.DcsData.Length);
                }
                else if (request.TotalNoColors == 8)
                {
                    CrossXColor.CLInitVectorDataSet(dcsDataSet, CrossXColor.icSig8colorData, request.DcsData, request.DcsData.Length);
                }
                else if (request.TotalNoColors == 9)
                {
                    CrossXColor.CLInitVectorDataSet(dcsDataSet, CrossXColor.icSig9colorData, request.DcsData, request.DcsData.Length);
                }
                else
                {
                    CrossXColor.CLInitVectorDataSet(dcsDataSet, CrossXColor.icSigCmykData, request.DcsData, request.DcsData.Length);
                }
                

                switch (request.PCSDataType)
                {
                    case PCSDataType.XYZ:
                        CrossXColor.CLInitVectorDataSet(pcsDataSet, CrossXColor.icSigXYZData, request.PcsData, request.PcsData.Length);
                        break;
                    case PCSDataType.Spectral:
                        CrossXColor.CLInitVectorDataSet(pcsDataSet, CrossXColor.clRemission, request.PcsData, request.PcsData.Length);
                        break;
                }

                if (request.PcsData.Length <= 0)
                {
                    response.Message = "Error - No PCS data loaded";
                    response.Severity = MessageSeverity.Error;
                    return response;
                }

                var err = CrossXColor.CLNoErr;
                request.DestinationProfileOptions.ForEach((param) => err = CrossXColor.CLSetProfilerOption(profiler, param.Name, param.Value));
                err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujifilm Graphic Systems USA");
                err = CrossXColor.CLSetProfilerOption(profiler, "SaveProfilerData", "1");

                if (request.TotalNoColors > 4)
                {
                    CrossXColor.CLSetProfilerData(profiler, request.DcsData.Length / request.TotalNoColors, dcsDataSet, pcsDataSet);
                }
                else
                {
                    CrossXColor.CLSetProfilerData(profiler, request.DcsData.Length / 4, dcsDataSet, pcsDataSet);
                }

                // calc profile in memory
                err = CrossXColor.CLCalcProfile(profiler, destProf);
                if (err != 0)
                {
                    response.Message = Enum.GetName(typeof(CrossXColor.CLErrorType), err);
                    response.Severity = MessageSeverity.Error;
                    return response;
                }
                response.DestinationProfile.ProfileData = Curve.GetDLP(ref destProf);
                #endregion

                #region Calculate Device link profile
                //clear profiler memory...
                CrossXColor.CLFreeCLProfilerRef(profiler);
                profiler = CrossXColor.CLNewCLProfilerRef();
                request.ProfileOptions.ForEach((param) => err = CrossXColor.CLSetProfilerOption(profiler, param.Name, param.Value));
                err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujifilm Graphic Systems USA");

                Curve.CopyProfileInMemory(request.SourceProfile.ProfileData, ref srcProf);

                err = CrossXColor.CLSetOptionProfile(profiler, "SrcProfile", srcProf);
                err = CrossXColor.CLSetOptionProfile(profiler, "DstProfile", destProf);

                // calc profile in memory
                err = CrossXColor.CLCalcProfile(profiler, prof);
                if (err != 0)
                {
                    response.Message = Enum.GetName(typeof(CrossXColor.CLErrorType), err);
                    response.Severity = MessageSeverity.Error;
                    return response;
                }
                response.DeviceLinkProfile.ProfileData = Curve.GetDLP(ref prof);
                #endregion
            }
            catch (Exception ex)
            {
                response.Message = String.Format("Error:{0}, Additional Info:{1}", ex.Message, ex.ToString());
                response.Severity = MessageSeverity.Error;
            }
            finally
            {
                //clear the unmanaged memory
                CrossXColor.CLFreeCLProfilerRef(profiler);
                CrossXColor.CLFreeICCProfileRef(prof);
                CrossXColor.CLFreeICCProfileRef(srcProf);
                CrossXColor.CLFreeICCProfileRef(destProf);
            }
            return response;
        }
        
    }
}