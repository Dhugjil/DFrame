namespace DFrame.Model
{
    /// <summary>
    /// 生成数据表必须继承这个
    /// </summary>
    public abstract class DBModel
    {
        /// <summary>
        /// 获取指定类型实体
        /// </summary>
        /// <returns></returns>
        public abstract object ToObject();
    }
}
