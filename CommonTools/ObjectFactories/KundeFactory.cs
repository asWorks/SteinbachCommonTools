using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace CommonTools.ObjectFactories
{
    public class KundeFactory
    {
        public static bool DeleteCustumer(int id)
        {

            using (var db = new SteinbachEntities())
            {

                var cust = db.firmen.Where(c => c.id == id).SingleOrDefault();
                if (cust != null)
                {
                    db.DeleteObject(cust);
                    db.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);

                }

            }

            return false;

        }

        public static bool DeleteFirmenAdresse(Firmen_Adressen adresse, SteinbachEntities db)
        {
            if (adresse.Firmen_Personen != null && adresse.Firmen_Personen.Count > 0)
            {
                var fp = adresse.Firmen_Personen.ToList();
                foreach (var item in fp)
                {
                    DeleteFirmenPersonen(item, db);
                }
            }

            db.DeleteObject(adresse);
            return true;
        }

        public static bool DeleteFirmenPersonen(Firmen_Personen person,SteinbachEntities db)
        {
            db.DeleteObject(person);
            return true;

        }

       




    }
}
