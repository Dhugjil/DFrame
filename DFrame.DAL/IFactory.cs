using System;
using System.Collections.Generic;
using System.Reflection;

namespace DFrame.DAL
{
    internal interface IFactory
    {
        /// <summary>
        /// 测试链接
        /// </summary>
        /// <returns></returns>
        bool TestConnection();
        /// <summary>
        /// 判断数据表是否存在
        /// </summary>
        /// <param name="tableName">数据库链接字符串</param>
        /// <returns></returns>
        bool TestTableExistence(string tableName);
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="dic">所有应有表</param>
        void CreateDBTable(Dictionary<Type, PropertyInfo[]> dic);
        /// <summary>
        /// 获取将添加SQL脚本
        /// </summary>
        /// <param name="tab">表-表字段</param>
        /// <param name="list">添加脚本</param>
        /// <returns></returns>
        void TestTableFieldExistence(KeyValuePair<Type, PropertyInfo[]> tab, ref List<ChangeField> list);

        /// <summary>
        /// 添加、删除table列
        /// </summary>
        /// <param name="list">操作列表</param>
        List<ChangeField> AddSubColumn(List<ChangeField> list);

        /// <summary>
        /// 生成外键关系
        /// </summary>
        /// <param name="list">将生成外键关系</param>
        void CreateForeignKeys(List<ChangeField> list);
    }
}
