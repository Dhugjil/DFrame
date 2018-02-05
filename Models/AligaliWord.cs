using System;

namespace Models
{
    /// <summary>
    /// 阿里嘎里
    /// </summary>
    public class AligaliWord : DFrame.Model.DBModel
    {
        public int? AligaliWordID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DFrame.Model.DBField(NotNull = true, DefaultValue = "")]
        public string Text { get; set; }
        /// <summary>
        /// 编号 由元字编码组成 中间-号隔开
        /// </summary>
        [DFrame.Model.DBField(NotNull = true, DefaultValue = "")]
        public string SKeys { get; set; }
        /// <summary>
        /// 编号 由元字编码组成 中间-号隔开
        /// </summary>
        [DFrame.Model.DBField(NotNull = true, DefaultValue = "")]
        public string LKeys { get; set; }
        /// <summary>
        /// 输入码
        /// </summary>
        [DFrame.Model.DBField(NotNull = true, DefaultValue = "")]
        public string InputCode { get; set; }
        /// <summary>
        /// 拉丁
        /// </summary>
        [DFrame.Model.DBField(NotNull = true, DefaultValue = "")]
        public string Latin { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DFrame.Model.DBField(NotNull = true)]
        public DateTime? CreateTime { get; set; }

        public override object ToObject()
        {
            throw new NotImplementedException();
        }
    }
}
