using System;
using System.Data;
using System.Data.SqlClient;
using DFrame.DAL.Access.Interface;
using System.Configuration;
using System.Data.OleDb;

namespace DFrame.DAL.Access
{
    /// <summary>
    /// Access帮助类
    /// </summary>
    class AccessHelper : IAccessHelper
    {
        /// <summary>
        /// 数据库链接
        /// </summary>
        protected string connstr;
        /// <summary>
        /// 链接字符串从config中获取 name=AccessConnString
        /// </summary>
        public AccessHelper()
        {
            try
            {
                connstr = ConfigurationManager.ConnectionStrings["AccessConnString"].ConnectionString;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 初始化连接字符串
        /// Access2003 Provider=Microsoft.Jet.OLEDB.4.0;Data Source = XXX.mdb;
        /// Access2007 Provider=Microsoft.ACE.OLEDB.12.0;Data Source = XXX.accdb;
        /// </summary>
        /// <param name="conn">链接字符串</param>
        public AccessHelper(string conn)
        {
            connstr = conn;
        }
        protected OleDbConnection conn = new OleDbConnection();
        protected OleDbCommand comm = new OleDbCommand();
        /// <summary>
        /// 打开数据库
        /// </summary>
        private void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.ConnectionString = connstr;
                comm.Connection = conn;
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                { throw new Exception(e.Message); }
            }
        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        private void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
                comm.Dispose();
            }
        }
        /// <summary>
        /// 执行指定连接ExecuteDataTable方法
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>返回数据表</returns>
        public DataTable DataAdapterRead(string commandText)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter();
            try
            {
                openConnection();
                comm.CommandType = CommandType.Text;
                comm.CommandText = commandText;
                da.SelectCommand = comm;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                closeConnection();
            }
            return dt;
        }
        /// <summary>
        /// 执行指定连接ExecuteNonQuery方法
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteNonQuery(string commandText)
        {
            try
            {
                openConnection();
                comm.CommandType = CommandType.Text;
                comm.CommandText = commandText;
                return comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            { closeConnection(); }
        }
        /// <summary>
        /// 执行指定连接ExecuteReader方法
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>返回结果集</returns>
        public OleDbDataReader ExecuteReader(string commandText)
        {
            OleDbDataReader dr = null;
            try
            {
                openConnection();
                comm.CommandText = commandText;
                comm.CommandType = CommandType.Text;
                dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                try
                {
                    dr.Close();
                    closeConnection();
                }
                catch { }
            }
            return dr;
        }
        /// <summary>
        /// 执行指定连接ExecuteScalar方法
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>返回结果值</returns>
        public object ExecuteScalar(string commandText)
        {
            object obj = null;
            OleDbDataAdapter da = new OleDbDataAdapter();
            try
            {
                openConnection();
                comm.CommandType = CommandType.Text;
                comm.CommandText = commandText;
                obj = comm.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                closeConnection();
            }
            return obj;
        }
    }
}
