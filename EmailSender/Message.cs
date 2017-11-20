using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Sender
{
    internal class Message
    {
        private MailMessage mail = new MailMessage();
        private SmtpClient client = new SmtpClient();
        private const string sender = "contacto.misofertas@gmail.com";
        private const string senderPassword = "contrasennia";
        private System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(sender, senderPassword);

        public Message(string subject, string body, List<string> receivers)
        {
            this.mail.From = new MailAddress(sender);
            this.mail.Subject = subject;
            this.mail.Body = body;

            this.client.Host = "smtp.gmail.com";
            this.client.UseDefaultCredentials = false;
            this.client.Port = 587;     
            this.client.EnableSsl = true;
            this.client.Timeout = 10000;
            this.client.Credentials = this.credentials;            
            foreach (string receiver in receivers)
            {
                this.mail.To.Add(new MailAddress(receiver));
            }
        }

        public void sendMail()
        {                        
           this.client.Send(this.mail);
        }
    }
}
