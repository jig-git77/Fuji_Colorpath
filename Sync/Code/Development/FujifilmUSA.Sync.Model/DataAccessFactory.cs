using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using System.Data;

namespace FujifilmUSA.Sync.Model
{
    internal class DataAccessFactory
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
            get { return ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString; }
        }
        public SqlDatabase Database
        {
            get { return _database ?? (_database = new SqlDatabase(ConnectionString)); }
        }

        #endregion

        #region Data Access Methods

        public DataSet GetResult(string procName, object[] parameters)
        {
            var command = Database.GetStoredProcCommand(procName, parameters);
            return Database.ExecuteDataSet(command);
        }

        public DataSet GetResult(string procName)
        {
            var command = Database.GetStoredProcCommand(procName);
            return Database.ExecuteDataSet(command);
        }

        #endregion
    }
}
