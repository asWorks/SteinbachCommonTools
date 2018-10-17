using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace CommonTools.ObjectFactories
{
    public class KundenbesuchsBerichteFactory
    {
        public static int DeleteBesuchsbericht(int id)
        {

            string Rights = "admin suCRM";
            if (!Rights.Contains(Session.User.rights))
            {
                return 0;
            }
            
            using (var db = new SteinbachEntities())
            {
                var besuch = db.Firmen_Kundenbesuche.Where(m => m.id == id).SingleOrDefault();
                if (besuch != null)
                {
                    foreach (var item in besuch.Kundenbesuche_TeilnehmerExtern.ToList())
                    {
                        db.DeleteObject(item);
                    }

                    db.DeleteObject(besuch);
                    return db.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);

                }
                else
                {
                    return 0;
                }

            }

        }


        public static Firmen_Kundenbesuch GetNewBesuch()
        {
            Firmen_Kundenbesuch besuch = new Firmen_Kundenbesuch();
            besuch.datum_von = CommonTools.Tools.DateTools.GetTodayWithCustomTime(9,0,0);
            besuch.datum_bis = CommonTools.Tools.DateTools.GetTodayWithCustomTime(12, 0, 0);
            return besuch;


        }


        public static Firmen_Kundenbesuch GetNewBesuch(int firmaID)
        {
            Firmen_Kundenbesuch besuch = new Firmen_Kundenbesuch();
            besuch.datum_von = CommonTools.Tools.DateTools.GetTodayWithCustomTime(9, 0, 0);
            besuch.datum_bis = CommonTools.Tools.DateTools.GetTodayWithCustomTime(12, 0, 0);
            besuch.id_firma = firmaID;
            return besuch;


        }


        //public static int DeleteBesuchsbericht(Firmen_Kundenbesuch besuch)
        //{

        //    using (var db = new SteinbachEntities())
        //    {
                
        //        if (besuch != null)
        //        {
        //            db.DeleteObject(besuch);
        //            return db.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);

        //        }
        //        else
        //        {
        //            return 0;
        //        }

        //    }

        //}

    }
}
