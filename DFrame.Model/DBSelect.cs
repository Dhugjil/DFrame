using System;
using System.Linq.Expressions;

namespace DFrame.Model
{
    /// <summary>
    /// 实体查询类
    /// </summary>
    public class DBSelect
    {
        /// <summary>
        /// SQL基类
        /// </summary>
        internal SQLBase _SQLBase;

        /// <summary>
        /// DBSelect查询条件
        /// </summary>
        /// <typeparam name="TSource">DBModel派生类</typeparam>
        /// <param name="func">查询字段</param>
        /// <returns></returns>
        public DBWhere Where<TSource>(Expression<Func<TSource, bool>> func) where TSource : DBModel
        {
            DBWhere model = Where<TSource>();
            model.GetWhereExpression<TSource>(func);

            return model;
        }
        /// <summary>
        /// DBSelect查询条件
        /// </summary>
        /// <typeparam name="TSource">DBModel派生类</typeparam>
        /// <param name="func">查询字段</param>
        /// <returns></returns>
        public DBWhere Where<TSource>() where TSource : DBModel
        {
            if (_SQLBase == null)
                throw new Exception("_SQLBase is null");
            if (_SQLBase.modelType != typeof(TSource))
                throw new Exception("the func Select() and Where() operation model type are not equal");

            DBWhere model = new DBWhere();
            model._DBSelect = this;
            model._DBSelect._SQLBase.where = string.Empty;

            return model;
        }
    }
}
