using System.Collections.Generic;

namespace DFrame.DAL.MySQL.Interface
{
    public interface ISqlDapperQuery
    {
        /// <summary>
        /// 查询Model集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件,数组 一般用户模糊查询</param>
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
        /// <summary>条件
        /// 分页查询Model表
        /// </summary>
        /// <param name="count">总行数</param>
        /// <param name="dataBase">数据库名</param>
        /// <param name="table">表名</param>
        /// <param name="where">条件（数组有效长度2）一般用户模糊查询</param>
        /// <param name="rows">每页显示个数</param>
        /// <param name="page">页码</param>
        /// <param name="sort">排序字段 默认：表名ID</param>
        /// <param name="order">排序方式</param>
        /// <returns>实体集合</returns>
        List<T> GetModelList<T>(ref long count, T[] where, int? rows, int? page, string sort, string order) where T : class;
        /// <summary>条件（数组有效长度2）一般用户模糊查询
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
        T GetModel<T>(string where ) where T : class;
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
