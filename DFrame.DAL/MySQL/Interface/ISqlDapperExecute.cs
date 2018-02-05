using System.Collections.Generic;
using System.Data;

namespace DFrame.DAL.MySQL.Interface
{
    /// <summary>
    /// dapper执行接口
    /// </summary>
    public interface ISqlDapperExecute
    {
        /// <summary>
        /// 新增单条记录
        /// </summary>
        /// <typeparam name="T">表名 表名和实体名相同</typeparam>
        /// <param name="obj">插入字段</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <returns>受影响行数    -1执行错误</returns>
        int Insert<T>(T obj, bool transaction, int? timeOut, CommandType? commandType) where T : class;
        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <typeparam name="T">表名 表名和实体名相同</typeparam>
        /// <param name="obj">插入字段集合</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <returns>受影响行数    -1执行错误</returns>
        int Insert<T>(T[] obj, bool transaction, int? timeOut, CommandType? commandType) where T : class;
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <typeparam name="T">实体类/表名</typeparam>
        /// <param name="where">条件</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <returns></returns>
        int Delete<T>(T where, bool transaction, int? timeOut, CommandType? commandType) where T : class;
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <typeparam name="T">实体类/表名</typeparam>
        /// <param name="where">sql条件</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <returns>受影响行数    -1执行错误</returns>
        int Delete<T>(string where, bool transaction, int? timeOut, CommandType? commandType) where T : class;
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <typeparam name="T">实体类/表名</typeparam>
        /// <param name="obj">更新字段</param>
        /// <param name="where">条件集合（非class）</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <returns>受影响行数    -1执行错误</returns>
        int Update<T>(T obj, List<object> where, bool transaction, int? timeOut, CommandType? commandType) where T : class;
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <typeparam name="T">实体类/表名</typeparam>
        /// <param name="obj">更新字段</param>
        /// <param name="where">条件</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <returns>受影响行数    -1执行错误</returns>
        int Update<T>(T obj, object where, bool transaction, int? timeOut, CommandType? commandType) where T : class;
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <typeparam name="T">实体类/表名</typeparam>
        /// <param name="obj">更新字段</param>
        /// <param name="where">sql条件</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <returns>受影响行数    -1执行错误</returns>
        int Update<T>(T obj, string where, bool transaction, int? timeOut, CommandType? commandType) where T : class;
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <returns>执行结果</returns>
        int Execute(string commandText);
    }
}
