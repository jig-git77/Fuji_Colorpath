#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
#endregion

#region Otheres

#endregion

namespace Fuji.Common
{
    public class CommonFunctions
    {
        #region Page Redirect
        public static void PageRedirect(string Page, string QueryString)
        {
            try
            {
                System.Web.HttpContext.Current.Response.Redirect(Page + ".aspx?" + QueryString);
            }
            catch (Exception Exc)
            { Exc.Message.ToString(); }// For stopping the error to database.
        }
        #endregion

        #region Set Datagrid Style
        public static void SetDataGridDefaultSettings(System.Web.UI.WebControls.DataGrid DGrid)
        {
            DGrid.AllowPaging = true;
            DGrid.AllowSorting = true;
            DGrid.AutoGenerateColumns = false;
            DGrid.PageSize = 10;
            DGrid.PagerStyle.Mode = System.Web.UI.WebControls.PagerMode.NumericPages;
            DGrid.CssClass = "DATAGRID";
            DGrid.EditItemStyle.CssClass = "EDITITEMSTYLE";
            DGrid.FooterStyle.CssClass = "FOOTERSTYLE";
            DGrid.HeaderStyle.CssClass = "HEADERSTYLE";
            DGrid.SelectedItemStyle.CssClass = "SELECTEDITEMSTYLE";
            DGrid.AlternatingItemStyle.CssClass = "ALTERNATINGITEMSTYLE";
            DGrid.ItemStyle.CssClass = "ITEMSTYLE";
            DGrid.PagerStyle.CssClass = "PAGERSTYLE";
            DGrid.AlternatingItemStyle.CssClass = "ALTERNATINGITEMSTYLE";
        }
        #endregion

        #region Set GridView Style
        public static void SetGridViewDefaultSettings(System.Web.UI.WebControls.GridView GView)
        {
            GView.AllowPaging = true;
            GView.AllowSorting = true;
            GView.AutoGenerateColumns = false;
            GView.PageSize = 10;
            GView.PagerSettings.Mode = System.Web.UI.WebControls.PagerButtons.Numeric;
            GView.CssClass = "GRIDVIEW";
            GView.EditRowStyle.CssClass = "EDITROWSTYLE";
            GView.FooterStyle.CssClass = "FOOTERSTYLE";
            GView.HeaderStyle.CssClass = "HEADERSTYLE";
            GView.SelectedRowStyle.CssClass = "SELECTEDROWSTYLE";
            GView.AlternatingRowStyle.CssClass = "ALTERNATINGROWSTYLE";
            GView.RowStyle.CssClass = "ROWSTYLE";
            GView.PagerStyle.CssClass = "PAGERSTYLE";
        }
        #endregion

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
                //System.Web.HttpContext.Current.Response.End();
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception Exc)
            { string str = Exc.Message; }
        }
        #endregion

        #region Check User Role And Redirect to Home page as per user role
        /// <summary>
        /// Used to check user role
        /// </summary>
        public static void CheckUserRole()
        {
            //========== Checking the Add Right for the User Management ===========//
            

            if (
            Fuji.Common.Sessions.Access_Branding == true ||
            Fuji.Common.Sessions.Add_Branch == true ||
            Fuji.Common.Sessions.Add_Company == true ||
            Fuji.Common.Sessions.Add_Device == true ||
            Fuji.Common.Sessions.Add_Dongle == true ||
            Fuji.Common.Sessions.Add_Support_User == true ||
            Fuji.Common.Sessions.Add_Temp_RH_Device == true ||
            Fuji.Common.Sessions.Add_User == true ||
            Fuji.Common.Sessions.Delete_Branch == true ||
            Fuji.Common.Sessions.Delete_Company == true ||
            Fuji.Common.Sessions.Delete_Device == true ||
            Fuji.Common.Sessions.Delete_Dongle == true ||
            Fuji.Common.Sessions.Delete_Print_Target == true ||
            Fuji.Common.Sessions.Delete_Temp_RH_Device == true ||
            Fuji.Common.Sessions.Delete_User == true ||
            Fuji.Common.Sessions.Remove_Support_User == true ||
            Fuji.Common.Sessions.Update_Branch == true ||
            Fuji.Common.Sessions.Update_Device == true ||
            Fuji.Common.Sessions.Update_Dongle == true ||
            Fuji.Common.Sessions.Update_Temp_RH_Device == true ||
            Fuji.Common.Sessions.Update_User == true ||
            Fuji.Common.Sessions.Access_Video == true
            )
            {  }
            else
            { 
                Fuji.Common.CommonFunctions.PageRedirect("AccessDenied", "");
                return; 
            }
        }
        #endregion

        #region Convert Name To HTML Color
        /// <summary>
        /// Converts Passed colorname to HTML Color
        /// </summary>
        /// <param name="CoName">ColorName as CYAN, LIGHT CYAN</param>
        /// <returns>HEX HTML Color</returns>
        /// <remarks>Author:cshah@fujifilmgs.com</remarks>
        public static string ConvertNameToHTMLColor(string CoName )
        {

            string HTMLColor = "";
            string ColorName = null;
            int ColorCode = 0;

            ColorName = CoName.ToUpper();
            if (ColorName.Contains("LIGHT") == true)
            {
                if (ColorName.Contains("LIGHT LIGHT") == true)
                {ColorCode = 200;}
                else
                {ColorCode = 100;}

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

        #region Decimal Round Function
        // Ajay (Softweb) 1-Feb-2010
        public static Double DecimalRound(Double Del)
        {
            return System.Math.Round(Del, 2);
        }
        #endregion

        #region SetDateByCultures
        public static String GetDateByCultures(DateTime DT)
        {
            string[] strLanguages = System.Web.HttpContext.Current.Request.UserLanguages;
            CultureInfo[] cul = System.Globalization.CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            return DT.ToString("d", CultureInfo.CreateSpecificCulture(strLanguages[0].ToString()));
        }

        public static string DecimalToString(string Dec)
        {

            if (Convert.ToString(Dec) != "")
            {
                return Convert.ToDecimal(Dec).ToString("N2");
            }
            return "0";

        }

        public static string IntToString(string Dec)
        {
            if (Convert.ToString(Dec) != "")
            {
                return (Math.Round(Convert.ToDecimal(Dec))).ToString();
            }
            return "0";

        }
        #endregion

    }
}
