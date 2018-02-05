using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DFrame.Common.EncryptDecrypt
{
    /// <summary>
    /// Hmac 加密
    /// </summary>
    public class HmacSha
    {
        /// <summary>
        /// HmacSha1 加密
        /// </summary>
        /// <param name="value">加密字符串</param>
        /// <param name="keyVal">加密秘钥</param>
        /// <returns></returns>
        public static string HmacSha1(string value, string keyVal)
        {
            if (value == null)
            {
                throw new ArgumentNullException("未将对象引用设置到对象的实例。");
            }
            var encoding = Encoding.UTF8;
            byte[] keyStr = encoding.GetBytes(keyVal);
            HMACSHA1 hmacSha1 = new HMACSHA1(keyStr);
            return HashAlgorithmBase(hmacSha1, value, encoding);
        }
        /// <summary>
        /// HmacSha256 加密
        /// </summary>
        /// <param name="value">加密字符串</param>
        /// <param name="keyVal">加密秘钥</param>
        /// <returns></returns>
        public static string HmacSha256(string value, string keyVal)
        {
            if (value == null)
            {
                throw new ArgumentNullException("未将对象引用设置到对象的实例。");
            }
            var encoding = Encoding.UTF8;
            byte[] keyStr = encoding.GetBytes(keyVal);
            HMACSHA256 hmacSha256 = new HMACSHA256(keyStr);
            return HashAlgorithmBase(hmacSha256, value, encoding);
        }
        /// <summary>
        /// HmacSha256 加密
        /// </summary>
        /// <param name="value">加密字符串</param>
        /// <param name="keyVal">加密秘钥</param>
        /// <returns></returns>
        public static string HmacSha384(string value, string keyVal)
        {
            if (value == null)
            {
                throw new ArgumentNullException("未将对象引用设置到对象的实例。");
            }
            var encoding = Encoding.UTF8;
            byte[] keyStr = encoding.GetBytes(keyVal);
            HMACSHA384 hmacSha384 = new HMACSHA384(keyStr);
            return HashAlgorithmBase(hmacSha384, value, encoding);
        }
        /// <summary>
        /// HmacSha512 加密
        /// </summary>
        /// <param name="value">加密字符串</param>
        /// <param name="keyVal">加密秘钥</param>
        /// <returns></returns>
        public static string HmacSha512(string value, string keyVal)
        {
            if (value == null)
            {
                throw new ArgumentNullException("未将对象引用设置到对象的实例。");
            }
            var encoding = Encoding.UTF8;
            byte[] keyStr = encoding.GetBytes(keyVal);
            HMACSHA512 hmacSha512 = new HMACSHA512(keyStr);
            return HashAlgorithmBase(hmacSha512, value, encoding);
        }
        /// <summary>
        /// HmacMd5 加密
        /// </summary>
        /// <param name="value">加密字符串</param>
        /// <param name="keyVal">加密秘钥</param>
        /// <returns></returns>
        public static string HmacMd5(string value, string keyVal)
        {
            if (value == null)
            {
                throw new ArgumentNullException("未将对象引用设置到对象的实例。");
            }
            var encoding = Encoding.UTF8;
            byte[] keyStr = encoding.GetBytes(keyVal);
            HMACMD5 hmacMd5 = new HMACMD5(keyStr);
            return HashAlgorithmBase(hmacMd5, value, encoding);
        }
        /// <summary>
        /// HmacRipeMd160 加密
        /// </summary>
        public static string HmacRipeMd160(string value, string keyVal)
        {
            if (value == null)
            {
                throw new ArgumentNullException("未将对象引用设置到对象的实例。");
            }
            var encoding = Encoding.UTF8;
            byte[] keyStr = encoding.GetBytes(keyVal);
            HMACRIPEMD160 hmacRipeMd160 = new HMACRIPEMD160(keyStr);
            return HashAlgorithmBase(hmacRipeMd160, value, encoding);
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
