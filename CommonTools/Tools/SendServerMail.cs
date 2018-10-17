using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace CommonTools.Tools
{
    public class SendServerMail
    {
        public static void DoSendMail(string Server, string User, string Password, string from, string To, string subject, string body)
        {
            try
            {
                SmtpClient _smtp = new SmtpClient(Server); //Adresse des Servers
                _smtp.Timeout = 20000;
                _smtp.Credentials = new NetworkCredential(User, Password);
                MailMessage _msg = new MailMessage();
                _msg.From = new MailAddress(from);
                _msg.To.Add(new MailAddress(To));
                _msg.Subject = subject;
                _msg.Body = body;

                _smtp.Send(_msg);
            }
            catch (Exception ex)
            {
                throw;



            }
        }
    }
}
