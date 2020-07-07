using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CPProfilerService.Models.Helper
{
    public class ErrorLogTrack
    {
        public static void WriteErrorLog(string message)
        {
            string ErrorLogPath = Path.Combine(HttpContext.Current.Server.MapPath("~/"), "Log");

            string FileName = "Log_" + DateTime.Now.ToString("MM-dd-yyyy") + ".txt";
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