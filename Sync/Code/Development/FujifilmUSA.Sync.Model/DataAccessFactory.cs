using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System;
using System.Transactions;
using System.Data.SqlClient;
using System.Xml;

namespace FujifilmUSA.Sync.DataAccess
{
    public class DataAccessFactory
    {
        #region Private Variables
        private static DataAccessFactory _instance;
        private SqlDatabase _database;
        #endregion

        #region Singleton
        private DataAccessFactory()
        {

        }

        public static DataAccessFactory Instance
        {
            get { return _instance ?? (_instance = new DataAccessFactory()); }
        }

        #endregion

        #region Database Instance

        private static string ConnectionString
        {
            get
            {
#if Local
                return ConfigurationManager.ConnectionStrings["LocalConnection"].ConnectionString;
#elif Jay_Local
                return ConfigurationManager.ConnectionStrings["Jay_LocalConnection"].ConnectionString;
#elif Dev
                return ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
#elif Production
                return ConfigurationManager.ConnectionStrings["ProductionConnection"].ConnectionString;
#endif
            }
        }
        public SqlDatabase Database
        {
            get { return _database ?? (_database = new SqlDatabase(ConnectionString)); }
        }

        #endregion

        #region Data Access Methods

        public DataSet GetDsResult(string procName)
        {
            var command = Database.GetStoredProcCommand(procName);
            return Database.ExecuteDataSet(command);
        }

        public DataSet GetDsResult(string procName, object[] parameters)
        {
            var command = Database.GetStoredProcCommand(procName, parameters);
            return Database.ExecuteDataSet(command);
        }

        public SqlDataReader GetReader(string procName)
        {
            var command = Database.GetStoredProcCommand(procName);
            return (SqlDataReader)Database.ExecuteReader(command);
        }

        public SqlDataReader GetReader(string procName, object[] parameters)
        {
            var command = Database.GetStoredProcCommand(procName, parameters);
            return (SqlDataReader)Database.ExecuteReader(command);
        }

        public object GetScalar(string procName)
        {
            var command = Database.GetStoredProcCommand(procName);
            return Database.ExecuteScalar(command);
        }

        public object GetScalar(string procName, object[] parameters)
        {
            var command = Database.GetStoredProcCommand(procName, parameters);
            return Database.ExecuteScalar(command);
        }

        public XmlReader GetXmlResult(string procName, object[] parameters)
        {
            var command = Database.GetStoredProcCommand(procName, parameters);
            return Database.ExecuteXmlReader(command);
        }

        

        public void ExecuteProcedureWithTransaction(List<Action> actions)
        {

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                actions.ForEach(x => x());
                scope.Complete();
            }
        }

        public int ExecuteProcedure(string procName, object[] parameters)
        {

            var command = Database.GetStoredProcCommand(procName, parameters);
            return Database.ExecuteNonQuery(command);
        }

        public List<Parameters> ExecuteProcedure(string procName, List<Parameters> inParameters, List<Parameters> outParameters)
        {
            var command = Database.GetStoredProcCommand(procName);
            inParameters.ForEach(x => Database.AddInParameter(command, x.ParameterName, x.Type, x.Value));
            outParameters.ForEach(x => Database.AddOutParameter(command, x.ParameterName, x.Type, x.Size));
            Database.ExecuteNonQuery(command);
            outParameters.ForEach(x => x.Value = Database.GetParameterValue(command, x.ParameterName));
            return outParameters;
        }

        #endregion
    }
}
