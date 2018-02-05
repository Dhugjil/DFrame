
namespace DFrame.BLL.MySQL
{
    /// <summary>
    /// MS SQLServer SQLHelper
    /// </summary>
    public class SqlHelper : DFrame.BLL.SQLServer.ISqlHelper
    {
        private DFrame.DAL.SQLServer.Interface.ISQLServerHelper _BaseDal = new DFrame.DAL.SQLServer.SQLServerHelper();
        private DFrame.DAL.SQLServer.Interface.ISQLServerHelper BaseDal
        {
            get
            {
                return _BaseDal;
            }
            set
            {
                _BaseDal = value;
            }
        }

        public void CacheParameters(string cacheKey, params System.Data.SqlClient.SqlParameter[] commandParameters)
        {
            BaseDal.CacheParameters(cacheKey, commandParameters);
        }

        public System.Data.SqlClient.SqlParameter[] GetCachedParameters(string cacheKey)
        {
            return BaseDal.GetCachedParameters(cacheKey);
        }

        public int ExecuteNonQuery(string connectionstring, bool transaction, string commandtext, System.Data.CommandType comtype, params System.Data.SqlClient.SqlParameter[] commandparameters)
        {
            return ExecuteNonQuery(connectionstring, transaction, commandtext, comtype, commandparameters);
        }

        public int ExecuteNonQueryText(bool transaction, string commandtext, params System.Data.SqlClient.SqlParameter[] commandparameters)
        {
            return BaseDal.ExecuteNonQueryText(transaction, commandtext, commandparameters);
        }

        public int ExecuteNonQueryText(string commandtext, params System.Data.SqlClient.SqlParameter[] commandparameters)
        {
            return BaseDal.ExecuteNonQueryText(commandtext, commandparameters);
        }

        public int ExecuteNonQueryStoredProcedure(bool transaction, string commandtext, params System.Data.SqlClient.SqlParameter[] commandparameters)
        {
            return BaseDal.ExecuteNonQueryStoredProcedure(transaction, commandtext, commandparameters);
        }

        public int ExecuteNonQueryStoredProcedure(string commandtext, params System.Data.SqlClient.SqlParameter[] commandparameters)
        {
            return BaseDal.ExecuteNonQueryStoredProcedure(commandtext, commandparameters);
        }

        public int ExecuteNonQuery(string commandtext)
        {
            return BaseDal.ExecuteNonQuery(commandtext);
        }

        public System.Data.SqlClient.SqlDataReader ExecuteReader(string connectionstring, string commandtext, System.Data.CommandType comtype, params System.Data.SqlClient.SqlParameter[] commandparameters)
        {
            return BaseDal.ExecuteReader(connectionstring, commandtext, comtype, commandparameters);
        }

        public System.Data.SqlClient.SqlDataReader ExecuteReaderText(string commandtext, params System.Data.SqlClient.SqlParameter[] commandparameters)
        {
            return BaseDal.ExecuteReaderText(commandtext, commandparameters);
        }

        public System.Data.SqlClient.SqlDataReader ExecuteReaderStoredProcedure(string commandtext, params System.Data.SqlClient.SqlParameter[] commandparameters)
        {
            return BaseDal.ExecuteReaderStoredProcedure(commandtext, commandparameters);
        }

        public System.Data.SqlClient.SqlDataReader ExecuteReader(string commandtext)
        {
            return BaseDal.ExecuteReader(commandtext);
        }

        public object ExecuteScalar(string connectionstring, string commandtext, System.Data.CommandType comtype, params System.Data.SqlClient.SqlParameter[] commandparameters)
        {
            return BaseDal.ExecuteScalar(connectionstring, commandtext, comtype, commandparameters);
        }

        public object ExecuteScalarText(string commandtext, params System.Data.SqlClient.SqlParameter[] commandparameters)
        {
            return BaseDal.ExecuteScalarText(commandtext, commandparameters);
        }

        public object ExecuteScalarStoredProcedure(string commandtext, params System.Data.SqlClient.SqlParameter[] commandparameters)
        {
            return BaseDal.ExecuteScalarStoredProcedure(commandtext, commandparameters);
        }

        public object ExecuteScalar(string commandtext)
        {
            return BaseDal.ExecuteScalar(commandtext);
        }

        public System.Data.DataTable DataAdapterRead(string connectionstring, string tablename, string commandtext, System.Data.CommandType comtype, params System.Data.SqlClient.SqlParameter[] commandparameters)
        {
            return BaseDal.DataAdapterRead(connectionstring, tablename, commandtext, comtype, commandparameters);
        }

        public System.Data.DataTable DataAdapterReadText(string tablename, string commandtext, params System.Data.SqlClient.SqlParameter[] commandparameters)
        {
            return BaseDal.DataAdapterReadText(tablename, commandtext, commandparameters);
        }

        public System.Data.DataTable DataAdapterReadStoredProcedure(string tablename, string commandtext, params System.Data.SqlClient.SqlParameter[] commandparameters)
        {
            return BaseDal.DataAdapterReadStoredProcedure(tablename, commandtext, commandparameters);
        }

        public System.Data.DataTable DataAdapterRead(string commandtext)
        {
            return BaseDal.DataAdapterRead(commandtext);
        }

        public System.Data.SqlClient.SqlParameter[] ProcessNull(System.Data.SqlClient.SqlParameter[] dataParameter)
        {
            return BaseDal.ProcessNull(dataParameter);
        }
    }
}
