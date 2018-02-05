using System;

namespace Models
{
    /// <summary>
    /// 阿礼嘎礼 原字表
    /// </summary>
    public class Aligali_Atom : DFrame.Model.DBModel
    {
        /// <summary>
        /// 
        /// </summary>
        public long? Aligali_AtomID { get; set; }
        /// <summary>
        /// 短原字编码
        /// </summary>
        [DFrame.Model.DBField(NotNull = true)]
        public decimal? SKey { get; set; }
        /// <summary>
        /// 长原字编码
        /// </summary>
        [DFrame.Model.DBField(NotNull = true)]
        public decimal? LKey { get; set; }
        /// <summary>
        /// 型字
        /// </summary>
        [DFrame.Model.DBField(NotNull = true)]
        public string Text { get; set; }
        /// <summary>
        /// 契丹大字
        /// </summary>
        public string Other_Text { get; set; }
        /// <summary>
        /// 输入码
        /// </summary>
        [DFrame.Model.DBField(NotNull = true)]
        public string InputCode { get; set; }
        /// <summary>
        /// 拉丁
        /// </summary>
        [DFrame.Model.DBField(NotNull = true)]
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
