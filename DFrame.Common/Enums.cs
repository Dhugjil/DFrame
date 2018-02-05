namespace DFrame.Common
{
    /// <summary>
    /// 包含所有枚举
    /// </summary>
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
        /// <summary>
        /// 水印图片位置
        /// </summary>
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
        /// <summary>
        /// 请求属性类型
        /// https://www.cnblogs.com/ID890/p/http.html
        /// </summary>
        public enum HttpMethod
        {
            /// <summary>
            /// GET方法
            /// </summary>
            Get = 1,
            /// <summary>
            /// POST方法
            /// </summary>
            Post = 2,
            /// <summary>
            /// HEAD方法 只返回首部。不会反回实体的主体部分
            /// </summary>
            Head = 3,
            /// <summary>
            /// TRACE方法
            /// </summary>
            Trace = 4,
            /// <summary>
            /// OPTIONS方法
            /// </summary>
            Option = 5,
            /// <summary>
            /// DELETE方法
            /// </summary>
            Delete = 6,
            /// <summary>
            /// LOCK方法
            /// </summary>
            Lock = 7,
            /// <summary>
            /// MKCOL方法
            /// </summary>
            MKCOL = 8,
            /// <summary>
            /// MOVE方法
            /// </summary>
            Move = 9,
            /// <summary>
            /// PUT方法
            /// </summary>
            Put = 10
        }
        /// <summary>
        /// POST HttpRequest请求类型
        /// https://www.cnblogs.com/xiaozong/p/5732332.html
        /// </summary>
        public enum PostContentType
        {
            /// <summary>
            /// application/x-www-form-urlencoded
            /// </summary>
            Application = 1,
            /// <summary>
            /// multipart/form-data
            /// </summary>
            multipart = 2,
            /// <summary>
            /// application/json
            /// </summary>
            ApplicationJson = 3,
            /// <summary>
            /// text/xml
            /// </summary>
            TextXml = 4
        }
    }
}
