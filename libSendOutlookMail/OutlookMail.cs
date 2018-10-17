using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Outlook;
using Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;



namespace asOutlookMail
{

    public enum MessageType
    {
        text, html

    }

    public class OutlookMail
    {

        //private MailItem mailItem = null;
        //private Microsoft.Office.Interop.Outlook.TaskItem taskItem = null;
        //private int counter = 0;

        public static void sendmail(string address, string subject, string message)
        {

            MailItem mailItem = null;
            Microsoft.Office.Interop.Outlook.TaskItem taskItem = null;
            int counter = 0;

            try
            {
                Microsoft.Office.Interop.Outlook.Application myApp = new Microsoft.Office.Interop.Outlook.Application();


                //  ae = new AddressEntry


                mailItem = ((MailItem)myApp.CreateItem((OlItemType.olMailItem)));
                mailItem.Subject = subject;

                mailItem.To = address;
                mailItem.Body = message;

                // Send the email to the customer
                ((_MailItem)mailItem).Send();

            }

            catch (System.Exception ex)
            {

                throw;


            }


        }


        public static void sendmail(string address, string subject, string TextMessage, string HTMLMessage, string[] Attachments, MessageType Type)
        {

            MailItem mailItem = null;
            Microsoft.Office.Interop.Outlook.TaskItem taskItem = null;
            int counter = 0;

            try
            {
                Microsoft.Office.Interop.Outlook.Application myApp = new Microsoft.Office.Interop.Outlook.Application();


                //  ae = new AddressEntry


                mailItem = ((MailItem)myApp.CreateItem((OlItemType.olMailItem)));
                mailItem.Subject = subject;

                mailItem.To = address;
                


                if (Type == MessageType.html)
                {
                    mailItem.HTMLBody = HTMLMessage;
                }
                else if (Type == MessageType.text)
                {
                    mailItem.Body = TextMessage;
                }

                if (Attachments.Length > 0)
                {
                    foreach (string item in Attachments)
                    {
                        
                      //  Attachment att = new Attachment(item);
                        mailItem.Attachments.Add(item);

                    }
                }


                // Send the email to the customer
                ((_MailItem)mailItem).Send();

            }

            catch (System.Exception ex)
            {

                throw;


            }


        }


    //    private void TestSendWord()
    //    {

    //        Microsoft.Office.Interop.Word.Application wd;
    //        Microsoft.Office.Interop.Word.Document doc;
    //        MailItem itm;
    //  String ID;
    // Boolean blnWeOpenedWord;

    // wd = new Microsoft.Office.Interop.Word.Application();
    
    //wd.Visible = true;
    
    //doc = wd.Documents.Open("C:\\Users\\this\toEmail.doc");
    //itm = doc.MailEnvelope.item;
    //itm.To = "this@email.com";
    //itm.Subject = "My Subject";
    //itm.Send();
    
    
    //doc.Close();
    //wd.Quit();
  
    //doc = null;
    //itm = null;
    //wd = null;


    //    }



        public static string DisplayAccountInformation()
        {
            try
            {


                Microsoft.Office.Interop.Outlook.Application application = new Microsoft.Office.Interop.Outlook.Application();

                // The Namespace Object (Session) has a collection of accounts. 
                Accounts accounts = application.Session.Accounts;

                // Concatenate a message with information about all accounts. 
                StringBuilder builder = new StringBuilder();

                // Loop over all accounts and print detail account information. 
                // All properties of the Account object are read-only. 
                foreach (Account account in accounts)
                {

                    // The DisplayName property represents the friendly name of the account. 
                    builder.AppendFormat("DisplayName: {0}\n", account.DisplayName);

                    // The UserName property provides an account-based context to determine identity. 
                    builder.AppendFormat("UserName: {0}\n", account.UserName);

                    // The SmtpAddress property provides the SMTP address for the account. 
                    builder.AppendFormat("SmtpAddress: {0}\n", account.SmtpAddress);

                    // The AccountType property indicates the type of the account. 
                    builder.Append("AccountType: ");
                    switch (account.AccountType)
                    {

                        case OlAccountType.olExchange:
                            builder.AppendLine("Exchange");
                            break;

                        case OlAccountType.olHttp:
                            builder.AppendLine("Http");
                            break;

                        case OlAccountType.olImap:
                            builder.AppendLine("Imap");
                            break;

                        case OlAccountType.olOtherAccount:
                            builder.AppendLine("Other");
                            break;

                        case OlAccountType.olPop3:
                            builder.AppendLine("Pop3");
                            break;
                    }

                    builder.AppendLine();
                }

                // Display the account information. 
                return builder.ToString();

            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);

            }
            //System.Windows.Forms.MessageBox.Show(builder.ToString());
        }

    }
}
