using System.Web;

namespace DFrame.Common
{
    public class UserInfo
    {
        /// <summary>
        /// 用户名 HttpContext.Current.User.Identity.Name
        /// </summary>
        public static string UserName
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }
        /// <summary>
        /// 用户IP HttpContext.Current.Request.UserHostAddress
        /// </summary>
        public static string IP
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }
        /// <summary>
        /// URI绝对路径 HttpContext.Current.Request.UserHostAddress
        /// </summary>
        public static string AbsolutePath
        {
            get
            {
                return HttpContext.Current.Request.Url.AbsolutePath;
            }
        }
    }
}
