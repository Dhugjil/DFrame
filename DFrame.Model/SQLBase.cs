using System;
using System.Collections.Generic;

namespace DFrame.Model
{
    /// <summary>
    /// DBModel查询基类
    /// </summary>
    internal class SQLBase
    {
        /// <summary>
        /// 实体类型（与数据库表名对应）
        /// </summary>
        public Type modelType = null;
        /// <summary>
        /// 实体的查询字段
        /// </summary>
        public List<string> select = new List<string>();
        /// <summary>
        /// 实体的插叙条件
        /// </summary>
        public string where = string.Empty;
        /// <summary>
        /// 实体的排序字段及方式
        /// </summary>
        public List<string> orderBy = new List<string>();

        /// <summary>
        /// 实体最终查询语句
        /// </summary>
        public string querySql = string.Empty;
    }
}
