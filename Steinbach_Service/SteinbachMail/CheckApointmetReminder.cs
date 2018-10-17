using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DAL.Tools;


namespace SteinbachMail
{
    public class CheckApointmetReminder
    {
        const int VerarbeitungFehler = 0;
        const int MailAppointment = 10;


        bool TestMode = false;
        string AlternativeMailAdress = string.Empty;


        public CheckApointmetReminder()
        {
            using (var dbCon = new SteinbachEntities())
            {
                TestMode = DAL.Session.Testmodus;
                AlternativeMailAdress = DAL.Session.TestMailadresse;


            }

        }


        public void CheckAppointments()
        {


            try
            {
                StringBuilder sb = new StringBuilder();

                using (SteinbachEntities db = new SteinbachEntities())
                {

                    DateTime Von = DateTime.Now.AddMinutes(-30);
                    DateTime Bis = DateTime.Now.AddMinutes(15);

                    var ap = from t in db.CRMTermine
                             where (t.EmailSent == null || t.EmailSent == 0) && t.Erinnerung == 1
                                 && t.ErinerungDatum >= Von && t.ErinerungDatum <= Bis
                             select t;



                    foreach (var item in ap)
                    {
                        var m = from i in db.Termine_TeilnehmerSI
                                where i.id_Termin == item.id
                                select i;

                        foreach (var itemM in m)
                        {
                            string appShort = string.Empty;
                            person Person = (from p in db.personen
                                             where p.id == itemM.id_Teilnehmer
                                             select p).SingleOrDefault();
                            sb.Clear();
                            sb.Append("Termin von :");
                            sb.AppendLine(item.TerminVon.ToString());
                            sb.Append("Termin bis :");
                            sb.AppendLine(item.TerminBis.ToString());
                            sb.AppendLine();
                            sb.Append("Betreff :");
                            sb.AppendLine(item.Betreff);
                            sb.AppendLine();
                            sb.AppendLine("Details :");
                            sb.AppendLine(item.Details);
                            appShort = sb.ToString();

                            DoSendMail(Person.email, "EMail Erinnerung für " + item.Betreff, sb.ToString());
                            Eventlogging.log(MailAppointment,"Mail versandt an :" + Person.email, "Termin : " + item.id.ToString() ,appShort );
                        }

                        item.EmailSent = 1;
                        item.EmailSentDate = DateTime.Now;


                    }
                    db.SaveChanges();


                }
            }
            catch (Exception ex)
            {
               
                Eventlogging.log(VerarbeitungFehler,System.Diagnostics.EventLogEntryType.Error,"Fehler bei EMailversand Terminerinnerung"
                    ,ex.Message
                    ,ex.InnerException == null ? "" :ex.InnerException.Message);
            }




        }



        private bool DoSendMail(string address, string desc, string Message)
        {
            try
            {
                if (TestMode)
                {

                    // LoggingTool.addDatabaseLogging("EMail", 99, "Mailversand Testmode", AlternativeMailAdress);
                    TestMail.TestSendMail(Session.SMTPServer, Session.UsernameSMTP, Session.PasswordSMTP, Session.UsernameSMTP, AlternativeMailAdress, desc, Message);
                    Eventlogging.log("Mailversand Testmode", AlternativeMailAdress);
                    // mail.sendmail(AlternativeMailAdress, desc, Message);
                    return true;


                }
                else
                {
                    // LoggingTool.addDatabaseLogging("EMail", 99, "Mailversand Realmode", address);
                    TestMail.TestSendMail(Session.SMTPServer, Session.UsernameSMTP, Session.PasswordSMTP, Session.UsernameSMTP, address, desc, Message);
                    Eventlogging.log("Mailversand Realmode", address);
                    // mail.sendmail(address, desc, Message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LoggingTool.addDatabaseLogging("EMail", 99, "Fehler bei Aufruf DoSendMail", address, desc, Message, ex.Message);
                return false;
            }

        }
    }
}
