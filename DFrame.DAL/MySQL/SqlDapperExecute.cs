using System;
using System.Data;
using DFrame.DAL.Dapper;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using DFrame.DAL.MySQL.Interface;

namespace DFrame.DAL.MySQL
{
    /// <summary>
    /// MySQL Dapper
    /// </summary>
    public class SqlDapperExecute : Base, ISqlDapperExecute
    {
        /// <summary>
        /// 初始化DapperExecute,连接字符串访问webconfig的MySQLConnString
        /// </summary>
        public SqlDapperExecute() : base()
        { }
        /// <summary>
        /// 初始化DapperExecute数据库链接字符串
        /// </summary>
        /// <param name="conn">MySQL连接字符串</param>
        public SqlDapperExecute(string conn) : base(conn)
        { }
        /// <summary>
        /// 新增单条记录
        /// </summary>
        /// <typeparam name="T">表名 表名和实体名相同</typeparam>
        /// <param name="obj">插入字段</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <returns>受影响行数    -1执行错误</returns>
        public int Insert<T>(T obj, bool transaction = false, int? timeOut = null, CommandType? commandType = null) where T : class
        {
            if (obj == null)
            {
                return 0;
            }
            //获取表名
            Type type = typeof(T);
            string tableName = type.Name;

            string commandText = "INSERT INTO " + DataBaseName + "." + tableName + " (" + obj.GetKeysStr() + ") VALUES (" + obj.GetValuesStr() + ")";
            lock (LockUp)
            {
                using (IDbConnection conn = OpenConnection())
                {
                    if (transaction)
                    {
                        IDbTransaction trans = conn.BeginTransaction();
                        try
                        {
                            int i = conn.Execute(commandText, obj, trans, timeOut, commandType);
                            trans.Commit();
                            return i;
                        }
                        catch
                        {
                            trans.Rollback();
                            return -1;
                        }
                    }
                    else
                    {
                        return conn.Execute(commandText, obj, null, timeOut, commandType);
                    }
                }
            }
        }
        /// <summary>
        /// 新增多条记录(注意！新增数据依据第一条记录的有效数据字段）
        /// </summary>
        /// <typeparam name="T">表名 表名和实体名相同</typeparam>
        /// <param name="obj">插入字段集合</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <returns>受影响行数    -1执行错误</returns>
        public int Insert<T>(T[] obj, bool transaction = false, int? timeOut = null, CommandType? commandType = null) where T : class
        {
            if (obj == null)
            {
                return 0;
            }
            //获取表名
            Type type = typeof(T);
            string tableName = type.Name;

            string commandText = "INSERT INTO " + DataBaseName + "." + tableName + " (" + obj[0].GetKeysStr() + ") VALUES (" + obj[0].GetValuesStr() + ")";

            lock (LockUp)
            {
                using (IDbConnection conn = OpenConnection())
                {
                    if (transaction)
                    {
                        IDbTransaction trans = conn.BeginTransaction();
                        try
                        {
                            int i = conn.Execute(commandText, obj, trans, timeOut, commandType);
                            trans.Commit();
                            return i;
                        }
                        catch
                        {
                            trans.Rollback();
                            return -1;
                        }
                    }
                    else
                    {
                        return conn.Execute(commandText, obj, null, timeOut, commandType);
                    }
                }
            }
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <typeparam name="T">实体类/表名</typeparam>
        /// <param name="where">条件</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <returns></returns>
        public int Delete<T>(T where, bool transaction = false, int? timeOut = null, CommandType? commandType = null) where T : class
        {
            //获取表名
            Type type = typeof(T);
            string tableName = type.Name;

            string commandText = "DELETE FROM " + DataBaseName + "." + tableName + " WHERE " + where.GetWhereStr();
            lock (LockUp)
            {
                using (IDbConnection conn = OpenConnection())
                {
                    if (transaction)
                    {
                        IDbTransaction trans = conn.BeginTransaction();
                        try
                        {
                            int i = conn.Execute(commandText, where, trans, timeOut, commandType);
                            trans.Commit();
                            return i;
                        }
                        catch
                        {
                            trans.Rollback();
                            return -1;
                        }
                    }
                    else
                    {
                        return conn.Execute(commandText, where, null, timeOut, commandType);
                    }
                }
            }
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <typeparam name="T">实体类/表名</typeparam>
        /// <param name="where">sql条件</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <returns>受影响行数，-1出错</returns>
        public int Delete<T>(string where = " 1 = 0 ", bool transaction = false, int? timeOut = null, CommandType? commandType = null) where T : class
        {
            Regex reg = new Regex(".*where[ ]*", RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(reg.Replace(where, "").Trim()))
                where = " 1 = 0 ";
            //获取表名
            Type type = typeof(T);
            string tableName = type.Name;

            string commandText = "DELETE FROM " + DataBaseName + "." + tableName + " WHERE " + where;
            lock (LockUp)
            {
                using (IDbConnection conn = OpenConnection())
                {
                    if (transaction)
                    {
                        IDbTransaction trans = conn.BeginTransaction();
                        try
                        {
                            int i = conn.Execute(commandText, null, trans, timeOut, commandType);
                            trans.Commit();
                            return i;
                        }
                        catch
                        {
                            trans.Rollback();
                            return -1;
                        }
                    }
                    else
                    {
                        return conn.Execute(commandText, null, null, timeOut, commandType);
                    }
                }
            }
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <typeparam name="T">实体类/表名</typeparam>
        /// <param name="obj">更新字段</param>
        /// <param name="where">条件集合（非class集合）</param>
        /// <param name="transaction">是否启用事务</param>
        /// <param name="timeOut">响应时间</param>
        /// <param name="commandType">类型</param>
        /// <returns>受影响行数    -1执行错误</returns>
        public int Update<T>(T obj, List<object> where, bool transaction = false, int? timeOut = null, CommandType? commandType = null) where T : class
        {
            return Update<T>(obj, where.GetWhereSql(obj.GetType().Name), transaction, timeOut, commandType);
        }
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
        public int Update<T>(T obj, object where, bool transaction = false, int? timeOut = null, CommandType? commandType = null) where T : class
        {

            return Update<T>(obj, where.GetWhereSql(obj.GetType().Name), transaction, timeOut, commandType);
        }
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
        public int Update<T>(T obj, string where = " 1 = 0 ", bool transaction = false, int? timeOut = null, CommandType? commandType = null) where T : class
        {
            Regex reg = new Regex(".*where[ ]*", RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(reg.Replace(where, "").Trim()))
                where = " 1 = 0 ";
            //获取表名
            Type type = typeof(T);
            string tableName = type.Name;

            string commandText = "UPDATE " + DataBaseName + "." + tableName + " SET " + obj.GetSetStr() + "  WHERE " + where;
            lock (LockUp)
            {
                using (IDbConnection conn = OpenConnection())
                {
                    if (transaction)
                    {
                        IDbTransaction trans = conn.BeginTransaction();
                        try
                        {
                            int i = conn.Execute(commandText, obj, trans, timeOut, commandType);//可能报错，因为更新字段里没有主键ID，但obj里有
                            trans.Commit();
                            return i;
                        }
                        catch
                        {
                            trans.Rollback();
                            return -1;
                        }
                    }
                    else
                    {
                        return conn.Execute(commandText, obj, null, timeOut, commandType);
                    }
                }
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <returns>执行结果</returns>
        public int Execute(string commandText)
        {
            lock (LockUp)
            {
                using (IDbConnection conn = OpenConnection())
                {
                    return conn.Execute(commandText);
                }
            }
        }
    }
}