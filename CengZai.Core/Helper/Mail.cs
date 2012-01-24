using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;


namespace CengZai.Helper
{

    /// <summary>
    ///Email 的摘要说明
    /// </summary>
    public class Mail
    {
        //用户名：task 密码：sxmobi
        public static string mFrom = "曾在网<no-reply@cengzai.com>";  //外面配置用：快乐网[service@kuaile.us]
        public static string mUsername = "no-reply@cengzai.com";
        public static string mPassword = "lingmon";
        public static string mSmtpServer = "smtp.exmail.qq.com";
        public static int mSmtpPort = 25;//端口号

        static Mail()
        {
            InitConfig(Config.MailFrom, Config.MailUserName, Config.MailPassword, Config.MailSmtpServer, Config.MailSmtpPort);
        }

        /// <summary>
        /// 配置邮件
        /// </summary>
        /// <param name="from"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="smtpServer"></param>
        public static void InitConfig(string from, string username, string password, string smtpServer, int port)
        {
            if (from != "")
            {
                mFrom = from.Replace('[', '<').Replace(']', '>');
            }
            if (username != "")
            {
                mUsername = username;
            }
            if (password != "")
            {
                mPassword = password;
            }
            if (smtpServer != "")
            {
                mSmtpServer = smtpServer;
            }
            if (port > 0)
            {
                mSmtpPort = port;
            }
        }

        /// <summary>
        /// 发送邮件，默认内容为HTML
        /// </summary>
        /// <param name="to"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public static void Send(string to, string title, string content)
        {
            Send(to, "", title, content, true);
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="isHtml"></param>
        public static void Send(string to, string title, string content, bool isHtml)
        {
            Send(to, "", title, content, isHtml);
        }

        /// <summary>
        /// 发送邮件，默认内容为HTML
        /// </summary>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public static void Send(string to, string cc, string title, string content)
        {
            Send(to, cc, title, content, true);
        }


        /// <summary>
        /// 新版
        /// </summary>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="isHtml"></param>
        public static void Send(string to, string cc, string title, string content, bool isHtml)
        {
            to = to.Replace('[', '<').Replace(']', '>');
            cc = cc.Replace('[', '<').Replace(']', '>');

            
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.From = new System.Net.Mail.MailAddress(mFrom);
            message.IsBodyHtml = isHtml;
            message.Subject = title;
            message.Body = content;

            string[] arrTo = to.Split(';');
            string[] arrCc = cc.Split(';');

            //添加多接收人
            foreach(string feTo in arrTo)
            {
                if (feTo.IndexOf('@') != -1)
                {
                    try
                    {
                        message.To.Add(new System.Net.Mail.MailAddress(feTo));
                    }
                    catch(Exception ex)
                    {
                        Log.AddErrorInfo("Email地址非法：" + feTo + ",原因：" + ex.Message);
                        //WebLog.WriteErrLog("Email地址非法：" + feTo + ",原因：" + ex.Message);
                    }
                }
            }

            //添加多抄送人
            foreach (string feCc in arrCc)
            {
                if (feCc.IndexOf('@') != -1)
                {
                    try
                    {
                        message.CC.Add(new System.Net.Mail.MailAddress(feCc));
                    }
                    catch (Exception ex)
                    {
                        Log.AddErrorInfo("Email地址非法：" + feCc + ",原因：" + ex.Message);
                        //WebLog.WriteErrLog("Email地址非法：" + feCc + ",原因：" + ex.Message);
                    }
                }
            }

            try
            {

                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(mSmtpServer, mSmtpPort);
                smtpClient.Credentials = new System.Net.NetworkCredential(mUsername, mPassword);
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                //WebLog.WriteErrLog("发送邮件出错！异常：" + ex.Message);
                throw ex;   //底层不捕捉异常
            }
            
        }



    }


    

}
