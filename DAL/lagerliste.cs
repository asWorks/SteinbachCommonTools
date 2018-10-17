using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace DAL
{
    public partial class lagerliste
    {


        public lagerliste()
        {
            created = DateTime.Now;
            //artikelnr = "{ neu }";
            Handelsware = 0;
            using (var db = new SteinbachEntities())
            {
                wkz = db.StammWaehrungen.Where(n => n.istStandardWaehrung == 1).SingleOrDefault().WKZ;
            }
            
            


        }




        private ObservableCollection<vw_LagerbestaendeLagerorte> _LagerbestaendeView;
        public ObservableCollection<vw_LagerbestaendeLagerorte> LagerbestaendeView
        {
            get { return _LagerbestaendeView; }
            set
            {
                if (value != _LagerbestaendeView)
                {
                    _LagerbestaendeView = value;
                    OnPropertyChanged("LagerbestaendeView");

                }
            }
        }


        private int? _Gesamtbestand;
        public int? Gesamtbestand
        {
            get { return _Gesamtbestand; }
            set
            {
                if (value != _Gesamtbestand)
                {
                    _Gesamtbestand = value;
                    OnPropertyChanged("Gesamtbestand");

                }
            }
        }


        partial void OnidChanged()
        {

            LagerbestaendeView = GetLagerbestaende(id);

        }


        partial void OnpreiseuroChanged()
        {
            if (preiseuro.HasValue && preiseuro != 0)
            {
                if (!sonderpreis.HasValue || sonderpreis == 0)
                {
                    preisbrutto = Tools.ParseFormula.GetCalculation((decimal)preiseuro, 1);
                    preisvom = DateTime.Now;


                }
            }


        }

        partial void OnsonderpreisChanged()
        {
            Console.WriteLine(sonderpreis.ToString());
        }



        private ObservableCollection<vw_LagerbestaendeLagerorte> GetLagerbestaende(int id)
        {
            using (var db = new SteinbachEntities())
            {
                var Bestaende = new ObservableCollection<vw_LagerbestaendeLagerorte>(db.vw_LagerbestaendeLagerorte.Where(k => k.id_Lagerliste == id));
                int? gesamt = 0;
                foreach (var item in Bestaende)
                {
                    gesamt += item.Lagerbestand;
                }
                Gesamtbestand = gesamt;
                return Bestaende;

            }

        }






    }
}
