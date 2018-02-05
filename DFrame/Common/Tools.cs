using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DFrame.Common
{
    public class Tools
    {
        ///// <summary>
        ///// DataTable To List<T>
        ///// </summary>
        ///// <typeparam name="T">实体类</typeparam>
        ///// <param name="table">DataTable</param>
        ///// <returns>List<T></returns>
        public static List<T> ConvertToList<T>(DataTable table) where T : new()
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
