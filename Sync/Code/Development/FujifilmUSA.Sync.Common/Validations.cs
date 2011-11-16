using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Common
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
    }
}
