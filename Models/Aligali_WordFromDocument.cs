using System;

namespace Models
{
    /// <summary>
    /// 阿礼噶礼 词和文献关系
    /// </summary>
    public class Aligali_WordFromDocument : DFrame.Model.DBModel
    {
        public long? Aligali_WordFromDocumentID { get; set; }
        /// <summary>
        /// 文献ID
        /// </summary>
        public int? DocumentID { get; set; }
        /// <summary>
        /// 小字
        /// </summary>
        [DFrame.Model.DBField(ForeignKey = typeof(AligaliWord))]
        public long? AligaliWordID { get; set; }
        /// <summary>
        /// 编号 由元字编码组成 中间-号隔开
        /// </summary>
        public string SKeys { get; set; }
        /// <summary>
        /// 编号 由元字编码组成 中间-号隔开
        /// </summary>
        public string LKeys { get; set; }
        /// <summary>
        /// 输入码
        /// </summary>
        public string InputCode { get; set; }
        /// <summary>
        /// 拉丁
        /// </summary>
        public string Latin { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int? RowNo { get; set; }
        /// <summary>
        /// 第几个词
        /// </summary>
        public int? WordNo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        public override object ToObject()
        {
            throw new NotImplementedException();
        }
    }
}
