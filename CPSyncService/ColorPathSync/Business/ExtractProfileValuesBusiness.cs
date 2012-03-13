using ColorPathSync.MessageContracts;
using ColorPathSync.DataContracts;
using System;
using System.Collections.Generic;

namespace ColorPathSync.Business
{
    public partial class ProfilerBusiness
    {
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
    }
}