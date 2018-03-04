using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DFrame.Common.EncryptDecrypt
{
    /// <summary>
    /// RSA 公钥私钥加密解密
    /// </summary>
    public class RSA
    {
        /// <summary>
        /// 公钥加密
        /// </summary>
        /// <param name="publickey">公钥</param>
        /// <param name="content">加密内容</param>
        /// <returns>加密内容</returns>
        public static string RSAEncrypt(string publickey, string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);
            return Convert.ToBase64String(cipherbytes);
        }
        /// <summary>
        /// 私钥解密
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="content">解密内容</param>
        /// <returns>解密内容</returns>
        public static string RSADecrypt(string privateKey, string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(privateKey);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);
            return Encoding.UTF8.GetString(cipherbytes);
        }
        /// <summary>
        /// 生成公钥私钥
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="privateKey">私钥</param>
        public static void CreateKey(ref string publicKey, ref string privateKey)
        {
            RSACryptoServiceProvider oRSA = new RSACryptoServiceProvider();
            privateKey = oRSA.ToXmlString(true);
            publicKey = oRSA.ToXmlString(false);
        }
        /// <summary>
        /// 生成私钥签名
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="content">内容</param>
        /// <param name="halg">哈希算法名称 MD5 SHA1 MurmurHash quickHash</param>
        /// <returns>私钥签名</returns>
        public static string CreateSignData(string privateKey, string content, string halg = "SHA1")
        {
            byte[] contentBytes = Encoding.UTF8.GetBytes(content);
            RSACryptoServiceProvider oRSA3 = new RSACryptoServiceProvider();
            oRSA3.FromXmlString(privateKey);
            return Encoding.UTF8.GetString(oRSA3.SignData(contentBytes, halg));
        }
        /// <summary>
        /// 公钥验证私钥
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="content">私钥加密内容</param>
        /// <param name="verifyStr">验证字符</param>
        /// <param name="halg">哈希算法名称 MD5 SHA1 MurmurHash quickHash</param>
        /// <returns>验证状态</returns>
        public static bool VerifySingData(string publicKey, string content, string verifyStr, string halg = "SHA1")
        {
            byte[] contentBytes = Encoding.UTF8.GetBytes(content);
            byte[] verifyStrBytes = Encoding.UTF8.GetBytes(verifyStr);
            RSACryptoServiceProvider oRSA4 = new RSACryptoServiceProvider();
            oRSA4.FromXmlString(publicKey);
            return oRSA4.VerifyData(verifyStrBytes, halg, contentBytes);
        }
        //哈希算法是指将任意长度的二进制值映射为固定长度的较小二进制值，这个小的二进制值称为哈希值。

        //
        //A有自己的公钥和私钥：        A_public_key, A_private_key
        //B也有自己的公钥和私钥：      B_public_key, B_private_key
        //A和B互换公钥
        //加密：公钥加密，私钥解密
        //认证：私钥加密，公钥解密
    }
}
