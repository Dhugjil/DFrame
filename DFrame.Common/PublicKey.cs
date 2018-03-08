using System;

namespace DFrame.Common
{
    /// <summary>
    /// 生成公钥类
    /// </summary>
    public class CreateKey
    {
        /// <summary>
        /// 创建公钥2分钟有效
        /// </summary>
        /// <returns>0当前公钥 1前一分钟公钥</returns>
        public static string[] PublicKey()
        {
            string[] public_key = new string[2];
            throw new Exception();
            string yan = "yusang_hugjil@163.com";
            public_key[0] = EncryptDecrypt.MD5.GetAbstractToMD5(EncryptDecrypt.MD5.GetAbstractToMD5(DateTime.Now.ToString("yyyy-MM-dd HH:mm") + yan) + yan);
            public_key[1] = EncryptDecrypt.MD5.GetAbstractToMD5(EncryptDecrypt.MD5.GetAbstractToMD5(DateTime.Now.AddMinutes(-1).ToString("yyyy-MM-dd HH:mm") + yan) + yan);
            return public_key;
        }
        /// <summary>
        /// 生成Guid字符串
        /// 
        /// null// 9af7f46a-ea52-4aa3-b8c3-9fd484c2af12
        /// N// e0a953c3ee6040eaa9fae2b667060e09
        /// D// 9af7f46a-ea52-4aa3-b8c3-9fd484c2af12
        /// B// {734fd453-a4f8-4c5d-9c98-3fe2d7079760}
        /// P//  (ade24d16-db0f-40af-8794-1e08e2040df3)
        /// X// {0x3fa412e3,0x8356,0x428f,{0xaa,0x34,0xb7,0x40,0xda,0xaf,0x45,0x6f}}
        /// </summary>
        /// <param name="type">字符串格式</param>
        /// <returns></returns>
        public static string GuidKey(string type = "")
        {
            var id = "";
            switch (type)
            {
                case "N":
                    id = Guid.NewGuid().ToString(type);
                    break;
                case "D":
                    id = Guid.NewGuid().ToString(type);
                    break;
                case "B":
                    id = Guid.NewGuid().ToString(type);
                    break;
                case "P":
                    id = Guid.NewGuid().ToString(type);
                    break;
                case "X":
                    id = Guid.NewGuid().ToString(type);
                    break;
                default:
                    id = Guid.NewGuid().ToString();
                    break;
            }
            return id;
        }
    }
}
