using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DFrame.Common
{
    /// <summary>
    /// http服务类
    /// </summary>
    public class HttpService
    {
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }
        /// <summary>
        /// Post访问
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="content">请求内容</param>
        /// <param name="contentType">请求格式</param>
        /// <param name="timeout">超时值（秒）</param>
        /// <returns></returns>
        public static string Post(string url, string content = "", Enums.PostContentType contentType = Enums.PostContentType.Application, int timeout = 1)
        {
            GC.Collect();
            string result = "";

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;

            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = timeout * 1000;
                request.Method = "POST";
                switch (contentType)
                {
                    case Enums.PostContentType.Application:
                        request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                        break;
                    case Enums.PostContentType.ApplicationJson:
                        request.ContentType = "application/json";
                        break;
                    case Enums.PostContentType.multipart:
                        request.ContentType = "multipart/form-data";
                        break;
                    case Enums.PostContentType.TextXml:
                        request.ContentType = "text/xml";
                        break;
                    default:
                        throw new Exception("ContentType 类型不正确");
                }
                byte[] data = Encoding.UTF8.GetBytes(content);
                request.ContentLength = data.Length;
                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //务器返回
                response = (HttpWebResponse)request.GetResponse();

                //获取HTTP返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                System.Threading.Thread.ResetAbort();
                throw new Exception("Exception message:" + e.Message);
            }
            catch (WebException e)
            {
                throw new Exception("WebException message:" + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("HttpService message:" + e.Message);
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
                if (reqStream != null)
                {
                    reqStream = null;
                }
            }
            return result;
        }
        /// <summary>
        /// Post访问
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="cookies">模拟cookies</param>
        /// <param name="content">请求内容</param>
        /// <param name="contentType">请求格式</param>
        /// <param name="timeout">超时值（秒）</param>
        /// <returns></returns>
        public static HttpWebResponse PostCookie(string url, List<Cookie> cookies, string content = "", Enums.PostContentType contentType = Enums.PostContentType.Application, int timeout = 1)
        {
            GC.Collect();

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;

            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = timeout * 1000;
                request.Method = "POST";
                foreach (Cookie item in cookies)
                {
                    request.CookieContainer.Add(item);
                }
                switch (contentType)
                {
                    case Enums.PostContentType.Application:
                        request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                        break;
                    case Enums.PostContentType.ApplicationJson:
                        request.ContentType = "application/json";
                        break;
                    case Enums.PostContentType.multipart:
                        request.ContentType = "multipart/form-data";
                        break;
                    case Enums.PostContentType.TextXml:
                        request.ContentType = "text/xml";
                        break;
                    default:
                        throw new Exception("ContentType 类型不正确");
                }
                byte[] data = Encoding.UTF8.GetBytes(content);
                request.ContentLength = data.Length;
                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //务器返回
                return (HttpWebResponse)request.GetResponse();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                System.Threading.Thread.ResetAbort();
                throw new Exception("Exception message:" + e.Message);
            }
            catch (WebException e)
            {
                throw new Exception("WebException message:" + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("HttpService message:" + e.Message);
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
                if (reqStream != null)
                {
                    reqStream = null;
                }
            }
        }
        /// <summary>
        /// Get访问
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="timeout">超时值（秒）</param>
        /// <returns></returns>
        public static string Get(string url, int timeout = 1)
        {
            GC.Collect();
            string result = "";

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = timeout * 1000;
                request.Method = "GET";

                //务器返回
                response = (HttpWebResponse)request.GetResponse();

                //获取HTTP返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                System.Threading.Thread.ResetAbort();
                throw new Exception("Exception message:" + e.Message);
            }
            catch (WebException e)
            {
                throw new Exception("WebException message:" + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("HttpService message:" + e.Message);
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }
        /// <summary>
        /// Get访问
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="cookies">模拟cookie集合</param>
        /// <param name="timeout">超时值（秒）</param>
        /// <returns></returns>
        public static HttpWebResponse GetCookie(string url, List<Cookie> cookies, int timeout = 1)
        {
            GC.Collect();

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = timeout * 1000;
                request.Method = "GET";
                foreach (Cookie item in cookies)
                {
                    request.CookieContainer.Add(item);
                }

                //务器返回
                return (HttpWebResponse)request.GetResponse();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                System.Threading.Thread.ResetAbort();
                throw new Exception("Exception message:" + e.Message);
            }
            catch (WebException e)
            {
                throw new Exception("WebException message:" + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("HttpService message:" + e.Message);
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
        }
    }
}
