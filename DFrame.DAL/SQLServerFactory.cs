using System.Data;
using System.Data.SqlClient;
using DFrame.DAL.SQLServer.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace DFrame.DAL
{
    /// <summary>
    /// sqlserver脚本语法库+加工厂
    /// </summary>
    internal class SQLServerFactory : IFactory
    {
        private string _connStr;
        private string _databaseName;
        private ISQLServerHelper _sqlHelper;
        private SQLServerFactory(string connStr, string databaseName)
        {
            this._connStr = connStr;
            this._databaseName = databaseName;
            this._sqlHelper = new SQLServer.SQLServerHelper(connStr);
        }
        /// <summary>
        /// 创建Factory实体类
        /// </summary>
        /// <param name="connStr">链接字符串</param>
        /// <param name="databaseName">数据库名称</param>
        /// <returns></returns>
        public static SQLServerFactory CreateFactory(string connStr, string databaseName)
        {
            SQLServerFactory fac = new SQLServerFactory(connStr, databaseName);
            return fac;
        }

        /// <summary>
        /// 测试链接
        /// </summary>
        /// <returns></returns>
        public bool TestConnection()
        {
            //创建连接对象
            SqlConnection mySqlConnection = new SqlConnection(this._connStr);
            bool IsCanConnectioned = false;
            try
            {
                //Open DataBase
                mySqlConnection.Open();
                IsCanConnectioned = true;
            }
            catch
            {
                //Can not Open DataBase
                IsCanConnectioned = false;
            }
            finally
            {
                //Close DataBase
                mySqlConnection.Close();
            }
            //mySqlConnection is a SqlConnection object 
            if (mySqlConnection.State == ConnectionState.Closed || mySqlConnection.State == ConnectionState.Broken)
            {
                //Connection is not available  
                return IsCanConnectioned;
            }
            else
            {
                //Connection is available  
                return IsCanConnectioned;
            }
        }

        /// <summary>
        /// 判断数据表是否存在
        /// </summary>
        /// <param name="tableName">数据库链接字符串</param>
        /// <returns></returns>
        public bool TestTableExistence(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                return false;
            string sqlStr = "select count(*) from sysobjects where xtype='U' and name = '" + tableName.Trim() + "'";
            object result = _sqlHelper.ExecuteScalarText(sqlStr, null);
            if (Convert.ToInt32(result) == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 创建表 只包含int主键
        /// </summary>
        /// <param name="dic">所有应有表</param>
        public void CreateDBTable(Dictionary<Type, PropertyInfo[]> dic)
        {
            //----------后期这里可以改主键格式 如ID
            string sqlStr = @"if object_id(N'[{0}]',N'U') is null
                                begin
	                                CREATE TABLE dbo.[{0}]
	                                ({0}ID BIGINT IDENTITY(1,1) PRIMARY KEY)
                                end";
            foreach (KeyValuePair<Type, PropertyInfo[]> item in dic)
            {
                _sqlHelper.ExecuteNonQueryText(string.Format(sqlStr, item.Key.Name), null);
            }
        }

        /// <summary>
        /// 获取将添加SQL脚本
        /// </summary>
        /// <param name="tab">表-表字段</param>
        /// <param name="list">添加脚本</param>
        /// <returns></returns>
        public void TestTableFieldExistence(KeyValuePair<Type, PropertyInfo[]> tab, ref List<ChangeField> list)
        {
            if (TestTableExistence(tab.Key.Name))
            {
                string sqlStr = "Select name from syscolumns Where ID = OBJECT_ID('" + tab.Key.Name + "')";
                DataTable dt = _sqlHelper.DataAdapterReadText(null, sqlStr, null);
                //添加字段
                foreach (PropertyInfo item in tab.Value)
                {
                    DataRow[] dr = dt.Select("name = '" + item.Name + "'");
                    if (dr.Length == 0)
                    {
                        list.Add(new ChangeField
                        {
                            type = tab.Key,
                            action = true,
                            propertyInfo = item
                        });
                    }
                }
                //减去字段
                foreach (DataRow item in dt.Rows)
                {
                    if ((from i in tab.Value where i.Name == item["name"].ToString() select i).Count() == 0)
                    {
                        list.Add(new ChangeField
                        {
                            action = false,
                            type = tab.Key,
                            fieldName = item["name"].ToString()
                        });
                    }
                }
            }
            else
            {
                foreach (PropertyInfo item in tab.Value)
                {
                    list.Add(new ChangeField
                    {
                        action = true,
                        type = tab.Key,
                        propertyInfo = item
                    });
                }
            }
        }

        /// <summary>
        /// 添加、删除table列
        /// </summary>
        /// <param name="list">操作列表</param>
        public List<ChangeField> AddSubColumn(List<ChangeField> list)
        {
            List<ChangeField> addList = list.Where(x => x.action == true).ToList();
            List<ChangeField> subList = list.Where(x => x.action == false).ToList();
            List<ChangeField> foreignKeyList = new List<ChangeField>();

            #region 添加列
            string sqlStr = @"ALTER TABLE [{0}]
                                ADD {1} {2}{3} {4} {5} {6}";
            foreach (ChangeField item in addList)
            {
                string tableName = "";
                string fieldName = "";

                string fieldType = "";
                string lenght = "";

                string fieldKey = "";
                string defaultStr = "";
                string isNotNull = "";
                //获取表名
                tableName = item.type.Name;
                //获取字段名称
                fieldName = "[" + item.propertyInfo.Name + "]";

                DFrame.Model.DBFieldAttribute attr = item.propertyInfo.GetCustomAttribute<DFrame.Model.DBFieldAttribute>(false);
                if (attr != null)
                {
                    //获取字段类型设定
                    if (!string.IsNullOrWhiteSpace(attr.DBFieldType))
                        fieldType = attr.DBFieldType.Trim().ToUpper();
                    else
                        fieldType = Common.GetSQLServerFieldTypeStr(item.propertyInfo);

                    //attr.ForeignKey非空并且attr.DBFieldType空 时该字段guid类型
                    //if (attr.ForeignKey == null || !string.IsNullOrWhiteSpace(attr.DBFieldType))------------后续改成主键类型可以修改
                    if (attr.ForeignKey == null)
                    {
                        //获取字符设定长度
                        if (attr.Lenght == 4001 && (fieldType == "NVARCHAR" || fieldType == "VARBINARY" || fieldType == "VARCHAR"))
                            lenght = "(MAX)";
                        else if (fieldType == "NVARCHAR" || fieldType == "VARBINARY" || fieldType == "VARCHAR" || fieldType == "BINARY" || fieldType == "CHAR" || fieldType == "NCHAR")
                            lenght = "(" + attr.Lenght.ToString() + ")";
                    }
                    else
                    {
                        fieldType = "BIGINT";
                        item.foreignKeyType = attr.ForeignKey;
                        foreignKeyList.Add(item);
                    }

                    //获取键类型
                    if (attr.DBFieldKey != DFrame.Model.Enums.DBFieldKey.None &&
                        lenght != "(MAX)" &&
                        fieldType.ToLower() != "bit" &&
                        fieldType.ToLower() != "image" &&
                        fieldType.ToLower() != "geography" &&
                        fieldType.ToLower() != "geometry" &&
                        fieldType.ToLower() != "sql_variant" &&
                        fieldType.ToLower() != "text" &&
                        fieldType.ToLower() != "xml")
                        fieldKey = attr.DBFieldKey.ToString();
                    //获取默认值
                    if (attr.DefaultValue != null)
                    {
                        if (fieldType.ToLower() == "bigint" ||
                            fieldType.ToLower() == "binary" ||
                            fieldType.ToLower() == "char" ||
                            fieldType.ToLower() == "decimal" ||
                            fieldType.ToLower() == "float" ||
                            fieldType.ToLower() == "int" ||
                            fieldType.ToLower() == "nchar" ||
                            fieldType.ToLower() == "numeric" ||
                            fieldType.ToLower() == "real" ||
                            fieldType.ToLower() == "smallint" ||
                            fieldType.ToLower() == "smallmoney" ||
                            fieldType.ToLower() == "tinyint" ||
                            fieldType.ToLower() == "varbinary")
                            defaultStr = "DEFAULT " + attr.DefaultValue.ToString();
                        else
                            defaultStr = "DEFAULT '" + attr.DefaultValue.ToString() + "'";
                    }
                    //获取是否可空
                    if (attr.NotNull)
                        isNotNull = "NOT NULL";
                }
                else
                {
                    //获取字段类型设定
                    fieldType = Common.GetSQLServerFieldTypeStr(item.propertyInfo);
                    //获取字符设定长度
                    if (fieldType == "NVARCHAR" ||
                        fieldType == "VARBINARY" ||
                        fieldType == "VARCHAR" ||
                        fieldType == "BINARY" ||
                        fieldType == "CHAR" ||
                        fieldType == "NCHAR")
                        lenght = "(255)";
                }

                //执行
                //先带not null执行  如果添加失败，则在进行可空执行
                try
                {
                    _sqlHelper.ExecuteNonQueryText(string.Format(sqlStr, tableName, fieldName, fieldType, lenght, fieldKey, defaultStr, isNotNull), null);
                }
                catch
                {
                    _sqlHelper.ExecuteNonQueryText(string.Format(sqlStr, tableName, fieldName, fieldType, lenght, fieldKey, defaultStr, isNotNull).Replace("NOT NULL", ""), null);
                }
            }
            #endregion

            #region 删除列
            sqlStr = @"ALTER TABLE [{0}]  
                        DROP COLUMN {1} ";
            foreach (ChangeField item in subList)
            {
                string tableName = "";
                string fieldName = "";

                //获取表名
                tableName = item.type.Name;
                //获取字段名称
                fieldName = item.fieldName;

                //执行
                _sqlHelper.ExecuteNonQueryText(string.Format(sqlStr, tableName, fieldName), null);
            }
            #endregion

            return foreignKeyList;
        }

        /// <summary>
        /// 生成外键关系
        /// </summary>
        /// <param name="list">将生成外键关系</param>
        public void CreateForeignKeys(List<ChangeField> list)
        {
            string sqlStr = @"if exists(select 1 from sysobjects where name= '{0}' and xtype= 'F ')
	                                ALTER TABLE [{1}] DROP CONSTRAINT {0}; 
                                alter table [{1}] 
                                add constraint {0} foreign key({2}) references [{3}]({3}ID)";
            foreach (ChangeField item in list)
            {
                //主键表名称
                string fkTableName = item.type.Name;
                //外键字段名称
                string fkFieldName = item.propertyInfo.Name;
                //外键表名称
                string tabName = item.foreignKeyType.Name;
                //外键名称    fk_外键表名称_字段名称_主键表名称
                string foreignKeyName = "fk_" + fkTableName.ToLower() + "_" + fkFieldName.ToLower() + "_" + tabName.ToLower();
                //执行
                _sqlHelper.ExecuteNonQueryText(string.Format(sqlStr, foreignKeyName, fkTableName, fkFieldName, tabName), null);
            }
        }
    }
}
