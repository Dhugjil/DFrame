using DFrame.Common;
using System;
using System.IO;
using System.Text;

namespace DFrame.Common
{
    /// <summary>
    /// 系统错误信息处理
    /// </summary>
    public class TextError
    {
        /// <summary>
        /// 记录系统错误/存入根目录下APP_Data/Error文件夹下
        /// </summary>
        /// <param name="msg">错误信息</param>
        public static void SaveError(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fffffff") + "--------------------\r\n");
            try
            {
                sb.Append("loginName:" + UserInfo.UserName + "\r\n");
                sb.Append("IP:" + UserInfo.IP + "\r\n");
            }
            catch
            {
                sb.Append("loginName:....\r\n");
                sb.Append("IP:....\r\n");
            }
            sb.Append(msg + "\r\n");
            sb.Append("+++++++++++++++++++++++++++++++++++++++++++++++++++\r\n");

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/Error");
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            string fileName = filePath + "/" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            File.AppendAllText(fileName, sb.ToString(), Encoding.UTF8);
            sb.Clear();
        }
    }
}
