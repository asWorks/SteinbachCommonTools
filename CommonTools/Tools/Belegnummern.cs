using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Drawing;

namespace CommonTools.Tools
{
    public class Belegnummern
    {
        public static string GetBelegnummerProjekt(string Belegart, int id_projekt)
        {

            try
            {
                using (var db = new SteinbachEntities())
                {
                    string KK;

                    projekt p = db.projekte.Where(n => n.id == id_projekt).SingleOrDefault();
                    if (p.FirmenNr != null && p.FirmenNr != 0)
                    {
                        var pArt = db.StammProjektTypen.Where(n => n.Projekttyp == p.type).SingleOrDefault();
                        var Firma = db.firmen.Where(n => n.KdNr == p.FirmenNr).SingleOrDefault();
                        KK = Firma.kurzname;
                        if (p.FirmenNr == 10177)  //Jets
                        {
                            if (p.schiff == 1)
                            {
                                KK += "S";
                            }
                            if (p.landx == 1)
                            {
                                KK += "L";
                            }
                        }

                        var ba = db.StammBelegarten.Where(n => n.id == Belegart).SingleOrDefault();

                        //BR 100031-S-E R1
                        string beleg=string.Format("{0} {1}-{2}-{3}",KK,p.projektnummer,pArt.TypKennung,ba.BelegnummerKennung);
                        var BelegCount = db.SI_Belege.Where(n => n.Belegnummer.Trim().Contains(beleg)).Count();
                        beleg += "-" + (BelegCount + 1).ToString();

                    
                        return beleg;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler bei Belegnummernerzeugung.");
                return "Fehler";
            }

            return "Fehler";
        }


        public static string GetBelegnummerOhneProjekt(string Belegart)
        {


            try
            {

                using (var db = new SteinbachEntities())
                {
                    string prefix = string.Format("{0}-{1}-",Belegart.ToUpper(), DateTime.Now.ToString("yy"));

                    var number = from n in db.SI_Belege
                                 where n.Belegnummer.ToUpper().StartsWith(prefix.Trim())
                                 select n.Belegnummer;
                    

                    string num = number.Max();

                    if (num != null)
                    {
                        int len = Belegart.Trim().Length + 4;
                        int newnum = int.Parse(num.Substring(len)) + 1;
                        string pNum = prefix.Trim() + newnum.ToString().PadLeft(6, '0');
                        return pNum;
                    }
                    else
                    {
                        return prefix.Trim() + "000001";
                    }
                }
            }
            catch (Exception ex)
            {

               CommonTools.Tools.ErrorMethods.HandleStandardError(ex, "Fehler bei Belegnummernerzeugung.");
               return "Fehler";
            }

        

          
        }





    }
}
