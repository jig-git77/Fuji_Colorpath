using ColorPathSync.DataContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ColorPathSync.Helper
{
    public class ICCHelper
    {
        public static List<Patch> readICC(byte[] ncProfile)
        {
            bool status;
            ICCFile iccFile = new ICCFile();

            MemoryStream stream = new MemoryStream();
            stream.Write(ncProfile, 0, ncProfile.Length);
            BinaryDataReader bs2 = new BinaryDataReader(stream);

            MemoryStream mb2 = new MemoryStream();
            bs2.BaseStream.CopyTo(mb2);

            status = iccFile.readHeader(bs2, mb2);

            status = iccFile.readTAG(bs2, mb2);

            status = iccFile.readNcl2TAG(bs2, mb2);


            bs2.BaseStream.Position = bs2.BaseStream.Position + 64;

            double pcsL = 0;
            double pcsA = 0;
            double pcsB = 0;
            double colorant1 = 0;
            double colorant2 = 0;
            double colorant3 = 0;
            double colorant4 = 0;
            double colorant5 = 0;
            double colorant6 = 0;
            double colorant7 = 0;

            //DataTable dt = new DataTable();
            //dt.Columns.Add("PatchID", typeof(int));
            //dt.Columns.Add("ColorName", typeof(int));

            List<Patch> listPatch = new List<Patch>();

            for (int patchID = 1; patchID <= iccFile.ncl2Count; patchID++)
            {
                Patch patch = new Patch();
                patch.PatchID = patchID;
                byte[] charBytes = bs2.ReadBytes(32);


                string ColorName = System.Text.Encoding.UTF8.GetString(charBytes);

                //ColorName = Trim(ColorName, "");
                //int test = bs2.ReadUInt();
                pcsL = bs2.ReadUInt16() / 652.8;
                pcsA = (bs2.ReadUInt16() / 256.0) - 128.0;
                pcsB = (bs2.ReadUInt16() / 256.0) - 128.0;

                colorant1 = bs2.ReadUInt16() / 655.35;
                colorant2 = bs2.ReadUInt16() / 655.35;
                colorant3 = bs2.ReadUInt16() / 655.35;
                colorant4 = bs2.ReadUInt16() / 655.35;

                patch.ColorName = ColorName.TrimEnd('\0');
                patch.Cie_L = Math.Round(pcsL, 4);
                patch.Cie_a = Math.Round(pcsA, 4);
                patch.Cie_b = Math.Round(pcsB, 4);
                patch.Spot1 = 0;
                patch.Cmyk_C = Math.Round(colorant1, 4);
                patch.Cmyk_M = Math.Round(colorant2, 4);
                patch.Cmyk_Y = Math.Round(colorant3, 4);
                patch.Cmyk_K = Math.Round(colorant4, 4);

                listPatch.Add(patch);
            }
            bs2.Close();

            return listPatch;
        }

        public static string Trim(string source, string charsToTrim)
        {
            int srcLen = source.Length;
            int i, leftPos, rightPos;
            for (i = 0; i < srcLen; i++)
            {
                if (charsToTrim.IndexOf(Mid(charsToTrim, i, 1)) == 0) break;
            }
            leftPos = i;
            if (leftPos > srcLen)
                return "";

            for (i = srcLen - 1; i >= 0; i--)
            {
                if (charsToTrim.IndexOf(Mid(charsToTrim, i, 1)) == 0) break;
            }
            rightPos = i;

            return Mid(source, leftPos, rightPos - leftPos + 1);

        }

        public static string Mid(string input, int index, int length)
        {

            string temp = input.Substring(index, length);
            return temp;
        }
    }
}