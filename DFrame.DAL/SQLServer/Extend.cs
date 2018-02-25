using System;
using System.Collections.Generic;
using System.Reflection;

namespace DFrame.DAL.SQLServer
{
    internal static class Extend
    {
        /// <summary>
        /// 获取（带值）and查询条件
        /// 如果实体变量内部值全null是全部查询
        /// </summary>
        /// <param name="model">实体类条件</param>
        /// <returns></returns>
        internal static string GetWhereSql(this object model)
        {
            if (model == null)
                return " 1 = 0 ";

            //实体单元查询条件
            List<string> modelStr = new List<string>();
            //获取实体类型
            Type modelType = model.GetType();

            //遍历属性字段
            PropertyInfo[] prop = modelType.GetProperties();
            foreach (PropertyInfo propertyInfo in prop)
            {
                //字段非空是加入条件
                if (propertyInfo.GetValue(model) != null)
                {
                    //获取字段类型 下面根据字段类型加引号之类sql语句
                    Type attType = propertyInfo.PropertyType;

                    if (attType == typeof(int) || attType == typeof(int?))
                    {
                        modelStr.Add(" " + propertyInfo.Name + " = " + ((int)propertyInfo.GetValue(model)).ToString());
                    }
                    else if (attType == typeof(decimal) || attType == typeof(decimal?))
                    {
                        modelStr.Add(" " + propertyInfo.Name + " = " + ((decimal)propertyInfo.GetValue(model)).ToString());
                    }
                    else if (attType == typeof(float) || attType == typeof(float?))
                    {
                        modelStr.Add(" " + propertyInfo.Name + " = " + ((float)propertyInfo.GetValue(model)).ToString());
                    }
                    else if (attType == typeof(double) || attType == typeof(double?))
                    {
                        modelStr.Add(" " + propertyInfo.Name + " = " + ((double)propertyInfo.GetValue(model)).ToString());
                    }
                    else if (attType == typeof(bool) || attType == typeof(bool?))
                    {
                        string temp = "";
                        if ((bool)propertyInfo.GetValue(model))
                            temp = "1";
                        else
                            temp = "0";
                        modelStr.Add(" " + propertyInfo.Name + " = " + temp);
                    }
                    else if (attType == typeof(DateTime) || attType == typeof(DateTime?))
                    {
                        modelStr.Add(" " + propertyInfo.Name + " = '" + ((DateTime)propertyInfo.GetValue(model)).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
                    }
                    else if (attType == typeof(string))
                    {
                        modelStr.Add(" " + propertyInfo.Name + " = '" + (string)propertyInfo.GetValue(model) + "'");
                    }
                    else
                    {
                        modelStr.Add(" 1 = 0 ");
                    }
                }
            }
            if (modelStr.Count != 0)
                return string.Join(" and ", modelStr);
            else
                return " 1 = 1 ";
        }
        /// <summary>
        /// 获取（带值）and查询条件 多个实体间or查询
        /// 如果实体变量内部值全null是全部查询
        /// </summary>
        /// <param name="list">实体类条件集合</param>
        /// <returns></returns>
        internal static string GetWhereSql(this List<object> list)
        {
            if (list == null)
                return " 1 = 0 ";

            //实体集合查询条件
            List<string> modelsStr = new List<string>();
            //遍历实体集合
            foreach (object model in list)
            {
                //实体单元查询条件
                List<string> modelStr = new List<string>();
                //获取实体类型
                Type modelType = model.GetType();

                //遍历属性字段
                PropertyInfo[] prop = modelType.GetProperties();
                foreach (PropertyInfo propertyInfo in prop)
                {
                    //字段非空是加入条件
                    if (propertyInfo.GetValue(model) != null)
                    {
                        //获取字段类型 下面根据字段类型加引号之类sql语句
                        Type attType = propertyInfo.PropertyType;

                        if (attType == typeof(int) || attType == typeof(int?))
                        {
                            modelStr.Add(" " + propertyInfo.Name + " = " + ((int)propertyInfo.GetValue(model)).ToString());
                        }
                        else if (attType == typeof(decimal) || attType == typeof(decimal?))
                        {
                            modelStr.Add(" " + propertyInfo.Name + " = " + ((decimal)propertyInfo.GetValue(model)).ToString());
                        }
                        else if (attType == typeof(float) || attType == typeof(float?))
                        {
                            modelStr.Add(" " + propertyInfo.Name + " = " + ((float)propertyInfo.GetValue(model)).ToString());
                        }
                        else if (attType == typeof(double) || attType == typeof(double?))
                        {
                            modelStr.Add(" " + propertyInfo.Name + " = " + ((double)propertyInfo.GetValue(model)).ToString());
                        }
                        else if (attType == typeof(bool) || attType == typeof(bool?))
                        {
                            string temp = "";
                            if ((bool)propertyInfo.GetValue(model))
                                temp = "1";
                            else
                                temp = "0";
                            modelStr.Add(" " + propertyInfo.Name + " = " + temp);
                        }
                        else if (attType == typeof(DateTime) || attType == typeof(DateTime?))
                        {
                            modelStr.Add(" " + propertyInfo.Name + " = '" + ((DateTime)propertyInfo.GetValue(model)).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
                        }
                        else if (attType == typeof(string))
                        {
                            modelStr.Add(" " + propertyInfo.Name + " = '" + (string)propertyInfo.GetValue(model) + "'");
                        }
                        else
                        {
                            modelStr.Add(" 1 = 0 ");
                        }
                    }
                }
                if (modelStr != null)
                {
                    modelsStr.Add(" ( " + string.Join(" and ", modelStr) + " ) ");
                }
            }
            if (modelsStr.Count != 0)
                return string.Join(" or ", modelsStr);
            else
                return " 1 = 1 ";
        }
        /// <summary>
        /// 获取（不带值）and查询条件
        /// 如果实体变量内部值全null是全部查询
        /// </summary>
        /// <param name="obj">条件集合用户模糊查询</param>
        /// <returns></returns>
        internal static string GetWhereStr(this object[] obj)
        {
            if (obj == null)
                return " 1 = 0 ";
            if (obj.Length == 1)
            {
                Type type = obj[0].GetType();
                PropertyInfo[] prop = type.GetProperties();
                List<string> list = new List<string>();
                foreach (PropertyInfo propertyInfo in prop)
                {
                    if (propertyInfo.GetValue(obj[0]) != null)
                    {
                        list.Add(" " + propertyInfo.Name + " = @" + propertyInfo.Name);
                    }
                }
                if (list.Count != 0)
                    return string.Join(" and ", list);
                else
                    return " 1 = 1 ";
            }
            else
            {
                Type type = obj[0].GetType();
                PropertyInfo[] prop = type.GetProperties();
                List<string> list = new List<string>();
                foreach (PropertyInfo propertyInfo in prop)
                {
                    if (propertyInfo.GetValue(obj[0]) != null)
                    {
                        Type attType = propertyInfo.PropertyType;
                        if (propertyInfo.GetValue(obj[0]).ToString() == propertyInfo.GetValue(obj[1]).ToString())
                        {
                            list.Add(" " + propertyInfo.Name + " = @" + propertyInfo.Name);
                        }
                        else if (propertyInfo.GetValue(obj[0]) != propertyInfo.GetValue(obj[1]) && propertyInfo.GetValue(obj[1]).ToString().Trim() == "%")
                        {
                            //获取字段类型 下面根据字段类型加引号之类sql语句
                            if (attType == typeof(string))
                            {
                                string likeval = propertyInfo.GetValue(obj[0]).ToString().Trim();
                                string replaseChars = "',\"%=><";
                                char[] c = replaseChars.ToCharArray();
                                foreach (char item in c)
                                {
                                    likeval = likeval.Replace(item.ToString(), "");
                                }
                                list.Add(" " + propertyInfo.Name + " like '%" + likeval + "%'");
                            }
                            else
                            {
                                list.Add(" " + propertyInfo.Name + " = @" + propertyInfo.Name);
                            }
                        }
                        else if (propertyInfo.GetValue(obj[0]) != propertyInfo.GetValue(obj[1]) && (attType == typeof(DateTime) || attType == typeof(DateTime?)))
                        {
                            DateTime dt = (DateTime)propertyInfo.GetValue(obj[0]);
                            DateTime checkDT = (DateTime)propertyInfo.GetValue(obj[1]);
                            if (checkDT == DateTime.MaxValue.Date)
                            {
                                list.Add(" " + type.Name + "." + propertyInfo.Name + " <= @" + propertyInfo.Name + "");
                            }
                            else if (checkDT == DateTime.MinValue)
                            {
                                list.Add(" " + type.Name + "." + propertyInfo.Name + " >= @" + propertyInfo.Name + "");
                            }
                            else
                            {
                                list.Add(" " + type.Name + "." + propertyInfo.Name + " = @" + propertyInfo.Name + "");
                            }
                        }
                    }
                }
                if (list.Count != 0)
                    return string.Join(" and ", list);
                else
                    return " 1 = 1 ";
            }
        }
        /// <summary>
        /// 获取（不带值）and查询条件
        /// 如果实体变量内部值全null是全部查询
        /// </summary>
        /// <param name="obj">条件查询实体</param>
        /// <returns></returns>
        internal static string GetWhereStr(this object obj)
        {
            if (obj == null)
                return " 1 = 0 ";

            Type type = obj.GetType();
            PropertyInfo[] prop = type.GetProperties();
            List<string> list = new List<string>();
            foreach (PropertyInfo propertyInfo in prop)
            {
                if (propertyInfo.GetValue(obj) != null)
                {
                    list.Add(" " + propertyInfo.Name + " = @" + propertyInfo.Name);
                }
            }
            if (list.Count != 0)
                return string.Join(" and ", list);
            else
                return " 1 = 1 ";
        }
        /// <summary>
        /// 获取（不带值）新增实体字段名称，带@
        /// </summary>
        /// <param name="obj">新增实体</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        internal static string GetValuesStr(this object obj, string tableName = "")
        {
            if (obj == null)
                return "";

            Type type = obj.GetType();
            PropertyInfo[] prop = type.GetProperties();
            List<string> list = new List<string>();
            foreach (PropertyInfo propertyInfo in prop)
            {
                if (propertyInfo.Name.ToUpper() != tableName.ToUpper() + "ID" && propertyInfo.GetValue(obj) != null)
                {
                    list.Add(" @" + propertyInfo.Name);
                }
            }
            return string.Join(",", list);
        }
        /// <summary>
        /// 获取 新增实体字段名称
        /// </summary>
        /// <param name="obj">新增实体</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        internal static string GetKeysStr(this object obj, string tableName = "")
        {
            if (obj == null)
                return "";

            Type type = obj.GetType();
            PropertyInfo[] prop = type.GetProperties();
            List<string> list = new List<string>();
            foreach (PropertyInfo propertyInfo in prop)
            {
                if (propertyInfo.Name.ToUpper() != tableName.ToUpper() + "ID" && propertyInfo.GetValue(obj) != null)
                    list.Add(" " + propertyInfo.Name);
            }
            return string.Join(",", list);
        }
        /// <summary>
        /// 获取（不带值）更新实体字段
        /// </summary>
        /// <param name="obj">条件</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        internal static string GetSetStr(this object obj, string tableName)
        {
            if (obj == null)
                return "";

            Type type = obj.GetType();
            PropertyInfo[] prop = type.GetProperties();
            List<string> list = new List<string>();
            foreach (PropertyInfo propertyInfo in prop)
            {
                if (propertyInfo.Name.ToUpper() != tableName.ToUpper() + "ID" && propertyInfo.GetValue(obj) != null)
                {
                    list.Add(" " + propertyInfo.Name + " = @" + propertyInfo.Name);
                }
            }
            return string.Join(",", list);
        }
    }
}