using System;

namespace Models
{
    /// <summary>
    /// 阿礼嘎礼 原字表
    /// </summary>
    public class Person : DFrame.Model.DBModel
    {
        /// <summary>
        /// 
        /// </summary>
        public long? PersonID { get; set; }
        /// <summary>
        /// 型字
        /// </summary>
        [DFrame.Model.DBField(NotNull = true,DBFieldKey =DFrame.Model.Enums.DBFieldKey.Unique,DefaultValue ="默认",ForeignKey =typeof(Person),Lenght =4001)]
        public string Text { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DFrame.Model.DBField(NotNull = true)]
        public DateTime? CreateTime { get; set; }

        public int? Age { get; set; }

        public bool? State { get; set; }

        public override object ToObject()
        {
            throw new NotImplementedException();
        }
    }
}
