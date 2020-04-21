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
    public class OptimizeSpotLibraryBusiness
    {
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
    }
}