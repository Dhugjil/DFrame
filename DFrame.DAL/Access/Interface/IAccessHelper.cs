using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace DFrame.DAL.Access.Interface
{
    /// <summary>
    /// Access帮助接口
    /// </summary>
    public interface IAccessHelper
    {
        /// <summary>
        /// 执行指定连接ExecuteNonQuery方法
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>返回受影响行数</returns>
        int ExecuteNonQuery(string commandText);
        /// <summary>
        /// 执行指定连接ExecuteReader方法
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>返回结果集</returns>
        OleDbDataReader ExecuteReader(string commandText);
        /// <summary>
        /// 执行指定连接ExecuteScalar方法
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>返回结果值</returns>
        object ExecuteScalar(string commandText);
        /// <summary>
        /// 执行指定连接ExecuteDataTable方法
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>返回数据表</returns>
        DataTable DataAdapterRead(string commandText);
    }
}
