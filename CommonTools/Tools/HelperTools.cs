using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Diagnostics;
using CommonTools.Tools;

namespace CommonTools.Tools
{
    public class HelperTools
    {

        public enum ChooseEintraegeIndexField
        {
            id,
            ai_sort,
            id_This_int,
            id_This_string
        }

   

        public static void BuildArtikelLookout()
        {

            string filename = CommonTools.Tools.ConfigEntry<string>.GetConfigEntry("ScannerLookoutDatei", "C:\\CipherLab\\Lookout\\Artikellookout.txt", "");
            try
            {

                int counter = 0;
                int MaxLen = CommonTools.Tools.ConfigEntry<int>.GetConfigEntry("LookoutDateiMaxLenBezeichnung", "50", "");
                int Len = 0;
                System.IO.StreamWriter fs = new System.IO.StreamWriter(filename, false);

                using (var db = new DAL.SteinbachEntities())
                {
                    var query = db.lagerlisten.Where(n => n.artikelnr != string.Empty && n.bezeichnung != string.Empty);
                    foreach (var item in query)
                    {
                        if (!item.artikelnr.Contains("?") && !item.artikelnr.Contains("*") 
                            && !item.artikelnr.Contains(".") && !item.artikelnr.ToLower().Contains("x")) 
                        {
                            if (item.artikelnr != string.Empty)
                            {
                                ++counter;
                                 Len = item.bezeichnung.Length > MaxLen ? MaxLen : item.bezeichnung.Length;
                                 fs.WriteLine(string.Format("{0};{1}",item.artikelnr,item.bezeichnung.Substring(0,Len)));
                            }
                           
                        }
                    }
                    
                    fs.Close();
                    fs = null;
                }

                UserMessage.NotifyUser(string.Format("Datei -> {1} <- mit {0} Einträgen wurde erzeugt.",counter,filename));
            }

            catch (Exception ex)
            {

                ErrorMethods.HandleStandardError(ex);

            }




        }


        public static string GetConfigEntry(string key)
        {
            try
            {
                using (var db = new DAL.SteinbachEntities())
                {

                    string res = db.config.Where(s => s.mkey == key).FirstOrDefault().value;
                    return res;

                }
            }
            catch (Exception ex)
            {
                return "";

            }


        }

        public static string GetConfigEntry(string key, string Default, string Explanation = "")
        {
            try
            {
                using (var db = new DAL.SteinbachEntities())
                {

                    string res = db.config.Where(s => s.mkey == key).FirstOrDefault().value;
                    return res;

                }
            }
            catch (Exception ex)
            {
                try
                {
                    using (var db = new DAL.SteinbachEntities())
                    {
                        var c = new DAL.config { mkey = key, value = Default, Description = Explanation };
                        db.AddToconfig(c);
                        db.SaveChanges();
                        return Default;

                    }
                }
                catch (Exception)
                {

                    return "";
                }


            }


        }


        public static string GetAuswahlEintraegeEntry(int? id)
        {

            if (!id.HasValue)
            {
                return "Eintrag=null";
            }

            using (var db = new DAL.SteinbachEntities())
            {
                var res = db.AuswahlEintraege.Where(i => i.id == (int)id).SingleOrDefault();
                if (res != null)
                {
                    return res.Eintrag;
                }
                else
                {
                    return String.Format("Kein Eintrag({0})", id);
                }
            }


        }

        /// <summary>
        /// Gibt ein IQueryable aua AuswahlEinträge zurück.
        /// </summary>
        /// <param name="Gruppe"></param>
        /// <param name="sort">Sort = 0 sortiert nach ID | sort = 1 Sortiert nach ai_Sort wenn as_Sort nicht gefüllt sortiert nach Eintrag</param>
        /// <returns></returns>
        public static IQueryable GetAuswahlEintraege(string Gruppe, int sort = 0)
        {
            using (var db = new DAL.SteinbachEntities())
            {
                if (sort == 1)
                {
                    var res = db.AuswahlEintraege.Where(ae => ae.Gruppe == Gruppe && ae.ai_sort != null).OrderBy(s => s.ai_sort).ToList();
                    if (res.Count == 0)
                    {
                        res = db.AuswahlEintraege.Where(ae => ae.Gruppe == Gruppe).OrderBy(s => s.Eintrag).ToList();
                    }

                    return res.AsQueryable();
                }
                else
                {
                    var res = db.AuswahlEintraege.Where(ae => ae.Gruppe == Gruppe).OrderBy(s => s.id).ToList();
                    return res.AsQueryable();
                }
            }


        }



        public static List<DAL.AuswahlEintraege> GetAuswahlEintraegeList(string Gruppe, int sort = 0)
        {
            using (var db = new DAL.SteinbachEntities())
            {
                if (sort == 1)
                {
                    var res = db.AuswahlEintraege.Where(ae => ae.Gruppe == Gruppe && ae.ai_sort != null).OrderBy(s => s.ai_sort).ToList();
                    if (res.Count == 0)
                    {
                        res = db.AuswahlEintraege.Where(ae => ae.Gruppe == Gruppe).OrderBy(s => s.Eintrag).ToList();
                    }

                    return res;
                }
                else
                {
                    var res = db.AuswahlEintraege.Where(ae => ae.Gruppe == Gruppe).OrderBy(s => s.id).ToList();
                    return res;
                }
            }


        }




        public static void CheckUpdate(string InstalledPath, string UpdatePath, string UpdaterArgument)
        {

            try
            {

                //MessageBox.Show("Aufruf CheckUpdatePath");
                FileInfo fiUpdate = new FileInfo(Path.Combine(UpdatePath, UpdaterArgument));
                FileInfo fiCurrent = new FileInfo(Path.Combine(InstalledPath, UpdaterArgument));



                if (fiCurrent.Length == 0 || fiUpdate.Length == 0)
                {
                    MessageBox.Show("Fehler");
                }

                string Argument = string.Format("{0},{1},{2}", InstalledPath, UpdatePath, UpdaterArgument);
                //MessageBox.Show(Argument);



                if (fiUpdate.LastWriteTime > fiCurrent.LastWriteTime)
                {
                    if (MessageBox.Show("Es ist ein neues Update verfügbar. Jetzt installieren?", "Updateprüfung", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        //Process P = new Process();
                        //P.StartInfo.FileName = Tools.ConfigEntry<string>.GetConfigEntry("PathUpdater", @"F:\ALLGEMEIN\EDV\asUpdater\asWorksUpdater", "Pfad zur UpdaterExe");
                        //P.StartInfo.Arguments = Argument;
                        //P.Start();


                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = Tools.ConfigEntry<string>.GetConfigEntry("PathUpdater", @"E:\Projects\Test\Steinbach_Test\asWorksUpdater\bin\Release\asWorksUpdater", "Pfad zur UpdaterExe");
                        startInfo.Arguments = Argument;
                        Process.Start(startInfo);



                    }
                }

            }
            catch (FileNotFoundException ex)
            {

                MessageBox.Show("Updateprüfung nicht möglich. Ungültiger Pfad in Updateaufruf");
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }



        }




    }
}
