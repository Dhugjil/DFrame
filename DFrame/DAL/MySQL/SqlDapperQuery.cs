using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DFrame.DAL.Dapper;
using DFrame.DAL.MySQL.Interface;

namespace DFrame.DAL.MySQL
{
    /// <summary>
    /// dapper查询
    /// </summary>
    public class SqlDapperQuery : Base, ISqlDapperQuery
    {
        /// <summary>
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
        public List<T> GetModelList<T>(ref long count, T[] where, int? rows = 10, int? page = 1, string sort = "ID", string order = "asc") where T : class
        {
            if (where != null)
            {
                using (IDbConnection conn = OpenConnection())
                {
                    //获取表名
                    Type type = typeof(T);
                    string tableName = type.Name;
                    //默认排序字段
                    if (string.IsNullOrWhiteSpace(sort) || sort.Trim().ToUpper() == "ID")
                        sort = tableName + "ID";

                    string selectCountCommandText = "select count(*) from " + DataBaseName + "." + tableName + " where " + where.GetWhereStr();
                    count = Convert.ToInt32(conn.ExecuteScalar(selectCountCommandText, where[0]));

                    string commandText = "SELECT * FROM " + DataBaseName + "." + tableName + " WHERE " + where.GetWhereStr() + " ORDER BY " + tableName + "." + sort + " " + order + " LIMIT " + (page - 1) * rows + "," + rows + ";";
                    return (List<T>)conn.Query<T>(commandText, where[0]);
                }
            }
            else
            {
                return null;
            }
        }
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
        public List<T> GetModelList<T>(ref long count, T where, int? rows = 10, int? page = 1, string sort = "ID", string order = "asc") where T : class
        {//可能报错，查询的列名s和实体类不对应
            using (IDbConnection conn = OpenConnection())
            {
                //获取表名
                Type type = typeof(T);
                string tableName = type.Name;
                //默认排序字段
                if (string.IsNullOrWhiteSpace(sort) || sort.Trim().ToUpper() == "ID")
                    sort = tableName + "ID";

                string selectCountCommandText = "select count(*) from " + DataBaseName + "." + tableName;
                count = conn.ExecuteScalar<int>(selectCountCommandText);
                
                string commandText = "SELECT * FROM " + DataBaseName + "." + tableName + " WHERE " + where.GetWhereStr() + " ORDER BY " + tableName + "." + sort + " " + order + " LIMIT " + (page - 1) * rows + "," + rows + ";";
                return (List<T>)conn.Query<T>(commandText, where);
            }
        }
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
        public List<T> GetModelList<T>(ref long count, string where = "1 = 0", int? rows = 10, int? page = 1, string sort = "ID", string order = "asc") where T : class
        {//可能报错，查询的列名s和实体类不对应
            using (IDbConnection conn = OpenConnection())
            {
                if (string.IsNullOrWhiteSpace(where))
                {
                    where = "1 = 0";
                }
                //获取表名
                Type type = typeof(T);
                string tableName = type.Name;
                //默认排序字段
                if (string.IsNullOrWhiteSpace(sort) || sort.Trim().ToUpper() == "ID")
                    sort = tableName + "ID";

                string selectCountCommandText = "select count(*) from " + DataBaseName + "." + tableName;
                count = conn.ExecuteScalar<int>(selectCountCommandText);
                
                string commandText = "SELECT * FROM " + DataBaseName + "." + tableName + " WHERE " + where.GetWhereStr() + " ORDER BY " + tableName + "." + sort + " " + order + " LIMIT " + (page - 1) * rows + "," + rows + ";";
                return (List<T>)conn.Query<T>(commandText);
            }
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">查询条件</param>
        /// <returns>获取实体</returns>
        public T GetModel<T>(T where) where T : class
        {
            using (IDbConnection conn = OpenConnection())
            {
                //获取表名
                Type type = typeof(T);
                string tableName = type.Name;

                string commandText = "select * from " + DataBaseName + "." + tableName + " where " + where.GetWhereStr();

                return conn.Query<T>(commandText, where).FirstOrDefault();
            }
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">sql查询条件</param>
        /// <returns>获取实体</returns>
        public T GetModel<T>(string where = "1 = 0") where T : class
        {
            using (IDbConnection conn = OpenConnection())
            {
                //获取表名
                Type type = typeof(T);
                string tableName = type.Name;

                string commandText = "select * from " + DataBaseName + "." + tableName + " where " + where;

                return conn.Query<T>(commandText).FirstOrDefault();
            }
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="select">查询字段(单个字段)</param>
        /// <param name="where">sql查询条件</param>
        /// <returns>查询值</returns>
        public object ExecuteScalar<T>(string select = "ID", string where = "1 = 0") where T : class
        {
            using (IDbConnection conn = OpenConnection())
            {
                //获取表名
                Type type = typeof(T);
                string tableName = type.Name;

                //默认主键ID
                if (select == "ID")
                    select = tableName + select;

                string commandText = "select " + select + " from " + DataBaseName + "." + tableName + " where " + where;

                return conn.ExecuteScalar(commandText);
            }
        }

        /// <summary>
        /// 获取值（单）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="select">查询字段</param>
        /// <param name="where">查询条件</param>
        /// <returns>查询值</returns>
        public object ExecuteScalar<T>(string select, T where) where T : class
        {
            using (IDbConnection conn = OpenConnection())
            {
                //获取表名
                Type type = typeof(T);
                string tableName = type.Name;

                //默认主键ID
                if (select == "ID")
                    select = tableName + select;

                string commandText = "select " + select + " from " + DataBaseName + "." + tableName + " where " + where.GetWhereStr();

                return conn.ExecuteScalar(commandText, where);
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <returns>查询结果</returns>
        public IEnumerable<dynamic> Query(string commandText)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.Query(commandText);
            }
        }
        /// <summary>
        /// 查询Model集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件,数组 一般用户模糊查询</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">排序方式</param>
        /// <returns>实体集合</returns>
        public List<T> GetModelList<T>(T[] where, string sort, string order) where T : class
        {
            if (where != null)
            {
                using (IDbConnection conn = OpenConnection())
                {
                    //获取表名
                    Type type = typeof(T);
                    string tableName = type.Name;
                    //默认排序字段
                    if (string.IsNullOrWhiteSpace(sort) || sort.Trim().ToUpper() == "ID")
                        sort = tableName + "ID";

                    string commandText = @"select * from " + DataBaseName + "." + tableName + " where " + where.GetWhereStr();
                    return (List<T>)conn.Query<T>(commandText, where[0]);
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 查询Model集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">排序方式</param>
        /// <returns>实体集合</returns>
        public List<T> GetModelList<T>(T where, string sort, string order) where T : class
        {
            using (IDbConnection conn = OpenConnection())
            {
                //获取表名
                Type type = typeof(T);
                string tableName = type.Name;
                //默认排序字段
                if (string.IsNullOrWhiteSpace(sort) || sort.Trim().ToUpper() == "ID")
                    sort = tableName + "ID";

                string commandText = @"select * from " + DataBaseName + "." + tableName + " where " + where.GetWhereStr();
                return (List<T>)conn.Query<T>(commandText, where);
            }
        }

        /// <summary>
        /// 查询Model集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">sql条件</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">排序方式</param>
        /// <returns>实体集合</returns>
        public List<T> GetModelList<T>(string where, string sort = "ID", string order = "asc") where T : class
        {
            using (IDbConnection conn = OpenConnection())
            {
                if (string.IsNullOrWhiteSpace(where))
                {
                    where = "1 = 0";
                }
                //获取表名
                Type type = typeof(T);
                string tableName = type.Name;
                //默认排序字段
                if (string.IsNullOrWhiteSpace(sort) || sort.Trim().ToUpper() == "ID")
                    sort = tableName + "ID";

                string commandText = @"select * from " + DataBaseName + "." + tableName + " where " + where;
                return (List<T>)conn.Query<T>(commandText);
            }
        }
    }
}