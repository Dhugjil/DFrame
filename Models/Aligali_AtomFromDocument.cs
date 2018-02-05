using System;

namespace Models
{
    /// <summary>
    /// 阿礼嘎礼 原字-文献关系表
    /// </summary>
    public class Aligali_AtomFromDocument : DFrame.Model.DBModel
    {
        /// <summary>
        /// 
        /// </summary>
        public long? Aligali_AtomFromDocumentID { get; set; }
        /// <summary>
        /// 文献ID
        /// </summary>
        public int? DocumentID { get; set; }
        /// <summary>
        /// 原字编码
        /// </summary>
        [DFrame.Model.DBField(ForeignKey = typeof(Aligali_Atom), NotNull = true)]
        public long? Aligali_AtomID { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int? RowNo { get; set; }
        /// <summary>
        /// 第几个词
        /// </summary>
        public int? WordNo { get; set; }
        /// <summary>
        /// 第几个原字
        /// </summary>
        public int? AtomNo { get; set; }
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
