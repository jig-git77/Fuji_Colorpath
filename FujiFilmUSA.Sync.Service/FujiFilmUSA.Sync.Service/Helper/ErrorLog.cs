using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FujiFilmUSA.Sync.Service.Helper
{
    public class ErrorLog
    {
        public static void WriteErrorLog(string message)
        {
            string FileName = "Log_" + DateTime.Now.ToString("MM-dd-yyyy") + ".txt";
            string ErrorLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/bin/", "Log");
            string FilePath = Path.Combine(ErrorLogPath, FileName);

            if (!Directory.Exists(ErrorLogPath))
                Directory.CreateDirectory(ErrorLogPath);
            if (!File.Exists(FilePath))
            {
                var myfile = File.Create(FilePath);
                myfile.Close();
            }

            StreamWriter sw = new StreamWriter(FilePath, true);
            sw.WriteLine("==============  " + DateTime.Now + "  =============");
            sw.WriteLine(" ");
            sw.WriteLine(message);
            sw.WriteLine(" ");
            sw.WriteLine("====================================================");
            sw.WriteLine(" ");
            sw.Flush();
            sw.Close();
        }
    }
}