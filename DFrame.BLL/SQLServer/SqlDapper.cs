using System;
using System.Collections.Generic;

namespace DFrame.BLL.SQLServer
{
    /// <summary>
    /// MS SQLServer Dapper
    /// </summary>
    public class SqlDapper : DFrame.BLL.SQLServer.ISqlDapper
    {
        private DFrame.DAL.SQLServer.Interface.ISqlDapperQuery _BaseQuery = new DFrame.DAL.SQLServer.SqlDapperQuery();
        private DFrame.DAL.SQLServer.Interface.ISqlDapperQuery BaseQuery
        {
            get
            {
                return _BaseQuery;
            }
            set
            {
                _BaseQuery = value;
            }
        }

        private DFrame.DAL.SQLServer.Interface.ISqlDapperExecute _BaseExecute = new DFrame.DAL.SQLServer.SqlDapperExecute();
        private DFrame.DAL.SQLServer.Interface.ISqlDapperExecute BaseExecute
        {
            get
            {
                return _BaseExecute;
            }
            set
            {
                _BaseExecute = value;
            }
        }

        public int Insert<T>(T obj, bool transaction, int? timeOut, System.Data.CommandType? commandType, string userType) where T : class
        {
            return BaseExecute.Insert<T>(obj, transaction, timeOut, commandType);
        }

        public int Insert<T>(T[] obj, bool transaction, int? timeOut, System.Data.CommandType? commandType, string userType) where T : class
        {
            return BaseExecute.Insert<T>(obj, transaction, timeOut, commandType);
        }

        public int Delete<T>(T where, bool transaction, int? timeOut, System.Data.CommandType? commandType, string userType) where T : class
        {
            return BaseExecute.Delete<T>(where, transaction, timeOut, commandType);
        }

        public int Delete<T>(string where, bool transaction, int? timeOut, System.Data.CommandType? commandType, string userType) where T : class
        {
            return BaseExecute.Delete<T>(where, transaction, timeOut, commandType);
        }

        public int Update<T>(T obj, List<T> where, bool transaction, int? timeOut, System.Data.CommandType? commandType, string userType) where T : class
        {
            return BaseExecute.Update<T>(obj, where, transaction, timeOut, commandType);
        }

        public int Update<T>(T obj, T where, bool transaction, int? timeOut, System.Data.CommandType? commandType, string userType) where T : class
        {
            return BaseExecute.Update<T>(obj, where, transaction, timeOut, commandType);
        }

        public int Update<T>(T obj, string where, bool transaction, int? timeOut, System.Data.CommandType? commandType, string userType) where T : class
        {
            return BaseExecute.Update<T>(obj, where, transaction, timeOut, commandType);
        }

        public List<T> GetModelList<T>(T[] where, string sort, string order) where T : class
        {
            return BaseQuery.GetModelList<T>(where, sort, order);
        }

        public List<T> GetModelList<T>(T where, string sort, string order) where T : class
        {
            return BaseQuery.GetModelList<T>(where, sort, order);
        }

        public List<T> GetModelList<T>(string where, string sort, string order) where T : class
        {
            return BaseQuery.GetModelList<T>(where, sort, order);
        }

        public List<T> GetModelList<T>(ref long count, T[] where, int? rows, int? page, string sort, string order) where T : class
        {
            return BaseQuery.GetModelList<T>(ref count, where, rows, page, sort, order);
        }

        public List<T> GetModelList<T>(ref long count, T where, int? rows, int? page, string sort, string order) where T : class
        {
            return BaseQuery.GetModelList<T>(ref count, where, rows, page, sort, order);
        }

        public List<T> GetModelList<T>(ref long count, string where, int? rows, int? page, string sort, string order) where T : class
        {
            return BaseQuery.GetModelList<T>(ref count, where, rows, page, sort, order);
        }

        public T GetModel<T>(T obj) where T : class
        {
            return BaseQuery.GetModel<T>(obj);
        }


        public T GetModel<T>(string where) where T : class
        {
            return BaseQuery.GetModel<T>(where);
        }

        public object ExecuteScalar<T>(string select, string where) where T : class
        {
            return BaseQuery.ExecuteScalar<T>(select, where);
        }


        public object ExecuteScalar<T>(string select, T where) where T : class
        {
            return BaseQuery.ExecuteScalar<T>(select, where);
        }


        public int Execute(string commandText, string msg, string userType)
        {
            return BaseExecute.Execute(commandText);
        }


        public IEnumerable<dynamic> Query(string commandText)
        {
            return BaseQuery.Query(commandText);
        }
    }
}
