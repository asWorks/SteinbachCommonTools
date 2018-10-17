using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DAL.Tools;
using System.Diagnostics;



using System.Windows;
using System.Data;
using System.Globalization;

namespace SteinbachMail
{
    public class CheckMailReminder
    {
        bool TestMode = false;
        string AlternativeMailAdress = string.Empty;
        readonly bool MailTimerActive = false;


        public CheckMailReminder()
        {

            using (var dbCon = new SteinbachEntities())
            {
                TestMode = DAL.Session.Testmodus;
                AlternativeMailAdress = DAL.Session.TestMailadresse;
                MailTimerActive = DAL.Session.MailTimerActiveSMTP;

            }


             
        }

        public static string GetEnvironment()
        {

            var sb = new StringBuilder();
            sb.Append("Testmodus :");
            sb.Append(Session.Testmodus.ToString());
            sb.Append(" - ");
            sb.Append(Session.SMTPServer);
            sb.Append(" - ");
            sb.Append(Session.EMailZahlungFaellig);
            sb.Append(" - ");
            sb.Append(Session.EMailZahlungErfolgt);
            sb.Append(" - ");
            sb.Append(Session.MailTimerIntervalSMTP.ToString());


            return sb.ToString();

        }

        public bool checkMail()
        {

            const int VerarbeitungFehler = 0;
            const int MailLieferzeitVersandt = 1;
            const int MailZahlungFaelligSIVersandt = 2;
            const int MailZahlungFaelligAnfrageVersandt = 3;
            const int MailZahlungErfolgtSIKunden = 4;
            const int MailZahlungErfolgtSILieferanten = 5;
            const int MailZahlungErfolgtAnfrageKunden = 6;
            const int MailZahlungErfolgtAnfrageLieferanten = 7;
            const int MailProjektVerlauf = 8;






            if (MailTimerActive == false)
            {
                LoggingTool.addDatabaseLogging("EMail", 0, "Mailtimer ist deaktiviert");
                return false;
            }
            DateTime vd;
            DateTime bd;



            using (SteinbachEntities db = new SteinbachEntities())
            {
                var sb = new StringBuilder();
                try
                {
                    vd = DateTime.Now.AddDays(14);
                    bd = DateTime.Now;


                    var lz = from l in db.projekt_anlage_lieferzeiten
                             where
                             (l.hassend == 0 || l.hassend == null)
                             && (l.lieferzeit <= vd && l.lieferzeit >= bd)
                             && l.versandtam == null && l.projekt != null
                             select l;

                    if (lz.Count() > 0)
                    {
                        person p;
                        foreach (projekt_anlage_lieferzeit item in lz)
                        {

                            if (item.id_personchange != null && item.id_personchange != 0)
                            {
                                p = GetPerson((int)item.id_personchange);
                            }
                            else
                            {
                                p = GetPerson((int)item.projekt.id_personchange);
                            }


                            if (p != null)
                            {

                                sb.Clear();
                                if (p.email != null && p.email != string.Empty)
                                {
                                    sb.Append("Kundenname: ");
                                    sb.AppendLine(item.projekt.kundenname);
                                    sb.Append("NB-Nr: ");
                                    sb.AppendLine(item.projekt.werftnummer);
                                    sb.Append("Projektnummer: ");
                                    sb.AppendLine(item.projekt.projektnummer);
                                    sb.Append("Liefertermin: ");
                                    sb.AppendLine(GetDate(item.lieferzeit));

                                    if (checkOutlook())
                                    {
                                        if (DoSendMail(p.email, String.Format("Projektdatenbank / Projekt {0} / Lieferzeit", item.projekt.projektnummer), sb.ToString()))
                                        {
                                            item.hassend = 1;

                                            Eventlogging.log(MailLieferzeitVersandt,"Mail versand - Lieferzeit", p.email, String.Format("Projektdatenbank / Projekt {0} / Lieferzeit", item.projekt.projektnummer));

                                            // LoggingTool.addDatabaseLogging("EMail", 99, "Mail versand - Lieferzeit", p.email, String.Format("Projektdatenbank / Projekt {0}/Lieferzeit", item.projekt.projektnummer));
                                        }
                                        else
                                        {
                                            Eventlogging.log(VerarbeitungFehler,EventLogEntryType.Error, "Fehler Mail Lieferzeit - DoSendMail = false ", p.email, String.Format("Projektdatenbank / Projekt {0} / Lieferzeit", item.projekt.projektnummer));
                                            // LoggingTool.addDatabaseLogging("EMail", 99, "Fehler Mail Lieferzeit - DoSendMail = false ", p.email, String.Format("Projektdatenbank / Projekt {0}/Lieferzeit", item.projekt.projektnummer));
                                        }



                                    }
                                    else
                                    {
                                        Eventlogging.log(VerarbeitungFehler,EventLogEntryType.Error, "Fehler Mail Lieferzeit - Outlook nicht bereit: ", p.email, String.Format("Projektdatenbank / Projekt {0}  /Lieferzeit", item.projekt.projektnummer));
                                        // LoggingTool.addDatabaseLogging("EMail", 99, "Fehler Mail Lieferzeit - Outlook nicht bereit: ", p.email, String.Format("Projektdatenbank / Projekt {0}/Lieferzeit", item.projekt.projektnummer));
                                    }
                                }
                            }



                        }

                        db.SaveChanges();
                    }



                    // Mail an Fr. Steinbach das Rechnung fällig. EMail Adresse in Tabelle Config mit Key "EMailZahlungFaellig"
                    vd = DateTime.Now;
                    var rf = from r in db.projekt_si_rgkunden
                             where r.rechnungfaellig <= vd && r.rechnungvom == null && (r.hassend == 0 || r.hassend == null) && r.projekt != null
                             select r;
                    foreach (var item in rf)
                    {
                        sb.Clear();
                        sb.Append("Erinnerung vom ");
                        sb.AppendLine(GetDate(item.rechnungfaellig));
                        sb.Append("Projektnummer : ");
                        sb.AppendLine(item.projekt.projektnummer);
                        sb.Append("Rechnungsnummer: ");
                        sb.AppendLine(item.rechnungsnr ?? " Keine Rechnungsnummer vorhanden.");


                        //var cf = db.config.Where(c => String.Compare(c.mkey, "EMailZahlungFaellig", true) == 0).FirstOrDefault();
                        // var ZahlungFaellig = Session.EMailZahlungFaellig;

                        if (checkOutlook())
                        {
                            try
                            {
                                DoSendMail(Session.EMailZahlungFaellig, String.Format("Projektdatenbank / Projekt {0} / Ausgangsrechnung / Zahlung fällig", item.projekt.projektnummer), sb.ToString());
                                //mail.sendmail(((config)cf).value, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung - Zahlung fällig", item.projekt.projektnummer), sb.ToString());
                                item.hassend = 1;
                                Eventlogging.log(MailZahlungFaelligSIVersandt,"Mail versandt - Ausgangsrechnung fällig  ", Session.EMailZahlungFaellig, String.Format("Projektdatenbank / Projekt {0}/Ausgangsrechnung", item.projekt.projektnummer));
                                // LoggingTool.addDatabaseLogging("EMail", 99, "Mail versandt - Ausgangsrechnung fällig  ", Session.EMailZahlungFaellig , String.Format("Projektdatenbank / Projekt {0}/Ausgangsrechnung", item.projekt.projektnummer));
                            }
                            catch (Exception ex)
                            {
                                Eventlogging.log(VerarbeitungFehler,EventLogEntryType.Error, "Fehler Mail -Ausgangsrechnung fällig ", Session.EMailZahlungFaellig, String.Format("Projektdatenbank / Projekt {0}/Ausgangsrechnung", item.projekt.projektnummer), ex.Message);

                                //LoggingTool.addDatabaseLogging("EMail", 99, "Fehler Mail -Ausgangsrechnung fällig ", Session.EMailZahlungFaellig, String.Format("Projektdatenbank / Projekt {0}/Ausgangsrechnung", item.projekt.projektnummer), ex.Message);

                            }

                        }
                    }

                    db.SaveChanges();


                    // Version für Anfragerechnungen
                    // Mail an Fr. Steinbach das Rechnung fällig. EMail Adresse in Tabelle Config mit Key "EMailZahlungFaellig"
                    vd = DateTime.Now;
                    var rf1 = from r in db.projekt_rechnungenkunde
                             where r.rechnungfaellig <= vd && r.rechnungbezahlt == null && (r.hassend == 0 || r.hassend == null) && r.projekt != null
                             select r;
                    foreach (var item in rf1)
                    {
                        sb.Clear();
                        sb.AppendLine("Vorauftragsrechnung bzw. Sonderprojekte");
                        sb.Append("Erinnerung vom ");
                        sb.AppendLine(GetDate(item.rechnungfaellig));
                        sb.Append("Projektnummer : ");
                        sb.AppendLine(item.projekt.projektnummer);
                        sb.Append("Rechnungsnummer: ");
                        sb.AppendLine(item.rechnungsnummer ?? " Keine Rechnungsnummer vorhanden.");


                        //var cf = db.config.Where(c => String.Compare(c.mkey, "EMailZahlungFaellig", true) == 0).FirstOrDefault();
                        // var ZahlungFaellig = Session.EMailZahlungFaellig;

                        if (checkOutlook())
                        {
                            try
                            {
                                DoSendMail(Session.EMailZahlungFaellig, String.Format("Projektdatenbank / Projekt {0} / Ausgangsrechnung / Zahlung fällig", item.projekt.projektnummer), sb.ToString());
                                //mail.sendmail(((config)cf).value, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung - Zahlung fällig", item.projekt.projektnummer), sb.ToString());
                                item.hassend = 1;
                                Eventlogging.log(MailZahlungFaelligAnfrageVersandt,"Mail versandt - Ausgangsrechnung fällig  ", Session.EMailZahlungFaellig, String.Format("Projektdatenbank / Projekt {0}/Ausgangsrechnung", item.projekt.projektnummer));
                                // LoggingTool.addDatabaseLogging("EMail", 99, "Mail versandt - Ausgangsrechnung fällig  ", Session.EMailZahlungFaellig , String.Format("Projektdatenbank / Projekt {0}/Ausgangsrechnung", item.projekt.projektnummer));
                            }
                            catch (Exception ex)
                            {
                                Eventlogging.log(VerarbeitungFehler,EventLogEntryType.Error, "Fehler Mail -Ausgangsrechnung fällig ", Session.EMailZahlungFaellig, String.Format("Projektdatenbank / Projekt {0}/Ausgangsrechnung", item.projekt.projektnummer), ex.Message);

                                //LoggingTool.addDatabaseLogging("EMail", 99, "Fehler Mail -Ausgangsrechnung fällig ", Session.EMailZahlungFaellig, String.Format("Projektdatenbank / Projekt {0}/Ausgangsrechnung", item.projekt.projektnummer), ex.Message);

                            }

                        }
                    }

                    db.SaveChanges();






                    // Mail an Fr. Seider-Manske das Rechnungseingang erfolgt. EMail Adresse in hinterlegt in Tabelle Config mit Key "EMailZahlungErfolgt"
                    var mb = from b in db.projekt_si_rgkunden
                             where b.rechnungvom != null && (b.emailbezahlt == 0 || b.emailbezahlt == null) && b.id_projekt != null && b.projekt != null
                             select b;

                    foreach (var item in mb)
                    {


                        sb.Clear();
                        sb.Append("Rechnung bezahlt : ");
                        sb.AppendLine(GetDate(item.rechnungvom));
                        sb.Append("Projektnummer : ");
                        sb.AppendLine(item.projekt.projektnummer);
                        sb.Append("Rechnungsnummer: ");
                        sb.AppendLine(item.rechnungsnr ?? " Keine Rechnungsnummer vorhanden.");


                        //var cf1 = db.config.Where(c => String.Compare(c.mkey, "EMailZahlungErfolgt", true) == 0).FirstOrDefault();
                        string ZahlungErfolgt = Session.EMailZahlungErfolgt;

                        if (checkOutlook())
                        {
                            try
                            {

                                DoSendMail(ZahlungErfolgt, String.Format("Projektdatenbank  / Projekt {0}  / Ausgangsrechnung bezahlt", item.projekt.projektnummer), sb.ToString());
                                item.emailbezahlt = 1;
                                Eventlogging.log(MailZahlungErfolgtSIKunden,"Mail versand - Ausgangsrechnung bezahlt ", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer));
                                // LoggingTool.addDatabaseLogging("EMail", 99, "Mail versand - Ausgangsrechnung bezahlt ", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer));
                            }
                            catch (Exception ex)
                            {
                                Eventlogging.log(VerarbeitungFehler,EventLogEntryType.Error, "Fehler Mail - Ausgangsrechnung bezahlt ", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer), ex.Message);
                                // LoggingTool.addDatabaseLogging("EMail", 99, "Fehler Mail - Ausgangsrechnung bezahlt ", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer), ex.Message);

                            }

                        }
                    }

                    db.SaveChanges();

                    // Version Anfragerechnungen 
                    // Mail an Fr. Seider-Manske das Rechnungseingang erfolgt. EMail Adresse in hinterlegt in Tabelle Config mit Key "EMailZahlungErfolgt"
                    var mb1 = from b in db.projekt_rechnungenkunde
                             where b.rechnungbezahlt != null && (b.emailbezahlt == 0 || b.emailbezahlt == null) && b.id_projekt != null && b.projekt != null
                             select b;

                    foreach (var item in mb1)
                    {


                        sb.Clear();
                        sb.AppendLine("Vorauftragsrechnung bzw. Sonderprojekte");
                        sb.Append("Rechnung bezahlt : ");
                        sb.AppendLine(GetDate(item.rechnungbezahlt));
                        sb.Append("Projektnummer : ");
                        sb.AppendLine(item.projekt.projektnummer);
                        sb.Append("Rechnungsnummer: ");
                        sb.AppendLine(item.rechnungsnummer ?? " Keine Rechnungsnummer vorhanden.");


                        //var cf1 = db.config.Where(c => String.Compare(c.mkey, "EMailZahlungErfolgt", true) == 0).FirstOrDefault();
                        string ZahlungErfolgt = Session.EMailZahlungErfolgt;

                        if (checkOutlook())
                        {
                            try
                            {

                                DoSendMail(ZahlungErfolgt, String.Format("Projektdatenbank  / Projekt {0}  / Ausgangsrechnung bezahlt", item.projekt.projektnummer), sb.ToString());
                                item.emailbezahlt = 1;
                                Eventlogging.log(MailZahlungErfolgtAnfrageKunden,"Mail versand - Ausgangsrechnung bezahlt ", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer));
                                // LoggingTool.addDatabaseLogging("EMail", 99, "Mail versand - Ausgangsrechnung bezahlt ", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer));
                            }
                            catch (Exception ex)
                            {
                                Eventlogging.log(VerarbeitungFehler,EventLogEntryType.Error, "Fehler Mail - Ausgangsrechnung bezahlt ", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer), ex.Message);
                                // LoggingTool.addDatabaseLogging("EMail", 99, "Fehler Mail - Ausgangsrechnung bezahlt ", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer), ex.Message);

                            }

                        }
                    }

                    db.SaveChanges();






                    // Mail an Fr. Seider-Manske das Lieferantenrechnung bezahlt. EMail Adresse in hinterlegt in Tabelle Config mit Key "EMailZahlungErfolgt"
                    var lr = from b in db.projekt_si_rglieferanten
                             where b.lieferantbezahlt != null && (b.emailbezahlt == 0 || b.emailbezahlt == null) && b.id_projekt != null && b.projekt != null
                             select b;

                    foreach (var item in lr)
                    {


                        sb.Clear();
                        sb.Append("Rechnung bezahlt : ");
                        sb.AppendLine(GetDate(item.lieferantbezahlt));
                        sb.Append("Projektnummer : ");
                        sb.AppendLine(item.projekt.projektnummer);
                        sb.Append("Rechnungsnummer: ");
                        sb.AppendLine(item.lieferantnummer ?? " Keine Rechnungsnummer vorhanden.");
                        sb.Append("Lieferant: ");
                        sb.AppendLine(item.lieferantname ?? " Kein Lieferantenname vorhanden.");


                        string ZahlungErfolgt = Session.EMailZahlungErfolgt;

                        if (checkOutlook())
                        {
                            try
                            {

                                DoSendMail(ZahlungErfolgt, String.Format("Projektdatenbank  / Projekt {0}  / Lieferantenrechnung bezahlt", item.projekt.projektnummer), sb.ToString());
                                item.emailbezahlt = 1;
                                Eventlogging.log(MailZahlungErfolgtSILieferanten,"Mail versand - Lieferantenrechnung bezahlt ", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer));
                                // LoggingTool.addDatabaseLogging("EMail", 99, "Mail versand - Ausgangsrechnung bezahlt ", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer));
                            }
                            catch (Exception ex)
                            {
                                Eventlogging.log(VerarbeitungFehler,EventLogEntryType.Error, "Fehler Mail - Lieferantenrechnung bezahlt", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer), ex.Message);
                                // LoggingTool.addDatabaseLogging("EMail", 99, "Fehler Mail - Ausgangsrechnung bezahlt ", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer), ex.Message);

                            }

                        }
                    }

                    db.SaveChanges();




                    // Version Anfragerechnungen
                    // Mail an Fr. Seider-Manske das Lieferantenrechnung bezahlt. EMail Adresse in hinterlegt in Tabelle Config mit Key "EMailZahlungErfolgt"
                    var lr1 = from b in db.projekt_rechnungenlieferant
                             where b.rechnungbezahlt != null && (b.emailbezahlt == 0 || b.emailbezahlt == null) && b.id_projekt != null && b.projekt != null
                             select b;

                    foreach (var item in lr1)
                    {


                        sb.Clear();
                        sb.AppendLine("Vorauftragsrechnung bzw. Sonderprojekte");
                        sb.Append("Rechnung bezahlt : ");
                        sb.AppendLine(GetDate(item.rechnungbezahlt));
                        sb.Append("Projektnummer : ");
                        sb.AppendLine(item.projekt.projektnummer);
                        sb.Append("Rechnungsnummer: ");
                        sb.AppendLine(item.rechnungsnummer ?? " Keine Rechnungsnummer vorhanden.");
                        sb.Append("Lieferant: ");
                        sb.AppendLine(item.lieferantname ?? " Kein Lieferantenname vorhanden.");


                        string ZahlungErfolgt = Session.EMailZahlungErfolgt;

                        if (checkOutlook())
                        {
                            try
                            {

                                DoSendMail(ZahlungErfolgt, String.Format("Projektdatenbank  / Projekt {0}  / Lieferantenrechnung bezahlt", item.projekt.projektnummer), sb.ToString());
                                item.emailbezahlt = 1;
                                Eventlogging.log(MailZahlungErfolgtAnfrageLieferanten,"Mail versand - Lieferantenrechnung Anfragerechnungen bezahlt ", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer));
                                // LoggingTool.addDatabaseLogging("EMail", 99, "Mail versand - Ausgangsrechnung bezahlt ", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer));
                            }
                            catch (Exception ex)
                            {
                                Eventlogging.log(VerarbeitungFehler,EventLogEntryType.Error, "Fehler Mail - Lieferantenrechnung bezahlt", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer), ex.Message);
                                // LoggingTool.addDatabaseLogging("EMail", 99, "Fehler Mail - Ausgangsrechnung bezahlt ", ZahlungErfolgt, String.Format("Projektdatenbank / Projekt {0}/ Ausgangsrechnung bezahlt", item.projekt.projektnummer), ex.Message);

                            }

                        }
                    }

                    db.SaveChanges();






                    // Mail wenn in Projektverlauf Datum gesetzt wurde
                    vd = DateTime.Now.AddDays(-4);
                    bd = DateTime.Now;

                    var pv = from p in db.projekt_verlaufe
                             where p.datum >= vd && p.datum <= bd && (p.hassend == 0 || p.hassend == null) && p.id_projekt != null
                             select p;
                    foreach (var item in pv)
                    {

                        sb.Clear();
                        sb.Append("Erinnerung vom : ");
                        sb.AppendLine(GetDate(item.datum));
                        sb.AppendLine("Bemerkung :");
                        sb.AppendLine(item.bemerkung);

                        var cf1 = db.personen.Where(c => c.id == item.id_personchange).FirstOrDefault();

                        if (checkOutlook())
                        {


                            if (DoSendMail(((person)cf1).email, String.Format("Projektdatenbank / Projekt {0} / Verlauf", item.projekt.projektnummer), sb.ToString()))
                            {
                                item.hassend = 1;
                                Eventlogging.log(MailProjektVerlauf,"Mail versandt - Projektverlauf ", ((person)cf1).email, String.Format("Projektdatenbank / Projekt {0}/ Projektverlauf", item.projekt.projektnummer), item.datum.ToString(), item.bemerkung);
                                // LoggingTool.addDatabaseLogging("EMail", 99, "Mail versandt - Projektverlauf ", ((person)cf1).email, String.Format("Projektdatenbank / Projekt {0}/ Projektverlauf", item.projekt.projektnummer), item.datum.ToString(), item.bemerkung);
                            }
                            else
                            {
                                Eventlogging.log(VerarbeitungFehler,EventLogEntryType.Error, "Fehler Mail - Projektverlauf ", ((person)cf1).email, String.Format("Projektdatenbank / Projekt {0}/ verlauf", item.projekt.projektnummer), item.datum.ToString(), item.bemerkung);
                                // LoggingTool.addDatabaseLogging("EMail", 99, "Fehler Mail - Projektverlauf ", ((person)cf1).email, String.Format("Projektdatenbank / Projekt {0}/ verlauf", item.projekt.projektnummer), item.datum.ToString(), item.bemerkung);
                            }





                        }

                    }


                    db.SaveChanges();
                    return true;
                }




                //return false;





                catch (Exception ex)
                {

                    db.SaveChanges();
                    Eventlogging.log(VerarbeitungFehler,EventLogEntryType.Error, "Mailversand gescheitert", ex.Message, ex.InnerException == null ? "no innerException" : ex.InnerException.Message);
                    //LoggingTool.addDatabaseLogging("EMail", 99, "Mailversand gescheitert", ex.Message, ex.InnerException == null ? "no innerException" : ex.InnerException.Message);
                    LoggingTool.LogExeption(ex, "checkMail", "");
                    return false;

                }
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

        private person GetPerson(int ID)
        {

            using (var db = new SteinbachEntities())
            {

                var query = from p in db.personen
                            where p.id == ID
                            select p;


                return query.FirstOrDefault();


            }

        }

        private bool checkOutlook()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                LoggingTool.addDatabaseLogging("EMail", 99, "Outlookfehler", ex.Message);
                return false;
            }

        }

        private string GetDate(DateTime? dt)
        {
            DateTime ndt = (DateTime)dt;
            System.Globalization.Calendar objCal = CultureInfo.CurrentCulture.Calendar;
            int weekofyear = objCal.GetWeekOfYear(ndt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);


            return String.Format("{0:d} -  Kalenderwoche : {1}", ndt, weekofyear);
        }

    }
}
