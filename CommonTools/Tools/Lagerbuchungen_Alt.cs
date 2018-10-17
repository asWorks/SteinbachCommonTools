using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace CommonTools.Lagerbuchungen
{
    public class Lagerbuchungen_Alt
    {

        public static void TransferLagerbestaende()
        {
            using (var db = new SteinbachEntities())
            {
                var ll = db.lagerlisten;
                foreach (var item in ll)
                {
                    var lb = new Lagerbestaende();
                    lb.id_Lagerliste = item.id;
                    lb.id_Lagerort = 1;
                    lb.Lagerbestand = item.anzahlauflager;
                    db.AddToLagerbestaende(lb);
                }

                db.SaveChanges();

            }
        }


        public bool Lagerbuchung(int Quelllager,int Ziellager,int WirkungQuelllager ,int WirkungZiellager ,int Bewegungsmenge,int BewegungsArt,int id_Projekt,int id_Beleg)
        {


            return false;
        }

        public bool Lagerbuchung(int id_Lagerort, int id_Artikel, int BewegungsMenge)
        {
            try
            {
                using (var db = new SteinbachEntities())
                {
                    var lb = db.Lagerbestaende.Where(n => n.id_Lagerliste == id_Artikel && n.id_Lagerort == id_Lagerort).SingleOrDefault();
                    if (lb == null)
                    {
                        lb = new Lagerbestaende();
                        lb.id_Lagerliste = id_Artikel;
                        lb.id_Lagerort = id_Lagerort;
                        lb.Lagerbestand = 0;
                        db.AddToLagerbestaende(lb);
                    }

                    if (!lb.Lagerbestand.HasValue)
                    {
                        lb.Lagerbestand = 0;
                    }

                    lb.Lagerbestand += BewegungsMenge;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Lagerbuchung fehlgeschlagen.");

                return false;
               // throw;

            }
            




        }



        public int GetBestandLagerort(int id_Artikel, int id_Lagerort)
        {
            return Lagerbuchungen_Alt.GetLagerbestand(id_Artikel, id_Lagerort);
        }

        public static int GetLagerbestand(int ArtikelID, int LagerortID)
        {
            if (ArtikelID != null && LagerortID != null)
            {
                using (var db = new SteinbachEntities())
                {
                    var lo = db.Lagerbestaende.Where(n => n.id_Lagerliste == ArtikelID && n.id_Lagerort == LagerortID).SingleOrDefault();
                    if (lo != null)
                    {

                        return (int)lo.Lagerbestand;
                    }

                }


            }

            return 0;

        }

    }
}
