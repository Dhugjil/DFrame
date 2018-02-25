using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DFrame.Model
{
    /// <summary>
    /// 实体条件类 提供执行方法
    /// </summary>
    public class DBWhere
    {
        /// <summary>
        /// 查询类
        /// </summary>
        internal DBSelect _DBSelect;

        /// <summary>
        /// 符合条件数量
        /// </summary>
        /// <returns></returns>
        public long Count()
        {
            if (_DBSelect == null)
                throw new Exception("_DBSelect is null");
            if (_DBSelect._SQLBase == null)
                throw new Exception("_DBSelect._SQLBase is null");
            return _DBSelect.GetCount();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <typeparam name="TResult">获取集合实体类型</typeparam>
        /// <returns></returns>
        public List<TResult> ToList<TResult>() where TResult : class, new()
        {
            if (_DBSelect == null)
                throw new Exception("_DBSelect is null");
            if (_DBSelect._SQLBase == null)
                throw new Exception("_DBSelect._SQLBase is null");

            return _DBSelect.GetList<TResult>();
        }
        /// <summary>
        /// 删除条件集合
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            if (_DBSelect == null)
                throw new Exception("_DBSelect is null");
            if (_DBSelect._SQLBase == null)
                throw new Exception("_DBSelect._SQLBase is null");
            return _DBSelect.Delete();
        }
        /// <summary>
        /// 升序排序
        /// </summary>
        /// <typeparam name="TSource">数据实体类型</typeparam> 
        /// <param name="func">lambda表达式</param>
        /// <returns></returns>
        public DBWhere OrderBy<TSource>(Expression<Func<TSource, object>> func) where TSource : DBModel
        {
            if (_DBSelect == null)
                throw new Exception("_DBSelect is null");
            if (_DBSelect._SQLBase == null)
                throw new Exception("_SQLBase is null");
            if (_DBSelect._SQLBase.modelType != typeof(TSource))
                throw new Exception("the func Select() and Where() operation model type are not equal");

            this.GetOrderBy(func);

            return this;
        }
        /// <summary>
        /// 降序排序
        /// </summary>
        /// <typeparam name="TSource">数据实体类型</typeparam> 
        /// <param name="func">lambda表达式</param>
        /// <returns></returns>
        public DBWhere OrderByDescending<TSource>(Expression<Func<TSource, object>> func) where TSource : DBModel
        {
            if (_DBSelect == null)
                throw new Exception("_DBSelect is null");
            if (_DBSelect._SQLBase == null)
                throw new Exception("_SQLBase is null");
            if (_DBSelect._SQLBase.modelType != typeof(TSource))
                throw new Exception("the func Select() and Where() operation model type are not equal");

            this.GetOrderByDescending(func);

            return this;
        }
    }
}
