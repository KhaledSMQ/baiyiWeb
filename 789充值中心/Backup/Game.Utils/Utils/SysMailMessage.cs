namespace Game.Utils
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    public class SysMailMessage
    {
        private string m_accounts;
        private string m_body;
        private string m_from;
        private string m_fromName;
        private bool m_html;
        private bool m_isAsync = false;
        private string m_mailDomain;
        private int m_mailserverport;
        private string m_password;
        private string m_recipient;
        private string m_recipientName;
        private string m_subject;

        public bool AddRecipient(params string[] username)
        {
            this.m_recipient = username[0].Trim();
            return true;
        }

        private string Base64Encode(string str)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(str));
        }

        public bool Send()
        {
            MailMessage message = new MailMessage();
            Encoding displayNameEncoding = Encoding.GetEncoding("utf-8");
            message.From = new MailAddress(this.From, this.Subject, displayNameEncoding);
            message.To.Add(this.m_recipient);
            message.Subject = this.Subject;
            message.IsBodyHtml = true;
            message.Body = this.Body;
            message.Priority = MailPriority.Normal;
            message.BodyEncoding = Encoding.GetEncoding("utf-8");
            SmtpClient client = new SmtpClient {
                Host = this.MailDomain,
                Port = this.MailDomainPort,
                Credentials = new NetworkCredential(this.MailServerUserName, this.MailServerPassWord)
            };
            if (this.MailDomainPort != 0x19)
            {
                client.EnableSsl = true;
            }
            try
            {
                if (this.IsAsync)
                {
                    string userToken = "userToken";
                    client.SendAsync(message, userToken);
                }
                else
                {
                    client.Send(message);
                    message.Dispose();
                }
            }
            catch (SmtpException exception)
            {
                string str2 = exception.Message;
                TextLogger.Write(exception.ToString());
                return false;
            }
            return true;
        }

        public string Body
        {
            get
            {
                return this.m_body;
            }
            set
            {
                this.m_body = value;
            }
        }

        public string From
        {
            get
            {
                return this.m_from;
            }
            set
            {
                this.m_from = value;
            }
        }

        public string FromName
        {
            get
            {
                return this.m_fromName;
            }
            set
            {
                this.m_fromName = value;
            }
        }

        public bool Html
        {
            get
            {
                return this.m_html;
            }
            set
            {
                this.m_html = value;
            }
        }

        public bool IsAsync
        {
            get
            {
                return this.m_isAsync;
            }
            set
            {
                this.m_isAsync = value;
            }
        }

        public string MailDomain
        {
            get
            {
                return this.m_mailDomain;
            }
            set
            {
                this.m_mailDomain = value;
            }
        }

        public int MailDomainPort
        {
            get
            {
                return this.m_mailserverport;
            }
            set
            {
                this.m_mailserverport = value;
            }
        }

        public string MailServerPassWord
        {
            get
            {
                return this.m_password;
            }
            set
            {
                this.m_password = value;
            }
        }

        public string MailServerUserName
        {
            get
            {
                return this.m_accounts;
            }
            set
            {
                if (value.Trim() != "")
                {
                    this.m_accounts = value.Trim();
                }
                else
                {
                    this.m_accounts = "";
                }
            }
        }

        public string RecipientName
        {
            get
            {
                return this.m_recipientName;
            }
            set
            {
                this.m_recipientName = value;
            }
        }

        public string Subject
        {
            get
            {
                return this.m_subject;
            }
            set
            {
                this.m_subject = value;
            }
        }
    }
}

