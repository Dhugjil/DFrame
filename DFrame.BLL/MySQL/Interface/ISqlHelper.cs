using System.Data;
using System.Data.SqlClient;

namespace DFrame.BLL.MySQL
{
    /// <summary>
    /// MS SQLServer SQLHelper接口
    /// </summary>
    public interface ISqlHelper
    {
        /// <summary>
        /// 隐藏SqlParameter,键为key
        /// </summary>
        /// <param name="cacheKey">取键key</param>
        /// <param name="commandParameters">将要隐藏SqlParameter[]</param>
        void CacheParameters(string cacheKey, params SqlParameter[] commandParameters);
        /// <summary>
        /// 输入key提取隐藏SqlParameter[]
        /// </summary>
        /// <param name = "cacheKey" > SqlParameter[]的key</param>
        /// <returns>返回key的SQLParameter</returns>
        SqlParameter[] GetCachedParameters(string cacheKey);
        /// <summary>
        /// 执行自定义连接的ExecuteNonQuery方法
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="connectionstring">连接字符串</param>
        /// <param name="trans">判断是否需要事物处理</param>
        /// <param name="commandtext">Sql语句或存储过程名称</param>
        /// <param name="comtype">Sql语句类型</param>
        /// <param name="commandparameters">参数数组</param>
        /// <returns>返回受影响行数</returns>
        int ExecuteNonQuery(string connectionstring, bool transaction, string commandtext, CommandType comtype, params SqlParameter[] commandparameters);
        /// <summary>
        /// 执行指定连接的带参数ExecuteNonQueryText方法，判断是否需要事物处理
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="trans">判断是否需要事物处理</param>
        /// <param name="commandtext">Sql语句</param>
        /// <param name="commandparameters">参数数组</param>
        /// <returns>返回受影响行数</returns>
        int ExecuteNonQueryText(bool transaction, string commandtext, params SqlParameter[] commandparameters);
        /// <summary>
        /// 执行指定连接的带参数ExecuteNonQueryText方法，不需要事物处理
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="commandtext">Sql语句</param>
        /// <param name="commandparameters">参数数组</param>
        /// <returns>返回受影响行数</returns>
        int ExecuteNonQueryText(string commandtext, params SqlParameter[] commandparameters);
        /// <summary>
        /// 执行指定连接的带参数ExecuteNonQueryStoredProcedure方法，判断是否需要事物处理
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="transaction">判断是否需要事物处理</param>
        /// <param name="commandtext">Sql存储过程名称</param>
        /// <param name="commandparameters">参数数组</param>
        /// <returns>返回受影响行数</returns>
        int ExecuteNonQueryStoredProcedure(bool transaction, string commandtext, params SqlParameter[] commandparameters);
        /// <summary>
        /// 执行指定连接的带参数ExecuteNonQueryStoredProcedure方法，不需要事物处理
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="commandtext">Sql存储过程名称</param>
        /// <param name="commandparameters">参数数组</param>
        /// <returns>返回受影响行数</returns>
        int ExecuteNonQueryStoredProcedure(string commandtext, params SqlParameter[] commandparameters);
        /// <summary>
        /// 执行指定连接ExecuteNonQuery方法
        /// </summary>
        /// <param name="commandtext">Sql语句</param>
        /// <returns>返回受影响行数</returns>
        int ExecuteNonQuery(string commandtext);
        /// <summary>
        /// 查询自定义连接的ExecuteReader方法，返回查询结果集或表格
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="connectionstring">连接字符串</param>
        /// <param name="commandtext">Sql语句或者存储过程名</param>
        /// <param name="comtype">类型</param>
        /// <param name="commandparameters">参数数组</param>
        /// <returns>返回结果集</returns>
        SqlDataReader ExecuteReader(string connectionstring, string commandtext, CommandType comtype, params SqlParameter[] commandparameters);
        /// <summary>
        /// 查询指定连接ExecuteReaderText方法，返回查询结果集或表格
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="connectionstring">连接字符串</param>
        /// <param name="commandtext">Sql语句或者存储过程名</param>
        /// <param name="comtype">类型</param>
        /// <param name="commandparameters">参数数组</param>
        /// <returns>返回结果集</returns>
        SqlDataReader ExecuteReaderText(string commandtext, params SqlParameter[] commandparameters);
        /// <summary>
        /// 查询指定连接ExecuteReaderStoredProcedure方法，返回查询结果集或表格
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="connectionstring">连接字符串</param>
        /// <param name="commandtext">Sql语句或者存储过程名</param>
        /// <param name="comtype">类型</param>
        /// <param name="commandparameters">参数数组</param>
        /// <returns>返回结果集</returns>
        SqlDataReader ExecuteReaderStoredProcedure(string commandtext, params SqlParameter[] commandparameters);
        /// <summary>
        /// 执行指定连接ExecuteReader方法
        /// </summary>
        /// <param name="commandtext">Sql语句</param>
        /// <returns>返回结果集</returns>
        SqlDataReader ExecuteReader(string commandtext);
        /// <summary>
        /// 查询自定义连接的ExecuteScalar方法，返回查询集结果中的第一行第一列的值
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="connectionstring">连接字符串</param>
        /// <param name="commandtext">Sql语句或者存储过程名</param>
        /// <param name="comtype">类型</param>
        /// <param name="commandparameters">参数数组</param>
        /// <returns>返回结果值</returns>
        object ExecuteScalar(string connectionstring, string commandtext, CommandType comtype, params SqlParameter[] commandparameters);
        /// <summary>
        /// 查询指定连接ExecuteScalarText方法，返回查询集结果中的第一行第一列的值
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="connectionstring">连接字符串</param>
        /// <param name="commandtext">Sql语句或者存储过程名</param>
        /// <param name="comtype">类型</param>
        /// <param name="commandparameters">参数数组</param>
        /// <returns>返回结果值</returns>
        object ExecuteScalarText(string commandtext, params SqlParameter[] commandparameters);
        /// <summary>
        /// 查询指定连接ExecuteScalarStoredProcedure方法，返回查询集结果中的第一行第一列的值
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="connectionstring">连接字符串</param>
        /// <param name="commandtext">Sql语句或者存储过程名</param>
        /// <param name="comtype">类型</param>
        /// <param name="commandparameters">参数数组</param>
        /// <returns>返回结果值</returns>
        object ExecuteScalarStoredProcedure(string commandtext, params SqlParameter[] commandparameters);
        /// <summary>
        /// 执行指定连接ExecuteScalar方法
        /// </summary>
        /// <param name="commandtext">Sql语句</param>
        /// <returns>返回结果值</returns>
        object ExecuteScalar(string commandtext);
        /// <summary>
        ///查询自定义连接的ExecuteDataTable方法，返回查询数据表
        ///参数数组形式赋值
        /// </summary>
        /// <param name="connectionstring">自定义连接字符串</param>
        /// <param name="tablename">自定义表格名称</param>
        /// <param name="commandtext">Sql语句或存储过程名称</param>
        /// <param name="comtype">类型</param>
        /// <param name="commandparameters">参数数组</param>
        /// <returns>返回数据表</returns>
        DataTable DataAdapterRead(string connectionstring, string tablename, string commandtext, CommandType comtype, params SqlParameter[] commandparameters);
        /// <summary>
        /// 查询指定连接ExecuteDataTable方法
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="tablename">自定义表格名称</param>
        /// <param name="commandtext">Sql语句</param>
        /// <param name="commandparameters">参数数组</param>
        /// <returns>返回数据表</returns>
        DataTable DataAdapterReadText(string tablename, string commandtext, params SqlParameter[] commandparameters);
        /// <summary>
        /// 查询指定连接ExecuteDataTableStoredProcedure方法
        /// 参数数组形式赋值
        /// </summary>
        /// <param name="commandtext">存储过程名称</param>
        /// <param name="commandparameters">参数数组</param>
        /// <returns>返回表集合</returns>
        DataTable DataAdapterReadStoredProcedure(string tablename, string commandtext, params SqlParameter[] commandparameters);
        /// <summary>
        /// 执行指定连接ExecuteDataTable方法
        /// </summary>
        /// <param name="commandtext">Sql语句</param>
        /// <returns>返回数据表</returns>
        DataTable DataAdapterRead(string commandtext);
        /// <summary>
        /// 参数控制，判断parameter是否存在null，如果有改为DBNull
        /// </summary>
        /// <param name="dataParameter">参数</param>
        /// <returns></returns>
        SqlParameter[] ProcessNull(SqlParameter[] dataParameter);
    }
}

