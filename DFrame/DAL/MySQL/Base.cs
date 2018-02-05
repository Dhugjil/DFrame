using System.Configuration;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace DFrame.DAL.MySQL
{
    public class Base
    {
        /// <summary>
        /// 数据库链接
        /// </summary>
        private static readonly string connstr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        /// <summary>
        /// 数据库操作加锁用 static修饰会在整个程序池里共享，所以对所有iis请求起作用。
        /// </summary>
        protected static object LockUp = new object();
        /// <summary>
        /// 数据库建立链接
        /// </summary>
        /// <returns></returns>
        protected static MySqlConnection OpenConnection()
        {
            MySqlConnection conn = new MySqlConnection(connstr);
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
                Regex regex = new Regex(".*database[ ]*=[ ]*(?<databasename>[0-9a-zA-Z]*)[ ]*;", RegexOptions.IgnoreCase);
                Match mc = regex.Match(connstr);
                return mc.Groups["databasename"].Value;
            }
        }
    }
}