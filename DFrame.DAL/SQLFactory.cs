using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace DFrame.DAL
{
    /// <summary>
    /// 数据库加工厂
    /// </summary>
    public class SQLFactory : IDisposable
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public enum DatabaseType
        {
            /// <summary>
            /// MSSQLServer
            /// </summary>
            MSSQLServer = 1,
            /// <summary>
            /// MySQL
            /// </summary>
            MySQL = 2//,
            //Access = 3
        }

        private Assembly _assembly;
        private DatabaseType _databaseType;
        private string _databaseName;
        private string _connString;
        private IFactory factory;

        private SQLFactory(DatabaseType databaseType, string databaseName, Assembly assembly, string connString = "")
        {
            this._assembly = assembly;
            this._databaseType = databaseType;
            this._databaseName = databaseName;
            if (string.IsNullOrWhiteSpace(connString))
            {
                switch (databaseType)
                {
                    case DatabaseType.MSSQLServer:
                        _connString = ConfigurationManager.ConnectionStrings["SQLServerConnString"].ConnectionString;
                        break;
                    case DatabaseType.MySQL:
                        _connString = ConfigurationManager.ConnectionStrings["MySQLConnString"].ConnectionString;
                        break;
                    //case DatabaseType.Access:
                    //    _connString = ConfigurationManager.ConnectionStrings["AccessConnString"].ConnectionString;
                    //    break;
                    default:
                        new Exception(databaseType.ToString() + "databaseType undefind");
                        break;
                }
            }
            else
                _connString = connString.Trim();
        }

        /// <summary>
        /// 公共实体类下所有有效公共属性
        /// </summary>
        private Dictionary<Type, PropertyInfo[]> classPropertiesDic = new Dictionary<Type, PropertyInfo[]>();
        #region 第一步获取有效信息
        /// <summary>
        /// 公共实体类
        /// </summary>
        private List<Type> publicClass = new List<Type>();
        /// <summary>
        /// 从指定Assembly下获取有效数据信息
        /// </summary>
        private void GetValidInfos()
        {
            try
            {
                Type[] types = _assembly.GetTypes();
                foreach (Type item in types)
                {
                    if (item.IsPublic && item.IsClass && item.BaseType == typeof(DFrame.Model.DBModel))
                    {
                        publicClass.Add(item);
                    }
                }
                foreach (Type item in publicClass)
                {
                    classPropertiesDic[item] = item.GetProperties();
                    //----------后期这里可以改主键格式 如ID
                    if ((from i in classPropertiesDic[item] where i.Name == item.Name + "ID" select i).Count() == 0)
                        throw new Exception("表:" + item.Name + "不存在" + item.Name + "ID(约束为主键)");
                }
                publicClass.Clear();
            }
            catch (Exception e)
            {
                throw new Exception("获取指定Assembly下的有效信息失败！Exception:" + e.Message);
            }
        }
        #endregion

        #region 第二步创建表（只包含主键id guid）
        private void CreateDBTable()
        {
            try
            {
                factory.CreateDBTable(classPropertiesDic);
            }
            catch (Exception e)
            {
                throw new Exception("第二步创建表（只包含主键id guid）失败，Exception:" + e.Message);
            }
        }
        #endregion

        /// <summary>
        /// 将执行生成表字段
        /// </summary>
        private List<ChangeField> changeField = new List<ChangeField>();
        #region 第三步跟数据库字段进行对比
        private void ObtainExecuteField()
        {
            try
            {
                foreach (KeyValuePair<Type, PropertyInfo[]> item in classPropertiesDic)
                {
                    factory.TestTableFieldExistence(item, ref changeField);
                }
            }
            catch (Exception e)
            {
                throw new Exception("第三步跟数据库字段进行对比获取将执行字段失败，Exception:" + e.Message);
            }
        }
        #endregion

        /// <summary>
        /// 将生成外键关系字段
        /// </summary>
        private List<ChangeField> foreignKeys = new List<ChangeField>();
        #region 第四步 添加、删除列
        private void AddSubColumn()
        {
            try
            {
                foreignKeys = factory.AddSubColumn(changeField);
            }
            catch (Exception e)
            {
                throw new Exception("第四步 添加、删除列出错。AddSubColumn-Exception：" + e.Message);
            }
        }
        #endregion

        #region 第五部 生成外键关系
        private void CreateForeignKeys()
        {
            try
            {
                factory.CreateForeignKeys(foreignKeys);
            }
            catch (Exception e)
            {
                throw new Exception("第五部 生成外键关系。CreateForeignKeys-Exception：" + e.Message);
            }
        }
        #endregion

        /// <summary>
        /// 创建数据库结构
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="assembly">目标实体类</param>
        /// <param name="connString">链接字符串</param>
        public static void Create(DatabaseType databaseType, string databaseName, Assembly assembly, string connString = "")
        {
            //Process processes = Process.GetCurrentProcess();
            //Debug.WriteLine("start:" + databaseType.ToString() + "\t\t\t\t" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff") + "\t\t\t\t线程：" + processes.Id.ToString());
            try
            {
                using (SQLFactory cla = new SQLFactory(databaseType, databaseName, assembly, connString))
                {
                    switch (databaseType)
                    {
                        case DatabaseType.MSSQLServer:
                            cla.factory = SQLServerFactory.CreateFactory(cla._connString, databaseName);
                            if (!cla.factory.TestConnection())
                                throw new Exception(databaseType.ToString() + ":" + cla._connString + ":link failed");
                            break;
                        case DatabaseType.MySQL:
                            cla.factory = MySQLFactory.CreateFactory(cla._connString, databaseName);
                            if (!cla.factory.TestConnection())
                                throw new Exception(databaseType.ToString() + ":" + cla._connString + ":link failed");
                            break;
                        default:
                            throw new Exception(databaseType.ToString() + ":" + "databaseType undefind");
                    }
                    cla.publicClass.Clear();
                    cla.classPropertiesDic.Clear();

                    cla.GetValidInfos();
                    cla.CreateDBTable();
                    cla.ObtainExecuteField();
                    cla.AddSubColumn();
                    cla.CreateForeignKeys();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            //Debug.WriteLine("end:" + databaseType.ToString() + "\t\t\t\t" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _assembly = null;
            publicClass = null;
            classPropertiesDic = null;
            factory = null;
            _connString = null;
            _databaseName = null;
            changeField = null;
            foreignKeys = null;
        }
    }
    /// <summary>
    /// 将变更的表和字段
    /// </summary>
    internal class ChangeField
    {
        /// <summary>
        /// 表格
        /// </summary>
        public Type type { get; set; }
        /// <summary>
        /// 字段属性
        /// </summary>
        public PropertyInfo propertyInfo { get; set; }
        /// <summary>
        /// 执行类型 true添加 false减去
        /// </summary>
        public bool action { get; set; }

        /// <summary>
        /// 减去用字段名称
        /// </summary>
        public string fieldName { get; set; }

        /// <summary>
        /// 主键表
        /// </summary>
        public Type foreignKeyType { get; set; }
    }
}
