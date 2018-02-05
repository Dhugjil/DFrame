using System;

namespace DFrame.Model
{
    /// <summary>
    /// 数据库生成用属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class DBFieldAttribute : Attribute
    {
        /// <summary>
        /// 外键
        /// </summary>
        public Type ForeignKey { get; set; } = null;

        /// <summary>
        /// 数据库字段类型 null时系统根据属性类型设置
        /// </summary>
        public string DBFieldType { get; set; } = null;
        /// <summary>
        /// 长度 4001最长 默认4001
        /// </summary>
        public int Lenght { get; set; } = 255;
        /// <summary>
        /// 键类型
        /// </summary>
        public Enums.DBFieldKey DBFieldKey { get; set; } = Enums.DBFieldKey.None;
        /// <summary>
        /// 默认值
        /// </summary>
        public object DefaultValue { get; set; } = null;
        /// <summary>
        /// 是否可空 默认false
        /// </summary>
        public bool NotNull { get; set; } = false;
    }
}
