using System;
using System.Linq.Expressions;

namespace DFrame.Model
{
    /// <summary>
    /// 生成数据表必须继承这个
    /// </summary>
    public abstract class DBModel : DBView
    {
        /// <summary>
        /// 获取指定类型实体
        /// </summary>
        /// <returns></returns>
        public abstract object ToObject();
        /// <summary>
        /// DBModel查询
        /// </summary>
        /// <typeparam name="TSource">DBModel派生类</typeparam>
        /// <param name="func">查询字段</param>
        /// <returns></returns>
        public static DBSelect Select<TSource>(Expression<Func<TSource, object>> func) where TSource : DBModel, new()
        {
            DBSelect model = Select<TSource>();
            model.GetSelectField(func);
            return model;
        }
        /// <summary>
        /// DBModel查询所有
        /// </summary>
        /// <typeparam name="TSource">DBModel派生类</typeparam> 
        /// <returns></returns>
        public static DBSelect Select<TSource>() where TSource : DBModel, new()
        {
            DBSelect model = new DBSelect();
            model._SQLBase = new SQLBase();
            model._SQLBase.modelType = typeof(TSource);
            return model;
        }
    }
}