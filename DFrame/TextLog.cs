﻿using DFrame.Common;
using System;
using System.IO;
using System.Text;

namespace DFrame
{
    /// <summary>
    /// 系统日志处理
    /// </summary>
    public class TextLog 
    {
        /// <summary>
        /// 记录系统错误/存入根目录下APP_Data/Error文件夹下
        /// </summary>
        /// <param name="msg">错误信息</param>
        public static void SaveLog(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fffffff") + "--------------------\r\n");
            sb.Append("loginName:" + UserInfo.UserName + "\r\n");
            sb.Append("IP:" + UserInfo.IP + "\r\n");
            sb.Append(msg + "\r\n");
            sb.Append("+++++++++++++++++++++++++++++++++++++++++++++++++++");

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/Log");
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            string fileName = filePath + "/" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            File.AppendAllText(fileName, sb.ToString(), Encoding.UTF8);
            sb.Clear();
        }
    }
}
