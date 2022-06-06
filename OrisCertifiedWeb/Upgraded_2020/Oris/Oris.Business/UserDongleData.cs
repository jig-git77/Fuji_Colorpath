#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Oris.DataAccess;
#endregion

namespace Oris.Business
{
    public class UserDongleData : UserDongle
    {
        #region uspGetUserDongleData
        /// <summary>
        /// Gets the UserDongle by using UserID
        /// </summary>
        /// <returns>returns the UserDongle DAta</returns>
        public DataTable uspGetUserDongleData()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetUserDongleData";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDongleByUserIDAndBranchID
        /// <summary>
        /// Gets the company by using UserID
        /// </summary>
        /// <returns>returns the Dongle data by userid and branch id</returns>
        public DataTable uspGetDongleByUserIDAndBranchID(int _RoleID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDongleByUserIDAndBranchID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoleID);
            Cmd.Parameters.AddWithValue("@BranchID", this.UserDongleID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region Retrieve Dongle Data by BranchID
        /// <summary>
        /// Retrieve a dongle using BranchID
        /// </summary>
        /// <returns>returns the dongle by branch </returns>
        public System.Collections.ArrayList uspGetDongleByBranchID()
        {
            //========== Execute Command with SqlDataReader ==========//
            SqlCommand Cmd = new SqlCommand();
            System.Collections.ArrayList arrResult = new System.Collections.ArrayList();

            Cmd.CommandText = "CPV.uspGetDongleByBranchID";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@BranchID", this.DongleDesc);
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@RoleID", this.CPDongleID);          

            IDataReader IDR = (new Database()).ExecuteDataReader(Cmd);// TPSDataAccess.TPSDADatabase.ExecuteDataReader(cmd, TPSDataAccess.TPSDBCSTRING.GetCSTRING(TPSDataAccess.TPSDBCSTRING.DBASE.TPSS))
            if (IDR == null)
            {
                if (Cmd.Connection.State == ConnectionState.Open)
                {
                    Cmd.Connection.Close();
                }
                return arrResult;
            }
            CPDongleData ObjDG;
            while (IDR.Read())
            {
                ObjDG = new CPDongleData();
                ObjDG.CPDongleID = Convert.ToInt32(IDR[0]);
                ObjDG.DongleDescription = IDR[1].ToString();
                ObjDG.CPSerialNUM = IDR[2].ToString();
                arrResult.Add(ObjDG);
            }
            if (Cmd.Connection.State == ConnectionState.Open)
            {
                Cmd.Connection.Close();
            }
            return arrResult;
        }
        #endregion

        #region uspGetAssignSupportUserOfDongleID
        /// <summary>
        /// Gets the Support User using by DongleID
        /// </summary>
        /// <returns>returns the Assigned support user data</returns>
        public DataTable uspGetAssignSupportUserOfDongleID(int _RoleID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAssignSupportUserOfDongleID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoleID);
            Cmd.Parameters.AddWithValue("@DongleID", this.CPDongleID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region Inserting the User Dongle
        /// <summary>
        /// Inserting the User Dongle.
        /// </summary>
        /// <param name="_SelectedIDs">selected id of the list</param>
        /// <param name="_NonSelectedIDs">non selected id of the list</param>
        /// <returns>returns the int value as a result</returns>
        public int uspInsertUserDongleData(string _SelectedIDs, string _NonSelectedIDs)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspInsertUserDongleData";
            Cmd.CommandType = CommandType.StoredProcedure;

            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@SelectedIDs", _SelectedIDs);
            Cmd.Parameters.AddWithValue("@NonSelectedIDs", _NonSelectedIDs);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion
    }
}
