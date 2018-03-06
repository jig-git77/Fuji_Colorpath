using ColorPathSync.DataContracts;
using ColorPathSync.Helper;
using ColorPathSync.MessageContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ColorPathSync.Business
{
    public class ExtractSpotLABBusiness
    {
        public static ExtractSpotLABResponse ExtractSpotLABValues(ExtractSpotLABRequest request)
        {
            try
            {
                var response = new ExtractSpotLABResponse();
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
                TYPE_p_p_void NCPSpec = CrossXColor.CLNewFileSpecRef();
                CrossXColor.CLName2FileSpec(NCPSpec, "NCP2patch.icc");
                err = CrossXColor.CLWriteProfile(prof, NCPSpec, 0);

                response.namedColorProfile = Curve.GetDLP(ref prof);
                System.Console.WriteLine("CLWriteProfile-Error = {0:d}", err);

                //now we create the collection for reading the named color profile into
                List<Patch> listPatch = new List<Patch>();

                //just pass the path to the saved named color profile
                //this is a hack should be passed in memeory as byte array in managed way
                listPatch = ICCHelper.readICC("NCP2patch.icc");

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
    }
}