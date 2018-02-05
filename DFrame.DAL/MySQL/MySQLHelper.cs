using System;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;
using DFrame.DAL.MySQL.Interface;

namespace DFrame.DAL.MySQL
{
    /// <summary>
    /// MySQL帮助类
    /// </summary>
    public class MySQLHelper :Base, IMySQLHelper
    {
        /// <summary>
        /// 初始化DapperExecute,连接字符串访问webconfig的MySQLConnString
        /// </summary>
        public MySQLHelper() : base()
        { }
        /// <summary>
        /// 初始化DapperExecute数据库链接字符串
        /// </summary>
        /// <param name="conn">MySQL连接字符串</param>
        public MySQLHelper(string conn) : base(conn)
        { }

        #region 提高并发
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());//提高并发访问
        /// <summary>
        /// 隐藏SqlParameter,键为key
        /// </summary>
        /// <param name="cacheKey">取键key</param>
        /// <param name="commandParameters">将要隐藏SqlParameter[]</param>
        public void CacheParameters(string cacheKey, params MySqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }
        /// <summary>
        /// 输入key提取隐藏SqlParameter[]
        /// </summary>
        /// <param name = "cacheKey" > SqlParameter[]的key</param>
        /// <returns>返回key的SQLParameter</returns>
        public MySqlParameter[] GetCachedParameters(string cacheKey)
        {
            MySqlParameter[] cachedParms = (MySqlParameter[])parmCache[cacheKey];
            if (cachedParms == null)
                return null;
            MySqlParameter[] clonedParms = new MySqlParameter[cachedParms.Length];
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (MySqlParameter)((ICloneable)cachedParms[i]).Clone();
            parmCache.Remove(cacheKey);//释放，删除键cacheKey的值
            return clonedParms;
        }
        #endregion

        #region ExecuteNonQuery方法
        /// <summary>
        /// 执行自定义连接的ExecuteNonQuery方法
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="transaction">判断是否需要事物处理</param>
        /// <param name="commandText">Sql语句或存储过程名称</param>
        /// <param name="comType">Sql语句类型</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteNonQuery(string connectionString, bool transaction, string commandText, CommandType comType, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                lock (LockUp)
                {
                    SetVariable(cmd, conn, commandText, comType, commandParameters);
                }
                if (transaction)
                {
                    MySqlTransaction trans = conn.BeginTransaction();
                    cmd.Transaction = trans;
                    try
                    {
                        int val = cmd.ExecuteNonQuery();
                        trans.Commit();
                        cmd.Parameters.Clear();
                        conn.Close();
                        return val;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
                else
                    cmd.Transaction = null;
                int eval = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
                return eval;
            }
        }
        /// <summary>
        /// 执行指定连接的带参数ExecuteNonQueryText方法，判断是否需要事物处理
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="transaction">判断是否需要事物处理</param>
        /// <param name="commandText">Sql语句</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteNonQueryText(bool transaction, string commandText, params MySqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(connstr, transaction, commandText, CommandType.Text, commandParameters);
        }
        /// <summary>
        /// 执行指定连接的带参数ExecuteNonQueryText方法，不需要事物处理
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteNonQueryText(string commandText, params MySqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(connstr, false, commandText, CommandType.Text, commandParameters);
        }
        /// <summary>
        /// 执行指定连接的带参数ExecuteNonQueryStoredProcedure方法，判断是否需要事物处理
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="transaction">判断是否需要事物处理</param>
        /// <param name="commandText">Sql存储过程名称</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteNonQueryStoredProcedure(bool transaction, string commandText, params MySqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(connstr, transaction, commandText, CommandType.StoredProcedure, commandParameters);
        }
        /// <summary>
        /// 执行指定连接的带参数ExecuteNonQueryStoredProcedure方法，不需要事物处理
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="commandText">Sql存储过程名称</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteNonQueryStoredProcedure(string commandText, params MySqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(connstr, false, commandText, CommandType.StoredProcedure, commandParameters);
        }
        /// <summary>
        /// 执行指定连接ExecuteNonQuery方法
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(connstr, false, commandText, CommandType.Text, null);
        }
        #endregion

        #region ExecuteReader方法
        /// <summary>
        /// 查询自定义连接的ExecuteReader方法，返回查询结果集或表格
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">Sql语句或者存储过程名</param>
        /// <param name="comType">类型</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回结果集</returns>
        public MySqlDataReader ExecuteReader(string connectionString, string commandText, CommandType comType, params MySqlParameter[] commandParameters)
        {
            MySqlCommand com = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connectionString;
            try
            {
                lock (LockUp)//线程安全，加锁
                {
                    SetVariable(com, conn, commandText, comType, commandParameters);
                }
                MySqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);//在执行该命令时，如果关闭关联的 DataReader 对象，则关联的 Connection 对象也将关闭。
                com.Parameters.Clear();//清空parameters数据
                return dr;
            }
            catch
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
                throw;
            }
        }
        /// <summary>
        /// 查询指定连接ExecuteReaderText方法，返回查询结果集或表格
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="commandText">Sql语句或者存储过程名</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回结果集</returns>
        public MySqlDataReader ExecuteReaderText(string commandText, params MySqlParameter[] commandParameters)
        {
            return ExecuteReader(connstr, commandText, CommandType.Text, commandParameters);
        }
        /// <summary>
        /// 查询指定连接ExecuteReaderStoredProcedure方法，返回查询结果集或表格
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="commandText">Sql语句或者存储过程名</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回结果集</returns>
        public MySqlDataReader ExecuteReaderStoredProcedure(string commandText, params MySqlParameter[] commandParameters)
        {
            return ExecuteReader(connstr, commandText, CommandType.StoredProcedure, commandParameters);
        }
        /// <summary>
        /// 执行指定连接ExecuteReader方法
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>返回结果集</returns>
        public MySqlDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(connstr, commandText, CommandType.Text, null);
        }
        #endregion

        #region ExecuteScalar方法
        /// <summary>
        /// 查询自定义连接的ExecuteScalar方法，返回查询集结果中的第一行第一列的值
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">Sql语句或者存储过程名</param>
        /// <param name="comType">类型</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回结果值</returns>
        public object ExecuteScalar(string connectionString, string commandText, CommandType comType, params MySqlParameter[] commandParameters)
        {
            MySqlCommand com = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection())
            {
                conn.ConnectionString = connectionString;
                lock (LockUp)//线程安全，加锁
                {
                    SetVariable(com, conn, commandText, comType, commandParameters);
                }
                object val = com.ExecuteScalar();
                com.Parameters.Clear();
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
                return val;
            }
        }
        /// <summary>
        /// 查询指定连接ExecuteScalarText方法，返回查询集结果中的第一行第一列的值
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="commandText">Sql语句或者存储过程名</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回结果值</returns>
        public object ExecuteScalarText(string commandText, params MySqlParameter[] commandParameters)
        {
            return ExecuteScalar(connstr, commandText, CommandType.Text, commandParameters);
        }
        /// <summary>
        /// 查询指定连接ExecuteScalarStoredProcedure方法，返回查询集结果中的第一行第一列的值
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="commandText">Sql语句或者存储过程名</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回结果值</returns>
        public object ExecuteScalarStoredProcedure(string commandText, params MySqlParameter[] commandParameters)
        {
            return ExecuteScalar(connstr, commandText, CommandType.StoredProcedure, commandParameters);
        }
        /// <summary>
        /// 执行指定连接ExecuteScalar方法
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>返回结果值</returns>
        public object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(connstr, commandText, CommandType.Text, null);
        }
        #endregion

        #region DataAdapterRead方法
        /// <summary>
        ///查询自定义连接的ExecuteDataTable方法，返回查询数据表
        ///参数数组形式赋值
        /// </summary>
        /// <param name="connectionString">自定义连接字符串</param>
        /// <param name="tableName">自定义表格名称</param>
        /// <param name="commandText">Sql语句或存储过程名称</param>
        /// <param name="comType">类型</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回数据表</returns>
        public DataTable DataAdapterRead(string connectionString, string tableName, string commandText, CommandType comType, params MySqlParameter[] commandParameters)
        {
            MySqlCommand com = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection())
            {
                conn.ConnectionString = connectionString;
                lock (LockUp)//线程安全，加锁
                {
                    SetVariable(com, conn, commandText, comType, commandParameters);
                }
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = com;
                DataTable  dt = new DataTable();
                //dt.TableName = tableName;
                adapter.Fill(dt);
                //adapter.FillSchema(dt, SchemaType.Mapped);
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
                return dt;
            }
        }
        /// <summary>
        /// 查询指定连接ExecuteDataTable方法
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="tableName">自定义表格名称</param>
        /// <param name="commandText">Sql语句</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回数据表</returns>
        public DataTable DataAdapterReadText(string tableName, string commandText, params MySqlParameter[] commandParameters)
        {
            return DataAdapterRead(connstr, tableName, commandText, CommandType.Text, commandParameters);
        }
        /// <summary>
        /// 查询指定连接ExecuteDataTableStoredProcedure方法
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="commandText">存储过程名称</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回表集合</returns>
        public DataTable DataAdapterReadStoredProcedure(string tableName, string commandText, params MySqlParameter[] commandParameters)
        {
            return DataAdapterRead(connstr, tableName, commandText, CommandType.StoredProcedure, commandParameters);
        }
        /// <summary>
        /// 执行指定连接ExecuteDataTable方法
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>返回数据表</returns>
        public DataTable DataAdapterRead(string commandText)
        {
            return DataAdapterRead(connstr, null, commandText, CommandType.Text, null);
        }
        #endregion

        #region 参数控制
        /// <summary>
        /// 参数控制，判断parameter是否存在null，如果有改为DBNull
        /// </summary>
        /// <param name="dataParameter">参数</param>
        /// <returns></returns>
        public MySqlParameter[] ProcessNull(MySqlParameter[] dataParameter)
        {
            foreach (MySqlParameter parameter in dataParameter)
                if (parameter == null)
                    parameter.Value = DBNull.Value;
            return dataParameter;
        }
        #endregion

        #region 赋值参数
        /// <summary>
        /// 赋值参数
        /// </summary>
        /// <param name="com">command对象</param>
        /// <param name="conn">connection对象</param>
        /// <param name="commandText">Sql语句或者存储过程</param>
        /// <param name="comType">类型</param>
        /// <param name="commandParameters">参数数组</param>
        private static void SetVariable(MySqlCommand com, MySqlConnection conn, string commandText, CommandType comType, params MySqlParameter[] commandParameters)
        {
            if (conn.State != ConnectionState.Open) //打开数据库连接
                conn.Open();
            com.Connection = conn;//command的连接对象
            com.CommandText = commandText;//Sql语句或存储过程名称
            com.CommandType = comType;//类型，语句或存储过程
            if (commandParameters != null)//遍历添加数组值
                foreach (MySqlParameter parameter in commandParameters)
                    com.Parameters.Add(parameter);
        }
        #endregion
    }
}