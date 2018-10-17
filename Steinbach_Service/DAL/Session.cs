using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DAL.Tools;
using System.Windows.Threading;

namespace DAL
{
    public static class Session
    {
        private static person _user;
        public static int CurrentProjectID;
        private static int _LogState = 0;
        private static Dictionary<string, string> _Filterstrings;

        public static Dictionary<string, string> Filterstrings
        {
            get
            {
                if (_Filterstrings == null)
                {
                    _Filterstrings = new Dictionary<string, string>();
                }
                return _Filterstrings;
            }
            set { _Filterstrings = value; }
        }



        public static bool IsUserLoggedOn
        {
            get
            {
                return (_user != null);

            }
        }

        public static person User
        {
            get
            {
                return _user;
            }
        }

        public static int LogState
        {
            get
            {
                try
                {
                    using (var db = new SteinbachEntities())
                    {
                        var res = db.config.Where(s => s.mkey == "LogState").FirstOrDefault();
                        _LogState = int.Parse(res.value);

                    }

                    return _LogState;
                }
                catch (Exception)
                {

                    return 0;
                }

            }

        }



        public static bool MailTimerActive
        {
            get
            {
                RefreshUser();
                try
                {
                    var res = (int)DAL.Session.User.MailTimerActive;
                    return res == 0 ? false : true;
                }
                catch (Exception)
                {
                    return true;
                }


            }

        }

        public static bool MailTimerActiveSMTP
        {
            get
            {
                RefreshUser();
                try
                {
                    using (var db = new SteinbachEntities())
                    {
                        var res = db.config.Where(s => s.mkey == "MailTimerActive").FirstOrDefault();
                        return long.Parse(res.value) == 0 ? false : true ;
                    }

                }
                catch (Exception)
                {
                    return true;
                }


            }

        }

        public static long MailTimerIntervall
        {
            get
            {
                RefreshUser();
                try
                {
                    var res = (long)DAL.Session.User.MailTimerInterval;
                    return res;
                }
                catch (Exception ex)
                {
                    DAL.Tools.LoggingTool.LogExeption(ex, "Session.cs", "MailTimerIntervall", LoggingTool.LogState.high);
                    return 3600;

                }




            }

        }

        public static long MailTimerIntervalSMTP
        {
            get
            {
                try
                {
                    using (var db = new SteinbachEntities())
                    {
                        var res = db.config.Where(s => s.mkey == "MailTimerInterval").FirstOrDefault();
                        return long.Parse(res.value);
                    }
                }
                catch (Exception ex)
                {
                    DAL.Tools.LoggingTool.LogExeption(ex, "Session.cs", "MailTimerIntervalSMTP", LoggingTool.LogState.high);
                    return 3600;
                }

            }

        }

        public static bool ProjekteAufklappen
        {
            get
            {
                RefreshUser();
                try
                {
                    var res = (int)DAL.Session.User.ProjekteAufklappen;
                    return res == 0 ? false : true;
                }
                catch (Exception ex)
                {
                    DAL.Tools.LoggingTool.LogExeption(ex, "Session.cs", "ProjekteAufklappen", LoggingTool.LogState.high);
                    return true;
                }


            }

        }

        public static bool Testmodus
        {
            get
            {
                try
                {
                    using (var db = new SteinbachEntities())
                    {
                        var res = db.config.Where(s => s.mkey == "Testmodus").FirstOrDefault();
                        return int.Parse(res.value) == 0 ? false : true;
                    }
                }
                catch (Exception ex)
                {
                    DAL.Tools.LoggingTool.LogExeption(ex, "Session.cs", "Testmodus", LoggingTool.LogState.high);
                    return false;
                }

            }

        }

        public static string TestMailadresse
        {
            get
            {
                try
                {
                    using (var db = new SteinbachEntities())
                    {
                        var res = db.config.Where(s => s.mkey == "TestMailadresse").FirstOrDefault().value;
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    DAL.Tools.LoggingTool.LogExeption(ex, "Session.cs", "TestMailadresse", LoggingTool.LogState.high);
                    return "me@asWorks.de";
                }

            }

        }

        public static string EMailZahlungFaellig
        {
            get
            {
                try
                {
                    using (var db = new SteinbachEntities())
                    {
                        var res = db.config.Where(s => s.mkey == "EMailZahlungFaellig").FirstOrDefault().value;
                        return res;
                    }

                }
                catch (Exception ex)
                {
                    DAL.Tools.LoggingTool.LogExeption(ex, "Session.cs", "EMailZahlungFaellig", LoggingTool.LogState.high);
                    return "me@asWorks.de";
                }

            }

        }

        public static string EMailZahlungErfolgt
        {
            get
            {
                try
                {
                    using (var db = new SteinbachEntities())
                    {
                        var res = db.config.Where(s => s.mkey == "EMailZahlungErfolgt").FirstOrDefault().value;
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    DAL.Tools.LoggingTool.LogExeption(ex, "Session.cs", "EMailZahlungErfolgt", LoggingTool.LogState.high);
                    return "me@asWorks.de";
                }

            }

        }

        public static string SMTPServer
        {
            get
            {
                try
                {
                    using (var db = new SteinbachEntities())
                    {
                        var res = db.config.Where(s => s.mkey == "SMTPServer").FirstOrDefault().value;
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    DAL.Tools.LoggingTool.LogExeption(ex, "Session.cs", "SMTPServer", LoggingTool.LogState.high);
                    return "mail.hostedoffice.ag";
                }

            }

        }

        public static string UsernameSMTP
        {
            get
            {
                try
                {
                    using (var db = new SteinbachEntities())
                    {
                        var res = db.config.Where(s => s.mkey == "UsernameSMTP").FirstOrDefault().value;
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    DAL.Tools.LoggingTool.LogExeption(ex, "Session.cs", "UsernameSMTP", LoggingTool.LogState.high);
                    return "dev@asWorks.de";
                }

            }

        }

        public static string PasswordSMTP
        {
            get
            {
                try
                {
                    using (var db = new SteinbachEntities())
                    {
                        var res = db.config.Where(s => s.mkey == "PasswordSMTP").FirstOrDefault().value;
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    DAL.Tools.LoggingTool.LogExeption(ex, "Session.cs", "PasswordSMTP", LoggingTool.LogState.high);
                    return "MnAbQHJ2010";
                }

            }

        }

        private static bool RefreshUser()
        {
            try
            {
                using (var db = new SteinbachEntities())
                {
                    if (_user != null)
                    {
                        var query = from p in db.personen
                                    where p.benutzername == _user.benutzername && p.passwort == _user.passwort
                                    select p;

                        _user = query.FirstOrDefault();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }



        }



        public static bool Login(string user, string password)
        {

            try
            {



                using (var db = new SteinbachEntities())
                {

                    var query = from p in db.personen
                                where p.benutzername == user && p.passwort == password
                                select p;

                    _user = query.FirstOrDefault();

                    if (_user == null)
                        return false;

                }
                return true;
            }

            catch (Exception ex)
            {


                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Beim Verbinden mit der Datenbank ist ein Fehler aufgetreten");
                sb.AppendLine();
                sb.AppendLine(ex.Message);
                if (ex.InnerException != null)
                {
                    sb.AppendLine(ex.InnerException.Message);
                }
                System.Windows.MessageBox.Show(sb.ToString());
                return false;


            }

        }
    }
}