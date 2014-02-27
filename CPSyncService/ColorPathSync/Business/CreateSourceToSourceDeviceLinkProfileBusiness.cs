using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ColorPathSync.MessageContracts;
using ColorPathSync.DataContracts;

namespace ColorPathSync.Business
{
    public partial class ProfilerBusiness
    {
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
    }
}