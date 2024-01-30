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
    public class TempRHDeviceTypeData : TempRHDeviceType
    {
        #region Retieving the TempRHDeviceType
        /// <summary>
        /// Retrieving the TempRHDeviceType
        /// </summary>
        /// <returns>returns the TempRHDevice Type Data</returns>
        public System.Collections.ArrayList uspGetTempRHDeviceType()
        {
            //========== Execute Command with SqlDataReader ==========//
            SqlCommand Cmd = new SqlCommand();
            System.Collections.ArrayList arrResult = new System.Collections.ArrayList();

            Cmd.CommandText = "CPV.uspGetTempRHDeviceType";
            Cmd.CommandType = CommandType.StoredProcedure;            
            IDataReader IDR = (new Database()).ExecuteDataReader(Cmd);// TPSDataAccess.TPSDADatabase.ExecuteDataReader(cmd, TPSDataAccess.TPSDBCSTRING.GetCSTRING(TPSDataAccess.TPSDBCSTRING.DBASE.TPSS))
            if (IDR == null)
            {
                if (Cmd.Connection.State == ConnectionState.Open)
                {
                    Cmd.Connection.Close();
                }
                return arrResult;
            }
            TempRHDeviceTypeData ObjRls;
            while (IDR.Read())
            {
                ObjRls = new TempRHDeviceTypeData();
                ObjRls.TRHDeviceTypeID = Convert.ToInt16(IDR[0]);
                ObjRls.TRHDeviceTypeDesc = IDR[1].ToString();
                arrResult.Add(ObjRls);
            }
            if (Cmd.Connection.State == ConnectionState.Open)
            {
                Cmd.Connection.Close();
            }
            return arrResult;

        }
        #endregion
    }
}
