using System;
using ColorPathSync.DataContracts;
using ColorPathSync.MessageContracts;

namespace ColorPathSync.Business
{
    public partial class ProfilerBusiness
    {
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
                CrossXColor.CLInitVectorDataSet(pcsDataSet, CrossXColor.icSigXYZData, request.PCSData, request.PCSData.Length);

                if (request.PCSData.Length <= 0)
                {
                    response.Message = "Error - No PCS data loaded";
                    response.Severity = MessageSeverity.Error;
                    return response;
                }

                var err = CrossXColor.CLNoErr;
                request.Options.ForEach((param) => err = CrossXColor.CLSetProfilerOption(profiler, param.Name, param.Value));
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
    }
}