using DocumentFormat.OpenXml.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujiFilmUSA.Sync.Service.Models
{
    public class BinaryFunction
    {
        public byte bt0xF = 0xF;
        public byte bt0x2 = 0x2;
        public byte bt0x30 = 0x01;
        public ushort usrt0105 = 0x0105;

        public byte[] GetBytesDetails(byte btValue , Int32 intLength)
        {
            byte[] btReturn = new byte[intLength]; ;
            btReturn[0] = btValue;
            return btReturn;
        }

        public float ConvertToFloat(double dbValue)
        {
            return float.Parse(dbValue.ToString());
        }
    }
}