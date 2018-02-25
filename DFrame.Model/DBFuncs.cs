using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace DFrame.Model
{
    /// <summary>
    /// 包含所有DBModel私有方法
    /// </summary>
    internal static class DBFuncs
    {
        internal static string connectionString = string.Empty;
        /// <summary>
        /// 根据lambda表达式 生成查询字段
        /// </summary>
        /// <typeparam name="TSource">数据实体类型</typeparam>
        /// <param name="_DBSelect">查询类</param>
        /// <param name="func">lambda表达式</param>
        internal static void GetSelectField<TSource>(this DBSelect _DBSelect, Expression<Func<TSource, object>> func) where TSource : DBModel
        {
            if (_DBSelect._SQLBase.select.Count > 0)
                _DBSelect._SQLBase.select.Clear();

            if (func.Body is MemberExpression)
            {
                MemberExpression exp = (MemberExpression)func.Body;
                _DBSelect._SQLBase.select.Add(exp.Expression.Type.Name + "." + exp.Member.Name);
            }
            else if (func.Body is MemberInitExpression)
            {
                MemberInitExpression exp = (MemberInitExpression)func.Body;
                foreach (var item in exp.Bindings)
                {
                    _DBSelect._SQLBase.select.Add(exp.Type.Name + "." + item.Member.Name);
                }
            }
            else if (func.Body is UnaryExpression)
            {
                UnaryExpression exp = (UnaryExpression)func.Body;
                if (exp.Operand is MemberExpression)
                {
                    _DBSelect._SQLBase.select.Add(GetMemberExpression((MemberExpression)exp.Operand));
                }
            }
            else
            {
                throw new Exception();
            }
        }
        /// <summary>
        /// 根据lambda表达式 获取查询条件
        /// </summary>
        /// <typeparam name="TSource">数据实体类型</typeparam>
        /// <param name="_DBWhere">条件类</param>
        /// <param name="func">lambda表达式</param>
        /// <returns></returns>
        internal static void GetWhereExpression<TSource>(this DBWhere _DBWhere, Expression<Func<TSource, bool>> func)
        {
            string where = string.Empty;
            if (func.Body is BinaryExpression)
            {
                where = GetBinaryExpressionSql((BinaryExpression)func.Body);
            }
            else
            {
                where = GetOtherExpression(func);
            }
            _DBWhere._DBSelect._SQLBase.where = "WHERE " + where;
        }
        /// <summary>
        /// 根据lambda表达式 获取升序排序字段
        /// </summary>
        /// <typeparam name="TSource">数据实体类型</typeparam> 
        /// <param name="_DBWhere">条件类</param>
        /// <param name="func">lambda表达式</param>
        internal static void GetOrderBy<TSource>(this DBWhere _DBWhere, Expression<Func<TSource, object>> func) where TSource : DBModel
        {
            if (func.Body is MemberExpression)
            {
                MemberExpression exp = (MemberExpression)func.Body;
                _DBWhere._DBSelect._SQLBase.orderBy.Add(exp.Expression.Type.Name + "." + exp.Member.Name + " ASC");
            }
            else if (func.Body is MemberInitExpression)
            {
                MemberInitExpression exp = (MemberInitExpression)func.Body;
                foreach (var item in exp.Bindings)
                {
                    _DBWhere._DBSelect._SQLBase.orderBy.Add(exp.Type.Name + "." + item.Member.Name + " ASC");
                }
            }
            else if (func.Body is UnaryExpression)
            {
                UnaryExpression exp = (UnaryExpression)func.Body;
                if (exp.Operand is MemberExpression)
                {
                    _DBWhere._DBSelect._SQLBase.orderBy.Add(GetMemberExpression((MemberExpression)exp.Operand) + " ASC");
                }
            }
            else
            {
                throw new Exception();
            }
        }
        /// <summary>
        /// 根据lambda表达式 获取降序排序字段
        /// </summary>
        /// <typeparam name="TSource">数据实体类型</typeparam> 
        /// <param name="_DBWhere">条件类</param>
        /// <param name="func">lambda表达式</param>
        internal static void GetOrderByDescending<TSource>(this DBWhere _DBWhere, Expression<Func<TSource, object>> func) where TSource : DBModel
        {
            if (func.Body is MemberExpression)
            {
                MemberExpression exp = (MemberExpression)func.Body;
                _DBWhere._DBSelect._SQLBase.orderBy.Add(exp.Expression.Type.Name + "." + exp.Member.Name + " DESC");
            }
            else if (func.Body is MemberInitExpression)
            {
                MemberInitExpression exp = (MemberInitExpression)func.Body;
                foreach (var item in exp.Bindings)
                {
                    _DBWhere._DBSelect._SQLBase.orderBy.Add(exp.Type.Name + "." + item.Member.Name + " DESC");
                }
            }
            else if (func.Body is UnaryExpression)
            {
                UnaryExpression exp = (UnaryExpression)func.Body;
                if (exp.Operand is MemberExpression)
                {
                    _DBWhere._DBSelect._SQLBase.orderBy.Add(GetMemberExpression((MemberExpression)exp.Operand) + " DESC");
                }
            }
            else
            {
                throw new Exception();
            }
        }

        #region 私有方法 
        /// <summary>
        /// 获取二元运算符表达式SQL语句
        /// </summary>
        /// <param name="exp">二元运算符表达式</param>
        /// <returns>SQL语句</returns>
        private static string GetBinaryExpressionSql(BinaryExpression exp)
        {
            string tempWhereSql = "({0} {1} {2})";

            string leftSql = "1";
            string rightSql = "0";
            string nodeType = "=";
            //左边计算
            if (exp.Left is BinaryExpression)
            {
                leftSql = GetBinaryExpressionSql((BinaryExpression)exp.Left);
            }
            else
            {
                leftSql = GetOtherExpression(exp.Left);
            }
            //右边计算
            if (exp.Right is BinaryExpression)
            {
                rightSql = GetBinaryExpressionSql((BinaryExpression)exp.Right);
            }
            else
            {
                rightSql = GetOtherExpression(exp.Right);
            }
            //表达式计算
            nodeType = GetExpressionNodeType(exp.NodeType);

            if (leftSql == "NULL" && rightSql == "NULL")
            {
                return "(1 = 0)";
            }
            else if (leftSql == "NULL")
            {
                if (nodeType == "=")
                {
                    return "(" + rightSql + " IS NULL)";
                }
                else
                {
                    return "(" + rightSql + " IS NOT NULL)";
                }
            }
            else if (rightSql == "NULL")
            {
                if (nodeType == "=")
                {
                    return "(" + leftSql + " IS NULL)";
                }
                else
                {
                    return "(" + leftSql + " IS NOT NULL)";
                }
            }
            else
            {
                return string.Format(tempWhereSql, leftSql, nodeType, rightSql);
            }
        }
        /// <summary>
        /// 转换获取SQL运算符
        /// </summary>
        /// <param name="type">ExpressionType</param>
        /// <returns></returns>
        private static string GetExpressionNodeType(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.OrElse:
                    return "OR";
                case ExpressionType.Or:
                    return "|";
                case ExpressionType.And:
                    return "&";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Add:
                    return "+";
                case ExpressionType.Subtract:
                    return "-";
                case ExpressionType.Multiply:
                    return "*";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Modulo:
                    return "%";
                case ExpressionType.Equal:
                    return "=";
                default:
                    throw new Exception();
            }
        }
        /// <summary>
        /// 获取常量值
        /// </summary>
        /// <param name="exp">ConstantExpression</param>
        /// <returns></returns>
        private static string GetConstantExpression(ConstantExpression exp)
        {
            if (exp.Value == null)
                return "NULL";

            if (exp.Type == typeof(bool))
                return (bool)exp.Value ? "(1 = 1)" : "(1 = 0)";
            else
                return "'" + exp.Value.ToString() + "'";
        }
        /// <summary>
        /// 获取DateTime含有变量的值
        /// </summary>
        /// <param name="exp">ConstantExpression</param>
        /// <returns></returns>
        private static string GetConstantExpressionDateTime(ConstantExpression exp)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            long mils = Convert.ToInt64(Regex.Replace(jss.Serialize(exp.Value).Split(':')[1], "\\D+", ""));
            mils = new DateTime(1970, 1, 1).Ticks + mils * 10000 + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours * 3600 * (long)10000000;
            DateTime _theTime = new DateTime(mils);
            return "'" + _theTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
        }
        /// <summary>
        /// 获取用户方法返回值
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private static string GetMethodCallExpression(MethodCallExpression exp)
        {
            if (exp.ToString().Contains("Contains"))
                return GetMethodCallExpressionContains(exp);

            throw new Exception("GetMethodCallExpression-:" + exp.ToString());
        }
        /// <summary>
        /// 获取string.Contains
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private static string GetMethodCallExpressionContains(MethodCallExpression exp)
        {
            string arg = string.Empty;
            if (exp.Arguments[0] is ConstantExpression)
            {
                ConstantExpression _arg = (ConstantExpression)exp.Arguments[0];
                arg = _arg.Value.ToString();
            }
            else if (exp.Arguments[0] is MemberExpression)
            {
                MemberExpression _arg = (MemberExpression)exp.Arguments[0];
                if (_arg.Expression is ConstantExpression)
                {
                    ConstantExpression _constant = (ConstantExpression)_arg.Expression;
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    arg = jss.Serialize(_constant.Value).Split(':')[1].Trim('"', '}');
                }
            }
            else
                throw new Exception("GetMethodCallExpressionContains-Arguments-:" + exp.ToString());

            string field = string.Empty;
            if (exp.Object is MemberExpression)
                field = GetMemberExpressionField((MemberExpression)exp.Object);
            else
                throw new Exception("GetMethodCallExpressionContains-field-:" + exp.ToString());

            return "(" + field + " LIKE '%" + arg.Replace("'", "") + "%')";
        }
        /// <summary>
        /// 获取字段、字段bool类型value、datetime.now、dateTime类型变量
        /// </summary>
        /// <param name="exp">MemberExpression</param>
        /// <returns></returns>
        private static string GetMemberExpression(MemberExpression exp)
        {
            if (exp.Type == typeof(DateTime) && exp.Expression is ConstantExpression)
                return GetConstantExpressionDateTime((ConstantExpression)exp.Expression);
            if (exp.Type == typeof(DateTime) && exp.Member.Name == "Now")
                return "GETDATE()";
            if (exp.Type == typeof(bool) && exp.Expression is MemberExpression && exp.Expression.Type.Name.Contains("Nullable"))
                return GetMemberExpressionField((MemberExpression)exp.Expression) + " IS NOT NULL";
            if (exp.Type != typeof(bool) && !exp.Expression.Type.Name.Contains("Nullable"))
                return exp.Expression.Type.Name + "." + exp.Member.Name;

            throw new Exception("GetOtherExpression-MemberExpression-:" + exp.ToString());
        }
        /// <summary>
        /// 获取实体字段
        /// </summary>
        /// <param name="exp">MemberExpression</param>
        /// <returns></returns>
        private static string GetMemberExpressionField(MemberExpression exp)
        {
            return exp.Expression.Type.Name + "." + exp.Member.Name;
        }
        /// <summary>
        /// 描述lambda表达式的值
        /// </summary>
        /// <param name="exp">LambdaExpression</param>
        /// <returns></returns>
        private static string GetLambdaExpression(LambdaExpression exp)
        {
            if (exp.Body is ConstantExpression)
                return GetConstantExpression((ConstantExpression)exp.Body);
            if (exp.Body is MethodCallExpression)
                return GetMethodCallExpression((MethodCallExpression)exp.Body);
            if (exp.Body is MemberExpression)
                return GetMemberExpression((MemberExpression)exp.Body);
            throw new Exception("GetLambdaExpression-:" + exp.ToString());
        }
        /// <summary>
        /// 获取一元运算符表达式
        /// </summary>
        /// <param name="exp">UnaryExpression</param>
        /// <returns></returns>
        private static string GetUnaryExpression(UnaryExpression exp)
        {
            if (exp.Operand is ConstantExpression)//int bool
            {
                ConstantExpression _operand = (ConstantExpression)exp.Operand;

                if (_operand.Type == typeof(bool))
                    return (bool)_operand.Value ? "1" : "0";
                else
                    return _operand.Value.ToString();
            }

            UnaryExpression _temp = exp;//long
            do
            {
                if (_temp.Operand is ConstantExpression)
                {
                    return ((ConstantExpression)_temp.Operand).Value.ToString();
                }
                else if (_temp.Operand is UnaryExpression)
                {
                    _temp = (UnaryExpression)_temp.Operand;
                }
                else
                    break;
            }
            while (_temp is UnaryExpression);

            if (exp.Operand is MemberExpression)
                return GetMemberExpression((MemberExpression)exp.Operand);

            throw new Exception("GetUnaryExpression");
        }
        /// <summary>
        /// 运算字符串
        /// </summary>
        /// <param name="exp">Expression</param>
        /// <returns></returns>
        private static string GetOtherExpression(Expression exp)
        {
            if (exp is MemberExpression)
                return GetMemberExpression((MemberExpression)exp);
            if (exp is ConstantExpression)
                return GetConstantExpression((ConstantExpression)exp);
            if (exp is MethodCallExpression)
                return GetMethodCallExpression((MethodCallExpression)exp);
            if (exp is LambdaExpression)
                return GetLambdaExpression((LambdaExpression)exp);
            if (exp is UnaryExpression)
                return GetUnaryExpression((UnaryExpression)exp);

            throw new Exception();
        }
        /// <summary>
        /// DataTable To T List
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>T List</returns>
        private static List<T> ConvertToList<T>(DataTable table) where T : class, new()
        {
            List<T> tl = new List<T>();
            if (table == null)
                return tl;
            Type type = typeof(T);
            string temp = string.Empty;
            foreach (DataRow tr in table.Rows)
            {
                T model = new T();
                PropertyInfo[] property = model.GetType().GetProperties();
                foreach (PropertyInfo P in property)
                {
                    if (!P.CanWrite)
                        break;
                    temp = P.Name;
                    if (table.Columns.Contains(temp))
                    {
                        if (tr[temp] != DBNull.Value)
                        {
                            object value = tr[temp];
                            if (!P.PropertyType.IsGenericType)
                            {
                                P.SetValue(model, Convert.ChangeType(value, P.PropertyType), null);
                            }
                            else
                            {
                                Type genericTypeDefinition = P.PropertyType.GetGenericTypeDefinition();
                                if (genericTypeDefinition == typeof(Nullable<>))
                                {
                                    P.SetValue(model, Convert.ChangeType(value, Nullable.GetUnderlyingType(P.PropertyType)), null);
                                }
                            }
                        }
                    }
                }
                tl.Add(model);
            }
            return tl;
        }
        /// <summary>
        /// 重数据库获数据
        /// </summary>
        /// <param name="_DBSelect">实体条件类</param>
        /// <returns></returns>
        private static DataTable GetTable(DBSelect _DBSelect)
        {
            string sqlstr = "SELECT {0} FROM DBO.{1} {2} {3}";
            string select = _DBSelect._SQLBase.select.Count == 0 ? "*" : string.Join(",", _DBSelect._SQLBase.select);
            string orderBy = _DBSelect._SQLBase.orderBy.Count == 0 ? "" : "ORDER BY " + string.Join(",", _DBSelect._SQLBase.orderBy);
            sqlstr = string.Format(sqlstr, select, _DBSelect._SQLBase.modelType.Name, _DBSelect._SQLBase.where, orderBy);

            SqlCommand com = new SqlCommand();
            using (SqlConnection conn = new SqlConnection())
            {
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    try
                    {
                        connectionString = ConfigurationManager.ConnectionStrings["SQLServerConnString"].ConnectionString;
                    }
                    catch
                    {
                        throw new Exception("数据库链接字符串未初始化 SQLServerConnString");
                    }
                }
                conn.ConnectionString = connectionString;
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                com.Connection = conn;
                com.CommandText = sqlstr;
                com.CommandType = CommandType.Text;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = com;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                adapter.FillSchema(dt, SchemaType.Mapped);
                if (conn.State != ConnectionState.Closed)
                    conn.Close();

                return dt;
            }
        }
        #endregion

        /// <summary>
        /// 查询总数 MSSQLServer数据库
        /// </summary>
        /// <param name="_DBSelect">实体条件类</param>
        /// <returns>总数</returns>
        internal static long GetCount(this DBSelect _DBSelect)
        {
            string sqlstr = "SELECT COUNT(*) FROM DBO." + _DBSelect._SQLBase.modelType.Name + " {0}";
            sqlstr = string.Format(sqlstr, _DBSelect._SQLBase.where);

            SqlCommand com = new SqlCommand();
            using (SqlConnection conn = new SqlConnection())
            {
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    try
                    {
                        connectionString = ConfigurationManager.ConnectionStrings["SQLServerConnString"].ConnectionString;
                    }
                    catch
                    {
                        throw new Exception("数据库链接字符串未初始化 SQLServerConnString");
                    }
                }
                conn.ConnectionString = connectionString;
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                com.Connection = conn;
                com.CommandText = sqlstr;
                com.CommandType = CommandType.Text;

                object val = com.ExecuteScalar();
                com.Parameters.Clear();
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
                return Convert.ToInt64(val);
            }
        }
        /// <summary>
        /// 从数据库获取数据集合
        /// </summary>
        /// <typeparam name="TResult">获取实体类型</typeparam>
        /// <param name="_DBSelect">实体条件类</param>
        /// <returns>List集合</returns>
        internal static List<TResult> GetList<TResult>(this DBSelect _DBSelect) where TResult : class, new()
        {
            return ConvertToList<TResult>(GetTable(_DBSelect));
        }
        /// <summary>
        /// 删除集合 MSSQLServer数据库
        /// </summary>
        /// <param name="_DBSelect">实体条件类</param>
        /// <returns>总数</returns>
        internal static int Delete(this DBSelect _DBSelect)
        {
            if (string.IsNullOrWhiteSpace(_DBSelect._SQLBase.where))
                throw new Exception("where 语法空");

            string sqlstr = "DELETE FROM DBO." + _DBSelect._SQLBase.modelType.Name + " {0}";
            sqlstr = string.Format(sqlstr, _DBSelect._SQLBase.where);

            SqlCommand com = new SqlCommand();//实例化command对象
            using (SqlConnection conn = new SqlConnection())//使用空间，完成语句时释放空间
            {
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    try
                    {
                        connectionString = ConfigurationManager.ConnectionStrings["SQLServerConnString"].ConnectionString;
                    }
                    catch
                    {
                        throw new Exception("数据库链接字符串未初始化 SQLServerConnString");
                    }
                }
                conn.ConnectionString = connectionString;

                if (conn.State != ConnectionState.Open)
                    conn.Open();
                com.Connection = conn;
                com.CommandText = sqlstr;
                com.CommandType = CommandType.Text;

                SqlTransaction trans = conn.BeginTransaction();
                com.Transaction = trans;
                try
                {
                    int val = com.ExecuteNonQuery();
                    trans.Commit();
                    com.Parameters.Clear();
                    conn.Close();
                    return val;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    trans.Rollback();
                    string massage = e.Message;
                    conn.Close();
                    throw;
                }
            }
        }
    }
}
