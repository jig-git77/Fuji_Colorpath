using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;

namespace CPProfilerService.Business
{
    public partial class Common
    {
        internal static byte[] GetDLP(ref TYPE_p_void prof)
        {
            IntPtr pAddr = IntPtr.Zero;
            try
            {
                int err = CrossXColor.CLNoErr;

                // write profile in memory and then save in the database
                int profSize = 0;
                err = CrossXColor.CLWriteProfileGetSize(prof, ref profSize);
                // now alloc unmanaged buffer and write profile to it
                pAddr = Marshal.AllocHGlobal(profSize);
                err = CrossXColor.CLWriteProfileToMemory(prof, pAddr, profSize);
                if (err != 0)
                {
                    throw new Exception(Enum.GetName(typeof(CrossXColor.CLErrorType), err));
                }

                // alloc a managed array and copy profile data to it 
                var profData = new byte[profSize];
                Marshal.Copy(pAddr, profData, 0, profSize);
                return profData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (pAddr != IntPtr.Zero)
                {
                    // free the unmanaged buffer 
                    Marshal.FreeHGlobal(pAddr);
                }
            }
        }

        /// <summary>
        /// Copy profile from byte array (Managed .NET) to unmanaged memory buffer (C++) 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        internal static void CopyProfileInMemory(byte[] profile, ref TYPE_p_void srcProf)
        {
            IntPtr pAddr = IntPtr.Zero;
            Int32 profSize = profile.Length;

            try
            {
                int err = CrossXColor.CLNoErr;

                pAddr = Marshal.AllocHGlobal(profSize);
                Marshal.Copy(profile, 0, pAddr, profSize);

                err = CrossXColor.CLReadProfileFromMemory(srcProf, pAddr, profSize);
                if (err != 0)
                {
                    throw new Exception(Enum.GetName(typeof(CrossXColor.CLErrorType), err));
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {

                if (pAddr != IntPtr.Zero)
                {
                    // free the unmanaged buffer 
                    Marshal.FreeHGlobal(pAddr);
                }
            }

        }
    }
}