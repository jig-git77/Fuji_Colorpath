using FujiFilmUSA.Sync.Service.App_Start;
using FujiFilmUSA.Sync.Service.Helper;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;

[assembly: OwinStartup(typeof(StartUp))]
namespace FujiFilmUSA.Sync.Service.App_Start
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
                // ErrorLog.WriteErrorLog("Owin Start");
                double inteval = Convert.ToDouble(ConfigurationManager.AppSettings["ExecutionMinuteInterval"]);
                Timer timer = new Timer();
                timer.Elapsed += async (sender, e) => await DeleteFiles();
                timer.Start();
                
                
            }
            catch(Exception ex)
            {
                ErrorLog.WriteErrorLog("Error in StartUp: "+ex.Message.ToString());
            }
        }
        public static async Task<string> DeleteFiles()
        {
            try
            {
                var ZipfileDirPath = System.Web.Hosting.HostingEnvironment.MapPath(ConfigurationManager.AppSettings["ZipDirectoryPath"]);
                DirectoryInfo directory = new DirectoryInfo(ZipfileDirPath);
                if (directory.Exists)
                {
                    var files = directory.GetFiles();
                    if (files != null)
                        if (files.Length > 0)
                            foreach (var item in files)
                                if (item.CreationTime < DateTime.Now.AddDays(Convert.ToInt16("-" + ConfigurationManager.AppSettings["RetentionDays"])))
                                    item.Delete();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            await Task.Delay(1000000);
            return "";
        }
    }
}