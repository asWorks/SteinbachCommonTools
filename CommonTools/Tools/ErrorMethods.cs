using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DAL.Tools;
using DAL;
using asOutlookMail;

namespace CommonTools.Tools
{
    public static class ErrorMethods
    {
        public static string GetExceptionMessageInfo(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Es ist folgender Fehler aufgetreten");
            sb.AppendLine();
            sb.AppendLine(ex.Message);

            sb.AppendLine(ex.InnerException == null ? "No inner exception" : ex.InnerException.Message);
            sb.AppendLine("Source = ");
            sb.AppendLine(ex == null ? "" : ex.Source);
            sb.AppendLine();
            sb.AppendLine(CleanMessage(ex.StackTrace));


            return sb.ToString();

        }

        public static string GetExceptionMessageInfo(string Meldung, Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Meldung);
            sb.AppendLine();
            sb.AppendLine(GetExceptionMessageInfo(ex));


            return sb.ToString();

        }


        public static void HandleStandardError(Exception ex)
        {
            MessageBox.Show(ErrorMethods.GetExceptionMessageInfo(ex));
        }

        public static void HandleStandardError(Exception ex, string Message)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine(Message);
            //sb.Append(ErrorMethods.GetExceptionMessageInfo(ex));
            //MessageBox.Show(sb.ToString());
            HandleStandardError(ex, Message, false);
        }

        public static void HandleStandardError(Exception ex, string Message,bool logMessage = false)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Message);
            sb.Append(ErrorMethods.GetExceptionMessageInfo(ex));
            if (logMessage)
            {
                DAL.Tools.LoggingTool.addDatabaseLogging("", 0, DAL.Tools.LoggingTool.LogState.medium,ex.Message, Message);
            }
            
            MessageBox.Show(sb.ToString());
        }


           public static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message);

        }


        /// <summary>
        /// Gibt Fehlermeldung je nach Einstellung in Config als MessageBox, LogEintrag in DB oder beides aus.
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowErrorMessage(Exception ex)
        {
            int res = int.Parse(HelperTools.GetConfigEntry("AusgabeFehlermeldungException"));

            switch (res)
            {
                case 0:
                    {

                        break;
                    }
                case 1:
                    {
                        MessageBox.Show(GetExceptionMessageInfo(ex));
                        break;
                    }
                case 2:
                    {
                        DAL.Tools.LoggingTool.addDatabaseLogging("0", 0, DAL.Tools.LoggingTool.LogState.high, GetExceptionMessageInfo(ex));

                        break;
                    }

                case 3:
                    {
                        DAL.Tools.LoggingTool.addDatabaseLogging("0", 0, DAL.Tools.LoggingTool.LogState.high, GetExceptionMessageInfo(ex));
                        MessageBox.Show(GetExceptionMessageInfo(ex));

                        break;
                    }

                default:
                    {
                        break;
                    }

            }



        }


        public static void ShowErrorMessageToUser(Exception ex, string Message, String ClassName = "", string MethodName = "")
        {
            // string UserMessage = string.Empty;
            DAL.Tools.LoggingTool.addDatabaseLogging("0", 0, DAL.Tools.LoggingTool.LogState.high, GetExceptionMessageInfo(ex));
            // string email = DAL.Session.User != null ? DAL.Session.User.email : DAL.Session.EMailZahlungFaellig;
            string messaage = GetExceptionMessageInfo(ex);
            StringBuilder nst = new StringBuilder();
            nst.AppendLine(messaage);
            nst.AppendLine(DateTime.Now.ToString());
            nst.AppendLine("User : " + Session.GetValidUser.benutzername);

            string sentTo = CommonTools.Tools.HelperTools.GetConfigEntry("EmailService");
         
            // SMTP Mail funktinieren nur intern - Sicherheitseinstellung ?
            // SendServerMail.DoSendMail(Session.SMTPServer, Session.UsernameSMTP, Session.PasswordSMTP,Session.GetValidUser.email, sentTo, "Fehlermeldung Steinbach", nst.ToString());
         
            if (CommonTools.Tools.HelperTools.GetConfigEntry("SendMailErrorMessage") == "1")
            {
                string adress = CommonTools.Tools.HelperTools.GetConfigEntry("EmailService");
                if (adress != string.Empty)
                {
                    // Deshalb Client Outlook verwenden.
                    OutlookMail.sendmail(adress, "Fehlermeldung Steinberg CRM", messaage);
                }

            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Es ist ein unerwarteter Fehler aufgetreten");
            sb.AppendLine("Ein Fehlerprotokoll wurde erstellt.");
            sb.AppendLine("Die Anwendung wird geschlossen");


            MessageBox.Show(sb.ToString());
            Application.Current.Shutdown(99);
        }



        /// <summary>
        /// Logt Fehlermeldung je nach Einstellung in Config als LogEintrag in DB.
        /// Gibt Fehlermeldung als MessageBox wenn bMessageAnyWay == true;
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowErrorMessage(Exception ex, bool bMessageboxAnyWay)
        {
            int res = int.Parse(HelperTools.GetConfigEntry("AusgabeFehlermeldungException"));

            if (bMessageboxAnyWay)
            {
                MessageBox.Show(GetExceptionMessageInfo(ex));
            }

            switch (res)
            {
                case 0:
                    {

                        break;
                    }
                case 1:
                    {

                        break;
                    }
                case 2:
                    {
                        DAL.Tools.LoggingTool.addDatabaseLogging("0", 0, DAL.Tools.LoggingTool.LogState.high, GetExceptionMessageInfo(ex));

                        break;
                    }

                case 3:
                    {
                        DAL.Tools.LoggingTool.addDatabaseLogging("0", 0, DAL.Tools.LoggingTool.LogState.high, GetExceptionMessageInfo(ex));


                        break;
                    }

                default:
                    {
                        break;
                    }

            }





        }

        private static string CleanMessage(string message)
        {
            StringBuilder sb = new StringBuilder();
            char[] delimiters = new char[] { '\r', '\n' };

            var st = message.Split(delimiters, StringSplitOptions.None);
            foreach (var item in st)
            {
                if (!item.Contains("System."))
                {
                    sb.AppendLine(item);
                }
            }

            return sb.ToString();

        }

    }
}
