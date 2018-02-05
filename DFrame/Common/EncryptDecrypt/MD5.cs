using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DFrame.Common.EncryptDecrypt
{
    /// <summary>
    /// md5操作
    /// </summary>
    public class MD5
    {
        /// <summary>
        /// 获取字符串的md5摘要
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>MD5摘要</returns>
        public static string GetAbstractToMD5(string str)
        {
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            return Analysis(result);
        }
        /// <summary>
        /// 获取文件的md5摘要 加盐
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="salt">盐</param>
        /// <returns>MD5摘要</returns>
        public static string GetAbstractToMD5(string str, string salt)
        {
            return salt == null ? GetAbstractToMD5(str) : GetAbstractToMD5(str + "《『" + salt + "』》");
        }
        /// <summary>
        /// 获取文件的md5摘要
        /// </summary>
        /// <param name="sFile">文件流</param>
        /// <returns>MD5摘要结果</returns>
        public static string GetAbstractToMD5(Stream sFile)
        {
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(sFile);
            return Analysis(result);
        }
        /// <summary>
        /// 获取文件的md5摘要
        /// </summary>
        /// <param name="dataFile">文件流</param>
        /// <returns>MD5摘要结果</returns>
        public static string GetAbstractToMD5(byte[] dataFile)
        {
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(dataFile);
            return Analysis(result);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private static string Analysis(byte[] b)
        {
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < b.Length; i++)
            {
                sb.Append(b[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
    }
}
