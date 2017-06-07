using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;

namespace FujiFilmUSA.Sync.Service.Helper
{
    public class PdfHelper
    {
        public BinaryDataWriter b { get; set; }
        public int n { get; set; }
        public List<string> xref { get; set; }
        public FileStream outFileStream;
        internal PdfHelper Dictionary;			// indirect objects dictionary or stream dictionary

        public PdfHelper(char orientation = 'P', string unit = "mm", string format = "A4", double pWidth = 0, double pHeigth = 0)
        {
            this.n = 0;
            xref = new List<string>();
        }

        public void write(string s)
        {
            s = s + (char)10;
            this.b.Write(s);
        }

        public void WriteString(String Str)
        {
            this.b.WriteString(Str);
        }

        public int writeStringSize(String Str)
        {
            Byte[] ByteArray = new Byte[Str.Length];

            // convert content from string to binary
            // do not use Encoding.ASCII.GetBytes(...)
            for (Int32 Index = 0; Index < ByteArray.Length; Index++) ByteArray[Index] = (Byte)Str[Index];

            // write to pdf file
            WriteString(Str);
            return ByteArray.Length;
        }

        public async void WriteByteArray(byte[] ByteArray)
        {
            byte[] ByteArray2;
            ByteArray2 = CompressStream(ByteArray);
            this.b.WriteByteArray(ByteArray2);
        }

        public void putHeader()
        {
            // write PDF version number
            WriteString("%PDF-1.4\n");

            // add this comment to tell compression programs that this is a binary file
            WriteString("%\u00e2\u00e3\u00cf\u00d3\n");
        }

        public void newObject()
        {
            this.n = this.n + 1;
            this.xref.Add(FormatNumber(this.b.BaseStream.Position.ToString()));
            WriteString(this.n + " 0 obj\n");
        }

        public void writeXrefs()
        {
            for (Int32 i = 0; i < this.xref.Count; i++)
                WriteString(this.xref[i] + " 00000 n\n");
            // WriteString(" 00000 n\n");
        }

        public String currentPosition()
        {
            return this.b.BaseStream.Position.ToString();
        }

        public static string FormatNumber(string num)
        {
            string format = num;
            for (int i = num.Length; i < 10; i++)
                format = "0" + format;

            return format;
        }

        public void endObject()
        {
            WriteString("endobj\n");
        }

        public void resetPDFObjectValues()
        {
            this.n = 0;
            xref.Clear();
        }


        public void writeString(string str)
        {
            Byte[] buffer = null;
            buffer = ASCIIEncoding.ASCII.GetBytes(str);
            outFileStream.Write(buffer, 0, buffer.Length);
        }



        internal Byte[] CompressStream(Byte[] InputBuf)
        {
            try
            {


                // input length
                Int32 InputLen = InputBuf.Length;

                // input buffer too small to compress
                if (InputLen < 16) return (InputBuf);

                // create output memory stream to receive the compressed buffer
                MemoryStream OutputStream = new MemoryStream();

                // deflate compression object
                DeflateStream Deflate = new DeflateStream(OutputStream, CompressionMode.Compress, true);

                // load input buffer into the compression class
                Deflate.Write(InputBuf, 0, InputBuf.Length);

                // compress, flush and close
                Deflate.Close();

                // compressed file length
                Int32 OutputLen = (Int32)OutputStream.Length;

                // make sure compressed stream is shorter than input stream
                if (OutputLen + 6 >= InputLen) return (InputBuf);

                // create output buffer
                Byte[] OutputBuf = new Byte[OutputLen + 6];

                // write two bytes in most significant byte first
                OutputBuf[0] = (Byte)0x78;
                OutputBuf[1] = (Byte)0x9c;

                // copy the compressed result
                OutputStream.Seek(0, SeekOrigin.Begin);
                OutputStream.Read(OutputBuf, 2, OutputLen);
                OutputStream.Close();

                // reset adler32 checksum
                UInt32 ReadAdler32 = Adler32Checksum(InputBuf);

                // ZLib checksum is Adler32 write it big endian order, high byte first
                OutputLen += 2;
                OutputBuf[OutputLen++] = (Byte)(ReadAdler32 >> 24);
                OutputBuf[OutputLen++] = (Byte)(ReadAdler32 >> 16);
                OutputBuf[OutputLen++] = (Byte)(ReadAdler32 >> 8);
                OutputBuf[OutputLen] = (Byte)ReadAdler32;


                // successful exit
                return (OutputBuf);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog("From CompressStream : " + ex.Message);
                throw;
            }
        }

        /////////////////////////////////////////////////////////////////////
        // Accumulate Adler Checksum
        /////////////////////////////////////////////////////////////////////

        private UInt32 Adler32Checksum(Byte[] Buffer)
        {
            const UInt32 Adler32Base = 65521;

            // split current Adler checksum into two 
            UInt32 AdlerLow = 1; // AdlerValue & 0xFFFF;
            UInt32 AdlerHigh = 0; // AdlerValue >> 16;

            Int32 Len = Buffer.Length;
            Int32 Pos = 0;
            while (Len > 0)
            {
                // We can defer the modulo operation:
                // Under worst case the starting value of the two halves is 65520 = (AdlerBase - 1)
                // each new byte is maximum 255
                // The low half grows AdlerLow(n) = AdlerBase - 1 + n * 255
                // The high half grows AdlerHigh(n) = (n + 1)*(AdlerBase - 1) + n * (n + 1) * 255 / 2
                // The maximum n before overflow of 32 bit unsigned integer is 5552
                // it is the solution of the following quadratic equation
                // 255 * n * n + (2 * (AdlerBase - 1) + 255) * n + 2 * (AdlerBase - 1 - UInt32.MaxValue) = 0
                Int32 n = Len < 5552 ? Len : 5552;
                Len -= n;
                while (--n >= 0)
                {
                    AdlerLow += (UInt32)Buffer[Pos++];
                    AdlerHigh += AdlerLow;
                }
                AdlerLow %= Adler32Base;
                AdlerHigh %= Adler32Base;
            }
            return ((AdlerHigh << 16) | AdlerLow);
        }
    }
}