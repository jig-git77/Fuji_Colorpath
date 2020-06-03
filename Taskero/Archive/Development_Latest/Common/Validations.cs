#region System
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Fuji.Common
{
    public class Validations
    {
        #region Constructor
        public Validations()
        { }
        #endregion

        #region IsInt16
        public static bool IsInt16(string Val)
        {
            try
            {
                int i = Convert.ToInt16(Val);
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region IsInt32
        public static bool IsInt32(string Val)
        {
            try
            {
                int i = Convert.ToInt32(Val);
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region IsInt64
        public static bool IsInt64(string Val)
        {
            try
            {
                long i = Convert.ToInt64(Val);
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region IsDouble
        public static bool IsDouble(string Val)
        {
            try
            {
                double i = Convert.ToDouble(Val);
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region IsDecimal
        public static bool IsDecimal(string Val)
        {
            try
            {
                decimal i = Convert.ToDecimal(Val);
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region IsDateTime
        public static bool IsDateTime(string Val)
        {
            try
            {
                DateTime i = Convert.ToDateTime(Val);
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region IsEmail
        public static bool IsEmail(string Val)
        {
            System.Text.RegularExpressions.Regex r;
            r = new System.Text.RegularExpressions.Regex("\\w+([-+.\']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
            return r.Match(Val).Success;
        }
        #endregion

        #region IsUrl
        public static bool IsUrl(string Val)
        {
            if (Val != null || Val == "")
            {
                if (Val.ToUpper().IndexOf("HTTP" + "://", 0) == -1)
                    return false;
                if (Val.IndexOf(".") == -1)
                    return false;
                if (Val.IndexOf(".") == 7)
                    return false;
                if ((Val.IndexOf(".") + 2) > Val.Length)
                    return false;
                for (int i = 0; i < Val.Length; i++)
                {
                    if (Convert.ToChar(Val.Substring(i, 1)) >= 97 && Convert.ToChar(Val.Substring(i, 1)) <= 122) // a=97 ,z=122
                    { }
                    else if (Convert.ToChar(Val.Substring(i, 1)) == 58) 	// :=58					
                    { }
                    else if (Convert.ToChar(Val.Substring(i, 1)) >= 46 && Convert.ToChar(Val.Substring(i, 1)) <= 57) // .=46. /=47  0 to 9	,0=48 & 9=57					
                    { }
                    else if (Convert.ToChar(Val.Substring(i, 1)) >= 65 && Convert.ToChar(Val.Substring(i, 1)) <= 90) // @=64,A=65,Z=90
                    { }
                    else if (Convert.ToChar(Val.Substring(i, 1)) == 95 || Convert.ToChar(Val.Substring(i, 1)) == 45) 	// _=95					
                    { }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Check Valid IP Address
        public static bool IsValidIP(string IP)
        {
            System.Net.IPAddress IPAddr;
            return System.Net.IPAddress.TryParse(IP, out IPAddr);
        }
        #endregion

        #region Check Valid US Phone
        public static bool IsValidUsPhone(string Phone)
        {
            try
            {
                // === Updated By Manish Buhecha On:16 April,2009 === //
                // === This code is commented, because Fuji must accept International phone number and format and not just USA format === //

                //int intPhoneCounter = 0, intHyphen = 0, intDot = 0, intSlash = 0, intOpenBrace = 0, intCloseBrace = 0, intSpace = 0;
                if (Phone != null || Phone == "")
                {
                    //    for (int i = 0; i <= Phone.Length - 1; i++)
                    //    {
                    //        if (Convert.ToChar(Phone.Substring(i, 1)) >= 48 && Convert.ToChar(Phone.Substring(i, 1)) <= 57) //0 to 9	,0=48 & 9=57					
                    //            intPhoneCounter = intPhoneCounter + 1;
                    //        else if (Convert.ToChar(Phone.Substring(i, 1)) == 45)
                    //            intHyphen = intHyphen + 1;
                    //        else if (Convert.ToChar(Phone.Substring(i, 1)) == 46)
                    //            intDot = intDot + 1;
                    //        else if (Convert.ToChar(Phone.Substring(i, 1)) == 47)
                    //            intSlash = intSlash + 1;
                    //        else if (Convert.ToChar(Phone.Substring(i, 1)) == 40)
                    //            intOpenBrace = intOpenBrace + 1;
                    //        else if (Convert.ToChar(Phone.Substring(i, 1)) == 41)
                    //            intCloseBrace = intCloseBrace + 1;
                    //        else if (Convert.ToChar(Phone.Substring(i, 1)) == 32)
                    //            intSpace = intSpace + 1;
                    //        else
                    //            return false;
                    //    }
                    //    if (intPhoneCounter > 10 || intPhoneCounter < 10)
                    //        return false;
                    //    if (intHyphen > 2)
                    //        return false;
                    //    if (intDot > 2)
                    //        return false;
                    //    if (intSlash > 2)
                    //        return false;
                    //    if (intOpenBrace > 1)
                    //        return false;
                    //    if (intCloseBrace > 1)
                    //        return false;
                    //    if (intSpace > 2)
                    //        return false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
