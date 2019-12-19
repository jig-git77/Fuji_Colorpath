using CPProfilerService.Models.Entity;
using CPProfilerService.Models.Request;
using CPProfilerService.Models.Response;
using System;
using System.Collections.Generic;
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
    }
}