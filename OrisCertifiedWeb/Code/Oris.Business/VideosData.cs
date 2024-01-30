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
    public class VideosData : Videos
    {
        #region uspInsertVideo
        /// <summary>
        /// Used to insert New Video
        /// </summary>
        public int uspInsertVideo()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            //Cmd.CommandText = "CPV.uspInsertVideo";
            Cmd.CommandText = "Video.uspInsertVideo";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@VideoID", this.VideoID);
            Cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);
            Cmd.Parameters.AddWithValue("@LinkText", this.LinkText);
            Cmd.Parameters.AddWithValue("@VideoFile", this.VideoFile);
            Cmd.Parameters.AddWithValue("@Description", this.Description);
            Cmd.Parameters.AddWithValue("@Keywords", this.Keywords);
            Cmd.Parameters.AddWithValue("@UploadTo", this.UploadTo);
            Cmd.Parameters.AddWithValue("@UploadedBy", this.UploadedBy);
            Cmd.Parameters.AddWithValue("@ModifiedBy", this.ModifiedBy);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #region uspUpdateVideo
        /// <summary>
        /// Used to Update Existing Video
        /// </summary>
        public int uspUpdateVideo()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.CommandText = "CPV.uspUpdateVideo";
            Cmd.CommandText = "Video.uspUpdateVideo";
            Cmd.Parameters.AddWithValue("@VideoID", this.VideoID);
            Cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);
            Cmd.Parameters.AddWithValue("@LinkText", this.LinkText);
            Cmd.Parameters.AddWithValue("@VideoFile", this.VideoFile);
            Cmd.Parameters.AddWithValue("@Description", this.Description);
            Cmd.Parameters.AddWithValue("@Keywords", this.Keywords);
            Cmd.Parameters.AddWithValue("@UploadTo", this.UploadTo);
            Cmd.Parameters.AddWithValue("@ModifiedBy", this.ModifiedBy);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #region uspDeleteVideo
        /// <summary>
        /// Used to Delete Particular Video
        /// </summary>
        public void uspDeleteVideo()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.CommandText = "CPV.uspDeleteVideo";
            Cmd.CommandText = "Video.uspDeleteVideo";
            Cmd.Parameters.AddWithValue("@VideoID", this.VideoID);
            (new Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspGetAllVideos
        /// <summary>
        /// Get All Videos
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetAllVideos()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.CommandText = "CPV.uspGetAllVideos";
            Cmd.CommandText = "Video.uspGetAllVideos";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetVideosByVideoID
        /// <summary>
        /// Get Videos By VideoID
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetVideosByVideoID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.CommandText = "CPV.uspGetVideosByVideoID";
            Cmd.CommandText = "Video.uspGetVideosByVideoID";
            Cmd.Parameters.AddWithValue("@VideoID", this.VideoID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetAllVideosByUserID
        /// <summary>
        /// Get All Videos By UserID
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetAllVideosByUserID(int TaskeroUserID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.CommandText = "CPV.uspGetAllVideosByUserID";
            Cmd.CommandText = "Video.uspGetAllVideosByUserID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", TaskeroUserID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
