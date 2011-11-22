using System;
using System.Configuration;

namespace FujifilmUSA.Sync.Common
{
    public class Utilities
    {
        #region DownloadFile
        public static void DownlaodFile(string FolderPath, string FileName, string ShowOnDialogName)
        {
            try
            {
                //========== Download File ==========//
                System.IO.FileStream fs = System.IO.File.OpenRead(FolderPath + FileName);
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.AddHeader("Content-Type", "application/*");
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + ShowOnDialogName);
                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                fs.Close();
                System.Web.HttpContext.Current.Response.End();
                //System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception Exc)
            { string str = Exc.Message; }
        }
        #endregion

        #region Convert Name To HTML Color
        /// <summary>
        /// Converts Passed colorname to HTML Color
        /// </summary>
        /// <param name="CoName">ColorName as CYAN, LIGHT CYAN</param>
        /// <returns>HEX HTML Color</returns>
        /// <remarks>Author:cshah@fujifilmgs.com</remarks>
        public static string ConvertNameToHTMLColor(string CoName)
        {

            string HTMLColor = "";
            string ColorName = null;
            int ColorCode = 0;

            ColorName = CoName.ToUpper();
            if (ColorName.Contains("LIGHT") == true)
            {
                if (ColorName.Contains("LIGHT LIGHT") == true)
                { ColorCode = 200; }
                else
                { ColorCode = 100; }

            }
            else
            {
                ColorCode = 0;
            }

            if (ColorName.Contains("CYAN") == true)
            {
                ColorCode = ColorCode + 2;
            }
            else if (ColorName.Contains("MAGENTA") == true)
            {
                ColorCode = ColorCode + 4;
            }
            else if (ColorName.Contains("YELLOW") == true)
            {
                ColorCode = ColorCode + 8;
            }
            else if (ColorName.Contains("BLACK") == true)
            {
                ColorCode = ColorCode + 16;
            }
            else if (ColorName.Contains("WASTE") == true)
            {
                ColorCode = ColorCode + 32;
            }
            else if (ColorName.Contains("CUTTER") == true)
            {
                ColorCode = ColorCode + 64;
            }
            else if (ColorName.Contains("GREEN") == true)
            {
                ColorCode = ColorCode + 50;
            }
            else if (ColorName.Contains("ORANGE") == true)
            {
                ColorCode = ColorCode + 100;
            }


            switch (ColorCode)
            {
                //========== RGB Also Added ==========//
                case 32: //Waste tank
                    HTMLColor = "#996633"; //153 , 102 , 51
                    break;
                case 64: //CUTTER
                    HTMLColor = "#6B8E23"; // 107 , 142 , 35
                    break;
                case 2: //CYAN
                    HTMLColor = "#00FFFF"; // 0 , 255 , 255
                    break;
                case 102: //LIGHT CYAN
                    HTMLColor = "#40FFFF"; // 64 , 255 , 255
                    break;
                case 202: //LIGHT LIGHT CYAN
                    HTMLColor = "#BFFFFF"; // 191 , 255 , 255
                    break;
                case 4: //MAGENTA
                    HTMLColor = "#FF00FF"; // 255, 0 , 255
                    break;
                case 104: //LIGHT MAGENTA
                    HTMLColor = "#FF40FF"; // 255 , 64 , 255
                    break;
                case 204: //LIGHT LIGHT MAGENTA
                    HTMLColor = "#FFBFFF"; // 255 , 191 , 255
                    break;
                case 8: //YELLOW
                    HTMLColor = "#FFFF00"; // 255 , 255 , 0
                    break;
                case 108: //LIGHT YELLOW
                    HTMLColor = "#FFFF40"; // 255, 255, 64
                    break;
                case 208: //LIGHT LIGHT YELLOW
                    HTMLColor = "#FFFF7F"; // 255 , 255 , 127
                    break;
                case 16: //BLACK
                    HTMLColor = "#000000"; // 000 , 000 , 000
                    break;
                case 116: //LIGHT BLACK
                    HTMLColor = "#404040"; // 64 64 64
                    break;
                case 216: //LIGHT LIGHT BLACK
                    HTMLColor = "#BFBFBF"; // 191 , 191 , 191
                    break;
                case 50: //Green
                    HTMLColor = "#008000"; //0,255,0
                    break;
                case 100: //Orange
                    HTMLColor = "#FFA500";
                    break;
                default:
                    HTMLColor = "#FFFFFF"; // 255 , 255 , 255
                    break;
            }
            return HTMLColor;
        }
        #endregion

        /// <summary>
        /// Read configuration value from AppSettings of the config file
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReadConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static long DbValueToLong(object dbValue)
        {
            if (dbValue == DBNull.Value)
                return 0;
            else
            {
                try
                {
                    long value = Convert.ToInt64(dbValue);
                    return value;
                }
                catch { }
                return 0;
            }
        }

        public static int DbValueToInt(object dbValue)
        {
            if (dbValue == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(dbValue);
        }

        public static bool DbValueToBool(object dbValue)
        {
            if (dbValue == DBNull.Value)
                return false;
            else
                return Convert.ToBoolean(dbValue);
        }

        public static string DbValueToString(object dbValue)
        {
            if (dbValue == DBNull.Value)
                return "";
            else
                return Convert.ToString(dbValue);
        }
    }
}
