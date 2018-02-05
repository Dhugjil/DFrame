using System.Collections.Generic;

namespace DFrame.Common
{
    /// <summary>
    /// 按实体字段去重
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class ListComparer<T> : IEqualityComparer<T>
    {
        /// <summary>
        /// 定义代理
        /// </summary>
        /// <typeparam name="F">实体类型</typeparam>
        /// <param name="x">实体</param>
        /// <param name="y">实体</param>
        /// <returns></returns>
        public delegate bool EqualsConparer<F>(F x, F y);
        /// <summary>
        /// 存储代理
        /// </summary>
        public EqualsConparer<T> _equalsConparer;
        /// <summary>
        /// 去重字段 (p1,p2)=>(p1.Feild == p2.Feild)
        /// </summary>
        /// <param name="equalsConparer"></param>
        public ListComparer(EqualsConparer<T> equalsConparer)
        {
            this._equalsConparer = equalsConparer;
        }
        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(T x, T y)
        {
            if (this._equalsConparer != null)
            {
                return _equalsConparer(x, y);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获取哈希值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(T obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
