using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Sender
{
    internal class Message
    {
        private MailMessage mail { get; set; }
        private SmtpClient client { get; set; }
        private const string sender = "contacto.misofertas@gmail.com";

        public Message(string subject, string body, List<string> receivers)
        {
            this.mail = new MailMessage();
            this.mail.From = new MailAddress(sender);
            this.mail.Subject = subject;
            this.mail.Body = body;

            this.client = new SmtpClient();
            this.client.Host = "smtp.gmail.com";
            client.UseDefaultCredentials = false;
            this.client.Credentials = new System.Net.NetworkCredential("contacto.misofertas@gmail.com", "contrasennia");
            this.client.Port = 587;     
            this.client.EnableSsl = true;
            //this.client.DeliveryMethod = SmtpDeliveryMethod.Network;
            this.client.Timeout = 10000;
            
            
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
