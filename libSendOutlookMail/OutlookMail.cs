using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;



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
            object mailItem = null;
            object myApp = null;

            try
            {
                // create Outlook Application via late binding so no interop assembly is required at compile time
                var prog = System.Type.GetTypeFromProgID("Outlook.Application");
                myApp = Activator.CreateInstance(prog);

                // olMailItem == 0
                mailItem = myApp.GetType().InvokeMember("CreateItem", System.Reflection.BindingFlags.InvokeMethod, null, myApp, new object[] { 0 });
                mailItem.GetType().GetProperty("Subject")?.SetValue(mailItem, subject);
                mailItem.GetType().GetProperty("To")?.SetValue(mailItem, address);
                mailItem.GetType().GetProperty("Body")?.SetValue(mailItem, message);

                // Send the email to the customer
                mailItem.GetType().InvokeMember("Send", System.Reflection.BindingFlags.InvokeMethod, null, mailItem, null);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                // release COM objects to avoid leaving Outlook process running
                try { if (mailItem != null) Marshal.ReleaseComObject(mailItem); } catch { }
                mailItem = null;
                try { if (myApp != null) Marshal.ReleaseComObject(myApp); } catch { }
                myApp = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }


        }


            public static void sendmail(string address, string subject, string TextMessage, string HTMLMessage, string[] Attachments, MessageType messageType)
        {

            object mailItem = null;
            object myApp = null;

            try
            {
                var prog = System.Type.GetTypeFromProgID("Outlook.Application");
                myApp = Activator.CreateInstance(prog);

                // olMailItem == 0
                mailItem = myApp.GetType().InvokeMember("CreateItem", System.Reflection.BindingFlags.InvokeMethod, null, myApp, new object[] { 0 });
                mailItem.GetType().GetProperty("Subject")?.SetValue(mailItem, subject);
                mailItem.GetType().GetProperty("To")?.SetValue(mailItem, address);

                if (messageType == MessageType.html)
                {
                    mailItem.GetType().GetProperty("HTMLBody")?.SetValue(mailItem, HTMLMessage);
                }
                else if (messageType == MessageType.text)
                {
                    mailItem.GetType().GetProperty("Body")?.SetValue(mailItem, TextMessage);
                }

                if (Attachments != null && Attachments.Length > 0)
                {
                    foreach (string item in Attachments)
                    {
                        // add attachment explicitly using late-binding
                        var attachments = mailItem.GetType().GetProperty("Attachments")?.GetValue(mailItem);
                        if (attachments != null)
                        {
                            // olByValue == 1
                            attachments.GetType().InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, attachments, new object[] { item, 1, System.Type.Missing, System.Type.Missing });
                        }
                    }
                }

                // Send the email to the customer
                mailItem.GetType().InvokeMember("Send", System.Reflection.BindingFlags.InvokeMethod, null, mailItem, null);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                // release COM objects to avoid leaving Outlook process running
                try { if (mailItem != null) Marshal.ReleaseComObject(mailItem); } catch { }
                mailItem = null;
                try { if (myApp != null) Marshal.ReleaseComObject(myApp); } catch { }
                myApp = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
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
                // create Outlook Application via late binding
                var prog = System.Type.GetTypeFromProgID("Outlook.Application");
                object application = Activator.CreateInstance(prog);

                // The Namespace Object (Session) has a collection of accounts.
                object session = application.GetType().GetProperty("Session")?.GetValue(application);
                object accounts = session?.GetType().GetProperty("Accounts")?.GetValue(session);

                // Concatenate a message with information about all accounts.
                StringBuilder builder = new StringBuilder();

                if (accounts != null)
                {
                    foreach (object account in (System.Collections.IEnumerable)accounts)
                    {
                        try
                        {
                            var displayName = account.GetType().GetProperty("DisplayName")?.GetValue(account) as string;
                            var userName = account.GetType().GetProperty("UserName")?.GetValue(account) as string;
                            var smtp = account.GetType().GetProperty("SmtpAddress")?.GetValue(account) as string;
                            var acctTypeObj = account.GetType().GetProperty("AccountType")?.GetValue(account);

                            builder.AppendFormat("DisplayName: {0}\n", displayName);
                            builder.AppendFormat("UserName: {0}\n", userName);
                            builder.AppendFormat("SmtpAddress: {0}\n", smtp);

                            builder.Append("AccountType: ");
                            int acctType = acctTypeObj != null ? Convert.ToInt32(acctTypeObj) : -1;
                            switch (acctType)
                            {
                                case 0:
                                    builder.AppendLine("Exchange");
                                    break;
                                case 1:
                                    builder.AppendLine("Http");
                                    break;
                                case 2:
                                    builder.AppendLine("Imap");
                                    break;
                                case 3:
                                    builder.AppendLine("Other");
                                    break;
                                case 4:
                                    builder.AppendLine("Pop3");
                                    break;
                                default:
                                    builder.AppendLine("Unknown");
                                    break;
                            }

                            builder.AppendLine();
                        }
                        finally
                        {
                            try { if (account != null) Marshal.ReleaseComObject(account); } catch { }
                        }
                    }
                }

                // release session and application
                try { if (accounts != null) Marshal.ReleaseComObject(accounts); } catch { }
                try { if (session != null) Marshal.ReleaseComObject(session); } catch { }
                try { if (application != null) Marshal.ReleaseComObject(application); } catch { }

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
