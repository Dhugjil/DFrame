namespace DFrame.Model
{
    /// <summary>
    /// DFrame.Model所用的所有枚举
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// 数据库字段的用途。
        /// </summary>
        public enum DBFieldKey
        {
            /// <summary>
            /// 未定义。
            /// </summary>
            None = 0x00,
            ///// <summary>
            ///// 用于主键。
            ///// </summary>
            //PrimaryKey = 0x01,
            /// <summary>
            /// 用于唯一键。
            /// </summary>
            Unique = 0x02,
            ///// <summary>
            ///// 由系统控制该字段的值。
            ///// </summary>
            //BySystem = 0x04
        }
    }
}
