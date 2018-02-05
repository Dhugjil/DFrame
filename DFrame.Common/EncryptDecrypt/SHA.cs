using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DFrame.Common.EncryptDecrypt
{
    /// <summary>
    /// SHA加密
    /// </summary>
    public class SHA
    {
        /// <summary>
        /// SHA1 加密
        /// </summary>
        /// <param name="value">加密字符串</param>
        /// <returns></returns>
        public static string Sha1(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("未将对象引用设置到对象的实例。");
            }
            var encoding = Encoding.UTF8;
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            return HashAlgorithmBase(sha1, value, encoding);
        }
        /// <summary>
        /// Sha256 加密
        /// </summary>
        /// <param name="value">加密字符串</param>
        /// <returns></returns>
        public static string Sha256(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("未将对象引用设置到对象的实例。");
            }
            var encoding = Encoding.UTF8;
            SHA256 sha256 = new SHA256Managed();
            return HashAlgorithmBase(sha256, value, encoding);
        }
        /// <summary>
        /// Sha512 加密
        /// </summary>
        /// <param name="value">加密字符串</param>
        /// <returns></returns>
        public static string Sha512(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("未将对象引用设置到对象的实例。");
            }
            var encoding = Encoding.UTF8;
            SHA512 sha512 = new SHA512Managed();
            return HashAlgorithmBase(sha512, value, encoding);
        }
        /// <summary>
        /// HashAlgorithm 加密统一方法
        /// </summary>
        private static string HashAlgorithmBase(HashAlgorithm hashAlgorithmObj, string source, Encoding encoding)
        {
            byte[] btStr = encoding.GetBytes(source);
            byte[] hashStr = hashAlgorithmObj.ComputeHash(btStr);
            return Bytes2Str(hashStr);
        }
        /// <summary>
        /// 转换成字符串
        /// </summary>
        private static string Bytes2Str(IEnumerable<byte> source, string formatStr = "{0:X2}")
        {
            StringBuilder pwd = new StringBuilder();
            foreach (byte btStr in source) { pwd.AppendFormat(formatStr, btStr); }
            return pwd.ToString();
        }
    }
}
