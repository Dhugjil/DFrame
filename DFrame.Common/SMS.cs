using System.Net;
using System.IO;
using System.Text;
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace DFrame.Common
{
    /// <summary>
    /// 短信接口 阿里云
    /// </summary>
    public class SMS
    {
        /// <summary>
        /// 实例化阿里云短信接口
        /// </summary>
        /// <param name="appcode">你自己的AppCode</param>
        /// <param name="method">请求方式,默认"GET"</param>
        /// <param name="host">接口地址,默认"http://sms.market.alicloudapi.com"</param>
        /// <param name="path">接口路径,默认"/singleSendSms"</param>
        public SMS(string appcode, string method = "GET", string host = "http://sms.market.alicloudapi.com", string path = "/singleSendSms")
        {
            this._appcode = appcode;
            this._path = path;
            this._host = host;
            this._method = method;
        }
        private string _host = "";
        private string _path = "";
        private string _method = "";
        private string _appcode = "";

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="ParamString">模板变量。例如：短信模板为：“短信验证码${no}”则传递 {"no":"123456"}</param>
        /// <param name="RecNum">目标手机号,多条记录可以英文逗号分隔</param>
        /// <param name="SignName">签名名称</param>
        /// <param name="TemplateCode">模板CODE</param>
        /// <returns>null:参数错误，{"success": true}:正常，{"message": "The specified templateCode is wrongly formed.","success": false}:错误</returns>
        public HttpWebResponse SendSMS(string ParamString, string RecNum, string TemplateCode, string SignName = null)
        {
            if (!string.IsNullOrEmpty(ParamString) && !string.IsNullOrEmpty(RecNum) && !string.IsNullOrEmpty(TemplateCode) && ParamString.Trim() != "" && RecNum.Trim() != "" && TemplateCode.Trim() != "")
            {
                string querys = "ParamString=" + HttpUtility.UrlEncode(ParamString.Trim(), Encoding.Default) + "&RecNum=" + RecNum.Trim() + "&TemplateCode=" + TemplateCode.Trim();
                if (!string.IsNullOrEmpty(SignName) && SignName.Trim() != "")
                    querys += "&SignName=" + SignName.Trim();
                string bodys = "";
                string url = _host + _path;
                HttpWebRequest httpRequest = null;
                HttpWebResponse httpResponse = null;

                if (0 < querys.Length)
                {
                    url = url + "?" + querys;
                }

                if (_host.Contains("https://"))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
                }
                else
                {
                    httpRequest = (HttpWebRequest)WebRequest.Create(url);
                }
                httpRequest.Method = _method;
                httpRequest.Headers.Add("Authorization", "APPCODE " + _appcode);
                if (0 < bodys.Length)
                {
                    byte[] data = Encoding.UTF8.GetBytes(bodys);
                    using (Stream stream = httpRequest.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                try
                {
                    httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                }
                catch (WebException ex)
                {
                    httpResponse = (HttpWebResponse)ex.Response;
                }
                return httpResponse;
            }
            else
                return null;
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }
}


