using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Windows;

namespace CommonTools.ObjectFactories
{
    public class TerminFactory
    {

        public static int DeleteTermine(int id)
        {


            try
            {
                using (var db = new SteinbachEntities())
                {


                    var adr = db.CRMTermine.Where(t => t.id == id).SingleOrDefault();
                    //TeilnehmerSI Löschen wegen Ref. Integritätfehler
                    //var tsi = db.Termine_TeilnehmerSI.Where(s => s.id_Termin == adr.id);
                    foreach (var teilnehmerSI in adr.Termine_TeilnehmerSI.ToList())
                    {
                        db.DeleteObject(teilnehmerSI);
                    }
                    //TeilnehmerExtern Löschen wegen Ref. Integritätfehler
                    //var tex = db.Termin_TeilnehmerExtern.Where(t => t.id_Termin == adr.id);
                    foreach (var TeilnehmerExtern in adr.Termin_TeilnehmerExtern.ToList())
                    {
                        db.DeleteObject(TeilnehmerExtern);
                    }
                    //Termin löschen
                    db.DeleteObject(adr);

                   return db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }










        }
    }
}
