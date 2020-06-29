using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace Mail_Sender
{
    public class MailSender : IMailSender
    {
        #region Properties
        public string SMTPServer { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public PortNumber Port { get; set; }
        public bool SSLEnabled { get; set; }
        public bool UseTLSEncryption { get; set; }
        public List<Person> ToAddress { get; set; }
        public string FromAddress { get; set; }
        public string FromPersonName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SuccessMsg { get; set; }
        public string FailureMsg { get; set; }
        public bool IsBodyHtml { get; set; }
        public System.Text.Encoding BodyEncoding { get; set; }
        #endregion

        #region General
        public MailSender()
        {
            Initializer();
        }
        private void Initializer()
        {
            this.ToAddress = new List<Person>();
            this.Port = PortNumber.Port587;
            this.SSLEnabled = false;
            this.UseTLSEncryption = false;
            this.SuccessMsg = "Message Sent!";
            this.FailureMsg = "Message sending failure";
            this.IsBodyHtml = true;
            this.BodyEncoding = Encoding.UTF8;
        }
        #endregion

        public void AddPerson(string name, string email)
        {
            this.ToAddress.Add(new Person() { Name = name, Email = email });
        }
        public string SendEmail()
        {
            try
            {
                MailMessage mail = CreateMessage();
                SmtpClient SmtpServer = CreateClient();

                SmtpServer.Send(mail);
                return this.SuccessMsg;
            }
            catch(Exception e)
            {
                return this.FailureMsg + ": "+ e.Message;
            }
        }

        private MailMessage CreateMessage()
        {
            MailMessage mail = new MailMessage()
            {
                IsBodyHtml = this.IsBodyHtml,
                BodyEncoding = this.BodyEncoding,
                From = new MailAddress(this.FromAddress, this.FromPersonName),
                Subject = this.Subject,
                Body = this.Body
            };

            foreach (var item in ToAddress)
            {
                mail.To.Add(new MailAddress(item.Email, item.Name));
            }

            return mail;
        }
        private SmtpClient CreateClient()
        {
            SmtpClient client = new SmtpClient(this.SMTPServer);

            client.Port = (int)this.Port;
            client.Credentials = new System.Net.NetworkCredential(this.Username, this.Password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = this.SSLEnabled;
            if (this.UseTLSEncryption)
            {
                client.EnableSsl = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            }

            return client;
        }
    }
}
