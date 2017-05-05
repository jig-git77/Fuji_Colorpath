using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujiFilmSyncExt.Helper
{
    public class PDFHelper
    {
        //public BinaryReader b { get; set; }
        public BinaryDataWriter b { get; set; }
        public int n { get; set; }
        //public IEnumerable offsets {get;set;}
        public List<string> xref { get; set; }
        public FileStream outFileStream;

        public PDFHelper(char orientation = 'P', string unit = "mm", string format = "A4", double pWidth = 0, double pHeigth = 0)
        {
            this.n = 0;
            xref = new List<string>();
            //b = new MemoryStream();
        }

        public void write(string s)
        {
            s = s + (char)10;
            this.b.Write(s);
        }

        public void putHeader()
        {
            string tmpstr = "";
            int chars = 0;
            write("%PDF-1.4");
            chars = 0x25;
            tmpstr = Convert.ToByte(chars).ToString();
            chars = 0xE2;
            tmpstr = tmpstr + Convert.ToByte(chars).ToString();
            chars = 0xE3;
            tmpstr = tmpstr + Convert.ToByte(chars).ToString();
            chars = 0xCF;
            tmpstr = tmpstr + Convert.ToByte(chars).ToString();
            chars = 0xD3;
            tmpstr = tmpstr + Convert.ToByte(chars).ToString();
            write(tmpstr);
        }

        public void newObject()
        {
            this.n = this.n + 1;
            this.xref.Add(FormatNumber(this.b.BaseStream.Position.ToString()));
            write(this.n + " 0 obj");
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
            this.write("endobj");
        }

        public MemoryStream CreateColorStream()
        {
            //try
            //{
            //    Color NewColor = new Color();
            //    Color UpdateColor = new Color();
            //    Color qUpdateColor = new Color();
            //    Dictionary ColorDict = new Dictionary();
            //    Dictionary StackColorDict = new Dictionary();
            //    int colorID = 0;
            //    int stackcount = 0;
            //    int stacksize = 0;
            //    string vString = chrB(0) + chrB(0) + chrB(0) + chrB(0);

            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
            return null;
        }

        public void writeString(string str)
        {
            Byte[] buffer = null;
            buffer = ASCIIEncoding.ASCII.GetBytes(str);
            outFileStream.Write(buffer, 0, buffer.Length);
        }

    }

}
