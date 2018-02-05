using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace DFrame.Common
{
    /// <summary>  
    /// 电子邮件操作类。  
    /// </summary>  
    public class Mail
    {
        /// <summary>  
        /// 初始化一个电子邮件操作类的空实例。  
        /// </summary>  
        public Mail()
        {
        }
        /// <summary>  
        /// 初始化一个电子邮件操作类的实例。  
        /// </summary>  
        /// <param name="from">发件人的电子邮件地址。</param>  
        /// <param name="fromName">发件人的姓名。</param>  
        /// <param name="recipient">{"Name","Email"}。</param>
        /// <param name="subject">电子邮件的主题。</param>  
        /// <param name="body">电子邮件的内容。</param>  
        /// <param name="host">电子邮件的服务器地址。</param>  
        /// <param name="port">电子邮件的主机端口号。</param>  
        /// <param name="username">登录电子邮件服务器的用户名。</param>  
        /// <param name="password">登录电子邮件服务器的用户密码。</param>  
        /// <param name="isBodyHtml">邮件的正文是否是HTML格式。</param>  
        /// <param name="filepath">邮件附件的文件路径。</param>  
        public Mail(string from, string fromName, Dictionary<string, string> recipient, string subject, string body, string host, int port, string username, string password, bool isBodyHtml, string filepath)
        {
            foreach (var item in recipient)
            {
                if (item.Value != null && item.Value.Trim() != "")
                    this._recipientDictionary.Add(item.Key, item.Value.Trim());
            }
            this._from = from;
            this._fromName = fromName;
            this._subject = subject;
            this._body = body;
            this._serverHost = host;
            this._serverPort = port;
            this._username = username;
            this._password = password;
            this._isBodyHtml = isBodyHtml;
            this._attachment.Add(filepath);
        }
        /// <summary>  
        /// 初始化一个电子邮件操作类的实例。  
        /// </summary>  
        /// <param name="from">发件人的电子邮件地址。</param>  
        /// <param name="fromName">发件人的姓名。</param>  
        /// <param name="recipient">收件人的电子邮件地址。</param>  
        /// <param name="recipientName">收件人的姓名。</param>  
        /// <param name="subject">电子邮件的主题。</param>  
        /// <param name="body">电子邮件的内容。</param>  
        /// <param name="host">电子邮件的服务器地址。</param>  
        /// <param name="port">电子邮件的主机端口号。</param>  
        /// <param name="username">登录电子邮件服务器的用户名。</param>  
        /// <param name="password">登录电子邮件服务器的用户密码。</param>  
        /// <param name="isBodyHtml">邮件的正文是否是HTML格式。</param>  
        /// <param name="filepath">邮件附件的文件路径。</param>  
        public Mail(string from, string fromName, string recipient, string recipientName, string subject, string body, string host, int port, string username, string password, bool isBodyHtml, string filepath)
        {
            this._from = from;
            this._fromName = fromName;
            if (recipient != null && recipient.Trim() != "")
                this._recipientDictionary.Add(recipientName, recipient.Trim());
            this._subject = subject;
            this._body = body;
            this._serverHost = host;
            this._serverPort = port;
            this._username = username;
            this._password = password;
            this._isBodyHtml = isBodyHtml;
            this._attachment.Add(filepath);
        }
        /// <summary>  
        /// 初始化一个电子邮件操作类的实例。  
        /// </summary>  
        /// <param name="from">发件人的电子邮件地址。</param>  
        /// <param name="fromName">{"Name","Email"}。</param>  
        /// <param name="recipient">收件人的电子邮件地址。</param>  
        /// <param name="subject">电子邮件的主题。</param>  
        /// <param name="body">电子邮件的内容。</param>  
        /// <param name="host">电子邮件的服务器地址。</param>  
        /// <param name="port">电子邮件的主机端口号。</param>  
        /// <param name="username">登录电子邮件服务器的用户名。</param>  
        /// <param name="password">登录电子邮件服务器的用户密码。</param>  
        /// <param name="isBodyHtml">邮件的正文是否是HTML格式。</param>  
        public Mail(string from, string fromName, Dictionary<string, string> recipient, string subject, string body, string host, int port, string username, string password, bool isBodyHtml)
            : this(from, fromName, recipient, subject, body, host, 25, username, password, isBodyHtml, null)
        {
        }
        /// <summary>  
        /// 初始化一个电子邮件操作类的实例。  
        /// </summary>  
        /// <param name="from">发件人的电子邮件地址。</param>  
        /// <param name="fromName">发件人的姓名。</param>  
        /// <param name="recipient">收件人的电子邮件地址。</param>  
        /// <param name="recipientName">收件人的姓名。</param>  
        /// <param name="subject">电子邮件的主题。</param>  
        /// <param name="body">电子邮件的内容。</param>  
        /// <param name="host">电子邮件的服务器地址。</param>  
        /// <param name="port">电子邮件的主机端口号。</param>  
        /// <param name="username">登录电子邮件服务器的用户名。</param>  
        /// <param name="password">登录电子邮件服务器的用户密码。</param>  
        /// <param name="isBodyHtml">邮件的正文是否是HTML格式。</param>  
        public Mail(string from, string fromName, string recipient, string recipientName, string subject, string body, string host, int port, string username, string password, bool isBodyHtml)
            : this(from, fromName, recipient, recipientName, subject, body, host, 25, username, password, isBodyHtml, null)
        {
        }
        /// <summary>  
        /// 初始化一个电子邮件操作类的实例。  
        /// </summary>  
        /// <param name="from">发件人的电子邮件地址。</param>  
        /// <param name="fromName">发件人的姓名。</param>  
        /// <param name="recipient">{"Name","Email"}。</param>
        /// <param name="subject">电子邮件的主题。</param>  
        /// <param name="body">电子邮件的内容。</param>  
        /// <param name="host">电子邮件的服务器地址。</param>  
        /// <param name="username">登录电子邮件服务器的用户名。</param>  
        /// <param name="password">登录电子邮件服务器的用户密码。</param>  
        public Mail(string from, string fromName, Dictionary<string, string> recipient, string subject, string body, string host, string username, string password)
            : this(from, fromName, recipient, subject, body, host, 25, username, password, false, null)
        {
        }
        /// <summary>  
        /// 初始化一个电子邮件操作类的实例。  
        /// </summary>  
        /// <param name="from">发件人的电子邮件地址。</param>  
        /// <param name="fromName">发件人的姓名。</param>  
        /// <param name="recipient">收件人的电子邮件地址。</param>  
        /// <param name="recipientName">收件人的姓名。</param>  
        /// <param name="subject">电子邮件的主题。</param>  
        /// <param name="body">电子邮件的内容。</param>  
        /// <param name="host">电子邮件的服务器地址。</param>  
        /// <param name="username">登录电子邮件服务器的用户名。</param>  
        /// <param name="password">登录电子邮件服务器的用户密码。</param>  
        public Mail(string from, string fromName, string recipient, string recipientName, string subject, string body, string host, string username, string password)
            : this(from, fromName, recipient, recipientName, subject, body, host, 25, username, password, false, null)
        {
        }
        /// <summary>  
        /// 初始化一个电子邮件操作类的实例（邮件的正文非HTML格式，端口默认25）。  
        /// </summary>  
        /// <param name="from">发件人的电子邮件地址。</param>  
        /// <param name="recipient">收件人的电子邮件地址集合。</param>  
        /// <param name="subject">电子邮件的主题。</param>  
        /// <param name="body">电子邮件的内容。</param>  
        /// <param name="host">电子邮件的服务器地址。</param>  
        /// <param name="username">登录电子邮件服务器的用户名。</param>  
        /// <param name="password">登录电子邮件服务器的用户密码。</param>  
        public Mail(string from, List<string> recipient, string subject, string body, string host, string username, string password)
            : this(from, null, null, null, subject, body, host, 25, username, password, false, null)
        {
            recipient = recipient.Distinct().ToList();
            foreach (string item in recipient)
            {
                this._recipientDictionary.Add(null, item.Trim());
            }
        }
        /// <summary>  
        /// 初始化一个电子邮件操作类的实例（邮件的正文非HTML格式，端口默认25）。  
        /// </summary>  
        /// <param name="from">发件人的电子邮件地址。</param>  
        /// <param name="recipient">收件人的电子邮件地址。</param>  
        /// <param name="subject">电子邮件的主题。</param>  
        /// <param name="body">电子邮件的内容。</param>  
        /// <param name="host">电子邮件的服务器地址。</param>  
        /// <param name="username">登录电子邮件服务器的用户名。</param>  
        /// <param name="password">登录电子邮件服务器的用户密码。</param>  
        public Mail(string from, string recipient, string subject, string body, string host, string username, string password)
            : this(from, null, recipient, null, subject, body, host, 25, username, password, false, null)
        {
        }
        /// <summary>  
        /// 初始化一个电子邮件操作类的实例（邮件的正文非HTML格式，端口默认25）。  
        /// </summary>  
        /// <param name="from">发件人的电子邮件地址。</param>  
        /// <param name="recipient">收件人的电子邮件地址集合。</param>  
        /// <param name="subject">电子邮件的主题。</param>  
        /// <param name="body">电子邮件的内容。</param>  
        /// <param name="port">电子邮件的主机端口号。</param>  
        /// <param name="host">电子邮件的服务器地址。</param>  
        /// <param name="username">登录电子邮件服务器的用户名。</param>  
        /// <param name="password">登录电子邮件服务器的用户密码。</param>  
        public Mail(string from, List<string> recipient, string subject, string body, string host, int port, string username, string password)
            : this(from, null, null, null, subject, body, host, port, username, password, false, null)
        {
            recipient = recipient.Distinct().ToList();
            foreach (string item in recipient)
            {
                this._recipientDictionary.Add(null, item.Trim());
            }
        }

        private string _subject;
        private string _body;
        private string _from;
        private string _fromName;
        private string _serverHost;
        private int _serverPort;
        private string _username;
        private string _password;
        private bool _isBodyHtml;
        private Dictionary<string, string> _recipientDictionary;
        private List<string> _attachment = new List<string> { };

        /// <summary>  
        /// 收件人列表。Name Email
        /// </summary>  
        public Dictionary<string, string> RecipientDictionary
        {
            get { return this._recipientDictionary; }
            set { this._recipientDictionary = value; }
        }

        /// <summary>  
        /// 获取或设置邮件的主题。  
        /// </summary>  
        public string Subject
        {
            get { return this._subject; }
            set { this._subject = value; }
        }

        /// <summary>  
        /// 获取或设置邮件的正文内容。  
        /// </summary>  
        public string Body
        {
            get { return this._body; }
            set { this._body = value; }
        }

        /// <summary>  
        /// 获取或设置发件人的邮件地址。  
        /// </summary>  
        public string From
        {
            get { return this._from; }
            set { this._from = value; }
        }

        /// <summary>  
        /// 获取或设置发件人的姓名。  
        /// </summary>  
        public string FromName
        {
            get { return this._fromName; }
            set { this._fromName = value; }
        }

        /// <summary>  
        /// 获取或设置邮件服务器主机地址。  
        /// </summary>  
        public string ServerHost
        {
            get { return this._serverHost; }
            set { this._serverHost = value; }
        }

        /// <summary>  
        /// 获取或设置邮件服务器的端口号。  
        /// </summary>  
        public int ServerPort
        {
            set { this._serverPort = value; }
            get { return this._serverPort; }
        }
        
        /// <summary>  
        /// 获取或设置SMTP认证时使用的用户名。  
        /// </summary>  
        public string Username
        {
            get { return this._username; }
            set
            {
                if (value.Trim() != "")
                {
                    this._username = value.Trim();
                }
                else
                {
                    this._username = "";
                }
            }
        }

        /// <summary>  
        /// 获取或设置SMTP认证时使用的密码。  
        /// </summary>  
        public string Password
        {
            set { this._password = value; }
            get { return this._password; }
        }

        /// <summary>  
        /// 获取或设置指示邮件正文是否为 Html 格式的值。  
        /// </summary>  
        public bool IsBodyHtml
        {
            get { return this._isBodyHtml; }
            set { this._isBodyHtml = value; }
        }

        /// <summary>  
        /// 获取电子邮件附件。  
        /// </summary>  
        public List<string> Attachment
        {
            get { return this._attachment; }
            set { this._attachment = value; }
        }

        /// <summary>  
        /// 添加电子邮件附件。  
        /// </summary>  
        /// <param name="fileList">文件列表。</param>  
        /// <returns>是否添加成功。</returns>  
        public bool AddAttachment(params string[] fileList)
        {
            if (fileList.Length > 0)
            {
                foreach (string file in fileList)
                {
                    if (file != null)
                    {
                        this._attachment.Add(file);
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>  
        /// 发送电子邮件。  
        /// </summary>  
        /// <returns>是否发送成功。</returns>  
        public bool Send()
        {
            if (this.RecipientDictionary.Count > 0)
            {
                //初始化 MailMessage 对象。  
                MailMessage mail = new MailMessage();
                Encoding encoding = Encoding.GetEncoding("utf-8");
                mail.From = new MailAddress(this.From, this.FromName, encoding);
                //这里添加多个收件人
                foreach (var item in this.RecipientDictionary)
                {
                    mail.To.Add(new MailAddress(item.Value, item.Key));
                }
                mail.Subject = this.Subject;
                mail.IsBodyHtml = this.IsBodyHtml;
                mail.Body = this.Body;
                mail.Priority = MailPriority.Normal;
                mail.BodyEncoding = encoding;
                if (this.Attachment.Count > 0)
                {
                    foreach (string file in this.Attachment)
                    {
                        mail.Attachments.Add(new Attachment(file));
                    }
                }
                //初始化 SmtpClient 对象。  
                SmtpClient smtp = new SmtpClient();
                smtp.Host = this.ServerHost;
                smtp.Port = this.ServerPort;
                smtp.Credentials = new NetworkCredential(this.Username, this.Password);
                if (smtp.Port != 25)
                {
                    smtp.EnableSsl = true;
                }
                try
                {
                    smtp.Send(mail);
                }
                catch (SmtpException ex)
                {
                    string message = ex.Message;
                    return false;
                }
                return true;
            }
            else
                return false;
        }
    }
}
