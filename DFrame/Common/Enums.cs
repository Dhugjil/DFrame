namespace DFrame.Common
{
    public class Enums
    {
        /// <summary>
        /// 生成缩略图的方式
        /// </summary>
        public enum ImageThumbnailMode
        {
            /// <summary>
            /// 指定高宽缩放（可能变形）
            /// </summary>
            HW,
            /// <summary>
            /// 指定宽，高按比例
            /// </summary>
            W,
            /// <summary>
            /// 指定高，宽按比例
            /// </summary>
            H,
            /// <summary>
            /// 指定高宽裁减（不变形）
            /// </summary>
            Cut
        }
        public enum ImageWatermarkLocation
        {
            /// <summary>
            /// 左上
            /// </summary>
            LT,
            /// <summary>
            /// 上
            /// </summary>
            T,
            /// <summary>
            /// 右上
            /// </summary>
            RT,
            /// <summary>
            /// 左中
            /// </summary>
            LC,
            /// <summary>
            /// 中
            /// </summary>
            C,
            /// <summary>
            /// 右中
            /// </summary>
            RC,
            /// <summary>
            /// 左下
            /// </summary>
            LB,
            /// <summary>
            /// 下
            /// </summary>
            B,
            /// <summary>
            /// 左下
            /// </summary>
            RB
        }
    }
}
