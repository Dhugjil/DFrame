using System;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace DFrame.Common
{
    /// <summary>
    /// json操作
    /// </summary>
    public class Json
    {
        /// <summary>
        /// 从一个Json串生成对象信息
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <returns>对象</returns>
        public static T JsonToObject<T>(string jsonString) where T : class
        {
            Regex r = new Regex("/date\\((?<date>\\d*)\\)/", RegexOptions.IgnoreCase);
            while (r.IsMatch(jsonString))
            {
                DateTime dt = new DateTime(1970, 1, 1);
                Match match = r.Match(jsonString);
                dt = dt.AddMilliseconds(Convert.ToInt64(match.Groups["date"].Value));
                jsonString = jsonString.Replace(match.Value, dt.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(jsonString);
        }
        /// <summary>
        /// 从一个对象信息生成Json串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>json字符串</returns>
        public static string ObjectToJson(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }
    }
}
