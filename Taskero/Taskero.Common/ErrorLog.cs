using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Taskero.Common
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

        public static void WriteJsonFile(string message, string projectID, string runID)
        {
            string ErrorLogPath = Path.Combine(HttpContext.Current.Server.MapPath("~/"), "JsonTemp");

            string FileName = projectID + "_" + runID + "_" + DateTime.Now.ToString("MM-dd-yyyy") + ".json";
            string FilePath = Path.Combine(ErrorLogPath, FileName);

            if (!Directory.Exists(ErrorLogPath))
                Directory.CreateDirectory(ErrorLogPath);
            if (!File.Exists(FilePath))
            {
                var myfile = File.Create(FilePath);
                myfile.Close();
            }
            StreamWriter sw = new StreamWriter(FilePath, false);
            sw.WriteLine(message);
            sw.Flush();
            sw.Close();
        }
    }
}
