using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace FujifilmUSA.Sync.Common
{
	/// <summary>
	/// Summary description for DSReader.
	/// </summary>
	public class DataReader
	{
	    /// <summary>
	    /// To read a string column value from datarow.
	    /// </summary>
	    /// <param name="objRow"></param>
	    /// <param name="columnName"></param>
	    /// <returns>string</returns>
	    public static string ReadString(DataRow objRow, string columnName)
		{
			string sRetVal = "";
			if (objRow.IsNull(columnName))
				sRetVal =  "";
			else
				sRetVal= objRow[columnName].ToString();
			return sRetVal;
		}

		/// <summary>
		/// To read a boolean column value from datarow.
		/// </summary>
		/// <param name="objRow"></param>
		/// <param name="columnName"></param>
		/// <returns>int</returns>
        public static Boolean ReadBoolean(DataRow objRow, string columnName)
		{
			Boolean bRetVal = false;
			try
			{
				
				if (objRow.IsNull(columnName))
					return bRetVal;
				else
					bRetVal = Convert.ToBoolean(objRow[columnName]);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return bRetVal;
		}


	    /// <summary>
	    /// To read a short column value from datarow.
	    /// </summary>
	    /// <param name="objRow"></param>
	    /// <param name="columnName"></param>
	    /// <param name="version"></param>
	    /// <returns>int</returns>
        public static Boolean ReadBoolean(DataRow objRow, string columnName, DataRowVersion version)
		{
            Boolean bRetVal = false;
			try
			{				
				if (objRow[columnName, version].Equals(DBNull.Value))
					return bRetVal;
				else
					bRetVal = Convert.ToBoolean(objRow[columnName, version]);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return bRetVal;

		}

		/// <summary>
		/// To read a short column value from datarow.
		/// </summary>
		/// <param name="objRow"></param>
		/// <param name="columnName"></param>
		/// <returns>int</returns>
		public static short ReadShort(DataRow objRow, string columnName)
		{
			short iRetVal = -1;
			try
			{
				
				if (objRow.IsNull(columnName))
					return iRetVal;
				else
					iRetVal = Convert.ToInt16(objRow[columnName]);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return iRetVal;
		}

	    /// <summary>
	    /// To read a short column value from datarow.
	    /// </summary>
	    /// <param name="objRow"></param>
	    /// <param name="columnName"></param>
	    /// <param name="version"></param>
	    /// <returns>int</returns>
	    public static short ReadShort(DataRow objRow, string columnName, DataRowVersion version)
		{
			short iRetVal = -1;
			try
			{
				
				if (objRow[columnName, version].Equals(DBNull.Value))
					return iRetVal;
				else
					iRetVal = Convert.ToInt16(objRow[columnName, version]);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return iRetVal;

		}

		/// <summary>
		/// To read a integer column value from datarow.
		/// </summary>
		/// <param name="objRow"></param>
		/// <param name="columnName"></param>
		/// <returns>int</returns>
		public static int ReadInt(DataRow objRow, string columnName)
		{
			int iRetVal = -1;
			try
			{
				
				if (objRow.IsNull(columnName))
					return iRetVal;
				else
					iRetVal = Convert.ToInt32(objRow[columnName]);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return iRetVal;

		}

        /// <summary>
        /// To read byte array column value from datarow.
        /// </summary>
        /// <param name="objRow"></param>
        /// <param name="columnName"></param>
        /// <returns>byte[]</returns>
        public static byte[] ReadByteArray(DataRow objRow, string columnName)
        {
            byte[] iRetVal;
            try
            {
                if (objRow.IsNull(columnName))
                    return default(byte[]);
                else
                    iRetVal = (byte[])(objRow[columnName]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRetVal;

        }

        /// <summary>
        /// To read byte column value from datarow.
        /// </summary>
        /// <param name="objRow"></param>
        /// <param name="columnName"></param>
        /// <returns>byte[]</returns>
        public static byte ReadByte(DataRow objRow, string columnName)
        {
            byte iRetVal;
            try
            {
                if (objRow.IsNull(columnName))
                    return default(byte);
                else
                    iRetVal = (byte)(objRow[columnName]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRetVal;

        }

		/// <summary>
		/// To read a Long column value from datarow.
		/// </summary>
		/// <param name="objRow"></param>
		/// <param name="columnName"></param>
		/// <returns>long</returns>
		public static long ReadLong(DataRow objRow, string columnName)
		{
			long iRetVal = -1;
			try
			{
				
				if (objRow.IsNull(columnName))
					return iRetVal;
				else
					iRetVal = Convert.ToInt64(objRow[columnName]);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return iRetVal;

		}

		/// <summary>
		/// To read a double column value from datarow.
		/// </summary>
		/// <param name="objRow"></param>
		/// <param name="columnName"></param>
		/// <returns>double</returns>
		public static double ReadDouble(DataRow objRow, string columnName)
		{
			double dRetVal = -1;
			try
			{
				
				if (objRow.IsNull(columnName))
					return dRetVal;
				else
					dRetVal = Convert.ToDouble(objRow[columnName]);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return dRetVal;
		}

        /// <summary>
        /// To read a decimal column value from datarow.
        /// </summary>
        /// <param name="objRow"></param>
        /// <param name="columnName"></param>
        /// <returns>decimal</returns>
        public static decimal ReadDecimal(DataRow objRow, string columnName)
        {
            decimal dRetVal = -1;
            try
            {

                if (objRow.IsNull(columnName))
                    return dRetVal;
                else
                    dRetVal = Convert.ToDecimal(objRow[columnName]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dRetVal;
        }

	    /// <summary>
	    /// To read a string column value from datarow.
	    /// </summary>
	    /// <param name="objRow"></param>
	    /// <param name="columnName"></param>
	    /// <param name="version"></param>
	    /// <returns>string</returns>
	    public static string ReadString(DataRow objRow, string columnName, DataRowVersion version)
		{
			string sRetVal = "";
			if (objRow[columnName,version] == DBNull.Value)
				sRetVal =  "";
			else
				sRetVal= objRow[columnName,version].ToString();
			return sRetVal;
		}


	    /// <summary>
	    /// To read a DateTime column value from datarow.
	    /// </summary>
	    /// <param name="objRow"></param>
	    /// <param name="columnName"></param>
	    /// <param name="version"></param>
	    /// <returns>DateTime</returns>
	    public static DateTime ReadDate(DataRow objRow, string columnName, DataRowVersion version)
		{
			DateTime dtTime;
			if (objRow[columnName,version] == DBNull.Value)
				dtTime =  DateTime.MinValue;
			else
				dtTime= Convert.ToDateTime(objRow[columnName,version]);
			return dtTime;
		}


		
		/// <summary>
		/// To read a DateTime column value from datarow.
		/// </summary>
		/// <param name="objRow"></param>
		/// <param name="columnName"></param>
		/// <returns>DateTime</returns>
		public static DateTime ReadDate(DataRow objRow, string columnName)
		{
			DateTime dtTime;
			if (objRow.IsNull(columnName))
				dtTime =  DateTime.MinValue;
			else
				dtTime= Convert.ToDateTime(objRow[columnName]);
			return dtTime;
		}

	    /// <summary>
	    /// To read a integer column value from datarow.
	    /// </summary>
	    /// <param name="objRow"></param>
	    /// <param name="columnName"></param>
	    /// <param name="version"></param>
	    /// <returns>int</returns>
	    public static int ReadInt(DataRow objRow, string columnName, DataRowVersion version)
		{
			int iRetVal = -1;
			try
			{
				
				if (objRow[columnName,version] == DBNull.Value)
					return iRetVal;
				else
					iRetVal = Convert.ToInt32(objRow[columnName,version]);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return iRetVal;

		}

		// To read a Long column value from datarow.
		/// <summary>
		/// To read a long column value from datarow based on version.
		/// </summary>
		/// <param name="objRow"></param>
		/// <param name="columnName"></param>
		/// <param name="version"></param>
		/// <returns>long </returns>
		public static long ReadLong(DataRow objRow, string columnName, DataRowVersion version)
		{
			long iRetVal = -1;
			try
			{
				
				if (objRow[columnName,version] == DBNull.Value)
					return iRetVal;
				else
					iRetVal = Convert.ToInt64(objRow[columnName,version]);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return iRetVal;

		}

	    /// <summary>
	    /// To read a double column value from datarow.
	    /// </summary>
	    /// <param name="objRow"></param>
	    /// <param name="columnName"></param>
	    /// <param name="version"></param>
	    /// <returns>double</returns>
	    public static double ReadSDouble(DataRow objRow, string columnName,DataRowVersion version)
		{
			double dRetVal = -1;
			try
			{
				
				if (objRow[columnName,version] == DBNull.Value)
					return dRetVal;
				else
					dRetVal = Convert.ToDouble(objRow[columnName,version]);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return dRetVal;
		}

        /// <summary>
        /// To read a guid column value from datarow.
        /// </summary>
        /// <param name="objRow"></param>
        /// <param name="columnName"></param>
        /// <returns>double</returns>
        public static Guid ReadGuid(DataRow objRow, string columnName)
        {
            var dRetVal = new Guid();
            try
            {

                if (objRow.IsNull(columnName))
                    return dRetVal;
                else
                    dRetVal = Guid.Parse(objRow[columnName].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dRetVal;
        }

		/// <summary>
		/// Verify for a DBNull in the parameter list  and change it to null.
		/// </summary>
		/// <param name="args"></param>
		/// <returns>void</returns>
		public static void RemoveDbNull(object[] args)
		{
			
			try
			{
				if(args != null)
				{
					int idx = System.Array.IndexOf(args,DBNull.Value);
					while (idx >= 0)
					{
						args.SetValue(null,idx);
						idx = System.Array.IndexOf(args,DBNull.Value);
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return ;
		}

		public static object ValidValue(object oValue)
		{
			object oRetValue = null;
			try
			{
				if (oValue != DBNull.Value)
					oRetValue = oValue;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return oRetValue;
					
		}


	}
}
