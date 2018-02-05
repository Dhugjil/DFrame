using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DFrame.Common
{
    /// <summary>
    /// 其他工具
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// DataTable To T List
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>T List</returns>
        public static List<T> ConvertToList<T>(DataTable table) where T : class, new()
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

    }
}
