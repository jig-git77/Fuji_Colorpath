using FujiFilmSyncExt.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujiFilmSyncExt.Helper
{
    public class ICCHelper
    {
        public static List<Patch> readICC(DirectoryInfo dirInfo)
        {
            bool status;
            ICCFile iccFile = new ICCFile();
            //BinaryReader bs2 = new BinaryReader(File.Open(dirInfo.FullName, FileMode.Open));
            BinaryDataReader bs2 = new BinaryDataReader(File.Open(dirInfo.FullName, FileMode.Open));

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

            for (int patchID = 0; patchID <= iccFile.ncl2Count - 1; patchID++)
            {
                Patch patch = new Patch();
                patch.PatchID = patchID;
                byte[] charBytes = bs2.ReadBytes(32);


                string ColorName = System.Text.Encoding.UTF8.GetString(charBytes);

                //ColorName = Trim(ColorName, "");
                //int test = bs2.ReadUInt();
                pcsL = bs2.ReadUInt16() / 652.8;
                pcsA = bs2.ReadUInt16() / 652.8;
                pcsB = bs2.ReadUInt16() / 652.8;

                colorant1 = bs2.ReadUInt16() / 655.35;
                colorant2 = bs2.ReadUInt16() / 655.35;
                colorant3 = bs2.ReadUInt16() / 655.35;
                colorant4 = bs2.ReadUInt16() / 655.35;

                patch.ColorName = ColorName;
                patch.cie_L = pcsL;
                patch.cie_a = pcsA;
                patch.cie_b = pcsB;
                patch.spot1 = 0;
                patch.cmyk_C = colorant1;
                patch.cmyk_M = colorant2;
                patch.cmyk_Y = colorant3;
                patch.cmyk_K = colorant4;

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
