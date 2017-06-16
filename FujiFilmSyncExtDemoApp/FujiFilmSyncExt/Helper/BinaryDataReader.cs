using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujiFilmSyncExt.Helper
{
    public class BinaryDataReader : BinaryReader
    {
        private byte[] a16 = new byte[2];
        private byte[] a32 = new byte[4];
        private byte[] a64 = new byte[8];
        public BinaryDataReader(System.IO.Stream stream) : base(stream) { }
        public override int ReadInt32()
        {
            a32 = base.ReadBytes(4);
            Array.Reverse(a32);
            return BitConverter.ToInt32(a32, 0);
        }

        //public Int16 readInt161()
        //{
        //    return base.ReadUInt16();
        //} 
        public override ushort ReadUInt16()
        {
            a16 = base.ReadBytes(2);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(a16);
            return BitConverter.ToUInt16(a16, 0);

            //byte[] b = ReadBytes(2);
            //return (short)(b[1] + (b[0] << 8));
        }
        public Int64 ReadInt64()
        {
            a64 = base.ReadBytes(8);
            Array.Reverse(a64);
            return BitConverter.ToInt64(a64, 0);
        }
        public UInt32 ReadUInt32()
        {
            a32 = base.ReadBytes(4);
            Array.Reverse(a32);
            return BitConverter.ToUInt32(a32, 0);
        }

    }
}
