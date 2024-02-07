#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Taskero.DataAccess;
#endregion

namespace Taskero.Business
{
    public class RecentlyVideosData : RecentlyVideos
    {
        #region uspInsertRecentlyVideos
        /// <summary>
        /// Used to insert recently video
        /// </summary>
        public int uspInsertRecentlyVideos()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            //Cmd.CommandText = "CPV.uspInsertRecentlyVideos";
            Cmd.CommandText = "Video.uspInsertRecentlyVideos";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@VideoID", this.VideoID);
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #region uspGetAllRecentlyVideoByUserID
        /// <summary>
        /// Get RecentlyVideos By UserID
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetAllRecentlyVideoByUserID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.CommandText = "CPV.uspGetAllRecentlyVideoByUserID";
            Cmd.CommandText = "Video.uspGetAllRecentlyVideoByUserID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
