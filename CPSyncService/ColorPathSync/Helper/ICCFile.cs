using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ColorPathSync.Helper
{
    public class ICCFile
    {
        //attributes
        public UInt32 CmmType { get; set; }
        public int ColorSize { get; set; }
        public UInt32 ColorSpace { get; set; }
        public int Creator { get; set; }
        public string dateTime { get; set; }
        public int Device { get; set; }
        public int DeviceAttr { get; set; }
        public int Flags { get; set; }
        public uint magicNumber { get; set; }
        public UInt32 ncl2Count { get; set; }
        public UInt32 ncl2DCScount { get; set; }
        public UInt32 ncl2TagLeng { get; set; }
        public UInt32 ncl2TagLoc { get; set; }
        public int PCS { get; set; }
        public int Platform { get; set; }
        public UInt32 ProfileClass { get; set; }
        public UInt32 ProfileSize { get; set; }
        public UInt32 ProfileVersion { get; set; }
        public int RenderingIntent { get; set; }
        public UInt32 tagcount { get; set; }
        public int Xilluminant { get; set; }
        public int Yilluminant { get; set; }
        public int Zilluminant { get; set; }

        //constant
        //public const string ncl2 = "&h6E636C32";
        //public const string Sig5colorData = "&h35434C52";
        //public const string Sig6colorData = "&h36434C52";
        //public const string Sig7colorData = "&h37434C52";
        //public const string SigcmykData = "&h434D594B";
        //public const string SigMagicNumber = "&h61637370";
        //public const string SigNamedColor = "&h6e6d636c";

        public const int ncl2 = 0X6E636C32;
        public const int Sig5colorData = 0x35434C52;
        public const int Sig6colorData = 0x36434C52;
        public const int Sig7colorData = 0x37434C52;
        public const int SigcmykData = 0x434D594B;
        public const int SigMagicNumber = 0x61637370;
        public const int SigNamedColor = 0x6e6d636c;


        //methods

        public bool readHeader(BinaryDataReader bs, MemoryStream ms)
        {

            bs.BaseStream.Position = 36;
            //magicNumber = bs.ReadUInt32();
            magicNumber = bs.ReadUInt32();

            if (magicNumber == SigMagicNumber)
            {
                bs.BaseStream.Position = 8;
                ProfileVersion = bs.ReadUInt32();
                ProfileClass = bs.ReadUInt32();

                if (ProfileClass == SigNamedColor)
                {
                    ColorSpace = bs.ReadUInt32();
                    switch (ColorSpace)
                    {
                        case SigcmykData:
                            ColorSize = 4;
                            break;
                        case Sig5colorData:
                            ColorSize = 5;
                            break;
                        case Sig6colorData:
                            ColorSize = 6;
                            break;
                        case Sig7colorData:
                            ColorSize = 7;
                            break;
                        default:
                            return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool readTAG(BinaryDataReader bs2, MemoryStream ms)
        {
            bs2.BaseStream.Position = 128;
            tagcount = bs2.ReadUInt32();
            UInt32 tagname = default(UInt32);

            for (int cnt = 1; cnt <= tagcount; cnt++)
            {
                tagname = bs2.ReadUInt32();
                if (tagname == ncl2)
                {
                    ncl2TagLoc = bs2.ReadUInt32();
                    ncl2TagLeng = bs2.ReadUInt32();
                    return true;
                }
                else
                {
                    bs2.BaseStream.Position = bs2.BaseStream.Position + 8;
                }


            }

            return false;

            //return true;
        }

        public bool readNcl2TAG(BinaryDataReader bs2, MemoryStream ms)
        {
            bs2.BaseStream.Position = ncl2TagLoc + 12;
            ncl2Count = bs2.ReadUInt32();
            ncl2DCScount = bs2.ReadUInt32();
            return true;
        }
    }
}