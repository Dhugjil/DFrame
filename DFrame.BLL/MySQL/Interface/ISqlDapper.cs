using System.Collections.Generic;
using System.Data;

namespace DFrame.BLL.MySQL
{
    /// <summary>
    /// MS SQLServer Dapper接口
    /// </summary>
    public interface ISqlDapper
    {
        /// <summary>
        /// 新增单条记录
        /// </summary>
        /// <typeparam name="T">表名 表名和实体名相同</typeparam>
        /// <param name="obj">插入字段</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <param name="userType">用户类型</param>
        /// <param name="log">是否写日志</param>
        /// <returns>受影响行数    -1执行错误</returns>
        int Insert<T>(T obj, bool transaction, int? timeOut, CommandType? commandType, string userType) where T : class;
        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <typeparam name="T">表名 表名和实体名相同</typeparam>
        /// <param name="obj">插入字段集合</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <param name="userType">用户类型</param>
        /// <param name="log">是否写日志</param>
        /// <returns>受影响行数    -1执行错误</returns>
        int Insert<T>(T[] obj, bool transaction, int? timeOut, CommandType? commandType, string userType) where T : class;
        /// 删除记录
        /// </summary>
        /// <typeparam name="T">实体类/表名</typeparam>
        /// <param name="where">条件</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <param name="userType">用户类型</param>
        /// <param name="log">是否写日志</param>
        /// <returns></returns>
        int Delete<T>(T where, bool transaction, int? timeOut, CommandType? commandType, string userType) where T : class;
        /// 删除记录
        /// </summary>
        /// <typeparam name="T">实体类/表名</typeparam>
        /// <param name="where">sql条件</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <param name="userType">用户类型</param>
        /// <param name="log">是否写日志</param>
        /// <returns>受影响行数    -1执行错误</returns>
        int Delete<T>(string where, bool transaction, int? timeOut, CommandType? commandType, string userType) where T : class;
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <typeparam name="T">实体类/表名</typeparam>
        /// <param name="obj">更新字段</param>
        /// <param name="where">条件集合（非class）</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <param name="userType">用户类型</param>
        /// <param name="log">是否写日志</param>
        /// <returns>受影响行数    -1执行错误</returns>
        int Update<T>(T obj, List<T> where, bool transaction, int? timeOut, CommandType? commandType, string userType) where T : class;
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <typeparam name="T">实体类/表名</typeparam>
        /// <param name="obj">更新字段</param>
        /// <param name="where">条件</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <param name="userType">用户类型</param>
        /// <param name="log">是否写日志</param>
        /// <returns>受影响行数    -1执行错误</returns>
        int Update<T>(T obj, T where, bool transaction, int? timeOut, CommandType? commandType, string userType) where T : class;
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <typeparam name="T">实体类/表名</typeparam>
        /// <param name="obj">更新字段</param>
        /// <param name="where">sql条件</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <param name="userType">用户类型</param>
        /// <param name="log">是否写日志</param>
        /// <returns>受影响行数    -1执行错误</returns>
        int Update<T>(T obj, string where, bool transaction, int? timeOut, CommandType? commandType, string userType) where T : class;
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="userType">用户类型</param>
        /// <param name="log">是否写日志</param>
        /// <returns>执行结果</returns>
        int Execute(string commandText, string msg, string userType);
        /// <summary>
        /// 查询Model集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件 集合 一般用户模糊查询</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">排序方式</param>
        /// <returns>实体集合</returns>
        List<T> GetModelList<T>(T[] where, string sort, string order) where T : class;
        /// <summary>
        /// 查询Model集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">排序方式</param>
        /// <returns>实体集合</returns>
        List<T> GetModelList<T>(T where, string sort, string order) where T : class;
        /// <summary>
        /// 查询Model集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">sql条件</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">排序方式</param>
        /// <returns>实体集合</returns>
        List<T> GetModelList<T>(string where, string sort, string order) where T : class;
        /// <summary>
        /// 分页查询Model表
        /// </summary>
        /// <param name="count">总行数</param>
        /// <param name="dataBase">数据库名</param>
        /// <param name="table">表名</param>
        /// <param name="where">条件 集合 一般用户模糊查询</param>
        /// <param name="rows">每页显示个数</param>
        /// <param name="page">页码</param>
        /// <param name="sort">排序字段 默认：表名ID</param>
        /// <param name="order">排序方式</param>
        /// <returns>实体集合</returns>
        List<T> GetModelList<T>(ref long count, T[] where, int? rows, int? page, string sort, string order) where T : class;
        /// <summary>
        /// 分页查询Model表
        /// </summary>
        /// <param name="count">总行数</param>
        /// <param name="dataBase">数据库名</param>
        /// <param name="table">表名</param>
        /// <param name="where">条件</param>
        /// <param name="rows">每页显示个数</param>
        /// <param name="page">页码</param>
        /// <param name="sort">排序字段 默认：表名ID</param>
        /// <param name="order">排序方式</param>
        /// <returns>实体集合</returns>
        List<T> GetModelList<T>(ref long count, T where, int? rows, int? page, string sort, string order) where T : class;
        /// <summary>
        /// 分页查询Model表
        /// </summary>
        /// <param name="count">总行数</param>
        /// <param name="dataBase">数据库名</param>
        /// <param name="table">表名</param>
        /// <param name="where">sql条件</param>
        /// <param name="rows">每页显示个数</param>
        /// <param name="page">页码</param>
        /// <param name="sort">排序字段 默认：表名ID</param>
        /// <param name="order">排序方式</param>
        /// <returns>实体集合</returns>
        List<T> GetModelList<T>(ref long count, string where, int? rows, int? page, string sort, string order) where T : class;
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">查询条件</param>
        /// <returns>获取实体</returns>
        T GetModel<T>(T where) where T : class;
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">sql查询条件</param>
        /// <returns>获取实体</returns>
        T GetModel<T>(string where) where T : class;
        /// <summary>
        /// 获取值（单）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="select">查询字段</param>
        /// <param name="where">sql查询条件</param>
        /// <returns>查询值</returns>
        object ExecuteScalar<T>(string select, string where) where T : class;
        /// <summary>
        /// 获取值（单）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="select">查询字段</param>
        /// <param name="where">查询条件</param>
        /// <returns>查询值</returns>
        object ExecuteScalar<T>(string select, T where) where T : class;
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <returns>查询结果</returns>
        IEnumerable<dynamic> Query(string commandText);
    }
}
