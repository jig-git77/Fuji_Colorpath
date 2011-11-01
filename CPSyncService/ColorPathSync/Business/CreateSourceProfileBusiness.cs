using ColorPathSync.MessageContracts;
using ColorPathSync.DataContracts;
using System;
using System.ServiceModel;

namespace ColorPathSync.Business
{
    internal partial class ProfilerBusiness
    {
        public static CreateSourceProfileResponse CreateSourceProfile(CreateSourceProfileRequest request)
        {
            var response = new CreateSourceProfileResponse()
                               {
                                   Profile = new IccProfile()
                               };
            TYPE_p_void profiler = CrossXColor.CLNewCLProfilerRef(); // this object referece to CroxColor Profiler object
            TYPE_p_void prof = CrossXColor.CLNewICCProfileRef();  // this object  references an ICC profile to access its data.

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
                //err = CrossXColor.CLSetProfilerOption(profiler, "ProfileType", "LinearizationDeviceLink");
                //err = CrossXColor.CLSetProfilerOption(profiler, "LinearizationType", "G7");
                err = CrossXColor.CLSetProfilerOption(profiler, "Creator", "Fuji");
                err = CrossXColor.CLSetProfilerOption(profiler, "Copyright", "Copyright Fujifilm Graphic Systems USA");
                CrossXColor.CLSetProfilerData(profiler, request.DCSData.Length / 4, dcsDataSet, pcsDataSet);

                // calc profile in memory
                err = CrossXColor.CLCalcProfile(profiler, prof);
                if (err != 0)
                {
                    throw new FaultException(Enum.GetName(typeof(CrossXColor.CLErrorType), err));
                }
                response.Profile.Profile = Curve.GetDLP(ref prof);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.ToString());
            }
            finally
            {
                //clear the unmanaged memory
                CrossXColor.CLFreeCLProfilerRef(profiler);
                CrossXColor.CLFreeICCProfileRef(prof);
            }
            return response;
        }
    }
}