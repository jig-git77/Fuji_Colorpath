#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Oris.Common;
using Oris.DataAccess;
#endregion

namespace Oris.Business
{
    public class UserCompanyData : UserCompany
    {
        #region uspGetCompanyByUserID
        /// <summary>
        /// Gets the company by using UserID
        /// </summary>
        /// <returns>returns the Company Data</returns>
        public DataTable uspGetCompanyByUserID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetCompanyByUserID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@RoleID", Convert.ToInt16(Sessions.RoleID));
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region Inserting the User Company
        /// <summary>
        /// Inserting the User Company.
        /// </summary>
        /// <param name="_SelectedIDs">selected id of the list</param>
        /// <param name="_NonSelectedIDs">non selected id of the list</param>
        /// <returns>returns the int value as a result</returns>
        public int uspInsertUserCompanyData(string _SelectedIDs, string _NonSelectedIDs)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspInsertUserCompanyData";
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
