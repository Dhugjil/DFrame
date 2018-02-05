using System.Drawing;
using System.Text;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace DFrame.Common
{
    /// <summary>
    /// 生成二维码和解读二维码
    /// </summary>
    public class QRCode
    {
        /// <summary>
        /// 生成QRCode
        /// </summary>
        /// <param name="data">生成字符串</param>
        /// <param name="data_encoding">字符串编码格式</param>
        /// <param name="encode_mode">编码模式</param>
        /// <param name="error_correction">容错级别L:7% M:15% Q:25% H:30%（微信短连接建议L）</param>
        /// <param name="version">版本1-40（微信短链接建议4）</param>
        /// <param name="scale">大小尺寸（微信短链接建议4）</param>
        /// <returns>二维码image</returns>
        public Image Encoder(string data, Encoding data_encoding = null, QRCodeEncoder.ENCODE_MODE encode_mode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC, QRCodeEncoder.ERROR_CORRECTION error_correction = QRCodeEncoder.ERROR_CORRECTION.L, int version = 1, int scale = 4)
        {
            if (version > 40 || version < 1)
                throw new System.Exception("QRCodeVersion请选择1-40区间的数字");

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = encode_mode;
            qrCodeEncoder.QRCodeErrorCorrect = error_correction;
            qrCodeEncoder.QRCodeScale = scale;
            qrCodeEncoder.QRCodeVersion = version;

            //文字生成图片
            Image image;

            AGAIN:
            try
            {
                if (data_encoding == null)
                    image = qrCodeEncoder.Encode(data);
                else
                    image = qrCodeEncoder.Encode(data, data_encoding);
            }
            catch
            {
                if (qrCodeEncoder.QRCodeVersion == 40)
                    throw new System.Exception("文本内容太大,生成二维码失败！");
                qrCodeEncoder.QRCodeVersion++;
                goto AGAIN;
            }
            return image;
        }
        /// <summary>
        /// QRCode解码
        /// </summary>
        /// <param name="image">图片信息</param>
        /// <returns>解码信息</returns>
        public string Decoder(Image image)
        {
            Bitmap myBitmap = new Bitmap(image);
            QRCodeDecoder decoder = new QRCodeDecoder();
            string decodedString = decoder.decode(new QRCodeBitmapImage(myBitmap));
            return decodedString;
        }
    }
}
