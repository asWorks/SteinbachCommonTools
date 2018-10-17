using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace CommonTools.EntitiesActions
{
    public class FirmaActions
    {
        public static firma GetNewFirma(SteinbachEntities db)
        {

            firma i = new firma();


            i.istFirma = 3;
            i.IstKunde = 1;
            i.KdNr = (int)db.firmen.Max(id => id.id) + 10001;
            i.name = "Neue Firma - KdNr. : " + i.KdNr.ToString();
            i.istVerarbeitet = 1;

            i.created = DateTime.Now;
            i.AngelegtAm = DateTime.Now;
            i.AngelegtVon = Session.User.id;
            db.AddTofirmen(i);
            return i;

        }
    }
}
