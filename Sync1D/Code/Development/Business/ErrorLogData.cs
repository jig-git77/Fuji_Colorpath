#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Fuji.DataAccess;
#endregion

namespace Fuji.Business
{
    public class ErrorLogData : ErrorLog
    {
        #region Insert
        /// <summary>
        /// Inserts and Error in Error Log
        /// </summary>
        public void Insert()
        {
            try
            {
                //========== Execute Command ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "CPV.uspInsertErrorLog";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@TimeStamp", this.TimeStamp);
                Cmd.Parameters.AddWithValue("@ExType", this.ExType);
                Cmd.Parameters.AddWithValue("@ExMessage", this.ExMessage);
                Cmd.Parameters.AddWithValue("@ExStackTracke", this.ExStackTracke);
                Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
                (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region HandleException
        /// <summary>
        /// Returns Stack Trace to get the error
        /// </summary>
        /// <param name="Exc"></param>
        /// <returns></returns>
        public string HandleException(Exception Exc)
        {
            if (Exc.Message.IndexOf("Thread") > -1)
            { return ""; }
            //========== For adding to database ==========//
            if (Fuji.Common.WebSettings.ErrorLogType == "D" || Fuji.Common.WebSettings.ErrorLogType == "B")
            {
                //========== Assign Variable ==========//
                this.ExType = "Exception";
                this.ExMessage = Exc.Message;
                this.ExStackTracke = Exc.StackTrace;
                this.TaskeroUserID = Fuji.Common.Sessions.UserID;
                this.Insert();
            }

            //========== Sending Error in Email ==========//
            if (Fuji.Common.WebSettings.ErrorLogType == "E" || Fuji.Common.WebSettings.ErrorLogType == "B")
            { Fuji.Common.WebMails.SendErrorEmail(Exc); }

            return "Message : " + Exc.Message + "<br>Trace : " + Exc.StackTrace;
        }
        #endregion
    }
}
