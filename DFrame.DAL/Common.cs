using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DFrame.DAL
{
    /// <summary>
    /// DAL公共法法
    /// </summary>
    internal class Common
    {
        /// <summary>
        /// 获取SQLServer属性类型
        /// </summary>
        /// <param name="prop">字段属性信息</param>
        /// <returns></returns>
        public static string GetSQLServerFieldTypeStr(PropertyInfo prop)
        {
            if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                return "INT";
            if (prop.PropertyType == typeof(long) || prop.PropertyType == typeof(long?))
                return "BIGINT";
            if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                return "BIT";
            if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                return "DATETIME";
            if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
                return "DECIMAL(18,2)";
            if (prop.PropertyType == typeof(float) || prop.PropertyType == typeof(float?))
                return "FLOAT";
            if (prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(Guid?))
                return "UNIQUEIDENTIFIER";
            if (prop.PropertyType == typeof(string))
                return "NVARCHAR";
            else
                throw new Exception("GetSQLServerFieldTypeStr 处属性类型：" + prop.PropertyType.Name + "未定义");
        }
        /// <summary>
        /// 获取MySQL属性类型
        /// </summary>
        /// <param name="prop">字段属性信息</param>
        /// <returns></returns>
        public static string GetMySQLFieldTypeStr(PropertyInfo prop)
        {
            if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                return "INT";
            if (prop.PropertyType == typeof(long) || prop.PropertyType == typeof(long?))
                return "BIGINT(8)";
            if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                return "BIT";
            if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                return "DATETIME";
            if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
                return "DECIMAL(18,2)";
            if (prop.PropertyType == typeof(float) || prop.PropertyType == typeof(float?))
                return "FLOAT";
            if (prop.PropertyType == typeof(char) || prop.PropertyType == typeof(char?))
                return "CHAR";
            if (prop.PropertyType == typeof(string))
                return "VARCHAR";
            else
                throw new Exception("GetMySQLFieldTypeStr 处属性类型：" + prop.PropertyType.Name + "未定义");
        }
    }
}
