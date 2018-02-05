using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using DFrame.DAL.SQLServer.Interface;

namespace DFrame.DAL.SQLServer
{
    /// <summary>
    /// SQLServer帮助类
    /// </summary>
    public class SQLServerHelper : Base, ISQLServerHelper
    {
        /// <summary>
        /// 初始化DapperExecute,连接字符串访问webconfig的SQLServerConnString
        /// </summary>
        public SQLServerHelper() : base()
        { }
        /// <summary>
        /// 初始化DapperExecute数据库链接字符串
        /// </summary>
        /// <param name="conn">SQLServer连接字符串</param>
        public SQLServerHelper(string conn):base(conn)
        { }

        #region 提高并发访问
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());//提高并发访问
        /// <summary>
        /// 隐藏SqlParameter,键为key
        /// </summary>
        /// <param name="cacheKey">取键key</param>
        /// <param name="commandParameters">将要隐藏SqlParameter[]</param>
        public void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }
        /// <summary>
        /// 输入key提取隐藏SqlParameter[]
        /// </summary>
        /// <param name = "cacheKey" > SqlParameter[]的key</param>
        /// <returns>返回key的SQLParameter</returns>
        public SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];//键cacheKey对象从hashtable拆箱赋值给cachedParms
            if (cachedParms == null)//判断是否空
                return null;//如果空直接返回
            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];//建立给定长度的clonedParms
            for (int i = 0, j = cachedParms.Length; i < j; i++)//循环赋值
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();//拆箱赋值
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
        public int ExecuteNonQuery(string connectionString, bool transaction, string commandText, CommandType comType, params SqlParameter[] commandParameters)
        {

            SqlCommand com = new SqlCommand();//实例化command对象
            using (SqlConnection conn = new SqlConnection())//使用空间，完成语句时释放空间
            {
                conn.ConnectionString = connectionString;//添加连接字符串
                lock (LockUp)//线程安全，加锁
                {
                    SetVariable(com, conn, commandText, comType, commandParameters);
                }
                if (transaction == true)//开始数据库事务，多条Sql语句时，如果一条语句错误则全部滚回。
                {
                    SqlTransaction trans = conn.BeginTransaction();
                    com.Transaction = trans;
                    try
                    {
                        int val = com.ExecuteNonQuery();
                        trans.Commit();
                        com.Parameters.Clear();
                        conn.Close();
                        return val;//返回受影响行数
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        trans.Rollback();
                        //string massage = throw new Exception(e.Message);
                        //string massage = new Exception(e.Message).ToString();
                        string massage = e.Message;
                        conn.Close();
                        throw;//返回错误信息
                    }
                    //finally
                    //{
                    //    if(conn.State!=ConnectionState.Closed)
                    //        conn.Close();
                    //}
                }
                else
                    com.Transaction = null;
                int eval = com.ExecuteNonQuery();
                com.Parameters.Clear();
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
        public int ExecuteNonQueryText(bool transaction, string commandText, params SqlParameter[] commandParameters)
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
        public int ExecuteNonQueryText(string commandText, params SqlParameter[] commandParameters)
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
        public int ExecuteNonQueryStoredProcedure(bool transaction, string commandText, params SqlParameter[] commandParameters)
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
        public int ExecuteNonQueryStoredProcedure(string commandText, params SqlParameter[] commandParameters)
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
        public SqlDataReader ExecuteReader(string connectionString, string commandText, CommandType comType, params SqlParameter[] commandParameters)
        {
            SqlCommand com = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            try
            {
                lock (LockUp)//线程安全，加锁
                {
                    SetVariable(com, conn, commandText, comType, commandParameters);
                }
                SqlDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection);//在执行该命令时，如果关闭关联的 DataReader 对象，则关联的 Connection 对象也将关闭。
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
        public SqlDataReader ExecuteReaderText(string commandText, params SqlParameter[] commandParameters)
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
        public SqlDataReader ExecuteReaderStoredProcedure(string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(connstr, commandText, CommandType.StoredProcedure, commandParameters);
        }
        /// <summary>
        /// 执行指定连接ExecuteReader方法
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>返回结果集</returns>
        public SqlDataReader ExecuteReader(string commandText)
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
        public object ExecuteScalar(string connectionString, string commandText, CommandType comType, params SqlParameter[] commandParameters)
        {
            SqlCommand com = new SqlCommand();
            using (SqlConnection conn = new SqlConnection())
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
        public object ExecuteScalarText(string commandText, params SqlParameter[] commandParameters)
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
        public object ExecuteScalarStoredProcedure(string commandText, params SqlParameter[] commandParameters)
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
        public DataTable DataAdapterRead(string connectionString, string tableName, string commandText, CommandType comType, params SqlParameter[] commandParameters)
        {
            SqlCommand com = new SqlCommand();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionString;
                lock (LockUp)//线程安全，加锁
                {
                    SetVariable(com, conn, commandText, comType, commandParameters);
                }
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = com;
                DataTable dt = new DataTable();
                dt.TableName = tableName;
                adapter.Fill(dt);
                adapter.FillSchema(dt, SchemaType.Mapped);
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
        public DataTable DataAdapterReadText(string tableName, string commandText, params SqlParameter[] commandParameters)
        {
            return DataAdapterRead(connstr, tableName, commandText, CommandType.Text, commandParameters);
        }
        /// <summary>
        /// 查询指定连接ExecuteDataTableStoredProcedure方法
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="tableName">自定义表格名称</param>
        /// <param name="commandText">存储过程名称</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>返回表集合</returns>
        public DataTable DataAdapterReadStoredProcedure(string tableName, string commandText, params SqlParameter[] commandParameters)
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

        #region 赋值参数
        /// <summary>
        /// 赋值参数
        /// </summary>
        /// <param name="com">command对象</param>
        /// <param name="conn">connection对象</param>
        /// <param name="commandtext">Sql语句或者存储过程</param>
        /// <param name="comtype">类型</param>
        /// <param name="commandparameters">参数数组</param>
        private static void SetVariable(SqlCommand com, SqlConnection conn, string commandtext, CommandType comtype, params SqlParameter[] commandparameters)
        {
            if (conn.State != ConnectionState.Open) //打开数据库连接
                conn.Open();
            com.Connection = conn;//command的连接对象
            com.CommandText = commandtext;//Sql语句或存储过程名称
            com.CommandType = comtype;//类型，语句或存储过程
            if (commandparameters != null)//遍历添加数组值
                foreach (SqlParameter parameter in commandparameters)
                    com.Parameters.Add(parameter);
        }
        #endregion

        #region 参数控制
        /// <summary>
        /// 参数控制，判断parameter是否存在null，如果有改为DBNull
        /// </summary>
        /// <param name="dataParameter">参数</param>
        /// <returns></returns>
        public SqlParameter[] ProcessNull(SqlParameter[] dataParameter)
        {
            foreach (SqlParameter parameter in dataParameter)
                if (parameter == null)
                    parameter.Value = DBNull.Value;
            return dataParameter;
        }
        #endregion
    }
}