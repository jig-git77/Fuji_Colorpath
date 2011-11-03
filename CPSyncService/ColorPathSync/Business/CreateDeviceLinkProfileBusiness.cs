using System;
using ColorPathSync.DataContracts;
using ColorPathSync.MessageContracts;

namespace ColorPathSync.Business
{
    internal partial class ProfilerBusiness
    {
        public static CreateDeviceLinkProfileResponse CreateDeviceLinkProfile(CreateDeviceLinkProfileRequest request)
        {
            var response = new CreateDeviceLinkProfileResponse()
            {
                DeviceLinkProfile = new IccProfile()
            };
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

            TYPE_p_void profiler = CrossXColor.CLNewCLProfilerRef(); // this object referece to CroxColor Profiler object
            TYPE_p_void prof = CrossXColor.CLNewICCProfileRef();  // this object  references an ICC profile to access its data.
            TYPE_p_void srcProf = CrossXColor.CLNewICCProfileRef();
            TYPE_p_void destProf = CrossXColor.CLNewICCProfileRef();

            try
            {
                var err = CrossXColor.CLNoErr;
                request.Options.ForEach((param) => err = CrossXColor.CLSetProfilerOption(profiler, param.Name, param.Value));
                err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujifilm Graphic Systems USA");

                Curve.CopyProfileInMemory(request.SourceProfile.ProfileData, ref srcProf);
                Curve.CopyProfileInMemory(request.DestinationProfile.ProfileData, ref destProf);

                err = CrossXColor.CLSetProfilerOption(profiler, "SrcProfile", "C:\\Data\\Projects1\\Fujifilm\\CPSyncService\\ColorPathSyncTest\\DataFiles\\gracol2006_320_gcrV4Z2.icc");
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