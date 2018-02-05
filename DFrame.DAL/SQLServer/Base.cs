using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace DFrame.DAL.SQLServer
{
    /// <summary>
    /// Dapper基类
    /// </summary>
    public class Base
    {
        /// <summary>
        /// 数据库链接
        /// </summary>
        protected static string connstr;
        /// <summary>
        /// 链接字符串从config中获取 name=SQLServerConnString
        /// </summary>
        protected Base()
        {
            try
            {
                connstr = ConfigurationManager.ConnectionStrings["SQLServerConnString"].ConnectionString;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 初始化连接字符串
        /// </summary>
        /// <param name="conn">链接字符串</param>
        protected Base(string conn)
        {
            connstr = conn;
        }
        /// <summary>
        /// 数据库操作加锁用 static修饰会在整个程序池里共享，所以对所有iis请求起作用。
        /// </summary>
        protected static object LockUp = new object();
        /// <summary>
        /// 数据库建立链接
        /// </summary>
        /// <returns></returns>
        protected static SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            return conn;
        }
        /// <summary>
        /// 获取数据库名称
        /// </summary>
        protected static string DataBaseName
        {
            get
            {
                Regex regex = new Regex("(database|initial[ ]*catalog)[ ]*=[ ]*(?<databasename>[0-9a-zA-Z]*);", RegexOptions.IgnoreCase);
                Match mc = regex.Match(connstr);
                return mc.Groups["databasename"].Value;
            }
        }
    }
}