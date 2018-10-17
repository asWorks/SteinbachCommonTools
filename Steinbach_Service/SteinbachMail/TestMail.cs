using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace SteinbachMail
{
    public class TestMail
    {
        public enum MessageType
        {
          HTML,
            Text
        }

        public static void TestSendMail()
        {

            try
            {
                SmtpClient _smtp = new SmtpClient("mail.hostedoffice.ag"); //Adresse des Servers
                _smtp.Timeout = 20000;
                _smtp.Credentials = new NetworkCredential("dev@asWorks.de", "MnAbQHJ2010");
                MailMessage _msg = new MailMessage();
                _msg.From = new MailAddress("x10@asWorks.de");
                _msg.To.Add(new MailAddress("asWorks@yahoo.de"));
                _msg.Subject = "Test Exchange Mail";
                _msg.Body = "Test Exchange Mail";
                

                _smtp.Send(_msg);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static void TestSendMail(string Server, string User, string Password, string from, string To, string subject, string body)
        {
         TestSendMail(Server, User, Password, from, To, subject,  body, MessageType.Text);
        }

       

        public static void TestSendMail(string Server, string User, string Password, string from, string To, string subject, string body,MessageType Type)
        {
            try
            {
                SmtpClient _smtp = new SmtpClient(Server); //Adresse des Servers
                _smtp.Timeout = 20000;
                _smtp.UseDefaultCredentials = false;
                _smtp.Credentials = new NetworkCredential(User, Password);
                MailMessage _msg = new MailMessage();
                _msg.From = new MailAddress(from);
                _msg.To.Add(new MailAddress(To));
                _msg.Subject = subject;
                _msg.IsBodyHtml = Type == MessageType.HTML ? true : false;
                _msg.Body = body;
                

                _smtp.Send(_msg);
            }
            catch (Exception ex)
            {
                throw;



            }

        }


        public static void TestSendMail(string Server, string User, string Password, string from, string To, string subject, string body,string[] Attachments,MessageType Type)
        {
            try
            {
                SmtpClient _smtp = new SmtpClient(Server); //Adresse des Servers
                _smtp.Timeout = 20000;
                _smtp.UseDefaultCredentials = false;
                _smtp.Credentials = new NetworkCredential(User, Password);
                MailMessage _msg = new MailMessage();
                _msg.From = new MailAddress(from);
                _msg.To.Add(new MailAddress(To));
                _msg.Subject = subject;
                _msg.IsBodyHtml = Type == MessageType.HTML ? true : false;
                _msg.Body = body;
                

                if (Attachments.Length > 0)
                {
                    foreach (string item in Attachments)
                    {
                        Attachment att = new Attachment(item);
                        _msg.Attachments.Add(att);
                        
                    }
                }

                _smtp.Send(_msg);
            }
            catch (Exception ex)
            {
                throw;



            }

        }

        //Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
        //    // Add time stamp information for the file.
        //    ContentDisposition disposition = data.ContentDisposition;
        //    disposition.CreationDate = System.IO.File.GetCreationTime(file);
        //    disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
        //    disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
        //    // Add the file attachment to this e-mail message.
        //    message.Attachments.Add(data);



    }
}
