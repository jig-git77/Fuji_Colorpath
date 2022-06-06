using Oris.Common;
using System;
using System.Data;
using System.Data.SqlClient;


namespace Oris.DataAccess
{
    public class Database
    {

        public static readonly string strConStr = WebSettings.ConnectionString;

        #region Execute Non Query
        /// <summary>
        /// Insert/Update/Delete
        /// </summary>
        /// <param name="Cmd">This are the params and the query form the application or business</param>
        public void ExecuteNonQuery(SqlCommand Cmd)
        {
            //========== Connection ==========//
            SqlConnection Conn = new SqlConnection(strConStr);

            try
            {
                //========== Open Connection ==========//
                Conn.Open();

                //========== Execute Command ==========//
                Cmd.Connection = Conn;
                Cmd.CommandTimeout = 180;
                Cmd.ExecuteNonQuery();
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
            finally
            {
                //======== Closing Connection ========//
                if (Conn.State == ConnectionState.Open)
                { Conn.Close(); }

                //======== Disposing object ========//
                Conn = null;
            }
        }
        #endregion

        #region Get Data Set
        /// <summary>
        /// Get dataset from the funciton on passing the command.
        /// </summary>
        /// <param name="Cmd">This are the params and the query form the application or business</param>
        /// <returns>This will return the dataset.</returns>
        public DataSet GetDataSet(SqlCommand Cmd)
        {
            //========== Declaring DataSet ==========//
            DataSet DSet = new DataSet();

            //========== Connection ==========//
            SqlConnection Conn = new SqlConnection(strConStr);

            try
            {
                //========== Open Connection ==========//
                Conn.Open();

                //======== Getting Dataset ========//
                SqlDataAdapter DA = new SqlDataAdapter();
                Cmd.Connection = Conn;
                Cmd.CommandTimeout = 180;
                DA.SelectCommand = Cmd;
                DA.Fill(DSet);
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
            finally
            {
                //======== Closing Connection ========//
                if (Conn.State == ConnectionState.Open)
                { Conn.Close(); }

                //======== Disposing object ========//
                Conn = null;
            }

            //======== returning Dataset ========//
            return DSet;
        }
        public DataSet GetDataSetExtendedTimeout(SqlCommand Cmd)
        {
            //========== Declaring DataSet ==========//
            DataSet DSet = new DataSet();

            //========== Connection ==========//
            SqlConnection Conn = new SqlConnection(strConStr);

            try
            {
                //========== Open Connection ==========//
                Conn.Open();

                //======== Getting Dataset ========//
                SqlDataAdapter DA = new SqlDataAdapter();
                Cmd.CommandTimeout = 300;
                Cmd.Connection = Conn;
                DA.SelectCommand = Cmd;
                DA.Fill(DSet);
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
            finally
            {
                //======== Closing Connection ========//
                if (Conn.State == ConnectionState.Open)
                { Conn.Close(); }

                //======== Disposing object ========//
                Conn = null;
            }

            //======== returning Dataset ========//
            return DSet;
        }
        #endregion

        #region Get Data Table
        /// <summary>
        /// Get a single datatable from the funciton on passing the command.
        /// </summary>
        /// <param name="Cmd">This are the params and the query form the application or business</param>
        /// <returns>This will return the datatable.</returns>
        public DataTable GetDataTable(SqlCommand Cmd)
        {
            //========== Declaring DataTable ==========//
            DataTable DTbl = new DataTable();

            //========== Connection ==========//
            SqlConnection Conn = new SqlConnection(strConStr);

            try
            {
                //========== Open Connection ==========//
                Conn.Open();

                //======== Getting Dataset ========//
                SqlDataAdapter DA = new SqlDataAdapter();
                Cmd.Connection = Conn;
                Cmd.CommandTimeout = 180;
                DA.SelectCommand = Cmd;
                DA.Fill(DTbl);
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
            finally
            {
                //======== Closing Connection ========//
                if (Conn.State == ConnectionState.Open)
                { Conn.Close(); }

                //======== Disposing object ========//
                Conn = null;
            }

            //======== returning Dataset ========//
            return DTbl;
        }
        #endregion

        #region Get Data Reader
        /// <summary>
        /// Get a single SqlDataReader from the funciton on passing the command.
        /// </summary>
        /// <param name="Cmd">This are the params and the query form the application or business</param>
        /// <returns>This will return the SqlDataReader.</returns>
        public SqlDataReader GetDataReader(SqlCommand Cmd)
        {
            //========== Declaring DataTable ==========//
            SqlDataReader DReader;

            //========== Connection ==========//
            SqlConnection Conn = new SqlConnection(strConStr);

            try
            {
                //========== Open Connection ==========//
                Conn.Open();

                //======== Getting Dataset ========//
                Cmd.Connection = Conn;
                Cmd.CommandTimeout = 180;
                DReader = Cmd.ExecuteReader();
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
            finally
            {
                //======== Closing Connection ========//
                if (Conn.State == ConnectionState.Open)
                { Conn.Close(); }

                //======== Disposing object ========//
                Conn = null;
            }

            //======== returning Dataset ========//
            return DReader;
        }
        public IDataReader ExecuteDataReader(SqlCommand Cmd)
        {
            //======== Declaring Connection Variable ========//
            SqlConnection Conn = new SqlConnection(strConStr);
            try
            {
                //if (Conn.State == ConnectionState.Closed)
                Conn.Open();
                Cmd.Connection = Conn;
                Cmd.CommandTimeout = 180;
                return Cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {

                //======== Closing Connection ========//
                if (Conn.State == ConnectionState.Open)
                { Conn.Close(); }

                //======== Disposing object ========//
                Conn = null;
                return null;
            }
            //finally
            //{
            //    //======== Closing Connection ========//
            //    if (Conn.State == ConnectionState.Open)
            //    { Conn.Close(); }

            //    //======== Disposing object ========//
            //    Conn = null;                
            //}
        }
        #endregion
    }
}