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
    public class CompanyBrandingData : CompanyBranding
    {
        
        #region uspInsertCompanyBrandingData
        /// <summary>
        /// Inserts new Branding Data
        /// </summary>
        public int uspInsertCompanyBrandingData()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspInsertCompanyBrandingData";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@BrandName", this.BrandName);
            Cmd.Parameters.AddWithValue("@Css", this.Css);
            Cmd.Parameters.AddWithValue("@HeaderLogo", this.HeaderLogo);
            Cmd.Parameters.AddWithValue("@LargeImage", this.LargeImage);

            Cmd.Parameters.AddWithValue("@TopLine", this.TopLine);
            Cmd.Parameters.AddWithValue("@MainTabMenuText", this.MainTabMenuText);
            Cmd.Parameters.AddWithValue("@MainTabMenuTextOver", this.MainTabMenuTextOver);
            Cmd.Parameters.AddWithValue("@MainTabMenuBack", this.MainTabMenuBack);
            Cmd.Parameters.AddWithValue("@MainTabMenuBackOver", this.MainTabMenuBackOver);
            Cmd.Parameters.AddWithValue("@MainTabMenuBorder", this.MainTabMenuBorder);
            Cmd.Parameters.AddWithValue("@MainTabMenuBorderOver", this.MainTabMenuBorderOver);
            Cmd.Parameters.AddWithValue("@MenuBottomLine", this.MenuBottomLine);
            Cmd.Parameters.AddWithValue("@SubTabMenuText", this.SubTabMenuText);
            Cmd.Parameters.AddWithValue("@SubTabMenuTextOver", this.SubTabMenuTextOver);
            Cmd.Parameters.AddWithValue("@MainForm", this.MainForm);
            Cmd.Parameters.AddWithValue("@Footer", this.Footer);
            Cmd.Parameters.AddWithValue("@FooterText", this.FooterText);
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

            Cmd.Parameters.AddWithValue("@PopUpTabMainColor", this.PopUpTabMainColor);
            Cmd.Parameters.AddWithValue("@PopUpTabText", this.PopUpTabText);
            Cmd.Parameters.AddWithValue("@PopUpTabSelectedText", this.PopUpTabSelectedText);

            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #region uspGetCompanyBrandingData
        /// <summary>
        /// Gets the taskero users per role id.
        /// </summary>
        /// <returns>returns the information of the user</returns>
        public DataTable uspGetCompanyBrandingData(int _TaskeroUserID)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspGetCompanyBrandingData";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _TaskeroUserID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetCompanyBrandingByCompanyID
        /// <summary>
        /// Gets the taskero users per role id.
        /// </summary>
        /// <returns>returns the information of the user</returns>
        public DataTable uspGetCompanyBrandingByCompanyID()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspGetCompanyBrandingByCompanyID";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspUpdateCompanyBrandingData
        /// <summary>
        /// Inserts new Branding Data
        /// </summary>
        public int uspUpdateCompanyBrandingData()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspUpdateCompanyBrandingData";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@BrandID", this.BrandID);
            Cmd.Parameters.AddWithValue("@BrandName", this.BrandName);
            Cmd.Parameters.AddWithValue("@Css", this.Css);
            Cmd.Parameters.AddWithValue("@HeaderLogo", this.HeaderLogo);
            Cmd.Parameters.AddWithValue("@LargeImage", this.LargeImage);

            Cmd.Parameters.AddWithValue("@TopLine", this.TopLine);
            Cmd.Parameters.AddWithValue("@MainTabMenuText", this.MainTabMenuText);
            Cmd.Parameters.AddWithValue("@MainTabMenuTextOver", this.MainTabMenuTextOver);
            Cmd.Parameters.AddWithValue("@MainTabMenuBack", this.MainTabMenuBack);
            Cmd.Parameters.AddWithValue("@MainTabMenuBackOver", this.MainTabMenuBackOver);
            Cmd.Parameters.AddWithValue("@MainTabMenuBorder", this.MainTabMenuBorder);
            Cmd.Parameters.AddWithValue("@MainTabMenuBorderOver", this.MainTabMenuBorderOver);
            Cmd.Parameters.AddWithValue("@MenuBottomLine", this.MenuBottomLine);
            Cmd.Parameters.AddWithValue("@SubTabMenuText", this.SubTabMenuText);
            Cmd.Parameters.AddWithValue("@SubTabMenuTextOver", this.SubTabMenuTextOver);            
            Cmd.Parameters.AddWithValue("@MainForm", this.MainForm);
            Cmd.Parameters.AddWithValue("@Footer", this.Footer);
            Cmd.Parameters.AddWithValue("@FooterText", this.FooterText);
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

            Cmd.Parameters.AddWithValue("@PopUpTabMainColor", this.PopUpTabMainColor);
            Cmd.Parameters.AddWithValue("@PopUpTabText", this.PopUpTabText);
            Cmd.Parameters.AddWithValue("@PopUpTabSelectedText", this.PopUpTabSelectedText);

            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #region uspDeleteCompanyBrand
        /// <summary>
        /// Deletes the Company Brand Data
        /// </summary>
        /// <returns>returns the result of the last operation</returns>
        public int uspDeleteCompanyBrand()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspDeleteCompanyBrand";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@BrandID", this.BrandID);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

    }
}
