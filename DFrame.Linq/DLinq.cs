using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace DFrame.Linq
{
    public static class DLinq
    {
        /// <summary>
        /// 条件查询实体
        /// </summary>
        /// <returns></returns>
        public static void Select<TSource>(this TSource model, Func<TSource, bool> predicate)
        {
            
            Expression firstArg = Expression.Constant(2);
            //return res;
            Debug.WriteLine(predicate.ToString());
            //return list;
        }
    }
}
