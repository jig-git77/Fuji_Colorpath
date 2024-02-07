#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Oris.DataAccess;
#endregion

namespace Oris.Business
{
    public class UserLogData : UserLog
    {
        #region uspInsertUserLog
        /// <summary>
        /// Used to insert UserLog Data 
        /// </summary>
        public void uspInsertUserLog()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspInsertUserLog";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@IPAddress", this.IPAddress);
            Cmd.Parameters.AddWithValue("@LoginTime", this.LoginTime);
            (new Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspUpdateUserLogOut
        /// <summary>
        /// Used to update user logout time on logout of user
        /// </summary>
        public void uspUpdateUserLogOut()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspUpdateUserLogOut";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@IPAddress", this.IPAddress);
            (new Database()).ExecuteNonQuery(Cmd);
        }
        #endregion
    }
}
