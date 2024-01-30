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
    public class SiteLicenseData : SiteLicense
    {
        #region GetSiteLicense Text
        /// <summary>
        /// Update site license agreement
        /// </summary>
        public void uspUpdateSiteLicenseAgreement()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "dbo.uspUpdateSiteLicenseAgreement";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@IPAddress", this.RemoteHost);
            Cmd.Parameters.AddWithValue("@AcceptedDT", this.AcceptedDate);
            Cmd.Parameters.AddWithValue("@SiteLicenseTypeID", this.SiteLicenseTypeID);
            Cmd.Parameters.AddWithValue("@SiteLicenseAgreed", this.SiteLicenseAgreed);
           
         
            (new Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

       
    }
}
